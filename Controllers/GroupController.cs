using DaMid.Attributes;
using DaMid.Interfaces.Data;
using DaMid.Models;
using DaMid.Services;
using Microsoft.AspNetCore.Mvc;

namespace DaMid.Controllers {
    [Route("api/groups")]
    [ApiController]
    public class GroupController(IGroupService groupService) : ControllerBase {
        private readonly IGroupService _groupService = groupService;

        [HttpGet("get")]
        public async Task<ActionResult> GetGroupsAsync(int offset = 0, int limit = 10) {
            return Ok(new {
                Status = "ok",
                Result = await _groupService.GetGroupsAsync(offset, limit)
            });
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchGroupsAsync(string name, int offset = 0, int limit = 10) {
            return Ok(new {
                Status = "ok",
                Result = await _groupService.SearchGroupsAsync(name, offset, limit)
            });
        }

        [HttpPost("add")]
        [Authorization(UserRole.Admin)]
        public async Task<ActionResult> AddGroupAsync([FromBody] IAddGroupData groupData) {
            var group = await _groupService.GetGroupByNameAsync(groupData.Name);
            if (group != null) {
                return BadRequest(new {
                    Status = "error",
                    Message = "Группа уже существует"
                });
            }

            await _groupService.AddGroupAsync(new GroupModel {
                Name = groupData.Name
            });

            return Ok(new {
                Status = "ok"
            });
        }

        [HttpPost("remove")]
        [Authorization(UserRole.Admin)]
        public async Task<ActionResult> RemoveGroupAsync([FromBody] IRemoveGroupData groupData) {
            var group = await _groupService.GetGroupByIdAsync(groupData.Id);
            if (group == null) {
                return BadRequest(new {
                    Status = "error",
                    Message = "Группа не найдено"
                });
            }

            await _groupService.RemoveGroupAsync(group);

            return Ok(new {
                Status = "ok"
            });
        }
    }
}