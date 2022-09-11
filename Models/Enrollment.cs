using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace open_auburn_api.Models
{
    [Table("Enrollment")]
    public partial class Enrollment
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("OAID")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Oaid { get; set; }
        public string? Major { get; set; }
        public string? MajorCode { get; set; }
        public string? College { get; set; }
        public string? Term { get; set; }
        public int? UGMale { get; set; }
        public int? UGFemale { get; set; }
        public int? GRMale { get; set; }
        public int? GRFemale { get; set; }
        public int? Total { get; set; }
    }
}
