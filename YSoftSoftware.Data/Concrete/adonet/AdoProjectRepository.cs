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
    public class AdoProjectRepository : IProjectRepository
    {
        string connStr = "server=(localdb)\\MSSQLLocalDB;database=YSoftDb; Integrated Security=true;";

        public Project GetById(int id)
        {
            Project project = new Project();

            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = connStr;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                List<Personel> personels = new List<Personel>();

                string sql = @"select per.PersonelId,per.Name from Personels per
                            inner join PersonelProject pp on pp.PersonelsPersonelId = per.PersonelId
                            inner join Projects pro on pro.ProjectId = pp.ProjectsProjectId
                            where ProjectId=" + id;
                cmd.CommandText = sql;
                cmd.Connection = conn;
                SqlDataReader rdr2 = cmd.ExecuteReader();
                while (rdr2.Read())
                {
                    var personel = new Personel()
                    {
                        PersonelId = Convert.ToInt32(rdr2["PersonelId"]),
                        Name = rdr2["Name"].ToString()
                    };
                    personels.Add(personel);
                }
                rdr2.Close();

                sql = "select * from Projects where ProjectId=@Id;";
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    project.ProjectId = Convert.ToInt32(rdr["ProjectID"]);
                    project.ProjectName = rdr["ProjectName"].ToString();
                    project.MinPersonel = Convert.ToInt32(rdr["MinPersonel"]);
                    project.MaxPersonel = Convert.ToInt32(rdr["MaxPersonel"]);
                    project.Status = Convert.ToBoolean(rdr["Status"]);
                    project.Personels = personels;
                }

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
            return project;
        }

        public IQueryable<Project> GetAll()
        {
            List<Project> projects = new List<Project>();
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = connStr;
                conn.Open();
                string sql = "select * from Projects;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandText = sql;
                cmd.Connection=conn;
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var project = new Project()
                    {
                        ProjectId = Convert.ToInt32(rdr["ProjectId"]),
                        ProjectName = rdr["ProjectName"].ToString(),
                        MinPersonel = Convert.ToInt32(rdr["MinPersonel"]),
                        MaxPersonel = Convert.ToInt32(rdr["MaxPersonel"]),
                        Status = Convert.ToBoolean(rdr["Status"])
                    };
                    projects.Add(project);
                }
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
            return projects.AsQueryable();
        }

        public void Add(Project Entity)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();
            try
            {

                string sql =
                    "insert into Projects (ProjectName,MinPersonel,MaxPersonel,Status) values(@Name,@Min,@Max,@Status) SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(sql, conn, transaction);
                cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 20).Value = Entity.ProjectName;
                cmd.Parameters.Add("@Min", System.Data.SqlDbType.Int, 20).Value = Entity.MinPersonel;
                cmd.Parameters.Add("@Max", System.Data.SqlDbType.Int, 20).Value = Entity.MaxPersonel;
                cmd.Parameters.Add("@Status", System.Data.SqlDbType.Bit, 20).Value = Entity.Status;
                int id = Convert.ToInt32(cmd.ExecuteScalar());

                sql =
                    "insert into PersonelProject (PersonelsPersonelId,ProjectsProjectId) values(@PersonelId,@ProjectId);";
                if (Entity.Personels != null)
                {
                    foreach (var item in Entity.Personels)
                    {
                        cmd = new SqlCommand(sql, conn,transaction);
                        cmd.Parameters.Add("@PersonelId", SqlDbType.Int, 20).Value = item.PersonelId;
                        cmd.Parameters.Add("@ProjectId", SqlDbType.Int, 20).Value = id;
                        cmd.ExecuteNonQuery();

                    }
                }

                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction.Rollback();
                throw;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
        }

        public void Update(Project Entity)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();
            try
            {

                string sql =
                    "Update Projects set ProjectName=@PName, MinPersonel=@Min, MaxPersonel=@Max, Status=@Status where ProjectId="+Entity.ProjectId;
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Transaction = transaction;
                cmd.Parameters.Add("@PName", System.Data.SqlDbType.NVarChar, 20).Value = Entity.ProjectName;
                cmd.Parameters.Add("@Min", System.Data.SqlDbType.Int, 20).Value = Entity.MinPersonel;
                cmd.Parameters.Add("@Max", System.Data.SqlDbType.Int, 20).Value = Entity.MaxPersonel;
                cmd.Parameters.Add("@Status", System.Data.SqlDbType.Bit, 20).Value = Entity.Status;
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.Bit, 20).Value = Entity.ProjectId;
                cmd.ExecuteNonQuery();
                sql = "delete from PersonelProject where ProjectsProjectId=" + Entity.ProjectId;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();


                sql =
                    "insert into PersonelProject (PersonelsPersonelId,ProjectsProjectId) values(@PersonelId,@ProjectId);";
                if (Entity.Personels != null)
                {
                    foreach (var item in Entity.Personels)
                    {
                        cmd = new SqlCommand(sql, conn, transaction);
                        cmd.Parameters.Add("@PersonelId", SqlDbType.Int, 20).Value = item.PersonelId;
                        cmd.Parameters.Add("@ProjectId", SqlDbType.Int, 20).Value = Entity.ProjectId;
                        cmd.ExecuteNonQuery();

                    }
                }

                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction.Rollback();
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
                string sql = "delete from Projects where projectId=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn,transaction);
                cmd.Parameters.AddWithValue("@Id", id);
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

        public IQueryable<Project> SearchProject(string q)
        {
            List<Project> projects = new List<Project>();
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = connStr;
                conn.Open();
                string sql =
                    @"select distinct pro.ProjectId,pro.ProjectName,pro.MinPersonel,pro.MaxPersonel,pro.Status from Projects pro 
                    left join PersonelProject pp on pp.ProjectsProjectId = pro.ProjectId
                    left join Personels per on per.PersonelId = pp.PersonelsPersonelId
                    where pro.ProjectName like @ProjectName or per.Name like @PersonelName ; ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ProjectName", "%" + q + "%");
                cmd.Parameters.AddWithValue("@PersonelName", "%" + q + "%");
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var project = new Project()
                    {
                        ProjectId = Convert.ToInt32(rdr["ProjectId"]),
                        ProjectName = rdr["ProjectName"].ToString(),
                        MinPersonel = Convert.ToInt32(rdr["MinPersonel"]),
                        MaxPersonel = Convert.ToInt32(rdr["MaxPersonel"]),
                        Status = Convert.ToBoolean(rdr["Status"])
                    };
                    projects.Add(project);
                }
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
            return projects.AsQueryable();
        }

    }
}
