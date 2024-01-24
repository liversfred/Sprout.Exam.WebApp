namespace Sprout.Exam.Common.DataTransferObjects
{
    public class ContractualEmployeeDto : EmployeeDto
    {
        public decimal DailyRate { get; set; }
        public decimal WorkedDays { get; set; }

        public override decimal CalculateNetIncome()
        {
            return DailyRate * WorkedDays;
        }
    }
}
