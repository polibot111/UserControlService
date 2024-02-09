using Application.CQRS.Department;
using Application.Repositories.Department;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        readonly private IDepartmentService _departmentService;



        [HttpGet("{Page}/{Size}")]
        public async Task<IActionResult> GetAllWithPagination([FromRoute] DepartmentDetailPaginationQuery request)
        {
            return Ok(await _departmentService.GetAllWithPaginationAsync(request));
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _departmentService.GetAllAsync());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] DepartmentDetailQuery request)
        {
            return Ok(await _departmentService.GetByIdAsync(request));
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentInsertCommand request)
        {
            return Ok(await _departmentService.AddAsync(request));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentUpdateCommand request)
        {
            return Ok(await _departmentService.UpdateAsync(request));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDepartment([FromBody] DepartmentUpdateStatusCommand request)
        {
            return Ok(await _departmentService.UpdateStatusAsync(request));
        }


    }
}
