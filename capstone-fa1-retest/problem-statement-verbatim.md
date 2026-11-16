# CureHospital – FA1 Retest (Problem Statement, verbatim)

> Transcribed word-for-word from the photos in `ps-pics/`. Spelling and grammar are kept exactly as on screen.
> Parts that were cut off or not photographed are marked **[not captured]** or **[cut off]**.

---

**[not captured – beginning of the problem statement]**

… reduction in your score even if the code is executing without any errors

**Guidelines to set up the SQL scripts:**

- Open the SQL scripts given inside CureHospitalDALCrossPlatform

  ```
  ◢ CureHospitalDALCrossPlatform
    ▷ Dependencies
    ▷ Models
      CureHospitalDB.sql
      EduAppsettings.json
    ▷ C# HospitalRepository.cs
  ```

- Click on connect button present in query editor window

  ```
  CureHospitalDB.sql
  1  USE [master]
  2  GO
  ```

- Provide the Server Name in the connect window and click on connect

  *(Connect window – History / Browse)*

**[not captured]**

… ASP.NET Web API Core.

---

## DATABASE

**Database Design:**

```
Doctor                  DoctorSpecialization
🔑 DoctorID             🔑 DoctorID
   DoctorName           🔑 SpecializationCode
                           SpecializationDate

Surgery                 Specialization
🔑 SurgeryID            🔑 SpecializationCode
   DoctorID                SpecializationName
   SurgeryDate
   StartTime
   EndTime
   SurgeryCategory
```

(Doctor → DoctorSpecialization, Doctor → Surgery, Specialization → DoctorSpecialization, Specialization → Surgery)

Execute the provided database scripts in SQL Server. Following is the description of the database objects.

### TABLES

#### 1. Table Name: Specialization

This table contains the details of all specializations.

**Fields:**

| Field Name | Data Type | Description |
| --- | --- | --- |
| <u>SpecializationCode</u> | CHAR (3) | Primary key |
| SpecializationName | VARCHAR (50) | Name of the specialization, mandatory, should be unique |

**Sample Data:**

| <u>SpecializationCode</u> | SpecializationName |
| --- | --- |
| GYN | Gynecologist |
| CAR | Cardiologist |
| ANE | Anesthesiologist |

#### 2. Table Name: Doctor

This table contains the details of doctors.

**Fields:**

| Field Name | Data Type | Description |
| --- | --- | --- |
| <u>DoctorID</u> | INT | Primary key, IDENTITY, starts from 1001 and incremented by 1 |
| DoctorName | VARCHAR (50) | Name of the doctor, Mandatory, should be unique |

**Sample Data:**

| <u>DoctorID</u> | DoctorName |
| --- | --- |
| 1001 | Albert |
| 1002 | Olivia |
| 1003 | Susan |

#### 3. Table Name: DoctorSpecialization

This table contains the specialization details of doctors.

**Fields:**

| Field Name | Data Type | Description |
| --- | --- | --- |
| <u>DoctorID</u> | INT | Should be an existing DoctorID in **Doctor** table, mandatory |
| <u>SpecializationCode</u> | CHAR (3) | Should be an existing SpecializationCode in **Specialization** table, mandatory |
| SpecializationDate | DATE | Date on which specialization is added, mandatory |

**Note: Combination of DoctorID and SpecializationCode is the primary key of the table**

**Sample Data:**

| <u>DoctorID</u> | <u>SpecializationCode</u> | SpecializationDate |
| --- | --- | --- |
| 1001 | ANE | 2010-01-01 |
| 1002 | CAR | 2016-10-30 |
| 1003 | CAR | 2013-05-19 |

#### 4. Table Name: Surgery

This table contains the details of all the surgeries.

**Fields:**

