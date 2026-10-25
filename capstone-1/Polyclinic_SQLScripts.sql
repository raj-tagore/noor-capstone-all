USE [master]
GO
IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = N'PolyclinicDB'OR name = N'PolyclinicDB')))
DROP DATABASE PolyclinicDB

CREATE DATABASE PolyclinicDB
GO

USE PolyclinicDB
GO

IF OBJECT_ID('Doctors')  IS NOT NULL
DROP TABLE Doctors
GO

IF OBJECT_ID('Patients') IS NOT NULL
DROP TABLE Patients
GO

IF OBJECT_ID('Appointments') IS NOT NULL
DROP TABLE Appointments
GO

IF OBJECT_ID('usp_GetDoctorAppointment') IS NOT NULL
DROP PROC usp_GetDoctorAppointment
GO

IF OBJECT_ID('ufn_FetchAllAppointments') IS NOT NULL
DROP FUNCTION ufn_FetchAllAppointments
GO

IF OBJECT_ID('ufn_CalculateDoctorFees')  IS NOT NULL
DROP FUNCTION ufn_CalculateDoctorFees
GO

CREATE TABLE Doctors
(
	[DoctorID] CHAR(3) CONSTRAINT pk_DoctorID PRIMARY KEY,
	[Specialization] VARCHAR(40) NOT NULL,
	[DoctorName] VARCHAR(50) NOT NULL,
	[Fees] MONEY NOT NULL
)
GO 

-- Insertion scripts for Doctors
INSERT INTO Doctors(DoctorID, Specialization, DoctorName, Fees) VALUES('D1','Physician','Jacob Johnson',500)
INSERT INTO Doctors(DoctorID, Specialization, DoctorName, Fees) VALUES('D2','orthopaedics','Smith Garry',600)
INSERT INTO Doctors(DoctorID, Specialization, DoctorName, Fees) VALUES('D3','Pediatrics','Anna Kirsten',500)
INSERT INTO Doctors(DoctorID, Specialization, DoctorName, Fees) VALUES('D4','Dermatology','Jennifer Kane',500)
GO

CREATE TABLE Patients
(
	[PatientID] CHAR(4) CONSTRAINT pk_PatientID PRIMARY KEY CONSTRAINT chk_PatientID CHECK(PatientID LIKE 'P%'),
	[PatientName] VARCHAR(40) NOT NULL,
	[Age] TINYINT NOT NULL,
	[Gender] CHAR(1) CONSTRAINT chk_Gender CHECK(Gender='F' OR Gender='M') NOT NULL,
	[ContactNumber] VARCHAR(10) NOT NULL	
)
GO

-- Insertion script for Patients
INSERT INTO Patients(PatientID, PatientName, Age, Gender, ContactNumber) VALUES('P101','Laila',26,'F','9999998855')
INSERT INTO Patients(PatientID, PatientName, Age, Gender, ContactNumber) VALUES('P102','Anne',23,'F','9988996611')
INSERT INTO Patients(PatientID, PatientName, Age, Gender, ContactNumber) VALUES('P103','Jane',53,'F','6666668855')
INSERT INTO Patients(PatientID, PatientName, Age, Gender, ContactNumber) VALUES('P104','Feroz',18,'M','1199998833')
INSERT INTO Patients(PatientID, PatientName, Age, Gender, ContactNumber) VALUES('P105','Amiya',46,'F','7779998822')
INSERT INTO Patients(PatientID, PatientName, Age, Gender, ContactNumber) VALUES('P106','Susan',31,'F','6666668880')
INSERT INTO Patients(PatientID, PatientName, Age, Gender, ContactNumber) VALUES('P107','Leo',69,'M','9999971133')
INSERT INTO Patients(PatientID, PatientName, Age, Gender, ContactNumber) VALUES('P108','Dennis',22,'M','3333338855')
INSERT INTO Patients(PatientID, PatientName, Age, Gender, ContactNumber) VALUES('P109','Maybel',33,'F','9944665511')
INSERT INTO Patients(PatientID, PatientName, Age, Gender, ContactNumber) VALUES('P110','Richard',35,'M','8766443355')
GO

CREATE TABLE Appointments
(
	[AppointmentNo] INT CONSTRAINT pk_AppointmentNo PRIMARY KEY IDENTITY,
	[PatientID] CHAR(4) CONSTRAINT fk_PatientID REFERENCES  Patients(PatientID) NOT NULL,
	[DoctorID] CHAR(3) CONSTRAINT fk_DoctorID REFERENCES Doctors(DoctorID) NOT NULL,
	[DateofAppointment] DATE CONSTRAINT chk_DateofAppointment CHECK(DateofAppointment >= CONVERT(DATE,GETDATE())) NOT NULL
)
GO

