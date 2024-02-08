using Application.Repositories.Department;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
    

        public DepartmentController(IDepartmentReadRepo readRepo, IDepartmentWriteRepo writeRepo)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
        }

        readonly private IDepartmentWriteRepo _writeRepo;
        readonly private IDepartmentReadRepo _readRepo;

        [HttpGet]
        public IActionResult GetAll()
        {
            var Departments = _readRepo.GetAll();
            return Ok(Departments);
        }

        [HttpPost]
        public async Task Get()
        {
            await _writeRepo.AddRangeAsync(new()
            {
                new(){Id = Guid.NewGuid(), DepartmentName="Ataşehir"},
                new(){Id = Guid.NewGuid(), DepartmentName="Pendik"},
                new(){Id = Guid.NewGuid(), DepartmentName="Avcılar"},
                new(){Id = Guid.NewGuid(), DepartmentName="Sarıyer"},
                new(){Id = Guid.NewGuid(), DepartmentName="Kadıköy"},
                new(){Id = Guid.NewGuid(), DepartmentName="Fatih"},
                new(){Id = Guid.NewGuid(), DepartmentName="Bağcılar"},
            });
            await _writeRepo.SaveAsync();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var department = await _readRepo.GetByIdAsync(id);
            return Ok(department);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Update(string id)
        {
            bool department = await _writeRepo.UpdateStatus(id);
            await _writeRepo.SaveAsync();
            return Ok(department);
        }


    }
}
