USE [itmanager]
GO

/****** Object:  Table [dbo].[act_activo]    Script Date: 14/08/2022 03:15:55 p. m. ******/
/*
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
	[act_dadodebaja] [bit] NOT NULL,
	[tac_id] [bigint] NULL,
	[tfa_id] [bigint] NULL,
	[act_ddb_comentario] [varchar](400) NULL,
	[act_ddb_causal] [varchar](100) NULL,
 CONSTRAINT [PK_act_activo] PRIMARY KEY CLUSTERED 
(
	[act_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY]
GO
*/

ALTER TABLE act_activo ADD [act_ddb_comentario] [varchar](400) NULL,
	[act_ddb_causal] [varchar](100) NULL

ALTER TABLE [dbo].[act_activo] ADD  CONSTRAINT [DF_act_activo_act_estado]  DEFAULT ('almacenado') FOR [act_estado]
GO

ALTER TABLE [dbo].[act_activo] ADD  CONSTRAINT [DF_act_activo_act_inactivo]  DEFAULT ((0)) FOR [act_inactivo]
GO

ALTER TABLE [dbo].[act_activo] ADD  CONSTRAINT [DF__act_activ__act_u__7D8391DF]  DEFAULT (left(newid(),(8))) FOR [act_uid]
GO

ALTER TABLE [dbo].[act_activo] ADD  CONSTRAINT [DF_act_activo_act_dadodebaja]  DEFAULT ((0)) FOR [act_dadodebaja]
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


