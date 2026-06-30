import { useNavigate } from "react-router-dom";

function Contact() {
  const navigate = useNavigate();

  return (
    <div className="page">
      <h1>Contact Us</h1>
      <p>Email: support@studentportal.com</p>
      <p>Phone: 9876543210</p>
      <p>Location: Chennai, India</p>

      <button onClick={() => navigate(-1)}>Go Back</button>
    </div>
  );
}

export default Contact;