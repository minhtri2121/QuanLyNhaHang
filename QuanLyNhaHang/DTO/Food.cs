using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class Food
    {
        public Food(int id, string name, string idnhom, string namenhom, int iddvt, int gia)
        {
            this.Id = id;
            this.Name = name;
            this.Idnhom = idnhom;
            this.Namenhom = namenhom;
            this.Iddvt = iddvt;
            this.Tendvt = tendvt;
            this.Gia = gia;
        }

        public Food(DataRow row)
        {
            this.Id = (int)row["IDMon"];
            this.Name = row["TenMon"].ToString();
            this.Idnhom = row["IDNhomMon"].ToString();
            this.Iddvt = (int)row["IDDVT"];
            this.Gia = row["Gia"] != DBNull.Value ? Convert.ToSingle(row["Gia"]) : 0;

        }

        private int id;
        private string name;
        private string idnhom;
        private string namenhom;
        private int iddvt;
        private string tendvt;
        private float gia;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name {
            get { return name; }
            set { name = value; } 
        }
        public string Idnhom
        {
            get { return idnhom; }
            set { idnhom = value; }
        }
        public string Namenhom
        {
            get { return namenhom; }
            set { namenhom = value; }
        }
        public int Iddvt
        {
            get { return iddvt ; }
            set { iddvt  = value; }
        }
        public float Gia
        {
            get { return gia ; }
            set { gia = value; }
        }
        public string Tendvt
        {
            get { return tendvt; }
            set { tendvt = value; }
        }
    }
}