| Field Name | Data Type | Description |
| --- | --- | --- |
| <u>SurgeryID</u> | INT | Primary key, IDENTITY, starts with 5000 and auto-incremented by 1 |
| DoctorID | INT | Should be an existing DoctorID in **Doctor** table, mandatory |
| SurgeryCategory | CHAR (3) | Should be an existing SpecializationCode in **Specialization** table, mandatory |
| SurgeryDate | DATE | Date of the surgery, mandatory |
| StartTime | DECIMAL (4, 2) | Start time of the surgery, mandatory |
| EndTime | DECIMAL (4, 2) | End time of the surgery, mandatory |

**Sample Data:**

| <u>SurgeryID</u> | DoctorID | SurgeryCategory | SurgeryDate | StartTime | EndTime |
| --- | --- | --- | --- | --- | --- |
| 5000 | 1001 | ANE | Current Date + 2 Days | 09.00 | 14.00 |
| 5001 | 1002 | CAR | Current Date + 5 Days | 10.00 | 16.00 |

**Note:** StartTime and EndTime should be in HH.MM (24 hours) format.

---

### Stored Procedure: usp_AddSurgeryDetails

This procedure is used to insert the surgery details into Surgery table and provide the newly generated SurgeryID as OUTPUT parameter.

**Input Parameters:**

- DoctorID INT
- SurgeryCategory CHAR (3)
- SurgeryDate DATE
- StartTime DECIMAL (4,2)
- EndTime DECIMAL (4,2)

**Output Parameters:**

- SurgeryID INT

**Functionality:**

- Checks if DoctorID exists in Doctor table
- Checks if SurgeryCategory exists in Specialization table as SpecializationCode
- Checks if combination of DoctorID and SurgeryCategory exists in DoctorSpecialization table
- Checks if start time is less than end time
- Checks if the doctor is available between the start time and end time for the given surgery date
- Checks if the surgery date is less than the present date
- If all the validations are successful, inserts the data into Surgery table and set the OUT parameter to the newly generated SurgeryID
- In all other cases, sets the OUT parameter to 0

**Return Values:**

- 1, in case of successful insertion
- -1, if DoctorID does not exist in Doctor table
- -2, if SurgeryCategory does not exist in Specialization table as SpecializationCode
- -3, if the combination of DoctorID and SurgeryCategory does not exist in DoctorSpecialization table
- -4, if start time is less than end time
- -5, if doctor is not available for the given date and time period
- -6, if surgery date is less than present date
- -99, in case of any exception

### Stored Procedure: usp_AddDoctorDetails

This procedure is used to insert details of doctors and their specialization in Specialization table.

**Input Parameters:**

- DoctorName VARCHAR (50)
- SpecializationCode CHAR (3)
- SpecializationName VARCHAR (50)

**Functionality:**

- Checks if the SpecializationCode exists in Specialization table
  - If SpecializationCode does not exists, inserts the SpecializationCode and SpecializationName in Specialization table
- Inserts the doctor details in the Doctor table
- Inserts the newly generated DoctorID and the given SpecializationCode in DoctorSpecialization table with SpecializationDate as current date

**Return Values:**

- 1, in case of successful insertion
- -99, in case of any exception

### Function: ufn_FetchSurgeryDetails

A table valued function, used to fetch the details of surgeries and return a table for the given surgery date

**Return Values:**

A table containing following fields:

| SurgeryID | DoctorName | SpecializationName | SurgeryDate | StartTime |
| --- | --- | --- | --- | --- |

### Function: ufn_FetchSurgeryCount

A scalar function, used to fetch the number of surgeries for a given specialization and date.

**Input Parameters:**

- SpecializationName VARCHAR (50)
- SurgeryDate DATE

**Functionality:**

- Fetch the number of the surgeries done from Surgery table for a specialization on a given datex

**Return Values:**

- Count INT

---

## Data Access Layer

**[30 Marks]**

**Note:**

- Provided solution has:
  - **CureHospitalDALCrossPlatform** to be implemented using **Entity Framework Core**
  - **CureHospitalTest** for testing the DAL methods
  - **CureHospitalWebService** to be implemented using **ASP.NET Core Web API**
