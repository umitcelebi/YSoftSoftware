using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using YSoftSoftware.Data.Abstract;
using YSoftSoftware.Entity;

namespace YSoftSoftware.Data.Concrete.adonet
{
    public class AdoAccountingProgramRepository:IAccountingProgramRepository
    {
        static string connStr = "server=(localdb)\\MSSQLLocalDB;database=YSoftDb; Integrated Security=true;";
        private string sql = "";

        public AccountingProgram GetById(int id)
        {
            AccountingProgram program = new AccountingProgram();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            try
            {
                conn.Open();
                sql = "select * from AccountingProgram where AccountingProgramId=@Id;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    program.AccountingProgramId = Convert.ToInt32(rdr["AccountingProgramId"]);
                    program.Name = rdr["Name"].ToString();
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

            return program;
        }

        public IQueryable<AccountingProgram> GetAll()
        {
            List<AccountingProgram> list = new List<AccountingProgram>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;

            try
            {
                conn.Open();
                sql = "select * from AccountingProgram;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var program = new AccountingProgram()
                    {
                        AccountingProgramId = Convert.ToInt32(rdr["AccountingProgramId"]),
                        Name = rdr["Name"].ToString()
                    };
                    list.Add(program);
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

        public void Add(AccountingProgram Entity)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                sql = "insert into AccountingProgram (Name) values(@Name);";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 50).Value = Entity.Name;

                cmd.ExecuteNonQuery();
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

        public void Update(AccountingProgram Entity)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();
            try
            {
                sql = "update AccountingProgram set Name= @Name where AccountingProgramId=" + Entity.AccountingProgramId;
                SqlCommand cmd = new SqlCommand(sql, conn,transaction);
                cmd.Parameters.AddWithValue("@Name",Entity.Name);

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

        public void Delete(int id)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();
            try
            {
                sql = "update Personels set AccountingProgramId=null where AccountingProgramId=" + id;
                SqlCommand cmd = new SqlCommand(sql, conn, transaction);
                cmd.ExecuteNonQuery();
                sql = "delete from AccountingProgram where AccountingProgramId=@Id;";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
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
    }
}
