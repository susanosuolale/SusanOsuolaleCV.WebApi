using Microsoft.AspNetCore.Mvc;

namespace SusanOsuolaleCV.Api.Controllers
{
    /// <summary>
    /// Simple tax calculator endpoint using user-provided progressive brackets.
    /// Accepts annual income and returns tax owed using configured brackets.
    /// </summary>
    [ApiController]
    [Route("api/tax")]
    public class TaxCalculatorController : ControllerBase
    {
        /// <summary>
        /// Simple tax calculator endpoint using user-provided progressive brackets.
        /// Accepts annual income and returns tax owed using configured brackets.
        /// </summary>
        // Accepts annual income and returns tax owed using configured brackets

        public class TaxRequest
        {
            public decimal AnnualIncome { get; set; }
        }

        public class TaxResult
        {
            public decimal AnnualIncome { get; set; }
            public decimal TaxOwed { get; set; }
            public decimal NetIncome { get; set; }
        }

        public class TaxBracket
        {
            public decimal Threshold { get; set; }
            public decimal Rate { get; set; }
        }

        // User-provided brackets
        public List<TaxBracket> Brackets { get; } = new List<TaxBracket>
        {
            new TaxBracket { Threshold = 800000m, Rate = 0.00m },
            new TaxBracket { Threshold = 3000000m, Rate = 0.15m },
            new TaxBracket { Threshold = 12000000m, Rate = 0.18m },
            new TaxBracket { Threshold = 25000000m, Rate = 0.21m },
            new TaxBracket { Threshold = 50000000m, Rate = 0.23m },
            new TaxBracket { Threshold = decimal.MaxValue, Rate = 0.25m }
        };

        /// <summary>
        /// Calculates the tax owed based on annual income.
        /// </summary>
        /// <param name="req">The tax request containing annual income.</param>
        /// <returns>The calculated tax result.</returns>
        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] TaxRequest req)
        {
            if (req == null) return BadRequest();
            var income = req.AnnualIncome;
            if (income < 0) return BadRequest("Income must be non-negative");

            decimal tax = 0m;
            decimal previousThreshold = 0m;

            foreach (var bracket in Brackets)
            {
                var upper = bracket.Threshold;
                var slice = Math.Max(0m, Math.Min(income, upper) - previousThreshold);
                if (slice > 0)
                {
                    tax += slice * bracket.Rate;
                }

                previousThreshold = upper;

                if (income <= upper) break;
            }

            var result = new TaxResult
            {
                AnnualIncome = income,
                TaxOwed = Math.Round(tax, 2),
                NetIncome = Math.Round(income - tax, 2)
            };

            return Ok(result);
        }
    }
}
