using EmployeePayroll_Using_Threads;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace EmployeePayrollTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<EmployeeModel> employeeDetails = new List<EmployeeModel>();
            employeeDetails.Add(new EmployeeModel(1, "Bruce", 'M', "Capgemini", "HR", "9879090799", "Hyderabad", DateTime.Now, 100000));
            employeeDetails.Add(new EmployeeModel(2, "Banner", 'M', "Microsoft", "HR", "8976528393", "Hyderabad", DateTime.Now, 100000));
            employeeDetails.Add(new EmployeeModel(3, "clark", 'M', "Microsoft", "HR", "9564728292", "Hyderabad", DateTime.Now, 100000));
            employeeDetails.Add(new EmployeeModel(4, "Mike", 'M', "Capgemini", "HR", "9949567826", "Hyderabad", DateTime.Now, 00000));
            employeeDetails.Add(new EmployeeModel(5, "Jason", 'M', "Microsoft", "HR", "9678623419", "Hyderabad", DateTime.Now, 100000));
            employeeDetails.Add(new EmployeeModel(6, "Patrick", 'M', "TataElxsi", "HR", "9949675729", "Hyderabad", DateTime.Now, 100000));
            employeeDetails.Add(new EmployeeModel(7, "Maria", 'F', "Capgemini", "HR", "8976528356", "Hyderabad", DateTime.Now, 100000));
            employeeDetails.Add(new EmployeeModel(8, "Steve", 'M', "TataElxsi", "HR", "8987656342", "Hyderabad", DateTime.Now, 100000));

            EmployeePayrollOperation employeePayrollOperations = new EmployeePayrollOperation();

            DateTime startDateTime = DateTime.Now;
            employeePayrollOperations.addMultipleEmployeeToList(employeeDetails);
            DateTime stopDateTime = DateTime.Now;
            Console.WriteLine("Duration without thread: " + (stopDateTime - startDateTime));

            DateTime startDateTimeThread = DateTime.Now;
            employeePayrollOperations.addMultipleEmployeeToListWithThread(employeeDetails);
            DateTime stopDateTimeThread = DateTime.Now;
            Console.WriteLine("Duration with thread: " + (stopDateTimeThread - startDateTimeThread));
        }
    }
}

