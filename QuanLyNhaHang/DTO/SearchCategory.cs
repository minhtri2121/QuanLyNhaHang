using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class SearchCategory
    {
        public SearchCategory(int idmathang, string tenmathang, int gianhap, string hansudung, int idloaimh)
        {
            this.IdMH = idmathang;
            this.TenMathang = tenmathang;
            this.giaNhap = gianhap;
            this.Hansudung = hansudung;
            this.IdloaiMH = idloaimh;
        }

        private int idMH;
        private string tenMathang;
        private int Gianhap;
        private string hansuDung;
        private int idLoaimh;


        public int IdMH
        {
            get { return idMH; }
            set { idMH = value; }
        }

        public string TenMathang
        {
            get { return tenMathang; }
            set { tenMathang = value; }
        }
        public int giaNhap
        {
            get { return Gianhap; }
            set { Gianhap = value; }
        }
        public string Hansudung
        {
            get { return hansuDung; }
            set { hansuDung = value; }
        }
        public int IdloaiMH
        {
            get { return idLoaimh; }
            set { idLoaimh = value; }
        }

        public SearchCategory(DataRow row)
        {
            this.IdMH = row.Table.Columns.Contains("IDMatHang") && row["IDMatHang"] != DBNull.Value ? Convert.ToInt32(row["IDMatHang"]) : 0;
            this.TenMathang = row.Table.Columns.Contains("TenMatHang") && row["TenMatHang"] != DBNull.Value ? row["TenMatHang"].ToString() : string.Empty;
            this.giaNhap = row.Table.Columns.Contains("GiaNhap") && row["GiaNhap"] != DBNull.Value ? Convert.ToInt32(row["GiaNhap"]) : 0;
            this.Hansudung = row.Table.Columns.Contains("HanSuDung") && row["HanSuDung"] != DBNull.Value ? row["HanSuDung"].ToString() : string.Empty;
            this.IdloaiMH = row.Table.Columns.Contains("IDLoaiMH") && row["IDLoaiMH"] != DBNull.Value ? Convert.ToInt32(row["IDLoaiMH"]) : 0;
        }

    }
}
