using Dapper;
using System.Data;
using VPN.Context;
using VPN.Contracts;
using VPN.DTO;
using VPN.Entities;

namespace VPN.Repository
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

            var procedureName = "GetCompanies";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(procedureName,null,commandType: CommandType.StoredProcedure);
                return companies.ToList();
            }
        }

        public async Task<Company> GetCompany(int id)
        {
            var procedureName = "GetCompany";
            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Company>(procedureName, new { id }, commandType: CommandType.StoredProcedure);
                return company;
            }
        }

        public async Task<Company> CreateCompany(CompanyForCreationDto company)
        {

            var procedureName = "CreateCompany";
            var parameters = new DynamicParameters();
            parameters.Add("Name", company.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("BillingID", company.BillingID, DbType.String, ParameterDirection.Input);
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
                    ID = id,
                    Name = company.Name,
                    BillingID = company.BillingID,
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
            parameters.Add("ID", id, DbType.Int32);
            parameters.Add("Name", company.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("BillingID", company.BillingID, DbType.String, ParameterDirection.Input);
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



