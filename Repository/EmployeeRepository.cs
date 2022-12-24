using Contracts;
using Entities.Models;

namespace Repository
{
    internal sealed class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Employee GetEmployee(Guid companyid, Guid id, bool trackChanges) => 
            FindByCondition(e => e.CompanyId.Equals(companyid) && e.Id.Equals(id), false).SingleOrDefault();

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges) => 
            FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
                .OrderBy(e => e.Name)
                .ToList();
    }
}
