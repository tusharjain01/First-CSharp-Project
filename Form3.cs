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

namespace application
{
    public partial class Form3 : Form
    {
        string connextionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\tusha\\OneDrive\\Desktop\\Experiment\\application\\application\\bin\\Debug\\net6.0-windows\\Database1.mdf;Integrated Security=True";
        public int recievedKey3;
        public int quantityLeft;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            getDetails();
            getCustID();
            //getCustDetails();
        }

        void getDetails()
        {
            SqlConnection con = new SqlConnection(connextionString);
            string query = "SELECT productName,productPrice from productDetails where productId = " + recievedKey3;
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                textBox1.Text = reader["productName"].ToString();
                textBox2.Text = reader["productPrice"].ToString();
            }
            con.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            if(int.TryParse(textBox3.Text, out i))
            {
                errorProvider1.SetError(textBox3, "");
                SqlConnection con = new SqlConnection(connextionString);
                string query = "SELECT * from productDetails where productId = " + recievedKey3;
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                var reader = cmd.ExecuteReader();
                int quantity = 0;
                if (reader.Read())
                {
                    quantity = Convert.ToInt32(reader["productQuantityLeft"].ToString());
                }
                int quantityWant = Convert.ToInt32(textBox3.Text.ToString());
                int unitPrice = Convert.ToInt32(textBox2.Text.ToString());
                if (quantityWant <= quantity)
                {
                    textBox4.Text = (quantityWant * unitPrice).ToString();
                    quantityLeft = quantity - quantityWant;
                    button2.Enabled = true;
                    
                    dataGridView1.Rows.Add(reader["productName"], reader["productPrice"], textBox3.Text, textBox4.Text);
                }
                else
                {
                    errorProvider1.SetError(textBox3, "We have only " + quantity + " items");
                }
                con.Close();
            }
            else
            {
                errorProvider1.SetError(textBox3, "Enter integer value");
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 fr = new Form2();
            fr.recievedKey1 = recievedKey3;
            this.Hide();
            fr.ShowDialog();
            this.Close();
        }

        void getCustID()
        {
            comboBox1.Items.Clear();
            SqlConnection con = new SqlConnection(connextionString);
            string query = "SELECT custID from customerDetails";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["custID"]);
            }
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = comboBox1.SelectedItem.ToString();
            SqlConnection con = new SqlConnection(connextionString);
            string query = "SELECT * from customerDetails where custID = '" + str + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                textBox5.Text = reader["custFirstName"].ToString() + " " + reader["custSecondName"].ToString();
                textBox6.Text = reader["custPhNumber"].ToString();
                textBox7.Text = reader["custAddress"].ToString();
                textBox8.Text = reader["custCCNumber"].ToString();
            }
            con.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string pid = recievedKey3.ToString();
            string cid = comboBox1.GetItemText(comboBox1.SelectedItem);
            //MessageBox.Show(pid + " " + cid);
            string price = textBox4.Text;
            DateTime time = DateTime.Now;              // Use current time
            string format = "yyyy-MM-dd HH:mm:ss";    // modify the format depending upon input required in the column in database 
            SqlConnection con = new SqlConnection(connextionString);
            string query = "INSERT INTO relation(productID,cust,pricePaid,Time) values ('" + pid + "','" + cid + "','" + price + "' ,'"+ time.ToString(format) + "')";
            string query2 = "UPDATE productDetails SET productQuantityLeft = '" + quantityLeft + "' where productId = '" + pid + "'";
            SqlCommand cmd = new SqlCommand(query + query2, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Thanks for purchasing !!");

        }

        
    }
}
