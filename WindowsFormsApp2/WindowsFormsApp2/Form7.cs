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

namespace WindowsFormsApp2
{
    public partial class Form7 : Form
    {
        int count=0;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ritesh\Desktop\BCA\SYBCA\project\projectDB.mdf;Integrated Security=True;Connect Timeout=30");
        public Form7()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            string firstname = fname.Text;
            string lastname = lname.Text;
            firstname.ToLower();
            lastname.ToLower();
                if (fname.Text == "" || lname.Text == "" || email.Text == "")
                {
                    MessageBox.Show("Check if any field is Empty", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (verify("SELECT COUNT(*) FROM booking_information WHERE first_name='" + firstname + "' AND last_name='" + lastname + "' AND email='" + email.Text + "'") > 0)
                    {
                            count += 1;
                            string query = "select booking_id as Booking_ID, first_name as Name, room_type as Room_Type, num_of_room as     Num_Of_Room, guest_per_room as Guest_per_Room, arrival_date as Arrival_Date, DEPARTURE_date as Departure_Date, Amount    from booking_information where first_name='" + firstname + "' AND last_name='" + lastname + "' AND email='" +     email.Text + "'";
                        try
                        {
                            con.Open();
                            SqlCommand com = new SqlCommand(query, con);
                            SqlDataAdapter da = new SqlDataAdapter(com);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                            con.Close();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("Oops! Something went wrong with the database. Please try again later.", "Database Error",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                   
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        MessageBox.Show("No match found for the provided details. Please verify and try again.", "Try again",   MessageBoxButtons.OK,MessageBoxIcon.Error);                   
                    }
                }
        }

        public int verify(string query)
        {
            int count = 0;
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                count = Convert.ToInt32(com.ExecuteScalar());
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Oops! Something went wrong with the database. Please try again later.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return count;
        }

        private void Form7_Load(object sender, EventArgs e)
        {
           

        }

        private void backpicture_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void back_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void fname_TextChanged(object sender, EventArgs e)
        {
       
        }

        private void lname_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void label8_Click(object sender, EventArgs e)
        {
            new Form5().Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            new Form6().Show();
            this.Hide();
        }
    }
}
