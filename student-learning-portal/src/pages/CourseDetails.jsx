import { useParams, useNavigate } from "react-router-dom";
import { courses } from "../data/courses";

function CourseDetails() {
  const { courseId } = useParams();
  const navigate = useNavigate();

  const course = courses.find((c) => c.id === Number(courseId));

  if (!course) {
    return (
      <div className="page center">
        <h1>Course not found</h1>
        <button onClick={() => navigate("/courses")}>Back to Courses</button>
      </div>
    );
  }

  return (
    <div className="page details-card">
      <h1>Course Details</h1>
      <p>Course ID: {course.id}</p>
      <p>Title: {course.title}</p>
      <p>Category: {course.category}</p>
      <p>Duration: {course.duration}</p>
      <p>Trainer: {course.trainer}</p>
      <p>Description: {course.description}</p>

      <button onClick={() => navigate("/courses")}>Back to Courses</button>
    </div>
  );
}

export default CourseDetails;