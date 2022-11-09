USE [itmanager]
GO
/****** Object:  Table [dbo].[opc_opcion]    Script Date: 14/08/2022 03:17:51 p. m. ******/
/*SET ANSI_NULLS ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO*/
SET IDENTITY_INSERT [dbo].[opc_opcion] ON 
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (1, N'Administración de Sistema', N'Administración de Sistema', N'Parametrizacion del sistema', N'', N'', 6, NULL, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (2, N'Roles', N'Administración de roles', N'Un rol es una colección de permisos definida para todo el sistema que Usted puede asignar a usuarios específicos en contextos específicos. La combinación de roles y contexto definen la habilidad de un usuario específico para hacer algo en alguna página. ', N'/Roles', N'adm_roles.jpg', 1, 1, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (3, N'Usuarios', N'Administración de usuarios', N'Permite la administración de los usuarios del sistema, permite definir las cuentas de administrador la cual permite realizar cambios que afectan a otros usuarios. Los administradores pueden cambiar la configuración de seguridad y obtener acceso a todos los modulos del sistema.', N'/Users', N'adm_usuarios.jpg', 2, 1, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (8, N'Activos', N'Activos', N'', N'/Active', N'distribucion_clientes.jpg', 1, 21, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (9, N'Tipos de Activo', N'Tipos de Activo', N'', N'/ActiveType', N'distribucion_canales.jpg', 2, 21, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (18, N'Reportes', N'Reportes', N'Reportes', N'', N'', 3, NULL, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (19, N'Reporte de auditoria', N'Reporte de auditoria', N'Reporte de auditoria', N'/Reports/Audit', N'rep_auditoria.jpg', 3, 18, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (21, N'Administración de Activos', N'Gestión de Inventarios', N'Administración de Riesgos', N'', N'', 5, NULL, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (22, N'Tipos de Disco', N'Tipos de Disco', N'Sirven para obtener una "primera vista" general, o panorama, de la distribución de la población, o de la muestra, respecto a una característica, cuantitativa y continua (como la longitud o el peso). De esta manera ofrece una visión de grupo permitiendo observar una preferencia, o tendencia, por parte de la muestra o población por ubicarse hacia una determinada región de valores dentro del espectro de valores posibles (sean infinitos o no) que pueda adquirir la característica. Así pues, podemos evidenciar comportamientos, observar el grado de homogeneidad, acuerdo o concisión entre los valores de todas las partes que componen la población o la muestra, o, en contraposición, poder observar el grado de variabilidad, y por ende, la dispersión de todos los valores que toman las partes, también es posible no evidenciar ninguna tendencia y obtener que cada miembro de la población toma por su lado y adquiere un valor de la característica aleatoriamente sin mostrar ninguna preferencia o tendencia, entre otras cosas', N'/DiscType', N'adm_rangos.jpg', 3, 21, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (24, N'Parametrización de Fabricantes', N'Parametrización de Fabricantes', N'Definición de Variables por Jurisdicción', N'/Manufacturers', N'adm_varjur.jpg', 6, 21, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (26, N'Opciones', N'Parametrización de opciones', N'El menú de opciones contiene una jerarquía de submenús desde los que puede iniciar las aplicaciones que están instaladas en el sistema. Cada submenú que se encuentra en la raíz permite incluir opciones o módulos. Por ejemplo, en el submenú Administración, encontrará módulos para administrar los usuarios, roles, sistema...', N'/Options', N'adm_opciones.jpg', 3, 1, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (28, N'Reporte de Inventarios', N'Reporte de Inventarios', N'', N'/Reports/Inventory', N'adm_report.jpg', 1, 18, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (36, N'Parametros', N'Parametros', N'Parametros', N'/Parameters', N'adm_parametros.jpg', 6, 1, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (37, N'Ajustes', N'Ajustes', N'Ajustes', N'/SystemConfig', N'adm_ajustes.jpg', 7, 1, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (38, N'Soporte', N'Soporte', N'Soporte', N'', N'', 4, NULL, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (52, N'Ficha Técnica del Activo', N'Ficha Técnica del Activo', N'Desde este reporte ...', N'/Management/ActivesList', N'alert_comparaciones.jpg', 13, 21, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (54, N'Captura de Inventario', N'Captura de Inventario', N'Esta función de alerta toma una columna y detalla cuando una contraparte es considerada como PEP, de acuerdo a las siguientes definiciones:<br/><br/>1) PEP de organizaciones internacionales:<br/>Son aquellas personas que ejercen funciones directivas en una organización internacional. Se entienden por PEP de organizaciones internacionales directores, subdirectores, miembros de juntas directivas o cualquier persona que ejerza una función equivalente.<br/>En ningún caso, dichas categorías comprenden funcionarios de niveles intermedios o inferiores. Adicionalmente, se consideran PEP de organizaciones internacionales durante el periodo en que ocupen sus cargos y durante los dos (2) años siguientes a su dejación, renuncia, despido, o cualquier otra forma de desvinculación.<br/><br/>2) PEP extranjeros:<br/>Son aquellas personas que desempeñan funciones públicas destacadas en otro país. Se entienden por PEP extranjeros: (i) jefes de Estado, jefes de Gobierno, ministros, subsecretarios o secretarios de Estado; (ii) congresistas o parlamentarios; (iii) miembros de tribunales supremos, tribunales constitucionales u otras altas instancias judiciales cuyas decisiones no admitan normalmente recurso, salvo en circunstancias excepcionales; (iv) miembros de tribunales o de las juntas directivas de bancos centrales; (v) embajadores, encargados de negocios y altos funcionarios de las fuerzas armadas, y (vi) miembros de los órganos administrativos, de gestión o de supervisión de empresas de propiedad estatal.<br/>En ningún caso, dichas categorías comprenden funcionarios de niveles intermedios o inferiores. Adicionalmente, se consideran PEP extranjeros durante el periodo en que ocupen sus cargos y durante los dos (2) años siguientes a su dejación, renuncia, despido, o cualquier otra forma de desvinculación.<br/><br/>', N'/Inventory', N'alert_comparaciones.jpg', 11, 58, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (56, N'Tipos de Procesador', N'Tipos de Procesador', N'Ejecución manual de alertas', N'/ProcessorType', N'adm_report.jpg', 4, 21, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (58, N'Gestión de Inventarios', NULL, NULL, N'', N'', 2, NULL, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (10058, N'Asignacion de Activos', N'Asignacion de Activos', N'Asignacion de Activos', N'/Support/SelectUser', NULL, 1, 38, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (10059, N'Mis Activos en Soporte (ADM)', N'Mis activos', N'Mis activos', N'/Support/MyAssets', NULL, 2, 38, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (10062, N'Dashboard', N'Dashboard', N'Dashboard', N'/Dashboard', NULL, 1, NULL, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (10063, N'Gestión de Activos', N'Gestión de Activos', N'Gestión de Activos', N'/Management', NULL, 1, 58, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (10064, N'Mis activos en Soporte', N'Mis activos', N'Mis activos', N'/Support/Actives', NULL, 3, 38, 0)
GO
INSERT [dbo].[opc_opcion] ([opc_id], [opc_nombre], [opc_nombre_modulo], [opc_descripcion], [opc_nombre_object], [opc_logo], [opc_orden], [opc_padre], [opc_deshabilitado_sistema]) VALUES (20064, N'Dados de Baja', N'Dados de Baja', N'Dado de baja', N'/Active/Discharged', NULL, 7, 21, 0)
GO
SET IDENTITY_INSERT [dbo].[opc_opcion] OFF
GO
ALTER TABLE [dbo].[opc_opcion] ADD  CONSTRAINT [DF__opc_opcio__OPC_D__398D8EEE]  DEFAULT ((0)) FOR [opc_deshabilitado_sistema]
GO
