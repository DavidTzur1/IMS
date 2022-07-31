using Dapper;
using System.Data;
using VPN.Context;
using VPN.Contracts;
using VPN.DTO;
using VPN.Entities;

namespace VPN.Repository
{
    public class ScreeningDataRepository : IScreeningDataRepository
    {
        private readonly DapperContext _context;
        public ScreeningDataRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<ScreeningData> CreateScreeningData(ScreeningDataForCreationDto screeningData)
        {
            var procedureName = "CreateScreeningData";
            var parameters = new DynamicParameters();
            parameters.Add("Name", screeningData.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("ScreeningID", screeningData.ScreeningID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Number", screeningData.Number, DbType.String, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                var createdScreeningData = new ScreeningData
                {
                    ID = id,
                    Name = screeningData.Name,
                    ScreeningID = screeningData.ScreeningID,
                    Number = screeningData.Number

                };
                return createdScreeningData;
            }
        }

        public async Task DeleteScreeningData(int id)
        {
            var procedureName = "DeleteScreeningData";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ScreeningData>> GetScreeningDataByScreeningID(int screeningID)
        {
            var procedureName = "GetScreeningDataByScreeningID";
            using (var connection = _context.CreateConnection())
            {
                var screeningsData = await connection.QueryAsync<ScreeningData>(procedureName, new { screeningID }, commandType: CommandType.StoredProcedure);
                return screeningsData.ToList();
            }
        }

        public async Task<ScreeningData> GetScreeningData(int id)
        {
            var procedureName = "GetScreeningData";
            using (var connection = _context.CreateConnection())
            {
                var screeningData = await connection.QuerySingleOrDefaultAsync<ScreeningData>(procedureName, new { id }, commandType: CommandType.StoredProcedure);
                return screeningData;
            }
        }



        public async Task UpdateScreeningData(int id, ScreeningDataForUpdateDto screeningData)
        {
            var procedureName = "UpdateScreeningData";
            var parameters = new DynamicParameters();
            parameters.Add("ID", id, DbType.Int32);
            parameters.Add("Name", screeningData.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("ScreeningID", screeningData.ScreeningID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Number", screeningData.Number, DbType.String, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
