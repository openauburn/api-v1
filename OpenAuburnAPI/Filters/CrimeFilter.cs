using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

#nullable enable
namespace open_auburn_api.Filters
{
    public class CrimeFilter
    {
        public string? Campus { get; set; }
        [Column(TypeName = "date")]
        public string? Date { get; set; }
        [Column(TypeName = "date")]
        [FromQuery(Name = "min_date")]
        public string? MinDate { get; set; }
        [Column(TypeName = "date")]
        [FromQuery(Name = "max_date")]
        public string? MaxDate { get; set; }
        [FromQuery(Name = "indcident_type")]
        public string? IncidentType { get; set; }
        [FromQuery(Name = "clery_class")]
        public string? CleryClass { get; set; }
        public string? Address { get; set; }
        public CrimeFilter()
        {
            this.Campus = "";
            this.Date = "";
            this.MinDate = DateTime.MinValue.ToString();
            this.MaxDate = DateTime.MaxValue.ToString();
            this.IncidentType = "";
            this.CleryClass = "";
            this.Address = "";
        }
        public CrimeFilter(string campus, string date, string minDate, string maxDate, string incidentType, string cleryClass, string address)
        {
            this.Campus = string.IsNullOrEmpty(campus) ? "" : campus;
            this.Date = DateTime.TryParse(date, out DateTime tempDate) ? tempDate.ToString() : "";
            this.MinDate = DateTime.TryParse(minDate, out DateTime tempMin) ? tempMin.ToString() : DateTime.MinValue.ToString();
            this.MaxDate = DateTime.TryParse(maxDate, out DateTime tempMax) ? tempMax.ToString() : DateTime.MaxValue.ToString();
            this.IncidentType = string.IsNullOrEmpty(incidentType) ? "" : incidentType;
            this.CleryClass = string.IsNullOrEmpty(cleryClass) ? "" : cleryClass;
            this.Address = string.IsNullOrEmpty(address) ? "" : address;
        }
    }
}
