-- PERMISS�O DE LEITURA APENAS (SELECT)
--EXEC sp_addrolemember 'db_datareader', 'financeiro_app';


-- PERMISS�O DE ESCRITRA (INSERT, UPDATE E DELTE)
EXEC sp_addrolemember 'db_datawriter', 'financeiro_app';

-- PERMISS�O TOTAL NO BANCO
--EXEC sp_addrolemember 'db_owner', 'MeuUsuario'; 


--PERMITIR A��O ESPECIFICA EM UMA TABELA (SELECT, INSERT, UPDATE E DELTE)
--GRANT SELECT ON dbo.Usuario TO financeiro_app;   

--REMOVER A��O ESPECIFICA EM UMA TABELA  (SELECT, INSERT, UPDATE E DELTE)
--REVOKE SELECT ON dbo.Usuario TO financeiro_app;   
