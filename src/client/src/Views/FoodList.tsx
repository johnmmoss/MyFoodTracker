import React, { useEffect } from "react";
import { AppPage } from "../Components/AppPage";
import { getFoodList } from "../Services/FoodService";
import { Food } from "../Types/Food";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";

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
   }, []);

   return (
      <>
         <AppPage pageTitle={pageTitle}>
            {foodList == null ? (
               <div>loading</div>
            ) : (
               <TableContainer component={Paper}>
                  <Table
                     sx={{ minWidth: 650 }}
                     size="small"
                     aria-label="a dense table"
                  >
                     <TableHead>
                        <TableRow>
                           <TableCell>Food (100g serving)</TableCell>
                           <TableCell align="right">Calories</TableCell>
                           <TableCell align="right">Fat&nbsp;(g)</TableCell>
                           <TableCell align="right">Carbs&nbsp;(g)</TableCell>
                           <TableCell align="right">Protein&nbsp;(g)</TableCell>
                        </TableRow>
                     </TableHead>
                     <TableBody>
                        {foodList.map((row) => (
                           <TableRow
                              key={row.name}
                              sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                           >
                              <TableCell component="th" scope="row">
                                 {row.name}
                              </TableCell>
                              <TableCell align="right">
                                 {row.nutritionalInfo.calories}
                              </TableCell>
                              <TableCell align="right">
                                 {row.nutritionalInfo.fat}
                              </TableCell>
                              <TableCell align="right">
                                 {row.nutritionalInfo.carbohydrate}
                              </TableCell>
                              <TableCell align="right">
                                 {row.nutritionalInfo.protein}
                              </TableCell>
                           </TableRow>
                        ))}
                     </TableBody>
                  </Table>
               </TableContainer>
            )}
         </AppPage>
      </>
   );
};
