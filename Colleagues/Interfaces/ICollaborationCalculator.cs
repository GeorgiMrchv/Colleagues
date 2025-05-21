using Colleagues.Models;

namespace Colleagues.Interfaces
{
    public interface ICollaborationCalculator
    {
        List<CommonProjectEmployee> CalculateTotalDaysPerPair(List<EmployeeProjectInfo> entries);
        List<CommonProjectEmployee> CalculateDaysPerPairPerProject(List<EmployeeProjectInfo> entries);
        List<CommonProjectEmployee> GetTopPairWithProjects(List<EmployeeProjectInfo> entries);
    }
}
