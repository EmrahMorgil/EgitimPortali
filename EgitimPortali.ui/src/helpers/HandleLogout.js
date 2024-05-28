import CookieManager from "./CookieManager";

export const HandleLogout = () => {
  CookieManager.clearCookies();
  window.location.href = window.location.origin + "/login";
};
