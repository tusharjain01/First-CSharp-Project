using System.Data.SqlClient;

namespace application
{
    public partial class Form1 : Form
    {
        string connextionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename="+Application.StartupPath+"Database1.mdf;Integrated Security=True;Connect Timeout=30";
        public int passingKey12;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getItems();
            dataGridView1.SelectionMode= DataGridViewSelectionMode.FullRowSelect;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            
            SqlConnection con = new SqlConnection(connextionString);
            con.Open();
            string query = "Select productId,productName from productDetails where productName like '%"+text+"%'";
            SqlCommand cmd = new SqlCommand(query, con);
            var reader = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader["productId"], reader["productName"]);
            }
            con.Close();
        }
        void getItems()
        {
            SqlConnection con = new SqlConnection(connextionString);
            con.Open();
            string query = "Select productId,productName from productDetails";
            SqlCommand cmd = new SqlCommand(query, con);
            dataGridView1.Rows.Clear();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader["productId"], reader["productName"]);
            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            passingKey12 = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            //MessageBox.Show("Selected Row is " + passingKey12);
            Form2 f2 = new Form2();
            f2.recievedKey1 = passingKey12;
            this.Hide();
            f2.ShowDialog();
            this.Close();
        }
    }
}