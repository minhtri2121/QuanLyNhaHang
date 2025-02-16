using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using QuanLyNhaHang.DTO;

namespace QuanLyNhaHang.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get 
            {
                if (instance == null) instance = new BillDAO();
                return instance; 
            }
            private set { BillDAO.instance = value; }
        }
        private BillDAO() { }

        public int GetUncheckBillIDByTableID(int id)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT * FROM HOA_DON WHERE IDBan = " + id + " AND TrangThai = N'Chưa thanh toán'");
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.IdHoaDon;
            }
            return -1;
        }

        public void InsertBill(int idban, int idnguoidung)
        {
            DataProvider.Instance.ExcuteQuery("EXEC InsertBill @idban , @idnguoidung ", new object[] { idban, idnguoidung});
        }

        public int GetMaxIdBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExcuteNonScalar("SELECT MAX(IDHoaDon) FROM HOA_DON");
            }
            catch
            {
                return 1;
            }
        }

        public void CheckOut(int idhoadon, int giamgia, double tongtien)
        {
            string query = "UPDATE HOA_DON SET TrangThai = N'Đã thanh toán', GioThanhToan = CONVERT(TIME(7), GETDATE()), Ca = CASE \r\nWHEN GioVao >= '00:00:00' AND GioVao < '06:00:00' THEN N'Khuya'\r\nWHEN GioVao >= '06:00:00' AND GioVao < '12:00:00' THEN N'Sáng'\r\nWHEN GioVao >= '12:00:00' AND GioVao < '18:00:00' THEN N'Chiều'\r\nELSE N'Tối' END, GiamGia = " + giamgia + ", TongTien = " + tongtien + " WHERE IDHoaDon = " + idhoadon;
            DataProvider.Instance.ExcuteNonQuery(query);
        }

        public DataTable GetBillByDate(DateTime date1, DateTime date2)
        {
            string query = "EXEC DoanhThuByNgay @fisrtDate , @finalDate ";

            return DataProvider.Instance.ExcuteQuery(query, new object[] {date1, date2});
        }

        public DataTable GetBillByNguoiDung(string NguoiDung)
        {
            string query = "EXEC DoanhThuByNguoiDung @idNguoiDung ";

            return DataProvider.Instance.ExcuteQuery(query, new object[] { NguoiDung });
        }
        public DataTable GetBillByTenBan(string tenBan)
        {
            string query = "EXEC DoanhThuByBan @tenBan ";

            return DataProvider.Instance.ExcuteQuery(query, new object[] { tenBan });
        }

        internal void CheckOut(int idBill, int giamGia, double? tongTien)
        {
            throw new NotImplementedException();
        }
    }
}
