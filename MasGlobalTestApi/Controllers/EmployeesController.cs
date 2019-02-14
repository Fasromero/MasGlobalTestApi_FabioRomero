using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using MasGlobalTestApi.DTO;
using MasGlobalTestApi.Business.Interface;
using MasGlobalTestApi.Business;

namespace MasGlobalTestApi.Controllers
{
    public class EmployeesController : ApiController
    {
        IEmployeeBusiness EmployeeHandler = new EmployeeBusiness();

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
        {
            IEnumerable<EmployeeDTO> allEmployees = await EmployeeHandler.LoadAllEmployeesAsync();

            return allEmployees;
        }

        public async Task<IHttpActionResult> GetEmployee(string id)
        {
            IEnumerable<EmployeeDTO> employeesToFilter = await EmployeeHandler.LoadAllEmployeesAsync();
            var filteredEmployee = employeesToFilter.FirstOrDefault((e) => e.Id == id);
            if (filteredEmployee == null)
            {
                return NotFound();
            } else
            {
                return Ok(filteredEmployee);
            }
        }

    }
}
