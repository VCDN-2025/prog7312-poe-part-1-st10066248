using MunicipalServicesMvcCore.Models;

namespace MunicipalServicesMvcCore.Data
{
    public class IssueRepository : IIssueRepository
    {
        private readonly List<Issue> _issues = new();

        public void Add(Issue issue) => _issues.Add(issue);

        public IReadOnlyList<Issue> GetAll() => _issues.AsReadOnly();
    }
}