- Right click on CureHospitalTest project -> Set as Startup Project to execute the application. You have been provided with one sample value in Program.cs, ensure that your code works for all possible values of input
- The connection string in appsettings.json is saved with the name 'DBConnectionString'
- Implement structured exception handling in all the methods.

**[2 Marks]**

### Entity Framework Core

- **Models** folder is already available with the entities and context class for the given **[cut off]**

**[not captured]**

| Sl. No | Method Name | Description | Return Values |
| --- | --- | --- | --- |
| 1 | FetchDoctorIDs | Fetch all the DoctorID(s) from the DoctorSpecialization table whose specialization code matches the given specializationCode | **List\<int\>** whose SpecializationCode matches the given code<br><br>In case of an excepti**[cut off]** return value should b**[cut off]** **null** |
| 2 | AddDoctorSpecialization | Add the given details to the DoctorSpecialization table | **true** – In case of successful execution<br><br>**false** – In case of failure or exception |
| 3 | UpdateSurgeryTime | Update the surgery start time and end time for the given surgeryID in the Surgery table | **1** – In case of successful execution<br><br>**-1** – In case EndTime not greater than StartTime<br><br>**-98** – In case of an exception |
| 4 | RemoveSurgeryDetails | Delete all the surgery details for the given surgery date from the Surgery table | **true** – In case of successful execution<br><br>**false**– In case of an exception or if no surgery details are found |
| 5 | AddSurgeryDetails | Add the new surgery details to the Surgery table by invoking the stored procedure **usp_AddSurgeryDetails** | SurgeryID as OUT parameter from the stored procedure<br><br>Return the return val**[cut off]** from stored procedu**[cut off]**<br><br>In case of an exceptio**[cut off]**<br>• return value shou**[cut off]** be set to **-98**<br>• out parameter should be set to **[cut off]** |
| 6 | GetSurgeryDetails | Fetch all surgery details by invoking the table valued function **ufn_FetchSurgeryDetails** for the given SurgeryDate<br><br>NOTE: Use the class **SurgeryDetail** in Models folder | **List\<SurgeryDetail\>** whose SurgeryDate matches the given id**[cut off]**<br><br>In case of an exceptio**[cut off]** return value should**[cut off]** **null** |

**Use the following inputs for testing DAL methods against the return and OUT parameter values:**

**Method Name:** AddSurgeryDetails

| doctorID | surgeryCategory | surgeryDate | startTime | endTime | Return value | OUT parameter |
| --- | --- | --- | --- | --- | --- | --- |
| 1003 | CAR | 2025-05-09 | 10 | 12 | 1 | 5002 |
| **1005** | CAR | 2025-05-09 | 10 | 12 | -1 | 0 |
| 1003 | **ENT** | 2025-05-09 | 10 | 12 | -2 | 0 |
| **1001** | **CAR** | 2025-05-09 | 10 | 12 | -3 | 0 |
| 1003 | CAR | 2025-05-09 | **12** | 10 | -4 | 0 |
| 1003 | null | 2025-05-09 | 10 | 12 | -98 | 0 |

**Method Name:** UpdateSurgeryTime

| surgeryID | newStartTime | newEndTime | Return Value |
| --- | --- | --- | --- |
| 5000 | 11 | 15 | 1 |
| 5000 | **15** | 11 | -1 |
| **5007** | 11 | 15 | -99 |

---

## Service Layer using ASP.NET Web API Core

**[20 Marks]**

- Implement appropriate exception handling in all the actions

**[3 Mark]**

- Map all the actions with appropriate HTTP verbs

**Note**: The HTTP verb should match based on the operations performed

**[3 Marks]**

