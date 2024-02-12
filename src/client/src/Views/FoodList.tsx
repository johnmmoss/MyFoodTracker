import React, { useEffect } from "react";
import { AppPage } from "../Components/AppPage";
import { getFoodList } from "../Services/FoodService";
import { Food } from "../Types/Food";

export const FoodList = () => {
  const pageTitle = "My Foods";
  const [foodList, setFoodList] = React.useState<Food[]>([]);
  useEffect(() => {
    getFoodList().then((foods) => {
      if (foods?.length) {
        console.log(foods);
        setFoodList(foods);
      }
    });
  },[]);
  return (
    <>
      <AppPage pageTitle={pageTitle}>
        <table cellPadding={2} cellSpacing={1} border={1}>
          <tr>
            <th>Name</th>
            <th>Protein</th>
            <th>Fat</th>
            <th>Carbohydrate</th>
            <th>Calories</th>
          </tr>
          {foodList == null ? (
            <div>loading</div>
          ) : (
            foodList.map((x) => <FoodRow food={x} />)
          )}
        </table>
      </AppPage>
    </>
  );
};

type FoodRowProps = {
  food: Food;
};
const FoodRow = ({ food }: FoodRowProps) => {
  return (
    <>
      <tr>
        <td>{food.name}</td>
        <td>{food.nutritionalInfo.protein}</td>
        <td>{food.nutritionalInfo.fat}</td>
        <td>{food.nutritionalInfo.carbohydrate}</td>
        <td>{food.nutritionalInfo.calories}</td>
      </tr>
    </>
  );
};
