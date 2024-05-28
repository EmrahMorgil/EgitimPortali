import CookieManager from "../helpers/CookieManager";
import { GetAsync, PostAsync } from "./ApiClient";

const token = JSON.parse(CookieManager.getCookie("user"))?.token;
function servicePath() {
  return "user/";
}

const UserService = {
  Create: async (req) => {
    const response = await PostAsync(servicePath() + "create", {
      method: "POST",
      body: JSON.stringify(req),
      headers: { "Content-Type": "application/json" },
    });
    return response;
  },

  Login: async (req) => {
    const response = await PostAsync(servicePath() + "login", {
      method: "POST",
      body: JSON.stringify(req),
      headers: { "Content-Type": "application/json" },
    });
    return response;
  },

  Update: async (req) => {
    const response = await PostAsync(servicePath() + "update", {
      method: "POST",
      body: JSON.stringify(req),
      headers: { "Content-Type": "application/json", Authorization: token },
    });
    return response;
  },

  Detail: async () => {
    const response = await GetAsync(servicePath() + "detail", {
      method: "GET",
      headers: { Authorization: token },
    });
    return response;
  },

  InstructorDashboard: async (id) => {
    const response = await GetAsync(servicePath() + "instructor/dashboard", {
      method: "GET",
      headers: { "Content-Type": "application/json", Authorization: token },
    });
    return response;
  },
  studentDashboard: async () => {
    const response = await GetAsync(servicePath() + "student/dashboard", {
      method: "GET",
      headers: { "Content-Type": "application/json", Authorization: token },
    });
    return response;
  },
};

export default UserService;

