import React, { useState } from "react";
import axios from "axios"; // Dodaj import axios, jeśli jeszcze tego nie zrobiłeś

function Transfer() {
  const [formData, setFormData] = useState({
    fromAccountNumber: "",
    toAccountNumber: "",
    ammount: "",
    title: "",
    description: "",
  });

  const handleSubmit = async (e) => {
    e.preventDefault();

    // Ustaw datę na bieżącą przed wysłaniem żądania
    const transactionData = {
      ...formData,
      date: new Date().toISOString(), // Ustawienie bieżącej daty i czasu
    };

    try {
      // Wywołanie endpointu AddTransaction z użyciem axios
      const response = await axios.post(
        "https://localhost:7191/api/Transaction/AddTransaction",
        transactionData
      );
      console.log("Response:", response.data);
    } catch (error) {
      console.error("Error:", error);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevFormData) => ({
      ...prevFormData,
      [name]: value,
    }));
  };

  return (
    <div>
      <h2>Wykonaj przelew</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="fromAccountNumber">Z konta:</label>
          <input
            type="text"
            id="fromAccountNumber"
            name="fromAccountNumber"
            value={formData.fromAccountNumber}
            onChange={handleChange}
          />
        </div>
        <div>
          <label htmlFor="toAccountNumber">Na konto:</label>
          <input
            type="text"
            id="toAccountNumber"
            name="toAccountNumber"
            value={formData.toAccountNumber}
            onChange={handleChange}
          />
        </div>
        <div>
          <label htmlFor="ammount">Kwota:</label>
          <input
            type="number"
            id="ammount"
            name="ammount"
            value={formData.ammount}
            onChange={handleChange}
          />
        </div>
        <div>
          <label htmlFor="title">Tytuł:</label>
          <input
            type="text"
            id="title"
            name="title"
            value={formData.title}
            onChange={handleChange}
          />
        </div>
        <div>
          <label htmlFor="description">Opis:</label>
          <input
            type="text"
            id="description"
            name="description"
            value={formData.description}
            onChange={handleChange}
          />
        </div>
        <button type="submit">Wykonaj przelew</button>
      </form>
    </div>
  );
}

export default Transfer;
