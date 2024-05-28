import React, { useState } from "react";
import { useEffect } from "react";
import { useParams } from "react-router-dom";
import CourseService from "../service/CourseService";
import { toast } from "react-toastify";
import getServerFile from "../helpers/GetServerFile";
import EducationTypeHelper from "../helpers/EducationTypeHelper";
import CookieManager from "../helpers/CookieManager";
import Role from "../enums/Role";
import UserCourseService from "../service/UserCourseService";
import CourseStatus from "../enums/CourseStatus";

const CourseIntroduction = () => {
  const { id } = useParams();
  const [course, setCourse] = useState();
  var user = JSON.parse(CookieManager.getCookie("user"));


  useEffect(() => {
    getCourse();
  }, []);

  const getCourse = async () => {
    const response = await CourseService.Detail(id);
    if (response.success) {
      setCourse(response.body);
    } else {
      toast.error(response.message);
    }
  };

  const handleSubscribe = async() => {
    var request = {courseId: id}
    const response = await UserCourseService.Create(request);
    if(response.success){
      getCourse();
      toast.success(response.message);
    }else{
      toast.error(response.message);
    }
  };


  return (
    <React.Fragment>
      {course && (
        <div className="container w-50 mt-5">
          <div className="row">
            <div className="col-md-8">
              <h5 className="fw-bold">{course.title}</h5>
              <p className="d-block mt-3">{course.description}</p>
              <i className="d-block mt-3">
                Created By <b>{course.instructor.name}</b>.
              </i>
            </div>
            <div className="col-md-4 d-flex flex-column align-items-center">
              <img
                className="border border-5"
                width={200}
                height={150}
                src={getServerFile(course.introductionPhoto)}
              />
              <p className="badge bg-info">
                {EducationTypeHelper(course.educationType)}
              </p>
              <p className="badge bg-warning">{course.time} Minute</p>
              <p
                className="d-block"
                style={{ fontWeight: "bold", fontStyle: "italic" }}
              >
                ₺ {course.price}
              </p>

              {user.role == Role.Student && course.courseStatus == CourseStatus.Discarded && (
                <button id="subscribe" className="btn btn-danger disable">
                  Discarded
                </button>
              )}
              {user.role == Role.Student && course.courseStatus == CourseStatus.Approved && (
                <button id="subscribe" className="btn btn-success disable">
                  Approved
                </button>
              )}
              {user.role == Role.Student && course.courseStatus == CourseStatus.Subscribe && (
                <button id="subscribe" className="btn btn-success" onClick={handleSubscribe}>
                  Subscribe
                </button>
              )}
              {user.role == Role.Student && course.courseStatus == CourseStatus.Pending && (
                <button className="btn btn-success disable" >
                  Subscribe Request Sended
                </button>
              )}
            </div>
          </div>
          <div className="mt-5 fw-bold d-flex flex-column align-items-center">
            Introduction
            <div className="d-block">
              <iframe
                width="560"
                height="315"
                src={getServerFile(course.introductionVideo)}
                allowFullScreen
              ></iframe>
            </div>
          </div>
        </div>
      )}
    </React.Fragment>
  );
};

export default CourseIntroduction;
