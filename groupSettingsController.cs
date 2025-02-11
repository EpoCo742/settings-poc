using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GroupSettingsApi.Models;
using GroupSettingsApi.Repositories;

namespace GroupSettingsApi.Controllers
{
    [Route("groups/{groupId}/settings")]
    [ApiController]
    public class GroupSettingsController : ControllerBase
    {
        private readonly GroupSettingsRepository _repository;

        public GroupSettingsController(GroupSettingsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSettings(int groupId)
        {
            var settings = await _repository.GetAllSettingsAsync(groupId);
            return Ok(new { data = settings });
        }

        [HttpGet("{settingKey}")]
        public async Task<IActionResult> GetSetting(int groupId, string settingKey)
        {
            var setting = await _repository.GetSettingAsync(groupId, settingKey);
            if (setting == null)
            {
                return NotFound();
            }

            return Ok(new { data = setting });
        }

        [HttpPost]
        public async Task<IActionResult> CreateSetting(int groupId, [FromBody] GroupSettingCreateDto dto)
        {
            var setting = new GroupSetting
            {
                GroupId = groupId,
                SettingKey = dto.SettingKey,
                SettingValue = dto.SettingValue,
                ValueType = dto.ValueType,
                CreatedBy = dto.CreatedBy
            };

            await _repository.CreateSettingAsync(setting);

            return CreatedAtAction(nameof(GetSetting), new { groupId, settingKey = dto.SettingKey }, new { data = setting });
        }

        [HttpPatch("{settingKey}")]
        public async Task<IActionResult> UpdateSetting(int groupId, string settingKey, [FromBody] GroupSettingUpdateDto dto)
        {
            var setting = await _repository.GetSettingAsync(groupId, settingKey);
            if (setting == null)
            {
                return NotFound();
            }

            setting.SettingValue = dto.SettingValue;
            setting.ValueType = dto.ValueType;
            setting.LastUpdatedDate = System.DateTime.UtcNow;

            await _repository.UpdateSettingAsync(setting);

            return Ok(new { data = setting });
        }
    }
}
