using System;
using System.ComponentModel.DataAnnotations;

namespace GroupSettingsApi.Models
{
    public class GroupSetting
    {
        [Required]
        public int GroupId { get; set; }

        [Required]
        public string SettingKey { get; set; }

        [Required]
        public string SettingValue { get; set; }

        [Required]
        public string ValueType { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }

    public class GroupSettingCreateDto
    {
        [Required]
        public string SettingKey { get; set; }

        [Required]
        public string SettingValue { get; set; }

        [Required]
        public string ValueType { get; set; }

        [Required]
        public int CreatedBy { get; set; }
    }

    public class GroupSettingUpdateDto
    {
        [Required]
        public string SettingValue { get; set; }

        [Required]
        public string ValueType { get; set; }

        [Required]
        public int LastUpdatedBy { get; set; }
    }
}
