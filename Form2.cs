using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace application
{

    public partial class Form2 : Form
    {
        string connextionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename="+Application.StartupPath+"Database1.mdf;Integrated Security=True;Connect Timeout=30;Integrated Security=True";
        public int recievedKey1;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            if (!File.Exists(@"Images\"+recievedKey1+".jpg"))
            {
                MessageBox.Show("Image is not avaiable");
            }
            else
            {
                Image image = Image.FromFile(@"Images\" + recievedKey1 + ".jpg");
                this.pictureBox1.Image = image;
            }

            SqlConnection con = new SqlConnection(connextionString);
            con.Open();
            string cmd = "SELECT productName,productDesc from productDetails where productId = " + recievedKey1;
            SqlCommand sqlCommand= new SqlCommand(cmd, con);
            var reader = sqlCommand.ExecuteReader();
            if(reader.Read())
            {
                label1.Text = reader["productName"].ToString();
                textBox1.Text = reader["productDesc"].ToString();
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fr = new Form1();
            this.Hide();
            fr.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 fr = new Form3();
            fr.recievedKey3 = recievedKey1;
            this.Hide();
            fr.ShowDialog();
            this.Close();
        }
    }
}
