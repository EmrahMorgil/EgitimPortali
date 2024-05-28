const CourseStatusHelper = (type) => {
  switch (type) {
    case 1:
      return "Pending";
    case 2:
      return "Approved";
    case 3:
      return "Subscribe";
    case 4:
      return "Discarded";
  }
};

export default CourseStatusHelper;
