using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarLifeGym
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string username = "mesut";
        double password = 1234;
      

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == username && Convert.ToDouble(textBox2.Text) == password)
            {
                Form2 form2 = new Form2();
                form2.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Yanlış Parola veya Kullanıcı adı", "Uyari");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
            this.Text = "Marlife Gym"; 
        }
    }
}
