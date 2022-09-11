using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace open_auburn_api.Models
{
    [Table("College")]
    public partial class College
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("OAID")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Oaid { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Website { get; set; }

        public string? Bulletin { get; set; }

        public string? Phone { get; set; }

    }
}
