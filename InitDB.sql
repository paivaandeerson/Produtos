

 IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'ProdutoDB')
  BEGIN
    CREATE DATABASE ProdutoDB
END;

IF OBJECT_ID('ProdutoModels','U') IS NOT NULL
DROP TABLE [ProdutoDB].[ProdutoModels]

CREATE TABLE [ProdutoModels] (
    [Id] bigint NOT NULL,
    [Nome] nvarchar(max) NULL,
    [Valor] decimal(18,2) NOT NULL,
    [ImagemPath] nvarchar(max) NULL,
    CONSTRAINT [PK_ProdutoModels] PRIMARY KEY ([Id])
);
GO

--SELECT * FROM [ProdutoModels]