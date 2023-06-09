USE [master]
GO
/****** Object:  Database [itmanager]    Script Date: 23/07/2022 07:14:20 p. m. ******/
CREATE DATABASE [itmanager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RiskManager', FILENAME = N'/var/opt/mssql/data/itmanager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RiskManager_log', FILENAME = N'/var/opt/mssql/data/itmanager_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [itmanager] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [itmanager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [itmanager] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [itmanager] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [itmanager] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [itmanager] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [itmanager] SET ARITHABORT OFF 
GO
ALTER DATABASE [itmanager] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [itmanager] SET AUTO_SHRINK ON 
GO
ALTER DATABASE [itmanager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [itmanager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [itmanager] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [itmanager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [itmanager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [itmanager] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [itmanager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [itmanager] SET  DISABLE_BROKER 
GO
ALTER DATABASE [itmanager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [itmanager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [itmanager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [itmanager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [itmanager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [itmanager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [itmanager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [itmanager] SET RECOVERY FULL 
GO
ALTER DATABASE [itmanager] SET  MULTI_USER 
GO
ALTER DATABASE [itmanager] SET PAGE_VERIFY NONE  
GO
ALTER DATABASE [itmanager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [itmanager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [itmanager] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [itmanager] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [itmanager] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'itmanager', N'ON'
GO
ALTER DATABASE [itmanager] SET QUERY_STORE = OFF
GO
USE [itmanager]
GO
/****** Object:  User [sicweb]    Script Date: 23/07/2022 07:14:20 p. m. ******/
CREATE USER [sicweb] FOR LOGIN [sicweb] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [riskmanager]    Script Date: 23/07/2022 07:14:20 p. m. ******/
CREATE USER [riskmanager] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [sicweb]
GO
/****** Object:  Table [dbo].[est_estado]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[est_estado](
	[est_estado] [varchar](20) NOT NULL,
	[est_grupo] [varchar](20) NOT NULL,
	[est_acc_asigna] [int] NULL,
	[est_inactivo] [int] NOT NULL,
 CONSTRAINT [PK_ges_grupo_estado] PRIMARY KEY CLUSTERED 
(
	[est_estado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tac_tipo_activo]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tac_tipo_activo](
	[tac_id] [bigint] IDENTITY(1,1) NOT NULL,
	[tac_nombre] [varchar](100) NOT NULL,
	[tac_inv_min] [int] NOT NULL,
 CONSTRAINT [PK_tac_tipo_activo] PRIMARY KEY CLUSTERED 
(
	[tac_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[act_activo]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[act_activo](
	[act_id] [bigint] IDENTITY(1,1) NOT NULL,
	[act_modelo] [varchar](200) NOT NULL,
	[act_serie] [varchar](200) NULL,
	[act_observaciones] [varchar](4000) NULL,
	[act_estado] [varchar](20) NULL,
	[act_inactivo] [bit] NOT NULL,
	[act_imagen1] [varchar](300) NULL,
	[act_imagen2] [varchar](300) NULL,
	[act_fechacompra] [date] NULL,
	[act_nrocompra] [varchar](200) NULL,
	[act_proveedorcompra] [varchar](200) NULL,
	[act_uid] [varchar](36) NULL,
	[act_condicion] [varchar](50) NULL,
	[act_licencia] [varchar](200) NULL,
	[tac_id] [bigint] NULL,
	[tfa_id] [bigint] NULL,
 CONSTRAINT [PK_act_activo] PRIMARY KEY CLUSTERED 
(
	[act_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_inventario_stock]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_inventario_stock]
AS
SELECT        t.tac_nombre, COUNT(a.act_id) AS stock
FROM            dbo.act_activo AS a INNER JOIN
                         dbo.est_estado AS e ON e.est_estado = a.act_estado INNER JOIN
                         dbo.tac_tipo_activo AS t ON t.tac_id = a.tac_id
WHERE        (e.est_estado <> 'baja')
GROUP BY t.tac_nombre
GO
/****** Object:  Table [dbo].[din_detalle_inventario]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[din_detalle_inventario](
	[din_id] [bigint] IDENTITY(1,1) NOT NULL,
	[inv_id] [bigint] NOT NULL,
	[act_id] [bigint] NOT NULL,
	[act_uid] [varchar](36) NOT NULL,
	[din_fecha] [datetime] NOT NULL,
	[din_estado] [varchar](20) NULL,
	[din_usuario] [varchar](100) NULL,
	[din_comentarios] [varchar](100) NULL,
 CONSTRAINT [PK_din_detalle_inventario] PRIMARY KEY CLUSTERED 
(
	[din_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[inv_inventario]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[inv_inventario](
	[inv_id] [bigint] IDENTITY(1,1) NOT NULL,
	[inv_nombre] [varchar](200) NOT NULL,
	[inv_date] [date] NOT NULL,
	[inv_inicio] [date] NOT NULL,
	[inv_fin] [date] NOT NULL,
 CONSTRAINT [PK_inv_inventario] PRIMARY KEY CLUSTERED 
(
	[inv_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_inventario_actual]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_inventario_actual]
AS
SELECT        t.tac_nombre, COUNT(a.act_id) AS stock, MAX(d.din_fecha) AS lastdate
FROM            dbo.inv_inventario AS i INNER JOIN
                         dbo.din_detalle_inventario AS d ON i.inv_id = d.inv_id INNER JOIN
                         dbo.act_activo AS a ON a.act_id = d.act_id INNER JOIN
                         dbo.tac_tipo_activo AS t ON t.tac_id = a.tac_id
WHERE        (i.inv_id IN
                             (SELECT        TOP (1) inv_id
                               FROM            dbo.inv_inventario
                               ORDER BY inv_fin DESC))
GROUP BY t.tac_nombre
GO
/****** Object:  View [dbo].[v_inventario_concilia]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_inventario_concilia]
AS
SELECT        dbo.v_inventario_actual.tac_nombre, dbo.v_inventario_stock.stock AS teorico, dbo.v_inventario_actual.stock AS fisico, TRY_CONVERT(DATE, dbo.v_inventario_actual.lastdate) AS lastdate, 
                         dbo.v_inventario_stock.stock - dbo.v_inventario_actual.stock AS diff, ISNULL(ROUND(dbo.v_inventario_actual.stock / CAST(dbo.v_inventario_stock.stock AS FLOAT) * 100, 2), 0) AS perc
FROM            dbo.v_inventario_actual RIGHT OUTER JOIN
                         dbo.v_inventario_stock ON dbo.v_inventario_actual.tac_nombre = dbo.v_inventario_stock.tac_nombre
GO
/****** Object:  View [dbo].[v_inventario_alerta]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_inventario_alerta]
AS
SELECT        t.tac_nombre, COUNT(a.act_id) AS stock, t.tac_inv_min, CASE WHEN COUNT(a.act_id) < t .tac_inv_min THEN 'Inv. Minimo' END AS msg, ISNULL(ROUND(COUNT(a.act_id) / CAST(t.tac_inv_min AS FLOAT) * 100, 2), 0) 
                         AS perc
FROM            dbo.act_activo AS a INNER JOIN
                         dbo.est_estado AS e ON e.est_estado = a.act_estado INNER JOIN
                         dbo.tac_tipo_activo AS t ON t.tac_id = a.tac_id
WHERE        (e.est_grupo = 'almacenado')
GROUP BY t.tac_nombre, t.tac_inv_min
GO
/****** Object:  Table [dbo].[aud_auditoria]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aud_auditoria](
	[aud_id] [bigint] IDENTITY(1,1) NOT NULL,
	[aud_fecha] [datetime] NOT NULL,
	[act_id] [bigint] NOT NULL,
	[aud_uid] [varchar](36) NULL,
	[aud_estado_anterior] [varchar](50) NULL,
	[aud_estado_nuevo] [varchar](50) NULL,
	[aud_modelo] [varchar](100) NULL,
	[aud_serie] [varchar](100) NULL,
	[emp_id] [bigint] NULL,
	[emp_nombre] [varchar](200) NULL,
	[emp_tipo_asignacion] [varchar](100) NULL,
	[usu_modificado_por] [varchar](200) NULL,
	[con_id] [varchar](36) NULL,
	[aud_tipo_persona] [varchar](3) NULL,
	[aud_añomes]  AS (concat(datepart(year,[aud_fecha]),'-',datepart(month,[aud_fecha]))),
 CONSTRAINT [PK_aud_auditoria] PRIMARY KEY CLUSTERED 
(
	[aud_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_ultimas_asignaciones]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_ultimas_asignaciones]
AS
SELECT        TOP (5) dbo.aud_auditoria.aud_fecha, dbo.aud_auditoria.emp_nombre, dbo.aud_auditoria.emp_tipo_asignacion, dbo.aud_auditoria.aud_tipo_persona, dbo.aud_auditoria.aud_estado_anterior, 
                         dbo.aud_auditoria.aud_estado_nuevo, dbo.aud_auditoria.aud_modelo, dbo.aud_auditoria.aud_uid, dbo.aud_auditoria.act_id, DATEDIFF(MINUTE, dbo.aud_auditoria.aud_fecha, GETDATE()) AS diffm, DATEDIFF(HOUR, 
                         dbo.aud_auditoria.aud_fecha, GETDATE()) AS diffh, DATEDIFF(DAY, dbo.aud_auditoria.aud_fecha, GETDATE()) AS diffd, CASE WHEN DATEDIFF(MINUTE, dbo.aud_auditoria.aud_fecha, GETDATE()) 
                         < 60 THEN CAST(DATEDIFF(MINUTE, dbo.aud_auditoria.aud_fecha, GETDATE()) AS VARCHAR) + ' m' ELSE CASE WHEN DATEDIFF(HOUR, dbo.aud_auditoria.aud_fecha, GETDATE()) < 24 THEN CAST(DATEDIFF(HOUR, 
                         dbo.aud_auditoria.aud_fecha, GETDATE()) AS VARCHAR) + ' h' ELSE CAST(DATEDIFF(DAY, dbo.aud_auditoria.aud_fecha, GETDATE()) AS VARCHAR) + ' d' END END AS tiempo, dbo.tac_tipo_activo.tac_nombre, 
                         dbo.aud_auditoria.aud_serie
FROM            dbo.aud_auditoria INNER JOIN
                         dbo.act_activo ON dbo.aud_auditoria.act_id = dbo.act_activo.act_id INNER JOIN
                         dbo.tac_tipo_activo ON dbo.act_activo.tac_id = dbo.tac_tipo_activo.tac_id
WHERE        (dbo.aud_auditoria.aud_estado_nuevo = 'asignado') AND (dbo.aud_auditoria.aud_estado_anterior <> dbo.aud_auditoria.aud_estado_nuevo)
ORDER BY dbo.aud_auditoria.aud_fecha DESC
GO
/****** Object:  View [dbo].[v_asignaciones]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_asignaciones]
AS
SELECT        TOP (100) PERCENT TRY_CONVERT(DATE, aud_fecha) AS fecha, DAY(aud_fecha) AS dia, COUNT(act_id) AS nro
FROM            dbo.aud_auditoria
WHERE        (aud_estado_nuevo = 'asignado') AND (aud_estado_anterior <> aud_estado_nuevo)
GROUP BY TRY_CONVERT(DATE, aud_fecha), DAY(aud_fecha)
GO
/****** Object:  View [dbo].[v_retiros]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_retiros]
AS
SELECT        TOP (100) PERCENT TRY_CONVERT(DATE, aud_fecha) AS fecha, DAY(aud_fecha) AS dia, COUNT(act_id) AS nro
FROM            dbo.aud_auditoria
WHERE        (aud_estado_nuevo = 'soporte') AND (aud_estado_anterior = 'pre-retiro')
GROUP BY TRY_CONVERT(DATE, aud_fecha), DAY(aud_fecha)
GO
/****** Object:  Table [dbo].[per_periodo]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[per_periodo](
	[fecha] [date] NOT NULL,
	[dia] [int] NOT NULL,
	[mes] [int] NOT NULL,
	[año] [int] NOT NULL,
 CONSTRAINT [PK_per_periodo] PRIMARY KEY CLUSTERED 
(
	[fecha] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_actividad_mes]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_actividad_mes]
AS
SELECT        TOP (100) PERCENT dbo.per_periodo.fecha, dbo.per_periodo.dia, ISNULL(dbo.v_asignaciones.nro, 0) AS asignaciones, ISNULL(dbo.v_retiros.nro, 0) AS retiros
FROM            dbo.v_retiros RIGHT OUTER JOIN
                         dbo.per_periodo ON dbo.v_retiros.fecha = dbo.per_periodo.fecha LEFT OUTER JOIN
                         dbo.v_asignaciones ON dbo.per_periodo.fecha = dbo.v_asignaciones.fecha
WHERE        (MONTH(GETDATE()) = MONTH(dbo.per_periodo.fecha)) AND (YEAR(GETDATE()) = YEAR(dbo.per_periodo.fecha))
ORDER BY dbo.per_periodo.fecha
GO
/****** Object:  View [dbo].[v_ultimos_retiros]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_ultimos_retiros]
AS
SELECT        TOP (5) dbo.aud_auditoria.aud_fecha, dbo.aud_auditoria.emp_nombre, dbo.aud_auditoria.emp_tipo_asignacion, dbo.aud_auditoria.aud_tipo_persona, dbo.aud_auditoria.aud_estado_anterior, 
                         dbo.aud_auditoria.aud_estado_nuevo, dbo.aud_auditoria.aud_modelo, dbo.aud_auditoria.aud_uid, dbo.aud_auditoria.act_id, DATEDIFF(MINUTE, dbo.aud_auditoria.aud_fecha, GETDATE()) AS diffm, DATEDIFF(HOUR, 
                         dbo.aud_auditoria.aud_fecha, GETDATE()) AS diffh, DATEDIFF(DAY, dbo.aud_auditoria.aud_fecha, GETDATE()) AS diffd, CASE WHEN DATEDIFF(MINUTE, dbo.aud_auditoria.aud_fecha, GETDATE()) 
                         < 60 THEN CAST(DATEDIFF(MINUTE, dbo.aud_auditoria.aud_fecha, GETDATE()) AS VARCHAR) + ' m' ELSE CASE WHEN DATEDIFF(HOUR, dbo.aud_auditoria.aud_fecha, GETDATE()) < 24 THEN CAST(DATEDIFF(HOUR, 
                         dbo.aud_auditoria.aud_fecha, GETDATE()) AS VARCHAR) + ' h' ELSE CAST(DATEDIFF(DAY, dbo.aud_auditoria.aud_fecha, GETDATE()) AS VARCHAR) + ' d' END END AS tiempo, dbo.tac_tipo_activo.tac_nombre, 
                         dbo.aud_auditoria.aud_serie
FROM            dbo.aud_auditoria INNER JOIN
                         dbo.act_activo ON dbo.aud_auditoria.act_id = dbo.act_activo.act_id INNER JOIN
                         dbo.tac_tipo_activo ON dbo.act_activo.tac_id = dbo.tac_tipo_activo.tac_id
WHERE        (dbo.aud_auditoria.aud_estado_nuevo = 'soporte') AND (dbo.aud_auditoria.aud_estado_anterior = 'pre-retiro')
ORDER BY dbo.aud_auditoria.aud_fecha DESC
GO
/****** Object:  View [dbo].[v_inventario]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_inventario]
AS
SELECT        dbo.din_detalle_inventario.din_fecha, dbo.din_detalle_inventario.din_estado, dbo.din_detalle_inventario.din_usuario, dbo.din_detalle_inventario.din_comentarios, dbo.din_detalle_inventario.inv_id, 
                         dbo.inv_inventario.inv_nombre, dbo.inv_inventario.inv_inicio, dbo.inv_inventario.inv_fin, dbo.act_activo.act_modelo, dbo.act_activo.act_serie, dbo.act_activo.act_estado, dbo.act_activo.act_condicion, 
                         dbo.act_activo.act_licencia, dbo.act_activo.act_imagen1, dbo.act_activo.act_imagen2, dbo.din_detalle_inventario.din_id
FROM            dbo.act_activo INNER JOIN
                         dbo.din_detalle_inventario ON dbo.act_activo.act_id = dbo.din_detalle_inventario.act_id INNER JOIN
                         dbo.inv_inventario ON dbo.din_detalle_inventario.inv_id = dbo.inv_inventario.inv_id
GO
/****** Object:  Table [dbo].[con_confirmacion]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[con_confirmacion](
	[con_id] [varchar](36) NOT NULL,
	[con_fecha] [datetime] NOT NULL,
	[con_firma] [text] NULL,
 CONSTRAINT [PK_con_confirmacion] PRIMARY KEY CLUSTERED 
(
	[con_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_auditoria]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- dbo.v_auditoria source

CREATE VIEW [dbo].[v_auditoria]
AS
SELECT        TOP (100) PERCENT dbo.aud_auditoria.emp_id, dbo.aud_auditoria.aud_uid, dbo.aud_auditoria.aud_fecha, dbo.aud_auditoria.act_id, dbo.aud_auditoria.aud_estado_anterior, 
						dbo.aud_auditoria.aud_estado_nuevo, dbo.aud_auditoria.aud_modelo, dbo.aud_auditoria.aud_serie, dbo.aud_auditoria.emp_nombre, 
						dbo.aud_auditoria.usu_modificado_por, dbo.con_confirmacion.con_fecha, dbo.aud_auditoria.con_id, dbo.aud_auditoria.aud_añomes 
FROM            dbo.aud_auditoria LEFT OUTER JOIN
                         dbo.con_confirmacion ON dbo.aud_auditoria.con_id = dbo.con_confirmacion.con_id;
GO
/****** Object:  View [dbo].[v_inventario_tipo]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_inventario_tipo]
AS
SELECT        dbo.tac_tipo_activo.tac_nombre, COUNT(DISTINCT dbo.act_activo.act_id) AS act_total, dbo.act_activo.act_estado, dbo.est_estado.est_grupo
FROM            dbo.tac_tipo_activo INNER JOIN
                         dbo.act_activo ON dbo.tac_tipo_activo.tac_id = dbo.act_activo.tac_id INNER JOIN
                         dbo.est_estado ON dbo.act_activo.act_estado = dbo.est_estado.est_estado
GROUP BY dbo.tac_tipo_activo.tac_nombre, dbo.act_activo.act_estado, dbo.est_estado.est_grupo
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ace_activo_empleado]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ace_activo_empleado](
	[emp_id] [bigint] NOT NULL,
	[act_id] [bigint] NOT NULL,
	[ace_fecha] [date] NOT NULL,
	[ace_tipo_asignacion] [int] NULL,
 CONSTRAINT [PK_ace_activo_empleado] PRIMARY KEY CLUSTERED 
(
	[emp_id] ASC,
	[act_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[acu_activo_usuario]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[acu_activo_usuario](
	[usu_id] [bigint] NOT NULL,
	[act_id] [bigint] NOT NULL,
	[acu_fecha] [datetime] NULL,
 CONSTRAINT [PK_acu_activo_usuario] PRIMARY KEY CLUSTERED 
(
	[usu_id] ASC,
	[act_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[dd_disco_duro]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dd_disco_duro](
	[dd_id] [bigint] IDENTITY(1,1) NOT NULL,
	[dd_capacidad] [varchar](200) NOT NULL,
	[tdd_id] [bigint] NOT NULL,
	[act_id] [bigint] NULL,
 CONSTRAINT [PK_dd_disco_duro] PRIMARY KEY CLUSTERED 
(
	[dd_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[emp_empleados]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[emp_empleados](
	[emp_id] [bigint] IDENTITY(1,1) NOT NULL,
	[emp_nombre] [varchar](220) NULL,
	[emp_cargo] [varchar](200) NULL,
	[emp_imagen] [varchar](200) NULL,
	[emp_extension] [varchar](200) NULL,
	[emp_correo] [varchar](200) NULL,
	[emp_area] [varchar](200) NULL,
	[emp_estado] [varchar](20) NULL,
 CONSTRAINT [PK_emp_empleados] PRIMARY KEY CLUSTERED 
(
	[emp_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[hw_hardware_specs]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hw_hardware_specs](
	[hw_id] [bigint] IDENTITY(1,1) NOT NULL,
	[hw_modeloproc] [varchar](200) NULL,
	[hw_generacionproc] [varchar](200) NULL,
	[hw_memoria] [varchar](200) NULL,
	[atc_id] [bigint] NULL,
	[tpr_id] [bigint] NULL,
 CONSTRAINT [PK_hw_hardware_specs] PRIMARY KEY CLUSTERED 
(
	[hw_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[opc_opcion]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[opc_opcion](
	[opc_id] [bigint] IDENTITY(1,1) NOT NULL,
	[opc_nombre] [varchar](40) NULL,
	[opc_nombre_modulo] [varchar](200) NULL,
	[opc_descripcion] [varchar](4000) NULL,
	[opc_nombre_object] [varchar](100) NULL,
	[opc_logo] [varchar](100) NULL,
	[opc_orden] [bigint] NULL,
	[opc_padre] [int] NULL,
	[opc_deshabilitado_sistema] [int] NULL,
 CONSTRAINT [PK_opc_opcion] PRIMARY KEY CLUSTERED 
(
	[opc_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[oro_opcion_rol]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[oro_opcion_rol](
	[rol_id] [bigint] NOT NULL,
	[opc_id] [bigint] NOT NULL,
	[oro_date] [datetime] NOT NULL,
 CONSTRAINT [PK_oro_opcion_rol] PRIMARY KEY CLUSTERED 
(
	[rol_id] ASC,
	[opc_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[par_parametro]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[par_parametro](
	[par_id] [bigint] IDENTITY(1,1) NOT NULL,
	[par_nombre] [varchar](100) NULL,
	[par_valor1] [varchar](2000) NULL,
	[par_valor2] [varchar](400) NULL,
	[par_tipo] [bigint] NULL,
 CONSTRAINT [PK_par_parametro] PRIMARY KEY CLUSTERED 
(
	[par_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rol_roles]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rol_roles](
	[rol_id] [bigint] IDENTITY(1,1) NOT NULL,
	[rol_nombre] [varchar](30) NOT NULL,
	[rol_descripcion] [varchar](200) NULL,
	[rol_bloqueado] [int] NOT NULL,
 CONSTRAINT [PK_rol_roles] PRIMARY KEY CLUSTERED 
(
	[rol_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sis_sistema]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sis_sistema](
	[sis_id] [bigint] IDENTITY(1,1) NOT NULL,
	[sis_nombre] [varchar](200) NULL,
	[sis_descripcion] [varchar](500) NULL,
	[sis_proceso] [varchar](200) NULL,
	[sis_tipo] [int] NULL,
 CONSTRAINT [PK_sis_sistema] PRIMARY KEY CLUSTERED 
(
	[sis_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tdd_tipo_disco]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tdd_tipo_disco](
	[tdd_id] [bigint] IDENTITY(1,1) NOT NULL,
	[tdd_nombre] [varchar](200) NOT NULL,
 CONSTRAINT [PK_tdd_tipo_disco] PRIMARY KEY CLUSTERED 
(
	[tdd_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tfa_tipo_fabricante]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tfa_tipo_fabricante](
	[tfa_id] [bigint] IDENTITY(1,1) NOT NULL,
	[tfa_nombre] [varchar](200) NULL,
 CONSTRAINT [PK_tfa_tipo_fabricante] PRIMARY KEY CLUSTERED 
(
	[tfa_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tip_tipo_parametro]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tip_tipo_parametro](
	[tip_id] [bigint] IDENTITY(1,1) NOT NULL,
	[tip_nombre] [varchar](255) NULL,
	[tip_orden] [int] NULL,
 CONSTRAINT [PK_tip_tipo_parametro] PRIMARY KEY CLUSTERED 
(
	[tip_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[top_tipo_opcion]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[top_tipo_opcion](
	[top_id] [bigint] IDENTITY(1,1) NOT NULL,
	[top_nombre] [varchar](50) NULL,
	[top_orden] [bigint] NULL,
 CONSTRAINT [PK_top_tipo_opcion] PRIMARY KEY CLUSTERED 
(
	[top_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tpr_tipo_procesador]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tpr_tipo_procesador](
	[tpr_id] [bigint] IDENTITY(1,1) NOT NULL,
	[tpr_nombre] [varchar](200) NULL,
	[tpr_imagen] [varchar](200) NULL,
 CONSTRAINT [PK_tpr_tipo_procesador] PRIMARY KEY CLUSTERED 
(
	[tpr_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tri_transaccion_identificador]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tri_transaccion_identificador](
	[id] [uniqueidentifier] NULL,
	[fecha] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[uro_usuario_rol]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[uro_usuario_rol](
	[usu_id] [bigint] NOT NULL,
	[rol_id] [bigint] NOT NULL,
	[uro_date] [datetime] NOT NULL,
 CONSTRAINT [PK_uro_usuario_rol] PRIMARY KEY CLUSTERED 
(
	[usu_id] ASC,
	[rol_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usu_usuarios]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usu_usuarios](
	[usu_id] [bigint] IDENTITY(1,1) NOT NULL,
	[usu_nombre] [varchar](90) NOT NULL,
	[usu_fecha_final] [datetime] NOT NULL,
	[usu_login] [varchar](30) NOT NULL,
	[usu_clave] [varchar](50) NULL,
	[usu_admin] [bit] NOT NULL,
	[usu_bloqueado] [bit] NOT NULL,
	[usu_imagen] [varchar](500) NULL,
	[usu_asigna_activos] [bit] NULL,
 CONSTRAINT [PK_usu_usuarios] PRIMARY KEY CLUSTERED 
(
	[usu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ace_activo_empleado] ADD  CONSTRAINT [DF__ace_activ__ace_f__7C8F6DA6]  DEFAULT (getdate()) FOR [ace_fecha]
GO
ALTER TABLE [dbo].[ace_activo_empleado] ADD  CONSTRAINT [DF_ace_activo_empleado_ace_tipo_asignacion]  DEFAULT ((1)) FOR [ace_tipo_asignacion]
GO
ALTER TABLE [dbo].[act_activo] ADD  CONSTRAINT [DF_act_activo_act_estado]  DEFAULT ('almacenado') FOR [act_estado]
GO
ALTER TABLE [dbo].[act_activo] ADD  CONSTRAINT [DF_act_activo_act_inactivo]  DEFAULT ((0)) FOR [act_inactivo]
GO
ALTER TABLE [dbo].[act_activo] ADD  CONSTRAINT [DF__act_activ__act_u__7D8391DF]  DEFAULT (left(newid(),(8))) FOR [act_uid]
GO
ALTER TABLE [dbo].[acu_activo_usuario] ADD  CONSTRAINT [DF__acu_activ__acu_f__1C0818FF]  DEFAULT (getdate()) FOR [acu_fecha]
GO
ALTER TABLE [dbo].[aud_auditoria] ADD  CONSTRAINT [DF_aud_auditoria_aud_fecha]  DEFAULT (getdate()) FOR [aud_fecha]
GO
ALTER TABLE [dbo].[con_confirmacion] ADD  CONSTRAINT [DF_con_confirmacion_con_fecha]  DEFAULT (getdate()) FOR [con_fecha]
GO
ALTER TABLE [dbo].[est_estado] ADD  CONSTRAINT [DF_ges_grupo_estado_est_acc_asigna]  DEFAULT ((0)) FOR [est_acc_asigna]
GO
ALTER TABLE [dbo].[est_estado] ADD  CONSTRAINT [DF_ges_grupo_estado_est_inactivo]  DEFAULT ((0)) FOR [est_inactivo]
GO
ALTER TABLE [dbo].[inv_inventario] ADD  CONSTRAINT [DF_inv_inventario_inv_date]  DEFAULT (getdate()) FOR [inv_date]
GO
ALTER TABLE [dbo].[opc_opcion] ADD  CONSTRAINT [DF__opc_opcio__OPC_D__398D8EEE]  DEFAULT ((0)) FOR [opc_deshabilitado_sistema]
GO
ALTER TABLE [dbo].[oro_opcion_rol] ADD  CONSTRAINT [DF_oro_opcion_rol_oro_date]  DEFAULT (getdate()) FOR [oro_date]
GO
ALTER TABLE [dbo].[rol_roles] ADD  CONSTRAINT [DF_rol_roles_rol_bloqueado]  DEFAULT ((0)) FOR [rol_bloqueado]
GO
ALTER TABLE [dbo].[tac_tipo_activo] ADD  CONSTRAINT [DF_tac_tipo_activo_tac_inv_min]  DEFAULT ((0)) FOR [tac_inv_min]
GO
ALTER TABLE [dbo].[tri_transaccion_identificador] ADD  CONSTRAINT [DF_tri_transaccion_identificador_fecha]  DEFAULT (getdate()) FOR [fecha]
GO
ALTER TABLE [dbo].[uro_usuario_rol] ADD  CONSTRAINT [DF_uro_usuario_rol_uro_date]  DEFAULT (getdate()) FOR [uro_date]
GO
ALTER TABLE [dbo].[usu_usuarios] ADD  CONSTRAINT [DF_usu_usuarios_usu_fecha_final]  DEFAULT (getdate()) FOR [usu_fecha_final]
GO
ALTER TABLE [dbo].[usu_usuarios] ADD  CONSTRAINT [DF_usu_usuarios_USU_ADMIN]  DEFAULT ((0)) FOR [usu_admin]
GO
ALTER TABLE [dbo].[usu_usuarios] ADD  CONSTRAINT [DF_usu_usuarios_USU_BLOQUEADO]  DEFAULT ((0)) FOR [usu_bloqueado]
GO
ALTER TABLE [dbo].[usu_usuarios] ADD  DEFAULT ((0)) FOR [usu_asigna_activos]
GO
ALTER TABLE [dbo].[ace_activo_empleado]  WITH CHECK ADD  CONSTRAINT [FK_ace_activo_empleado_act_activo] FOREIGN KEY([act_id])
REFERENCES [dbo].[act_activo] ([act_id])
GO
ALTER TABLE [dbo].[ace_activo_empleado] CHECK CONSTRAINT [FK_ace_activo_empleado_act_activo]
GO
ALTER TABLE [dbo].[ace_activo_empleado]  WITH CHECK ADD  CONSTRAINT [FK_ace_activo_empleado_emp_empleados] FOREIGN KEY([emp_id])
REFERENCES [dbo].[emp_empleados] ([emp_id])
GO
ALTER TABLE [dbo].[ace_activo_empleado] CHECK CONSTRAINT [FK_ace_activo_empleado_emp_empleados]
GO
ALTER TABLE [dbo].[act_activo]  WITH CHECK ADD  CONSTRAINT [FK_act_activo_tac_tipo_activo] FOREIGN KEY([tac_id])
REFERENCES [dbo].[tac_tipo_activo] ([tac_id])
GO
ALTER TABLE [dbo].[act_activo] CHECK CONSTRAINT [FK_act_activo_tac_tipo_activo]
GO
ALTER TABLE [dbo].[act_activo]  WITH CHECK ADD  CONSTRAINT [FK_act_activo_tfa_tipo_fabricante] FOREIGN KEY([tfa_id])
REFERENCES [dbo].[tfa_tipo_fabricante] ([tfa_id])
GO
ALTER TABLE [dbo].[act_activo] CHECK CONSTRAINT [FK_act_activo_tfa_tipo_fabricante]
GO
ALTER TABLE [dbo].[acu_activo_usuario]  WITH CHECK ADD  CONSTRAINT [FK_acu_activo_usuario_act_activo] FOREIGN KEY([act_id])
REFERENCES [dbo].[act_activo] ([act_id])
GO
ALTER TABLE [dbo].[acu_activo_usuario] CHECK CONSTRAINT [FK_acu_activo_usuario_act_activo]
GO
ALTER TABLE [dbo].[acu_activo_usuario]  WITH CHECK ADD  CONSTRAINT [FK_acu_activo_usuario_usu_usuarios] FOREIGN KEY([usu_id])
REFERENCES [dbo].[usu_usuarios] ([usu_id])
GO
ALTER TABLE [dbo].[acu_activo_usuario] CHECK CONSTRAINT [FK_acu_activo_usuario_usu_usuarios]
GO
ALTER TABLE [dbo].[aud_auditoria]  WITH CHECK ADD  CONSTRAINT [FK_aud_auditoria_act_activo] FOREIGN KEY([act_id])
REFERENCES [dbo].[act_activo] ([act_id])
GO
ALTER TABLE [dbo].[aud_auditoria] CHECK CONSTRAINT [FK_aud_auditoria_act_activo]
GO
ALTER TABLE [dbo].[aud_auditoria]  WITH NOCHECK ADD  CONSTRAINT [FK_aud_auditoria_con_confirmacion] FOREIGN KEY([con_id])
REFERENCES [dbo].[con_confirmacion] ([con_id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[aud_auditoria] NOCHECK CONSTRAINT [FK_aud_auditoria_con_confirmacion]
GO
ALTER TABLE [dbo].[dd_disco_duro]  WITH CHECK ADD  CONSTRAINT [FK_dd_disco_duro_act_activo] FOREIGN KEY([act_id])
REFERENCES [dbo].[act_activo] ([act_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[dd_disco_duro] CHECK CONSTRAINT [FK_dd_disco_duro_act_activo]
GO
ALTER TABLE [dbo].[dd_disco_duro]  WITH CHECK ADD  CONSTRAINT [FK_dd_disco_duro_tdd_tipo_disco] FOREIGN KEY([tdd_id])
REFERENCES [dbo].[tdd_tipo_disco] ([tdd_id])
GO
ALTER TABLE [dbo].[dd_disco_duro] CHECK CONSTRAINT [FK_dd_disco_duro_tdd_tipo_disco]
GO
ALTER TABLE [dbo].[din_detalle_inventario]  WITH CHECK ADD  CONSTRAINT [FK_din_detalle_inventario_act_activo] FOREIGN KEY([act_id])
REFERENCES [dbo].[act_activo] ([act_id])
GO
ALTER TABLE [dbo].[din_detalle_inventario] CHECK CONSTRAINT [FK_din_detalle_inventario_act_activo]
GO
ALTER TABLE [dbo].[din_detalle_inventario]  WITH CHECK ADD  CONSTRAINT [FK_din_detalle_inventario_inv_inventario] FOREIGN KEY([inv_id])
REFERENCES [dbo].[inv_inventario] ([inv_id])
GO
ALTER TABLE [dbo].[din_detalle_inventario] CHECK CONSTRAINT [FK_din_detalle_inventario_inv_inventario]
GO
ALTER TABLE [dbo].[hw_hardware_specs]  WITH CHECK ADD  CONSTRAINT [FK_hw_hardware_specs_act_activo] FOREIGN KEY([atc_id])
REFERENCES [dbo].[act_activo] ([act_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[hw_hardware_specs] CHECK CONSTRAINT [FK_hw_hardware_specs_act_activo]
GO
ALTER TABLE [dbo].[hw_hardware_specs]  WITH CHECK ADD  CONSTRAINT [FK_hw_hardware_specs_tpr_tipo_procesador] FOREIGN KEY([tpr_id])
REFERENCES [dbo].[tpr_tipo_procesador] ([tpr_id])
GO
ALTER TABLE [dbo].[hw_hardware_specs] CHECK CONSTRAINT [FK_hw_hardware_specs_tpr_tipo_procesador]
GO
ALTER TABLE [dbo].[oro_opcion_rol]  WITH CHECK ADD  CONSTRAINT [FK_oro_opcion_rol_opc_opcion] FOREIGN KEY([opc_id])
REFERENCES [dbo].[opc_opcion] ([opc_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[oro_opcion_rol] CHECK CONSTRAINT [FK_oro_opcion_rol_opc_opcion]
GO
ALTER TABLE [dbo].[oro_opcion_rol]  WITH CHECK ADD  CONSTRAINT [FK_oro_opcion_rol_rol_roles] FOREIGN KEY([rol_id])
REFERENCES [dbo].[rol_roles] ([rol_id])
GO
ALTER TABLE [dbo].[oro_opcion_rol] CHECK CONSTRAINT [FK_oro_opcion_rol_rol_roles]
GO
ALTER TABLE [dbo].[par_parametro]  WITH CHECK ADD  CONSTRAINT [FK_par_parametro_tip_tipo_parametro] FOREIGN KEY([par_tipo])
REFERENCES [dbo].[tip_tipo_parametro] ([tip_id])
GO
ALTER TABLE [dbo].[par_parametro] CHECK CONSTRAINT [FK_par_parametro_tip_tipo_parametro]
GO
ALTER TABLE [dbo].[uro_usuario_rol]  WITH CHECK ADD  CONSTRAINT [FK_uro_usuario_rol_rol_roles] FOREIGN KEY([rol_id])
REFERENCES [dbo].[rol_roles] ([rol_id])
GO
ALTER TABLE [dbo].[uro_usuario_rol] CHECK CONSTRAINT [FK_uro_usuario_rol_rol_roles]
GO
ALTER TABLE [dbo].[uro_usuario_rol]  WITH CHECK ADD  CONSTRAINT [FK_uro_usuario_rol_usu_usuarios] FOREIGN KEY([usu_id])
REFERENCES [dbo].[usu_usuarios] ([usu_id])
GO
ALTER TABLE [dbo].[uro_usuario_rol] CHECK CONSTRAINT [FK_uro_usuario_rol_usu_usuarios]
GO
/****** Object:  StoredProcedure [dbo].[Auth_Users]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Auth_Users]
	@login VARCHAR(36),
	@clave VARCHAR(50)
AS
BEGIN
  SET NOCOUNT ON;

    SELECT *
    FROM usu_usuarios
    WHERE usu_login = @login AND usu_clave = @clave

END
GO
/****** Object:  StoredProcedure [dbo].[CRUD_opc_opcion]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRUD_opc_opcion]
	@tipo VARCHAR(2),
	@id VARCHAR(36) = null,
	@nombre VARCHAR(100) = null,
	@modulo VARCHAR(100) = null,
	@desc VARCHAR(4000) = null,
	@page varchar(100) = null,
	@logo varchar(100) = null,
	@orden bigint = null,
	@deshabilitado int = null,
	@padre int = null,
	@rowid as bigint = 0,
	@guid varchar(36) = ''
AS
BEGIN
  SET NOCOUNT ON;

	DECLARE @count as INT = 0;
	DECLARE @val as INT = 0;

	-- // -------------------------------------------------------------------------------------------------------------------
	------ Administración CRUD
	----------------------------------------------------------------------------------------------------------------------- //
	
	IF (@tipo = 'I' or @tipo = 'U' or @tipo = 'D')	-- Valida solo para el crud
	BEGIN
		IF dbo.get_trx(@guid) = 1					-- Valida el trxid
		BEGIN

			-- Insert
			IF @tipo = 'I'
			BEGIN
				-- Autoincrementa el id de la tabla
				DECLARE @id_auto as BIGINT
				SELECT @id_auto = MAX(opc_id) + 1 FROM opc_opcion

				IF @padre = 0
				BEGIN
					SET @padre = NULL
				END 

				INSERT INTO opc_opcion (opc_id, opc_nombre, opc_descripcion, opc_nombre_modulo, opc_nombre_object, opc_logo, opc_orden, opc_deshabilitado_sistema,opc_padre)
				VALUES (@id_auto, @nombre, @desc, @modulo, @page, @logo, @orden, @deshabilitado, @padre)
			END

			-- Update
			IF @tipo = 'U'
			BEGIN
				IF @padre = 0
				BEGIN
					SET @padre = NULL
				END 

				UPDATE opc_opcion 
				SET opc_nombre = @nombre, opc_descripcion = @desc, opc_nombre_modulo = @modulo, opc_nombre_object = @page, opc_logo = @logo, opc_orden = @orden, opc_deshabilitado_sistema =  @deshabilitado, opc_padre = @padre
				WHERE opc_id = @rowid
			END

			-- Delete
			IF @tipo = 'D'
			BEGIN
				DELETE FROM opc_opcion 
				WHERE opc_id = @rowid
			END

		END
		ELSE
		BEGIN
			SELECT 'IDENTIFICADOR DE TRANSACCION NO VALIDO'
		END
	END		


	-- // -------------------------------------------------------------------------------------------------------------------
	------ Procesos adicionales sobre la tabla
	----------------------------------------------------------------------------------------------------------------------- //

	----------------------- Consulta la tabla completa
	IF @tipo = 'T'
	BEGIN
		SELECT o.*
		FROM opc_opcion o
		ORDER BY opc_padre, opc_orden
	END

	-- Busca por nombre de objeto
	IF @tipo = 'T2'
	BEGIN
		SELECT opc_id, opc_nombre
		FROM opc_opcion o
		WHERE opc_deshabilitado_sistema = 0 and opc_padre IS NULL
		ORDER BY opc_orden
	END

	----------------------- Consulta todo el registro por id de opción
	IF @tipo = 'G1'
	BEGIN
		SELECT *
		FROM opc_opcion
		WHERE opc_id = @id
	END

	----------------------- Consulta todo el registro por nombre de pagina
	IF @tipo = 'G2'
	BEGIN
		SELECT o.*, o2.opc_nombre as opc_padre_nombre
		FROM opc_opcion o
		LEFT OUTER JOIN opc_opcion o2 ON o.opc_padre = o2.opc_id
		WHERE o.opc_nombre_object = @page
	END

	----------------------- Numero de hijos de la opción
	IF @tipo = 'G3'
	BEGIN
		SELECT count(1) as contador
		FROM opc_opcion o
		WHERE opc_padre = @id
	END


END


GO
/****** Object:  StoredProcedure [dbo].[CRUD_oro_opcion_rol]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRUD_oro_opcion_rol]
	@tipo VARCHAR(2),
	@id int = 0,
	@opc int = 0
AS
BEGIN
  SET NOCOUNT ON;

	DECLARE @count as INT = 0;
	DECLARE @val as INT = 0;

	-- // -------------------------------------------------------------------------------------------------------------------
	------ Administración CRUD
	----------------------------------------------------------------------------------------------------------------------- //
	
	IF @tipo = 'I'		-- Insert
	BEGIN		
		INSERT INTO oro_opcion_rol (rol_id, opc_id)
		VALUES (@id, @opc)
	END

	IF @tipo = 'D'		-- Delete
	BEGIN		
		-- Autoincrementa el id de la tabla
		DELETE FROM oro_opcion_rol where rol_id = @id		
	END


	-- // -------------------------------------------------------------------------------------------------------------------
	------ Procesos adicionales sobre la tabla
	----------------------------------------------------------------------------------------------------------------------- //

	----------------------- Consulta la tabla completa
	IF @tipo = 'T'		
	BEGIN
		SELECT *
		FROM oro_opcion_rol
	END

	-- Todas las opciones del rol 
	IF @tipo = 'T2'
	BEGIN
		SELECT o.opc_padre, o.opc_id, opc_nombre, opc_deshabilitado_sistema, r.rol_id
		FROM opc_opcion o
		LEFT OUTER JOIN oro_opcion_rol r ON r.opc_id = o.opc_id AND r.rol_id = @id
		ORDER BY opc_padre, opc_orden
	END

END





GO
/****** Object:  StoredProcedure [dbo].[CRUD_par_parametro]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRUD_par_parametro]
	@tipo VARCHAR(3),
	@id varchar(100) = null,
	@nombre VARCHAR(100) = null,
	@valor1 VARCHAR(255) = null,
	@valor2 VARCHAR(255) = null,
	@tipop bigint = null,
	@rowid bigint = null,
	@guid varchar(36) = ''
AS
BEGIN
  SET NOCOUNT ON;

	DECLARE @count as INT = 0;
	DECLARE @val as INT = 0;

	-- // -------------------------------------------------------------------------------------------------------------------
	------ Administración CRUD
	----------------------------------------------------------------------------------------------------------------------- //
	
	
	IF (@tipo = 'I' or @tipo = 'U' or @tipo = 'D')	-- Valida solo para el crud
	BEGIN
		IF dbo.get_trx(@guid) = 1					-- Valida el trxid
		BEGIN

			IF @tipo = 'I'		-- Insert
			BEGIN		
				-- Autoincrementa el id de la tabla
				DECLARE @id_auto as BIGINT
				SELECT @id_auto = MAX(par_id) + 1 FROM par_parametro		

				INSERT INTO par_parametro (par_id, par_nombre, par_valor1, par_valor2, par_tipo)
				VALUES (@id_auto, @nombre, @valor1, @valor2, @tipop)
			END

			IF @tipo = 'U'		-- Update
			BEGIN
				UPDATE par_parametro 
				SET par_nombre = @nombre, par_valor1 = @valor1, par_valor2 = @valor2
				WHERE par_id = @rowid
			END

			IF @tipo = 'D'		-- Delete
			BEGIN
				DELETE FROM par_parametro 
				WHERE par_id = @rowid
			END

		END
		ELSE
		BEGIN
			SELECT 'IDENTIFICADOR DE TRANSACCION NO VALIDO'
		END
	END					

	-- // -------------------------------------------------------------------------------------------------------------------
	------ Procesos adicionales sobre la tabla
	----------------------------------------------------------------------------------------------------------------------- //

	----------------------- Consulta la tabla completa
	IF @tipo = 'T'		
	BEGIN
		SELECT *
		FROM par_parametro
		WHERE (@tipop IS NULL OR par_tipo = @tipop)
		ORDER BY par_nombre, par_valor1, par_valor2
	END

	----------------------- Consulta la tabla completa
	IF @tipo = 'TP1'		
	BEGIN
		SELECT tip_id as id, tip_nombre as nombre
		FROM tip_tipo_parametro
		order by tip_orden
	END
	
	----------------------- Consulta para el proceso de autenticación
	IF @tipo = 'G1'		
	BEGIN
		SELECT *
		FROM par_parametro
		WHERE par_id = @id
	END

		----------------------- Consulta para el proceso de autenticación
	IF @tipo = 'G2'		
	BEGIN
		SELECT *
		FROM par_parametro
		WHERE par_nombre = @nombre
	END

		----------------------- Consulta para el proceso de autenticación
	IF @tipo = 'G3'		
	BEGIN
		SELECT par_valor1
		FROM par_parametro
		WHERE par_nombre = @nombre
	END

		----------------------- Consulta para el proceso de autenticación
	IF @tipo = 'G4'		
	BEGIN
		SELECT par_valor2
		FROM par_parametro
		WHERE par_nombre = @nombre
	END

END





GO
/****** Object:  StoredProcedure [dbo].[CRUD_rol_roles]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRUD_rol_roles]
	@tipo VARCHAR(2),
	@id varchar(36) = null,
	@nombre VARCHAR(30) = null,
	@descripcion VARCHAR(200) = null,
	@bloqueado int = 0,
	@rowid bigint = 0,
	@guid varchar(36) = ''
AS
BEGIN
  SET NOCOUNT ON;

	DECLARE @count as INT = 0;
	DECLARE @val as INT = 0;

	-- // -------------------------------------------------------------------------------------------------------------------
	------ Administración CRUD
	----------------------------------------------------------------------------------------------------------------------- //
	
	
	IF (@tipo = 'I' or @tipo = 'U' or @tipo = 'D')	-- Valida solo para el crud
	BEGIN
		IF dbo.get_trx(@guid) = 1					-- Valida el trxid
		BEGIN

			IF @tipo = 'I'		-- Insert
			BEGIN		
				-- Autoincrementa el id de la tabla
				DECLARE @id_auto as BIGINT
				SELECT @id_auto = MAX(rol_id) + 1 FROM rol_roles		

				INSERT INTO rol_roles (rol_id, rol_nombre, rol_descripcion, rol_bloqueado)
				VALUES (@id_auto, @nombre, @descripcion, @bloqueado)
			END

			IF @tipo = 'U'		-- Update
			BEGIN
				UPDATE rol_roles 
				SET rol_nombre = @nombre, rol_descripcion = @descripcion
				WHERE rol_id = @rowid
			END

			IF @tipo = 'D'		-- Delete
			BEGIN
				DELETE FROM rol_roles 
				WHERE rol_id = @rowid
			END


		END
		ELSE
		BEGIN
			SELECT 'IDENTIFICADOR DE TRANSACCION NO VALIDO'
		END
	END					

	-- // -------------------------------------------------------------------------------------------------------------------
	------ Procesos adicionales sobre la tabla
	----------------------------------------------------------------------------------------------------------------------- //

	----------------------- Consulta la tabla completa
	IF @tipo = 'T'		
	BEGIN
		SELECT *
		FROM rol_roles
	END
	
	----------------------- Consulta para el proceso de autenticación
	IF @tipo = 'G1'		
	BEGIN
		SELECT *
		FROM rol_roles
		WHERE rol_id = @id
	END

END





GO
/****** Object:  StoredProcedure [dbo].[CRUD_sis_sistema]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRUD_sis_sistema]
	@tipo VARCHAR(2)
AS
BEGIN
  SET NOCOUNT ON;

	-- // -------------------------------------------------------------------------------------------------------------------
	------ Administración CRUD
	----------------------------------------------------------------------------------------------------------------------- //

	-- // -------------------------------------------------------------------------------------------------------------------
	------ Procesos adicionales sobre la tabla
	----------------------------------------------------------------------------------------------------------------------- //

	----------------------- Consulta la tabla completa

	IF @tipo = 'B'		
	BEGIN
		-- Borra los datos
		DELETE FROM aud_auditoria
		WHERE aud_fecha < GETDATE() - 30 
	END
	
	----------------------- Consulta para el proceso de autenticación
	IF @tipo = 'G1'		
	BEGIN
		SELECT *
		FROM sis_sistema
		WHERE sis_tipo = 1
	END

	----------------------- Consulta para el proceso de autenticación
	IF @tipo = 'G2'		
	BEGIN
		SELECT *
		FROM sis_sistema
		WHERE sis_tipo = 2
	END

END



GO
/****** Object:  StoredProcedure [dbo].[CRUD_top_tipo_opcion]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRUD_top_tipo_opcion]
	@tipo VARCHAR(2),
	@id varchar(36) = null,
	@nombre VARCHAR(30) = null,
	@orden INT = 0,
	@rowid bigint = 0,
	@guid varchar(36) = ''
AS
BEGIN
  SET NOCOUNT ON;

	DECLARE @count as INT = 0;
	DECLARE @val as INT = 0;

	-- // -------------------------------------------------------------------------------------------------------------------
	------ Administración CRUD
	----------------------------------------------------------------------------------------------------------------------- //
	
	
	IF (@tipo = 'I' or @tipo = 'U' or @tipo = 'D')	-- Valida solo para el crud
	BEGIN
		IF dbo.get_trx(@guid) = 1					-- Valida el trxid
		BEGIN

			IF @tipo = 'I'		-- Insert
			BEGIN		
				-- Autoincrementa el id de la tabla
				DECLARE @id_auto as BIGINT
				SELECT @id_auto = MAX(rol_id) + 1 FROM rol_roles		

				INSERT INTO top_tipo_opcion (top_id, top_nombre, top_orden)
				VALUES (@id_auto, @nombre, @orden)
			END

			IF @tipo = 'U'		-- Update
			BEGIN
				UPDATE top_tipo_opcion 
				SET top_nombre = @nombre, top_orden = @orden
				WHERE top_id = @rowid
			END

			IF @tipo = 'D'		-- Delete
			BEGIN
				DELETE FROM top_tipo_opcion 
				WHERE top_id = @rowid
			END

		END
		ELSE
		BEGIN
			SELECT 'IDENTIFICADOR DE TRANSACCION NO VALIDO'
		END
	END					

	-- // -------------------------------------------------------------------------------------------------------------------
	------ Procesos adicionales sobre la tabla
	----------------------------------------------------------------------------------------------------------------------- //

	----------------------- Consulta la tabla completa
	IF @tipo = 'T'		
	BEGIN
		SELECT *
		FROM top_tipo_opcion
	END
	
	----------------------- Consulta para el proceso de autenticación
	IF @tipo = 'G1'		
	BEGIN
		SELECT *
		FROM top_tipo_opcion
		WHERE top_id = @id
	END

END





GO
/****** Object:  StoredProcedure [dbo].[CRUD_uro_usuario_rol]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRUD_uro_usuario_rol]
	@tipo VARCHAR(2),
	@id int = 0,
	@usu int = 0
AS
BEGIN
  SET NOCOUNT ON;

	DECLARE @count as INT = 0;
	DECLARE @val as INT = 0;

	-- // -------------------------------------------------------------------------------------------------------------------
	------ Administración CRUD
	----------------------------------------------------------------------------------------------------------------------- //
	
	IF @tipo = 'I'		-- Insert
	BEGIN		
		INSERT INTO uro_usuario_rol (rol_id, usu_id)
		VALUES (@id, @usu)
	END

	IF @tipo = 'D'		-- Delete
	BEGIN		
		DELETE FROM uro_usuario_rol 
		WHERE rol_id = @id		
	END


	-- // -------------------------------------------------------------------------------------------------------------------
	------ Procesos adicionales sobre la tabla
	----------------------------------------------------------------------------------------------------------------------- //

	----------------------- Consulta la tabla completa
	IF @tipo = 'T'		
	BEGIN
		SELECT *
		FROM uro_usuario_rol
	END

	-- Todas las opciones del rol 
	IF @tipo = 'T2'
	BEGIN
		SELECT u.*, rol_id
		FROM usu_usuarios u
		LEFT OUTER JOIN uro_usuario_rol r ON r.usu_id = u.usu_id AND r.rol_id =  @id
	END

END





GO
/****** Object:  StoredProcedure [dbo].[CRUD_usu_usuario]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRUD_usu_usuario]
	@tipo VARCHAR(2),
	@id VARCHAR(36) = null,
	@nombre VARCHAR(90) = null,
	@fecha datetime = null,
	@login varchar(30) = null,
	@clave varchar(50) = null,
	@admin int = null,
	@imagen varchar(200) = null,
	@rowid BIGINT = 0,
	@guid VARCHAR(36) = ''

AS
BEGIN
  SET NOCOUNT ON;

	DECLARE @count as INT = 0;
	DECLARE @val as INT = 0;

	-- // -------------------------------------------------------------------------------------------------------------------
	------ Administración CRUD
	----------------------------------------------------------------------------------------------------------------------- //
	
	
	IF (@tipo = 'I' or @tipo = 'U' or @tipo = 'D' or @tipo = 'P')	-- Valida solo para el crud
	BEGIN
		IF dbo.get_trx(@guid) = 1					-- Valida el trxid
		BEGIN

			IF @tipo = 'I'		-- Insert
			BEGIN
				DECLARE @id_auto as BIGINT
				SELECT @id_auto = MAX(usu_id) + 1 FROM usu_usuarios

				INSERT INTO usu_usuarios (usu_id, usu_nombre, usu_fecha_final, usu_login, usu_clave, usu_admin, usu_imagen)
				VALUES (@id_auto, @nombre, @fecha, @login, null, @admin, @imagen)
			END

			-- Update
			IF @tipo = 'U'		-- Update
			BEGIN
				UPDATE usu_usuarios 
				SET usu_nombre = @nombre, usu_fecha_final = @fecha, usu_login = @login, usu_admin = @admin, usu_imagen = @imagen
				WHERE usu_id = @rowid
			END

			-- Update
			IF @tipo = 'D'		-- Delete'
			BEGIN
				DELETE FROM usu_usuarios 
				WHERE usu_id = @rowid
			END

			-- Update
			IF @tipo = 'P'		-- Password
			BEGIN
				UPDATE usu_usuarios 
				SET usu_clave = @clave
				WHERE usu_id = @rowid
			END

		END
		ELSE
		BEGIN
			SELECT 'IDENTIFICADOR DE TRANSACCION NO VALIDO'
		END
	END					

	-- // -------------------------------------------------------------------------------------------------------------------
	------ Procesos adicionales sobre la tabla
	----------------------------------------------------------------------------------------------------------------------- //

	----------------------- Consulta la tabla completa
	IF @tipo = 'T'
	BEGIN
		SELECT *, DATEDIFF(DAY, GETDATE(), usu_fecha_final) as usu_dias, CONVERT(DATE, usu_fecha_final, 102) as usu_expiracion
		FROM usu_usuarios
	END

	----------------------- Consulta para el proceso de autenticación
	IF @tipo = 'G1'
	BEGIN
		SELECT *
		FROM usu_usuarios
		WHERE usu_login = @login AND usu_clave = @clave
	END

	IF @tipo = 'G2'
	BEGIN
		SELECT TOP 1 *
		FROM usu_usuarios u
		LEFT OUTER JOIN uro_usuario_rol r ON u.usu_id = r.usu_id AND usu_login = @login
		INNER JOIN rol_roles ro ON r.rol_id = ro.rol_id
	END

END





GO
/****** Object:  StoredProcedure [dbo].[DEL_TrxID]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_TrxID]
	@id as varchar(36)
AS
BEGIN
	SET NOCOUNT ON;

	-- Depura la tabla de transacciones
	DELETE FROM tri_transaccion_identificador
	WHERE id = @id

END




GO
/****** Object:  StoredProcedure [dbo].[GET_LastID]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GET_LastID]
	@tabla VARCHAR(100),
	@campo VARCHAR(100) = null
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sql nvarchar(500);  
	DECLARE @id nvarchar(100);
	 
	SET @sql = 'SELECT max(' + @campo + ') as value FROM ' + @tabla + '';  
	EXECUTE sp_executesql @sql;

END
GO
/****** Object:  StoredProcedure [dbo].[GET_Menu]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GET_Menu]
	@tipo VARCHAR(2) = null, 
	@padre VARCHAR(10) = null,
	@page as varchar(200) = null
AS
BEGIN
  SET NOCOUNT ON;
	
	DECLARE @idact as varchar(10)

	IF @tipo = 'P'
	BEGIN
	
		SELECT @idact = opc_padre
		FROM opc_opcion
		WHERE opc_nombre_object = @page

		SELECT *, CASE WHEN opc_id = @idact THEN 1 ELSE 0 END as opc_selected
		FROM opc_opcion o
		WHERE opc_deshabilitado_sistema = 0
	      AND opc_padre IS NULL
		ORDER BY opc_orden
	END

	IF @tipo = 'H'
	BEGIN
		SELECT * 
		FROM opc_opcion o
		WHERE opc_deshabilitado_sistema = 0
		  AND opc_padre = @padre
		ORDER BY opc_orden
	END

END
GO
/****** Object:  StoredProcedure [dbo].[GET_Opciones]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GET_Opciones]
	@id bigint = null
AS
BEGIN
  SET NOCOUNT ON;

		DECLARE 
			@opc_id bigint,
			@opc_nombre varchar(40), 
			@opc_nombre_modulo varchar(200), 
			@opc_descripcion varchar(4000), 
			@opc_nombre_object varchar(100),
			@opc_logo varchar(100),
			@opc_orden bigint,
			@opc_padre bigint,
			@opc_deshabilitado_sistema int,
			@opc_secuencia varchar(100),
			@opc_rank as bigint,
			@rowid bigint

			-- DROP TABLE #opciones

			-- Crea la tabla temporal
			CREATE TABLE #opciones (
				[opc_id] [bigint] NOT NULL,
				[opc_nombre] [varchar](40) NULL,
				[opc_nombre_modulo] [varchar](200) NULL,
				[opc_descripcion] [varchar](4000) NULL,
				[opc_nombre_object] [varchar](100) NULL,
				[opc_logo] [varchar](100) NULL,
				[opc_orden] [bigint] NULL,
				[opc_padre] [int] NULL,
				[opc_deshabilitado_sistema] [int] NULL,
				[opc_secuencia] [varchar](40) NULL,
				[rowid] [bigint] NOT NULL,
			)

		DECLARE cursor1 CURSOR
		FOR 
			SELECT opc_id, opc_nombre, opc_nombre_modulo, opc_descripcion, opc_nombre_object, opc_logo, opc_orden, opc_padre, opc_deshabilitado_sistema, ROW_NUMBER() OVER (ORDER BY opc_orden) as opc_rank
			FROM opc_opcion
			WHERE opc_padre IS NULL
			ORDER BY opc_orden
		OPEN cursor1;

		FETCH NEXT FROM cursor1 INTO @opc_id, @opc_nombre, @opc_nombre_modulo, @opc_descripcion, @opc_nombre_object, @opc_logo, @opc_orden, @opc_padre, @opc_deshabilitado_sistema, @rowid, @opc_rank;

		WHILE @@FETCH_STATUS = 0
			BEGIN

				SET @opc_secuencia = CAST(@opc_rank as VARCHAR) + '.0' 

				INSERT INTO #opciones 
				VALUES (@opc_id, @opc_nombre, @opc_nombre_modulo, @opc_descripcion, @opc_nombre_object, @opc_logo, @opc_orden, @opc_padre, @opc_deshabilitado_sistema, @opc_secuencia, @rowid)

				INSERT INTO #opciones
				SELECT opc_id, opc_nombre, opc_nombre_modulo, opc_descripcion, opc_nombre_object, opc_logo, opc_orden, opc_padre, opc_deshabilitado_sistema, CAST(@opc_rank as VARCHAR) + '.' + CAST(ROW_NUMBER() OVER (ORDER BY opc_orden) as VARCHAR) as opc_rank
				FROM opc_opcion
				WHERE opc_padre = @opc_id
				ORDER BY opc_orden
        
				FETCH NEXT FROM cursor1 INTO @opc_id, @opc_nombre, @opc_nombre_modulo, @opc_descripcion, @opc_nombre_object, @opc_logo, @opc_orden, @opc_padre, @opc_deshabilitado_sistema, @rowid, @opc_rank;
			END;

		CLOSE cursor1;
		DEALLOCATE cursor1;

		IF @id IS NULL
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY opc_orden) as opc_rank, * 
			FROM #opciones
			ORDER BY 1
		END
		ELSE
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY opc_orden) as opc_rank, o.*, r.rol_id
			FROM #opciones o
			LEFT OUTER JOIN oro_opcion_rol r ON r.opc_id = o.opc_id AND r.rol_id = @id
			ORDER BY opc_secuencia
		END


END
GO
/****** Object:  StoredProcedure [dbo].[GET_RowCount]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GET_RowCount]
	@tabla VARCHAR(100),
	@campo VARCHAR(100) = null
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sql nvarchar(500);  
	DECLARE @id nvarchar(100);
	 
	SET @sql = 'SELECT count(1) as value FROM ' + @tabla + '';  
	EXECUTE sp_executesql @sql;

END
GO
/****** Object:  StoredProcedure [dbo].[GET_TrxID]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GET_TrxID]
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sql nvarchar(500); 
	DECLARE @id uniqueidentifier;


	-- Depura la tabla de transacciones
	DELETE FROM tri_transaccion_identificador
	WHERE fecha <= GETDATE() - 1

	-- Obtiene el nuevo ID
	SELECT @id = NEWID() 

	-- Inserta sobre la tabla de transacciones
	INSERT INTO tri_transaccion_identificador(id) VALUES (@id)

	-- Selecciona el ID
	SELECT @id as id;


END




GO
/****** Object:  StoredProcedure [dbo].[GET_ValorParametro]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GET_ValorParametro]
	@tipo INT
AS
BEGIN
	SET NOCOUNT ON;
	 
	SELECT *
	FROM par_parametro
	WHERE par_tipo = @tipo
	ORDER BY par_nombre

END
GO
/****** Object:  StoredProcedure [dbo].[VAL_TrxID]    Script Date: 23/07/2022 07:14:20 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[VAL_TrxID]
	@guid varchar(36) = ''
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @id uniqueidentifier

	IF DATALENGTH(@guid) > 0
	BEGIN
		SELECT CASE WHEN COUNT(1) >= 1 THEN 1 ELSE 0 END as val
		FROM tri_transaccion_identificador
		WHERE id = @guid
	END
	ELSE
	BEGIN
		SELECT 0 as val
	END

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id del Usuario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'usu_usuarios', @level2type=N'COLUMN',@level2name=N'usu_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[12] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "per_periodo"
            Begin Extent = 
               Top = 101
               Left = 326
               Bottom = 241
               Right = 496
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "v_retiros"
            Begin Extent = 
               Top = 22
               Left = 55
               Bottom = 135
               Right = 225
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "v_asignaciones"
            Begin Extent = 
               Top = 13
               Left = 583
               Bottom = 126
               Right = 753
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_actividad_mes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_actividad_mes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[12] 2[12] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "aud_auditoria"
            Begin Extent = 
               Top = 10
               Left = 88
               Bottom = 311
               Right = 289
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 16
         Width = 284
         Width = 990
         Width = 435
         Width = 465
         Width = 1500
         Width = 3165
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_asignaciones'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_asignaciones'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[13] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "aud_auditoria"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 326
               Right = 239
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "con_confirmacion"
            Begin Extent = 
               Top = 70
               Left = 340
               Bottom = 194
               Right = 523
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_auditoria'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_auditoria'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[15] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "act_activo"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 306
               Right = 243
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "din_detalle_inventario"
            Begin Extent = 
               Top = 35
               Left = 399
               Bottom = 275
               Right = 576
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "inv_inventario"
            Begin Extent = 
               Top = 26
               Left = 750
               Bottom = 225
               Right = 920
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 17
         Width = 284
         Width = 2100
         Width = 1500
         Width = 1500
         Width = 2295
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inventario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inventario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "i"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 153
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 6
               Left = 262
               Bottom = 208
               Right = 455
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t"
            Begin Extent = 
               Top = 6
               Left = 752
               Bottom = 119
               Right = 938
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 493
               Bottom = 136
               Right = 714
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 3150
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inventario_actual'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inventario_actual'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 243
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "e"
            Begin Extent = 
               Top = 6
               Left = 281
               Bottom = 136
               Right = 451
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t"
            Begin Extent = 
               Top = 6
               Left = 489
               Bottom = 119
               Right = 659
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 2340
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inventario_alerta'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inventario_alerta'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[13] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "v_inventario_stock"
            Begin Extent = 
               Top = 23
               Left = 46
               Bottom = 127
               Right = 216
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "v_inventario_actual"
            Begin Extent = 
               Top = 23
               Left = 298
               Bottom = 171
               Right = 468
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 2475
         Width = 1500
         Width = 2010
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inventario_concilia'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inventario_concilia'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[16] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 243
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "e"
            Begin Extent = 
               Top = 6
               Left = 281
               Bottom = 136
               Right = 451
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t"
            Begin Extent = 
               Top = 6
               Left = 489
               Bottom = 119
               Right = 659
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inventario_stock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inventario_stock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[12] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tac_tipo_activo"
            Begin Extent = 
               Top = 56
               Left = 841
               Bottom = 173
               Right = 1011
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "act_activo"
            Begin Extent = 
               Top = 11
               Left = 575
               Bottom = 314
               Right = 780
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "est_estado"
            Begin Extent = 
               Top = 59
               Left = 283
               Bottom = 189
               Right = 453
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 2220
         Width = 1950
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inventario_tipo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_inventario_tipo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[11] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "aud_auditoria"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 255
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 435
         Width = 465
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_retiros'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_retiros'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[16] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = -30
      End
      Begin Tables = 
         Begin Table = "aud_auditoria"
            Begin Extent = 
               Top = 18
               Left = 166
               Bottom = 320
               Right = 367
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "act_activo"
            Begin Extent = 
               Top = 53
               Left = 479
               Bottom = 256
               Right = 684
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tac_tipo_activo"
            Begin Extent = 
               Top = 6
               Left = 722
               Bottom = 119
               Right = 892
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 15
         Width = 284
         Width = 2340
         Width = 1500
         Width = 2100
         Width = 2010
         Width = 1500
         Width = 2205
         Width = 4380
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2100
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_ultimas_asignaciones'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_ultimas_asignaciones'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[13] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "aud_auditoria"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 239
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "act_activo"
            Begin Extent = 
               Top = 6
               Left = 277
               Bottom = 136
               Right = 482
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tac_tipo_activo"
            Begin Extent = 
               Top = 6
               Left = 520
               Bottom = 119
               Right = 690
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2565
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 3735
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_ultimos_retiros'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_ultimos_retiros'
GO
USE [master]
GO
ALTER DATABASE [itmanager] SET  READ_WRITE 
GO
