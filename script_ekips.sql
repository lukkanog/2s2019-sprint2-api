------------------------------------------------     D Q L    ----------------------------------------------------------------------

CREATE DATABASE M_Ekips;

USE M_Ekips;

CREATE TABLE Departamentos(
	IdDepartamento INT PRIMARY KEY IDENTITY
	,Nome VARCHAR(255) NOT NULL UNIQUE
);


CREATE TABLE Cargos(
	IdCargo INT PRIMARY KEY IDENTITY
	,Nome  VARCHAR(255) NOT NULL UNIQUE
	,EstaAtivo BIT DEFAULT(1)
);

ALTER TABLE Cargos
	ADD IdDepartamento INT FOREIGN KEY REFERENCES Departamentos (IdDepartamento)


CREATE TABLE Usuarios(
	IdUsuario INT PRIMARY KEY IDENTITY
	,Email VARCHAR(255) UNIQUE NOT NULL
	,Senha VARCHAR(255) NOT NULL
);

CREATE TABLE Funcionarios(
	IdFuncionario INT PRIMARY KEY IDENTITY
	,Nome VARCHAR(255) NOT NULL
	,Cpf VARCHAR(255) NOT NULL
	,DataNascimento DATE NOT NULL
	,Salario MONEY NOT NULL
	,IdCargo INT FOREIGN KEY REFERENCES Cargos (IdCargo)
	,IdUsuario INT FOREIGN KEY REFERENCES Usuarios (IdUsuario)
);

ALTER TABLE Usuarios
	ADD Permissao VARCHAR(255) DEFAULT('COMUM')

ALTER TABLE Funcionarios
	DROP COLUMN Cpf

ALTER TABLE Funcionarios
	ADD Cpf VARCHAR(255) UNIQUE NOT NULL

	
DROP TABLE Funcionarios

------------------------------------------------     D M L    - -----------------------------------------------------------------------

INSERT INTO Departamentos VALUES ('Administrativo'),('Produção'),('Jurídico'),('Tecnologia'),('Recursos Humanos')


INSERT INTO Cargos  (Nome,IdDepartamento)
	VALUES ('Advogado(a) Trabalhista',5),('Product Owner',6),('Gerente',3),('Diretor de RH',1)


INSERT INTO Usuarios (Email,Senha,Permissao)
	VALUES ('admin@email.com','123456','ADMINISTRADOR'),('bob@email.com','123456','COMUM'),('fernando@email.com','123456','COMUM')


SELECT * FROM DEPARTAMENTOS ORDER BY IdDepartamento ASC
SELECT * FROM CARGOS
SELECT * FROM Usuarios

INSERT INTO Funcionarios (Nome,Cpf,DataNascimento,Salario,IdCargo,IdUsuario)
	VALUES ('Fernando Pereira','281.291.140-08','1999-12-12',2900,3,3)

SELECT * FROM Funcionarios


------------------------------------------------     D Q L    -------------------------------------------------------------------------

CREATE VIEW vwFuncionarios
AS
SELECT F.IdFuncionario,F.Nome,F.DataNascimento,F.Salario,F.Cpf,C.IdCargo,C.Nome AS Cargo,C.EstaAtivo,D.Nome AS Departamento,U.*
FROM Funcionarios F
INNER JOIN Cargos C
ON F.IdCargo = C.IdCargo
INNER JOIN Departamentos D
ON C.IdDepartamento = D.IdDepartamento
INNER JOIN Usuarios U
ON F.IdUsuario = U.IdUsuario

--DROP VIEW vwFuncionarios

SELECT * FROM vwFuncionarios ORDER BY IdFuncionario DESC