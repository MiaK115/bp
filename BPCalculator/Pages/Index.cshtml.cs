using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

// page model

namespace BPCalculator.Pages
{
    public class BloodPressureModel : PageModel
    {
        [BindProperty]                              // bound on POST
        public BloodPressure BP { get; set; }

        private readonly ILogger<BloodPressureModel> _logger;

        public BloodPressureModel(ILogger<BloodPressureModel> logger)
        {
            _logger = logger;
        }

        // setup initial data
        public void OnGet()
        {
            _logger.LogInformation("Feature triggered: user accessed the Index page.");

            BP = new BloodPressure() { Systolic = 100, Diastolic = 60 };

            //log important values or outcomes
            int systolic = 140;
            int diastolic = 90;
            _logger.LogInformation("User input: Systolic = {Systolic}, Diastolic = {Diastolic}", systolic, diastolic);

            string category = "High Blood Pressure";
            _logger.LogInformation("Calculated category: {Category}", category);
        }

        // POST, validate
        public IActionResult OnPost()
        {
            // extra validation
            if (!(BP.Systolic > BP.Diastolic))
            {
                ModelState.AddModelError("", "Systolic must be greater than Diastolic");
            }
            return Page();
        }
    }
}