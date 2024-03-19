type AppPageProps = {
  pageTitle: string;
  children: React.ReactNode;
};

export const AppPage = ({ pageTitle, children }: AppPageProps) => {
  return (
    <>
      <div> 
        <h1 className="pageTitle">{pageTitle}</h1>
        <div>{children}</div>
      </div>
    </>
  );
};
