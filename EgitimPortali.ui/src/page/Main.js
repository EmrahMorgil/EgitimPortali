import React, { useEffect } from "react";
import Role from "../enums/Role";
import CookieManager from "../helpers/CookieManager";

const Main = () => {
  var user = JSON.parse(CookieManager.getCookie("user"));

  useEffect(() => {
    if (!user) {
      window.location.href = "/login";
    } else {
      if (user.role == Role.Instructor) {
        window.location.href = "/instructor/dashboard/" + user.id;
      } else {
        window.location.href = "/student/dashboard/" + user.id;
      }
    }
  }, []);

  return <div></div>;
};

export default Main;
