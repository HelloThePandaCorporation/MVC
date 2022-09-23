using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTC_CRUD_MVC.Models
{
    public class Karyawan
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public bool gender { get; set; }
        public int salaryid { get; set; }
        public decimal salary { get; set; }
        public string type_salary { get; set; }
    }
    public class Karyawan_ids
    {
        public int id { get; set; }
    }
        public class Karyawan_id
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public bool gender { get; set; }
        public int salaryid { get; set; }
    }
    public class New_Karyawan
    {
        public string name { get; set; }
        public string address { get; set; }
        public bool gender { get; set; }
        public int salaryid { get; set; }
    }
}
