# 📄 Crystal Report Viewer – WinForms

A lightweight Windows Forms application (.NET Framework) that loads and displays Crystal Reports (.rpt) files with dynamic SQL Server connection settings.

---

## 🚀 Features

* 📂 Load Crystal Report files (.rpt)

* 🔗 Apply SQL Server connection parameters dynamically through app.config

* 🔐 Supports:

    * Windows Authentication (TrustedConnection = true)

    * SQL Server Authentication (User/Password)

* 👓 Integrated visualization using **CrystalReportViewer**

---

## 🛠️ Technologies Used

* C# – WinForms (.NET Framework 4.5)

* Crystal Reports Runtime

* CrystalDecisions.CrystalReports.Engine

* CrystalDecisions.Windows.Forms

* SQL Server


# 📂 Structure du projet
    /ReportCrystal
    │
    ├── homeForm.cs        # Main UI to load/view reports
    ├── homeForm.Designer.cs
    ├── app.config         # Database connection configuration
    └── ...                # Other project files

# ⚙️ Database Configuration
SQL Server connection settings are handled through app.config:

```xml
<appSettings>
    <add key="ServerName" value="SERVER-NAME"/>
    <add key="DatabaseName" value="DATABASE-NAME"/>
    <add key="TrustedConnection" value="true"/>
    <add key="UserID" value=""/>
    <add key="Password" value=""/>
</appSettings>
```

# 🔐 Connection Modes
### ✔️ Windows Authentication (Recommended)
```ini
TrustedConnection = true
UserID = ""
Password = ""
```
### ✔️ SQL Server Authentication
```ini
TrustedConnection = false
UserID = "sa"
Password = "xxxx"
```

# 🖥️ Usage

1. Launch the application.

2. Click Load Repor.

3. Select a .rpt file

4. The report will be displayed using the connection settings defined in app.config

# 📦 Prérequis

* Windows 10/11

* .NET Framework 4.5+

* SAP Crystal Reports Runtime (matching your Visual Studio install)

# 🔧 Installing Crystal Reports Runtime

SAP Crystal Reports Runtime (x64/x86)

# 📜 Licence

This project is free to use for internal or educational purposes.
