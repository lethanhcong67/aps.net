namespace CuaHangRuou.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DONHANG")]
    public partial class DONHANG
    {
        [Key]
        public int MaDH { get; set; }

        [Column(TypeName = "ntext")]
        public string GhiChu { get; set; }

        [Required]
        [StringLength(50,ErrorMessage ="Nhập tối đa 50 ký tự!")]
        public string GiaoHang { get; set; }
        
        [Required(ErrorMessage ="Ngày đặt không được để trống")]
        public DateTime NgayDat { get; set; }

        [Required(ErrorMessage = "Tình trạng không được để trống")]
        [StringLength(50, ErrorMessage = "Nhập tối đa 50 ký tự!")]
        public string TinhTrang { get; set; }

        public int MaGH { get; set; }

        public virtual GIOHANG GIOHANG { get; set; }
    }
}
