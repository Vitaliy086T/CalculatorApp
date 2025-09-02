# CalculatorApp

A simple calculator with:
- **Backend (ASP.NET Core Web API)** — performs calculations.
- **Frontend (WinForms C#)** — user interface that connects to the API.

## How to Run

### Backend (API)
1. Open `CalculatorApi` project in Visual Studio.
2. Run it (F5).  
   It will start on `http://localhost:5218` (check your terminal).

### Frontend (WinForms)
1. Open `CalculatorFrontend` project in Visual Studio.
2. Run it.  
3. Enter an expression (e.g., `2+2*3`) and press **Calculate**.  
   The frontend will send the expression to the API and display the result.

## Tech Stack
- C# (.NET 6/7)
- ASP.NET Core Web API
- Windows Forms
- JSON (System.Text.Json)

## Notes
- Make sure backend is running before starting the frontend.
- API endpoint used: `http://localhost:5218/api/calculator/evaluate`.