using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using QuanLyNhaHang.DTO;

namespace QuanLyNhaHang.DAO
{
    internal class TableDAO
    {
        public static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance;  }
            private set { TableDAO.instance = value; }
        }

        public static int TableWidth = 80;
        public static int TableHieght = 80;

        private TableDAO() { }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table> ();

            DataTable data = DataProvider.Instance.ExcuteQuery("EXEC LoadTable;");

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;
        }

        public void DeleteTableByTableID(int id)
        {
            string query = "delete BAN where IDBan = " + id;
            DataProvider.Instance.ExcuteQuery(query);

        }

        public void SwitchTable(int id1, int id2, int idnguoidung)
        {
            string query = "EXEC USPSwitchTabe @idTable1 , @idTable2 , @idnguoidung ";
            DataProvider.Instance.ExcuteQuery(query, new object[] { id1, id2, idnguoidung });
        }

        public void MergeTable(int id1, int id2, int idnguoidung)
        {
            string query = "EXEC MergeTable @idTable1 , @idTable2 , @idnguoidung ";
            DataProvider.Instance.ExcuteQuery(query, new object[] { id1, id2, idnguoidung });
        }

        public List<Table> GetTableList()
        {
            List<Table> tableList = new List<Table>();
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT BAN.IDBan, BAN.TenBan, BAN.TrangThai, KHU_VUC.IDKhuVuc, KHU_VUC.TenKhuVuc FROM BAN INNER JOIN KHU_VUC ON BAN.IDKhuVuc = KHU_VUC.IDKhuVuc ");
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }
            return tableList;
        }

        public string GetTableByStatus(int id)
        {
            string query = "SELECT TrangThai FROM BAN WHERE IDBan = @id";
            object result = DataProvider.Instance.ExcuteNonScalar(query, new object[] { id });

            return result != null ? result.ToString() : "Không xác định"; 
        }

        public bool InsertTableFood (string name , int idzone)
        {
            string query = string.Format(
                "INSERT BAN( TenBan, IDKhuVuc ) VALUES( N'{0}', {1})",
                name, idzone
            );
            int result = DataProvider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateTableFood(int idban , string name, int? idzone)
        {
            string query = string.Format("UPDATE BAN SET TenBan = N'{0}', IDKhuVuc = {1} WHERE IDBan = {2}", name, idzone, idban);

            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteTableFood(int idban)
        {
            // Bước 1: Xóa các bản ghi trong CTHOADON liên quan đến IDHoaDon (nếu có)
            string deleteCTHoaDonQuery = string.Format("DELETE FROM CTHOADON WHERE IDHoaDon IN (SELECT IDHoaDon FROM HOA_DON WHERE IDBan = {0})", idban);
            DataProvider.Instance.ExcuteNonQuery(deleteCTHoaDonQuery);

            // Bước 2: Xóa các bản ghi trong HOA_DON liên quan đến IDBan
            string deleteHoaDonQuery = string.Format("DELETE FROM HOA_DON WHERE IDBan = {0}", idban);
            DataProvider.Instance.ExcuteNonQuery(deleteHoaDonQuery);

            // Bước 3: Xóa bản ghi trong BAN
            string query = string.Format("DELETE FROM BAN WHERE IDBan = {0}", idban);
            int result = DataProvider.Instance.ExcuteNonQuery(query);

            // Trả về true nếu xóa thành công, ngược lại trả về false
            return result > 0;
        }
    }
}
