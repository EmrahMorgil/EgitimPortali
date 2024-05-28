import React, { useEffect, useState } from "react";
import UserCourseService from "../../service/UserCourseService";
import CourseStatusHelper from "../../helpers/CourseStatusHelper";
import CourseStatus from "../../enums/CourseStatus";
import { toast } from "react-toastify";

const StudentManagement = () => {
  const [studentManagementData, setStudentManagementData] = useState();

  useEffect(() => {
    getUsers();
  }, []);

  const getUsers = async () => {
    const response = await UserCourseService.StudentManagementList();
    setStudentManagementData(response.body);
  };

  const updateCourseStatus = async (id, status) => {
    const request = { id: id, courseStatus: status };
    const response = await UserCourseService.Update(request);
    if (response.success) {
      toast.success(response.message);
      getUsers();
    } else {
      toast.error(response.message);
    }
  };


  return (
    <div className="container mt-5">
      <table className="table table-striped">
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">Name</th>
            <th scope="col">Course Name</th>
            <th scope="col">Status</th>
            <th scope="col">Actions</th>
          </tr>
        </thead>
        <tbody>
          {studentManagementData?.map((data, index) => (
            <React.Fragment key={index}>
              <tr>
                <th scope="row">{index + 1}</th>
                <td>{data.user.name}</td>
                <td>{data.course.title}</td>
                <td>{CourseStatusHelper(data.courseStatus)}</td>
                {data.courseStatus == CourseStatus.Pending && (
                  <td>
                    <button
                      className="btn btn-success"
                      onClick={()=>updateCourseStatus(data.id, CourseStatus.Approved)}
                    >
                      Approve
                    </button>
                    <button
                      className="btn btn-danger"
                      onClick={()=>updateCourseStatus(data.id, CourseStatus.Discarded)}
                    >
                      Discard
                    </button>
                  </td>
                )}

                {data.courseStatus == CourseStatus.Approved && (
                  <td>
                    <button className="btn btn-success disable">
                      Approved
                    </button>
                  </td>
                )}

                {data.courseStatus == CourseStatus.Discarded && (
                  <td>
                    <button className="btn btn-danger disable">
                      Discarded
                    </button>
                  </td>
                )}
              </tr>
            </React.Fragment>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default StudentManagement;
