namespace Sprout.Exam.Common.DataTransferObjects
{
    public class RegularEmployeeDto : EmployeeDto
    {
        public decimal Salary { get; set; }
        public decimal TaxRate { get; set; }
        public decimal AbsentDays { get; set; }

        public override decimal CalculateNetIncome()
        {
            decimal taxDeduction = Salary * TaxRate;
            decimal absentDeduction = AbsentDays * (Salary / 22);
            decimal totalDeductions = absentDeduction + taxDeduction;
            return Salary - totalDeductions;
        }
    }
}
