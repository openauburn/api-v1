using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations.Schema;

namespace open_auburn_api.Filters
{
    public class CollegeFilter
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Website { get; set; }

        public string? Bulletin { get; set; }

        public string? Phone { get; set; }

        public CollegeFilter()
        {
            this.Name = "";
            this.Description = "";
            this.Website = "";
            this.Bulletin = "";
            this.Phone = "";
        }
        public CollegeFilter(string name, string description, string website, string bulletin, string phone)
        {
            this.Name = string.IsNullOrEmpty(name) ? "" : name;
            this.Description = string.IsNullOrEmpty(description) ? "" : description;
            this.Website = string.IsNullOrEmpty(website) ? "" : website;
            this.Bulletin = string.IsNullOrEmpty(bulletin) ? "" : bulletin;
            this.Phone = string.IsNullOrEmpty(phone) ? "" : phone;
        }
    }
}
