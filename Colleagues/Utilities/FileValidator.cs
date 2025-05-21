using Colleagues.Interfaces;

namespace Colleagues.Utilities
{
    public class FileValidator : IFileValidator
    {
        public bool IsCsvFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var extension = Path.GetExtension(file.FileName);
            return string.Equals(extension, ".csv", StringComparison.OrdinalIgnoreCase);
        }
    }
}
