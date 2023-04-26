namespace QuanLySinhVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongBao")]
    public partial class ThongBao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ThongBaoId { get; set; }

        [Required]
        [StringLength(255)]
        public string NoiDung { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
