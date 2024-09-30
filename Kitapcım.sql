CREATE DATABASE Kitapc�m_DB;
GO
USE Kitapc�m_DB;
GO
CREATE TABLE Kitaplar (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Ad NVARCHAR(100),
    Yazar NVARCHAR(100),
    Yay�nevi NVARCHAR(100),
    YayinYili INT,
    T�r NVARCHAR(50),
    SayfaSayisi INT,
    Dil NVARCHAR(50),
    SatisFiyati DECIMAL(18, 2) 
);