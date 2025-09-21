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

-- �ndice para consultas por email (j� coberto pelo UNIQUE)

-- �ndice para consultas por nome (se necess�rio)
CREATE NONCLUSTERED INDEX IX_Usuario_Nome ON Usuario (Nome)