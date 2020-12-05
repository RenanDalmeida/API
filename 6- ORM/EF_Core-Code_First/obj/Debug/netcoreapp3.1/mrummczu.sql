ALTER TABLE [PedidosProdutos] ADD [Quantidade] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200909011407_AlterTablePedidoProduto', N'3.1.8');

GO

