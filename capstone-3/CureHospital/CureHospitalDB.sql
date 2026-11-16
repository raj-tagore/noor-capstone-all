USE [master]
GO
IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = N'CureHospitalDB' OR name = N'CureHospitalDB')))
DROP DATABASE CureHospitalDB

CREATE DATABASE CureHospitalDB
GO

USE CureHospitalDB
GO

CREATE TABLE Specialization
(
	SpecializationCode CHAR(3) CONSTRAINT pk_SpecializationCode PRIMARY KEY,
	SpecializationName VARCHAR(50) NOT NULL UNIQUE
)
GO

INSERT INTO Specialization VALUES('GYN','Gynecologist')
INSERT INTO Specialization VALUES('CAR','Cardiologist')
INSERT INTO Specialization VALUES('ANE','Anesthesiologist')
GO

CREATE TABLE Doctor
(
	DoctorID INT IDENTITY(1001,1) CONSTRAINT pk_DoctorID PRIMARY KEY,
	DoctorName VARCHAR(50) NOT NULL UNIQUE
)
GO

INSERT INTO Doctor VALUES('Albert')
INSERT INTO Doctor VALUES('Olivia')
INSERT INTO Doctor VALUES('Susan')
GO

CREATE TABLE DoctorSpecialization
(
	DoctorID INT CONSTRAINT fk_DoctorID REFERENCES Doctor(DoctorID),
	SpecializationCode CHAR(3) CONSTRAINT fk_SpecializationCode REFERENCES Specialization(SpecializationCode),
	SpecializationDate DATE NOT NULL,
	CONSTRAINT pk_DoctorIDSpecializationCode PRIMARY KEY (DoctorID, SpecializationCode)
)
GO

INSERT INTO DoctorSpecialization VALUES(1001,'ANE','2010-01-01')
INSERT INTO DoctorSpecialization VALUES(1002,'CAR','2016-10-30')
INSERT INTO DoctorSpecialization VALUES(1003,'CAR','2013-05-19')
GO

CREATE TABLE Surgery
(
	SurgeryID INT IDENTITY(5000,1) CONSTRAINT pk_SurgeryID PRIMARY KEY,
	DoctorID INT CONSTRAINT fk_DoctorID_Surgery REFERENCES Doctor(DoctorID),
	SurgeryDate DATE NOT NULL,
	StartTime DECIMAL(4,2) NOT NULL,
	EndTime DECIMAL(4,2) NOT NULL,
	SurgeryCategory CHAR(3) CONSTRAINT fk_SpecializationCode_Surgery REFERENCES Specialization(SpecializationCode)
)
GO

INSERT INTO Surgery VALUES(1001, DATEADD(DAY, 2, CAST(GETDATE() AS DATE)), 9.00, 14.00, 'ANE')
INSERT INTO Surgery VALUES(1002, DATEADD(DAY, 5, CAST(GETDATE() AS DATE)), 10.00, 16.00, 'CAR')
GO
