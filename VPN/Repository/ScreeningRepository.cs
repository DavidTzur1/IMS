using Dapper;
using System.Data;
using VPN.Context;
using VPN.Contracts;
using VPN.DTO;
using VPN.Entities;

namespace VPN.Repository
{
    public class ScreeningRepository : IScreeningRepository
    {
        private readonly DapperContext _context;
        public ScreeningRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Screening> CreateScreening(ScreeningForCreationDto screening)
        {
            var procedureName = "CreateScreening";
            var parameters = new DynamicParameters();
            parameters.Add("Name", screening.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("CompanyID", screening.CompanyID, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                var createdScreening = new Screening
                {
                    ID = id,
                    Name = screening.Name,
                    CompanyID = screening.CompanyID,

                };
                return createdScreening;
            }
        }

        public async Task DeleteScreening(int id)
        {
            var procedureName = "DeleteScreening";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Screening>> GetScreeningsByCompanyID(int companyID)
        {
            var procedureName = "GetScreeningsByCompanyID";
            using (var connection = _context.CreateConnection())
            {
                var screenings = await connection.QueryAsync<Screening>(procedureName, new { companyID }, commandType: CommandType.StoredProcedure);
                return screenings.ToList();
            }
        }

        public async Task<Screening> GetScreening(int id)
        {
            var procedureName = "GetScreening";
            using (var connection = _context.CreateConnection())
            {
                var screening = await connection.QuerySingleOrDefaultAsync<Screening>(procedureName, new { id }, commandType: CommandType.StoredProcedure);
                return screening;
            }
        }

      

        public async Task UpdateScreening(int id, ScreeningForUpdateDto screening)
        {
            var procedureName = "UpdateScreening";
            var parameters = new DynamicParameters();
            parameters.Add("ID", id, DbType.Int32);
            parameters.Add("Name", screening.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("CompanyID", screening.CompanyID, DbType.Int32, ParameterDirection.Input);
            
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}

