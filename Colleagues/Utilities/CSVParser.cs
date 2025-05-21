using Colleagues.Interfaces;
using Colleagues.Models;
using System.Globalization;

namespace Colleagues.Utilities
{
    public class CsvParser : ICSVParser
    {
        private static readonly string[] SupportedDateFormats = {
            "yyyy-MM-dd", "MM/dd/yyyy", "dd-MM-yyyy",
            "d/M/yyyy", "M/d/yyyy", "yyyy/MM/dd"
        };

        public async Task<List<EmployeeProjectInfo>> ParseEmployeeDataAsync(IFormFile file)
        {
            var entries = new List<EmployeeProjectInfo>();

            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);
            string? line;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',');

                if (parts.Length < 4 ||
                    !int.TryParse(parts[0].Trim(), out var empId) ||
                    !int.TryParse(parts[1].Trim(), out var projectId) ||
                    !TryParseDate(parts[2].Trim(), out var dateFrom))
                    continue;

                DateTime dateTo;
                var dateToRaw = parts[3].Trim();

                if (dateToRaw.Equals("NULL", StringComparison.OrdinalIgnoreCase))
                    dateTo = DateTime.Today;
                else if (!TryParseDate(dateToRaw, out dateTo))
                    continue;

                entries.Add(new EmployeeProjectInfo
                {
                    EmpID = empId,
                    ProjectID = projectId,
                    DateFrom = dateFrom,
                    DateTo = dateTo
                });
            }

            return entries;
        }

        #region Helper

        private static bool TryParseDate(string dateStr, out DateTime date)
        {
            return DateTime.TryParseExact(dateStr,
                                          SupportedDateFormats,
                                          CultureInfo.InvariantCulture,
                                          DateTimeStyles.None,
                                          out date);
        }

        #endregion
    }
}
