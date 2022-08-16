using Dapper;
using System.Data;
using VPNServer.Context;
using VPNServer.Contracts;
using VPNServer.Dto;
using VPNServer.Entities;

namespace VPNServer.Repository
{

    public class ScreeningItemRepository : IScreeningItemRepository
    {
        private readonly DapperContext _context;
        public ScreeningItemRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<ScreeningItem> CreateScreeningItem(ScreeningItemForCreationDto screeningItem)
        {
            var procedureName = "CreateScreeningItem";
            var parameters = new DynamicParameters();
            parameters.Add("Name", screeningItem.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("ScreeningId", screeningItem.ScreeningId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Number", screeningItem.Number, DbType.String, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                var createdScreeningItem = new ScreeningItem
                {
                    Id = id,
                    Name = screeningItem.Name,
                    ScreeningId = screeningItem.ScreeningId,
                    Number = screeningItem.Number

                };
                return createdScreeningItem;
            }
        }

        public async Task DeleteScreeningItem(int id)
        {
            var procedureName = "DeleteScreeningItem";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ScreeningItem>> GetScreeningItemByScreeningId(int screeningId)
        {
            var procedureName = "GetScreeningItemByScreeningId";
            using (var connection = _context.CreateConnection())
            {
                var screeningsData = await connection.QueryAsync<ScreeningItem>(procedureName, new { screeningId }, commandType: CommandType.StoredProcedure);
                return screeningsData.ToList();
            }
        }

        public async Task<ScreeningItem> GetScreeningItem(int id)
        {
            var procedureName = "GetScreeningItem";
            using (var connection = _context.CreateConnection())
            {
                var screeningData = await connection.QuerySingleOrDefaultAsync<ScreeningItem>(procedureName, new { id }, commandType: CommandType.StoredProcedure);
                return screeningData;
            }
        }



        public async Task UpdateScreeningItem(int id, ScreeningItemForUpdateDto screeningItem)
        {
            var procedureName = "UpdateScreeningItem";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", screeningItem.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("ScreeningId", screeningItem.ScreeningId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Number", screeningItem.Number, DbType.String, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
