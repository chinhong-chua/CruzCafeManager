import { BrowserRouter, Route, Routes } from "react-router-dom";
import CafesPage from "./pages/CafesPage";
import EmployeesPage from "./pages/EmployeesPage";


function App() {

  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<CafesPage />} />
          <Route path="/cafes" element={<CafesPage />} />
          <Route path="/employees" element={<EmployeesPage />} />
          {/* <Route path="/cafes/add" element={<AddEditCafePage />} />
          <Route path="/cafes/edit/:id" element={<AddEditCafePage />} />
          <Route path="/employees/add" element={<AddEditEmployeePage />} />
          <Route path="/employees/edit/:id" element={<AddEditEmployeePage />} />
          <Route path="*" element={<NotFoundPage />} /> */}
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
