import { Food, NewFood } from "../Types/Food";
import { getApiUrl } from "../configuration";

export const getFoodList = async (): Promise<Food[] | null> => {
   var response = await fetch(getApiUrl("food"), { method: "GET" });
   var food = await response.json() as Food[];
   return food ? food : null;
}

export const saveFood = async (food:NewFood)  => {
   var content = JSON.stringify(food);
   await fetch(getApiUrl("food"), { 
      method: "POST",
      body: content,
      headers: {
         'Content-Type': 'application/json'
     }
   }).catch((e) => console.log(e));
}