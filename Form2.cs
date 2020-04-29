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
using System.Diagnostics;
using System.Xml;

namespace MarLifeGym
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=" + Application.StartupPath + "\\marlifegymuyekayit.accdb");
       
        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = "Marlife Gym"; 
            tabPage1.Text = "ANASAYFA";
            tabPage2.Text = "ÜYE EKLE";
            tabPage3.Text = "KONTÖR YÜKLE";
            tabPage4.Text = "ÜYE LİSTESİ";
            tabPage5.Text = "ÜYE SİL";
            kayitlariGoster();
            timer1.Interval = 1000;
            timer1.Enabled = true;

            listBox1.Items.Clear();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            

        }
        private void kayitlariGoster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter listele = new OleDbDataAdapter("select * from kayitet", baglantim);
                DataSet dshafiza = new DataSet();
                listele.Fill(dshafiza);
                dataGridView2.DataSource = dshafiza.Tables[0];
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message);
                baglantim.Close();
            }
        }
        string[] üyelikler = {"1 Aylık" , "2 Aylık", "3 Aylık" , "4 Aylık" , "5 Aylık", "6 Aylık" ,"1 Yıllık" , "VIP"};
        private void button1_Click(object sender, EventArgs e)
        {
            
            string cinsiyet;
            if (radioButton1.Checked)
            {
                cinsiyet = "Bay";
            }
            else
            {
                cinsiyet = "Bayan";
            }
            try
            {
                
                baglantim.Open();
                OleDbCommand ekle = new OleDbCommand("insert into kayitet (kart_no , ad_soyad , cinsiyet , telefon_no , kalan_kontor , kayit_tarihi , fiyat) values ('" + textBox1.Text + "','" + textBox3.Text + "','" + cinsiyet + "','" + textBox2.Text + "','" + textBox6.Text + "' , '"+label29.Text+"' , '"+textBox4.Text+"')", baglantim);
                ekle.ExecuteNonQuery();
                listBox1.Items.Add("Sayın " + textBox3.Text + " kayit isleminiz tamamlanmiştir"); 
                baglantim.Close();
                kayitlariGoster();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message);
                baglantim.Close();
            }
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear();textBox6.Clear();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(üyelikler);
        }


        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            double fiyat;
            fiyat = eklenecek_kontor * 15;
            textBox7.Text = Convert.ToString(fiyat);
               
        }
        double eski_kontor ;
        double eklenecek_kontor ;
        double toplam_kontor; 
        private void button2_Click(object sender, EventArgs e)
        {
            baglantim.Open();
            OleDbCommand araKomutu = new OleDbCommand("select * from kayitet", baglantim);
            OleDbDataReader kayitOku = araKomutu.ExecuteReader();

            while (kayitOku.Read())
            {
                if (this.textBox5.Text == Convert.ToString(kayitOku["kart_no"]))
                {
                    textBox11.Text = kayitOku.GetValue(4).ToString();
                    eski_kontor = Convert.ToDouble(textBox11.Text); 
                    break;
                }
            }
            baglantim.Close();

            eklenecek_kontor = Convert.ToDouble(textBox8.Text);

            toplam_kontor = eklenecek_kontor + eski_kontor; 

            double fiyat2;
            fiyat2 = Convert.ToDouble(textBox8.Text);
            try
            {
                baglantim.Open();
                OleDbDataAdapter kontorEkle = new OleDbDataAdapter("update kayitet set kalan_kontor='" +Convert.ToString(toplam_kontor)+ "' where kart_no='" + textBox5.Text + "'", baglantim);
                DataSet dshafisa = new DataSet();
                kontorEkle.Fill(dshafisa);
                listBox2.Items.Add(textBox5.Text + " kart no'suna sahip olan kişinin kontör guncelleme işlemi tamamlandı");
                baglantim.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                baglantim.Close();
            }
           
            try
            {
                baglantim.Open();
                OleDbDataAdapter aramaKomutu = new OleDbDataAdapter("select * from kayitet where kart_no='" + textBox5.Text + "'", baglantim);
                DataSet dshafiza = new DataSet();
                aramaKomutu.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglantim.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                baglantim.Close();
            }
            textBox5.Clear(); textBox8.Clear(); textBox11.Clear();
            kayitlariGoster();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter silKomutu = new OleDbDataAdapter("delete from kayitet where kart_no='"+textBox9.Text+"' ", baglantim);
                DataSet dshafiza = new DataSet();
                silKomutu.Fill(dshafiza);
                baglantim.Close();
                kayitlariGoster();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message);
                baglantim.Close();
            }

            textBox9.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            baglantim.Open();
            OleDbCommand araKomutu = new OleDbCommand("select * from kayitet", baglantim);
            OleDbDataReader kayitOku = araKomutu.ExecuteReader();
            
            while (kayitOku.Read())
            {
                if (this.textBox10.Text == Convert.ToString(kayitOku["kart_no"]))
                { 
                    form3.label1.Text = kayitOku.GetValue(1).ToString();
                    form3.textBox1.Text = kayitOku.GetValue(0).ToString();
                    form3.textBox2.Text = kayitOku.GetValue(2).ToString();
                    if (form3.textBox2.Text == "Bay")
                    {
                        form3.pictureBox1.Visible = true; form3.pictureBox2.Visible = false;
                    }
                    else if (form3.textBox2.Text == "Bayan")
                    {
                        form3.pictureBox2.Visible = true; form3.pictureBox1.Visible = false;
                    }
                    form3.textBox3.Text = kayitOku.GetValue(3).ToString();
                    form3.textBox4.Text = kayitOku.GetValue(4).ToString();
                    form3.textBox5.Text = kayitOku.GetValue(5).ToString();
                    form3.textBox6.Text = kayitOku.GetValue(6).ToString(); 
                    form3.ShowDialog();
                    break;
                }
                

            }
            baglantim.Close();
            textBox10.Clear();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string secim = comboBox1.SelectedItem.ToString();
            if (secim == "1 Aylık")
            {
                textBox4.Text = "180 ₺";
            }
            else if (secim == "2 Aylık")
            {
                textBox4.Text = "360 ₺";
            }
            else if (secim == "3 Aylık")
            {
                textBox4.Text = "450 ₺";
            }
            else if (secim == "4 Aylık")
            {
                textBox4.Text = "7200 ₺";
            }
            else if (secim == "5 Aylık")
            {
                textBox4.Text = "900 ₺";
            }
            else if (secim == "6 Aylık")
            {
                textBox4.Text = "850 ₺";
            }
            else if (secim == "1 Yıllık")
            {
                textBox4.Text = "1800 ₺";
            }
            else if (secim == "VIP")
            {
                textBox4.Text = "1200 ₺";
            }
        }
       
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime Suan = DateTime.Now;
            label29.Text = Convert.ToString(Suan);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
