using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace open_auburn_api.Models
{
    [Table("Crime")]
    public partial class Crime
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("OAID")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Oaid { get; set; }
        [Column("IncidentID")]
        public string? IncidentId { get; set; }
        public string? Campus { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateReported { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOccurredFrom { get; set; }
        public TimeSpan? TimeOccurredFrom { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOccurredTo { get; set; }
        public TimeSpan? TimeOccurredTo { get; set; }
        public string? IncidentType { get; set; }
        public string? CleryClass { get; set; }
        public string? AdditionalInfo { get; set; }
        public string? Location { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Disposition { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOfSupplement { get; set; }
        public string? SupplementDisposition { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOfSupplement2 { get; set; }
        public string? SupplementDisposition2 { get; set; }
    }
}
