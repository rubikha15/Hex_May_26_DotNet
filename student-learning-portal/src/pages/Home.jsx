import { useNavigate } from "react-router-dom";

function Home() {
  const navigate = useNavigate();

  return (
    <div className="page center">
      <h1>Welcome to Student Learning Portal</h1>
      <p>
        Learn React, Web API, and Full Stack Development from one place.
      </p>

      <div className="button-group">
        <button onClick={() => navigate("/courses")}>View Courses</button>
        <button onClick={() => navigate("/dashboard")}>Go to Dashboard</button>
      </div>
    </div>
  );
}

export default Home;