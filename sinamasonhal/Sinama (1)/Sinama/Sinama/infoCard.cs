using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sinama
{
    public partial class infoCard : MetroFramework.Forms.MetroForm
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter dataAdapter;
        public infoCard()
        {
            InitializeComponent();
        }

        void Getir()
        {
            baglanti = new SqlConnection("Data Source=DESKTOP-UVEKSIR\\SQLEXPRESS;Initial Catalog=ScrumTaskBoard;Integrated Security=True");
            baglanti.Open();
            dataAdapter = new SqlDataAdapter("SELECT * FROM tbl_IsTakip", baglanti);
            DataTable tablo = new DataTable();
            dataAdapter.Fill(tablo);
            DGVPackIn.DataSource = tablo;
            baglanti.Close();
        }

        public string TahSure, Kartno, Pad, uzman, gerSure, notlar, aciklama, tarih, kartListno,butondegeri;

        public void ProgramGetir()
        {
            if (butondegeri=="1")
            {
                Getir();
                mtxtKartNo.Text = Kartno;
                mtxtProjeAd.Text = Pad;
                DateTimePickerTarih.Text = tarih;
                mtxtTahminiSure.Text = TahSure;
                mtxtGerceklesenSure.Text = gerSure;
                mtxtUzman.Text = uzman;
                rtxtAciklama.Text = aciklama;
                rtxtNotlar.Text = notlar;
                btn_EKLE.Visible = false;
            }
            if(butondegeri=="2")
            {
                btn_GUNCELLE.Visible = false;
            }
          

        }
        private void infoCard_Load(object sender, EventArgs e)
        {
           
            mtxtTahminiSure.Text = GetPredictTime().ToString();

        }
        public int GetPredictTime()  //******TAHMİN ALGORİTMASI
        {
          
            int median = 0;
            SqlCommand komut1 = new SqlCommand(

           "SELECT ((SELECT MAX(GerceklesenSure) FROM (SELECT TOP 50 PERCENT GerceklesenSure FROM tbl_card ORDER BY GerceklesenSure) AS BottomHalf)" +
           " + (SELECT MIN(GerceklesenSure) FROM (SELECT TOP 50 PERCENT GerceklesenSure FROM tbl_card ORDER BY GerceklesenSure DESC) AS TopHalf)) / 2 AS Median", baglanti);
            baglanti.Open();
            SqlDataReader dataReader = komut1.ExecuteReader();
            while (dataReader.Read())
                median = dataReader["median"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["median"]);
            baglanti.Close();

            return median;
        }

        private void DGVPackIn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKartid.Text = DGVPackIn.CurrentRow.Cells[0].Value.ToString();
            dateTimePickertakip.Text = DGVPackIn.CurrentRow.Cells[1].Value.ToString();
            txtDurum.Text = DGVPackIn.CurrentRow.Cells[2].Value.ToString();
            txtYapIs.Text = DGVPackIn.CurrentRow.Cells[3].Value.ToString();
            txtAciklama.Text = DGVPackIn.CurrentRow.Cells[4].Value.ToString();
        }

        private void btn_GUNCELLE_Click_1(object sender, EventArgs e)
        {
            string sorgu = "UPDATE tbl_card SET ProjeAd=@ProjeAd,TeknikUzman=@TeknikUzman,Tarih=@Tarih,TahminiSure=@TahminiSure,GerceklesenSure=@GerceklesenSure,IsAciklama=@IsAciklama,Notlar=@Notlar WHERE KartNo = @KartNo";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@ProjeAd", mtxtProjeAd.Text);
            komut.Parameters.AddWithValue("@TeknikUzman", mtxtUzman.Text);
            komut.Parameters.AddWithValue("@Tarih", DateTimePickerTarih.Value);
            komut.Parameters.AddWithValue("@TahminiSure", mtxtTahminiSure.Text);
            komut.Parameters.AddWithValue("@GerceklesenSure", mtxtGerceklesenSure.Text);
            komut.Parameters.AddWithValue("@IsAciklama", rtxtAciklama.Text);
            komut.Parameters.AddWithValue("@Notlar", rtxtNotlar.Text);
            komut.Parameters.AddWithValue("@KartNo", mtxtKartNo.Text);
            MessageBox.Show("Teknik Kart Guncellendi..");

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            Getir();
            mtxtTahminiSure.Text = GetPredictTime().ToString();
            this.Close();
        }
       

        private void infoCard_Load_1(object sender, EventArgs e)
        {
           
        }

        private void btn_EKLE_Click_1(object sender, EventArgs e)
        {
           
            Getir();
            mtxtTahminiSure.Text = GetPredictTime().ToString();
            MessageBox.Show("Teknik Kart Kaydı Yapıldı...");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        //******IS TAKİP CRUD*******
        private void DGVPackIn_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtKartid.Text = DGVPackIn.CurrentRow.Cells[0].Value.ToString();
            dateTimePickertakip.Text = DGVPackIn.CurrentRow.Cells[1].Value.ToString();
            txtDurum.Text = DGVPackIn.CurrentRow.Cells[2].Value.ToString();
            txtYapIs.Text = DGVPackIn.CurrentRow.Cells[3].Value.ToString();
            txtAciklama.Text = DGVPackIn.CurrentRow.Cells[4].Value.ToString();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO tbl_IsTakip(Tarih,Durum,YapılacakIs,Aciklama) VALUES (@Tarih,@Durum,@YapılacakIs,@Aciklama)";
            komut = new SqlCommand(sorgu, baglanti);

            //komut.Parameters.AddWithValue("@Kartid", txtKartid.Text);
            komut.Parameters.AddWithValue("@Tarih", dateTimePickertakip.Value);
            komut.Parameters.AddWithValue("@Durum", txtDurum.Text);
            komut.Parameters.AddWithValue("@YapılacakIs", txtYapIs.Text);
            komut.Parameters.AddWithValue("@Aciklama", txtAciklama.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            Getir();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM tbl_IsTakip WHERE Kartid = @Kartid ";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Kartid", Convert.ToInt32(txtKartid.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            Getir();
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE tbl_IsTakip SET Tarih=@Tarih,Durum=@Durum,YapılacakIs=@YapılacakIs,Aciklama=@Aciklama WHERE Kartid = @Kartid";
            komut = new SqlCommand(sorgu, baglanti);

            komut.Parameters.AddWithValue("@Kartid", Convert.ToInt32(txtKartid.Text));
            komut.Parameters.AddWithValue("@Tarih", dateTimePickertakip.Value);
            komut.Parameters.AddWithValue("@Durum", txtDurum.Text);
            komut.Parameters.AddWithValue("@YapılacakIs", txtYapIs.Text);
            komut.Parameters.AddWithValue("@Aciklama", txtAciklama.Text);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            Getir();
        }

        //******KART CRUD********
        private void btn_EKLE_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO tbl_card(ProjeAd,TeknikUzman,Tarih,TahminiSure,GerceklesenSure,IsAciklama,Notlar) " +
                "VALUES (@ProjeAd,@TeknikUzman,@Tarih,@TahminiSure,@GerceklesenSure,@IsAciklama,@Notlar)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@ProjeAd", mtxtProjeAd.Text);
            komut.Parameters.AddWithValue("@TeknikUzman", mtxtUzman.Text);
            komut.Parameters.AddWithValue("@Tarih", DateTimePickerTarih.Value);
            komut.Parameters.AddWithValue("@TahminiSure", mtxtTahminiSure.Text);
            komut.Parameters.AddWithValue("@GerceklesenSure", mtxtGerceklesenSure.Text);
            komut.Parameters.AddWithValue("@IsAciklama", rtxtAciklama.Text);
            komut.Parameters.AddWithValue("@Notlar", rtxtNotlar.Text);
            // komut.Parameters.AddWithValue("@KartNo", mtxtKartNo.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            Getir();
            mtxtTahminiSure.Text = GetPredictTime().ToString();
            MessageBox.Show("Teknik Kart Kaydı Yapıldı...");
            this.Close();
        }

        private void btn_GUNCELLE_Click(object sender, EventArgs e)
        {

            string sorgu = "UPDATE tbl_card SET ProjeAd=@ProjeAd,TeknikUzman=@TeknikUzman,Tarih=@Tarih,TahminiSure=@TahminiSure,GerceklesenSure=@GerceklesenSure,IsAciklama=@IsAciklama,Notlar=@Notlar WHERE KartNo = @KartNo";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@ProjeAd", mtxtProjeAd.Text);
            komut.Parameters.AddWithValue("@TeknikUzman", mtxtUzman.Text);
            komut.Parameters.AddWithValue("@Tarih", DateTimePickerTarih.Value);
            komut.Parameters.AddWithValue("@TahminiSure", mtxtTahminiSure.Text);
            komut.Parameters.AddWithValue("@GerceklesenSure", mtxtGerceklesenSure.Text);
            komut.Parameters.AddWithValue("@IsAciklama", rtxtAciklama.Text);
            komut.Parameters.AddWithValue("@Notlar", rtxtNotlar.Text);
            komut.Parameters.AddWithValue("@KartNo", mtxtKartNo.Text);
            MessageBox.Show("Teknik Kart Guncellendi..");

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            Getir();
            mtxtTahminiSure.Text = GetPredictTime().ToString();
            this.Close();
        }

        private void btn_SIL_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM tbl_card WHERE KartNo = @KartNo ";
            komut = new SqlCommand(sorgu, baglanti);

            komut.Parameters.AddWithValue("@KartNo", Convert.ToInt32(mtxtKartNo.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            mtxtTahminiSure.Text = GetPredictTime().ToString();
            MessageBox.Show(mtxtKartNo.Text + " No'lu Teknik Kart silindi");
            Getir();
            this.Close();
        }
    }
}


