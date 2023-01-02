﻿using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal sealed class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        

        public async Task<Employee> GetEmployeeAsync(Guid companyid, Guid id, bool trackChanges) => 
            await FindByCondition(e => e.CompanyId.Equals(companyid) && e.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges) => 
            await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
                .OrderBy(e => e.Name)
                .ToListAsync();

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }
    }
}
