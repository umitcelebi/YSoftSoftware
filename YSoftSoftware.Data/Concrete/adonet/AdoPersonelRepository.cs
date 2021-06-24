using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using YSoftSoftware.Data.Abstract;
using YSoftSoftware.Entity;

namespace YSoftSoftware.Data.Concrete.adonet
{
    public class AdoPersonelRepository:IPersonelRepository
    {
        //static string connStr = "server=server;user=root;database=ysoftsoftwaredb;password=123456;";
        static string connStr = "server=(localdb)\\MSSQLLocalDB;database=YSoftDb; Integrated Security=true;";
        

        public Personel GetById(int id)
        {
            var personel = new Personel();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            string sql = @"select p.PersonelId, p.Name, p.Phone, a.AccountingProgramId,a.Name accountingName, d.DepartmentId ,d.DepartmentName depName,p.DismissalDate,p.Salary,p.StartDate,p.Status from personels p
                        left join Departments d on d.DepartmentId = p.DepartmentId
                        left join AccountingProgram a on a.AccountingProgramId = p.AccountingProgramId
                        where personelId = "+id;
            SqlCommand cmd = new SqlCommand(sql,conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var department = new Department()
                {
                    DepartmentId = Convert.ToInt32(rdr["DepartmentId"]),
                    DepartmentName = rdr["depName"].ToString()
                };
                var accounting = new AccountingProgram()
                {
                    AccountingProgramId = Convert.ToInt32(rdr["AccountingProgramId"]),
                    Name = rdr["accountingName"].ToString()
            };
                personel.PersonelId = Convert.ToInt32(rdr["PersonelId"]);
                personel.Name= rdr["Name"].ToString();
                personel.Phone = rdr["Phone"].ToString();
                personel.Department = department;
                personel.AccountingProgram = accounting;
                personel.DismissalDate = Convert.ToDateTime(rdr["DismissalDate"]);
                personel.Salary = Convert.ToDecimal(rdr["Salary"]);
                personel.StartDate= Convert.ToDateTime(rdr["StartDate"]);
                personel.Status = Convert.ToBoolean(rdr["Status"]);
            }
            conn.Close();
            conn.Dispose();
            return personel;
        }

        public IQueryable<Personel> GetAll()
        {
            List<Personel> personels = new List<Personel>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            string sql = "select * from Personels;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var personel = new Personel();
                personel.PersonelId = Convert.ToInt32(rdr["PersonelId"]);
                personel.Name = rdr["Name"].ToString();
                personel.Phone = rdr["Phone"].ToString();
                personel.DepartmentId = Convert.ToInt32(rdr["DepartmentId"]);
                personel.DismissalDate = Convert.ToDateTime(rdr["DismissalDate"]);
                personel.Salary = Convert.ToDecimal(rdr["Salary"]);
                personel.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                personel.Status = Convert.ToBoolean(rdr["Status"]);

                personels.Add(personel);
            }
            conn.Close();
            return personels.AsQueryable();
        }

        public void Add(Personel Entity)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            string sql = "insert into Personels " +
                         "(Name,Phone,DepartmentId,AccountingProgramId,Salary,StartDate,Status) " +
                         "values (@N,@P,@D,@A,@S,@SD,1)";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@N", System.Data.SqlDbType.NVarChar, 100).Value = Entity.Name;
            cmd.Parameters.Add("@P", System.Data.SqlDbType.NVarChar, 100).Value = Entity.Phone;
            cmd.Parameters.Add("@D", System.Data.SqlDbType.NVarChar, 100).Value = Entity.DepartmentId;
            cmd.Parameters.Add("@A", System.Data.SqlDbType.NVarChar, 100).Value = Entity.AccountingProgramId;
            cmd.Parameters.Add("@SD", System.Data.SqlDbType.DateTime, 100).Value = DateTime.Now;
            cmd.Parameters.Add("@S", System.Data.SqlDbType.NVarChar, 100).Value = Entity.Salary;

            cmd.ExecuteNonQuery();
            
