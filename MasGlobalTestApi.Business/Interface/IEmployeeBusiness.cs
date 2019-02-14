using MasGlobalTestApi.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasGlobalTestApi.Business.Interface
{
    public interface IEmployeeBusiness
    {   
        Task<IEnumerable<EmployeeDTO>> LoadAllEmployeesAsync();
    }

    public interface IFactory<TFactory>
    {
        TProduct Build<TProduct>() where TProduct : IProduct<TFactory>, new();
    }

    public interface IProduct<TFactory>
    {
        string CalculateAnnualSalary(string salary);
    }
}
