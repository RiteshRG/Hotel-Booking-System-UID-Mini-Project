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
    public partial class Form6 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ritesh\Desktop\BCA\SYBCA\project\projectDB.mdf;Integrated Security=True;Connect Timeout=30");
        int booking_id;
        public Form6()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void backpicture_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Hide();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void back_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        
            string firstname = fname.Text;
            string lastname = lname.Text;       

             if (fname.Text == "" || lname.Text == "" || reason.SelectedIndex == -1)
             {
                 MessageBox.Show("Check if any field is Empty", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
             else
             {

                 if (int.TryParse(bookingid.Text, out int booking_id))
                 {
                    booking_id = Convert.ToInt32(bookingid.Text);
                    if (verify("select count(*) from booking_information where booking_id=" + booking_id + " and first_name='" + firstname + "' and     last_name='" + lastname + "'") > 0)
                    {
                        DialogResult result = DialogResult.None;
                        result = MessageBox.Show("Are you sure you want to cancel your room booking?", "Cancel Room Booking Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                con.Open();
                                SqlCommand com = new SqlCommand("delete from booking_information where booking_id=" + booking_id + "", con);
                                com.ExecuteNonQuery();
                                con.Close();
                            }
                            catch (SqlException ex)
                            {
                                MessageBox.Show("Oops! Something went wrong with the database. Please try again later.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                          
                            result = MessageBox.Show("Your room booking has been successfully cancelled", "Cancellation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fname.Text = "";
                            lname.Text = "";
                            bookingid.Text = "";
                            reason.SelectedIndex = -1;
                            textBox2.Text = "";
                            textBox2.Text = "Any other reason";
                        }
                    }
                    else
                    {
                        MessageBox.Show("No match found for the provided details. Please verify and try again.", "Try again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                 else
                 {
                     MessageBox.Show("Enter ineteger value for Booking ID or go to view and check your Booking ID", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            new Form7().Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new Form7().Show();
            this.Hide();
        }
    }
}
