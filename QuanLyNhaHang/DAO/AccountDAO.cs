using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        public static Account CurrentUser { get; private set; }

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }

        public int IDNguoiDung
        {
            get; set;
        }

        public bool Login(string username, string password)
        {
            string query = "EXEC LoadLogin @username , @password ";

            DataTable result = DataProvider.Instance.ExcuteQuery(query, new object[] { username, password });
            if (result.Rows.Count > 0)
            {
                CurrentUser = new Account(result.Rows[0]); // Lưu tài khoản đăng nhập vào biến static
                return true;
            }

            return false;
        }

        public bool UpdateAccount (string userName, string displayName , string pass, string newpass)
        {
           int result = DataProvider.Instance.ExcuteNonQuery("exec USP_UpdateAccount @userName  , @displayName  , @password , @newPassword", new object[] {userName,displayName,pass,newpass});
            return result > 0;
        }

        public DataTable GetListAccount() 
        {
            return DataProvider.Instance.ExcuteQuery("SELECT TenDangNhap, TenNguoiDung, CAST(Admin AS INT) AS Admin FROM Nguoi_Dung\r\n");
        }
        public Account GetAccountByUserName(string username)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("select * from NGUOI_DUNG where TenDangNhap = '" + username + "'");
            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }

        internal Account GetAccountByAdmin(object admin)
        {
            throw new NotImplementedException();
        }

        public bool InsertAccount (string name , string displayName , int type )
        {
            string querry = string.Format("INSERT dbo.NGUOI_DUNG(TenDangNhap, TenNguoiDung, Admin) VALUES (N'{0}', N'{1}', {2})", name,displayName,type);
            int result = DataProvider.Instance.ExcuteNonQuery(querry);

            return result > 0;
        }

        public bool UpdateAccount(string name, string displayName, int type)
        {
            string querry = string.Format("UPDATE dbo.NGUOI_DUNG SET  TenNguoiDung = N'{1}', Admin = {2}  WHERE  TenDangNhap= N'{0}'", name, displayName, type);
            int result = DataProvider.Instance.ExcuteNonQuery(querry);

            return result > 0;
        }

        public bool DeleteAccount(string name)
        {
            string querry = string.Format("DELETE NGUOI_DUNG WHERE TenDangNhap = N'{0}'", name);
            int result = DataProvider.Instance.ExcuteNonQuery(querry);

            return result > 0;
        }
        public bool ResetPassword(string name)
        {
            string querry = string.Format("UPDATE NGUOI_DUNG SET MatKhau = N'0' WHERE TenDangNhap = N'{0}'", name);
            int result = DataProvider.Instance.ExcuteNonQuery(querry);

            return result > 0;
        }

        public int GetIdAccount(string name)
        {
            try
            {
                return (int)DataProvider.Instance.ExcuteNonScalar("SELECT IDNguoiDung FROM NGUOI_DUNG WHERE TenDangNhap = N'" + name + "'");
            }
            catch
            {
                return 1;
            }
        }
    }
}
