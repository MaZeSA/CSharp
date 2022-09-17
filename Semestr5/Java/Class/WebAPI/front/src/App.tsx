import "./App.css";
import { Routes, Route } from "react-router-dom";
import HomePage from "./components/home";
import LoginPage from "./components/auth/login";
import RegisterPage from "./components/auth/register";
import HomeLayout from "./components/containers/homeLayout";
import ParentPage from "./components/kids/parent/list";
import ParentAddPage from "./components/kids/parent/add";
import ParentEditPage from "./components/kids/parent/edit";
import IParentUpdate from "./components/kids/parent/edit";
import SearchParentPage from "./components/kids/parent/search";

function App() {
  return (
    <Routes>
      <Route path="/" element={<HomeLayout />}>
        <Route index element={<HomePage />} />
        <Route path="parent" element={<ParentPage />} ></Route>
        <Route path="parent/add" element={<ParentAddPage />} />
        <Route path="login" element={<LoginPage />} />
        <Route path="register" element={<RegisterPage />} />
        <Route path="parent/search" element={<SearchParentPage />} />
        <Route path="/parent/edit" element={<ParentEditPage />}>
          <Route path=":id" element={<IParentUpdate />} />
        </Route>
      </Route>
    </Routes>
  );
}

export default App;
