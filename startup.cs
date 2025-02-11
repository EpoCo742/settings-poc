builder.Services.AddScoped<IDbConnection>(sp => new Oracle.ManagedDataAccess.Client.OracleConnection("Your Oracle Connection String"));
builder.Services.AddScoped<GroupSettingsRepository>();