-- Insertion script for Appointments
SET IDENTITY_INSERT Appointments ON
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(1,'P101','D1', DATEADD(d, 15, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(2,'P102','D2', DATEADD(d, 5, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(3,'P101','D2', DATEADD(d, 5, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(4,'P103','D3', DATEADD(d, 10, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(5,'P104','D4', DATEADD(d, 10, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(6,'P105','D1', DATEADD(d, 10, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(7,'P106','D4', DATEADD(d, 7, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(8,'P107','D3', DATEADD(d, 5, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(9,'P108','D4', DATEADD(d, 7, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(10,'P109','D4', DATEADD(d, 5, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(11,'P110','D2', DATEADD(d, 5, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(12,'P107','D3', DATEADD(d, 2, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(13,'P107','D2', DATEADD(d, 6, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(14,'P102','D1', DATEADD(d, 2, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(15,'P105','D2', DATEADD(d, 3, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(16,'P106','D4', DATEADD(d, 15, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(17,'P103','D1', DATEADD(d, 15, CONVERT(DATE, GETDATE())))
INSERT INTO Appointments(AppointmentNo,PatientID,DoctorID,DateofAppointment) VALUES(18,'P105','D1', DATEADD(d, 15, CONVERT(DATE, GETDATE())))
SET IDENTITY_INSERT Appointments OFF
GO

CREATE PROCEDURE usp_GetDoctorAppointment
(
	@PatientID CHAR(4),
	@DoctorID CHAR(3),
	@DateOfAppointment DATE,
	@AppointmentNo INT OUTPUT
)
AS
BEGIN
	DECLARE @RetVal INT
	SET @AppointmentNo = 0	
	BEGIN TRY
		IF NOT EXISTS (SELECT @PatientID FROM Patients WHERE PatientID = @PatientID)
			SET @RetVal = -1
		IF NOT EXISTS (SELECT @DoctorID FROM Doctors WHERE DoctorID = @DoctorID)
			SET @RetVal = -2
		IF (@DateOfAppointment < CONVERT(DATE, GETDATE()))
			SET @RetVal = -3
		IF EXISTS (SELECT @PatientID FROM Appointments WHERE PatientID=@PatientID AND DoctorID=@DoctorID AND DateOfAppointment=@DateOfAppointment)
		BEGIN
			SELECT @AppointmentNo=AppointmentNo FROM Appointments WHERE PatientID=@PatientID AND DoctorID=@DoctorID AND DateOfAppointment=@DateOfAppointment
			SET @RetVal = 2	
		END 
		IF NOT EXISTS (SELECT @PatientID FROM Appointments WHERE PatientID=@PatientID AND DoctorID=@DoctorID AND DateOfAppointment=@DateOfAppointment)		
		BEGIN
			INSERT INTO Appointments VALUES (@PatientID, @DoctorID, @DateOfAppointment)
			SELECT @AppointmentNo=IDENT_CURRENT('Appointments')	
			SET @RetVal = 1
		END			
		RETURN @RetVal 
	END TRY
	BEGIN CATCH
		SET @RetVal = -99
		SET @AppointmentNo = 0
		RETURN @RetVal 
	END CATCH		
END
GO

CREATE FUNCTION ufn_FetchAllAppointments(@DoctorID CHAR(3), @DateofAppointment DATE)
RETURNS TABLE 
AS
	RETURN (SELECT d.DoctorName, d.Specialization, p.PatientID, p.PatientName, a.AppointmentNo 
			FROM Doctors d INNER JOIN Appointments a 
			ON d.DoctorID = a.DoctorID
			INNER JOIN Patients p 
			ON a.PatientID = p.PatientID 
			WHERE a.DoctorID = @DoctorID AND a.DateofAppointment = @DateofAppointment)
GO

CREATE FUNCTION ufn_CalculateDoctorFees 
(
	@DoctorID CHAR(3),
	@DateofAppointment DATE
)
RETURNS MONEY
AS
BEGIN
	DECLARE @ReturnValue MONEY
	SET @ReturnValue=(SELECT SUM(d.Fees * 0.6) 
						FROM Appointments a INNER JOIN Doctors d 
						ON a.DoctorID = d.DoctorID 
						WHERE a.DoctorID = @DoctorID AND a.DateofAppointment = @DateofAppointment)	
	RETURN @ReturnValue
END
GO

SELECT * FROM Doctors
SELECT * FROM Patients
SELECT * FROM Appointments
SELECT dbo.ufn_CalculateDoctorFees('D2', '2020-08-06')
SELECT * FROM ufn_FetchAllAppointments('D2', '2020-08-06')