using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MunicipalServicesMvcCore.Models
{
    public class IssueCreateViewModel
    {
        [Required, Display(Name = "Location")]
        public string Location { get; set; } = string.Empty;

        [Required, Display(Name = "Category")]
        public string Category { get; set; } = string.Empty;

        [Required, Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Attachments")]
        public List<IFormFile>? Attachments { get; set; }
    }
}
