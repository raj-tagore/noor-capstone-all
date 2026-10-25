USE [master]
GO
IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = N'MusicLibraryDB' OR name = N'MusicLibraryDB')))
DROP DATABASE MusicLibraryDB

CREATE DATABASE MusicLibraryDB
GO

USE MusicLibraryDB
GO

IF OBJECT_ID('Songs') IS NOT NULL
DROP TABLE Songs
GO

IF OBJECT_ID('Albums') IS NOT NULL
DROP TABLE Albums
GO

CREATE TABLE Albums
(
	[AlbumName] VARCHAR(50) CONSTRAINT pk_AlbumName PRIMARY KEY,
	[TotalSongs] INT NOT NULL,
	[CurrentSongs] INT NOT NULL,
	CONSTRAINT chk_CurrentSongs CHECK (CurrentSongs >= 0 AND CurrentSongs <= TotalSongs)
)
GO

INSERT INTO Albums(AlbumName, TotalSongs, CurrentSongs) VALUES('Abbey Road', 5, 2)
INSERT INTO Albums(AlbumName, TotalSongs, CurrentSongs) VALUES('Help', 3, 1)
INSERT INTO Albums(AlbumName, TotalSongs, CurrentSongs) VALUES('Revolver', 2, 2)
GO

CREATE TABLE Songs
(
	[SongId] INT CONSTRAINT pk_SongId PRIMARY KEY IDENTITY,
	[SongName] VARCHAR(50) CONSTRAINT uq_SongName UNIQUE NOT NULL,
	[AlbumName] VARCHAR(50) CONSTRAINT fk_AlbumName REFERENCES Albums(AlbumName) NOT NULL
)
GO

INSERT INTO Songs(SongName, AlbumName) VALUES('Come Together', 'Abbey Road')
INSERT INTO Songs(SongName, AlbumName) VALUES('Something', 'Abbey Road')
INSERT INTO Songs(SongName, AlbumName) VALUES('Yesterday', 'Help')
INSERT INTO Songs(SongName, AlbumName) VALUES('Eleanor Rigby', 'Revolver')
INSERT INTO Songs(SongName, AlbumName) VALUES('Taxman', 'Revolver')
GO

SELECT * FROM Albums
SELECT * FROM Songs
GO
