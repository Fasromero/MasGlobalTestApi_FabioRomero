using MasGlobalTestApi.Business.Interface;
using System;
using System.Collections.Generic;
using MasGlobalTestApi.DTO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MasGlobalTestApi.DataAccessHandler.Api;

namespace MasGlobalTestApi.Business
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        
        List<EmployeeDTO> EmployeesDTO = new List<EmployeeDTO>();

        public async Task<IEnumerable<EmployeeDTO>> LoadAllEmployeesAsync()
        {
            Factory<CalculateSalary> CalculateSalaryFactory = new Factory<CalculateSalary>();

            DataAccessLayer data = new DataAccessLayer();
            Uri uri = new Uri("http://masglobaltestapi.azurewebsites.net/");
            await data.CallWebAPIAsync(uri, "api/Employees");
            string serializedResponse = data.SerializedResponse;
            if (serializedResponse != "" && serializedResponse != "NotFound")
            {
                List<Dictionary<string, string>> deserializedResponse = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(serializedResponse);
                if (deserializedResponse.Count > 0)
                {
                    foreach (Dictionary<string, string> row in deserializedResponse)
                    {
                        EmployeeDTO employeeDTO = new EmployeeDTO();
                        foreach (KeyValuePair<string, string> item in row)
                        {
                            switch (item.Key)
                            {
                                case "id":
                                    employeeDTO.Id = item.Value;
                                    break;
                                case "name":
                                    employeeDTO.Name = item.Value;
                                    break;
                                case "contractTypeName":
                                    employeeDTO.ContractTypeName = item.Value;
                                    break;
                                case "roleId":
                                    employeeDTO.RoleId = item.Value;
                                    break;
                                case "roleName":
                                    employeeDTO.RoleName = item.Value;
                                    break;
                                case "roleDescription":
                                    employeeDTO.RoleDescription = item.Value;
                                    break;
                                case "hourlySalary":
                                    employeeDTO.HourlySalary = item.Value;
                                    break;
                                case "monthlySalary":
                                    employeeDTO.MonthlySalary = item.Value;
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (employeeDTO.ContractTypeName == "MonthlySalaryEmployee")
                        {
                            MonthlySalary monthlySalary = CalculateSalaryFactory.Create<MonthlySalary>();
                            employeeDTO.AnnualSalary = monthlySalary.CalculateAnnualSalary(employeeDTO.MonthlySalary);
                        } else if (employeeDTO.ContractTypeName == "HourlySalaryEmployee")
                        {
                            HourlySalary hourlySalary = CalculateSalaryFactory.Create<HourlySalary>();
                            employeeDTO.AnnualSalary = hourlySalary.CalculateAnnualSalary(employeeDTO.HourlySalary);
                        }
                        EmployeesDTO.Add(employeeDTO);
                    }
                }
            }
            return EmployeesDTO;
        }

        class Factory<TFactory> where TFactory : IFactory<TFactory>, new()
        {
            public TProduct Create<TProduct>() where TProduct : IProduct<TFactory>, new()
            {
                return new TFactory().Build<TProduct>();
            }
        }

        class CalculateSalary : IFactory<CalculateSalary>
        {
            public TProduct Build<TProduct>() where TProduct : IProduct<CalculateSalary>, new()
            {
                return new TProduct();
            }
        }

        class HourlySalary : IProduct<CalculateSalary>
        {
            public string CalculateAnnualSalary(string salary)
            {
                double hourlySalary = Convert.ToDouble(salary);
                return (120 * hourlySalary * 12).ToString();
            }

        }

        class MonthlySalary : IProduct<CalculateSalary>
        {
            public string CalculateAnnualSalary(string salary)
            {
                double monthlySalary = Convert.ToDouble(salary);
                return (monthlySalary * 12).ToString();
            }

        }

    }
}
