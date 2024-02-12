using Application.Consts.Authorize;
using Application.CQRS.Persistence.Department;
using Application.CustomAttributes;
using Application.Enums;
using Application.Repositories.Department;
using Application.Services.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class DepartmentController : ControllerBase
    {

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        readonly private IDepartmentService _departmentService;



        [HttpGet("{Page}/{Size}")]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.Department, ActionType = ActionType.ReadingAll,
            Definition = "Read All Department With Pagination")]
        public async Task<IActionResult> GetAllWithPagination([FromRoute] DepartmentDetailPaginationQuery request)
        {
            return Ok(await _departmentService.GetAllWithPaginationAsync(request));
        }


        [HttpGet]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.Department, ActionType = ActionType.ReadingAll,
            Definition = "Read All Department")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _departmentService.GetAllAsync());
        }

        [HttpGet("{Id}")]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.Department, ActionType = ActionType.ReadingById,
            Definition = "Read Department")]
        public async Task<IActionResult> GetById([FromRoute] DepartmentDetailQuery request)
        {
            return Ok(await _departmentService.GetByIdAsync(request));
        }

        [HttpPost]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.Department, ActionType = ActionType.Writing,
            Definition = "Create Department")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentInsertCommand request)
        {
            return Ok(await _departmentService.AddAsync(request));
        }

        [HttpPut]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.Department, ActionType = ActionType.Updating,
            Definition = "Update Department")]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentUpdateCommand request)
        {
            return Ok(await _departmentService.UpdateAsync(request));
        }

        [HttpDelete]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.Department, ActionType = ActionType.Deleting,
            Definition = "Delete Department")]
        public async Task<IActionResult> DeleteDepartment([FromBody] DepartmentUpdateStatusCommand request)
        {
            return Ok(await _departmentService.UpdateStatusAsync(request));
        }


    }
}
