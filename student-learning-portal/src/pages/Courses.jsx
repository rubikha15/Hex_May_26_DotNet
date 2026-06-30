import { useNavigate } from "react-router-dom";
import { courses } from "../data/courses";

function Courses() {
  const navigate = useNavigate();

  return (
    <div className="page">
      <h1>Available Courses</h1>

      <div className="course-container">
        {courses.map((course) => (
          <div className="course-card" key={course.id}>
            <h2>{course.title}</h2>
            <p>Category: {course.category}</p>
            <p>Duration: {course.duration}</p>
            <p>Trainer: {course.trainer}</p>
            <button onClick={() => navigate(`/courses/${course.id}`)}>
              View Details
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Courses;