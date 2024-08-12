using DaMid.Attributes;
using DaMid.Interfaces.Data;
using DaMid.Models;
using DaMid.Services;
using Microsoft.AspNetCore.Mvc;

namespace DaMid.Controllers {
    [Route("api/audiences")]
    [ApiController]
    public class AudienceController(IAudienceService audienceService) : ControllerBase {
        private readonly IAudienceService _audienceService = audienceService;

        [HttpGet("get")]
        public async Task<ActionResult> GetAudiencesAsync(int offset = 0, int limit = 10) {
            return Ok(new {
                Status = "ok",
                Result = await _audienceService.GetAudiencesAsync(offset, limit)
            });
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchAudiencesAsync(string name, int offset = 0, int limit = 10) {
            return Ok(new {
                Status = "ok",
                Result = await _audienceService.SearchAudiencesAsync(name, offset, limit)
            });
        }

        [HttpPost("add")]
        [Authorization(UserRole.Admin)]
        public async Task<ActionResult> AddAudienceAsync([FromBody] IAddAudienceData audienceData) {
            var audience = await _audienceService.GetAudienceByNameAsync(audienceData.Name);
            if (audience != null) {
                return BadRequest(new {
                    Status = "error",
                    Message = "Аудитория уже существует"
                });
            }

            return Ok(new {
                Status = "ok",
                Result = await _audienceService.AddAudienceAsync(new AudienceModel {
                    Name = audienceData.Name
                })
            });
        }

        [HttpPost("remove")]
        [Authorization(UserRole.Admin)]
        public async Task<ActionResult> RemoveAudienceAsync([FromBody] IRemoveAudienceData audienceData) {
            var audience = await _audienceService.GetAudienceByIdAsync(audienceData.Id);
            if (audience == null) {
                return BadRequest(new {
                    Status = "error",
                    Message = "Аудитория не найдена"
                });
            }

            return Ok(new {
                Status = "ok",
                Result = await _audienceService.RemoveAudienceAsync(audience)
            });
        }
    }
}