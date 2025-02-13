using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
        public class Tim
        {
            public Tim(string tukhoa, string tenmon, string tennhommon, string tendvt, int gia)
            {
                this.TuKhoa = tukhoa;
                this.TenMon = tenmon;   
                this.TenNhomMon = tennhommon;
                this.TenDVT = tendvt;
                this.Gia = gia;
            }

            private string tuKhoa;
            private string tenMon;
            private string tenNhomMon;
            private string tenDVT;
            private int gia;


            public string TuKhoa
            {
                get { return tuKhoa; }
                set { tuKhoa = value; }
            }
            public string TenMon
            {
                get { return tenMon; }
                set { tenMon = value; }
            }
        public string TenNhomMon
        {
            get { return tenNhomMon; }
            set { tenNhomMon = value; }
        }
        public string TenDVT
        {
            get { return tenDVT; }
            set { tenDVT = value; }
        }
        public int Gia
        {
            get { return gia; }
            set { gia = value; }
        }
        // Constructor nhận DataRow và gán giá trị
        public Tim(DataRow row)
        {
            this.TuKhoa = row.Table.Columns.Contains("TuKhoa") && row["TuKhoa"] != DBNull.Value ? row["TuKhoa"].ToString() : string.Empty;
            this.TenMon = row.Table.Columns.Contains("TenMon") && row["TenMon"] != DBNull.Value ? row["TenMon"].ToString() : string.Empty;
            this.TenNhomMon = row.Table.Columns.Contains("TenNhomMon") && row["TenNhomMon"] != DBNull.Value ? row["TenNhomMon"].ToString() : string.Empty;
            this.TenDVT = row.Table.Columns.Contains("TenDVT") && row["TenDVT"] != DBNull.Value ? row["TenDVT"].ToString() : string.Empty;
            this.Gia = row.Table.Columns.Contains("Gia") && row["Gia"] != DBNull.Value ? Convert.ToInt32(row["Gia"]) : 0;
        }

    }
}
