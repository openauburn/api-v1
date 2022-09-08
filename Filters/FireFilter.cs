using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations.Schema;

namespace open_auburn_api.Filters
{
    public class FireFilter
    {
        public string? Campus { get; set; }
        public string? FireDate { get; set; }
        public string? MinFireDate { get; set; }
        public string? MaxFireDate { get; set; }
        public string? FireTime { get; set; }
        public string? MinFireTime { get; set; }
        public string? MaxFireTime { get; set; }
        public string? DateReported { get; set; }
        public string? MinDateReported { get; set; }
        public string? MaxDateReported { get; set; }
        public string? Description { get; set; }
        public string? Cause { get; set; }
        public string? DamageCost { get; set; }
        public string? Injuries { get; set; }
        public string? Deaths { get; set; }
        public string? Location { get; set; }
        public FireFilter()
        {
            this.Campus = "";

            this.FireDate = "";
            this.MinFireDate = DateTime.MinValue.ToString();
            this.MaxFireDate = DateTime.MaxValue.ToString();

            this.FireTime = "";
            this.MinFireTime = "";
            this.MaxFireTime = "";

            this.DateReported = "";
            this.MinDateReported = DateTime.MinValue.ToString();
            this.MaxDateReported = DateTime.MaxValue.ToString();

            this.Description = "";
            this.Cause = "";
            this.DamageCost = "";
        
            this.Injuries = "0";
            this.Deaths = "0";

            this.Location = "";
        }
        public FireFilter(string campus, string fireDate, string minFireDate, string maxFireDate, 
            string fireTime, string minFireTime, string maxFireTime, 
            string dateReported, string minDateReported, string maxDateReported,
            string description, string cause, string damageCost,
            string injuries, string deaths, string location)
        {
            this.Campus = string.IsNullOrEmpty(campus) ? "" : campus;

            this.FireDate = DateTime.TryParse(fireDate, out DateTime tempFireDate) ? tempFireDate.ToString() : "";
            this.MinFireDate = DateTime.TryParse(minFireDate, out DateTime tempMinFireDate) ? tempMinFireDate.ToString() : DateTime.MinValue.ToString();
            this.MaxFireDate = DateTime.TryParse(maxFireDate, out DateTime tempMaxFireDate) ? tempMaxFireDate.ToString() : DateTime.MinValue.ToString();

            this.FireTime = TimeSpan.TryParse(fireTime, out TimeSpan tempFireTime) ? tempFireTime.ToString() : "";
            this.MinFireTime = TimeSpan.TryParse(minFireTime, out TimeSpan tempMinFireTime) ? tempMinFireTime.ToString() : "";
            this.MaxFireTime = TimeSpan.TryParse(maxFireTime, out TimeSpan tempMaxFireTime) ? tempMaxFireTime.ToString() : "";

            this.DateReported = DateTime.TryParse(dateReported, out DateTime tempDate) ? tempDate.ToString() : "";
            this.MinFireDate = DateTime.TryParse(minDateReported, out DateTime tempMinDate) ? tempMinDate.ToString() : DateTime.MinValue.ToString();
            this.MaxFireDate = DateTime.TryParse(maxDateReported, out DateTime tempMaxDate) ? tempMaxDate.ToString() : DateTime.MinValue.ToString();

            this.Description = string.IsNullOrEmpty(description) ? "" : description;
            this.Cause = string.IsNullOrEmpty(cause) ? "" : cause;
            this.DamageCost = string.IsNullOrEmpty(damageCost) ? "" : damageCost;

            this.Injuries = int.TryParse(injuries, out int tempInjury) ? tempInjury.ToString() : "0";
            this.Deaths = int.TryParse(deaths, out int tempDeath) ? tempDeath.ToString() : "0";

            this.Location = string.IsNullOrEmpty(location) ? "" : location;
        }
    }
}
