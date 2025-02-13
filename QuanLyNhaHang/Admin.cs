using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;
using QuanLyNhaHang.DAO;
using QuanLyNhaHang.DTO;

namespace QuanLyNhaHang
{
    public partial class fAdmin : Form
    {
        BindingSource Foodlist = new BindingSource();

        BindingSource accountList = new BindingSource();

        public Account loginAccount;
        public fAdmin()
        {
            InitializeComponent();

            LoadAdmin();
        }

        // Code lại từ đây |

        #region methods

        List<Food> SearchFoodByName(string tenmon)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(tenmon);
            return listFood;
        }

        private void LoadAdmin()
        {
            dtgvMonAn.DataSource = Foodlist;

            dtgvTaiKhoan.DataSource = accountList;

            LoadQuanLiKho();

            LoadListFood();

            LoadAccount();

            LoadCategoryIntoComboBox(cbNhomMon);

            LoadDVTIntoComboBox(cbDVT);

            AddFoddBinding();

            AddAccountBinding();
        }

        void AddAccountBinding()
        {
            txtTenTk.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "TenDangNhap",true, DataSourceUpdateMode.Never));
            txtTenHienThi.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "TenNguoiDung", true, DataSourceUpdateMode.Never));
            nmLoaiTK.DataBindings.Add(new Binding("Value", dtgvTaiKhoan.DataSource, "Admin", true, DataSourceUpdateMode.Never));
        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }
        void LoadListFood() //không dùng cái này
        {
            string query = "SELECT TuKhoa, TenMon, TenNhomMon , TenDVT,FORMAT ( Gia ,'0') AS GiaTien\r\nFROM MON \r\nJOIN NHOM_MON ON MON.IDNhomMon = NHOM_MON.IDNhomMon\r\nJOIN DON_VI_TINH ON MON.IDDVT = DON_VI_TINH.IDDVT;";

            Foodlist.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        void LoadQuanLiKho() //không dùng cái này
        {
            string query = "SELECT  LMH.IDLoaiMH,LMH.TenLoaiMH, MH.IDMatHang, MH.TenMatHang , FORMAT(MH.GiaNhap , '0' ) AS GiaNhap , MH.HanSuDung\r\nFROM LOAI_MAT_HANG LMH JOIN MAT_HANG MH\r\nON LMH.IDLoaiMH = MH.IDLoaiMH;\r\n";

            dtgvKho.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }

        void AddAccount(string userName, string displayName, int type)
        {
          if (  AccountDAO.Instance.InsertAccount(userName, displayName, type))
          {
                MessageBox.Show("Thêm tài khoản thành công");
          }
          else
          {
                MessageBox.Show("Thêm tài khoản thất bại");
          }

          LoadAccount();
        }

        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }

            LoadAccount();
        }

        void DeleteAccount(string userName)
        {
            if (loginAccount.tenDangNhap.Equals(userName))
            {
                MessageBox.Show("Vui lòng đừng xoá chính bạn chứ");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xoá tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xoá tài khoản thất bại");
            }

            LoadAccount();
        }

        void ResetPass (string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }
        }
        void AddFoddBinding()
        {
            txtTenMon.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "TenMon", true, DataSourceUpdateMode.Never));
            txtTuKhoa.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "TuKhoa", true, DataSourceUpdateMode.Never));
            cbNhomMon.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "TenNhomMon", true, DataSourceUpdateMode.Never));
            cbDVT.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "TenDVT", true, DataSourceUpdateMode.Never));
            nmGia.DataBindings.Add(new Binding("Value", dtgvMonAn.DataSource, "GiaTien", true, DataSourceUpdateMode.Never));
        }
        void LoadCategoryIntoComboBox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetCategory();
            cb.DisplayMember = "Name";
        }
        void LoadDVTIntoComboBox(ComboBox cb)
        {
            cb.DataSource = FoodDAO.Instance.GetDVT();
            cb.DisplayMember = "Name";
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            string tukhoa = txtTuKhoa.Text;
            string tenmon = txtTenMon.Text;
             int idnhommon = (cbNhomMon.SelectedItem as Category).Id;
            int iddvt = (cbDVT.SelectedItem as DVT).Id;
            float Gia = (float)nmGia.Value;

            if (FoodDAO.Instance.InsertFood(tukhoa, tenmon, iddvt, idnhommon, (int)Gia))
            {
                MessageBox.Show("Thêm món ăn thành công.");
                LoadListFood();
            }       
            else
            {
                MessageBox.Show("Có lỗi khi thêm món ăn.");
            }            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string tukhoa = txtTuKhoa.Text;
            string tenmon = txtTenMon.Text;
            int idnhommon = (cbNhomMon.SelectedItem as Category).Id;
            int iddvt = (cbDVT.SelectedItem as DVT).Id;
            float Gia = (float)nmGia.Value;

            if (FoodDAO.Instance.InsertFood(tukhoa, tenmon, iddvt, idnhommon, (int)Gia))
            {
                MessageBox.Show("Sửa món ăn thành công.");
                LoadListFood();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa món ăn.");
            }
        }

        

        private void btnTim_Click(object sender, EventArgs e)
        {

            List<Tim> listTimFood = FoodDAO.Instance.Gettim(txtScFoodName.Text);
            if (listTimFood != null && listTimFood.Count > 0)
            {
                dtgvMonAn.DataSource = typeof(List<Tim>);
                dtgvMonAn.DataSource = listTimFood;

            }
            else
            {
                MessageBox.Show("Không tìm thấy món ăn nào.");
            }

        }

        private void btnXemTK_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnThemTK_Click(object sender, EventArgs e)
        {
            string userName = txtTenTk.Text;
            string displayName = txtTenHienThi.Text;
            int type = (int)nmLoaiTK.Value;
            
            AddAccount(userName, displayName, type);
        }

        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            string userName = txtTenTk.Text;
            DeleteAccount(userName);
        }

        private void btnSuaTK_Click(object sender, EventArgs e)
        {
            string userName = txtTenTk.Text;
            string displayName = txtTenHienThi.Text;
            int type = (int)nmLoaiTK.Value;

            EditAccount(userName, displayName, type);
        }

        private void btnRePass_Click(object sender, EventArgs e)
        {
            string userName = txtTenTk.Text;
            ResetPass(userName);
        }


        #endregion

    }
}