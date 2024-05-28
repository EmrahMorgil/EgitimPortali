import React, { useEffect, useState } from "react";
import CourseService from "../service/CourseService";
import getServerFile from "../helpers/GetServerFile";
import CookieManager from "../helpers/CookieManager";
import Role from "../enums/Role";

const CourseList = () => {
  var user = JSON.parse(CookieManager.getCookie("user"));
  const [courses, setCourses] = useState();

  useEffect(() => {
    const getCourseList = async () => {
      const response = await CourseService.List();
      setCourses(response);
    };

    getCourseList();
  }, []);

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
                src={getServerFile(course.introductionPhoto)}
                alt="Card image cap"
              />
              <div className="card-body">
                <h5 className="card-title fw-bold">{course.title}</h5>
                <p className="card-text">
                  {course.description.length > 100
                    ? course.description.substring(0, 100) + "..."
                    : course.description}
                </p>
                <div className="d-flex flex-column gap-1 align-items-center">
                  <a
                    href={"/course/introduction/" + course.id}
                    className="btn btn-primary w-75"
                  >
                    Course Detail
                  </a>
                  {user.role == Role.Instructor && (
                    <a
                      href={"/courseUpdate/" + course.id}
                      className="btn btn-warning w-75"
                    >
                      Course Update
                    </a>
                  )}
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

export default CourseList;
