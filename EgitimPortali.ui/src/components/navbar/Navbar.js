import "./Navbar.css";
import { HandleLogout } from "../../helpers/HandleLogout";
import CookieManager from "../../helpers/CookieManager";
import Role from "../../enums/Role";

const Navbar = () => {
  var role = JSON.parse(CookieManager.getCookie("user"))?.role;

  var studentNavigation = (
    <div className="collapse navbar-collapse" id="navbarNav">
      <ul className="navbar-nav ms-md-auto gap-2">
        <li className="nav-item rounded">
          <a className="nav-link active" aria-current="page" href="/student/dashboard">
            Dashboard
          </a>
        </li>
        <li className="nav-item rounded">
          <a className="nav-link" href="/myCourses">
            My Courses
          </a>
        </li>
        <li className="nav-item rounded">
          <a className="nav-link" href="/courseList">
            All Courses
          </a>
        </li>
        <li className="nav-item rounded">
          <a className="nav-link" href="/profile">
            Profile
          </a>
        </li>
        <li className="nav-item rounded" style={{cursor: "pointer"}}>
          <a className="nav-link cursor-pointer" onClick={HandleLogout}>
            Logout
          </a>
        </li>
      </ul>
    </div>
  );

  var instructorNavigation = (
    <div className="collapse navbar-collapse" id="navbarNav">
      <ul className="navbar-nav ms-md-auto gap-2">
        <li className="nav-item rounded">
          <a className="nav-link active" aria-current="page" href="/instructor/dashboard">
            Dashboard
          </a>
        </li>
        <li className="nav-item rounded">
          <a className="nav-link" href="/studentManagement">
            Student Management
          </a>
        </li>
        <li className="nav-item rounded">
          <a className="nav-link" href="/courseList">
            My Courses
          </a>
        </li>
        <li className="nav-item rounded">
          <a className="nav-link" href="/profile">
            Profile
          </a>
        </li>
        <li className="nav-item rounded" style={{cursor: "pointer"}}>
          <a className="nav-link" onClick={HandleLogout}>
            Logout
          </a>
        </li>
      </ul>
    </div>
  );

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
      <div className="container-fluid">
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon" />
        </button>
        {role == Role.Instructor ? instructorNavigation : studentNavigation}
      </div>
    </nav>
  );
};

export default Navbar;
