# KCA Student Result System

## 📌 Overview

The **KCA Student Result System** is a **C# console application** that allows users to:

- **Register students** with unique IDs.
- **Enter student results** (subjects, marks, grades, and GPA calculation).
- **View student results** in a formatted console output.
- **Generate a PDF result slip** using `iTextSharp`.

## 📂 Features

✅ **Student Registration** (`RegisterStudent()`) - Ensures unique student IDs.\
✅ **Enter Student Results** (`EnterResult()`) - Validates input and assigns grades.\
✅ **View Results** (`ViewResult()`) - Displays student results in a structured format.\
✅ **Generate PDF Report** (`GenerateReport()`) - Creates an official results slip.\
✅ **GPA Calculation** (`CalculateGPA()`) - Calculates GPA based on credit hours.\
✅ **Handles Errors Gracefully** - Prevents division by zero and invalid inputs.

## 🛠️ Technologies Used

- **C# (.NET)** - Core programming language.
- **iTextSharp** - Library for generating PDF reports.

## 📜 Installation & Setup

### **1️⃣ Prerequisites**

- Install **.NET SDK** (https\://dotnet.microsoft.com/en-us/download)
- Install **iTextSharp** for PDF generation
  ```sh
  dotnet add package itext7
  ```

### **2️⃣ Clone the Repository**

```sh
git clone https://github.com/yourusername/KCAStudentResultSystem.git
cd KCAStudentResultSystem
```

### **3️⃣ Compile & Run the Program**

```sh
dotnet run
```

## 📌 Usage Guide

1. **Register a Student** - Enter **Student ID, Name, and Class**.
2. **Enter Student Results** - Input **Subject Code, Description, Credit Hours, and Marks**.
3. **View Student Results** - Displays formatted results with **GPA**.
4. **Generate Report** - Creates a **PDF file** (`ResultSlip.pdf`) with results.
5. **Exit** - Close the program.

## 📊 Example Output (Console)

```
=====================================
       EXAMINATION RESULTS SLIP
=====================================
Student No: 
Name: CHEGE VICTOR
Faculty: SCHOOL OF TECHNOLOGY
Programme: BSc in Software Development
Semester: TRIM1 24

UNIT CODE   DESCRIPTION             CREDIT HOURS   GRADE   POINTS
--------------------------------------------------------------
BSD 2202    ICT PROJECT MANAGEMENT       3             
BSD 2301    NETWORK PROGRAMMING ENG      3              
--------------------------------------------------------------
TRIMESTER GPA:   TOTAL CREDIT HOURS:    TOTAL POINTS:
Signed: ........................................................
Dean  SCHOOL OF TECHNOLOGY
Date: ...........................................................
```

## 📌 Contribution Guide

✅ **Fork the repository**\
✅ **Create a new branch** (`feature/new-feature`)\
✅ **Commit changes** (`git commit -m "Added feature"`)\
✅ **Push to GitHub** (`git push origin feature/new-feature`)\
✅ **Create a Pull Request** 🎉

## 📄 License



## ✨ Author

Developed by **Victor Chege** 🎓

