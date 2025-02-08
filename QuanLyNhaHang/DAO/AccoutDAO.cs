using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class AccoutDAO
    {
        private static AccoutDAO instance;
        public static AccoutDAO Instance
        {
            get { if (instance == null) instance = new AccoutDAO(); return instance; }
            private set { instance = value; }
        }
        private AccoutDAO() { }
        public bool Login(string username, string password)
        {
            return false;
        }

    }
}
