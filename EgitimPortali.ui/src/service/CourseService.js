import CookieManager from "../helpers/CookieManager";
import { PostAsync, GetAsync } from "./ApiClient";

const token = JSON.parse(CookieManager.getCookie("user"))?.token;

function servicePath() {
  return "course/";
}

const CourseService = {
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

  Update: async (req) => {
    const response = await PostAsync(servicePath() + "update", {
      method: "POST",
      body: JSON.stringify(req),
      headers: { "Content-Type": "application/json" },
    });
    return response;
  },

  Detail: async (id) => {
    const response = await GetAsync(servicePath() + `detail?Id=` + id, {
      method: "GET",
      headers: { "Content-Type": "application/json", Authorization: token },
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
};

export default CourseService;