            conn.Close();
        }

        public void Update(Personel Entity)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();
            string sql = @"update personels set Name=@N, Phone=@P, DepartmentId=@D, AccountingProgramId=@A, 
                         Salary=@S where PersonelId=@ID";

            SqlCommand cmd = new SqlCommand(sql, conn,transaction);
            cmd.Parameters.Add("@N", System.Data.SqlDbType.NVarChar, 50).Value = Entity.Name;
            cmd.Parameters.Add("@P", System.Data.SqlDbType.NVarChar, 50).Value = Entity.Phone;
            cmd.Parameters.Add("@D", System.Data.SqlDbType.Int, 50).Value = Entity.DepartmentId;
            cmd.Parameters.Add("@A", System.Data.SqlDbType.Int, 50).Value = Entity.AccountingProgramId;
            cmd.Parameters.AddWithValue("@S", Entity.Salary);
            cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int, 50).Value = Entity.PersonelId;

            cmd.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();

        }

        public void Delete(int id)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();
            try
            {
                string sql = "delete from personels where PersonelId=@ID";
                SqlCommand cmd = new SqlCommand(sql, conn, transaction);
                cmd.Parameters.Add("@ID", SqlDbType.Int, 10).Value = id;
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
                conn.Dispose();
                conn.Close();
            }

        }

        public void Save()
        {

        }

        public IQueryable<Personel> GetByDepartment(string department)
        {
            List<Personel> list = new List<Personel>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            conn.Open();

            string sql =
                "select * from Personels p left join Departments d on p.DepartmentId=d.DepartmentId where d.DepartmentName=@Department;";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Department", System.Data.SqlDbType.NVarChar, 20).Value = department;
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var personel = new Personel()
                {
                    PersonelId = Convert.ToInt32(rdr["PersonelId"]),
                    Name = rdr["Name"].ToString(),
                    Phone = rdr["Phone"].ToString(),
                    DepartmentId = Convert.ToInt32(rdr["DepartmentId"]),
                    DismissalDate = Convert.ToDateTime(rdr["DismissalDate"]),
                    Salary = Convert.ToDecimal(rdr["Salary"]),
                    StartDate = Convert.ToDateTime(rdr["StartDate"]),
                    Status = Convert.ToBoolean(rdr["Status"])
            };
                list.Add(personel);
                
            }
            conn.Close();
            return list.AsQueryable();
        }

        public List<Personel> GetByIds(int[] Ids)
        {
            List<Personel> list = new List<Personel>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            
            var parameters = new string[Ids.Length];

            
            SqlCommand cmd = new SqlCommand();
            for (int i = 0; i < Ids.Length; i++)
            {
                parameters[i] = string.Format("@Ids{0}", i);
                cmd.Parameters.AddWithValue(parameters[i], Ids[i]);
            }
            cmd.CommandText = string.Format("select * from Personels where PersonelId in({0})", string.Join(", ", parameters).TrimEnd(','));
            cmd.Connection = conn;
            SqlDataReader rdr = cmd.ExecuteReader();
            
            while (rdr.Read())
            {
                var personel = new Personel()
                {
                    PersonelId = Convert.ToInt32(rdr["PersonelId"]),
                    Name = rdr["Name"].ToString(),
                    Phone = rdr["Phone"].ToString(),
                    DepartmentId = Convert.ToInt32(rdr["DepartmentId"]),
                    DismissalDate = Convert.ToDateTime(rdr["DismissalDate"]),
                    Salary = Convert.ToDecimal(rdr["Salary"]),
                    StartDate = Convert.ToDateTime(rdr["StartDate"]),
                    Status = Convert.ToBoolean(rdr["Status"])
                };
                list.Add(personel);
                
            }
            conn.Close();

            return list;
        }

        public IQueryable<Personel> GetByDepartmentAndProject(string department, int projectId)
        {
            List<Personel> list = new List<Personel>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            try
            {
                conn.Open();

                string sql =
                    @"select PersonelId,Name,Phone,per.DepartmentId,AccountingProgramId,DismissalDate,Salary,StartDate, per.Status  from Personels per
                    left join Departments dep on per.DepartmentId = dep.DepartmentId
                    left join PersonelProject pp on pp.PersonelsPersonelId = per.PersonelId
                    left join Projects pro on pp.ProjectsProjectId = pro.ProjectId
                    where dep.DepartmentName = @Department and pro.ProjectId =@ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Department", department);
                cmd.Parameters.AddWithValue("@ID", projectId);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var personel = new Personel()
                    {
                        PersonelId = Convert.ToInt32(rdr["PersonelId"]),
                        Name = rdr["Name"].ToString(),
                        Phone = rdr["Phone"].ToString(),
                        DepartmentId = Convert.ToInt32(rdr["DepartmentId"]),
                        DismissalDate = Convert.ToDateTime(rdr["DismissalDate"]),
                        Salary = Convert.ToDecimal(rdr["Salary"]),
                        StartDate = Convert.ToDateTime(rdr["StartDate"]),
                        Status = Convert.ToBoolean(rdr["Status"])
                    };
                    list.Add(personel);
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

            return list.AsQueryable();
        }

        public void Dismiss(int personelId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();

            string sql = "update Personels set status=0, DismissalDate=@Date where PersonelId=@ID ";
            
            SqlCommand cmd = new SqlCommand(sql, conn,transaction);
            cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int, 100).Value =personelId;
            cmd.Parameters.Add("@Date", System.Data.SqlDbType.DateTime, 100).Value = DateTime.Now;
            cmd.ExecuteNonQuery();


            sql = "delete PersonelProject from PersonelProject where PersonelsPersonelId = @ID ";
            cmd = new SqlCommand(sql, conn,transaction);
            cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int, 100).Value = personelId;
            cmd.ExecuteNonQuery();
            transaction.Commit();

            conn.Close();
        }

        public IQueryable<Personel> getDismissPersonels()
        {
            List<Personel> personels = new List<Personel>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            string sql = "select * from Personels where Status = 0;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var personel = new Personel();
                personel.PersonelId = Convert.ToInt32(rdr["PersonelId"]);
                personel.Name = rdr["Name"].ToString();
                personel.Phone = rdr["Phone"].ToString();
                personel.DepartmentId = Convert.ToInt32(rdr["DepartmentId"]);
                personel.DismissalDate = Convert.ToDateTime(rdr["DismissalDate"]);
                personel.Salary = Convert.ToDecimal(rdr["Salary"]);
                personel.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                personel.Status = Convert.ToBoolean(rdr["Status"]);

                personels.Add(personel);
            }
            conn.Close();
            return personels.AsQueryable();
        }
    }
}
