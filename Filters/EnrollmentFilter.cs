using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace open_auburn_api.Filters
{
    public class EnrollmentFilter
    {
        public string? Major { get; set; }
        public string? MajorCode { get; set; }
        public string? College { get; set; }
        public string? Term { get; set; }
        public string? UGMale { get; set; }
        public string? UGFemale { get; set; }
        public string? GRMale { get; set; }
        public string? GRFemale { get; set; }
        public string? Total { get; set; }

        public EnrollmentFilter()
        {
            this.Major = "";
            this.MajorCode = "";
            this.College = "";
            this.Term = "";
            this.UGMale = "";
            this.UGFemale = "";
            this.GRMale = "";
            this.GRFemale = "";
            this.Total = "";
        }
        public EnrollmentFilter(string major, string majorCode, 
            string college, string term, string ugMale, string ugFemale, 
            string grMale, string grFemale, string total)
        {
            this.Major = string.IsNullOrEmpty(major) ? "" : major;
            this.MajorCode = string.IsNullOrEmpty(majorCode) ? "" : majorCode;
            this.College = string.IsNullOrEmpty(college) ? "" : college;
            this.Term = string.IsNullOrEmpty(term) ? "" : term;
            this.UGMale = string.IsNullOrEmpty(ugMale) ? "" : ugMale;
            this.UGFemale = string.IsNullOrEmpty(ugFemale) ? "" : ugFemale;
            this.GRMale = string.IsNullOrEmpty(grMale) ? "" : grMale;
            this.GRFemale = string.IsNullOrEmpty(grFemale) ? "" : grFemale;
            this.Total = string.IsNullOrEmpty(total) ? "" : total;
        }
    }
}
