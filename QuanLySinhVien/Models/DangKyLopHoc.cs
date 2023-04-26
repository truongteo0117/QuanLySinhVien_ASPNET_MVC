using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace QuanLySinhVien.Models
{
    [Table("DangKyLopHoc")]
    public partial class DangKyLopHoc
    {
        [Key]
        public int DangKyId { get; set; }

        public int StudentId { get; set; }

        public int LopId { get; set; }

        public DateTime NgayDangKy { get; set; } = DateTime.Now;

        public long? IdMonHoc { get; set; }

        public virtual LopHoc LopHoc { get; set; }

        public virtual MonHoc MonHoc { get; set; }

        public virtual SinhVien SinhVien { get; set; }
    }
}
