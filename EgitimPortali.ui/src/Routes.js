import CourseIntroduction from "./page/CourseIntroduction";
import CourseList from "./page/CourseList";
import Main from "./page/Main";
import Profile from "./page/Profile";
import Login from "./page/auth/Login";
import Register from "./page/auth/Register";
import CourseCreate from "./page/instructor/CourseCreate";
import CourseUpdate from "./page/instructor/CourseUpdate";
import InstructorDashboard from "./page/instructor/InstructorDashboard";
import StudentManagement from "./page/instructor/StudentManagement";
import CourseDetail from "./page/student/CourseDetail";
import StudentCourses from "./page/student/StudentCourses";
import StudentDashboard from "./page/student/StudentDashboard";

const InstructorRoutes = [
  {
    path: "/courseCreate",
    component: <CourseCreate />,
  },
  {
    path: "/courseUpdate/:id",
    component: <CourseUpdate />,
  },
  {
    path: "/studentManagement",
    component: <StudentManagement />,
  },
  {
    path: "/instructor/dashboard",
    component: <InstructorDashboard />,
  },
];

const StudentRoutes = [
  {
    path: "/student/dashboard",
    component: <StudentDashboard />,
  },
  {
    path: "/myCourses",
    component: <StudentCourses />,
  },
  {
    path: "/course/detail/:id",
    component: <CourseDetail />
  }
];

const BothRoutes = [
  {
    path: "/",
    component: <Main />
  },
  {
    path: "/course/introduction/:id",
    component: <CourseIntroduction />,
  },
  {
    path: "/courseList",
    component: <CourseList />,
  },
  {
    path: "/profile",
    component: <Profile />,
  },
  {
    path: "/login",
    component: <Login />,
  },
  {
    path: "/Register",
    component: <Register />,
  },
];
export { InstructorRoutes, StudentRoutes, BothRoutes };
