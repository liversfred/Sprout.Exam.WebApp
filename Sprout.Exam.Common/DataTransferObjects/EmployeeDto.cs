using Sprout.Exam.Common.Interfaces;

namespace Sprout.Exam.Common.DataTransferObjects
{
    public abstract class EmployeeDto : IEmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Birthdate { get; set; }
        public string Tin { get; set; }
        public int TypeId { get; set; }

        public abstract decimal CalculateNetIncome();
    }
}
