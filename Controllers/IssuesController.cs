using Microsoft.AspNetCore.Mvc;
using MunicipalServicesMvcCore.Data;
using MunicipalServicesMvcCore.Models;

namespace MunicipalServicesMvcCore.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssueRepository _repo;
        private readonly IWebHostEnvironment _env;

        public IssuesController(IIssueRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        private static readonly string[] Categories = new[]
        {
            "Sanitation",
            "Roads",
            "Electricity / Utilities",
            "Water & Sanitation",
            "Parks & Recreation",
            "Safety & Security",
            "Other"
        };

        // GET: /Issues/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = Categories;
            return View(new IssueCreateViewModel());
        }

        // POST: /Issues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IssueCreateViewModel vm)
        {
            ViewBag.Categories = Categories;

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please correct the highlighted errors.";
                return View(vm);
            }

            var issue = new Issue
            {
                Location = vm.Location.Trim(),
                Category = vm.Category,
                Description = vm.Description.Trim()
            };

            // Save attachments under wwwroot/uploads/{IssueId}
            if (vm.Attachments is { Count: > 0 })
            {
                var issueFolder = Path.Combine(_env.WebRootPath, "uploads", issue.Id.ToString());
                Directory.CreateDirectory(issueFolder);

                foreach (var file in vm.Attachments.Where(f => f?.Length > 0))
                {
                    var safeName = Path.GetFileName(file!.FileName);
                    var destPath = Path.Combine(issueFolder, safeName);

                    using var stream = System.IO.File.Create(destPath);
                    await file.CopyToAsync(stream);

                    // Store a web-accessible relative URL for later viewing
                    var relativeUrl = $"/uploads/{issue.Id}/{safeName}";
                    issue.AttachmentPaths.Add(relativeUrl);
                }
            }

            _repo.Add(issue);
            TempData["Success"] = $"Issue submitted successfully! Reference: {issue.Id}";

            // Redirect to clear the form & show success
            return RedirectToAction(nameof(Create));
        }
    }
}
