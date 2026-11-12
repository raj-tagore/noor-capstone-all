-- GIVEN: tables and sample data from the problem statement.
-- The exam catalog name is CureHospitalDB. This practice copy uses CureHospitalFA1DB
-- so the database already used by capstone-3 is left in place.
-- SOLUTION objects (stored procedures and functions) are marked below.

USE [master]
GO
IF DB_ID(N'CureHospitalFA1DB') IS NOT NULL
BEGIN
    ALTER DATABASE CureHospitalFA1DB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CureHospitalFA1DB;
END
GO
CREATE DATABASE CureHospitalFA1DB
GO
USE CureHospitalFA1DB
GO

CREATE TABLE Specialization
(
    SpecializationCode CHAR(3) CONSTRAINT pk_SpecializationCode PRIMARY KEY,
    SpecializationName VARCHAR(50) NOT NULL UNIQUE
)
GO

INSERT INTO Specialization VALUES ('GYN', 'Gynecologist')
INSERT INTO Specialization VALUES ('CAR', 'Cardiologist')
INSERT INTO Specialization VALUES ('ANE', 'Anesthesiologist')
GO

CREATE TABLE Doctor
(
    DoctorID INT IDENTITY(1001, 1) CONSTRAINT pk_DoctorID PRIMARY KEY,
    DoctorName VARCHAR(50) NOT NULL UNIQUE
)
GO

INSERT INTO Doctor VALUES ('Albert')
INSERT INTO Doctor VALUES ('Olivia')
INSERT INTO Doctor VALUES ('Susan')
GO

CREATE TABLE DoctorSpecialization
(
    DoctorID INT CONSTRAINT fk_DoctorID REFERENCES Doctor(DoctorID),
    SpecializationCode CHAR(3) CONSTRAINT fk_SpecializationCode REFERENCES Specialization(SpecializationCode),
    SpecializationDate DATE NOT NULL,
    CONSTRAINT pk_DoctorIDSpecializationCode PRIMARY KEY (DoctorID, SpecializationCode)
)
GO

INSERT INTO DoctorSpecialization VALUES (1001, 'ANE', '2010-01-01')
INSERT INTO DoctorSpecialization VALUES (1002, 'CAR', '2016-10-30')
INSERT INTO DoctorSpecialization VALUES (1003, 'CAR', '2013-05-19')
GO

CREATE TABLE Surgery
(
    SurgeryID INT IDENTITY(5000, 1) CONSTRAINT pk_SurgeryID PRIMARY KEY,
    DoctorID INT CONSTRAINT fk_DoctorID_Surgery REFERENCES Doctor(DoctorID),
    SurgeryDate DATE NOT NULL,
    StartTime DECIMAL(4, 2) NOT NULL,
    EndTime DECIMAL(4, 2) NOT NULL,
    SurgeryCategory CHAR(3) CONSTRAINT fk_SpecializationCode_Surgery REFERENCES Specialization(SpecializationCode)
)
GO

INSERT INTO Surgery VALUES (1001, DATEADD(DAY, 2, CAST(GETDATE() AS DATE)), 9.00, 14.00, 'ANE')
INSERT INTO Surgery VALUES (1002, DATEADD(DAY, 5, CAST(GETDATE() AS DATE)), 10.00, 16.00, 'CAR')
GO

-- ===== SOLUTION START: usp_AddSurgeryDetails =====
-- Return codes follow the published DAL test table.
-- The slide text says "-4 if start time is less than end time".
-- The test row start=12 end=10 returns -4, so -4 means start is NOT less than end.
-- On every failure the OUT parameter is 0. On success it is the new SurgeryID.
CREATE OR ALTER PROCEDURE usp_AddSurgeryDetails
    @did INT,
    @sc CHAR(3),
    @sd DATE,
    @st DECIMAL(4, 2),
    @et DECIMAL(4, 2),
    @si INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @si = 0;

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Doctor WHERE DoctorID = @did)
            RETURN -1;

        IF NOT EXISTS (SELECT 1 FROM Specialization WHERE SpecializationCode = @sc)
            RETURN -2;

        IF NOT EXISTS (
            SELECT 1 FROM DoctorSpecialization
            WHERE DoctorID = @did AND SpecializationCode = @sc)
            RETURN -3;

        IF @st >= @et
            RETURN -4;

        IF EXISTS (
            SELECT 1 FROM Surgery
            WHERE DoctorID = @did
              AND SurgeryDate = @sd
              AND StartTime < @et
              AND EndTime > @st)
            RETURN -5;

        IF @sd < CAST(GETDATE() AS DATE)
            RETURN -6;

        INSERT INTO Surgery (DoctorID, SurgeryDate, StartTime, EndTime, SurgeryCategory)
        VALUES (@did, @sd, @st, @et, @sc);

        SET @si = CAST(SCOPE_IDENTITY() AS INT);
        RETURN 1;
    END TRY
    BEGIN CATCH
        SET @si = 0;
        RETURN -99;
    END CATCH
END
GO
-- ===== SOLUTION END: usp_AddSurgeryDetails =====

-- ===== SOLUTION START: usp_AddDoctorDetails =====
CREATE OR ALTER PROCEDURE usp_AddDoctorDetails
    @DoctorName VARCHAR(50),
    @SpecializationCode CHAR(3),
    @SpecializationName VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Specialization WHERE SpecializationCode = @SpecializationCode)
        BEGIN
            INSERT INTO Specialization (SpecializationCode, SpecializationName)
            VALUES (@SpecializationCode, @SpecializationName);
        END

        INSERT INTO Doctor (DoctorName) VALUES (@DoctorName);

        INSERT INTO DoctorSpecialization (DoctorID, SpecializationCode, SpecializationDate)
        VALUES (CAST(SCOPE_IDENTITY() AS INT), @SpecializationCode, CAST(GETDATE() AS DATE));

        RETURN 1;
    END TRY
    BEGIN CATCH
        RETURN -99;
    END CATCH
END
GO
-- ===== SOLUTION END: usp_AddDoctorDetails =====

-- ===== SOLUTION START: ufn_FetchSurgeryDetails =====
-- Columns shown on the slide: SurgeryID, DoctorName, SpecializationName, SurgeryDate, StartTime.
-- EndTime is included so each row still has the surgery window.
CREATE OR ALTER FUNCTION ufn_FetchSurgeryDetails (@SurgeryDate DATE)
RETURNS TABLE
AS
RETURN
(
    SELECT s.SurgeryID,
           d.DoctorName,
           sp.SpecializationName,
           s.SurgeryDate,
           s.StartTime,
           s.EndTime
    FROM Surgery s
    INNER JOIN Doctor d ON d.DoctorID = s.DoctorID
    INNER JOIN Specialization sp ON sp.SpecializationCode = s.SurgeryCategory
    WHERE s.SurgeryDate = @SurgeryDate
);
GO
-- ===== SOLUTION END: ufn_FetchSurgeryDetails =====

-- ===== SOLUTION START: ufn_FetchSurgeryCount =====
CREATE OR ALTER FUNCTION ufn_FetchSurgeryCount
(
    @SpecializationName VARCHAR(50),
    @SurgeryDate DATE
)
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;

    SELECT @Count = COUNT(*)
    FROM Surgery s
    INNER JOIN Specialization sp ON sp.SpecializationCode = s.SurgeryCategory
    WHERE sp.SpecializationName = @SpecializationName
      AND s.SurgeryDate = @SurgeryDate;

    RETURN ISNULL(@Count, 0);
END
GO
-- ===== SOLUTION END: ufn_FetchSurgeryCount =====
