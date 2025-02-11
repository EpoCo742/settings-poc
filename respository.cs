using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace GroupSettingsApi.Repositories
{
    public class GroupSettingsRepository
    {
        private readonly IDbConnection _dbConnection;

        public GroupSettingsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<GroupSetting>> GetAllSettingsAsync(int groupId)
        {
            const string query = "SELECT * FROM GroupSettings WHERE GroupId = :GroupId";
            return await _dbConnection.QueryAsync<GroupSetting>(query, new { GroupId = groupId });
        }

        public async Task<GroupSetting> GetSettingAsync(int groupId, string settingKey)
        {
            const string query = "SELECT * FROM GroupSettings WHERE GroupId = :GroupId AND SettingKey = :SettingKey";
            return await _dbConnection.QuerySingleOrDefaultAsync<GroupSetting>(query, new { GroupId = groupId, SettingKey = settingKey });
        }

        public async Task CreateSettingAsync(GroupSetting setting)
        {
            const string query = @"
                INSERT INTO GroupSettings (GroupId, SettingKey, SettingValue, ValueType, CreatedBy, CreatedDate, LastUpdatedDate)
                VALUES (:GroupId, :SettingKey, :SettingValue, :ValueType, :CreatedBy, SYSTIMESTAMP, SYSTIMESTAMP)";
            await _dbConnection.ExecuteAsync(query, setting);
        }

        public async Task UpdateSettingAsync(GroupSetting setting)
        {
            const string query = @"
                UPDATE GroupSettings
                SET SettingValue = :SettingValue, ValueType = :ValueType, LastUpdatedDate = SYSTIMESTAMP
                WHERE GroupId = :GroupId AND SettingKey = :SettingKey";
            await _dbConnection.ExecuteAsync(query, setting);
        }
    }
}
