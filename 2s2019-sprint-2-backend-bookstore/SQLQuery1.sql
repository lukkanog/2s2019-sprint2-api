CREATE DATABASE M_BookStore;

USE M_BookStore;

CREATE TABLE Generos
(
    IdGenero    INT PRIMARY KEY IDENTITY
    ,Descricao  VARCHAR(200) NOT NULL UNIQUE
);

CREATE TABLE Autores 
(
    IdAutor             INT PRIMARY KEY IDENTITY
    ,Nome               VARCHAR(200)
    ,Email              VARCHAR(255) UNIQUE
    ,Ativo              BIT DEFAULT(1) -- BIT/CHAR
    ,DataNascimento     DATE
);

CREATE TABLE Livros
(
    IdLivro             INT PRIMARY KEY IDENTITY
    ,Descricao          VARCHAR(255) NOT NULL UNIQUE
    ,IdAutor            INT FOREIGN KEY REFERENCES Autores (IdAutor)
    ,IdGenero           INT FOREIGN KEY REFERENCES Generos (IdGenero)
);


SELECT * FROM Autores

INSERT INTO Generos (Descricao) VALUES ('Drama');

INSERT INTO Autores (Nome,Email,Ativo,DataNascimento) VALUES ('Machado de Assis','assis.machado@email.com',1,'1930-09-20')

ALTER TABLE Livros
	ADD Titulo VARCHAR(255) NOT NULL

CREATE VIEW vwLivros
AS
SELECT L.IdLivro,L.Titulo,L.Descricao,L.IdAutor,A.Nome AS Autor,A.Email,A.Ativo,A.DataNascimento,L.IdGenero,G.Descricao AS Genero
	FROM Livros L
	INNER JOIN Autores A
	ON L.IdAutor = A.IdAutor
	INNER JOIN Generos G
	ON L.IdGenero = G.IdGenero

--DROP VIEW vwLivros

SELECT * FROM vwLivros

INSERT INTO Livros (Titulo,Descricao,IdAutor,IdGenero) VALUES ('A cidade de papel','cidade de origami',2)

SELECT * FROM vwLivros WHERE IdLivro = 1

CREATE PROCEDURE AlterarLivro @Titulo VARCHAR(255),  @Descricao VARCHAR(255), @Id INT
AS
UPDATE Livros
	SET Titulo = @Titulo
	WHERE IdLivro = @Id
UPDATE Livros
	SET Descricao = @Descricao
	WHERE IdLivro = @Id

--DROP PROC AlterarLivro
EXEC AlterarLivro 'Livro do Lula','Escreveu com o decimo dedo',2