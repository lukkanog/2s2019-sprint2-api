CREATE DATABASE M_AutoPecas;

USE M_AutoPecas;

CREATE TABLE Usuarios(
	IdUsuario INT PRIMARY KEY IDENTITY
	,Email VARCHAR(255) NOT NULL UNIQUE
	,Senha VARCHAR(80) NOT NULL
);

CREATE TABLE Fornecedores(
	IdFornecedor INT PRIMARY KEY IDENTITY
	,RazaoSozial VARCHAR(255) NOT NULL
	,NomeFantasia VARCHAR(255) NOT NULL
	,IdUsuario INT FOREIGN KEY REFERENCES Usuarios(IdUsuario) UNIQUE NOT NULL
);

ALTER TABLE Fornecedores
	DROP COLUMN IdUsuario

--DELETE FROM Fornecedores
--DELETE FROM PEcas
--DROP TABLE Fornecedores

CREATE TABLE Pecas(
	IdPeca INT PRIMARY KEY IDENTITY
	,Codigo VARCHAR(255) NOT NULL UNIQUE
	,Descricao VARCHAR(255) NOT NULL
	,Peso DECIMAL
	,PesoCusto MONEY NOT NULL
	,PesoVenda MONEY NOT NULL
	,IdFornecedor INT FOREIGN KEY REFERENCES Fornecedores(IdFornecedor)
);

ALTER TABLE Fornecedores
	DROP COLUMN RazaoSozial

ALTER TABLE Fornecedores
	ADD RazaoSocial VARCHAR(255) NOT NULL


INSERT INTO Usuarios VALUES ('constru@email.com','123123')


SELECT * FROM Fornecedores

SELECT * FROM USUARIOS
INSERT INTO Fornecedores (RazaoSocial,NomeFantasia,IdUsuario) VALUES ('Noah e Helena Transportes Ltda','2 Irmãos Transportadora',5)

SELECT * FROM Fornecedores
SELECT * FROM Pecas

INSERT INTO Pecas VALUES ('2d3ad2','Tijolo Baiano',1500,30.69,79.99,2)

INSERT INTO Fornecedores VALUES ('Fornecedor teste',5,'Fornecedor de forno')


SELECT U.*,F.*
	FROM Usuarios U
	INNER JOIN Fornecedores F
	ON U.IdUsuario = F.IdUsuario