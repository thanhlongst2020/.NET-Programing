using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class ThanhToan : Form
    {
        int MaPhong;
        int SoHD;
        string TenKH;
        int SoCMND;
        decimal SoTien;
        string NgayTT;
        int Phong;
        
        public ThanhToan()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void ResetFields(bool status)
        {
            /*txtbMSCB.Clear();
            txtbHoTenCB.Clear();
            cmbChucVu.SelectedIndex = -1;
            txtbSoGioGiang.Clear();
            txtbDonGia.Clear();
            btnSave.Enabled = status;
            btnCancel.Enabled = status;
            btnAdd.Enabled = !status;
            txtbMSCB.Focus();*/

            txtSoHD.Clear();
            txtHoTen.Clear();
            txtSoCMND.Clear();
            cmbSoPhong.SelectedIndex = -1;
            txtSoTienTT.Clear();
            dateTimePickerNgayTT.Value = DateTime.Now;
            btnSave.Enabled = status;
            btnAdd.Enabled = !status;
        }

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            ResetFields(false);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ResetFields(true);
            LoadComboBox();
            LoadSoHD();



        }

        private void LoadComboBox()
        {
            clsData.OpenConnection();
            string Sql = "select * from Phong";
            SqlCommand cmd = new SqlCommand(Sql, clsData.con);
            SqlDataReader DR = cmd.ExecuteReader();
            var DT = new DataTable();
            DT.Load(DR);
            DR.Dispose();
            cmbSoPhong.DisplayMember = "TenPhong";
            cmbSoPhong.ValueMember = "MaPhong";
            cmbSoPhong.DataSource = DT;

        }

        private void LoadSoHD()
        {
            // Auto load MaCB
            try
            {
                clsData.OpenConnection();
                SqlCommand com = new SqlCommand("Select Max(SoHD) From KhachHang", clsData.con);
                SoHD = Convert.ToInt32(com.ExecuteScalar());
                clsData.CloseConnection();
            }
            catch (Exception)
            {
                SoHD = 0;
            }
            SoHD++;
            txtSoHD.Text = SoHD.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strInsert = "Insert into KhachHang Values(@SoHD, @TenKH, @SoCMND, @SoTien, @NgayTT,@Phong)";
                clsData.OpenConnection();
                SqlCommand com = new SqlCommand(strInsert, clsData.con);

                SqlParameter p1 = new SqlParameter("@SoHD", SqlDbType.Int);
                p1.Value = SoHD;

                SqlParameter p2 = new SqlParameter("@TenKH", SqlDbType.NVarChar);
                p2.Value = txtHoTen.Text;

                SqlParameter p3 = new SqlParameter("@SoCMND", SqlDbType.Int);
                p3.Value = Convert.ToInt32(txtSoCMND.Text);

                SqlParameter p4 = new SqlParameter("@SoTien", SqlDbType.Money);
                p4.Value = Convert.ToDecimal(txtSoTienTT.Text);

                SqlParameter p5 = new SqlParameter("@NgayTT", SqlDbType.DateTime);
                p5.Value = Convert.ToDateTime(dateTimePickerNgayTT.Value); 
                
                SqlParameter p6 = new SqlParameter("@Phong", SqlDbType.Int);
                p6.Value = Phong;

                com.Parameters.Add(p1);
                com.Parameters.Add(p2);
                com.Parameters.Add(p3);
                com.Parameters.Add(p4);
                com.Parameters.Add(p5);
                com.Parameters.Add(p6);
                com.ExecuteNonQuery();


                MessageBox.Show("Insert successfully!!");
                clsData.CloseConnection();
                ResetFields(false);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSoPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSoPhong.SelectedIndex == -1) return;
            Phong = Convert.ToInt32(cmbSoPhong.SelectedValue);
        }
    }
}
