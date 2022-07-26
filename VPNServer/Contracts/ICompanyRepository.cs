﻿using VPNServer.Dto;
using VPNServer.Entities;

namespace VPNServer.Contracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompanyById(int id);
        public Task<Company> CreateCompany(CompanyForCreationDto company);
        public Task UpdateCompany(int id,CompanyForUpdateDto company);
        public Task DeleteCompany(int id);

        




    }
}
