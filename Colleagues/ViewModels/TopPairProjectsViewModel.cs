using Colleagues.Models;

namespace Colleagues.ViewModels
{
    public class TopPairProjectsViewModel
    {
        public int EmployeeId1 { get; set; }
        public int EmployeeId2 { get; set; }
        public int TotalDaysWorked => TopProjects?.Sum(x => x.DaysWorked) ?? 0;
        public List<CommonProjectEmployee> TopProjects { get; set; }
        public string? Message { get; set; }
    }
}