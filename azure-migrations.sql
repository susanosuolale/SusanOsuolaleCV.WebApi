IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013174257_InitialCreate'
)
BEGIN
    CREATE TABLE [Educations] (
        [Id] uniqueidentifier NOT NULL,
        [Degree] nvarchar(max) NOT NULL,
        [Institution] nvarchar(max) NOT NULL,
        [Years] nvarchar(max) NOT NULL,
        [Details] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Educations] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013174257_InitialCreate'
)
BEGIN
    CREATE TABLE [GalleryImages] (
        [Id] uniqueidentifier NOT NULL,
        [FileName] nvarchar(max) NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_GalleryImages] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013174257_InitialCreate'
)
BEGIN
    CREATE TABLE [Profiles] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Bio] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Profiles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013174257_InitialCreate'
)
BEGIN
    CREATE TABLE [Projects] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Summary] nvarchar(max) NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [TagsJson] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Projects] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013174257_InitialCreate'
)
BEGIN
    CREATE TABLE [WorkItems] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Company] nvarchar(max) NOT NULL,
        [Period] nvarchar(max) NOT NULL,
        [ResponsibilitiesJson] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_WorkItems] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013174257_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Degree', N'Details', N'Institution', N'Years') AND [object_id] = OBJECT_ID(N'[Educations]'))
        SET IDENTITY_INSERT [Educations] ON;
    EXEC(N'INSERT INTO [Educations] ([Id], [Degree], [Details], [Institution], [Years])
    VALUES (''11111111-1111-1111-1111-111111111111'', N''B.Eng. Electronic & Electrical Engineering'', N'''', N''Obafemi Awolowo University'', N''2022'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Degree', N'Details', N'Institution', N'Years') AND [object_id] = OBJECT_ID(N'[Educations]'))
        SET IDENTITY_INSERT [Educations] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013174257_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'FileName', N'Title') AND [object_id] = OBJECT_ID(N'[GalleryImages]'))
        SET IDENTITY_INSERT [GalleryImages] ON;
    EXEC(N'INSERT INTO [GalleryImages] ([Id], [Description], [FileName], [Title])
    VALUES (''44444444-4444-4444-4444-444444444441'', N''Professional headshot.'', N''photo1.jpg'', N''Profile Shot''),
    (''44444444-4444-4444-4444-444444444442'', N''Screenshot of SchoolMaster project.'', N''project1.png'', N''SchoolMaster Screenshot'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'FileName', N'Title') AND [object_id] = OBJECT_ID(N'[GalleryImages]'))
        SET IDENTITY_INSERT [GalleryImages] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013174257_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Bio', N'Name', N'Title') AND [object_id] = OBJECT_ID(N'[Profiles]'))
        SET IDENTITY_INSERT [Profiles] ON;
    EXEC(N'INSERT INTO [Profiles] ([Id], [Bio], [Name], [Title])
    VALUES (''55555555-5555-5555-5555-555555555555'', N''Backend Software Engineer with experience designing secure, scalable RESTful APIs using C#, ASP.NET Core, EF Core, SQL Server, and PostgreSQL.'', N''OJONE SUSAN OSUOLALE'', N''Backend Software Engineer'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Bio', N'Name', N'Title') AND [object_id] = OBJECT_ID(N'[Profiles]'))
        SET IDENTITY_INSERT [Profiles] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013174257_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Status', N'Summary', N'TagsJson', N'Title') AND [object_id] = OBJECT_ID(N'[Projects]'))
        SET IDENTITY_INSERT [Projects] ON;
    EXEC(N'INSERT INTO [Projects] ([Id], [Status], [Summary], [TagsJson], [Title])
    VALUES (''33333333-3333-3333-3333-333333333331'', N''Completed'', N''Multi-tenant school management backend.'', N''["C#","ASP.NET Core","EF Core","PostgreSQL","Redis","Docker"]'', N''SchoolMaster''),
    (''33333333-3333-3333-3333-333333333332'', N''In Progress'', N''Invoice/payment microservice platform.'', N''[" .NET","CQRS","RabbitMQ","PostgreSQL","Elasticsearch"]'', N''FreelanceFlow''),
    (''33333333-3333-3333-3333-333333333333'', N''Completed'', N''ASP.NET Core Web API project.'', N''["C#","ASP.NET Core","Redis","Docker"]'', N''TaskPulse''),
    (''33333333-3333-3333-3333-333333333334'', N''Completed'', N''AI-powered subscription platform.'', N''["FastAPI","OpenAI","PostgreSQL","Docker","Paystack"]'', N''AI Subscription Platform'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Status', N'Summary', N'TagsJson', N'Title') AND [object_id] = OBJECT_ID(N'[Projects]'))
        SET IDENTITY_INSERT [Projects] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013174257_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Company', N'Period', N'ResponsibilitiesJson', N'Title') AND [object_id] = OBJECT_ID(N'[WorkItems]'))
        SET IDENTITY_INSERT [WorkItems] ON;
    EXEC(N'INSERT INTO [WorkItems] ([Id], [Company], [Period], [ResponsibilitiesJson], [Title])
    VALUES (''22222222-2222-2222-2222-222222222222'', N''Self-Directed / Personal Projects'', N''Feb 2026 - Present'', N''["C# and ASP.NET Core for building REST APIs","SQL Server, PostgreSQL, EF Core","Redis, Docker, Clean Architecture, Microservices","Background processing and messaging"]'', N''Backend Software Developer''),
    (''22222222-2222-2222-2222-222222222223'', N''Various'', N''2023 - 2024'', N''["Python, Pandas, NumPy, Scikit-learn","Exploratory data analysis and visualization","Statistical analysis and machine learning"]'', N''Data Analyst / Data Scientist''),
    (''22222222-2222-2222-2222-222222222224'', N''Freelance'', N''2024 - 2025'', N''["Researched and identified potential leads","Maintained prospect lists for outreach"]'', N''Lead Generator''),
    (''22222222-2222-2222-2222-222222222225'', N''MEE Consults'', N''2023'', N''["Electrical distribution and lighting systems","Emergency power and low-voltage systems","AutoCAD electrical layouts and coordination"]'', N''Building Engineer'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Company', N'Period', N'ResponsibilitiesJson', N'Title') AND [object_id] = OBJECT_ID(N'[WorkItems]'))
        SET IDENTITY_INSERT [WorkItems] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013174257_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261013174257_InitialCreate', N'8.0.10');
END;
GO

COMMIT;
GO

