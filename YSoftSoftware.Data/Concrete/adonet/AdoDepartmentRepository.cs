using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using YSoftSoftware.Data.Abstract;
using YSoftSoftware.Entity;

namespace YSoftSoftware.Data.Concrete.adonet
{
    public class AdoDepartmentRepository:IDepartmentRepository
    {
        static string connStr = "server=(localdb)\\MSSQLLocalDB;database=YSoftDb; Integrated Security=true;";

        public Department GetById(int id)
        {
            Department department = new Department();
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = "select * from Departments where DepartmentId=@Id;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader rdr = cmd.ExecuteReader();
                department.DepartmentId = Convert.ToInt32(rdr["DepartmentId"]);
                department.DepartmentName = rdr["DepartmentName"].ToString();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                conn.Close();
            }

            return department;
        }

        public IQueryable<Department> GetAll()
        {
            List<Department> departments = new List<Department>();
            SqlConnection conn = new SqlConnection();

            try
            {
                conn.ConnectionString = connStr;
                conn.Open();
                string sql = "select * from Departments;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var department = new Department()
                    {
                        DepartmentId = Convert.ToInt32(rdr["DepartmentId"]),
                        DepartmentName = rdr["DepartmentName"].ToString()
                    };
                    departments.Add(department);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                conn.Close();
            }
            
            return departments.AsQueryable();
        }

        public void Add(Department Entity)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            try
            {
                conn.Open();
                string sql = "insert into Departments (DepartmentName) values(@DepartmentName);";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@DepartmentName", System.Data.SqlDbType.Int, 20).Value = Entity.DepartmentName;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(Department Entity)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;

            try
            {
                conn.Open();
                string sql = "update Departments set DepartmentName=@Name where DepartmentId=@Id;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 50).Value = Entity.DepartmentName;
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int, 50).Value = Entity.DepartmentId;

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Delete(int id)
        {
            SqlConnection conn = new SqlConnection();
            SqlTransaction transaction = conn.BeginTransaction();
            conn.ConnectionString = connStr;
            try
            {
                conn.Open();
                string sql = "delete Departments where DepartmentId=@Id;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@Id",System.Data.SqlDbType.Int,20).Value=id;
                cmd.ExecuteNonQuery();
                transaction.Commit();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Save()
        {
        }
    }
}
