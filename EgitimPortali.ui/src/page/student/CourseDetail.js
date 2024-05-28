import React, { useState } from "react";
import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { toast } from "react-toastify";
import UserCourseService from "../../service/UserCourseService";
import CookieManager from "../../helpers/CookieManager";
import getServerFile from "../../helpers/GetServerFile";
import EducationTypeHelper from "../../helpers/EducationTypeHelper";
import Role from "../../enums/Role";
import CourseStatus from "../../enums/CourseStatus";
import DocumentationType from "../../enums/DocumentationType";

const CourseDetail = () => {
  const { id } = useParams();
  const [course, setCourse] = useState();
  var user = JSON.parse(CookieManager.getCookie("user"));
  var imageDocs = course?.documentations.filter((doc) => doc.documentationType === DocumentationType.Image);
  var videoDocs = course?.documentations.filter((doc) => doc.documentationType === DocumentationType.Video);
  var pdfDocs = course?.documentations.filter((doc) => doc.documentationType === DocumentationType.Pdf);
  
  useEffect(() => {
    getCourse();
  }, []);

  const getCourse = async () => {
    const response = await UserCourseService.Detail(id);
    if (response.success) {
      setCourse(response.body);
    } else {
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
              {user.role == Role.Student &&
                course.courseStatus == CourseStatus.Subscribe && (
                  <button id="subscribe" className="btn btn-success">
                    Subscribe
                  </button>
                )}
              {user.role == Role.Student &&
                course.courseStatus == CourseStatus.Pending && (
                  <button className="btn btn-success disable">
                    Subscribe Request Sended
                  </button>
                )}
            </div>
          </div>

          <div className="mt-5">
            <div className="d-flex flex-column gap-2">
              <h3>Documentations</h3>

              <div>
                <div>
                  {imageDocs.length > 0 && (
                    <div>
                      <h2>Images</h2>
                      <div className="d-flex gap-5 flex-wrap">
                        {imageDocs.map((doc, index) => (
                          <div key={index}>
                            {doc.content && (
                              <img
                                src={getServerFile(doc.content)}
                                alt={`Documentation ${index}`}
                                style={{ width: "250px", height: "250px" }}
                              />
                            )}
                          </div>
                        ))}
                      </div>
                    </div>
                  )}
                </div>
                {videoDocs.length > 0 && <div className="dotted-line"></div>}

                <div>
                  {videoDocs.length > 0 && (
                    <div>
                      <h2>Videos</h2>
                      {videoDocs.map((doc, index) => (
                        <div key={index}>
                          {doc.content && (
                            <iframe
                              width="560"
                              height="315"
                              src={getServerFile(doc.content)}
                              allowFullScreen
                            ></iframe>
                          )}
                        </div>
                      ))}
                    </div>
                  )}
                </div>

                {pdfDocs.length > 0 && <div className="dotted-line"></div>}

                <div style={{ height: "20rem" }}>
                  {pdfDocs.length > 0 && (
                    <div>
                      <h2>Pdf</h2>
                      {pdfDocs.map((doc, index) => (
                        <div key={index}>
                          {doc.content && (
                            <embed
                              src={getServerFile(doc.content)}
                              type="application/pdf"
                            />
                          )}
                        </div>
                      ))}
                    </div>
                  )}
                </div>
              </div>
            </div>
          </div>
        </div>
      )}
    </React.Fragment>
  );
};

export default CourseDetail;
