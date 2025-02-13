using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; }
        }

        private FoodDAO() { }

        public List<Food> GetFoodByCategoryID(int ID)
        {
            List<Food> list = new List<Food>();
            string query = "select *\r\nfrom MON where IDNhomMon = '" + ID + "'";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food f = new Food(item);
                list.Add(f);
            }

            return list;
        }

        public List<DVT> GetDVT()
        {
            List<DVT> list = new List<DVT>();
            string query = "SELECT * FROM DON_VI_TINH";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DVT d = new DVT(item);
                list.Add(d);
            }

            return list;
        }

        public List<Tim> Gettim(string tenmon)
        {
            List<Tim> list = new List<Tim>();
            string query = "Select\tm.TuKhoa, m.TenMon, nm.TenNhomMon, dvt.TenDVT, format(m.Gia, '0') as Gia\r\nfrom Mon m join nhom_mon nm  on m.IDNhomMon = nm.IDNhomMon\r\n\t\t\tjoin Don_vi_tinh dvt on dvt.IDDVT = m.IDDVT WHERE m.TenMon LIKE N'%" + tenmon + "%'";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Tim t = new Tim(item);
                list.Add(t);
            }

            return list;
        }

        public List<Food> SearchFoodByName(string keyword)
        {
            List<Food> list = new List<Food>();
            try
            {
                string query = "SELECT MON.TuKhoa, MON.TenMon, NHOM_MON.TenNhomMon, DON_VI_TINH.TenDVT, MON.Gia AS GiaTien FROM MON JOIN NHOM_MON ON MON.IDNhomMon = NHOM_MON.IDNhomMon JOIN DON_VI_TINH ON MON.IDDVT = DON_VI_TINH.IDDVT WHERE MON.TenMon LIKE N'%' + @tenmon + '%'";
                DataTable data = DataProvider.Instance.ExcuteQuery(query, new object[] { "%" + keyword + "%" });

                foreach (DataRow item in data.Rows)
                {
                    Food f = new Food(item);
                    list.Add(f);
                }

                foreach (DataColumn col in data.Columns)
                {
                    Console.WriteLine(col.ColumnName);  // In tên từng cột ra console
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }

            return list;
        }




        public bool InsertFood(string tukhoa, string tenmon,int iddvt , int idnhommon,int gia)
        {
            string query = "INSERT MON( TuKhoa, TenMon, IDDVT, IDNhomMon, Gia) VALUES(N'"+ tukhoa + "', N'" + tenmon + "', "+ iddvt + ", N'" + idnhommon + "', "+ gia + ")";
            int result = DataProvider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateFood(string tukhoa, string tenmon, int iddvt, int idnhommon, int gia)
        {
            string query = string.Format("UPDATE dbo.MON SET TuKhoa = N'{0}', TenMon = N'{1}', IDNhomMon = {2}, IDDVT = {3}, Gia = {4} Where IDMon = {5}", tukhoa, tenmon, iddvt, idnhommon, gia);
            int result = DataProvider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }




    }
}