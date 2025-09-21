CREATE TABLE Usuario
(
	UsuarioId INT IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
	Nome NVARCHAR(255) NOT NULL,
	Email NVARCHAR(255) UNIQUE NOT NULL,
	Senha VARBINARY(256) NOT NULL,
	Status BIT NOT NULL DEFAULT 1,
	DataCadastro DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
	DataUltimaAtualizacao DATETIME2 NULL
)

-- Índice para consultas por email (já coberto pelo UNIQUE)

-- Índice para consultas por nome (se necessário)
CREATE NONCLUSTERED INDEX IX_Usuario_Nome ON Usuario (Nome)