using CalculatorApi.Models;
using Microsoft.AspNetCore.Mvc;
namespace CalculatorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        [HttpPost("evaluate")]
        public IActionResult Evaluate([FromBody] ExpressionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Expression))
            {
                return BadRequest(new CalculatorResponse
                {
                    Success = false,
                    ErrorMessage = "Пустое выражение"
                });
            }

            try
            {
                // A quick way using DataTable (not recommended for prod, but fine for demo)
                var result = new System.Data.DataTable().Compute(request.Expression, null);

                return Ok(new CalculatorResponse
                {
                    Result = Convert.ToDouble(result),
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new CalculatorResponse
                {
                    Success = false,
                    ErrorMessage = $"Ошибка разбора выражения: {ex.Message}"
                });
            }
        }

    }
}