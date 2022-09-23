using DTC_CRUD_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DTC_CRUD_MVC.Controllers
{
    public class KaryawanController : Controller 
    {
        public SqlConnection sqlConn = new SqlConnection();
        public SqlCommand sqlcom = new SqlCommand();
        public SqlParameter sqlpar = new SqlParameter();
        public string connectionString = "Data Source= LAPTOP-S368EH41;" +
                                       "Initial Catalog= MProject_Kantor;" +
                                       "User Id= HelloPanda;" +
                                       "Password= hello123;";
        Karyawan karyawan = new Karyawan();

        //READ
        public IActionResult Index(int id)
        {
            string query;
            if (id == 0)
            {
                query = "SELECT karyawan.*, gaji.salarytype, gaji.salary " +
                "FROM karyawan INNER JOIN gaji " +
                "ON karyawan.salaryid = gaji.salaryid";
            }
            else
            {
                query = $"SELECT karyawan.*, gaji.salarytype, gaji.salary " +
                "FROM karyawan INNER JOIN gaji " +
                "ON karyawan.salaryid = gaji.salaryid WHERE " +
                "personid = {id}";
                
            }
            
            sqlConn = new SqlConnection(connectionString);
            sqlcom = new SqlCommand(query, sqlConn);
            List<Karyawan> karyawans = new List<Karyawan>();
            
            try
            {
                sqlConn.Open();
                using (SqlDataReader sqlDataReader = sqlcom.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {

                        while (sqlDataReader.Read())
                        {
                            Karyawan karyawan = new Karyawan();
                            /*Console.Write(sqlDataReader[0] + " | ");
                            Console.Write(sqlDataReader[1] + " | ");
                            Console.Write(sqlDataReader[2] + " | ");
                            Console.Write(sqlDataReader[3] + " | ");
                            Console.Write(sqlDataReader[4] + " | ");
                            Console.WriteLine("");*/
                            karyawan.id = Convert.ToInt32(sqlDataReader[0]);
                            karyawan.name = (sqlDataReader[1]).ToString();
                            karyawan.address = (sqlDataReader[2]).ToString();
                            karyawan.gender = Convert.ToBoolean(sqlDataReader[3]);
                            karyawan.salaryid = Convert.ToInt32(sqlDataReader[4]);
                            karyawan.type_salary = sqlDataReader[5].ToString();
                            karyawan.salary=Convert.ToDecimal(sqlDataReader[6]);
                            karyawans.Add(karyawan);
                        }

                    }
                    else
                    {
                        Console.WriteLine("No Data Rows in Karyawan. -------");
                    }
                    sqlConn.Close();
                }
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error tampil_karyawan...");
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.Message);
            }
            return View(karyawans);
        }

        
        //CREATE
        //-GET
        public IActionResult Create()
        {
            
            return View();
        }
        //-POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(New_Karyawan karyawan)
        {
            using (sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                SqlTransaction sqltrans = sqlConn.BeginTransaction();

                sqlcom = sqlConn.CreateCommand();
                sqlcom.Transaction = sqltrans;

                //nama
                sqlpar = new SqlParameter();
                sqlpar.ParameterName = "@name";
                sqlpar.Value = karyawan.name;

                sqlcom.Parameters.Add(sqlpar);

                //alamat
                SqlParameter sqlpar1 = new SqlParameter();
                sqlpar1.ParameterName = "@address";
                sqlpar1.Value = karyawan.address;

                sqlcom.Parameters.Add(sqlpar1);

                //gender
                SqlParameter sqlpar2 = new SqlParameter();
                sqlpar2.ParameterName = "@gender";
                sqlpar2.Value = karyawan.gender;

                sqlcom.Parameters.Add(sqlpar2);

                //salaryid
                SqlParameter sqlpar3 = new SqlParameter();
                sqlpar3.ParameterName = "@salaryid";
                sqlpar3.Value = karyawan.salaryid;

                sqlcom.Parameters.Add(sqlpar3);


                try
                {
                    sqlcom.CommandText = "INSERT INTO karyawan " +
                        "" +
                        "VALUES (@name,@address,@gender,@salaryid)";
                    sqlcom.ExecuteNonQuery();
                    sqltrans.Commit();
                    Console.WriteLine("Berhasil!");
                    Console.WriteLine("");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Tambah_Karyawan....");
                    Console.WriteLine(ex.Message);
                }
            }
                return View();
        }

        //UPDATE
        //-GET
        public IActionResult Edit(Karyawan karyawan)
        {
            using (sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                SqlTransaction sqltrans = sqlConn.BeginTransaction();
                sqlcom = sqlConn.CreateCommand();
                sqlcom.Transaction = sqltrans;

                //id
                sqlpar = new SqlParameter();
                sqlpar.ParameterName = "@id";
                sqlpar.Value = karyawan.id;

                sqlcom.Parameters.Add(sqlpar);
                //nama
                SqlParameter sqlpar0 = new SqlParameter();

                sqlpar0.ParameterName = "@name";
                sqlpar0.Value = karyawan.name;

                sqlcom.Parameters.Add(sqlpar0);

                //alamat
                SqlParameter sqlpar1 = new SqlParameter();
                sqlpar1.ParameterName = "@address";
                sqlpar1.Value = karyawan.address;

                sqlcom.Parameters.Add(sqlpar1);

                //gender
                SqlParameter sqlpar2 = new SqlParameter();
                sqlpar2.ParameterName = "@gender";
                sqlpar2.Value = karyawan.gender;

                sqlcom.Parameters.Add(sqlpar2);

                //salaryid
                SqlParameter sqlpar3 = new SqlParameter();
                sqlpar3.ParameterName = "@salaryid";
                sqlpar3.Value = karyawan.salaryid;

                sqlcom.Parameters.Add(sqlpar3);


                try
                {
                    sqlcom.CommandText = "UPDATE karyawan SET " +
                        "name = @name,address = @address,gender= @gender," +
                        "salaryid = @salaryid  " +
                        "WHERE personid = @id";
                    sqlcom.ExecuteNonQuery();
                    sqltrans.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Update_Karyawan....");
                    Console.WriteLine(ex.Message);
                }

            }
            return View();
        }

        public IActionResult Delete()
        {
            using (sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                SqlTransaction sqltrans = sqlConn.BeginTransaction();

                sqlcom = sqlConn.CreateCommand();
                sqlcom.Transaction = sqltrans;
                Karyawan karyawan = new Karyawan();

                sqlcom.CommandText = "DELETE karyawan " +
                        "WHERE personid = " + karyawan.id;
                sqlcom.ExecuteNonQuery();
                sqltrans.Commit();
                List<Karyawan> karyawans = new List<Karyawan>();
                try
                {
                    sqlConn.Open();
                    using (SqlDataReader sqlDataReader = sqlcom.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                
                                karyawan.id = Convert.ToInt32(sqlDataReader[0]);
                                karyawan.name = (sqlDataReader[1]).ToString();
                                karyawan.address = (sqlDataReader[2]).ToString();
                                karyawan.gender = Convert.ToBoolean(sqlDataReader[3]);
                                karyawan.salaryid = Convert.ToInt32(sqlDataReader[4]);
                                
                                karyawans.Add(karyawan);

                            }
                        }
                        else
                        {
                            Console.WriteLine("No Data Rows");
                        }
                        sqlDataReader.Close();
                    }
                    sqlConn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Hapus_Karyawan....");
                    Console.WriteLine(ex.Message);
                }
            }
                
            return View();
        }
        //-POST

        //DELETE
        //-GET
        //-POST
    }
}
