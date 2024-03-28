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
using System.Text.RegularExpressions;

namespace WindowsFormsApp2
{
    public partial class Form4 : Form
    {
        bool available = false;
        String mail = "";
        String contact = "";
        String firstname="";
        String lastname="";
        DateTime? departuredate = null;
        int registerid;
        TimeSpan difference;
        int nights;

        SqlConnection con=new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ritesh\Desktop\BCA\SYBCA\project\projectDB.mdf;Integrated Security=True;Connect Timeout=30");

        public Form4()
        {
            InitializeComponent();
            registerid = Form2.registerid;

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            label13.Text = Convert.ToString(+room.Value + " Room, " + room.Value * guest.Value + " Guest");
            available = false;
            label12.Text = Convert.ToString("*Check Availability");
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Hide();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Hide();

        }

        public void button2_Click(object sender, EventArgs e)
        {
            int r = Convert.ToInt32(room.Value);
            int g = Convert.ToInt32(guest.Value);
            int price=0;
            if (fname.Text == "" || lname.Text == "" || email.Text == "" || roomtype.SelectedIndex == -1 || phone.Text == "")
            {
                MessageBox.Show("Check if any field is Empty", "Registration Failded", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (validation(fname.Text, @"^[a-zA-Z_]+$") == true)
                {
                    label4.Text = Convert.ToString("*");
                    firstname = fname.Text;
                }
                else
                {
                    label4.ForeColor = Color.Red;
                    label4.Text = Convert.ToString("Names can only contain letters (a-z, A-Z)");
                    label5.Text = "";
                   firstname = "";
                }

                if (validation(lname.Text, @"^[a-zA-Z_]+$") == true)
                {
                    label5.Text = "";
                    lastname = lname.Text;
                }
                else
                {
                    label4.ForeColor = Color.Red;
                    label4.Text = Convert.ToString("Names can only contain letters (a-z, A-Z)");
                    label5.Text = "";
                    lastname = "";
                }

                    if (validation(email.Text, @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+)*@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$") == true)
                    {
                        mail = email.Text;
                        label7.ForeColor = Color.White;
                        label7.Text = Convert.ToString("*");
                    }
                    else
                    {
                        label7.ForeColor = Color.Red;
                        label7.Text = Convert.ToString("Invaild Email");
                        mail = "";
                    }

                      if (validation(phone.Text, @"^\d{10}$") == true)
                      {
                          contact = phone.Text;
                        label9.ForeColor = Color.White;
                        label9.Text = Convert.ToString("*");
                      }
                      else
                      {
                          label9.ForeColor = Color.Red;
                          label9.Text = Convert.ToString("Invaild phone number");
                          contact = "";
                      }

                if(dateTimePicker2.Value.Date <=DateTime.Now.Date)
                {
                    departuredate = null;
                    label17.ForeColor = Color.Red;
                    label17.Text= Convert.ToString("Departure date cannot be earlier than today\nor today's date");
                }
                else if(dateTimePicker1.Value.Date > dateTimePicker2.Value.Date || dateTimePicker2.Value.Date < dateTimePicker1.Value.Date)
                {
                    label17.ForeColor = Color.Red;
                    label18.ForeColor = Color.Red;
                    label18.Text = "Select date rang is invalid";
                    label17.Text = "Select date rang is invalid";
                    departuredate = null;
                }
                else
                {
                    departuredate = dateTimePicker2.Value;
                    DateTime start = dateTimePicker1.Value.Date;
                    DateTime end = dateTimePicker2.Value.Date;

                    label17.ForeColor = Color.White;
                    label18.ForeColor = Color.White;
                    label18.Text = "*";
                    label17.Text = "*";

                    difference = end - start;
                    nights = difference.Days;

                    if (nights == 0)
                        nights = 1;
                }

                if (available == false)
                   label12.Text = Convert.ToString("*Check Availability");

               

                if (firstname != "" && lastname!="" && mail !="" && contact!="" && available==true && departuredate!=null)
                {

                    if(roomtype.Items[roomtype.SelectedIndex].ToString() == "Non-Ac Single Room")
                    {
                        price = nights * 900;
                        price = r * price;
                    }
                    else if (roomtype.Items[roomtype.SelectedIndex].ToString() == "AC Single Room")
                    {
                        price = nights * 1300;
                        price = r * price;
                    }
                    else if (roomtype.Items[roomtype.SelectedIndex].ToString() == "Non-Ac Double Room")
                    {
                        price = nights * 1900;
                        price = r * price;
                    }
                    else if (roomtype.Items[roomtype.SelectedIndex].ToString() == "AC Double Room")
                    {
                        price = nights * 2300;
                        price = r * price;
                    }
                    else
                    {
                        int totelguest;
                        totelguest = g * r;   
                        price = totelguest * 670;
                        price = price * nights;
                        price = r * price;
                    }
                    firstname.ToLower();
                    lastname.ToLower();

                    DialogResult result = DialogResult.None;

                     result=MessageBox.Show("Full Name : " + firstname + " " + lastname + "\n\nRoom type : " + roomtype.SelectedItem.ToString() + "\n" + room.Value + " Room, " + room.Value * guest.Value + " Guest\n\nArrival date : " + dateTimePicker1.Value.Date + "\nDeparture date : " + dateTimePicker2.Value.Date + "\n\nAmount : \u20B9" + price + "\n\nAre these room booking details and amounts suitable for confirming your reservation?", "Registration Details", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        insert("INSERT INTO booking_information (register_id, first_name, last_name, email, phone_no, room_type, num_of_room, guest_per_room, arrival_date, DEPARTURE_date, Amount) VALUES (" + registerid + ", '" + firstname + "', '" + lastname + "', '" + mail + "', '" + contact + "', '" + roomtype.SelectedItem.ToString() + "', " + room.Value + ", " + guest.Value + ", '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'," + price + ")");


                      
                       // int guest_id=0;
                       // con.Open();
                       // SqlCommand com = new SqlCommand("select guest_id from guest_info where first_name='" + //firstname + "' and last_name='" + lastname + "' and email='" + mail + "'", con) ;
                       //
                       // SqlDataReader dr = com.ExecuteReader();
                       // if (dr.Read())
                       // {
                       //     guest_id = Convert.ToInt32(dr["guest_id"]);
                       // }
                       // else
                       // {
                       //     MessageBox.Show("guest ID Not Found!");
                       // }
                       // con.Close();
                       //
                       // insert("INSERT INTO booking_info (g_id, room_type, num_of_room, guest_per_room, arrival_date, departure_date,Amount) VALUES (" + guest_id + ", '" + roomtype.SelectedItem.ToString() + "', " + room.Value + ", " + (room.Value * guest.Value) + ", '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "',"+ price + ")");

                        label12.Text = Convert.ToString("*Check Availability");
                        phone.Text = "";
                        roomtype.SelectedIndex = -1;
                        dateTimePicker1.Value = DateTime.Now;
                        dateTimePicker2.Value = DateTime.Now;
                    }

                }

            }
       
        }

        public void insert(string query)
        {
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Oops! Something went wrong with the database.  Please try again later.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool validation(string contant,string check)
        {
            Regex r1 = new Regex(check);
            if(r1.IsMatch(contant))
            {
                return true;
            }
            else
            {
                return false;
            }
        
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            available = false;
            label12.Text = Convert.ToString("*Check Availability");
        }

        private void label16_Click_1(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            label13.Text = Convert.ToString(+room.Value + " Room, " + room.Value * guest.Value + " Guest");
            if (roomtype.SelectedIndex == -1)
            {
                label12.Text = Convert.ToString(" Select a room type");
                available = false;
            }
            else if (roomtype.SelectedIndex >= 0 && roomtype.Items[roomtype.SelectedIndex].ToString() == "Non-Ac Single Room" || roomtype.Items[roomtype.SelectedIndex].ToString() == "AC Single Room")
            {
                if (guest.Value == 1)
                {
                    label12.Text = Convert.ToString("Available");
                    available = true;

                }
                else
                {
                    label12.Text = Convert.ToString(" Not Available");
                    available = false;
                }
            }
            else if (roomtype.SelectedIndex >= 0 && roomtype.Items[roomtype.SelectedIndex].ToString() == "AC Double Room" || roomtype.Items[roomtype.SelectedIndex].ToString() == "Non-Ac Double Room")
            {
                if (guest.Value == 2)
                {
                    label12.Text = Convert.ToString("Available");
                    available = true;
                }
                else
                {
                    label12.Text = Convert.ToString(" Not Available");
                    available = false;
                }
            }
            else if (roomtype.SelectedIndex >= 0 && roomtype.Items[roomtype.SelectedIndex].ToString() == "Family Room")
            {
                if (guest.Value > 2)
                {
                    label12.Text = Convert.ToString("Available");
                    available = true;
                }
                else
                {
                    label12.Text = Convert.ToString(" Not Available");
                    available = false;
                }
            }       
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.MinDate = DateTime.Now;
            label18.ForeColor = Color.White;

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = DateTime.Now;

            label18.ForeColor = Color.White;
        }

        private void guest_ValueChanged(object sender, EventArgs e)
        {
            label13.Text = Convert.ToString(+room.Value + " Room, " + room.Value * guest.Value + " Guest");
            label12.Text = Convert.ToString("*Check Availability");
            available = false;
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {
            new Form7().Show();
            this.Hide();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            new Form7().Show();
            this.Hide();
        }
    }
}
