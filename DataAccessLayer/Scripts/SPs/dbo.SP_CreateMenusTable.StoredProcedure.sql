USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateMenusTable]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CreateMenusTable]
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Create [Menus] table
    CREATE TABLE [dbo].[Menus](
        [Id] [int] NOT NULL,
        [BusinessId] [bigint] NOT NULL,
        [MenuName] [nvarchar](255) NOT NULL,
        [MenuUrl] [nvarchar](255) NOT NULL,
        [OrderBy] [int] NOT NULL,
        [isActive] [bit] NOT NULL,
     CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];

    -- Add default constraints
    ALTER TABLE [dbo].[Menus] ADD CONSTRAINT [DF_Menus_MenuName] DEFAULT ('') FOR [MenuName];
    ALTER TABLE [dbo].[Menus] ADD CONSTRAINT [DF_Menus_MenuUrl] DEFAULT ('') FOR [MenuUrl];
    ALTER TABLE [dbo].[Menus] ADD CONSTRAINT [DF_Menus_OrderBy] DEFAULT ((0)) FOR [OrderBy];
    ALTER TABLE [dbo].[Menus] ADD CONSTRAINT [DF_Menus_isActive] DEFAULT ((0)) FOR [isActive];
    
    PRINT 'Menus table created successfully.';
END;


GO
