import CookieManager from "../helpers/CookieManager";
import { PostAsync, GetAsync } from "./ApiClient";

const token = JSON.parse(CookieManager.getCookie("user"))?.token;

function servicePath() {
  return "userCourse/";
}

const UserCourseService = {
  Create: async (req) => {
    const response = await PostAsync(servicePath() + "create", {
      method: "POST",
      body: JSON.stringify(req),
      headers: { "Content-Type": "application/json", Authorization: token },
    });
    return response;
  },

  Delete: async (req) => {
    const response = await PostAsync(servicePath() + "delete", {
      method: "POST",
      body: JSON.stringify(req),
      headers: { "Content-Type": "application/json" },
    });
    return response;
  },

  List: async () => {
    const response = await GetAsync(servicePath() + "list", {
      method: "GET",
      headers: { "Content-Type": "application/json", Authorization: token },
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
  Detail: async (id) => {
    const response = await GetAsync(servicePath() + "detail?Id=" + id, {
      method: "GET",
      headers: { "Content-Type": "application/json", Authorization: token },
    });
    return response;
  },
  StudentManagementList: async () => {
    const response = await GetAsync(servicePath() + "studentManagementList", {
      method: "GET",
      headers: { "Content-Type": "application/json", Authorization: token },
    });
    return response;
  },
};

export default UserCourseService;
