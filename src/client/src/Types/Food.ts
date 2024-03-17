export type Food = {
      name: string
      nutritionalInfo:NutritionalInfo
}

export type NutritionalInfo = {
      quantity:number,
      protein:number,
      fat:number,
      carbohydrate:number,
      fibre:number,
      calories:number
}

export type NewFood = {
   name:string,
   protein:number,
   fat:number,
   carbs:number,
   calories:number
}