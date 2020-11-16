using System;
using System.Collections.Generic;

namespace EmployeePayroll_Using_Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            List<EmployeeModel> empList = new List<EmployeeModel>();
            EmployeePayrollOperation operation = new EmployeePayrollOperation();
            while (true)
            {
                Console.WriteLine("Menu :");
                Console.Write("1. Retrieve all employees\n2. Add New Employee\n3. Add Multiple Employees\n4. Add Multiple Employees using Threads\n5. Exit\n");
                Console.Write("Enter your choice :");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        operation.GetAllEmployee();
                        Console.WriteLine("________________________________________________________________________________________________________________________");
                        break;
                    case 2:
                        EmployeeModel employee = new EmployeeModel();
                        Console.WriteLine("Enter Name");
                        employee.EmployeeName = Console.ReadLine();
                        Console.WriteLine("Enter Gender");
                        employee.Gender = Convert.ToChar(Console.ReadLine());
                        Console.WriteLine("Enter Company");
                        employee.Company = Console.ReadLine();
                        Console.WriteLine("Enter Department");
                        employee.Department = Console.ReadLine();
                        Console.WriteLine("Enter PhoneNo");
                        employee.PhoneNumber = Console.ReadLine();
                        Console.WriteLine("Enter Address");
                        employee.Address = Console.ReadLine();
                        Console.WriteLine("Enter Start Date");
                        employee.StartDate = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("Enter Basic Pay");
                        employee.BasicPay = Convert.ToDecimal(Console.ReadLine());
                        employee.Deductions = 0.2M * employee.BasicPay;
                        employee.TaxablePay = employee.BasicPay - employee.Deductions;
                        employee.Tax = 0.1M * employee.TaxablePay;
                        employee.NetPay = employee.BasicPay - employee.Tax;
                        operation.AddEmployee(employee);
                        Console.WriteLine("________________________________________________________________________________________________________________________");
                        break;
                    case 3:
                        while (true)
                        {
                            EmployeeModel model = new EmployeeModel();
                            Console.WriteLine("Enter Name");
                            model.EmployeeName = Console.ReadLine();
                            Console.WriteLine("Enter Gender");
                            model.Gender = Convert.ToChar(Console.ReadLine());
                            Console.WriteLine("Enter Company");
                            model.Company = Console.ReadLine();
                            Console.WriteLine("Enter Department");
                            model.Department = Console.ReadLine();
                            Console.WriteLine("Enter PhoneNo");
                            model.PhoneNumber = Console.ReadLine();
                            Console.WriteLine("Enter Address");
                            model.Address = Console.ReadLine();
                            Console.WriteLine("Enter Start Date");
                            model.StartDate = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("Enter Basic Pay");
                            model.BasicPay = Convert.ToDecimal(Console.ReadLine());
                            model.Deductions = 0.2M * model.BasicPay;
                            model.TaxablePay = model.BasicPay - model.Deductions;
                            model.Tax = 0.1M * model.TaxablePay;
                            model.NetPay = model.BasicPay - model.Tax;
                            empList.Add(model);
                            Console.WriteLine("Do you want to add more contacts ? Yes / No");
                            string ans = Console.ReadLine();
                            if (ans.ToUpper() == "NO")
                                break;
                        }
                        DateTime startDateTime = DateTime.Now;
                        operation.AddMultipleEmployees(empList);
                        DateTime stopDateTime = DateTime.Now;
                        Console.WriteLine("Duration for adding multiple employees without thread: " + (stopDateTime - startDateTime));
                        Console.WriteLine("________________________________________________________________________________________________________________________");
                        break;
                    case 4:
                        while (true)
                        {
                            EmployeeModel model = new EmployeeModel();
                            Console.WriteLine("Enter Name");
                            model.EmployeeName = Console.ReadLine();
                            Console.WriteLine("Enter Gender");
                            model.Gender = Convert.ToChar(Console.ReadLine());
                            Console.WriteLine("Enter Company");
                            model.Company = Console.ReadLine();
                            Console.WriteLine("Enter Department");
                            model.Department = Console.ReadLine();
                            Console.WriteLine("Enter PhoneNo");
                            model.PhoneNumber = Console.ReadLine();
                            Console.WriteLine("Enter Address");
                            model.Address = Console.ReadLine();
                            Console.WriteLine("Enter Start Date");
                            model.StartDate = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("Enter Basic Pay");
                            model.BasicPay = Convert.ToDecimal(Console.ReadLine());
                            model.Deductions = 0.2M * model.BasicPay;
                            model.TaxablePay = model.BasicPay - model.Deductions;
                            model.Tax = 0.1M * model.TaxablePay;
                            model.NetPay = model.BasicPay - model.Tax;
                            empList.Add(model);
                            Console.WriteLine("Do you want to add more contacts ? Yes / No");
                            string ans = Console.ReadLine();
                            if (ans.ToUpper() == "NO")
                                break;
                        }
                        DateTime startDateTimeThread = DateTime.Now;
                        operation.AddMultipleEmployees(empList);
                        DateTime stopDateTimeThread = DateTime.Now;
                        Console.WriteLine("Duration for adding multiple employees without thread: " + (stopDateTimeThread - startDateTimeThread));
                        Console.WriteLine("________________________________________________________________________________________________________________________");
                        break;
                    case 5:
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Choice!");
                        break;
                }
            }
        }
    }
}
