
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MisFormas')
BEGIN
   
    CREATE DATABASE MisFormas;
END;
GO


USE MisFormas;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Cabecera')
BEGIN
    CREATE TABLE Cabecera (
        Id INT PRIMARY KEY IDENTITY(1,1),
        NombreTabla NVARCHAR(100) NOT NULL,
        FechaCreacion DATETIME DEFAULT GETDATE(),
        FechaModificacion DATETIME DEFAULT GETDATE(),
        Estado BIT NOT NULL, 
        CONSTRAINT CK_Cabecera_Estado CHECK (Estado IN (0, 1))  
    );
END;
GO


IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Detalle')
BEGIN
    CREATE TABLE Detalle (
        Id INT PRIMARY KEY IDENTITY(1,1),
        CabeceraId INT NOT NULL,
        CampoDetalle NVARCHAR(255) NOT NULL,
        CampoValor NVARCHAR(255) NULL,
        Estado BIT NOT NULL DEFAULT 1,  
        FechaCreacion DATETIME DEFAULT GETDATE(),
        FechaModificacion DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_Detalle_Cabecera FOREIGN KEY (CabeceraId) 
            REFERENCES Cabecera(Id) ON DELETE CASCADE,
        CONSTRAINT CK_Detalle_Estado CHECK (Estado IN (0, 1))  
    );
END;
GO