- The web service contains DoctorSpecialization and Surgery model which are already implemented as shown in the below class diagram

  ```
  DoctorSpecialization
  Class

  ◢ Properties
     DoctorId { get; set; } : int
     SpecializationCode { get; set; } : string
     SpecializationDate { get; set; } : DateTime
  ```

- Following are the business validations to be done on the **DoctorSpecialization** model:
  - All fields are mandatory
  - DoctorID should accept values only from 1001 to maximum value of int
  - Length of SpecializationCode should be exactly three
- Following are the business validations to be done on the **Surgery** model:
  - All fields except SurgeryId is mandatory
  - StartTime and EndTime should accept values only from the decimal type of values: 0.00 to 23.00

**[4 Marks]**

- Right click on **CureHospitalWebService** project and set as Startup Project
- Implement the following functionalities using ASP.NET Core Web API

| Sl. No | Method Name | Description | Return Values | Marks |
| --- | --- | --- | --- | --- |
| 1. | FetchDoctorIDs | Fetch DoctorIDs based on specialization code by invoking the CureHospitalRepositoryEF method **FetchDoctorIDs** | Value returned by data access layer method in the JSON format.<br><br>In case of an exception, **null** should be returned | 1.5 |
| 2. | AddDoctorSpecialization | Add doctor specialization details to the DoctorSpecialization table by invoking the CureHospitalRepositoryEF method **AddDoctorSpecialization** | Value returned by data access layer method<br><br>In case of an exception, **null** should be returned | 1.5 |
| 3. | UpdateSurgeryTime | Update StartTime and EndTime in Surgery table for the provided surgeryId by invoking CureHospitalRepositoryEF method **UpdateSurgeryTime** | Value returned by data access layer method<br><br>In case of an exception, -98 should be returned | 1.5 |
| 4. | RemoveSurgeryDetails | Delete all the surgery details for the given surgeryDate from the Surgery table by invoking the CureHospitalRepositoryEF method **RemoveSurgeryDetails** | Value returned by data access layer method<br><br>In case of an exception, **null** should be returned as JsonResult | 1.5 |
| 5 | **AddSurgeryDetails** | Add surgery details to the Surgery table by invoking the CureHospitalRepositoryEF method **AddSurgeryDetails** | A JsonResult value, based on the return value by the data access layer method:<br>• If 1 is returned by the DAL, return **surgeryId**<br>• Otherwise, return the value returned by the DAL<br>• In case of an exception, **-95** should be returned | 3 |
| 6. | GetSurgeryDetails | Get surgery details based on surgeryDate by invoking the CureHospitalRepositoryEF method **GetSurgeryDetails** | Value returned by data access layer method in the JSON format.<br><br>In case of an exception, **null** should be returned | 1 |

**Use the following inputs for testing the Web API methods against the return and OUT parameter values:**

**Method Name:** AddSurgeryDetails

| Model Input | Return Value in JSON |
| --- | --- |
| {<br>DoctorId = 1003,<br>SurgeryCategory = CAR,<br>SurgeryDate = 2025-05-09,<br>StartTime = 10<br>EndTime = 12<br>} | 5002 |
| {<br>DoctorId = **1005**,<br>SurgeryCategory = CAR,<br>SurgeryDate = 2025-05-09,<br>StartTime = 10<br>EndTime = 12<br>} | -1 |
| {<br>DoctorId = 1003,<br>SurgeryCategory = **ENT**,<br>SurgeryDate = 2025-05-09,<br>StartTime = 10<br>EndTime = 12<br>} | -2 |
| {<br>DoctorId = **1001**,<br>SurgeryCategory = **CAR**,<br>SurgeryDate = 2025-05-09,<br>StartTime = 10<br>EndTime = 12<br>} | -3 |

**Method Name:** UpdateSurgeryTime

| surgeryID | newStartTime | newEndTime | ReturnValue |
| --- | --- | --- | --- |
| 5000 | 11 | 15 | 1 |
| 5000 | **15** | 11 | -1 |
| **5007** | 11 | 15 | -98 |
