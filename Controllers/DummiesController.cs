using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Requests;
using WebApi.Models.Responses;
using WebApi.Services.V1;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummiesController : ControllerBase
    {
        private readonly IDummyService _dummyService;

        public DummiesController(
            IDummyService dummyService
            )
        {
            _dummyService = dummyService;
        }

        //[Authorize(Roles = Role.Admin)]
        [HttpPost]
        public ActionResult<DummyResponse> CreateDummy(CreateDummyRequest createDummyRequest)
        {
            var dummy = _dummyService.CreateDummy(createDummyRequest);
            return Ok(dummy);
        }

        [HttpGet]
        public ActionResult<IEnumerable<DummyResponse>> FindAllDummies()
        {
            var dummy = _dummyService.FindAllDummies();
            return Ok(dummy);
        }
    }
}
