using Dapper;
using System.Data;
using VPNServer.Context;
using VPNServer.Contracts;
using VPNServer.Dto;
using VPNServer.Entities;

namespace VPNServer.Repository
{

    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;

        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "SELECT * FROM Companies";

            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }

        public async Task<Company> GetCompanyById(int id)
        {
            var query = "SELECT * FROM Companies WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });

                return company;
            }
        }

        public async Task<Company> CreateCompany(CompanyForCreationDto company)
        {
            var procedureName = "CreateCompany";
            var parameters = new DynamicParameters();
            parameters.Add("Name", company.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("BillingID", company.BillingId, DbType.String, ParameterDirection.Input);
            parameters.Add("IsDeactivated", company.IsDeactivated, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("IsForceOnNet", company.IsForceOnNet, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("IsCompanyCallsOnly", company.IsCompanyCallsOnly, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("BarringList", company.BarringList, DbType.String, ParameterDirection.Input);
            parameters.Add("AllowanceList", company.AllowanceList, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                var createdCompany = new Company
                {
                    Id = id,
                    Name = company.Name,
                    BillingId = company.BillingId,
                    IsDeactivated = company.IsDeactivated,
                    IsForceOnNet = company.IsForceOnNet,
                    IsCompanyCallsOnly = company.IsCompanyCallsOnly,
                    AllowanceList = company.AllowanceList,
                    BarringList = company.BarringList


                };
                return createdCompany;
            }
        }

        public async Task UpdateCompany(int id, CompanyForUpdateDto company)
        {

            var procedureName = "UpdateCompany";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", company.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("BillingID", company.BillingId, DbType.String, ParameterDirection.Input);
            parameters.Add("IsDeactivated", company.IsDeactivated, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("IsForceOnNet", company.IsForceOnNet, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("IsCompanyCallsOnly", company.IsCompanyCallsOnly, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("BarringList", company.BarringList, DbType.String, ParameterDirection.Input);
            parameters.Add("AllowanceList", company.AllowanceList, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task DeleteCompany(int id)
        {

            var procedureName = "DeleteCompany";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, new { id }, commandType: CommandType.StoredProcedure);
            }
        }

      
    }
}
