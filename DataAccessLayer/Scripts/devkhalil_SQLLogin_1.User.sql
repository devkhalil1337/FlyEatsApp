USE [Flyeats]
GO
/****** Object:  User [devkhalil_SQLLogin_1]    Script Date: 11/25/2023 12:48:19 AM ******/
CREATE USER [devkhalil_SQLLogin_1] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [devkhalil_SQLLogin_1]
GO
