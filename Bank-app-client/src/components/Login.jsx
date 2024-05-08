import React, { useState } from "react";
import { Navigate } from "react-router-dom";
import "./Login.css";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  async function handleLogin(event) {
    event.preventDefault();
    try {
      const response = await fetch(
        `https://localhost:7191/api/User/GetAllUsers`
      );
      if (!response.ok) {
        throw new Error("Wystąpił błąd podczas pobierania danych użytkowników");
      }
      const { data: usersData } = await response.json();
      const user = usersData.find(
        (user) => user.login === username && user.password === password
      );
      if (!user) {
        throw new Error("Nieprawidłowa nazwa użytkownika lub hasło");
      }
      // Zapisanie identyfikatora użytkownika w localStorage
      localStorage.setItem("userId", user.userId);
      setIsLoggedIn(true);
    } catch (error) {
      setIsLoggedIn(false);
      console.error("Wystąpił błąd podczas logowania:", error);
    }
  }

  if (isLoggedIn) {
    return <Navigate to="/dashboard" />;
  }

  return (
    <div className="login-container">
      <h2>Logowanie</h2>
      <form onSubmit={handleLogin}>
        <div>
          <label htmlFor="username">Nazwa użytkownika:</label>
          <input
            type="text"
            id="username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
        </div>
        <div>
          <label htmlFor="password">Hasło:</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <button type="submit">Zaloguj</button>
      </form>
    </div>
  );
}

export default Login;
