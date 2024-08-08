using DaMid.Attributes;
using DaMid.Interfaces.Data;
using DaMid.Models;
using DaMid.Services;
using Microsoft.AspNetCore.Mvc;

namespace DaMid.Controllers {
    [Route("api/classes")]
    [ApiController]
    public class ClassController(IClassService classService) : ControllerBase {
        private readonly IClassService _classService = classService;

        [HttpGet("get")]
        [Authorization(UserRole.User)]
        public async Task<ActionResult> GetClassesAsync(int offset = 0, int limit = 10) {
            return Ok(new {
                Status = "ok",
                Result = await _classService.GetClassesAsync(offset, limit)
            });
        }

        [HttpGet("search")]
        [Authorization(UserRole.User)]
        public async Task<ActionResult> SearchClassesAsync(string name, int limit = 10) {
            return Ok(new {
                Status = "ok",
                Result = await _classService.SearchClassesAsync(name, limit)
            });
        }

        [HttpPost("add")]
        [Authorization(UserRole.Admin)]
        public async Task<ActionResult> AddClassAsync([FromBody] IAddClassData classData) {
            var _class = await _classService.GetClassByNameAsync(classData.Name);
            if (_class != null) {
                return BadRequest(new {
                    Status = "error",
                    Message = "Занятие уже существует"
                });
            }

            await _classService.AddClassAsync(new ClassModel {
                Name = classData.Name
            });

            return Ok(new {
                Status = "ok"
            });
        }

        [HttpPost("remove")]
        [Authorization(UserRole.Admin)]
        public async Task<ActionResult> RemoveClassAsync([FromBody] IRemoveClassData classData) {
            var _class = await _classService.GetClassByIdAsync(classData.Id);
            if (_class == null) {
                return BadRequest(new {
                    Status = "error",
                    Message = "Занятие не найдено"
                });
            }

            await _classService.RemoveClassAsync(_class);

            return Ok(new {
                Status = "ok"
            });
        }
    }
}