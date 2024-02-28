terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = ">= 3.83.0"
    }
  }

  backend "azurerm" {
    resource_group_name  = "myfoodtracker-rg-dev-uks"
    storage_account_name = "mfttfdevuks"
    container_name       = "tfstate"
    key                  = "terraform.tfstate"
  }

  required_version = ">= 1.6.5"
}

provider "azurerm" {
  features {}
}

module "api" {
  environment = "dev"
  source      = "../../resources"
}
