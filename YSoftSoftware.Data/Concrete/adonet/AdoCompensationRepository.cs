using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using YSoftSoftware.Data.Abstract;
using YSoftSoftware.Entity;

namespace YSoftSoftware.Data.Concrete.adonet
{
    public class AdoCompensationRepository:ICompensationRepository
    {
        static string connStr = "server=(localdb)\\MSSQLLocalDB;database=YSoftDb; Integrated Security=true;";
        private string sql = "";
        public IQueryable<Compensation> GetAll()
        {
            List<Compensation> compensations = new List<Compensation>();
            SqlConnection conn = new SqlConnection();
            
            try
            {
                conn.ConnectionString = connStr;
                conn.Open();
                sql = "select c.compensationId,p.PersonelId, p.Name personelName, p.DismissalDate,p.StartDate, c.Money from Compensations c " +
                "left join Personels p on p.PersonelId = c.PersonelId "+
                "left join Departments d on d.DepartmentId = p.DepartmentId "+
                "left join AccountingProgram a on a.AccountingProgramId = p.AccountingProgramId; ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var compensation = new Compensation();
                    var personel = new Personel();
                    compensation.compensationId = Convert.ToInt32(rdr["compensationId"]);

                    personel.Name = rdr["personelName"].ToString();
                    personel.PersonelId = Convert.ToInt32(rdr["PersonelId"]);
                    personel.DismissalDate = Convert.ToDateTime(rdr["DismissalDate"]);
                    personel.StartDate = Convert.ToDateTime(rdr["StartDate"]);

                    compensation.Personel = personel;
                    compensation.Money = Convert.ToDecimal(rdr["Money"]);


                    compensations.Add(compensation);
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
            return compensations.AsQueryable();
        }

        public void Add(int personelId, decimal money)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            try
            {
                conn.Open();

                sql = "insert into Compensations(PersonelId, Money) values(@Id,@Money);";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@Id", SqlDbType.Int, 10).Value = personelId;
                cmd.Parameters.Add("@Money", SqlDbType.Int, 10).Value = money;

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

        public void Save()
        {
        }
    }
}
