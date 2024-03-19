import React from "react";

export const When = ({ condition, children }: { condition: boolean; children: React.ReactNode }) => {
   return <>
      {condition && children}
   </>
};
