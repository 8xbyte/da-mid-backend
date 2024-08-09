using DaMid.Attributes;
using DaMid.Interfaces.Data;
using DaMid.Models;
using DaMid.Services;
using Microsoft.AspNetCore.Mvc;

namespace DaMid.Controllers {
    [Route("api/classes")]
    [ApiController]
    public class ClassController(IClassService classService, ISubjectService subjectService, ITeacherService teacherService, IAudienceService audienceService, IGroupService groupService) : ControllerBase {
        private readonly IClassService _classService = classService;
        private readonly ISubjectService _subjectService = subjectService;
        private readonly ITeacherService _teacherService = teacherService;
        private readonly IAudienceService _audienceService = audienceService;
        private readonly IGroupService _groupService = groupService;

        [HttpGet("get")]
        public async Task<ActionResult> GetClassesAsync(DateOnly beginDate, DateOnly endDate, int groupId) {
            return Ok(new {
                Status = "ok",
                Result = await _classService.GetClassesByDateIntervalAndGroupIdAsync(beginDate, endDate, groupId)
            });
        }

        [HttpPost("add")]
        [Authorization(UserRole.Admin)]
        public async Task<ActionResult> AddClassAsync([FromBody] IAddClassData classData) {
            var subject = await _subjectService.GetSubjectByIdAsync(classData.SubjectId);
            if (subject == null) {
                return BadRequest(new {
                    Status = "error",
                    Subject = "Предмет не найден"
                });
            }

            var audience = await _audienceService.GetAudienceByIdAsync(classData.AudienceId);
            if (audience == null) {
                return BadRequest(new {
                    Status = "error",
                    Subject = "Аудитория не найдена"
                });
            }

            var teacher = await _teacherService.GetTeacherByIdAsync(classData.TeacherId);
            if (teacher == null) {
                return BadRequest(new {
                    Status = "error",
                    Subject = "Преподаватель не найден"
                });
            }

            var group = await _groupService.GetGroupByIdAsync(classData.GroupId);
            if (group == null) {
                return BadRequest(new {
                    Status = "error",
                    Subject = "Группа не найден"
                });
            }

            await _classService.AddClassAsync(new ClassModel {
                SubjectId = subject.Id,
                AudienceId = audience.Id,
                TeacherId = teacher.Id,
                GroupId = group.Id,
                Date = classData.Date,
                Time = classData.Time
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