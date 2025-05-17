# 🔐 Task – ASP.NET Core JWT Authentication with Product Tracker

A secure full-stack application using **ASP.NET Core Web API**, **Entity Framework Core**, and **JWT Authentication**, with a Bootstrap frontend for registration. Authenticated users can add products, and each product tracks which user added it using the `AddedBy` field. Tokens are stored in the database and verified before any protected action.

---

## 🛠 Technologies Used

| Area        | Tools/Frameworks                     |
|-------------|--------------------------------------|
| Backend     | ASP.NET Core Web API, Entity Framework Core |
| Authentication | JWT (JSON Web Tokens)             |
| Frontend    | HTML, Bootstrap 5, jQuery (AJAX)     |
| Database    | SQL Server                           |
| ORM         | Entity Framework Core                |

---

## 🎯 Key Features

- 👤 User Registration with real-time JWT Token generation
- 🔐 User Login and Token retrieval from `TokenMaster` table
- 🧾 Add Product feature secured with JWT Authorization
- 🧑 Tracks who added each product using `AddedBy` (EmployeeID)
- ✅ AJAX-based frontend using Bootstrap 5
- 📥 Tokens stored and verified against DB for extra security

---

