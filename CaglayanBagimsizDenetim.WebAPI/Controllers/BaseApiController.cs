using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CaglayanBagimsizDenetim.Application.Wrappers;
using CaglayanBagimsizDenetim.WebAPI.Factories;

namespace CaglayanBagimsizDenetim.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        // ServiceResult (Verisiz) dönüşümü
        // Dictionary-based factory kullanarak if statement'ları elimine ettik
        [NonAction]
        public IActionResult CreateActionResult(ServiceResult result)
        {
            return ActionResultFactory.CreateResult(result, this);
        }

        // ServiceResult<T> (Verili) dönüşümü
        [NonAction]
        public IActionResult CreateActionResult<T>(ServiceResult<T> result)
        {
            return ActionResultFactory.CreateResult(result, this);
        }
    }
}
