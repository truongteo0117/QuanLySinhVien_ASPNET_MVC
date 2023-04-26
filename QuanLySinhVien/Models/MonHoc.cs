using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace QuanLySinhVien.Models
{
    public class MonHoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MonHocId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tên Môn Học")]
        public string TenMonHoc { get; set; }

        [Display(Name = "Tín Chỉ Môn")]
        public int TinChiMon { get; set; }
        public ICollection<DangKyLopHoc> DangKyLopHocs { get; internal set; }
    }
}