using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form5 : Form
    {
        bool available = false, display;
        int price;
        DateTime? departuredate = null;
        TimeSpan difference;
        int nights;
        int numOfRoom;
        int guestPerRoom;
        string firstname = "";
        string lastname = "";
        string roomType = "";
        int booking_id;

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ritesh\Desktop\BCA\SYBCA\project\projectDB.mdf;Integrated Security=True;Connect Timeout=30");

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void backpicture_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Hide();
        }

        private void back_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            available = false;
            label12.Text = Convert.ToString("*Check Availability");
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = DateTime.Now;
            label8.ForeColor = Color.White;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.MinDate = DateTime.Now;
            label9.ForeColor = Color.White;
        }

        public void button3_Click(object sender, EventArgs e)
        {
            int r = Convert.ToInt32(room.Value);
            int g = Convert.ToInt32(guest.Value);

            if (display == true)
            {
                label6.ForeColor = Color.White;
                label6.Text = Convert.ToString("*");

                if (dateTimePicker2.Value.Date <= DateTime.Now.Date)
                {
                    departuredate = null;
                    label8.ForeColor = Color.Red;
                    label8.Text = Convert.ToString("Departure date cannot be earlier than today\nor today's date");
                }
                else if (dateTimePicker1.Value.Date > dateTimePicker2.Value.Date || dateTimePicker2.Value.Date < dateTimePicker1.Value.Date)
                {
                    label8.ForeColor = Color.Red;
                    label9.ForeColor = Color.Red;
                    label9.Text = "Select date rang is invalid";
                    label8.Text = "Select date rang is invalid";
                    departuredate = null;
                }
                else
                {
                    departuredate = dateTimePicker2.Value;
                    DateTime start = dateTimePicker1.Value.Date;
                    DateTime end = dateTimePicker2.Value.Date;

                    label8.ForeColor = Color.White;
                    label9.ForeColor = Color.White;
                    label9.Text = "*";
                    label8.Text = "*";

                    difference = end - start;
                    nights = difference.Days;

                    if (nights == 0)
                        nights = 1;
                }

                if (available == false)
                    label12.Text = Convert.ToString("*Check Availability");

                if (firstname != "" && lastname != "" && available == true && departuredate != null)
                {
                    if (roomtype.Items[roomtype.SelectedIndex].ToString() == "Non-Ac Single Room")
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

                    result = MessageBox.Show("Full Name : " + firstname + " " + lastname + "\n\nRoom type : " + roomtype.SelectedItem.ToString() + "\n" + room.Value + " Room, " + room.Value * guest.Value + " Guest\n\nArrival date : " + dateTimePicker1.Value.Date + "\nDeparture date : " + dateTimePicker2.Value.Date + "\n\nAmount : \u20B9" + price + "\n\nAre these details and amounts suitable for confirming your reservation?", "Details", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        insert("UPDATE booking_information SET room_type='" + roomtype.SelectedItem.ToString() + "', num_of_room= " + room.Value + ", guest_per_room=" + guest.Value + ", arrival_date='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', DEPARTURE_date='" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "', Amount=" + price + " WHERE booking_id=" + booking_id + "");

                        label12.Text = Convert.ToString("*Check Availability");
                        roomtype.SelectedIndex = -1;
                        dateTimePicker1.Value = DateTime.Now;
                        dateTimePicker2.Value = DateTime.Now;
                    }
                }
            }
            else
            {
                label6.ForeColor = Color.Red;
                label6.Text = Convert.ToString("*Please enter valid\nName and Booking ID!");
                button3.BackColor = Color.White;
            }
        }



        public bool validation(string contant, string check)
        {
            Regex r1 = new Regex(check);
            if (r1.IsMatch(contant))
            {
                return true;
            }
            else
            {
                return false;
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
                MessageBox.Show("Oops! Something went wrong with the database. Please try again later.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void username_TextChanged(object sender, EventArgs e)
        {
            label6.ForeColor = Color.White;
            label6.Text = Convert.ToString("*");
            display = false;
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void guest_ValueChanged(object sender, EventArgs e)
        {
            available = false;
            label12.Text = Convert.ToString("*Check Availability");
        }

        private void roomtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            label12.Text = Convert.ToString("*Check Availability");
            available = false;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {
            new Form7().Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            new Form7().Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void bookingid_TextChanged(object sender, EventArgs e)
        {
            display = false;
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            roomtype.SelectedIndex = -1;
            room.Value = 1;
            guest.Value = 1;
            label12.Text = Convert.ToString("Check Availability");
            dateTimePicker1.MinDate = DateTime.Now;
            label9.ForeColor = Color.White;
            dateTimePicker2.MinDate = DateTime.Now;
            label8.ForeColor = Color.White;
            roomType = "";
            display = false;

            if (fname.Text == "" || lname.Text == "" || bookingid.Text == "")
            {
                MessageBox.Show("Check if any field is Empty", "Try again", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if ((validation(fname.Text, @"^[a-zA-Z_]+$") == true) && (validation(lname.Text, @"^[a-zA-Z_]+$") == true))
                {
                    label6.ForeColor = Color.White;
                    label6.Text = Convert.ToString("*");
                    firstname = fname.Text;
                   // label5.Text = "";
                    lastname = lname.Text;

                    if (int.TryParse(bookingid.Text, out booking_id))
                    {
                        booking_id = Convert.ToInt32(bookingid.Text);
                        if (verify("select count(*) from booking_information where booking_id=" + booking_id + " and first_name='" + firstname + "' and     last_name='" + lastname + "'") > 0)
                        {
                            displaydata();
                            display = true;
                            button3.BackColor = Color.Green;

                        }
                        else
                        {
                            MessageBox.Show("No match found for the provided details. Please verify and try again.", "Try again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            display = false;
                            button3.BackColor = Color.White;
                        }
                    }
                    else
                    {
                        button3.BackColor = Color.White;
                        MessageBox.Show("Enter ineteger value for Booking ID or go to view and check your Booking ID", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       
                    }

                }
                else
                {
                    label6.ForeColor = Color.Red;
                    label6.Text = Convert.ToString("Names can only contain\nletters (a-z, A-Z)");
                    //label5.Text = "";
                    firstname = "";
                    lastname = "";
                    button3.BackColor = Color.White;
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

        private void lname_TextChanged(object sender, EventArgs e)
        {
            label6.ForeColor = Color.White;
            label6.Text = Convert.ToString("*");
            display = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public void displaydata()
        {
            con.Open();
            SqlCommand com = new SqlCommand("select room_type, num_of_room, guest_per_room from booking_information where booking_id=" + booking_id + " and first_name='" + firstname + "' and     last_name='" + lastname + "'", con);
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                roomType = reader.GetString(0);
                numOfRoom = reader.GetInt32(1);
                guestPerRoom = reader.GetInt32(2);
            }
            if (roomType == "AC Double Room")
            {
                roomtype.SelectedIndex = 0;

            }
            else if (roomType == "AC Single Room")
            {
                roomtype.SelectedIndex = 1;
            }
            else if (roomType == "Non-Ac Double Room")
            {
                roomtype.SelectedIndex = 2;
            }
            else if (roomType == "Non-Ac Single Room")
            {
                roomtype.SelectedIndex = 3;
            }
            else if (roomType == "Family Room")
            {
                roomtype.SelectedIndex = 4;
            }
            room.Value = numOfRoom;
            guest.Value = guestPerRoom;
            con.Close();
        }
    }
}


