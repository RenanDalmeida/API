IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Pedidos] (
    [IdPedido] uniqueidentifier NOT NULL,
    [Status] nvarchar(max) NULL,
    [DiaDaCompra] datetime2 NOT NULL,
    CONSTRAINT [PK_Pedidos] PRIMARY KEY ([IdPedido])
);

GO

CREATE TABLE [Produtos] (
    [IdProduto] uniqueidentifier NOT NULL,
    [Nome] nvarchar(max) NULL,
    [Preco] real NOT NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY ([IdProduto])
);

GO

CREATE TABLE [PedidosProdutos] (
    [IdPedidoProduto] uniqueidentifier NOT NULL,
    [IdPedido] uniqueidentifier NOT NULL,
    [IdProduto] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_PedidosProdutos] PRIMARY KEY ([IdPedidoProduto]),
    CONSTRAINT [FK_PedidosProdutos_Pedidos_IdPedido] FOREIGN KEY ([IdPedido]) REFERENCES [Pedidos] ([IdPedido]) ON DELETE CASCADE,
    CONSTRAINT [FK_PedidosProdutos_Produtos_IdProduto] FOREIGN KEY ([IdProduto]) REFERENCES [Produtos] ([IdProduto]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_PedidosProdutos_IdPedido] ON [PedidosProdutos] ([IdPedido]);

GO

CREATE INDEX [IX_PedidosProdutos_IdProduto] ON [PedidosProdutos] ([IdProduto]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200909005631_InitialCreate', N'3.1.8');

GO

ALTER TABLE [PedidosProdutos] ADD [Quantidade] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200909011407_AlterTablePedidoProduto', N'3.1.8');

GO

