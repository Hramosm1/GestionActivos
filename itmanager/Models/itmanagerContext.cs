using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace itmanager.Models
{
    public partial class itmanagerContext : DbContext
    {

        public itmanagerContext(DbContextOptions<itmanagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AceActivoEmpleado> AceActivoEmpleados { get; set; } = null!;
        public virtual DbSet<ActActivo> ActActivos { get; set; } = null!;
        public virtual DbSet<AcuActivoUsuario> AcuActivoUsuarios { get; set; } = null!;
        public virtual DbSet<AudAuditorium> AudAuditoria { get; set; } = null!;
        public virtual DbSet<ConConfirmacion> ConConfirmacions { get; set; } = null!;
        public virtual DbSet<DdDiscoDuro> DdDiscoDuros { get; set; } = null!;
        public virtual DbSet<DinDetalleInventario> DinDetalleInventarios { get; set; } = null!;
        public virtual DbSet<EmpEmpleado> EmpEmpleados { get; set; } = null!;
        public virtual DbSet<EstEstado> EstEstados { get; set; } = null!;
        public virtual DbSet<HwHardwareSpec> HwHardwareSpecs { get; set; } = null!;
        public virtual DbSet<InvInventario> InvInventarios { get; set; } = null!;
        public virtual DbSet<OpcOpcion> OpcOpcions { get; set; } = null!;
        public virtual DbSet<OroOpcionRol> OroOpcionRols { get; set; } = null!;
        public virtual DbSet<ParParametro> ParParametros { get; set; } = null!;
        public virtual DbSet<PerPeriodo> PerPeriodos { get; set; } = null!;
        public virtual DbSet<RolRole> RolRoles { get; set; } = null!;
        public virtual DbSet<SisSistema> SisSistemas { get; set; } = null!;
        public virtual DbSet<TacTipoActivo> TacTipoActivos { get; set; } = null!;
        public virtual DbSet<TddTipoDisco> TddTipoDiscos { get; set; } = null!;
        public virtual DbSet<TfaTipoFabricante> TfaTipoFabricantes { get; set; } = null!;
        public virtual DbSet<TipTipoParametro> TipTipoParametros { get; set; } = null!;
        public virtual DbSet<TopTipoOpcion> TopTipoOpcions { get; set; } = null!;
        public virtual DbSet<TprTipoProcesador> TprTipoProcesadors { get; set; } = null!;
        public virtual DbSet<TriTransaccionIdentificador> TriTransaccionIdentificadors { get; set; } = null!;
        public virtual DbSet<UroUsuarioRol> UroUsuarioRols { get; set; } = null!;
        public virtual DbSet<UsuUsuario> UsuUsuarios { get; set; } = null!;
        public virtual DbSet<VActividadMe> VActividadMes { get; set; } = null!;
        public virtual DbSet<VAsignacione> VAsignaciones { get; set; } = null!;
        public virtual DbSet<VAuditorium> VAuditoria { get; set; } = null!;
        public virtual DbSet<VInventario> VInventarios { get; set; } = null!;
        public virtual DbSet<VInventarioActual> VInventarioActuals { get; set; } = null!;
        public virtual DbSet<VInventarioAlertum> VInventarioAlerta { get; set; } = null!;
        public virtual DbSet<VInventarioConcilium> VInventarioConcilia { get; set; } = null!;
        public virtual DbSet<VInventarioStock> VInventarioStocks { get; set; } = null!;
        public virtual DbSet<VInventarioTipo> VInventarioTipos { get; set; } = null!;
        public virtual DbSet<VRetiro> VRetiros { get; set; } = null!;
        public virtual DbSet<VUltimasAsignacione> VUltimasAsignaciones { get; set; } = null!;
        public virtual DbSet<VUltimosRetiro> VUltimosRetiros { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=linux;Initial Catalog=itmanager;User Id=sa;Password=Cde456tr#");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Modern_Spanish_CI_AS");

            modelBuilder.Entity<AceActivoEmpleado>(entity =>
            {
                entity.HasKey(e => new { e.EmpId, e.ActId });

                entity.ToTable("ace_activo_empleado");

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.ActId).HasColumnName("act_id");

                entity.Property(e => e.AceFecha)
                    .HasColumnType("date")
                    .HasColumnName("ace_fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AceTipoAsignacion)
                    .HasColumnName("ace_tipo_asignacion")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Act)
                    .WithMany(p => p.AceActivoEmpleados)
                    .HasForeignKey(d => d.ActId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ace_activo_empleado_act_activo");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.AceActivoEmpleados)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ace_activo_empleado_emp_empleados");
            });

            modelBuilder.Entity<ActActivo>(entity =>
            {
                entity.HasKey(e => e.ActId);

                entity.ToTable("act_activo");

                entity.Property(e => e.ActId).HasColumnName("act_id");

                entity.Property(e => e.ActCondicion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("act_condicion");

                entity.Property(e => e.ActDadodebaja).HasColumnName("act_dadodebaja");

                entity.Property(e => e.ActDdbCausal)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("act_ddb_causal");

                entity.Property(e => e.ActDdbComentario)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("act_ddb_comentario");

                entity.Property(e => e.ActEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("act_estado")
                    .HasDefaultValueSql("('almacenado')");

                entity.Property(e => e.ActFechacompra)
                    .HasColumnType("date")
                    .HasColumnName("act_fechacompra");

                entity.Property(e => e.ActImagen1)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("act_imagen1");

                entity.Property(e => e.ActImagen2)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("act_imagen2");

                entity.Property(e => e.ActInactivo).HasColumnName("act_inactivo");

                entity.Property(e => e.ActLicencia)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("act_licencia");

                entity.Property(e => e.ActModelo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("act_modelo");

                entity.Property(e => e.ActNrocompra)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("act_nrocompra");

                entity.Property(e => e.ActObservaciones)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("act_observaciones");

                entity.Property(e => e.ActProveedorcompra)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("act_proveedorcompra");

                entity.Property(e => e.ActSerie)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("act_serie");

                entity.Property(e => e.ActUid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("act_uid")
                    .HasDefaultValueSql("(left(newid(),(8)))");

                entity.Property(e => e.TacId).HasColumnName("tac_id");

                entity.Property(e => e.TfaId).HasColumnName("tfa_id");

                entity.HasOne(d => d.Tac)
                    .WithMany(p => p.ActActivos)
                    .HasForeignKey(d => d.TacId)
                    .HasConstraintName("FK_act_activo_tac_tipo_activo");

                entity.HasOne(d => d.Tfa)
                    .WithMany(p => p.ActActivos)
                    .HasForeignKey(d => d.TfaId)
                    .HasConstraintName("FK_act_activo_tfa_tipo_fabricante");
            });

            modelBuilder.Entity<AcuActivoUsuario>(entity =>
            {
                entity.HasKey(e => new { e.UsuId, e.ActId });

                entity.ToTable("acu_activo_usuario");

                entity.Property(e => e.UsuId).HasColumnName("usu_id");

                entity.Property(e => e.ActId).HasColumnName("act_id");

                entity.Property(e => e.AcuFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("acu_fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Act)
                    .WithMany(p => p.AcuActivoUsuarios)
                    .HasForeignKey(d => d.ActId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_acu_activo_usuario_act_activo");

                entity.HasOne(d => d.Usu)
                    .WithMany(p => p.AcuActivoUsuarios)
                    .HasForeignKey(d => d.UsuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_acu_activo_usuario_usu_usuarios");
            });

            modelBuilder.Entity<AudAuditorium>(entity =>
            {
                entity.HasKey(e => e.AudId);

                entity.ToTable("aud_auditoria");

                entity.Property(e => e.AudId).HasColumnName("aud_id");

                entity.Property(e => e.ActId).HasColumnName("act_id");

                entity.Property(e => e.AudAñomes)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("aud_añomes")
                    .HasComputedColumnSql("(concat(datepart(year,[aud_fecha]),'-',datepart(month,[aud_fecha])))", false);

                entity.Property(e => e.AudEstadoAnterior)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aud_estado_anterior");

                entity.Property(e => e.AudEstadoNuevo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aud_estado_nuevo");

                entity.Property(e => e.AudFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("aud_fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AudModelo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aud_modelo");

                entity.Property(e => e.AudSerie)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aud_serie");

                entity.Property(e => e.AudTipoPersona)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("aud_tipo_persona");

                entity.Property(e => e.AudUid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("aud_uid");

                entity.Property(e => e.ConId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("con_id");

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.EmpNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("emp_nombre");

                entity.Property(e => e.EmpTipoAsignacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emp_tipo_asignacion");

                entity.Property(e => e.UsuModificadoPor)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("usu_modificado_por");

                entity.HasOne(d => d.Act)
                    .WithMany(p => p.AudAuditoria)
                    .HasForeignKey(d => d.ActId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_aud_auditoria_act_activo");

                entity.HasOne(d => d.Con)
                    .WithMany(p => p.AudAuditoria)
                    .HasForeignKey(d => d.ConId)
                    .HasConstraintName("FK_aud_auditoria_con_confirmacion");
            });

            modelBuilder.Entity<ConConfirmacion>(entity =>
            {
                entity.HasKey(e => e.ConId);

                entity.ToTable("con_confirmacion");

                entity.Property(e => e.ConId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("con_id");

                entity.Property(e => e.ConFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("con_fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ConFirma)
                    .HasColumnType("text")
                    .HasColumnName("con_firma");
            });

            modelBuilder.Entity<DdDiscoDuro>(entity =>
            {
                entity.HasKey(e => e.DdId);

                entity.ToTable("dd_disco_duro");

                entity.Property(e => e.DdId).HasColumnName("dd_id");

                entity.Property(e => e.ActId).HasColumnName("act_id");

                entity.Property(e => e.DdCapacidad)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("dd_capacidad");

                entity.Property(e => e.TddId).HasColumnName("tdd_id");

                entity.HasOne(d => d.Act)
                    .WithMany(p => p.DdDiscoDuros)
                    .HasForeignKey(d => d.ActId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dd_disco_duro_act_activo");

                entity.HasOne(d => d.Tdd)
                    .WithMany(p => p.DdDiscoDuros)
                    .HasForeignKey(d => d.TddId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dd_disco_duro_tdd_tipo_disco");
            });

            modelBuilder.Entity<DinDetalleInventario>(entity =>
            {
                entity.HasKey(e => e.DinId);

                entity.ToTable("din_detalle_inventario");

                entity.Property(e => e.DinId).HasColumnName("din_id");

                entity.Property(e => e.ActId).HasColumnName("act_id");

                entity.Property(e => e.ActUid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("act_uid");

                entity.Property(e => e.DinComentarios)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("din_comentarios");

                entity.Property(e => e.DinEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("din_estado");

                entity.Property(e => e.DinFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("din_fecha");

                entity.Property(e => e.DinUsuario)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("din_usuario");

                entity.Property(e => e.InvId).HasColumnName("inv_id");

                entity.HasOne(d => d.Act)
                    .WithMany(p => p.DinDetalleInventarios)
                    .HasForeignKey(d => d.ActId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_din_detalle_inventario_act_activo");

                entity.HasOne(d => d.Inv)
                    .WithMany(p => p.DinDetalleInventarios)
                    .HasForeignKey(d => d.InvId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_din_detalle_inventario_inv_inventario");
            });

            modelBuilder.Entity<EmpEmpleado>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.ToTable("emp_empleados");

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.EmpArea)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("emp_area");

                entity.Property(e => e.EmpCargo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("emp_cargo");

                entity.Property(e => e.EmpCorreo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("emp_correo");

                entity.Property(e => e.EmpEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("emp_estado");

                entity.Property(e => e.EmpExtension)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("emp_extension");

                entity.Property(e => e.EmpImagen)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("emp_imagen");

                entity.Property(e => e.EmpNombre)
                    .HasMaxLength(220)
                    .IsUnicode(false)
                    .HasColumnName("emp_nombre");
            });

            modelBuilder.Entity<EstEstado>(entity =>
            {
                entity.HasKey(e => e.EstEstado1)
                    .HasName("PK_ges_grupo_estado");

                entity.ToTable("est_estado");

                entity.Property(e => e.EstEstado1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("est_estado");

                entity.Property(e => e.EstAccAsigna)
                    .HasColumnName("est_acc_asigna")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EstGrupo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("est_grupo");

                entity.Property(e => e.EstInactivo).HasColumnName("est_inactivo");
            });

            modelBuilder.Entity<HwHardwareSpec>(entity =>
            {
                entity.HasKey(e => e.HwId);

                entity.ToTable("hw_hardware_specs");

                entity.Property(e => e.HwId).HasColumnName("hw_id");

                entity.Property(e => e.AtcId).HasColumnName("atc_id");

                entity.Property(e => e.HwGeneracionproc)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("hw_generacionproc");

                entity.Property(e => e.HwMemoria)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("hw_memoria");

                entity.Property(e => e.HwModeloproc)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("hw_modeloproc");

                entity.Property(e => e.TprId).HasColumnName("tpr_id");

                entity.HasOne(d => d.Atc)
                    .WithMany(p => p.HwHardwareSpecs)
                    .HasForeignKey(d => d.AtcId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_hw_hardware_specs_act_activo");

                entity.HasOne(d => d.Tpr)
                    .WithMany(p => p.HwHardwareSpecs)
                    .HasForeignKey(d => d.TprId)
                    .HasConstraintName("FK_hw_hardware_specs_tpr_tipo_procesador");
            });

            modelBuilder.Entity<InvInventario>(entity =>
            {
                entity.HasKey(e => e.InvId);

                entity.ToTable("inv_inventario");

                entity.Property(e => e.InvId).HasColumnName("inv_id");

                entity.Property(e => e.InvDate)
                    .HasColumnType("date")
                    .HasColumnName("inv_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InvFin)
                    .HasColumnType("date")
                    .HasColumnName("inv_fin");

                entity.Property(e => e.InvInicio)
                    .HasColumnType("date")
                    .HasColumnName("inv_inicio");

                entity.Property(e => e.InvNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("inv_nombre");
            });

            modelBuilder.Entity<OpcOpcion>(entity =>
            {
                entity.HasKey(e => e.OpcId);

                entity.ToTable("opc_opcion");

                entity.Property(e => e.OpcId).HasColumnName("opc_id");

                entity.Property(e => e.OpcDescripcion)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("opc_descripcion");

                entity.Property(e => e.OpcDeshabilitadoSistema)
                    .HasColumnName("opc_deshabilitado_sistema")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OpcLogo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("opc_logo");

                entity.Property(e => e.OpcNombre)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("opc_nombre");

                entity.Property(e => e.OpcNombreModulo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("opc_nombre_modulo");

                entity.Property(e => e.OpcNombreObject)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("opc_nombre_object");

                entity.Property(e => e.OpcOrden).HasColumnName("opc_orden");

                entity.Property(e => e.OpcPadre).HasColumnName("opc_padre");
            });

            modelBuilder.Entity<OroOpcionRol>(entity =>
            {
                entity.HasKey(e => new { e.RolId, e.OpcId });

                entity.ToTable("oro_opcion_rol");

                entity.Property(e => e.RolId).HasColumnName("rol_id");

                entity.Property(e => e.OpcId).HasColumnName("opc_id");

                entity.Property(e => e.OroDate)
                    .HasColumnType("datetime")
                    .HasColumnName("oro_date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Opc)
                    .WithMany(p => p.OroOpcionRols)
                    .HasForeignKey(d => d.OpcId)
                    .HasConstraintName("FK_oro_opcion_rol_opc_opcion");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.OroOpcionRols)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_oro_opcion_rol_rol_roles");
            });

            modelBuilder.Entity<ParParametro>(entity =>
            {
                entity.HasKey(e => e.ParId);

                entity.ToTable("par_parametro");

                entity.Property(e => e.ParId).HasColumnName("par_id");

                entity.Property(e => e.ParNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("par_nombre");

                entity.Property(e => e.ParTipo).HasColumnName("par_tipo");

                entity.Property(e => e.ParValor1)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("par_valor1");

                entity.Property(e => e.ParValor2)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("par_valor2");

                entity.HasOne(d => d.ParTipoNavigation)
                    .WithMany(p => p.ParParametros)
                    .HasForeignKey(d => d.ParTipo)
                    .HasConstraintName("FK_par_parametro_tip_tipo_parametro");
            });

            modelBuilder.Entity<PerPeriodo>(entity =>
            {
                entity.HasKey(e => e.Fecha);

                entity.ToTable("per_periodo");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Año).HasColumnName("año");

                entity.Property(e => e.Dia).HasColumnName("dia");

                entity.Property(e => e.Mes).HasColumnName("mes");
            });

            modelBuilder.Entity<RolRole>(entity =>
            {
                entity.HasKey(e => e.RolId);

                entity.ToTable("rol_roles");

                entity.Property(e => e.RolId).HasColumnName("rol_id");

                entity.Property(e => e.RolBloqueado).HasColumnName("rol_bloqueado");

                entity.Property(e => e.RolDescripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("rol_descripcion");

                entity.Property(e => e.RolNombre)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("rol_nombre");
            });

            modelBuilder.Entity<SisSistema>(entity =>
            {
                entity.HasKey(e => e.SisId);

                entity.ToTable("sis_sistema");

                entity.Property(e => e.SisId).HasColumnName("sis_id");

                entity.Property(e => e.SisDescripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("sis_descripcion");

                entity.Property(e => e.SisNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("sis_nombre");

                entity.Property(e => e.SisProceso)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("sis_proceso");

                entity.Property(e => e.SisTipo).HasColumnName("sis_tipo");
            });

            modelBuilder.Entity<TacTipoActivo>(entity =>
            {
                entity.HasKey(e => e.TacId);

                entity.ToTable("tac_tipo_activo");

                entity.Property(e => e.TacId).HasColumnName("tac_id");

                entity.Property(e => e.TacInvMin).HasColumnName("tac_inv_min");

                entity.Property(e => e.TacNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tac_nombre");
            });

            modelBuilder.Entity<TddTipoDisco>(entity =>
            {
                entity.HasKey(e => e.TddId);

                entity.ToTable("tdd_tipo_disco");

                entity.Property(e => e.TddId).HasColumnName("tdd_id");

                entity.Property(e => e.TddNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("tdd_nombre");
            });

            modelBuilder.Entity<TfaTipoFabricante>(entity =>
            {
                entity.HasKey(e => e.TfaId);

                entity.ToTable("tfa_tipo_fabricante");

                entity.Property(e => e.TfaId).HasColumnName("tfa_id");

                entity.Property(e => e.TfaNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("tfa_nombre");
            });

            modelBuilder.Entity<TipTipoParametro>(entity =>
            {
                entity.HasKey(e => e.TipId);

                entity.ToTable("tip_tipo_parametro");

                entity.Property(e => e.TipId).HasColumnName("tip_id");

                entity.Property(e => e.TipNombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tip_nombre");

                entity.Property(e => e.TipOrden).HasColumnName("tip_orden");
            });

            modelBuilder.Entity<TopTipoOpcion>(entity =>
            {
                entity.HasKey(e => e.TopId);

                entity.ToTable("top_tipo_opcion");

                entity.Property(e => e.TopId).HasColumnName("top_id");

                entity.Property(e => e.TopNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("top_nombre");

                entity.Property(e => e.TopOrden).HasColumnName("top_orden");
            });

            modelBuilder.Entity<TprTipoProcesador>(entity =>
            {
                entity.HasKey(e => e.TprId);

                entity.ToTable("tpr_tipo_procesador");

                entity.Property(e => e.TprId).HasColumnName("tpr_id");

                entity.Property(e => e.TprImagen)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("tpr_imagen");

                entity.Property(e => e.TprNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("tpr_nombre");
            });

            modelBuilder.Entity<TriTransaccionIdentificador>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tri_transaccion_identificador");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<UroUsuarioRol>(entity =>
            {
                entity.HasKey(e => new { e.UsuId, e.RolId });

                entity.ToTable("uro_usuario_rol");

                entity.Property(e => e.UsuId).HasColumnName("usu_id");

                entity.Property(e => e.RolId).HasColumnName("rol_id");

                entity.Property(e => e.UroDate)
                    .HasColumnType("datetime")
                    .HasColumnName("uro_date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.UroUsuarioRols)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_uro_usuario_rol_rol_roles");

                entity.HasOne(d => d.Usu)
                    .WithMany(p => p.UroUsuarioRols)
                    .HasForeignKey(d => d.UsuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_uro_usuario_rol_usu_usuarios");
            });

            modelBuilder.Entity<UsuUsuario>(entity =>
            {
                entity.HasKey(e => e.UsuId);

                entity.ToTable("usu_usuarios");

                entity.Property(e => e.UsuId)
                    .HasColumnName("usu_id")
                    .HasComment("Id del Usuario");

                entity.Property(e => e.UsuAdmin).HasColumnName("usu_admin");

                entity.Property(e => e.UsuAsignaActivos).HasColumnName("usu_asigna_activos");

                entity.Property(e => e.UsuBloqueado).HasColumnName("usu_bloqueado");

                entity.Property(e => e.UsuClave)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("usu_clave");

                entity.Property(e => e.UsuFechaFinal)
                    .HasColumnType("datetime")
                    .HasColumnName("usu_fecha_final")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuImagen)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usu_imagen");

                entity.Property(e => e.UsuLogin)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("usu_login");

                entity.Property(e => e.UsuNombre)
                    .HasMaxLength(90)
                    .IsUnicode(false)
                    .HasColumnName("usu_nombre");
            });

            modelBuilder.Entity<VActividadMe>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_actividad_mes");

                entity.Property(e => e.Asignaciones).HasColumnName("asignaciones");

                entity.Property(e => e.Dia).HasColumnName("dia");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Retiros).HasColumnName("retiros");
            });

            modelBuilder.Entity<VAsignacione>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_asignaciones");

                entity.Property(e => e.Dia).HasColumnName("dia");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Nro).HasColumnName("nro");
            });

            modelBuilder.Entity<VAuditorium>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_auditoria");

                entity.Property(e => e.ActId).HasColumnName("act_id");

                entity.Property(e => e.AudAñomes)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("aud_añomes");

                entity.Property(e => e.AudEstadoAnterior)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aud_estado_anterior");

                entity.Property(e => e.AudEstadoNuevo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aud_estado_nuevo");

                entity.Property(e => e.AudFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("aud_fecha");

                entity.Property(e => e.AudModelo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aud_modelo");

                entity.Property(e => e.AudSerie)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aud_serie");

                entity.Property(e => e.AudUid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("aud_uid");

                entity.Property(e => e.ConFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("con_fecha");

                entity.Property(e => e.ConId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("con_id");

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.EmpNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("emp_nombre");

                entity.Property(e => e.UsuModificadoPor)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("usu_modificado_por");
            });

            modelBuilder.Entity<VInventario>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_inventario");

                entity.Property(e => e.ActCondicion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("act_condicion");

                entity.Property(e => e.ActEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("act_estado");

                entity.Property(e => e.ActImagen1)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("act_imagen1");

                entity.Property(e => e.ActImagen2)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("act_imagen2");

                entity.Property(e => e.ActLicencia)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("act_licencia");

                entity.Property(e => e.ActModelo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("act_modelo");

                entity.Property(e => e.ActSerie)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("act_serie");

                entity.Property(e => e.DinComentarios)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("din_comentarios");

                entity.Property(e => e.DinEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("din_estado");

                entity.Property(e => e.DinFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("din_fecha");

                entity.Property(e => e.DinId).HasColumnName("din_id");

                entity.Property(e => e.DinUsuario)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("din_usuario");

                entity.Property(e => e.InvFin)
                    .HasColumnType("date")
                    .HasColumnName("inv_fin");

                entity.Property(e => e.InvId).HasColumnName("inv_id");

                entity.Property(e => e.InvInicio)
                    .HasColumnType("date")
                    .HasColumnName("inv_inicio");

                entity.Property(e => e.InvNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("inv_nombre");
            });

            modelBuilder.Entity<VInventarioActual>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_inventario_actual");

                entity.Property(e => e.Lastdate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastdate");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.TacNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tac_nombre");
            });

            modelBuilder.Entity<VInventarioAlertum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_inventario_alerta");

                entity.Property(e => e.Msg)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("msg");

                entity.Property(e => e.Perc).HasColumnName("perc");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.TacInvMin).HasColumnName("tac_inv_min");

                entity.Property(e => e.TacNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tac_nombre");
            });

            modelBuilder.Entity<VInventarioConcilium>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_inventario_concilia");

                entity.Property(e => e.Diff).HasColumnName("diff");

                entity.Property(e => e.Fisico).HasColumnName("fisico");

                entity.Property(e => e.Lastdate)
                    .HasColumnType("date")
                    .HasColumnName("lastdate");

                entity.Property(e => e.Perc).HasColumnName("perc");

                entity.Property(e => e.TacNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tac_nombre");

                entity.Property(e => e.Teorico).HasColumnName("teorico");
            });

            modelBuilder.Entity<VInventarioStock>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_inventario_stock");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.TacNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tac_nombre");
            });

            modelBuilder.Entity<VInventarioTipo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_inventario_tipo");

                entity.Property(e => e.ActEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("act_estado");

                entity.Property(e => e.ActTotal).HasColumnName("act_total");

                entity.Property(e => e.EstGrupo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("est_grupo");

                entity.Property(e => e.TacNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tac_nombre");
            });

            modelBuilder.Entity<VRetiro>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_retiros");

                entity.Property(e => e.Dia).HasColumnName("dia");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Nro).HasColumnName("nro");
            });

            modelBuilder.Entity<VUltimasAsignacione>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_ultimas_asignaciones");

                entity.Property(e => e.ActId).HasColumnName("act_id");

                entity.Property(e => e.AudEstadoAnterior)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aud_estado_anterior");

                entity.Property(e => e.AudEstadoNuevo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aud_estado_nuevo");

                entity.Property(e => e.AudFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("aud_fecha");

                entity.Property(e => e.AudModelo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aud_modelo");

                entity.Property(e => e.AudSerie)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aud_serie");

                entity.Property(e => e.AudTipoPersona)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("aud_tipo_persona");

                entity.Property(e => e.AudUid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("aud_uid");

                entity.Property(e => e.Diffd).HasColumnName("diffd");

                entity.Property(e => e.Diffh).HasColumnName("diffh");

                entity.Property(e => e.Diffm).HasColumnName("diffm");

                entity.Property(e => e.EmpNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("emp_nombre");

                entity.Property(e => e.EmpTipoAsignacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emp_tipo_asignacion");

                entity.Property(e => e.TacNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tac_nombre");

                entity.Property(e => e.Tiempo)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("tiempo");
            });

            modelBuilder.Entity<VUltimosRetiro>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_ultimos_retiros");

                entity.Property(e => e.ActId).HasColumnName("act_id");

                entity.Property(e => e.AudEstadoAnterior)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aud_estado_anterior");

                entity.Property(e => e.AudEstadoNuevo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aud_estado_nuevo");

                entity.Property(e => e.AudFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("aud_fecha");

                entity.Property(e => e.AudModelo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aud_modelo");

                entity.Property(e => e.AudSerie)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aud_serie");

                entity.Property(e => e.AudTipoPersona)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("aud_tipo_persona");

                entity.Property(e => e.AudUid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("aud_uid");

                entity.Property(e => e.Diffd).HasColumnName("diffd");

                entity.Property(e => e.Diffh).HasColumnName("diffh");

                entity.Property(e => e.Diffm).HasColumnName("diffm");

                entity.Property(e => e.EmpNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("emp_nombre");

                entity.Property(e => e.EmpTipoAsignacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emp_tipo_asignacion");

                entity.Property(e => e.TacNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tac_nombre");

                entity.Property(e => e.Tiempo)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("tiempo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
