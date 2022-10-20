using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable
namespace open_auburn_api.Filters
{
    public class FireFilter
    {
        public string? Campus { get; set; }
        public string? ReportedOn { get; set; }
        public string? OccurredAt { get; set; }
        public string? Description { get; set; }
        public string? Cause { get; set; }
        public string? DamageCost { get; set; }
        public string? Injuries { get; set; }
        public string? Deaths { get; set; }
        public string? Address { get; set; }
        public FireFilter()
        {
            this.Campus = "";

            this.OccurredAt = "";
            this.ReportedOn = "";

            this.Description = "";
            this.Cause = "";
            this.DamageCost = "";
        
            this.Injuries = "0";
            this.Deaths = "0";

            this.Address = "";
        }
        public FireFilter(string campus, string fireDate, string dateReported,
            string description, string cause, string damageCost,
            string injuries, string deaths, string address)
        {
            this.Campus = string.IsNullOrEmpty(campus) ? "" : campus;

            this.OccurredAt = DateTime.TryParse(fireDate, out DateTime tempFireDate) ? tempFireDate.ToString() : "";

            this.ReportedOn = DateTime.TryParse(dateReported, out DateTime tempDate) ? tempDate.ToString() : "";
           
            this.Description = string.IsNullOrEmpty(description) ? "" : description;
            this.Cause = string.IsNullOrEmpty(cause) ? "" : cause;
            this.DamageCost = string.IsNullOrEmpty(damageCost) ? "" : damageCost;

            this.Injuries = int.TryParse(injuries, out int tempInjury) ? tempInjury.ToString() : "0";
            this.Deaths = int.TryParse(deaths, out int tempDeath) ? tempDeath.ToString() : "0";

            this.Address = string.IsNullOrEmpty(address) ? "" : address;
        }
    }
}
