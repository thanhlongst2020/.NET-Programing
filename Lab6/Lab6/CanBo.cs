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
    public partial class frmCanBo : Form
    {
        int MaCB;
        string TenCB;
        int ChucVuCB;
        int SoGioGiang;
        decimal DonGia;

        public frmCanBo()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ResetFields(true);
            LoadComboBox();

            // Auto load MaCB
            try
            {
                clsDatabase.OpenConnection();
                SqlCommand com = new SqlCommand("Select Max(MaCB) From CanBo", clsDatabase.con);
                MaCB = Convert.ToInt32(com.ExecuteScalar());
                clsDatabase.CloseConnection();
            }
            catch (Exception)
            {
                MaCB = 0;
            }
            MaCB++;
            txtbMSCB.Text = MaCB.ToString();




        }

        private void frmCanBo_Load(object sender, EventArgs e)
        {
            ResetFields(false);
        }

        private void LoadComboBox()
        {
            clsDatabase.OpenConnection();
            string Sql = "select * from ChucVu";
            SqlCommand cmd = new SqlCommand(Sql, clsDatabase.con);
            SqlDataReader DR = cmd.ExecuteReader();
            var DT = new DataTable();
            DT.Load(DR);
            DR.Dispose();
            cmbChucVu.DisplayMember = "TenCV";
            cmbChucVu.ValueMember = "MaCV";
            cmbChucVu.DataSource = DT;

        }

        private void ResetFields(bool status)
        {
            txtbMSCB.Clear();
            txtbHoTenCB.Clear();
            cmbChucVu.SelectedIndex = -1;
            txtbSoGioGiang.Clear();
            txtbDonGia.Clear();
            btnSave.Enabled = status;
            btnCancel.Enabled = status;
            btnAdd.Enabled = !status;
            txtbMSCB.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strInsert = "Insert into CanBo Values(@MaCB, @TenCB, @ChucVuCB, @SoGioGiang, @DonGia)";
                clsDatabase.OpenConnection();
                SqlCommand com = new SqlCommand(strInsert, clsDatabase.con);

                SqlParameter p1 = new SqlParameter("@MaCB", SqlDbType.Int);
                p1.Value = MaCB;

                SqlParameter p2 = new SqlParameter("@TenCB", SqlDbType.NVarChar);
                p2.Value = txtbHoTenCB.Text;

                SqlParameter p3 = new SqlParameter("@SoGioGiang", SqlDbType.Int);
                p3.Value = Convert.ToInt32(txtbSoGioGiang.Text);

                SqlParameter p4 = new SqlParameter("@DonGia", SqlDbType.Money);
                p4.Value = Convert.ToDecimal(txtbDonGia.Text);

                SqlParameter p5 = new SqlParameter("@ChucVuCB", SqlDbType.Int);
                p5.Value = ChucVuCB;

                com.Parameters.Add(p1);
                com.Parameters.Add(p2);
                com.Parameters.Add(p3);
                com.Parameters.Add(p4);
                com.Parameters.Add(p5);
                com.ExecuteNonQuery();


                MessageBox.Show("Insert successfully!!");
                clsDatabase.CloseConnection();
                ResetFields(false);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChucVu.SelectedIndex == -1) return;
            ChucVuCB = Convert.ToInt32(cmbChucVu.SelectedValue);
        }
    }
}
