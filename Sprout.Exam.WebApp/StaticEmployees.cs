using Sprout.Exam.Common.DataTransferObjects;
using System.Collections.Generic;

namespace Sprout.Exam.WebApp
{
    public static class StaticEmployees
    {
        public static List<EmployeeDto> ResultList = new()
        {
            new RegularEmployeeDto()
            {
                Id = 1,
                FullName = "Jane Doe",
                Birthdate = "1993-03-25",
                Tin = "123215413",
                TypeId = 1
            },
            new ContractualEmployeeDto
            {
                Id = 2,
                FullName = "John Doe",
                Birthdate = "1993-05-28",
                Tin = "957125412",
                TypeId = 2
            }
        };
    }
}
