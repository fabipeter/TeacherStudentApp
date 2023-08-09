import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { ToastContainer } from "react-toastify";
import { Route, Routes } from "react-router-dom";
import Teacher from "./Forms/Teacher";
import Student from "./Forms/Student";
import HomePage from "./components/HomePage";

function App() {
  return (
    <>
      <ToastContainer position="bottom-right" hideProgressBar theme="colored" />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/teacher" element={<Teacher />} />
        <Route path="/student" element={<Student />} />
      </Routes>
    </>
  );
}

export default App;
