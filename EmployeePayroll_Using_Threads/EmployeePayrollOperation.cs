using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayroll_Using_Threads
{
    public class EmployeePayrollOperation
    {
        public List<EmployeeModel> employeePayrollDetailsList = new List<EmployeeModel>();

        public static string connectionString = @"Data Source=LAPTOP-BSJLU8TT\SQLEXPRESS;Initial Catalog=Payroll_ServiceDB;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);

        // Method to retrieve the Employee Payroll from the Database
        public void GetAllEmployee()
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                using (connection)
                {
                    string query = @"Select * from Employee";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        // Printing column headers in Employee_Payroll
                        Console.Write("ID  " + " Name  " + " Gender  " + " Company" + "\t");
                        Console.Write("Department  " + " PhoneNo. " + "  Address" + "     " + "StartDate");
                        Console.Write("  Basic " + "Deductions " + "Taxable  " + "Tax  " + " NetPay\n");
                        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

                        while (dr.Read())
                        {
                            employeeModel.EmployeeID = dr.GetInt32(0);
                            employeeModel.EmployeeName = !dr.IsDBNull(1) ? dr.GetString(1) : "NA";
                            employeeModel.Gender = !dr.IsDBNull(2) ? Convert.ToChar(dr.GetString(2)) : 'N';
                            employeeModel.Company = !dr.IsDBNull(3) ? dr.GetString(3) : "NA";
                            employeeModel.Department = !dr.IsDBNull(4) ? dr.GetString(4) : "NA";
                            employeeModel.PhoneNumber = !dr.IsDBNull(5) ? dr.GetString(5) : "NA";
                            employeeModel.Address = !dr.IsDBNull(6) ? dr.GetString(6) : "NA";
                            employeeModel.StartDate = !dr.IsDBNull(7) ? dr.GetDateTime(7) : DateTime.MinValue;
                            employeeModel.BasicPay = !dr.IsDBNull(8) ? dr.GetDecimal(8) : 0;
                            employeeModel.Deductions = !dr.IsDBNull(9) ? dr.GetDecimal(9) : 0;
                            employeeModel.TaxablePay = !dr.IsDBNull(10) ? dr.GetDecimal(10) : 0;
                            employeeModel.Tax = !dr.IsDBNull(11) ? dr.GetDecimal(11) : 0;
                            employeeModel.NetPay = !dr.IsDBNull(12) ? dr.GetDecimal(12) : 0;

                            // Printing Employee_payroll data
                            Console.Write(employeeModel.EmployeeID.ToString().PadRight(3) + employeeModel.EmployeeName.PadRight(12) + employeeModel.Gender + "   " + employeeModel.Company.PadRight(12));
                            Console.Write(employeeModel.Department.PadRight(12) + employeeModel.PhoneNumber.PadRight(12) + employeeModel.Address.PadRight(12) + employeeModel.StartDate.ToString("dd-MM-yyyy").PadRight(12));
                            Console.Write(Math.Round(employeeModel.BasicPay, 0) + "\t" + Math.Round(employeeModel.Deductions, 0) + "\t" + Math.Round(employeeModel.TaxablePay, 0) + "\t");
                            Console.Write(Math.Round(employeeModel.Tax, 0) + "\t" + Math.Round(employeeModel.NetPay, 0));
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

            finally
            {
                connection.Close();
            }
        }

        // Method to add new employee to the database
        public bool AddEmployee(EmployeeModel model)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SpAddEmployeeDetails", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@Company_Name", model.Company);
                    command.Parameters.AddWithValue("@Dept_Name", model.Department);
                    command.Parameters.AddWithValue("@Phone_No", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@start_date", model.StartDate);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@Deduction", model.Deductions);
                    command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
                    command.Parameters.AddWithValue("@IncomeTax", model.Tax);
                    command.Parameters.AddWithValue("@NetPay", model.NetPay);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }

        // Method to add multiple employees to the database
        public int AddMultipleEmployees(List<EmployeeModel> empList)
        {
            int count = 0;
            empList.ForEach(employee =>
            {
                count++;
                AddEmployee(employee);
            }
            );
            Console.WriteLine("Multiple employees added to database succesfully!");
            return count;
        }

        // Add multiple employees to database using threads
        public int AddMultipleEmployeesUsingThread(List<EmployeeModel> empList)
        {
            int count = 0;
            empList.ForEach(employee =>
            {
                count++;
                Task thread = new Task(() =>
                {
                    Console.WriteLine("Employee being added: " + employee.EmployeeName);
                    AddEmployee(employee);
                    Console.WriteLine("Employee added: " + employee.EmployeeName);
                }
                );
                thread.Start();
            }
            );
            return count;
        }

        public void addEmployeeToList(EmployeeModel emp)
        {
            employeePayrollDetailsList.Add(emp);
        }

        public void addMultipleEmployeeToList(List<EmployeeModel> employeePayrollDetailsList)
        {
            employeePayrollDetailsList.ForEach(employeeData =>
            {
                Console.WriteLine("Employee being added: " + employeeData.EmployeeName);
                addEmployeeToList(employeeData);
                Console.WriteLine("Employee added: " + employeeData.EmployeeName);
            });
            Console.WriteLine(this.employeePayrollDetailsList.ToString());
        }

        public void addMultipleEmployeeToListWithThread(List<EmployeeModel> employeePayrollDetailsList)
        {
            employeePayrollDetailsList.ForEach(employeeData =>
            {
                Task thread = new Task(() =>
                {
                    Console.WriteLine("Employee being added: " + employeeData.EmployeeName);
                    this.addEmployeeToList(employeeData);
                    Console.WriteLine("Employee added: " + employeeData.EmployeeName);
                });
                thread.Start();
            });
            Console.WriteLine(this.employeePayrollDetailsList.Count);
        }
    }
}

