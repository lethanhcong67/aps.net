namespace CuaHangRuou.Areas.Admin.Models
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
        [StringLength(50)]
        public string GiaoHang { get; set; }

        public DateTime NgayDat { get; set; }

        [Required]
        [StringLength(50)]
        public string TinhTrang { get; set; }

        public int MaGH { get; set; }

        public virtual GIOHANG GIOHANG { get; set; }
    }
}
