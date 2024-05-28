import React from "react";
import { useState } from "react";
import { useEffect } from "react";
import CookieManager from "../helpers/CookieManager";
import getServerFile from "../helpers/GetServerFile";
import UserService from "../service/UserService";
import { toast } from "react-toastify";

const Profile = () => {
  const [user, setUser] = useState();
  const [passwordControl, setPasswordControl] = useState(false);

  useEffect(() => {
    const cookieUser = JSON.parse(CookieManager.getCookie("user"));
    if (cookieUser) {
      setUser(cookieUser);
    } else {
      window.location.href = "/login";
    }
  }, []);

  const handleUpdate = async () => {
    const prm = {
      id: user.id,
      name: user.name,
      email: user.email,
      image: user.image,
      oldPassword: user.oldPassword,
      newPassword: user.newPassword,
      newPasswordVerify: user.newPasswordVerify,
    };
    const response = await UserService.Update(prm);
    if (response.success) {
      CookieManager.setCookie("user", JSON.stringify(response.body), 1);
      toast.success(response.message);
      setTimeout(() => {
        window.location.reload();
      }, 1000);
    } else {
      toast.error(response.message);
    }
  };

  return (
    <div className="container d-flex flex-column align-items-center mt-5">
      <div className="d-flex gap-3">
        <img
          className="border border-5"
          width={200}
          height={230}
          src={getServerFile(user?.image)}
          style={{ borderRadius: "40%" }}
        />
      </div>
      <div className="d-flex gap-3 mt-5 align-items-center">
        <label className="form-label">Email:</label>
        <input
          className="form-control"
          value={user?.email}
          onChange={(e) => setUser({ ...user, email: e.target.value })}
        />
      </div>

      <div className="d-flex gap-3 mt-2 align-items-center">
        <label className="form-label">Name:</label>
        <input
          className="form-control"
          value={user?.name}
          onChange={(e) => setUser({ ...user, name: e.target.value })}
        />
      </div>

      <div className="d-flex gap-3 mt-2 align-items-center">
        <label className="form-label">Password:</label>
        <input
          className="form-control"
          type="password"
          value={user?.oldPassword}
          onChange={(e) => setUser({ ...user, oldPassword: e.target.value })}
        />
      </div>

      <div className="d-flex gap-3 mt-2 align-items-center">
        <p>I want to change my password</p>
        <input
          className=""
          type="checkbox"
          onChange={() => setPasswordControl(!passwordControl)}
        />
      </div>

      {passwordControl && (
        <React.Fragment>
          <div className="d-flex gap-3 mt-2 align-items-center">
            <label className="form-label">New Password:</label>
            <input
              className="form-control"
              type="password"
              value={user?.newPassword}
              onChange={(e) =>
                setUser({ ...user, newPassword: e.target.value })
              }
            />
          </div>
          <div className="d-flex gap-3 mt-2 align-items-center">
            <label className="form-label">New Password Confirm:</label>
            <input
              type="password"
              className="form-control"
              value={user?.newPasswordVerify}
              onChange={(e) =>
                setUser({ ...user, newPasswordVerify: e.target.value })
              }
            />
          </div>
        </React.Fragment>
      )}

      <button className="btn btn-warning mt-3" onClick={handleUpdate}>
        Save
      </button>
    </div>
  );
};

export default Profile;
