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
using System.Data.SqlClient;

namespace Kitaplık_Access_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\azade\\OneDrive\\Masaüstü\\Kitaplik.mdb");

        public string durum = "";

        void Listele()
        {
            conn.Open();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * from Kitaplar", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();

        }
        void Temizle()
        {
            txtKitapid.Clear();
            txtKitapAd.Clear();
            txtSayfa.Clear();
            cmbTur.Text = " ";
            txtYazar.Clear();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();

        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Yeni Kitap Kaydı Yapma

            conn.Open();
            OleDbCommand Kaydet = new OleDbCommand("INSERT INTO  Kitaplar (KitapAd,Yazar,SayfaSayisi,Tur,Durum) values (@p1,@p2,@p3,@p4,@p5)", conn);
            Kaydet.Parameters.AddWithValue("@p1", txtKitapAd.Text);
            Kaydet.Parameters.AddWithValue("@p2", txtYazar.Text);
            Kaydet.Parameters.AddWithValue("@p3", txtSayfa.Text);
            Kaydet.Parameters.AddWithValue("@p4", cmbTur.Text);
            Kaydet.Parameters.AddWithValue("@p5", durum);
            Kaydet.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Kitap Başarıyla Eklendi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ///datagrid den seçilen alanı araçlara aktarma

            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtKitapid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtKitapAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbTur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtSayfa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            durum = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }


        }

        private void btnSil_Click(object sender, EventArgs e)
        { 
            ///Kitap Silme

            conn.Open();
            OleDbCommand cmd = new OleDbCommand("Delete from Kitaplar where Kitapid=@p1", conn);
            cmd.Parameters.AddWithValue("@p1", txtKitapid.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Kitap Başarıyla Silindi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Kitap Güncelleme

            conn.Open();
            OleDbCommand cmd2 = new OleDbCommand("Update Kitaplar set KitapAd=@ka,Yazar=@y,Tur=@t,SayfaSayisi=@ss,Durum=@d where Kitapid=@ki", conn);
            cmd2.Parameters.AddWithValue("@ka", txtKitapAd.Text);
            cmd2.Parameters.AddWithValue("@y", txtYazar.Text);
            cmd2.Parameters.AddWithValue("@t", cmbTur.Text);
            cmd2.Parameters.AddWithValue("@ss", txtSayfa.Text);
            cmd2.Parameters.AddWithValue("@d", durum);
            cmd2.Parameters.AddWithValue("@ki",txtKitapid.Text);
            cmd2.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Kitap Başarıyla Güncellendi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();

        }
    }
}
