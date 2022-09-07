using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebCRUDAPI.Datos
{
    public partial class lockersqaContext : DbContext
    {
        public lockersqaContext()
        {
        }

        public lockersqaContext(DbContextOptions<lockersqaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bodega> Bodegas { get; set; } = null!;
        public virtual DbSet<Casillero> Casilleros { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Comuna> Comunas { get; set; } = null!;
        public virtual DbSet<Locker> Lockers { get; set; } = null!;
        public virtual DbSet<Oficina> Oficinas { get; set; } = null!;
        public virtual DbSet<Pedido> Pedidos { get; set; } = null!;
        public virtual DbSet<Regione> Regiones { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Usuariooficina> Usuariooficinas { get; set; } = null!;
        public virtual DbSet<Zona> Zonas { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseMySql("server=40.118.215.145;port=3306;user=root;password=era-2401;database=lockersqa", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.30-mysql"));
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");

            modelBuilder.Entity<Bodega>(entity =>
            {
                entity.HasKey(e => e.IdBodega)
                    .HasName("PRIMARY");

                entity.ToTable("bodegas");

                entity.Property(e => e.IdBodega)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.SBodega)
                    .HasMaxLength(50)
                    .HasColumnName("sBodega");
            });

            modelBuilder.Entity<Casillero>(entity =>
            {
                entity.HasKey(e => new { e.IdLocker, e.IdOficina, e.IdCasillero })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("casilleros");

                entity.HasIndex(e => e.IdOficina, "FK_Casilleros_1_idx");

                entity.Property(e => e.IdLocker).HasColumnType("int(11)");

                entity.Property(e => e.IdOficina).HasColumnType("int(11)");

                entity.Property(e => e.IdCasillero).HasColumnType("int(11)");

                entity.Property(e => e.IAlto)
                    .HasColumnType("int(11)")
                    .HasColumnName("iAlto");

                entity.Property(e => e.IAncho)
                    .HasColumnType("int(11)")
                    .HasColumnName("iAncho");

                entity.Property(e => e.ILargo)
                    .HasColumnType("int(11)")
                    .HasColumnName("iLargo");

                entity.Property(e => e.SEstado)
                    .HasMaxLength(1)
                    .HasColumnName("sEstado")
                    .IsFixedLength();

                entity.Property(e => e.STipo)
                    .HasMaxLength(1)
                    .HasColumnName("sTipo")
                    .IsFixedLength();

                entity.HasOne(d => d.IdLockerNavigation)
                    .WithMany(p => p.Casilleros)
                    .HasForeignKey(d => d.IdLocker)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Casilleros_2");

                entity.HasOne(d => d.IdOficinaNavigation)
                    .WithMany(p => p.Casilleros)
                    .HasForeignKey(d => d.IdOficina)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Casilleros_1");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.SDniCli)
                    .HasName("PRIMARY");

                entity.ToTable("clientes");

                entity.Property(e => e.SDniCli)
                    .HasMaxLength(12)
                    .HasColumnName("sDniCli");

                entity.Property(e => e.SCorreo)
                    .HasMaxLength(50)
                    .HasColumnName("sCorreo");

                entity.Property(e => e.SNombre)
                    .HasMaxLength(50)
                    .HasColumnName("sNombre");

                entity.Property(e => e.STelefono)
                    .HasMaxLength(12)
                    .HasColumnName("sTelefono");
            });

            modelBuilder.Entity<Comuna>(entity =>
            {
                entity.HasKey(e => e.IdComuna)
                    .HasName("PRIMARY");

                entity.ToTable("comunas");

                entity.HasIndex(e => e.IdRegion, "FK_Comunas_1_idx");

                entity.Property(e => e.IdComuna)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("idComuna");

                entity.Property(e => e.IdRegion)
                    .HasColumnType("int(11)")
                    .HasColumnName("idRegion");

                entity.Property(e => e.SComuna)
                    .HasMaxLength(50)
                    .HasColumnName("sComuna");

                entity.HasOne(d => d.IdRegionNavigation)
                    .WithMany(p => p.Comunas)
                    .HasForeignKey(d => d.IdRegion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comunas_1");
            });

            modelBuilder.Entity<Locker>(entity =>
            {
                entity.HasKey(e => e.IdLocker)
                    .HasName("PRIMARY");

                entity.ToTable("lockers");

                entity.HasIndex(e => e.IdComuna, "FK_lockers_1_idx");

                entity.Property(e => e.IdLocker)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("idLocker");

                entity.Property(e => e.BHab).HasColumnName("bHab");

                entity.Property(e => e.IDohf)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iDOHF");

                entity.Property(e => e.IDohi)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iDOHI");

                entity.Property(e => e.IDomf)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iDOMF");

                entity.Property(e => e.IDomi)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iDOMI");

                entity.Property(e => e.IJuhf)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iJUHF");

                entity.Property(e => e.IJuhi)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iJUHI");

                entity.Property(e => e.IJumf)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iJUMF");

                entity.Property(e => e.IJumi)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iJUMI");

                entity.Property(e => e.ILuhf)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iLUHF");

                entity.Property(e => e.ILuhi)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iLUHI");

                entity.Property(e => e.ILumf)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iLUMF");

                entity.Property(e => e.ILumi)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iLUMI");

                entity.Property(e => e.IMahf)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iMAHF");

                entity.Property(e => e.IMahi)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iMAHI");

                entity.Property(e => e.IMamf)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iMAMF");

                entity.Property(e => e.IMami)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iMAMI");

                entity.Property(e => e.IMihf)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iMIHF");

                entity.Property(e => e.IMihi)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iMIHI");

                entity.Property(e => e.IMimf)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iMIMF");

                entity.Property(e => e.IMimi)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iMIMI");

                entity.Property(e => e.INumPuertas)
                    .HasColumnType("tinyint(3)")
                    .HasColumnName("iNumPuertas");

                entity.Property(e => e.ISahf)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iSAHF");

                entity.Property(e => e.ISahi)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iSAHI");

                entity.Property(e => e.ISamf)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iSAMF");

                entity.Property(e => e.ISami)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iSAMI");

                entity.Property(e => e.IVihf)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iVIHF");

                entity.Property(e => e.IVihi)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iVIHI");

                entity.Property(e => e.IVimf)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iVIMF");

                entity.Property(e => e.IVimi)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("iVIMI");

                entity.Property(e => e.IdComuna).HasColumnType("int(11)");

                entity.Property(e => e.SDireccion)
                    .HasMaxLength(100)
                    .HasColumnName("sDireccion");

                entity.Property(e => e.SLatitud)
                    .HasMaxLength(20)
                    .HasColumnName("sLatitud");

                entity.Property(e => e.SLongitud)
                    .HasMaxLength(20)
                    .HasColumnName("sLongitud");

                entity.Property(e => e.SNomLocker)
                    .HasMaxLength(50)
                    .HasColumnName("sNomLocker");

                entity.HasOne(d => d.IdComunaNavigation)
                    .WithMany(p => p.Lockers)
                    .HasForeignKey(d => d.IdComuna)
                    .HasConstraintName("FK_lockers_1");
            });

            modelBuilder.Entity<Oficina>(entity =>
            {
                entity.HasKey(e => e.IdOficina)
                    .HasName("PRIMARY");

                entity.ToTable("oficinas");

                entity.HasIndex(e => e.IdComuna, "FK_Oficinas_1_idx");

                entity.Property(e => e.IdOficina)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("idOficina");

                entity.Property(e => e.BEstado)
                    .IsRequired()
                    .HasColumnName("bEstado")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IHorVencCarga)
                    .HasColumnType("int(11)")
                    .HasColumnName("iHorVencCarga")
                    .HasDefaultValueSql("'24'");

                entity.Property(e => e.IHorVencRetiro)
                    .HasColumnType("int(11)")
                    .HasColumnName("iHorVencRetiro")
                    .HasDefaultValueSql("'24'");

                entity.Property(e => e.IdComuna)
                    .HasColumnType("int(11)")
                    .HasColumnName("idComuna");

                entity.Property(e => e.SCodOfi)
                    .HasMaxLength(10)
                    .HasColumnName("sCodOfi");

                entity.Property(e => e.SCorreo)
                    .HasMaxLength(50)
                    .HasColumnName("sCorreo");

                entity.Property(e => e.SOficina)
                    .HasMaxLength(50)
                    .HasColumnName("sOficina");

                entity.HasOne(d => d.IdComunaNavigation)
                    .WithMany(p => p.Oficinas)
                    .HasForeignKey(d => d.IdComuna)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Oficinas_1");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.Idpedido)
                    .HasName("PRIMARY");

                entity.ToTable("pedidos");

                entity.HasIndex(e => e.IdOficina, "FK_Pedidos_2_idx");

                entity.HasIndex(e => new { e.IdCasillero, e.IdOficina, e.IdLocker }, "FK_Pedidos_3_idx");

                entity.HasIndex(e => e.SDniCli, "FK_Pedidos_4_idx");

                entity.HasIndex(e => e.SDniUsu, "FK_Pedidos_5_idx");

                entity.HasIndex(e => e.IdBodega, "FK_Pedidos_6_idx");

                entity.Property(e => e.Idpedido).HasColumnType("int(11)");

                entity.Property(e => e.BCarga)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("bCarga");

                entity.Property(e => e.BRetiroCli)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("bRetiroCli");

                entity.Property(e => e.ClaveCliente)
                    .HasColumnType("int(11)")
                    .HasColumnName("claveCliente");

                entity.Property(e => e.ClaveUsuario)
                    .HasColumnType("int(11)")
                    .HasColumnName("claveUsuario");

                entity.Property(e => e.FechIn)
                    .HasColumnType("datetime")
                    .HasColumnName("Fech_In");

                entity.Property(e => e.FechOut)
                    .HasColumnType("datetime")
                    .HasColumnName("Fech_Out");

                entity.Property(e => e.FechReg)
                    .HasColumnType("datetime")
                    .HasColumnName("Fech_Reg");

                entity.Property(e => e.FechVen)
                    .HasColumnType("datetime")
                    .HasColumnName("Fech_Ven");

                entity.Property(e => e.IdBodega).HasColumnType("int(11)");

                entity.Property(e => e.IdCasillero).HasColumnType("int(11)");

                entity.Property(e => e.IdLocker).HasColumnType("int(11)");

                entity.Property(e => e.IdOficina).HasColumnType("int(11)");

                entity.Property(e => e.SCodProd)
                    .HasMaxLength(50)
                    .HasColumnName("sCodProd");

                entity.Property(e => e.SCodQr)
                    .HasMaxLength(50)
                    .HasColumnName("sCodQR");

                entity.Property(e => e.SDniCli)
                    .HasMaxLength(12)
                    .HasColumnName("sDniCli");

                entity.Property(e => e.SDniUsu)
                    .HasMaxLength(12)
                    .HasColumnName("sDniUsu");

                entity.Property(e => e.SPedidoSku)
                    .HasMaxLength(45)
                    .HasColumnName("sPedidoSku");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_Pedidos_6");

                entity.HasOne(d => d.IdOficinaNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdOficina)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedidos_1");

                entity.HasOne(d => d.SDniCliNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.SDniCli)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedidos_2");

                entity.HasOne(d => d.SDniUsuNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.SDniUsu)
                    .HasConstraintName("FK_Pedidos_3");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => new { d.IdLocker, d.IdOficina, d.IdCasillero })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedidos_5");
            });

            modelBuilder.Entity<Regione>(entity =>
            {
                entity.HasKey(e => e.IdRegion)
                    .HasName("PRIMARY");

                entity.ToTable("regiones");

                entity.HasIndex(e => e.IdZona, "FK_Region_1_idx");

                entity.Property(e => e.IdRegion)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("idRegion");

                entity.Property(e => e.IOrden)
                    .HasColumnType("int(11)")
                    .HasColumnName("iOrden");

                entity.Property(e => e.IdZona)
                    .HasColumnType("int(11)")
                    .HasColumnName("idZona");

                entity.Property(e => e.SRegion)
                    .HasMaxLength(50)
                    .HasColumnName("sRegion");

                entity.HasOne(d => d.IdZonaNavigation)
                    .WithMany(p => p.Regiones)
                    .HasForeignKey(d => d.IdZona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Region_1");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.SDniUsu)
                    .HasName("PRIMARY");

                entity.ToTable("usuarios");

                entity.Property(e => e.SDniUsu)
                    .HasMaxLength(12)
                    .HasColumnName("sDniUsu");

                entity.Property(e => e.BHab).HasColumnName("bHab");

                entity.Property(e => e.SNombreUsu)
                    .HasMaxLength(50)
                    .HasColumnName("sNombreUsu");

                entity.Property(e => e.SPassUsu)
                    .HasMaxLength(255)
                    .HasColumnName("sPassUsu");
            });

            modelBuilder.Entity<Usuariooficina>(entity =>
            {
                entity.HasKey(e => new { e.IdOficina, e.SDniUsu })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("usuariooficinas");

                entity.HasIndex(e => e.SDniUsu, "FK_UsuarioOficinas_2_idx");

                entity.Property(e => e.IdOficina).HasColumnType("int(11)");

                entity.Property(e => e.SDniUsu)
                    .HasMaxLength(12)
                    .HasColumnName("sDniUsu");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.HasOne(d => d.IdOficinaNavigation)
                    .WithMany(p => p.Usuariooficinas)
                    .HasForeignKey(d => d.IdOficina)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioOficinas_1");

                entity.HasOne(d => d.SDniUsuNavigation)
                    .WithMany(p => p.Usuariooficinas)
                    .HasForeignKey(d => d.SDniUsu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioOficinas_2");
            });

            modelBuilder.Entity<Zona>(entity =>
            {
                entity.HasKey(e => e.IdZona)
                    .HasName("PRIMARY");

                entity.ToTable("zonas");

                entity.Property(e => e.IdZona)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("idZona");

                entity.Property(e => e.SZona)
                    .HasMaxLength(50)
                    .HasColumnName("sZona");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
