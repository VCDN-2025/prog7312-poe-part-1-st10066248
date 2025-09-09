using System.ComponentModel.DataAnnotations;

namespace MunicipalServicesMvcCore.Models
{
    public enum IssueStatus
    {
        Submitted,
        Acknowledged,
        InProgress,
        Resolved
    }

    public class Issue
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, StringLength(200)]
        public string Location { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Category { get; set; } = string.Empty;

        [Required, StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        public List<string> AttachmentPaths { get; set; } = new();

        public IssueStatus Status { get; set; } = IssueStatus.Submitted;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
