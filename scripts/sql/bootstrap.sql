USE master;
GO

DECLARE @db_id INT;
SET @db_id = DB_ID(N'BunkersAndBluffsDB');  
IF @db_id IS NULL   
  BEGIN;  
    CREATE DATABASE BunkersAndBluffsDB; 
  END;  
GO

DECLARE @suser_id INT;
SET @suser_id = SUSER_ID(N'accounts_app');  
IF @suser_id IS NULL   
  BEGIN;  
    CREATE LOGIN accounts_app WITH PASSWORD = '$(accountsPassword)'; 
  END;  
SET @suser_id = SUSER_ID(N'game_app');  
IF @suser_id IS NULL   
  BEGIN;  
    CREATE LOGIN game_app WITH PASSWORD = '$(gamePassword)'; 
  END;  
GO

USE BunkersAndBluffsDB;
GO

DECLARE @schema_id INT;
SET @schema_id = SCHEMA_ID(N'accounts');
IF @schema_id IS NULL
  BEGIN;  
    EXEC(N'CREATE SCHEMA accounts')
  END;  
SET @schema_id = SCHEMA_ID(N'game');
IF @schema_id IS NULL
  BEGIN;  
    EXEC(N'CREATE SCHEMA game')
  END;
SET @schema_id = SCHEMA_ID(N'catalog');
IF @schema_id IS NULL
  BEGIN;  
    EXEC(N'CREATE SCHEMA catalog')
  END;
GO

DECLARE @user_id INT;
SET @user_id = USER_ID(N'accounts_app');
IF @user_id IS NULL
  BEGIN;  
    CREATE USER accounts_app FOR LOGIN accounts_app WITH DEFAULT_SCHEMA = accounts;
  END;  
SET @user_id = USER_ID(N'game_app');
IF @user_id IS NULL
  BEGIN;  
    CREATE USER game_app FOR LOGIN game_app WITH DEFAULT_SCHEMA = game;
  END;  
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA :: accounts TO accounts_app;
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA :: game TO game_app;
GRANT SELECT ON SCHEMA :: catalog TO game_app;
GO