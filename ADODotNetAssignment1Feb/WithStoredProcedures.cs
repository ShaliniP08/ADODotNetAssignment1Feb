using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ADODotNetAssignment1Feb
{
    class WithSPCommands
    {
        SqlConnection cn = new SqlConnection(@"Data Source=LAPTOP-0RKRG5BA\MSSQLSERVER02;
                                         Initial Catalog=WFA3DotNet;Integrated Security=True");
        SqlCommand cm = null;
        SqlDataReader dr;

        public int ShowData()
        {
            try
            {
                Console.WriteLine("Data from the table EmployeeTab after DML command");
                cn = new SqlConnection(@"Data Source=LAPTOP-0RKRG5BA\MSSQLSERVER02;
                                         Initial Catalog=WFA3DotNet;Integrated Security=True");
                cm = new SqlCommand("Select * from EmployeeTab", cn);
                cn.Open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["EmpId"]} \t{dr["EmpName"]} \t\t{dr["Salary"]} \t\t{dr["DeptNo"]}");
                }
                return 0;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }

        public int InsertWithSp()
        {
            try
            {
                Console.WriteLine("Insert values below to be inserted in the table");
                Console.WriteLine("\nEnter Employee Name");
                var EName = Console.ReadLine();
                Console.WriteLine("Enter Employee Salary");
                var ESal = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Enter Employee Dept Number");
                var EDept = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection(@"Data Source=LAPTOP-0RKRG5BA\MSSQLSERVER02;
                                         Initial Catalog=WFA3DotNet;Integrated Security=True");

                cm = new SqlCommand("sp_InsertEmployee", cn);
                cm.CommandType = CommandType.StoredProcedure;

                cm.Parameters.Add("@EmpName", SqlDbType.VarChar, 20).Value = EName;
                cm.Parameters.Add("@ESal", SqlDbType.Float).Value = ESal;
                cm.Parameters.Add("@DeptId", SqlDbType.Int).Value = EDept;

                cn.Open();
                int i = cm.ExecuteNonQuery();
                ShowData();
                return i;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }

        public int DeleteWithSp()
        {
            try
            {
                Console.WriteLine("Enter Employee ID to be deleted");
                var EmpId = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection(@"Data Source=LAPTOP-0RKRG5BA\MSSQLSERVER02;
                                         Initial Catalog=WFA3DotNet;Integrated Security=True");

                cm = new SqlCommand("sp_DeleteEmployee", cn);
                cm.CommandType = CommandType.StoredProcedure;

                cm.Parameters.Add("@EmpId", SqlDbType.Int).Value = EmpId;

                cn.Open();
                int i = cm.ExecuteNonQuery();
                ShowData();
                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                cn.Close();
            }
        }
        public int UpdateWithSp()
        {
            try
            {
                Console.WriteLine("Enter an existing Employee ID that is to be updated");
                var EmpId = Console.ReadLine();
                Console.WriteLine("Enter Employee Name");
                var EName = Console.ReadLine();
                Console.WriteLine("Enter Employee Salary");
                var ESal = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Enter Employee Dept ID");
                var EDept = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection(@"Data Source=LAPTOP-0RKRG5BA\MSSQLSERVER02;
                                         Initial Catalog=WFA3DotNet;Integrated Security=True");
                cm = new SqlCommand("sp_UpdateEmp", cn);
                cm.CommandType = CommandType.StoredProcedure;

                cm.Parameters.Add("@EmpName", SqlDbType.VarChar, 20).Value = EName;
                cm.Parameters.Add("@ESal", SqlDbType.Float).Value = ESal;
                cm.Parameters.Add("@DeptId", SqlDbType.Int).Value = EDept;
                cm.Parameters.Add(@"EmpId", SqlDbType.Int).Value = EmpId;

                cn.Open();
                int i = cm.ExecuteNonQuery();
                ShowData();
                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }

        public int SelectEmpDetail()
        {
            try
            {
                Console.WriteLine("Enter  Employee ID whose details are to be displayed");
                var EmpId = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection(@"Data Source=LAPTOP-0RKRG5BA\MSSQLSERVER02;
                                         Initial Catalog=WFA3DotNet;Integrated Security=True");

                cm = new SqlCommand("sp_ShowEmpDetails", cn);
                cm.CommandType = CommandType.StoredProcedure;

                cm.Parameters.Add("@EmpId", SqlDbType.Int).Value = EmpId;
                cn.Open();
                dr = cm.ExecuteReader();
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Console.WriteLine($"Emp Id : {dr["EmpId"].ToString()}");
                        Console.WriteLine($"Emp name : {dr["EmpName"].ToString()}");
                        Console.WriteLine($"Salary : {dr["Salary"].ToString()}");
                        Console.WriteLine($"DeptName : {dr["DeptName"].ToString()}");
                    }
                }
                else
                {
                    Console.WriteLine("No data found");
                }

                ShowData();
                return 1;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                cn.Close();
            }
        }
    }
    class WithStoredProcedures
    {
        private static void WithSpFunc()
        {
            Console.WriteLine("Menu Driven for program using Stored Procedures");
            bool value = true;
            while (value)
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("1-- Insert a row using SP");
                Console.WriteLine("2-- Delete a row using SP");
                Console.WriteLine("3-- Update an existing row using SP");
                Console.WriteLine("4-- Display details of employee using SP");
                Console.WriteLine("5-- Show the current data of the table");
                Console.WriteLine("-------------------------------------------------------");

                Console.WriteLine("\nEnter choice of command to execute");
                var ch = Convert.ToInt32(Console.ReadLine());
                WithSPCommands wsp = new WithSPCommands();
                switch (ch)
                {
                    case 1:
                        wsp.InsertWithSp();
                        break;

                    case 2:
                       wsp.DeleteWithSp();
                        break;

                    case 3:
                        wsp.UpdateWithSp();
                        break;

                    case 4:
                        wsp.SelectEmpDetail();
                        break;

                    case 5:
                        wsp.ShowData();
                        break;

                    default:
                        Console.WriteLine("Incorrect Choice. Enter your choice again");
                        break;
                }
                Console.WriteLine("\nDo you want to perform another command operation?");
                string str = Console.ReadLine();
                if (str.Equals("no"))
                    value = false;
            }
        }

        static void Main()
        {
            WithSpFunc();
            Console.ReadLine();
        }
    }
}