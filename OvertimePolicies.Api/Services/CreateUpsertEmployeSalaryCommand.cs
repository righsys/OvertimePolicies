using OvertimePolicies.Api.Commands;
using OvertimePolicies.Api.DTOs;
using OvertimePolicies.Services.Interfaces;

namespace OvertimePolicies.Api.Services
{
    public class CreateUpsertEmployeSalaryCommand
    {
        private readonly IOvertimeCalculatorMethods _overtimeCalculatorMethods;

        public CreateUpsertEmployeSalaryCommand(IOvertimeCalculatorMethods overtimeCalculatorMethods)
        {
            _overtimeCalculatorMethods = overtimeCalculatorMethods;
        }

        public UpsertEmployeSalaryCommand GetCommandForUpsertEmployeeSalary(EmployeeSalaryForUpsertDto salary, out bool Success, out string ErrorMessage)
        {
            try
            {
                //
                // Generate command
                //
                int salaryTotal = 0;
                bool dataFormatHasError = false;
                string[] salaryDataHeaders = salary.SalaryData.Line1.Split("/", StringSplitOptions.RemoveEmptyEntries);
                string[] salaryData = salary.SalaryData.Line2.Split("/", StringSplitOptions.RemoveEmptyEntries);


                if (salaryDataHeaders[0].ToLower() != "firstname")
                    dataFormatHasError = true;
                if (salaryDataHeaders[1].ToLower() != "lastname")
                    dataFormatHasError = true;
                if (salaryDataHeaders[2].ToLower() != "basicsalary")
                    dataFormatHasError = true;
                if (salaryDataHeaders[3].ToLower() != "allowance")
                    dataFormatHasError = true;
                if (salaryDataHeaders[4].ToLower() != "transportation")
                    dataFormatHasError = true;
                if (salaryDataHeaders[5].ToLower() != "date")
                    dataFormatHasError = true;

                if (dataFormatHasError)
                {
                    Success = false;
                    ErrorMessage = "فرمت اطلاعات وارد شده صحیح نیست";
                    return null;
                }
                UpsertEmployeSalaryCommand command = new UpsertEmployeSalaryCommand()
                {
                    FirstName = salaryData[0],
                    LastName = salaryData[1],
                    BasicSalary = Convert.ToInt32(salaryData[2]),
                    Allowance = Convert.ToInt32(salaryData[3]),
                    Transportation = Convert.ToInt32(salaryData[4]),
                    Tax = 0
                };
                try
                {
                    string dat, sal, mah;
                    dat = salaryData[5];
                    sal = dat.Substring(0, 4);
                    mah = dat.Substring(4, 2);
                    command.Year = Convert.ToInt32(sal);
                    command.Month = Convert.ToInt32(mah);
                }
                catch (Exception)
                {
                    Success = false;
                    ErrorMessage = "فرمت تاریخ صحیح نیست. فرمت نمونه 13990501";
                    return null;
                }


                salaryTotal = command.BasicSalary;
                salaryTotal += command.Allowance;
                salaryTotal += command.Transportation;


                switch (salary.OverTimeCalculator)
                {
                    case "CalculatorA":
                        command.OverTime = _overtimeCalculatorMethods.CalculatorA(command.BasicSalary, command.Allowance);
                        salaryTotal += command.OverTime;
                        break;
                    case "CalculatorB":
                        command.OverTime = _overtimeCalculatorMethods.CalculatorB(command.BasicSalary, command.Allowance);
                        salaryTotal += command.OverTime;
                        break;
                    case "CalculatorC":
                        command.OverTime = _overtimeCalculatorMethods.CalculatorC(command.BasicSalary, command.Allowance);
                        salaryTotal += command.OverTime;
                        break;
                    default:
                        Success = false;
                        ErrorMessage = "OverTimeCalculator درست نیست";
                        return null;
                }
                salaryTotal -= command.Tax;
                command.Salary = salaryTotal;

                //
                // Return Command
                //
                Success = true;
                ErrorMessage = string.Empty;
                return command;
            }
            catch (Exception)
            {
                Success = false;
                ErrorMessage = "خطا در پردازش اطلاعات";
                return null;
            }
        }
    }
}
