using DaMid.Attributes;
using DaMid.Interfaces.Data;
using DaMid.Models;
using DaMid.Services;
using Microsoft.AspNetCore.Mvc;

namespace DaMid.Controllers {
    [Route("api/subjects")]
    [ApiController]
    public class SubjectController(ISubjectService subjectService) : ControllerBase {
        private readonly ISubjectService _subjectService = subjectService;

        [HttpGet("get")]
        public async Task<ActionResult> GetSubjectsAsync(int offset = 0, int limit = 10) {
            return Ok(new {
                Status = "ok",
                Result = await _subjectService.GetSubjectsAsync(offset, limit)
            });
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchSubjectsAsync(string name, int offset = 0, int limit = 10) {
            return Ok(new {
                Status = "ok",
                Result = await _subjectService.SearchSubjectsAsync(name, offset, limit)
            });
        }

        [HttpPost("add")]
        [Authorization(UserRole.Admin)]
        public async Task<ActionResult> AddSubjectAsync([FromBody] IAddSubjectData classData) {
            var _class = await _subjectService.GetSubjectByNameAsync(classData.Name);
            if (_class != null) {
                return BadRequest(new {
                    Status = "error",
                    Message = "Предмет уже существует"
                });
            }

            await _subjectService.AddSubjectAsync(new SubjectModel {
                Name = classData.Name
            });

            return Ok(new {
                Status = "ok"
            });
        }

        [HttpPost("remove")]
        [Authorization(UserRole.Admin)]
        public async Task<ActionResult> RemoveSubjectAsync([FromBody] IRemoveSubjectData classData) {
            var _class = await _subjectService.GetSubjectByIdAsync(classData.Id);
            if (_class == null) {
                return BadRequest(new {
                    Status = "error",
                    Message = "Предмет не найдено"
                });
            }

            await _subjectService.RemoveSubjectAsync(_class);

            return Ok(new {
                Status = "ok"
            });
        }
    }
}