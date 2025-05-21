using Colleagues.Models;

namespace Colleagues.Interfaces
{
    public interface ICSVParser
    {
        Task<List<EmployeeProjectInfo>> ParseEmployeeDataAsync(IFormFile file);
    }
}
