using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasGlobalTestApi.Models
{
    public class Employee
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ContractTypeName { get; set; }

        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public string RoleDescription { get; set; }

        public string HourlySalary { get; set; }

        public string MonthlySalary { get; set; }
    }
}