import React from "react";
import UserService from "../../service/UserService";
import { toast } from "react-toastify";
import Role from "../../enums/Role";
import CookieManager from "../../helpers/CookieManager";

const Login = () => {
  const [user, setUser] = React.useState();

  const handleLogin = async () => {
    const response = await UserService.Login(user);
    if(response.success){
      CookieManager.setCookie("user", JSON.stringify(response.body), 1);
      if(response.body.role == Role.Instructor){
        window.location.href = "/instructor/dashboard";
      }else{
        window.location.href = "/student/dashboard";
      }
    }else{
      toast.warning(response.message);
    }
  };

  return (
    <div
      className={"d-flex justify-content-center align-items-center"} style={{ marginTop: "15rem" }}>
      <form style={{ width: "300px" }} className="d-flex flex-column align-items-center">
        <h1 className="text-center mb-5 header-gradient">Login</h1>
        <div className="form-outline mb-4">
          <input
            type="email"
            name="email"
            className="form-control"
            onChange={(e) => setUser({ ...user, email: e.target.value })}
          />
        </div>
        <div className="form-outline mb-4">
          <input
            type="password"
            name="password"
            className="form-control"
            onChange={(e) => setUser({ ...user, password: e.target.value })}
          />
        </div>
        <button
          type="button"
          className="btn btn-primary btn-block mb-4"
          onClick={handleLogin}
        >
          Sign in
        </button>
        <div className="text-center">
          <p>
            Not a member? <a href="/register">Register</a>
          </p>
        </div>
      </form>
    </div>
  );
};

export default Login;
