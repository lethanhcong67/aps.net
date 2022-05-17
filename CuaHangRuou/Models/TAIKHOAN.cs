namespace CuaHangRuou.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TAIKHOAN")]
    public partial class TAIKHOAN
    {
        [Key]
        public int MaTK { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        [StringLength(20, ErrorMessage = "Tên đăng nhập không quá 20 ký tự!")]
        public string TenDN { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        [StringLength(15, ErrorMessage = "Mật khẩu không quá 15 ký tự!")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        //[Required]
        //[StringLength(15)]
        public string PhanQuyen { get; set; }

        public bool? BiKhoa { get; set; }

        public int MaKh { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}
