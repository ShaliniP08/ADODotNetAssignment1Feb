using System;
using System.Data;
using System.Data.SqlClient;

namespace ADODotNetAssignment1Feb
{
    class WithoutParametersCommands
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

        public int InsertOneRow()
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

                cm = new SqlCommand("Insert into EmployeeTab values('" + EName + "'," + ESal + ", " + EDept + ")", cn);
                cn.Open();
                int i = cm.ExecuteNonQuery();
                Console.WriteLine("1 row is inserted into the table EmployeeTab...");
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

        public int DeleteOneRow()
        {
            try
            {
                Console.WriteLine("Enter Employee ID to be deleted");
                var EmpId = Convert.ToInt32(Console.ReadLine());
                cm = new SqlCommand("Delete from EmployeeTab where EmpId =  " + EmpId + " ", cn);
                cn.Open();
                int i = cm.ExecuteNonQuery();
                Console.WriteLine("1 row is deleted from the table EmployeeTab...");
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

        public int Update()
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
                
                cm = new SqlCommand("Update EmployeeTab set EmpName = '" + EName + "', Salary=" + ESal + ", DeptNo= " + EDept + " where EmpId = " + EmpId + " ", cn);
                cn.Open();
                int i = cm.ExecuteNonQuery();
                Console.WriteLine("1 row is updated to the table EmployeeTab...");
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

        public int SelectEmpDetailWithout()
        {
            try
            {
                Console.WriteLine("Enter Employee ID whose details are to be displayed");
                var EmpId = Convert.ToInt32(Console.ReadLine());

                cm = new SqlCommand("Select et.EmpId,et.EmpName,et.Salary,dt.Deptname from EmployeeTab et join DeptTab dt on et.DeptNo = dt.DeptId where EmpId= " + EmpId + "", cn);
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
    class WithoutSQLParameters
    {
        private static void MenuDrivenFunc()
        {
            Console.WriteLine("Menu Driven for program without using SQL parameters");
            bool value = true;
            while (value)
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("1-- Insert a row");
                Console.WriteLine("2-- Delete a row");
                Console.WriteLine("3-- Update an existing row");
                Console.WriteLine("4-- Display details of employee");
                Console.WriteLine("5-- Show the current data of the table");
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("Enter your choice of commnd");

                var ch = Convert.ToInt32(Console.ReadLine());
                WithoutParametersCommands wpc = new WithoutParametersCommands();
                switch (ch)
                {
                    case 1:
                        wpc.InsertOneRow();
                        break;

                    case 2:
                        wpc.DeleteOneRow();
                        break;

                    case 3:
                        wpc.Update();
                        break;

                    case 4:
                        wpc.SelectEmpDetailWithout();
                        break;

                    case 5:
                        wpc.ShowData();
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
        static void Main(string[] args)
        {
            MenuDrivenFunc();
            Console.ReadLine();
        }
    }
}
