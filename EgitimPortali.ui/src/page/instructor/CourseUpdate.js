import React, { useEffect } from "react";
import { useState } from "react";
import { toast } from "react-toastify";
import CourseService from "../../service/CourseService";
import { useParams } from "react-router-dom";
import getServerFile from "../../helpers/GetServerFile";

const CourseUpdate = () => {
  const { id } = useParams();
  const [course, setCourse] = useState();

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

  const uploadFile = async (event) => {
    const file = event.target.files[0];

    if (file) {
      const reader = new FileReader();
      reader.onload = (e) => {
        let result = file.name + "," + e.target?.result;

        let fileType;
        if (result.includes("data:video/")) {
          fileType = 1; // Video
        } else if (result.includes("data:application/pdf")) {
          fileType = 2; // PDF
        } else if (result.includes("data:image/")) {
          fileType = 3; // Image
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

  const updateCourse = async () => {
    course.capacity = Number(course.capacity);
    course.educationType = Number(course.educationType);
    course.price = parseFloat(course.price);
    course.time = Number(course.time);
    const response = await CourseService.Update(course);
    if (response.success) {
      toast.success(response.message);
      getCourse();
    } else {
      toast.error(response.message);
    }
  };

  // ilk virgüle kadar olan string i sil
  const removeFirstPartBeforeComma = (str) => {
    if (!str) return str;
    const index = str.indexOf(",");
    return index !== -1 ? str.substring(index + 1) : str;
  };

  const isBase64 = (str) => {
    if (str.length > 255) return true;
    else return false;
  };

  const base64Control = (str) => {
    var base64Data = removeFirstPartBeforeComma(str);
    if (isBase64(base64Data)) {
      return base64Data;
    } else {
      return getServerFile(str);
    }
  };

  return (
    <div className="container mt-5 d-flex flex-column align-items-center">
      <div className="row d-flex">
        <div className="col-md-4">
          <div>
            <label>Education Type</label>
            <select
              className="form-control"
              value={course?.educationType}
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
              value={course?.title}
              className="form-control"
              onChange={(e) => setCourse({ ...course, title: e.target.value })}
            />
          </div>
          <div>
            <label>Description</label>
            <textarea
              value={course?.description}
              className="form-control"
              rows={10}
              onChange={(e) =>
                setCourse({ ...course, description: e.target.value })
              }
            />
          </div>

          <div>
            <label>Time (Minute)</label>
            <input
              value={course?.time}
              className="form-control"
              onChange={(e) => setCourse({ ...course, time: e.target.value })}
            />
          </div>

          <div>
            <label>Capacity</label>
            <input
              value={course?.capacity}
              className="form-control"
              onChange={(e) =>
                setCourse({ ...course, capacity: e.target.value })
              }
            />
          </div>

          <div>
            <label>Price</label>
            <input
              value={course?.price}
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
                src={base64Control(course.introductionPhoto)}
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
                <source
                  src={base64Control(course.introductionVideo)}
                  type="video/mp4"
                />
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
                    {doc.content && doc.documentationType == 3 && (
                      <img
                        src={base64Control(doc.content)}
                        alt={`Documentation ${index}`}
                        style={{ width: "80px", height: "80px" }}
                      />
                    )}
                    {doc.content && doc.documentationType == 1 && (
                      <video controls style={{ width: "80px", height: "80px" }}>
                        <source
                          src={base64Control(doc.content)}
                          type="video/mp4"
                        />
                        Your browser does not support the video tag.
                      </video>
                    )}
                    {doc.content && doc.documentationType == 2 && (
                      <embed
                        src={base64Control(doc.content)}
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
      <button className="btn btn-success mt-2 w-25" onClick={updateCourse}>
        Update Course
      </button>
    </div>
  );
};

export default CourseUpdate;
