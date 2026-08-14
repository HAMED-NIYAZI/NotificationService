USE [master]
GO
/****** Object:  Database [NotificationServiceDB]    Script Date: 8/14/2026 10:00:05 PM ******/
CREATE DATABASE [NotificationServiceDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NotificationServiceDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQL2022\MSSQL\DATA\NotificationServiceDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NotificationServiceDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQL2022\MSSQL\DATA\NotificationServiceDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [NotificationServiceDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NotificationServiceDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NotificationServiceDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NotificationServiceDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NotificationServiceDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NotificationServiceDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NotificationServiceDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET RECOVERY FULL 
GO
ALTER DATABASE [NotificationServiceDB] SET  MULTI_USER 
GO
ALTER DATABASE [NotificationServiceDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NotificationServiceDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NotificationServiceDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NotificationServiceDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NotificationServiceDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NotificationServiceDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'NotificationServiceDB', N'ON'
GO
ALTER DATABASE [NotificationServiceDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [NotificationServiceDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [NotificationServiceDB]
GO
/****** Object:  Table [dbo].[ClientConnections]    Script Date: 8/14/2026 10:00:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientConnections](
	[Id] [uniqueidentifier] NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[RecipientId] [nvarchar](250) NOT NULL,
	[ConnectionId] [nvarchar](250) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[ConnectedAt] [datetime2](7) NOT NULL,
	[DisconnectedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_ClientConnections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationApplications]    Script Date: 8/14/2026 10:00:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationApplications](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[ApiKey] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](3) NOT NULL,
	[UpdatedAt] [datetime2](3) NULL,
 CONSTRAINT [PK_NotificationApplications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationDeliveries]    Script Date: 8/14/2026 10:00:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationDeliveries](
	[Id] [uniqueidentifier] NOT NULL,
	[NotificationId] [uniqueidentifier] NOT NULL,
	[ClientConnectionId] [uniqueidentifier] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[RetryCount] [int] NOT NULL,
	[LeaseId] [uniqueidentifier] NULL,
	[LeaseUntil] [datetime2](3) NULL,
	[SentAt] [datetime2](3) NULL,
	[DeliveredAt] [datetime2](3) NULL,
	[ReadAt] [datetime2](3) NULL,
	[FailedAt] [datetime2](3) NULL,
	[LastError] [nvarchar](4000) NULL,
	[CreatedAt] [datetime2](3) NOT NULL,
	[UpdatedAt] [datetime2](3) NOT NULL,
	[NextRetryAt] [datetime2](3) NULL,
 CONSTRAINT [PK_NotificationDeliveries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 8/14/2026 10:00:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[Id] [uniqueidentifier] NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[RecipientId] [nvarchar](250) NOT NULL,
	[Type] [nvarchar](250) NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Message] [varchar](4000) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ExpireAt] [datetime2](7) NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[NotificationApplications] ([Id], [Name], [Description], [ApiKey], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'a2c57519-3d10-438a-b7d2-3258fb1adebe', N'ComplaintApp', NULL, N'a2c57519-3d10-438a-b7d2-3258fb1adebe', 1, CAST(N'2026-08-14T18:03:46.5560000' AS DateTime2), NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ClientConnections_Application_Recipient_Active]    Script Date: 8/14/2026 10:00:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_ClientConnections_Application_Recipient_Active] ON [dbo].[ClientConnections]
(
	[ApplicationId] ASC,
	[RecipientId] ASC,
	[IsActive] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UX_ClientConnections_ConnectionId]    Script Date: 8/14/2026 10:00:05 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UX_ClientConnections_ConnectionId] ON [dbo].[ClientConnections]
(
	[ConnectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UX_NotificationApplications_ApiKeyHash]    Script Date: 8/14/2026 10:00:05 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UX_NotificationApplications_ApiKeyHash] ON [dbo].[NotificationApplications]
(
	[ApiKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_NotificationDeliveries_ClientConnectionId]    Script Date: 8/14/2026 10:00:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_NotificationDeliveries_ClientConnectionId] ON [dbo].[NotificationDeliveries]
(
	[ClientConnectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_NotificationDeliveries_NotificationId]    Script Date: 8/14/2026 10:00:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_NotificationDeliveries_NotificationId] ON [dbo].[NotificationDeliveries]
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_NotificationDeliveries_Status]    Script Date: 8/14/2026 10:00:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_NotificationDeliveries_Status] ON [dbo].[NotificationDeliveries]
(
	[Status] ASC,
	[CreatedAt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Notifications_ApplicationId_RecipientId]    Script Date: 8/14/2026 10:00:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_Notifications_ApplicationId_RecipientId] ON [dbo].[Notifications]
(
	[ApplicationId] ASC,
	[RecipientId] ASC,
	[CreatedAt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ClientConnections] ADD  CONSTRAINT [DF_ClientConnections_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ClientConnections] ADD  CONSTRAINT [DF_ClientConnections_ConnectedAt]  DEFAULT (sysutcdatetime()) FOR [ConnectedAt]
GO
ALTER TABLE [dbo].[NotificationApplications] ADD  CONSTRAINT [DF_NotificationApplications_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[NotificationApplications] ADD  CONSTRAINT [DF_NotificationApplications_CreatedAt]  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[NotificationDeliveries] ADD  CONSTRAINT [DF_NotificationDeliveries_RetryCount]  DEFAULT ((0)) FOR [RetryCount]
GO
ALTER TABLE [dbo].[NotificationDeliveries] ADD  CONSTRAINT [DF_NotificationDeliveries_CreatedAt]  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[NotificationDeliveries] ADD  CONSTRAINT [DF_NotificationDeliveries_UpdatedAt]  DEFAULT (sysutcdatetime()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Notifications] ADD  CONSTRAINT [DF_Notifications_CreatedAt]  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[ClientConnections]  WITH CHECK ADD  CONSTRAINT [FK_ClientConnections_Applications] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[NotificationApplications] ([Id])
GO
ALTER TABLE [dbo].[ClientConnections] CHECK CONSTRAINT [FK_ClientConnections_Applications]
GO
ALTER TABLE [dbo].[NotificationDeliveries]  WITH CHECK ADD  CONSTRAINT [FK_NotificationDeliveries_ClientConnections] FOREIGN KEY([ClientConnectionId])
REFERENCES [dbo].[ClientConnections] ([Id])
GO
ALTER TABLE [dbo].[NotificationDeliveries] CHECK CONSTRAINT [FK_NotificationDeliveries_ClientConnections]
GO
ALTER TABLE [dbo].[NotificationDeliveries]  WITH CHECK ADD  CONSTRAINT [FK_NotificationDeliveries_Notifications] FOREIGN KEY([NotificationId])
REFERENCES [dbo].[Notifications] ([Id])
GO
ALTER TABLE [dbo].[NotificationDeliveries] CHECK CONSTRAINT [FK_NotificationDeliveries_Notifications]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_Applications] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[NotificationApplications] ([Id])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_Applications]
GO
ALTER TABLE [dbo].[NotificationDeliveries]  WITH CHECK ADD  CONSTRAINT [CK_NotificationDeliveries_Status] CHECK  (([Status]>=(1) AND [Status]<=(6)))
GO
ALTER TABLE [dbo].[NotificationDeliveries] CHECK CONSTRAINT [CK_NotificationDeliveries_Status]
GO
/****** Object:  StoredProcedure [dbo].[ClaimNextNotificationDelivery]    Script Date: 8/14/2026 10:00:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ClaimNextNotificationDelivery]
(
    @LeaseId UNIQUEIDENTIFIER,
    @LeaseDurationSeconds INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Now DATETIME2(3) = SYSUTCDATETIME();

    ;WITH NextDelivery AS
    (
        SELECT TOP (1)
            Id
        FROM dbo.NotificationDeliveries WITH
        (
            UPDLOCK,
            READPAST,
            ROWLOCK
        )
        WHERE
            -- Pending
            Status = 1

            OR

            -- Processing but lease expired
            (
                Status = 2
                AND LeaseUntil < @Now
            )

            OR

            -- Failed and ready for retry
            (
                Status = 6
                AND RetryCount < 8
                AND
                (
                    NextRetryAt IS NULL
                    OR NextRetryAt <= @Now
                )
            )

        ORDER BY CreatedAt
    )
    UPDATE d
    SET
        Status = 2,

        LeaseId = @LeaseId,

        LeaseUntil =
            DATEADD(
                SECOND,
                @LeaseDurationSeconds,
                @Now),

        UpdatedAt = @Now

    OUTPUT
        inserted.Id

    FROM dbo.NotificationDeliveries d

    INNER JOIN NextDelivery n
        ON d.Id = n.Id;
END
GO
USE [master]
GO
ALTER DATABASE [NotificationServiceDB] SET  READ_WRITE 
GO
