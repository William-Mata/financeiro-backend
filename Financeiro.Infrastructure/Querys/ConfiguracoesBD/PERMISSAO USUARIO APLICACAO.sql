-- PERMISSÃO DE LEITURA APENAS (SELECT)
--EXEC sp_addrolemember 'db_datareader', 'financeiro_app';


-- PERMISSÃO DE ESCRITRA (INSERT, UPDATE E DELTE)
EXEC sp_addrolemember 'db_datawriter', 'financeiro_app';

-- PERMISSÃO TOTAL NO BANCO
--EXEC sp_addrolemember 'db_owner', 'MeuUsuario'; 


--PERMITIR AÇÃO ESPECIFICA EM UMA TABELA (SELECT, INSERT, UPDATE E DELTE)
--GRANT SELECT ON dbo.Usuario TO financeiro_app;   

--REMOVER AÇÃO ESPECIFICA EM UMA TABELA  (SELECT, INSERT, UPDATE E DELTE)
--REVOKE SELECT ON dbo.Usuario TO financeiro_app;   
