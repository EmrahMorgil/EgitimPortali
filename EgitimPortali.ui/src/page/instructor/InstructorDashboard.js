import React, { useEffect, useState } from "react";
import UserService from "../../service/UserService";
import { toast } from "react-toastify";

const InstructorDashboard = () => {
  const [dashboard, setDashboard] = useState();

  useEffect(() => {
    const getInstructorDashboard = async () => {
      const response = await UserService.InstructorDashboard();
      if (response.success) {
        setDashboard(response.body);
      } else {
        toast.error(response.message);
      }
    };
    getInstructorDashboard();
  }, []);

  return (
    <div className="container d-flex flex-column align-items-center mt-5">
      <div className="d-flex">
        <label>Satılan kurs sayısı:</label>
        <p>{dashboard?.soldCourseCount}</p>
      </div>
      <div className="d-flex">
        <label>Kazanç:</label>
        <p>{dashboard?.totalRevenue} ₺</p>
      </div>
    </div>
  );
};

export default InstructorDashboard;
