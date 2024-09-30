using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace odev2._8KitapKayit
{
    public partial class Form1 : Form
    {
        public string ConStr { get; private set; }

        public Form1()
        {
            InitializeComponent();
            ConStr = @"Data Source=LAPTOP-H0JRKJJR\SQLEXPRESS; Initial Catalog=Kitapcım_DB; Integrated Security=True";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int mevcutYil = DateTime.Now.Year;
            for (int yil = mevcutYil; yil >= mevcutYil - 200; yil--)
            {
                cb_yayinyili.Items.Add(yil);
            }
        }

        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(tb_sayfasayisi.Text, out int sayfaSayisi))
            {
                MessageBox.Show("Sayfa sayısını geçerli bir sayı olarak girin.");
                return;
            }

            if (!decimal.TryParse(tb_satisfiyati.Text, out decimal satisFiyati))
            {
                MessageBox.Show("Satış fiyatını geçerli bir sayı olarak girin.");
                return;
            }

            XElement kitap = new XElement("Kitap",
                             new XElement("Ad", tb_kitapadi.Text),
                             new XElement("Yazar", tb_yazar.Text),
                             new XElement("Yayınevi", tb_yayinevi.Text),
                             new XElement("YayınYili", cb_yayinyili.Text),
                             new XElement("Tur", cb_tur.Text),
                             new XElement("SayfaSayisi", sayfaSayisi),
                             new XElement("Dil", cb_dil.Text),
                             new XElement("SatisFiyati", satisFiyati.ToString("F2"))); 

            XElement kitaplar;

            if (!File.Exists("kitaplar.xml"))
            {
                kitaplar = new XElement("Kitaplar", kitap);
                kitaplar.Save("kitaplar.xml");
            }
            else
            {
                kitaplar = XElement.Load("kitaplar.xml");
                kitaplar.Add(kitap);
                kitaplar.Save("kitaplar.xml");
            }

            foreach (var k in kitaplar.Elements("Kitap"))
            {
                Console.WriteLine($"Ad: {k.Element("Ad").Value}");
                Console.WriteLine($"Yazar: {k.Element("Yazar").Value}");
                Console.WriteLine($"Yayınevi: {k.Element("Yayınevi").Value}");
                Console.WriteLine($"Yayın Yılı: {k.Element("YayınYili").Value}");
                Console.WriteLine($"Tür: {k.Element("Tur").Value}");
                Console.WriteLine($"Sayfa Sayısı: {k.Element("SayfaSayisi").Value}");
                Console.WriteLine($"Dil: {k.Element("Dil").Value}");
                Console.WriteLine($"Satış Fiyatı: {k.Element("SatisFiyati").Value}");
                Console.WriteLine();
            }

            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                string query = "INSERT INTO Kitaplar (Ad, Yazar, Yayınevi, YayinYili, Tür, SayfaSayisi, Dil, SatisFiyati) " +
                               "VALUES (@Ad, @Yazar, @Yayinevi, @YayinYili, @Tur, @SayfaSayisi, @Dil, @SatisFiyati)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Ad", tb_kitapadi.Text);
                    command.Parameters.AddWithValue("@Yazar", tb_yazar.Text);
                    command.Parameters.AddWithValue("@Yayinevi", tb_yayinevi.Text);
                    command.Parameters.AddWithValue("@YayinYili", int.Parse(cb_yayinyili.Text));
                    command.Parameters.AddWithValue("@Tur", cb_tur.Text);
                    command.Parameters.AddWithValue("@SayfaSayisi", sayfaSayisi);
                    command.Parameters.AddWithValue("@Dil", cb_dil.Text);
                    command.Parameters.AddWithValue("@SatisFiyati", satisFiyati);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Kitap veritabanına ve XML'e kaydedildi.");
        }
    }
}



