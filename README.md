# Employee Task Manager 🚀

A professional ASP.NET Core MVC web application to manage employees and track task progress with authentication & role-based authorization.

---

## ⭐ Features

| Feature | Description |
|--------|-------------|
| 🔐 Authentication | ASP.NET Identity Login & Register |
| 👥 Roles | Admin & Employee Roles |
| 👩‍💼 Admin Dashboard | Manage Employees + All Tasks Access |
| 🧑‍💻 Employee Panel | Update Assigned Task Status Only |
| 📊 Analytics | Task counters on Dashboard |
| 🔎 Filters | Filter tasks by status & employee |
| 🎨 Modern UI | Bootstrap Clean & Professional Theme |

---

## 🛠️ Technologies Used

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Identity Authentication
- Bootstrap 5
- Razor Views

---

## 📸 Screenshots

### 1️⃣ Login Page (User Authentication)
<img width="1511" height="784" alt="Screenshot 2025-12-01 183028" src="https://github.com/user-attachments/assets/25e97893-72b0-4347-8ed7-579e4cb3899e" />



### 2️⃣ Dashboard – Admin View
📊 Shows task stats + recent tasks
<img width="1896" height="856" alt="Screenshot 2025-12-01 180031" src="https://github.com/user-attachments/assets/8a66753d-5e73-43b1-91b4-b76f71717910" />


### 3️⃣ Employee List – View/Edit/Delete
<img width="1897" height="826" alt="Screenshot 2025-12-01 182052" src="https://github.com/user-attachments/assets/dd91facf-5521-471c-947a-a0eafcd95ecd" />


### 4️⃣ Add New Employee Form
<img width="1917" height="830" alt="Screenshot 2025-12-01 180601" src="https://github.com/user-attachments/assets/9df0f209-13b0-4c6f-86af-36e21e3b3919" />


### 5️⃣ Tasks List – Admin (Full Actions)
<img width="1919" height="856" alt="Screenshot 2025-12-01 180624" src="https://github.com/user-attachments/assets/251d96af-484c-496c-847e-156cc92737be" />


### 6️⃣ Create Task Form
<img width="1891" height="865" alt="Screenshot 2025-12-01 180544" src="https://github.com/user-attachments/assets/70e9d9d3-32fb-429c-ae0b-17901aa4a23f" />


### 7️⃣ Task List — Employee (Only Status Update)
<img width="1916" height="809" alt="Screenshot 2025-12-01 180247" src="https://github.com/user-attachments/assets/33a9f091-c001-479c-b2a1-740a49c20776" />


### 8️⃣ Dashboard — Employee View
📌 Shows only employee’s own tasks
<img width="1918" height="863" alt="Screenshot 2025-12-01 180218" src="https://github.com/user-attachments/assets/785950bf-ebce-45af-a839-a06acb8eba69" />


---

## 🚀 How to Run Locally

```bash
git clone https://github.com/Riicorn/EmployeeTaskManager.git
cd EmployeeTaskManager
dotnet ef database update
dotnet run
