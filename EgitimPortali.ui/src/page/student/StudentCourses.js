import React, { useEffect, useState } from "react";
import CookieManager from "../../helpers/CookieManager";
import getServerFile from "../../helpers/GetServerFile";
import UserCourseService from "../../service/UserCourseService";
import Role from "../../enums/Role";
import { toast } from "react-toastify";
import CourseStatusHelper from "../../helpers/CourseStatusHelper";
import CourseStatus from "../../enums/CourseStatus";

const StudentCourses = () => {
  var user = JSON.parse(CookieManager.getCookie("user"));
  const [courses, setCourses] = useState();

  useEffect(() => {
    getCourseList();
  }, []);

  const getCourseList = async () => {
    const response = await UserCourseService.List();
    if (response.success) {
      setCourses(response.body);
    } else {
      toast.error(response.message);
    }
  };

  return (
    <div className="container mt-5">
      {user.role == Role.Instructor && (
        <a href="/courseCreate" className="btn btn-success">
          Add new course
        </a>
      )}

      <div className="d-flex gap-5 flex-wrap justify-content-center text-center mt-2">
        {courses && courses.length > 0 ? (
          courses.map((course) => (
            <div className="card" style={{ width: "18rem" }}>
              <img
                className="card-img-top"
                width={300}
                height={250}
                src={getServerFile(course.course.introductionPhoto)}
                alt="Card image cap"
              />
              <div className="card-body">
                <h5 className="card-title fw-bold">{course.course.title}</h5>
                <p className="card-text">
                  {course.course.description.length > 100
                    ? course.course.description.substring(0, 100) + "..."
                    : course.course.description}
                </p>
                <div>
                  <div className="d-flex align-items-center justify-content-center gap-2">
                    <b>Status:</b>
                    <span
                      className={`badge ${
                        course.status == CourseStatus.Approved
                          ? "bg-success"
                          : course.status == CourseStatus.Pending
                          ? "bg-info"
                          : "bg-danger"
                      }`}
                    >
                      {CourseStatusHelper(course.status)}
                    </span>
                  </div>
                  <a
                    href={"/course/detail/" + course.id}
                    className={`btn btn-primary w-75 ${
                      course.status != CourseStatus.Approved ? "disable" : ""
                    }`}
                  >
                    Course Detail
                  </a>
                </div>
              </div>
            </div>
          ))
        ) : (
          <div>Empty</div>
        )}
      </div>
    </div>
  );
};

export default StudentCourses;
