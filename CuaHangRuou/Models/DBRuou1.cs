using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CuaHangRuou.Models
{
    public partial class DBRuou1 : DbContext
    {
        public DBRuou1()
            : base("name=DBRuou1")
        {
        }

        public virtual DbSet<CHITIETGIOHANG> CHITIETGIOHANGs { get; set; }
        public virtual DbSet<DANHMUC> DANHMUCs { get; set; }
        public virtual DbSet<DONHANG> DONHANGs { get; set; }
        public virtual DbSet<GIOHANG> GIOHANGs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<SANPHAM> SANPHAMs { get; set; }
        public virtual DbSet<TAIKHOAN> TAIKHOANs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DANHMUC>()
                .HasMany(e => e.SANPHAMs)
                .WithRequired(e => e.DANHMUC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GIOHANG>()
                .HasMany(e => e.CHITIETGIOHANGs)
                .WithRequired(e => e.GIOHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GIOHANG>()
                .HasMany(e => e.DONHANGs)
                .WithRequired(e => e.GIOHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .HasMany(e => e.GIOHANGs)
                .WithRequired(e => e.KHACHHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KHACHHANG>()
                .HasMany(e => e.TAIKHOANs)
                .WithRequired(e => e.KHACHHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.Gia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.NongDo)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.CHITIETGIOHANGs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.TenDN)
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.PhanQuyen)
                .IsUnicode(false);
        }
    }
}
