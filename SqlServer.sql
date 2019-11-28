USE [master]
GO

/****** Object:  Database [Taller2_cotizaciones]    Script Date: 9/4/2019 5:25:18 PM ******/
CREATE DATABASE [Taller2_cotizaciones]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Taller2_cotizaciones', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Taller2_cotizaciones.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Taller2_cotizaciones_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Taller2_cotizaciones_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Taller2_cotizaciones].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Taller2_cotizaciones] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET ARITHABORT OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Taller2_cotizaciones] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Taller2_cotizaciones] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Taller2_cotizaciones] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Taller2_cotizaciones] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [Taller2_cotizaciones] SET  MULTI_USER 
GO

ALTER DATABASE [Taller2_cotizaciones] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Taller2_cotizaciones] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Taller2_cotizaciones] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Taller2_cotizaciones] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [Taller2_cotizaciones] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [Taller2_cotizaciones] SET QUERY_STORE = OFF
GO

ALTER DATABASE [Taller2_cotizaciones] SET  READ_WRITE 
GO

USE [Taller2_cotizaciones]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nchar](50) NULL,
	[LastName] [nchar](50) NULL,
	[Username] [nchar](10) NULL,
	[Password] [nchar](50) NULL,
	[Token] [nchar](1000) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO





