using DaMid.Attributes;
using DaMid.Interfaces.Data;
using DaMid.Models;
using DaMid.Services;
using Microsoft.AspNetCore.Mvc;

namespace DaMid.Controllers {
    [Route("api/teachers")]
    [ApiController]
    public class TeacherController(ITeacherService teacherService) : ControllerBase {
        private readonly ITeacherService _teacherService = teacherService;

        [HttpGet("get")]
        public async Task<ActionResult> GetClassesAsync(int offset = 0, int limit = 10) {
            return Ok(new {
                Status = "ok",
                Result = await _teacherService.GetTeachersAsync(offset, limit)
            });
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchClassesAsync(string name, string surname, int limit = 10) {
            return Ok(new {
                Status = "ok",
                Result = await _teacherService.SearchTeachersAsync(name, surname, limit)
            });
        }

        [HttpPost("add")]
        [Authorization(UserRole.Admin)]
        public async Task<ActionResult> AddClassAsync([FromBody] IAddTeacherData teacherData) {
            var teacher = await _teacherService.GetTeacherByNameAndSurnameAsync(teacherData.Name, teacherData.Surname);
            if (teacher != null) {
                return BadRequest(new {
                    Status = "error",
                    Message = "Преподаватель уже существует"
                });
            }

            await _teacherService.AddTeacherAsync(new TeacherModel {
                Name = teacherData.Name,
                Surname = teacherData.Surname
            });

            return Ok(new {
                Status = "ok"
            });
        }

        [HttpPost("remove")]
        [Authorization(UserRole.Admin)]
        public async Task<ActionResult> RemoveClassAsync([FromBody] IRemoveTeacherData teacherData) {
            var teacher = await _teacherService.GetTeacherByIdAsync(teacherData.Id);
            if (teacher == null) {
                return BadRequest(new {
                    Status = "error",
                    Message = "Преподаватель не найден"
                });
            }

            await _teacherService.RemoveTeacherAsync(teacher);

            return Ok(new {
                Status = "ok"
            });
        }
    }
}