import React, { useEffect } from "react";
import { AppPage } from "../Components/AppPage";
import { deleteFood, getFoodList, saveFood } from "../Services/FoodService";
import { Food, NewFood } from "../Types/Food";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import TextField from "@mui/material/TextField";
import Paper from "@mui/material/Paper";
import { Box, Button, CircularProgress, Modal } from "@mui/material";
import { When } from "../Components/When";

const modalStyle = {
   position: 'absolute' as 'absolute',
   top: '30%',
   left: '50%',
   transform: 'translate(-50%, -50%)',
   width: 500,
   bgcolor: 'background.paper',
   border: '2px solid #ccc',
   boxShadow: 24,
   p: 4,
};

export const FoodList = () => {

   const [foodList, setFoodList] = React.useState<Food[] | null>(null);

   type FormValidationErrors = {
      name: boolean,
      protein: boolean,
      fat: boolean,
      carbs: boolean,
      calories: boolean
   }

   const [validationError, setValidationError] = React.useState<FormValidationErrors>({
      name: false,
      protein: false,
      fat: false,
      carbs: false,
      calories: false
   });

   const [addFormOpen, setAddFormOpen] = React.useState(false);
   const openAddForm = () => setAddFormOpen(true);
   const closeAddForm = () => {
      setAddFormOpen(false);
      setValidationError({
         name: false,
         protein: false,
         fat: false,
         carbs: false,
         calories: false
      })
   };

   const [currentDeleteId, setCurrentDeleteId] = React.useState("");
   const [deleteFormOpen, setDeleteFormOpen] = React.useState(false);
   const openDeleteForm = () => setDeleteFormOpen(true);
   const closeDeleteForm = () => {
      setCurrentDeleteId("");
      setDeleteFormOpen(false);
   }

   const handleDeleteClick = (event: React.MouseEvent<HTMLButtonElement>) => {
      let deleteId = event.currentTarget.getAttribute("data-id");
      setCurrentDeleteId(deleteId!);
      openDeleteForm();
      console.log("deleteId:" + currentDeleteId);
   }

   const handleDeleteFoodItem = () => {
      deleteFood(currentDeleteId).then(() => {
         loadFood();
         closeDeleteForm()
      })
   }

   useEffect(() => {
      console.log(foodList);
      loadFood();
   }, []);

   const loadFood = () => {
      getFoodList()
         .then((foods) => {
            if (foods) {
               setFoodList(foods);
            }
         }).catch(error => console.log(error));
   }

   const validateText = (value: string, key: string) => {
      setValidationError(prevState => ({
         ...prevState,
         [key]: value === ''
      }));
   }

   const validateNumber = (value: string, key: string) => {
      setValidationError(prevState => ({
         ...prevState,
         [key]: !isValidNumber(value)
      }));
   }

   const isValidNumber = (value: string): boolean => {
      let parsed = parseInt(value);
      if (isNaN(parsed)) {
         return false;
      }
      return parsed > 0;
   }

   const handleAddFormSubmit = (event: React.FormEvent<HTMLFormElement>) => {
      event.preventDefault()
      let formIsValid = true;

      const name = event.currentTarget["nameTextBox"].value
      if (name == '') {
         formIsValid = false;
         setValidationError(prevState => ({
            ...prevState,
            name: true
         }));
      }

      const protein = event.currentTarget["proteinTextBox"].value
      if (!isValidNumber(protein)) {
         formIsValid = false;
         setValidationError(prevState => ({
            ...prevState,
            protein: true
         }));
      }

      const fat = event.currentTarget["fatTextBox"].value
      if (!isValidNumber(fat)) {
         formIsValid = false;
         setValidationError(prevState => ({
            ...prevState,
            fat: true
         }));
      }

      const carbs = event.currentTarget["carbsTextBox"].value
      if (!isValidNumber(carbs)) {
         formIsValid = false;
         setValidationError(prevState => ({
            ...prevState,
            carbs: true
         }));
      }

      const calories = event.currentTarget["caloriesTextBox"].value
      if (!isValidNumber(calories)) {
         formIsValid = false;
         setValidationError(prevState => ({
            ...prevState,
            calories: true
         }));
      }

      if (formIsValid) {
         let addFood: NewFood = {
            name: name,
            protein: protein,
            fat: fat,
            carbs: carbs,
            calories: calories
         }
         saveFood(addFood)
            .then(() => {
               loadFood()
               closeAddForm();
            });
      }
   }

   return (
      <AppPage pageTitle="Foods">
         <When condition={foodList == null}>
            <div className="loader">
               <span><p>loading...</p></span>
               <div><CircularProgress /></div>
            </div>
         </When>
         <When condition={foodList != null && foodList.length === 0}>
            <p>There are currently no food items</p>
         </When>
         <When condition={foodList != null && foodList.length > 0}>
            <TableContainer component={Paper}>
               <Table
                  sx={{ minWidth: 650 }}
                  size="small"
                  aria-label="a dense table"
               >
                  <TableHead>
                     <TableRow>
                        <TableCell>Food</TableCell>
                        <TableCell component="th" align="right">Protein</TableCell>
                        <TableCell align="right">Fat</TableCell>
                        <TableCell align="right">Carbs</TableCell>
                        <TableCell align="right">Calories</TableCell>
                        <TableCell></TableCell>
                     </TableRow>
                  </TableHead>
                  <TableBody>
                     {
                        foodList?.map((row) => (
                           <TableRow
                              key={row.name}
                              sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                           >
                              <TableCell scope="row">
                                 {row.name}
                              </TableCell>
                              <TableCell align="right">
                                 {row.nutritionalInfo.protein}
                              </TableCell>
                              <TableCell align="right">
                                 {row.nutritionalInfo.fat}
                              </TableCell>
                              <TableCell align="right">
                                 {row.nutritionalInfo.carbohydrate}
                              </TableCell>
                              <TableCell align="right">
                                 {row.nutritionalInfo.calories}
                              </TableCell>
                              <TableCell align="right">
                                 <Button size="small" data-id={row.id} onClick={(e) => handleDeleteClick(e)}>Delete</Button>
                              </TableCell>
                           </TableRow>
                        ))
                     }
                  </TableBody>
               </Table>
            </TableContainer>
         </When>
         <div>
            <Button onClick={openAddForm} variant="contained" sx={{ mt: 3 }}>Add Food</Button>
         </div>
         <Modal
            open={addFormOpen}
            onClose={closeAddForm}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description"
         >
            <Box sx={modalStyle}>
               <h2>Add Food</h2>
               <form onSubmit={handleAddFormSubmit}>
                  <TextField id="nameTextBox"
                     error={validationError.name}
                     label="Name"
                     variant="outlined"
                     fullWidth={true}
                     sx={{ mb: 3 }}
                     helperText={validationError.name ? "* Required " : undefined}
                     onBlur={(e) => validateText(e.target.value, "name")}
                  />
                  <TextField
                     id="proteinTextBox"
                     error={validationError.protein}
                     helperText={validationError.protein ? "* Required " : undefined}
                     label="Protein"
                     type="number"
                     InputLabelProps={{
                        shrink: true,
                     }}
                     variant="outlined"
                     defaultValue={10}
                     sx={{ mb: 3, mr: 2, width: "90px" }}
                     onBlur={(e) => validateNumber(e.target.value, "protein")}
                  />
                  <TextField
                     id="fatTextBox"
                     error={validationError.fat}
                     helperText={validationError.fat ? "* Required " : undefined}
                     label="Fat"
                     type="number"
                     InputLabelProps={{
                        shrink: true,
                     }}
                     variant="outlined"
                     defaultValue={10}
                     sx={{ mb: 3, mr: 2, width: "90px" }}
                     onBlur={(e) => validateNumber(e.target.value, "fat")}
                  />
                  <TextField
                     id="carbsTextBox"
                     error={validationError.carbs}
                     helperText={validationError.carbs ? "* Required " : undefined}
                     label="Carbs"
                     type="number"
                     InputLabelProps={{
                        shrink: true,
                     }}
                     variant="outlined"
                     defaultValue={10}
                     sx={{ mb: 3, mr: 2, width: "90px" }}
                     onBlur={(e) => validateNumber(e.target.value, "carbs")}
                  />
                  <TextField
                     id="caloriesTextBox"
                     error={validationError.calories}
                     helperText={validationError.calories ? "* Required " : undefined}
                     label="Calories"
                     type="number"
                     InputLabelProps={{
                        shrink: true,
                     }}
                     variant="outlined"
                     defaultValue={10}
                     sx={{ mb: 3, mr: 2, width: "90px" }}
                     onBlur={(e) => validateNumber(e.target.value, "calories")}
                  />
                  <div className="" style={{ width: "100%", display: "flex", justifyContent: "space-between" }}>
                     <Button variant="outlined" onClick={closeAddForm}>Cancel</Button>
                     <Button type="submit" variant="contained">Add</Button>
                  </div>
               </form>
            </Box>
         </Modal>

         <Modal
            open={deleteFormOpen}
            onClose={closeDeleteForm}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description"
         >
            <Box sx={modalStyle}>

               <p>Are you sure you want to delete this food ?</p>

               <div className="" style={{ width: "100%", display: "flex", justifyContent: "space-between" }}>
                  <Button variant="outlined" onClick={closeDeleteForm}>Cancel</Button>
                  <Button variant="contained" onClick={handleDeleteFoodItem}>Delete</Button>
               </div>
            </Box>
         </Modal>
      </AppPage>
   );
};