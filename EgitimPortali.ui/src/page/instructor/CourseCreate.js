import React from "react";
import { useState } from "react";
import { toast } from "react-toastify";
import CourseService from "../../service/CourseService";
import DocumentationType from "../../enums/DocumentationType";

const CourseCreate = () => {
  const [course, setCourse] = useState();

  const uploadFile = async (event) => {
    const file = event.target.files[0];

    if (file) {
      const reader = new FileReader();
      reader.onload = (e) => {
        let result = file.name + "," + e.target?.result;

        let fileType;
        if (result.includes("data:video/")) {
          fileType = DocumentationType.Video;
        } else if (result.includes("data:application/pdf")) {
          fileType = DocumentationType.Pdf; 
        } else if (result.includes("data:image/")) {
          fileType = DocumentationType.Image; 
        } else {
          toast.error("Unsupported file type!");
          return;
        }

        if (event.target.name == "photo") {
          if (fileType != 3) {
            toast.warning("Please select a image!");
          } else {
            setCourse({ ...course, introductionPhoto: result });
          }
        } else if (event.target.name == "video") {
          if (fileType != 1) {
            toast.warning("Please select a video!");
          } else {
            setCourse({ ...course, introductionVideo: result });
          }
        } else if (event.target.name == "documentations") {
          setCourse((prevCourse) => ({
            ...prevCourse,
            documentations: [
              ...(prevCourse?.documentations || []),
              { documentationType: fileType, content: result },
            ],
          }));
        }
      };
      reader.readAsDataURL(file);
    }
  };

  const createCourse = async () => {
    course.capacity = Number(course.capacity);
    course.educationType = Number(course.educationType);
    course.price = parseFloat(course.price);
    course.time = Number(course.time);
    const response = await CourseService.Create(course);

    if (response.success) {
      toast.success(response.message);
      setTimeout(() => {
        window.location.href = "/courseList";
      }, 2000);
    } else {
      toast.error(response.message);
    }
  };

  // ilk virgüle kadar olan string i sil
  const removeFirstPartBeforeComma = (str)=> {
    if (!str) return str;
    const index = str.indexOf(',');
    return index !== -1 ? str.substring(index + 1) : str;
  }

  return (
    <div className="container mt-5 d-flex flex-column align-items-center">
      <div className="row d-flex">
        <div className="col-md-4">
          <div>
            <label>Education Type</label>
            <select
              className="form-control"
              volume={course?.educationType}
              onChange={(e) =>
                setCourse({ ...course, educationType: e.target.value })
              }
            >
              <option value={0}>?</option>
              <option value={1}>Online</option>
              <option value={2}>Face To Face</option>
            </select>
          </div>
          <div>
            <label>Title</label>
            <input
              className="form-control"
              volume={course?.title}
              onChange={(e) => setCourse({ ...course, title: e.target.value })}
            />
          </div>
          <div>
            <label>Description</label>
            <textarea
              volume={course?.description}
              className="form-control"
              onChange={(e) =>
                setCourse({ ...course, description: e.target.value })
              }
            />
          </div>

          <div>
            <label>Time (Minute)</label>
            <input
              volume={course?.time}
              className="form-control"
              onChange={(e) => setCourse({ ...course, time: e.target.value })}
            />
          </div>

          <div>
            <label>Capacity</label>
            <input
              volume={course?.capacity}
              className="form-control"
              onChange={(e) =>
                setCourse({ ...course, capacity: e.target.value })
              }
            />
          </div>

          <div>
            <label>Price</label>
            <input
              volume={course?.price}
              className="form-control"
              onChange={(e) => setCourse({ ...course, price: e.target.value })}
            />
          </div>
        </div>

        <div className="col-md-4 d-flex flex-column gap-3">
          <div>
            <label>Introduction Photo</label>
            <input
              className="form-control"
              type="file"
              name="photo"
              onChange={uploadFile}
            />
            {course && course.introductionPhoto && (
              <img
                src={removeFirstPartBeforeComma(course.introductionPhoto)}
                className="mt-2"
                style={{ width: "80px", height: "80px" }}
              />
            )}
          </div>

          <div>
            <label>Introduction Video</label>
            <input
              className="form-control"
              type="file"
              name="video"
              onChange={uploadFile}
            />
            {course && course.introductionVideo && (
              <video
                controls
                style={{ width: "80px", height: "80px" }}
                className="mt-2"
              >
                <source src={removeFirstPartBeforeComma(course.introductionVideo)} type="video/mp4" />
                Your browser does not support the video tag.
              </video>
            )}
          </div>
        </div>

        <div className="col-md-4">
          <div className="d-flex flex-column gap-2">
            <h5>Documentations</h5>
            <div>
              <input
                className="form-control"
                type="file"
                name="documentations"
                onChange={uploadFile}
              />
            </div>
            <div className="d-flex gap-2 flex-wrap" style={{ width: "300px" }}>
              {course &&
                course.documentations &&
                course.documentations.length > 0 &&
                course.documentations.map((doc, index) => (
                  <div key={index}>
                    {doc.content && doc.content.includes("data:image/") && (
                      <img
                        src={removeFirstPartBeforeComma(doc.content)}
                        alt={`Documentation ${index}`}
                        style={{ width: "80px", height: "80px" }}
                      />
                    )}
                    {doc.content && doc.content.includes("data:video/") && (
                      <video controls style={{ width: "80px", height: "80px" }}>
                        <source src={removeFirstPartBeforeComma(doc.content)} type="video/mp4" />
                        Your browser does not support the video tag.
                      </video>
                    )}
                    {doc.content &&
                      doc.content.includes("data:application/pdf") && (
                        <embed
                          src={removeFirstPartBeforeComma(doc.content)}
                          type="application/pdf"
                          width="80"
                          height="80"
                        />
                      )}
                  </div>
                ))}
            </div>
          </div>
        </div>
      </div>
      <button className="btn btn-success mt-2 w-25" onClick={createCourse}>
        Create Course
      </button>
    </div>
  );
};

export default CourseCreate;
