import React, { useEffect, useState } from "react";
import UserService from "../../service/UserService";
import { toast } from "react-toastify";
import getServerFile from "../../helpers/GetServerFile";
import CookieManager from "../../helpers/CookieManager";
import Role from "../../enums/Role";

const Register = () => {
  const [uploadFile, setUploadFile] = useState();
  const [user, setUser] = useState();

  useEffect(() => {
    let userImage = document.getElementById("user-image");
    if (userImage) {
      userImage.src = getServerFile("default-user.jpg");
    }
  }, []);

  const handleRegister = async () => {
    let prepareImage = user.image;
    if (uploadFile.name) prepareImage = uploadFile.name + "," + prepareImage;

    const req = {
      email: user.email,
      name: user.name,
      password: user.password,
      image: prepareImage,
      role: user.role,
    };

    const response = await UserService.Create(req);

    if (response.success) {
      CookieManager.setCookie("user", JSON.stringify(response.body), 1);
      if (response.body.role == Role.Instructor) {
        window.location.href = "/instructor/dashboard";
      } else {
        window.location.href = "/student/dashboard";
      }
    } else {
      toast.warning(response.message);
    }
  };

  const uploadImage = async (event) => {
    const file = event.target.files[0];

    if (file) {
      setUploadFile(file);
      const reader = new FileReader();
      reader.onload = (e) => {
        let result = e.target?.result;
        setUser({ ...user, image: result });
      };
      reader.readAsDataURL(file);
    }
  };

  return (
    <div
      className={
        "d-flex flex-column justify-content-center align-items-center mt-5"
      }
    >
      <form
        style={{ width: "300px" }}
        className="d-flex flex-column align-items-center"
      >
        <h1 className="text-center mb-5 header-gradient">Register</h1>
        <img
          id="user-image"
          src={user?.image}
          style={{
            width: "120px",
            height: "120px",
            borderRadius: "50%",
            marginBottom: "2rem",
            border: "1px solid grey",
          }}
        />
        <div className="form-outline mb-4 text-center">
          <input
            type="email"
            name="email"
            className="form-control"
            placeholder="Email"
            onChange={(e) => setUser({ ...user, email: e.target.value })}
          />
        </div>
        <div className="form-outline mb-4 text-center">
          <input
            type="text"
            name="name"
            className="form-control"
            placeholder="Name"
            onChange={(e) => setUser({ ...user, name: e.target.value })}
          />
        </div>
        <div className="form-outline mb-4 text-center">
          <input
            type="text"
            name="surname"
            className="form-control"
            placeholder="Surname"
            onChange={(e) => setUser({ ...user, surname: e.target.value })}
          />
        </div>
        <div className="form-outline mb-4 text-center">
          <input
            type="password"
            name="password"
            className="form-control"
            placeholder="Password"
            onChange={(e) => setUser({ ...user, password: e.target.value })}
          />
        </div>
        <div className="form-outline mb-4 text-center">
          <select
            id="role"
            onChange={(e) => setUser({ ...user, role: e.target.value })}
            className="form-control"
          >
            <option value={0} className="disable">
              Role Seçiniz
            </option>
            <option value={"Instructor"}>Eğitmen</option>
            <option value={"Student"}>Öğrenci</option>
          </select>
        </div>
        <div className="form-outline mb-4 text-center">
          <input
            type="file"
            name="file"
            className="form-control"
            placeholder="File"
            onChange={uploadImage}
          />
        </div>

        <div className="d-flex gap-3 justify-content-center align-items-center">
          <p>
            I have an account <a href="/login">Login</a>
          </p>

          <button
            type="button"
            className="btn btn-warning "
            onClick={handleRegister}
          >
            Sign Up
          </button>
        </div>
      </form>
    </div>
  );
};

export default Register;
