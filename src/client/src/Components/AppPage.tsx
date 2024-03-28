import { AppBar, Toolbar } from "@mui/material";
import Container from "@mui/material/Container";

type AppPageProps = {
   pageTitle: string;
   children: React.ReactNode;
};

export const AppPage = ({ pageTitle, children }: AppPageProps) => {
   return (
      <>
         <div>
            <AppBar position="static">
               <Toolbar>
                  My Food Tracker
               </Toolbar>
            </AppBar>
            <Container>
               <div>
                  <h1>{pageTitle}</h1>
               </div>
               <div className="page-wrapper">
                  {children}
               </div>
            </Container>
         </div>
      </>
   );
};
