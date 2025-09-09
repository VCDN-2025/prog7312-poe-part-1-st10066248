using MunicipalServicesMvcCore.Models;

namespace MunicipalServicesMvcCore.Data
{
    public interface IIssueRepository
    {
        void Add(Issue issue);
        IReadOnlyList<Issue> GetAll();
    }
}
