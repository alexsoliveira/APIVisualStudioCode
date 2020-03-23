/*Criar Banco de Dados*/
CREATE DATABASE DesafioThomasGregDB;

/*Usar o Banco de dodos criado*/
USE DesafioThomasGregDB;

/*Criar Tabela Logradouro*/
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE XTYPE = 'U' AND NAME = 'Logradouro')
BEGIN
	DROP TABLE Logradouro
END
CREATE TABLE Logradouro
(
	LogradouroId INT IDENTITY(1,1) PRIMARY KEY,
	Tipo VARCHAR(50) NOT NULL
)

/*Criar Tabela Cliente*/
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE XTYPE = 'U' AND NAME = 'Cliente')
BEGIN
	DROP TABLE Cliente
END
CREATE TABLE Cliente
(
	ClienteId INT NOT NULL,
	Nome VARCHAR(50) NULL,
	Email VARCHAR(50) NOT NULL,
	LogoTipo VARCHAR(50) NULL,
	LogradouroId INT FOREIGN KEY REFERENCES Logradouro(LogradouroId)
)

/*Criar Procedure Criar Logradouro*/
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE XTYPE = 'P' AND NAME = 'CriarLogradouro')
BEGIN
	DROP PROCEDURE CriarLogradouro
END
CREATE PROCEDURE CriarLogradouro
	@Tipo VARCHAR(50)
AS
BEGIN
	INSERT INTO Logradouro VALUES (@Tipo)
END

/*Criar Procedure Atualizar Logradouro*/
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE XTYPE = 'P' AND NAME = 'AtualizarLogradouro')
BEGIN
	DROP PROCEDURE AtualizarLogradouro
END
CREATE PROCEDURE AtualizarLogradouro
	@LogradouroId INT,
	@Tipo VARCHAR(50)
AS
BEGIN
	UPDATE Logradouro SET Tipo = @Tipo WHERE LogradouroId = @LogradouroId;
END

/*Criar Procedure Visualizar Logradouro*/
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE XTYPE = 'P' AND NAME = 'VisualizarLogradouro')
BEGIN
	DROP PROCEDURE VisualizarLogradouro
END
CREATE PROCEDURE VisualizarLogradouro
	@LogradouroId INT
AS
BEGIN
	SELECT LogradouroId, Tipo FROM Logradouro WHERE LogradouroId = @LogradouroId;
END

/*Criar Procedure Remover Logradouro*/
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE XTYPE = 'P' AND NAME = 'RemoverLogradouro')
BEGIN
	DROP PROCEDURE RemoverLogradouro
END
CREATE PROCEDURE RemoverLogradouro
	@LogradouroId INT
AS
BEGIN
	DELETE FROM Cliente WHERE LogradouroId = @LogradouroId;
	DELETE FROM Logradouro WHERE LogradouroId = @LogradouroId;
END

/*Criar Procedure Criar Cliente*/
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE XTYPE = 'P' AND NAME = 'CriarCliente')
BEGIN
	DROP PROCEDURE CriarCliente
END
CREATE PROCEDURE CriarCliente
	@ClienteId INT,
	@Nome VARCHAR(50),
	@Email VARCHAR(50),
	@LogoTipo VARCHAR(50),
	@LogradouroId INT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Cliente CLI WHERE CLI.ClienteId = @ClienteId AND CLI.LogradouroId = @LogradouroId)
	BEGIN
		PRINT 'REGISTRO JÁ EXISTE!'
		ROLLBACK
	END
	ELSE
		INSERT INTO Cliente VALUES (@ClienteId,@Nome,@Email,@LogoTipo,@LogradouroId);

END

/*Criar Procedure Criar Cliente*/
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE XTYPE = 'P' AND NAME = 'CriarCliente')
BEGIN
	DROP PROCEDURE CriarCliente
END
CREATE PROCEDURE AtualizarCliente
	@ClienteId INT,
	@Nome VARCHAR(50),
	@Email VARCHAR(50),
	@LogoTipo VARCHAR(50),
	@LogradouroId INT
AS
BEGIN
	IF EXISTS (SELECT 1 
	             FROM Cliente CLI 
				WHERE CLI.ClienteId = @ClienteId
				  AND CLI.Email = @Email
				  AND CLI.LogradouroId = @LogradouroId)
	BEGIN
		PRINT 'REGISTRO JÁ EXISTE!'
		ROLLBACK
	END
	ELSE
		UPDATE Cliente SET Nome = @Nome, Email = @Email, LogoTipo = @LogoTipo, LogradouroId = @LogradouroId WHERE ClienteId = @ClienteId AND LogradouroId = @LogradouroId;
END

/*Criar Procedure Visualizar Cliente*/
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE XTYPE = 'P' AND NAME = 'VisualizarCliente')
BEGIN
	DROP PROCEDURE VisualizarCliente
END
CREATE PROCEDURE VisualizarCliente
	@ClienteId INT
AS
BEGIN
	SELECT CLI.ClienteId, CLI.Nome, CLI.Email, CLI.LogoTipo, LGR.LogradouroId 
	  FROM Cliente CLI 
	 INNER JOIN Logradouro LGR
	    ON CLI.LogradouroId = LGR.LogradouroId
	 WHERE CLI.ClienteId = @ClienteId;
END

/*Criar Procedure Remover Cliente*/
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE XTYPE = 'P' AND NAME = 'RemoverCliente')
BEGIN
	DROP PROCEDURE RemoverCliente
END
CREATE PROCEDURE RemoverCliente
	@ClienteId INT,
	@LogradouroId INT
AS
BEGIN
	DELETE FROM Cliente WHERE ClienteId = @ClienteId AND LogradouroId = @LogradouroId;
END

/*Criar Procedure E-mail Cliente*/
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE XTYPE = 'P' AND NAME = 'EmailCliente')
BEGIN
	DROP PROCEDURE EmailCliente
END
CREATE PROCEDURE EmailCliente
	@Email VARCHAR(50)
AS
BEGIN
	SELECT CLI.ClienteId, CLI.Nome, CLI.Email, CLI.LogoTipo, LGR.LogradouroId 
	  FROM Cliente CLI 
	 INNER JOIN Logradouro LGR
	    ON CLI.LogradouroId = LGR.LogradouroId
	 WHERE CLI.Email = @Email;
END