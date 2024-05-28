import React, { useState } from "react";
import { useEffect } from "react";
import UserService from "../../service/UserService";
import { toast } from "react-toastify";

const StudentDashboard = () => {
  const [dashboard, setDashboard] = useState();

  useEffect(() => {
    const getStudentDashboard = async () => {
      const response = await UserService.studentDashboard();
      debugger;
      if (response.success) {
        setDashboard(response.body);
      } else {
        toast.error(response.message);
      }
    };
    getStudentDashboard();
  }, []);

  return (
    <div className="container d-flex flex-column align-items-center mt-5">
      <div className="d-flex">
        <label>Aldığın Kurslar:</label>
        <p>{dashboard?.purchasedCourseCount}</p>
      </div>
      <div className="d-flex">
        <label>Toplam harcanan para:</label>
        <p>{dashboard?.totalSpentMoney} ₺</p>
      </div>
    </div>
  );
};

export default StudentDashboard;
