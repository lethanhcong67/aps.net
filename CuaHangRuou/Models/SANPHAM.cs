namespace CuaHangRuou.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SANPHAM")]
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            CHITIETGIOHANGs = new HashSet<CHITIETGIOHANG>();
        }

        [Key]
        public int MaSP { get; set; }

        [Required(ErrorMessage ="Tên sản phẩm không được để trống!")]
        [StringLength(150, ErrorMessage = "Tên sản phẩm không quá 150 ký tự!")]
        public string TenSP { get; set; }

        [Required(ErrorMessage = "Ảnh sản phẩm không được để trống!")]
        [StringLength(100)]
        public string Anh { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm không được để trống!")]
        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:#,###}")]
        public decimal Gia { get; set; }

        public int SoLuong { get; set; }

        [Required(ErrorMessage = "Loại sản phẩm không được để trống!")]
        [StringLength(50, ErrorMessage = "Loại sản phẩm không quá 50 ký tự!")]
        public string LoaiSP { get; set; }

        [Required(ErrorMessage = "Thương hiệu không được để trống!")]
        [StringLength(100, ErrorMessage = "Thương hiệu không quá 100 ký tự!")]
        public string ThuongHieu { get; set; }

        [Required(ErrorMessage = "Xuất xứ không được để trống!")]
        [StringLength(100, ErrorMessage = "Xuất xứ không quá 100 ký tự!")]
        public string XuatXu { get; set; }

        [Required(ErrorMessage = "Dung tích không được để trống!")]
        [StringLength(20, ErrorMessage = "Dung tích không quá 20 ký tự!")]
        public string DungTich { get; set; }

        [Required(ErrorMessage = "Nồng độ không được để trống!")]
        [StringLength(20, ErrorMessage = "Nồng độ không quá 20 ký tự!")]
        public string NongDo { get; set; }

        [StringLength(50, ErrorMessage = "Tuổi rượu không quá 50 ký tự!")]
        public string TuoiRuou { get; set; }

        [StringLength(100, ErrorMessage = "Bộ sưu tập không quá 100 ký tự!")]
        public string BoSuuTap { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống!")]
        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        public int MaDM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETGIOHANG> CHITIETGIOHANGs { get; set; }

        public virtual DANHMUC DANHMUC { get; set; }
    }
}
