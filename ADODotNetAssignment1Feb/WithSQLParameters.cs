using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ADODotNetAssignment1Feb
{
    class WithParametersCommands
    {
        SqlConnection cn = new SqlConnection(@"Data Source=LAPTOP-0RKRG5BA\MSSQLSERVER02;
                                         Initial Catalog=WFA3DotNet;Integrated Security=True");
        SqlCommand cm = null;
        SqlDataReader dr;

        #region Methods
        public int ShowData()
        {
            try
            {
                Console.WriteLine("\nData from the table EmployeeTab after DML command");
                cn = new SqlConnection(@"Data Source=LAPTOP-0RKRG5BA\MSSQLSERVER02;
                                         Initial Catalog=WFA3DotNet;Integrated Security=True");
                cm = new SqlCommand("Select * from EmployeeTab", cn);
                cn.Open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["EmpId"]} \t{dr["EmpName"]} \t\t{dr["Salary"]} \t\t{dr["DeptNo"]}");
                }
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

        public int InsertWithParameter()
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
                cm = new SqlCommand("Insert into EmployeeTab values(@Ename,@Esal,@Edept)", cn);
                cm.Parameters.Add("@EName", SqlDbType.VarChar, 20).Value = EName;
                cm.Parameters.Add("@ESal", SqlDbType.Float).Value = ESal;
                cm.Parameters.Add("@EDept", SqlDbType.Int).Value = EDept;

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

        public int DeleteWithParameter()
        {
            try
            {
                Console.WriteLine("Enter Employee ID to be deleted");
                var EmpId = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection(@"Data Source=LAPTOP-0RKRG5BA\MSSQLSERVER02;
                                         Initial Catalog=WFA3DotNet;Integrated Security=True");
                cm = new SqlCommand("Delete from EmployeeTab where EmpId = @EmpId", cn);
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

        public int UpdateWithParameter()
        {
            try
            {
                Console.WriteLine("Enter Employee ID that is to be updated");
                var EmpId = Console.ReadLine();
                Console.WriteLine("Enter Employee Name");
                var EName = Console.ReadLine();
                Console.WriteLine("Enter Employee Salary");
                var ESal = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Enter Employee Dept Number");
                var EDept = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection(@"Data Source=LAPTOP-0RKRG5BA\MSSQLSERVER02;
                                         Initial Catalog=WFA3DotNet;Integrated Security=True");

                cm = new SqlCommand("Update EmployeeTab set EmpName =@EName, Salary=@ESal, DeptNo=@EDept where EmpId=@EmpId", cn);
                cm.Parameters.Add("@EmpId", SqlDbType.Int).Value = EmpId;
                cm.Parameters.Add("@EName", SqlDbType.VarChar, 20).Value = EName;
                cm.Parameters.Add("@ESal", SqlDbType.Float).Value = ESal;
                cm.Parameters.Add("@EDept", SqlDbType.Int).Value = EDept;

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

        public int SelectEmpDetail()
        {
            try
            {
                Console.WriteLine("Enter Employee ID whose details are to be displayed");
                var EmpId = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection(@"Data Source=LAPTOP-0RKRG5BA\MSSQLSERVER02;
                                         Initial Catalog=WFA3DotNet;Integrated Security=True");

                cm = new SqlCommand("Select et.EmpId,et.EmpName,et.Salary,dt.Deptname from EmployeeTab et join DeptTab dt on et.DeptNo = dt.DeptId where EmpId=@EmpId", cn);
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

        #endregion
    }
    class WithSQLParameters
    {
        private static void WithParameterFunc()
        {
            Console.WriteLine("Menu Driven for program using SQL parameters");
            while (true)
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("1-- Insert a row using SQL Parameters");
                Console.WriteLine("2-- Delete a row using SQL Parameters");
                Console.WriteLine("3-- Update an existing row using SQL Parameters");
                Console.WriteLine("4-- Display details of employee using SQL Parameters");
                Console.WriteLine("5-- Show the current data of the table");
                Console.WriteLine("-------------------------------------------------------");

                Console.WriteLine("\nEnter choice of command to execute");
                var ch = Convert.ToInt32(Console.ReadLine());
                WithParametersCommands pc = new WithParametersCommands();
                switch (ch)
                {
                    case 1:
                        pc.InsertWithParameter();
                        break;

                    case 2:
                        pc.DeleteWithParameter();
                        break;

                    case 3:
                        pc.UpdateWithParameter();
                        break;

                    case 4:
                        pc.SelectEmpDetail();
                        break;

                    case 5:
                        pc.ShowData();
                        break;

                    default:
                        Console.WriteLine("Incorrect Choice. Enter your choice again");
                        break;
                }
            }
        }

        static void Main()
        {
            WithParameterFunc();
            Console.ReadLine();
        }
    }
}
