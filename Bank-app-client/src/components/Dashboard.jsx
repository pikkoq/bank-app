import React, { useState, useEffect } from "react";
import { Navigate } from "react-router-dom";
import "./Dashboard.css";
import TransferForm from "./Transfer";

function Dashboard() {
  const [accounts, setAccounts] = useState([]);
  const [isLoggedIn, setIsLoggedIn] = useState(true);

  useEffect(() => {
    const ownerId = localStorage.getItem("userId");
    if (!ownerId) {
      setIsLoggedIn(false);
      return;
    }

    fetch(`https://localhost:7191/api/Account/GetAccountsByOwnerId/${ownerId}`)
      .then((response) => {
        if (!response.ok) {
          throw new Error("Wystąpił błąd podczas pobierania kont");
        }
        return response.json();
      })
      .then((data) => {
        setAccounts(data.data);
        data.data.forEach((account) => {
          fetch(
            `https://localhost:7191/api/Transaction/GetTransactionsToByNumber/${account.accountNumber}`
          )
            .then((response) => {
              if (!response.ok) {
                throw new Error(
                  "Wystąpił błąd podczas pobierania transakcji przychodzących"
                );
              }
              return response.json();
            })
            .then((data) => {
              setAccounts((prevAccounts) => {
                return prevAccounts.map((prevAccount) => {
                  if (prevAccount.accountNumber === account.accountNumber) {
                    return {
                      ...prevAccount,
                      incomingTransactions: data.data,
                    };
                  }
                  return prevAccount;
                });
              });
            })
            .catch((error) => {
              console.error("Wystąpił błąd:", error);
            });

          fetch(
            `https://localhost:7191/api/Transaction/GetTransactionsFromByNumber/${account.accountNumber}`
          )
            .then((response) => {
              if (!response.ok) {
                throw new Error(
                  "Wystąpił błąd podczas pobierania transakcji wychodzących"
                );
              }
              return response.json();
            })
            .then((data) => {
              setAccounts((prevAccounts) => {
                return prevAccounts.map((prevAccount) => {
                  if (prevAccount.accountNumber === account.accountNumber) {
                    return {
                      ...prevAccount,
                      outgoingTransactions: data.data,
                    };
                  }
                  return prevAccount;
                });
              });
            })
            .catch((error) => {
              console.error("Wystąpił błąd:", error);
            });
        });
      })
      .catch((error) => {
        console.error("Wystąpił błąd:", error);
      });
  }, []);

  if (!isLoggedIn) {
    return <Navigate to="/login" />;
  }

  return (
    <div className="dashboard-container">
      <h2>Twoje konta</h2>
      {accounts.map((account) => (
        <div key={account.accountNumber}>
          <h3>
            Numer konta: {account.accountNumber}, Saldo: {account.balance} zł
          </h3>
          <h4>Przychodzące transakcje:</h4>
          <ul>
            {account.incomingTransactions &&
              account.incomingTransactions.map((transaction) => (
                <li key={transaction.transactionId}>
                  Data: {transaction.date}, Kwota: {transaction.ammount}, Od:{" "}
                  {transaction.fromAccountNumber}, Tytuł: {transaction.title},
                  Opis: {transaction.description}
                </li>
              ))}
          </ul>
          <h4>Wychodzące transakcje:</h4>
          <ul>
            {account.outgoingTransactions &&
              account.outgoingTransactions.map((transaction) => (
                <li key={transaction.transactionId}>
                  Data: {transaction.date}, Kwota: {transaction.ammount}, Do:{" "}
                  {transaction.toAccountNumber}, Tytuł: {transaction.title},
                  Opis: {transaction.description}
                </li>
              ))}
          </ul>
        </div>
      ))}
          <TransferForm></TransferForm>

    </div>
  );
}

export default Dashboard;
