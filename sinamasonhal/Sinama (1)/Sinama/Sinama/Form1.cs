using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Sinama
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection _baglan = new SqlConnection("Data Source=DESKTOP-UVEKSIR\\SQLEXPRESS;Initial Catalog=ScrumTaskBoard;Integrated Security=True");
        public void connect()
        {
            if (_baglan.State == ConnectionState.Closed)
            {
                _baglan.Open();
            }
        }
        public void ProgramYenile()
        {

            connect();
            Creater creater = new Creater();

            SqlCommand command = new SqlCommand("Select * From tbl_card", _baglan);
            SqlDataReader reader = command.ExecuteReader();
            Olustur panel1 = new bolum1();
            Olustur panel2 = new bolum2();
            Olustur panel3 = new bolum3();
            Olustur panel4 = new bolum4();
            Olustur panel5 = new bolum5();

            List<Olustur> pnl1 = new List<Olustur>();
            List<Olustur> pnl2 = new List<Olustur>();
            List<Olustur> pnl3 = new List<Olustur>();
            List<Olustur> pnl4 = new List<Olustur>();
            List<Olustur> pnl5 = new List<Olustur>();
            List<Olustur> pnl51 = new List<Olustur>();

            while (reader.Read())
            {
                string PanelID = reader["KartListNo"].ToString();
                switch (PanelID)
                {
                    case "1":
                        pnl1 = panel1.kartOlustur(reader["KartNo"].ToString(), reader["ProjeAd"].ToString());

                        break;

                    case "2":
                        pnl2 = panel2.kartOlustur(reader["KartNo"].ToString(), reader["ProjeAd"].ToString());

                        break;

                    case "3":
                        pnl3 = panel3.kartOlustur(reader["KartNo"].ToString(), reader["ProjeAd"].ToString());

                        break;

                    case "4":
                        pnl4 = panel4.kartOlustur(reader["KartNo"].ToString(), reader["ProjeAd"].ToString());

                        break;

                    case "5":
                        pnl5 = panel5.kartOlustur(reader["KartNo"].ToString(), reader["ProjeAd"].ToString());


                        break;

                }
            }
            reader.Close();
            _baglan.Close();
            dGVlist1.DataSource = pnl1;
            dGVlist2.DataSource = pnl2;
            dGVlist3.DataSource = pnl3;
            dGVlist4.DataSource = pnl4;
            dGVlist5.DataSource = pnl5;
            metroComboBox1.Items.Add(1);
            metroComboBox1.Items.Add(2);
            metroComboBox1.Items.Add(3);
            metroComboBox1.Items.Add(4);
            metroComboBox1.Items.Add(5);
        
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            ProgramYenile();

        }

        private void dGVlist1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblSeciliKart.Text = dGVlist1.CurrentRow.Cells[0].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lblSeciliKart.Text == "")
            {
                MessageBox.Show("Lütfen Kart Seçin");
            }
            else
            {
                connect();
                SqlCommand command = new SqlCommand("Delete From tbl_card where KartNo=@KartNo", _baglan);
                command.Parameters.AddWithValue("@KartNo", lblSeciliKart.Text);
                command.ExecuteNonQuery();
                _baglan.Close();
                ProgramYenile();
                MessageBox.Show("Başarıyla Silindi");
  
            }
        }

        private void dGVlist2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblSeciliKart.Text = dGVlist2.CurrentRow.Cells[0].Value.ToString();
        }

        private void dGVlist3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblSeciliKart.Text = dGVlist3.CurrentRow.Cells[0].Value.ToString();
        }

        private void dGVlist4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblSeciliKart.Text = dGVlist4.CurrentRow.Cells[0].Value.ToString();
        }

        private void dGVlist5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblSeciliKart.Text = dGVlist5.CurrentRow.Cells[0].Value.ToString();
        }
        
       
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lblSeciliKart.Text == "")
            {
                MessageBox.Show("Lütfen Kart Seçin");
            }
            else
            {


                infoCard ic = new infoCard();

                SqlCommand command = new SqlCommand("Select * From tbl_card where KartNo=@KartNo", _baglan);
                connect();
                command.Parameters.AddWithValue("@KartNo", lblSeciliKart.Text);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    ic.Kartno = reader["KartNo"].ToString();
                    ic.Pad = reader["ProjeAd"].ToString();
                    ic.uzman = reader["TeknikUzman"].ToString();
                    ic.tarih = reader["Tarih"].ToString();
                    ic.TahSure = reader["TahminiSure"].ToString();
                    ic.gerSure = reader["GerceklesenSure"].ToString();
                    ic.aciklama = reader["IsAciklama"].ToString();
                    ic.notlar = reader["Notlar"].ToString();
                    ic.kartListno = reader["KartListNo"].ToString();
                    ic.butondegeri = "1";
                }

                ic.ProgramGetir();
                reader.Close();
                _baglan.Close();
                ic.Show();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)

        {
            infoCard ic = new infoCard();
            ic.butondegeri = "2";
            ic.Show();

            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lblSeciliKart.Text == "" || metroComboBox1.Text == "")
            {

                MessageBox.Show("kartseçin veya alanı seçin");
            }
            else
            {
                connect();
                SqlCommand command = new SqlCommand("Update From tbl_card  SET KartListNo where KartNo = @KartNo", _baglan);
                
                command.ExecuteNonQuery();
                command.Parameters.AddWithValue("@KartNo", lblSeciliKart.Text.ToString());
                command.Parameters.AddWithValue("@KartListNo", Convert.ToInt32(metroComboBox1.Text));
                MessageBox.Show("Kart Guncellendi..");
                _baglan.Close();

            }
        }
    }
}
