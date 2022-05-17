namespace CuaHangRuou.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GIOHANG")]
    public partial class GIOHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GIOHANG()
        {
            CHITIETGIOHANGs = new HashSet<CHITIETGIOHANG>();
            DONHANGs = new HashSet<DONHANG>();
        }

        [Key]
        public int MaGH { get; set; }

        public int MaKh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETGIOHANG> CHITIETGIOHANGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONHANG> DONHANGs { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}
