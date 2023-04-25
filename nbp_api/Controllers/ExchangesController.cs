using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nbp_api.Services;
using System.Globalization;
using System.Xml.Linq;

namespace nbp_api.Controllers
{
    [Route("exchanges")]
    [ApiController]
    public class ExchangesController : ControllerBase
    {
        private readonly IBankService _bankService;
        public ExchangesController(IBankService bankService) 
        { 
            _bankService = bankService;
        }

        [Route("rate/{code}/{date}")]
        [HttpGet]
        public async Task<IActionResult> GetAvgRate(string code, string date)
        {
            DateOnly dateParsed;
            try
            { 
                dateParsed = DateOnly.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture); 
            }
            catch (System.FormatException)
            {
                return BadRequest("Wrong date format provided");
            }

            var toRet = await _bankService.getRateOnDate(code, dateParsed);
            if (toRet is not null)
                return Ok(toRet);
            else
                return NotFound();
        }

        [Route("min-max/{code}/{numberOfQuotations}")]
        [HttpGet]
        public async Task<IActionResult> GetMinMax(string code, int numberOfQuotations)
        {
            if (numberOfQuotations <= 0 || numberOfQuotations > 255) {
                return BadRequest("Number of quotations must be in range (1, 255)");
            }

            var toRet = await _bankService.getMinMax(code, numberOfQuotations);
            if (toRet is not null)
                return Ok(toRet);
            else
                return NotFound();
        }

        [Route("diff/{code}/{numberOfQuotations}")]
        [HttpGet]
        public async Task<IActionResult> GetDiff(string code, int numberOfQuotations)
        {
            if (numberOfQuotations <= 0 || numberOfQuotations > 255)
            {
                return BadRequest("Number of quotations must be in range (1, 255)");
            }

            var toRet = await _bankService.getDiff(code, numberOfQuotations);
            if (toRet is not null)
                return Ok(toRet);
            else
                return NotFound();
        }
    }
}
