using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace FormMenu
{
    public partial class Form1 : Form
    {
        OleDbConnection koneksi = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\TUGAS KULIAH\TUGAS KULIAH SEMESTER 4\PEMROGRAMAN VISUAL\FormMenu\Database\dbmenu.mdb");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbmenuDataSet.tb_menu' table. You can move, or remove it, as needed.
            this.tb_menuTableAdapter.Fill(this.dbmenuDataSet.tb_menu);

            koneksi.Open();
            string query = "select * from tb_menu";
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" && txtNama.Text == "" && txtHarga.Text == "")
            {
                MessageBox.Show("Isi Field Kosong!", "Peringatan!");
            }

            try
            {
                string sql = string.Format("insert into tb_menu values('{0}','{1}','{2}')", txtId.Text, txtNama.Text, txtHarga.Text);
                OleDbCommand perintah = new OleDbCommand(sql, koneksi);
                perintah.ExecuteNonQuery();
                MessageBox.Show("Data Tersimpan!");
                perintah.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal Menyimpan / Input Nama Sama");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update tb_menu set nama='" + txtNama.Text + "',harga='" + txtHarga.Text + "'where nama ='" + txtNama.Text + "'");
                OleDbCommand perintah = new OleDbCommand(sql, koneksi);
                perintah.ExecuteNonQuery();
                MessageBox.Show("Data berhasil diedit");
                perintah.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Data Gagal Diedit");
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("delete from tb_menu where nama ='" + txtNama.Text + "'");
                OleDbCommand perintah = new OleDbCommand(sql, koneksi);
                perintah.ExecuteNonQuery();
                MessageBox.Show("Data Terhapus!");
                perintah.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Data Gagal Dihapus");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from tb_menu";
                OleDbCommand perintah = new OleDbCommand(query, koneksi);
                DataSet ds = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter(perintah);
                adapter.Fill(ds, "res");
                dataGridView1.DataSource = ds.Tables["res"];
                adapter.Dispose();
                perintah.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal Menampilkan Data");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtNama.Text = row.Cells[1].Value.ToString();
            txtHarga.Text = row.Cells[2].Value.ToString();
        }
    }
}
