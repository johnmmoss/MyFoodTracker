import { Food } from "../Types/Food";
import { getApiUrl } from "../configuration";

export const getFoodList = async () : Promise<Food[]| null> => {
      var response = await fetch(getApiUrl("food"), { method: "GET"});
      console.log(getApiUrl("food"));
      if(!response.ok)
      {
            throw new Error("An error occurred getting food data");
      }
      var food = await response.json() as Food[];
      console.log(food);
      return food ? food : null;
  }