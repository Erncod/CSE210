# Eternal Quest Program

## 📘 Overview
The **Eternal Quest Program** is a goal-tracking system that helps users stay motivated and record their personal progress.  
It applies **Object-Oriented Programming (OOP)** principles—**encapsulation**, **inheritance**, and **polymorphism**—to manage different types of goals with reusable and extendable code.

This project was developed as part of **Week 6 (Polymorphism)** coursework.

---

## 🎯 Features
- Create and manage different types of goals:
  - **Simple Goals** – Can be completed once.
  - **Eternal Goals** – Never complete but reward points each time.
  - **Checklist Goals** – Require multiple completions and offer a bonus when finished.
- View all goals and track your total score.
- Record goal events and earn points.
- Save and load progress from a file.
- Clean and consistent class design using **OOP best practices**.

---

## 💡 Extra Creativity
This project includes a **“Level-Up” feature**:
- Each time a user reaches a milestone (e.g., every 500 points), a congratulatory message appears.
- Adds fun and motivation to the goal-tracking experience.

---

## 🧩 OOP Concepts Used
- **Encapsulation:** All member variables are private or protected.
- **Inheritance:** `SimpleGoal`, `EternalGoal`, and `ChecklistGoal` all derive from the `Goal` base class.
- **Polymorphism:** Methods like `RecordEvent()` and `GetDetailsString()` are overridden by each derived class to provide specific behaviors.

---

## ⚙️ How to Run
1. Clone this repository or download the files.
2. Open the project in **Visual Studio** or **VS Code**.
3. Build and run the program.
4. Follow the on-screen menu to:
   - Create goals  
   - Record events  
   - View score  
   - Save or load progress  

---

## 🧑‍💻 Author
**Eze Ernest Tobechukwu**  
BYU-Idaho – Week 6: Eternal Quest Program

---

## 🏅 License
This project is for educational purposes only and part of coursework assignments.  
Feel free to explore, modify, and learn from it.
