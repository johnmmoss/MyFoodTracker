# Manually create resource group: myfoodtracker-rg-dev-uks 
data "azurerm_resource_group" "rg" {
  name = "${var.application}-rg-${var.environment}-${var.loc}"
}

resource "azurerm_service_plan" "sp" {
  name                = "${var.application}-sp-${var.environment}-${var.loc}"
  resource_group_name = data.azurerm_resource_group.rg.name
  location            = data.azurerm_resource_group.rg.location
  os_type             = "Windows"
  sku_name            = "F1"
}

resource "azurerm_windows_web_app" "api" {
  name                = "${var.application}-api-${var.environment}-${var.loc}"
  resource_group_name = data.azurerm_resource_group.rg.name
  location            = azurerm_service_plan.sp.location
  service_plan_id     = azurerm_service_plan.sp.id

  site_config {
    always_on = false
    cors {
      allowed_origins = [trimsuffix(azurerm_storage_account.storage.primary_web_endpoint, "/")]
    }
  }

    app_settings = {
      ASPNETCORE_ENVIRONMENT = "Production"
    }

  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_storage_account" "storage" {
  name                     = "${var.application_short}web${var.environment}${var.loc}"
  resource_group_name      = data.azurerm_resource_group.rg.name
  location                 = data.azurerm_resource_group.rg.location
  account_replication_type = "LRS"
  account_tier             = "Standard"
  static_website {
    error_404_document = "error.html"
    index_document     = "index.html"
  }
}

resource "azurerm_storage_container" "container" {
  name                 = "main"
  storage_account_name = azurerm_storage_account.storage.name
}

## Application insights

resource "azurerm_application_insights" "appinsights" {
  name                = "${var.application}-ai-${var.environment}-${var.loc}"
  resource_group_name = data.azurerm_resource_group.rg.name
  location            = data.azurerm_resource_group.rg.location
  application_type    = "web"
  retention_in_days   = 30
}

## Permission for the api to access the storage

resource "azurerm_role_assignment" "example" {
  scope                = azurerm_storage_account.storage.id
  role_definition_name = "Storage Blob Data Contributor"
  principal_id         = azurerm_windows_web_app.api.identity[0].principal_id
}