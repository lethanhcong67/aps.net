namespace CuaHangRuou.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KHACHHANG")]
    public partial class KHACHHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHHANG()
        {
            GIOHANGs = new HashSet<GIOHANG>();
            TAIKHOANs = new HashSet<TAIKHOAN>();
        }

        [Key]
        public int MaKh { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống!")]
        [StringLength(60, ErrorMessage = "Họ tên không quá 60 ký tự!")]
        public string HoTen { get; set; }
        [RegularExpression("[0-9]{10}", ErrorMessage = "Số điện thoại chỉ bao gồm 10 số!")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống!")]
        [StringLength(100, ErrorMessage = "Địa chỉ không quá 100 ký tự!")]
        public string DiaChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GIOHANG> GIOHANGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAIKHOAN> TAIKHOANs { get; set; }
    }
}
