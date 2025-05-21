using Colleagues.Interfaces;
using Colleagues.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Colleagues.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ICollaborationCalculator _calculator;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IFileValidator _fileValidator;
        private readonly ICSVParser _csvParser;

        public EmployeeController(ICollaborationCalculator calculator,
                                  ILogger<EmployeeController> logger,
                                  IFileValidator fileValidator,
                                  ICSVParser csvParser)
        {
            _calculator = calculator;
            _logger = logger;
            _fileValidator = fileValidator;
            _csvParser = csvParser;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            if (!_fileValidator.IsCsvFile(file))
            {
                _logger.LogWarning("Invalid file upload attempt: Not a CSV.");
                ModelState.AddModelError(nameof(file), "Please select a valid CSV file.");
                return View("Index");
            }

            try
            {
                var entries = await _csvParser.ParseEmployeeDataAsync(file);
                var topProjects = _calculator.GetTopPairWithProjects(entries);

                if (!topProjects.Any())
                {
                    _logger.LogInformation("File uploaded: {FileName} ({FileLength} bytes)", file.FileName, file.Length);
                    var vm = new TopPairProjectsViewModel { Message = "No employee pairs found." };
                    return View("TopPairProjects", vm);
                }

                var model = new TopPairProjectsViewModel
                {
                    TopProjects = topProjects,
                    EmployeeId1 = topProjects.First().EmployeeId1,
                    EmployeeId2 = topProjects.First().EmployeeId2
                };

                return View("TopPairProjects", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while parsing CSV file.");
                ModelState.AddModelError("file", "An error occurred while processing the file.");
                return View("Index");
            }
        }

    }
}