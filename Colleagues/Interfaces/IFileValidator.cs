namespace Colleagues.Interfaces
{
    public interface IFileValidator
    {
        bool IsCsvFile(IFormFile file);
    }
}
