namespace QuanLySinhVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SinhVien")]
    public partial class SinhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SinhVien()
        {
            DangKyLopHocs = new HashSet<DangKyLopHoc>();
            Users = new HashSet<User>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int StudentId { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Mã Sinh Viên")]
        public string MaSV { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Họ Và Tên")]
        public string HoTen { get; set; }

        [Display(Name = "Giới Tính")]
        public bool GioiTinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgaySinh { get; set; }

        public int LopId { get; set; }

        public bool IsActive { get; set; } = true;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DangKyLopHoc> DangKyLopHocs { get; set; }

        public virtual LopHoc LopHoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
