import { NavLink, Outlet } from "react-router-dom";

function Dashboard() {
  return (
    <div className="page">
      <h1>Welcome to Student Dashboard</h1>

      <div className="dashboard-layout">
        <div className="dashboard-menu">
          <NavLink to="profile">Profile</NavLink>
          <NavLink to="my-courses">My Courses</NavLink>
          <NavLink to="settings">Settings</NavLink>
        </div>

        <div className="dashboard-content">
          <Outlet />
        </div>
      </div>
    </div>
  );
}

export default Dashboard;