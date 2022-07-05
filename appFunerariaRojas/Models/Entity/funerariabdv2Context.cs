using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace appFunerariaRojas.Models.Entity
{
    public partial class funerariabdv2Context : DbContext
    {
        public funerariabdv2Context()
        {
        }

        public funerariabdv2Context(DbContextOptions<funerariabdv2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<CompraDetalle> CompraDetalle { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemImagenes> ItemImagenes { get; set; }
        public virtual DbSet<ItemPresentacion> ItemPresentacion { get; set; }
        public virtual DbSet<PlanPaquete> PlanPaquete { get; set; }
        public virtual DbSet<PlanPaqueteItems> PlanPaqueteItems { get; set; }
        public virtual DbSet<PlanPaqueteServicios> PlanPaqueteServicios { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<ServicioAdicional> ServicioAdicional { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }
        public virtual DbSet<VentaItemPresentacion> VentaItemPresentacion { get; set; }
        public virtual DbSet<VentaPagos> VentaPagos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=localhost;Database=funerariabdv2; Uid=root;Pwd=2011y2012");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("categoria");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(150);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("cliente");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Apmaterno)
                    .HasColumnName("apmaterno")
                    .HasMaxLength(50);

                entity.Property(e => e.Appaterno)
                    .HasColumnName("appaterno")
                    .HasMaxLength(50);

                entity.Property(e => e.Ci)
                    .IsRequired()
                    .HasColumnName("ci")
                    .HasMaxLength(10);

                entity.Property(e => e.CiExp)
                    .IsRequired()
                    .HasColumnName("ci_exp")
                    .HasMaxLength(2);

                entity.Property(e => e.Ciudad)
                    .HasColumnName("ciudad")
                    .HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .HasMaxLength(150);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50);

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.ToTable("compra");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Cocepto)
                    .HasColumnName("cocepto")
                    .HasMaxLength(150);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.Hora).HasColumnName("hora");

                entity.Property(e => e.IdProveedor)
                    .HasColumnName("id_proveedor")
                    .HasMaxLength(50);

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CompraDetalle>(entity =>
            {
                entity.ToTable("compra_detalle");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IdCompra)
                    .HasColumnName("id_compra")
                    .HasMaxLength(50);

                entity.Property(e => e.IdItemPresentacion)
                    .HasColumnName("id_item_presentacion")
                    .HasMaxLength(50);

                entity.Property(e => e.Precio)
                    .HasColumnName("precio")
                    .HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Cajon)
                    .HasColumnName("cajon")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.CajonAcabado)
                    .HasColumnName("cajon_acabado")
                    .HasMaxLength(50);

                entity.Property(e => e.CajonDetalle)
                    .HasColumnName("cajon_detalle")
                    .HasMaxLength(50);

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(150);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IdCategoria)
                    .HasColumnName("id_categoria")
                    .HasMaxLength(50);

                entity.Property(e => e.RangoNivel).HasColumnName("rango_nivel");
            });

            modelBuilder.Entity<ItemImagenes>(entity =>
            {
                entity.ToTable("item_imagenes");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IdItem)
                    .HasColumnName("id_item")
                    .HasMaxLength(50);

                entity.Property(e => e.Imagen)
                    .HasColumnName("imagen")
                    .HasColumnType("longtext");
            });

            modelBuilder.Entity<ItemPresentacion>(entity =>
            {
                entity.ToTable("item_presentacion");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Cajon)
                    .HasColumnName("cajon")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(50);

                entity.Property(e => e.Descripion)
                    .HasColumnName("descripion")
                    .HasMaxLength(150);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IdItem)
                    .HasColumnName("id_item")
                    .HasMaxLength(50);

                entity.Property(e => e.Imagen).HasColumnName("imagen");

                entity.Property(e => e.PrecioCompra)
                    .HasColumnName("precio_compra")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.PrecioVenta)
                    .HasColumnName("precio_venta")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.Tamaño)
                    .HasColumnName("tamaño")
                    .HasMaxLength(50);

                entity.Property(e => e.UnidadMedida)
                    .HasColumnName("unidad_medida")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<PlanPaquete>(entity =>
            {
                entity.ToTable("plan_paquete");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(150);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(50);

                entity.Property(e => e.Precio)
                    .HasColumnName("precio")
                    .HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<PlanPaqueteItems>(entity =>
            {
                entity.ToTable("plan_paquete_items");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IdItemPresentacion)
                    .HasColumnName("id_item_presentacion")
                    .HasMaxLength(50);

                entity.Property(e => e.IdPlanPaquete)
                    .HasColumnName("id_plan_paquete")
                    .HasMaxLength(50);

                entity.Property(e => e.PrecioCosto)
                    .HasColumnName("precio_costo")
                    .HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<PlanPaqueteServicios>(entity =>
            {
                entity.ToTable("plan_paquete_servicios");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IdPlanPaquete)
                    .HasColumnName("id_plan_paquete")
                    .HasMaxLength(50);

                entity.Property(e => e.IdServicioAdicional)
                    .HasColumnName("id_servicio_adicional")
                    .HasMaxLength(50);

                entity.Property(e => e.PrecioCosto)
                    .HasColumnName("precio_costo")
                    .HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.ToTable("proveedor");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Ciudad)
                    .HasColumnName("ciudad")
                    .HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .HasMaxLength(150);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Nit)
                    .IsRequired()
                    .HasColumnName("nit")
                    .HasMaxLength(20);

                entity.Property(e => e.RazonSocial)
                    .HasColumnName("razon_social")
                    .HasMaxLength(50);

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<ServicioAdicional>(entity =>
            {
                entity.ToTable("servicio_adicional");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(150);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(50);

                entity.Property(e => e.Precio)
                    .HasColumnName("precio")
                    .HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Apmaterno)
                    .HasColumnName("apmaterno")
                    .HasMaxLength(50);

                entity.Property(e => e.Appaterno)
                    .HasColumnName("appaterno")
                    .HasMaxLength(50);

                entity.Property(e => e.Ci)
                    .IsRequired()
                    .HasColumnName("ci")
                    .HasMaxLength(10);

                entity.Property(e => e.CiExp)
                    .IsRequired()
                    .HasColumnName("ci_exp")
                    .HasMaxLength(2);

                entity.Property(e => e.Ciudad)
                    .HasColumnName("ciudad")
                    .HasMaxLength(50);

                entity.Property(e => e.Contrasenna)
                    .HasColumnName("contrasenna")
                    .HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .HasMaxLength(150);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Foto)
                    .HasColumnName("foto")
                    .HasColumnType("longtext");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50);

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("venta");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.ApmaternoFallecido)
                    .HasColumnName("apmaterno_fallecido")
                    .HasMaxLength(50);

                entity.Property(e => e.AppaternoFallecido)
                    .HasColumnName("appaterno_fallecido")
                    .HasMaxLength(50);

                entity.Property(e => e.CertDefuncion)
                    .HasColumnName("cert_defuncion")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.CertDefuncionImg)
                    .HasColumnName("cert_defuncion_img")
                    .HasColumnType("longtext");

                entity.Property(e => e.CertMedico)
                    .HasColumnName("cert_medico")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.CertMedicoImg)
                    .HasColumnName("cert_medico_img")
                    .HasColumnType("longtext");

                entity.Property(e => e.CiExpFallecido)
                    .IsRequired()
                    .HasColumnName("ci_exp_fallecido")
                    .HasMaxLength(2);

                entity.Property(e => e.CiFallecido)
                    .IsRequired()
                    .HasColumnName("ci_fallecido")
                    .HasMaxLength(10);

                entity.Property(e => e.CiudadFallecido)
                    .HasColumnName("ciudad_fallecido")
                    .HasMaxLength(50);

                entity.Property(e => e.Concepto)
                    .HasColumnName("concepto")
                    .HasMaxLength(150);

                entity.Property(e => e.CorejoDireccion)
                    .HasColumnName("corejo_direccion")
                    .HasMaxLength(150);

                entity.Property(e => e.Correlativo).HasColumnName("correlativo");

                entity.Property(e => e.CortejoFecha)
                    .HasColumnName("cortejo_fecha")
                    .HasColumnType("date");

                entity.Property(e => e.CortejoHora).HasColumnName("cortejo_hora");

                entity.Property(e => e.DetalleFallecimiento)
                    .HasColumnName("detalle_fallecimiento")
                    .HasMaxLength(150);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.FechaFallecimientoFallecido)
                    .HasColumnName("fecha_fallecimiento_fallecido")
                    .HasColumnType("date");

                entity.Property(e => e.FechaNacimientoFallecido)
                    .HasColumnName("fecha_nacimiento_fallecido")
                    .HasColumnType("date");

                entity.Property(e => e.Hora).HasColumnName("hora");

                entity.Property(e => e.IdCliente)
                    .HasColumnName("id_cliente")
                    .HasMaxLength(50);

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasMaxLength(50);

                entity.Property(e => e.NombreFallecido)
                    .IsRequired()
                    .HasColumnName("nombre_fallecido")
                    .HasMaxLength(50);

                entity.Property(e => e.PlanPaquete)
                    .HasColumnName("plan_paquete")
                    .HasMaxLength(50);

                entity.Property(e => e.Venta1)
                    .HasColumnName("venta")
                    .HasColumnType("bit(1)");
            });

            modelBuilder.Entity<VentaItemPresentacion>(entity =>
            {
                entity.ToTable("venta_item_presentacion");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IdItemPresentacion)
                    .HasColumnName("id_item_presentacion")
                    .HasMaxLength(50);

                entity.Property(e => e.IdVenta)
                    .HasColumnName("id_venta")
                    .HasMaxLength(50);

                entity.Property(e => e.Precio)
                    .HasColumnName("precio")
                    .HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<VentaPagos>(entity =>
            {
                entity.ToTable("venta_pagos");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.Hora).HasColumnName("hora");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasMaxLength(50);

                entity.Property(e => e.IdVenta)
                    .HasColumnName("id_venta")
                    .HasMaxLength(50);

                entity.Property(e => e.SaldoActual)
                    .HasColumnName("saldo_actual")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.SaldoCancelado)
                    .HasColumnName("saldo_cancelado")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.SaldoInicial)
                    .HasColumnName("saldo_inicial")
                    .HasColumnType("decimal(10,2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
