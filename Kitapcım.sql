CREATE DATABASE Kitapcým_DB;
GO
USE Kitapcým_DB;
GO
CREATE TABLE Kitaplar (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Ad NVARCHAR(100),
    Yazar NVARCHAR(100),
    Yayýnevi NVARCHAR(100),
    YayinYili INT,
    Tür NVARCHAR(50),
    SayfaSayisi INT,
    Dil NVARCHAR(50),
    SatisFiyati DECIMAL(18, 2) 
);