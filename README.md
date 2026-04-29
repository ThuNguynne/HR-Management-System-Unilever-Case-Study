# HR Management System (Unilever Case Study)

> **Academic course project** — End-to-end simulation of an HR management system for a multinational enterprise environment, covering business analysis, database design, and application development.

---

## Business Problem

Manual HR workflows create bottlenecks in recruitment processing, contract tracking, payroll calculation, and attendance monitoring. This project models a structured IT solution to digitise and automate these processes across eight HR functional areas.

---

## Functional Scope

| Module | Key Capabilities |
|---|---|
| Recruitment | Job posting, candidate tracking, offer management |
| Employee Contracts | Contract creation, renewal alerts, expiry automation |
| Timekeeping & Attendance | Check-in/out logging, shift management |
| Payroll | Automated salary calculation, deduction rules |
| Leave Management | Leave requests, approval workflow, balance tracking |
| Training | Training registration, completion tracking |
| Reporting | Power BI dashboard for HR KPIs |
| Access Control | Role-based permission management (RBAC) |

---

## Analysis Artifacts

This project follows a structured BA methodology:

- **Business Requirements Document (BRD)**
- **BPMN AS-IS / TO-BE Process Models** — 8 modules
- **User Stories & Use Cases**
- **SWOT Analysis**
- **Traceability Matrix** — requirements ↔ system features
- **Entity Relationship Diagram (ERD)** — 20+ tables

> Full documentation available in [Google Drive](https://drive.google.com/drive/folders/1d4izAH_1GSmGq65p26CTJyKuMVViXpJ_)

---

## Technology Stack

| Layer | Technology |
|---|---|
| Database | SQL Server / SSMS |
| Application | C# .NET WinForms (3-tier architecture) |
| Reporting | Power BI |
| Modeling | Enterprise Architect, Draw.io |

---

## Key Technical Features

- **Payroll automation** via stored procedures
- **Audit logging** via SQL triggers on sensitive tables
- **Auto contract-expiry alert** triggered at configurable thresholds
- **RBAC access control** — permissions scoped by role (Admin, HR Manager, Staff)

---

## System Architecture

```
Presentation Layer  →  C# WinForms UI
Business Logic Layer  →  Service classes, validation, rules
Data Access Layer  →  SQL Server stored procedures, triggers
```

---

## Repository Structure

```
/Forms          → WinForms UI screens
/Services       → Business logic layer
/SQL            → Schema scripts, stored procedures, triggers
/docs           → BRD, BPMN diagrams, ERD (excerpts)
/screenshots    → Application UI screenshots
```

---

## Screenshots

<img width="454" height="275" alt="image" src="https://github.com/user-attachments/assets/e7b0e853-6b51-4f8e-b6ef-161f6f94749e" />

<img width="454" height="270" alt="image" src="https://github.com/user-attachments/assets/78c945eb-6a2c-4d93-9a84-f344c2628ad6" />

<img width="454" height="272" alt="image" src="https://github.com/user-attachments/assets/9e1dae07-1624-4fd0-b18d-cbe632f4d6b8" />

<img width="454" height="270" alt="image" src="https://github.com/user-attachments/assets/693a91c9-1f4e-4bca-aabe-f83ada3ccd9f" />
---

## Academic Note

This repository is an academic course project developed for portfolio and internship review purposes. It simulates a real-world enterprise scenario and does not represent work performed for or on behalf of Unilever.

**Course:** Management Information Systems — University of Finance & Marketing (UFM)  
**Period:** November – December 2025  
**Contributor:** Nguyen Anh Thu
