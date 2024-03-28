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
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ritesh\Desktop\BCA\SYBCA\project\projectDB.mdf;Integrated Security=True;Connect Timeout=30");

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label9.ForeColor = Color.White;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label8.ForeColor = Color.White;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            username.Text = "";
            password.Text = "";
            cpassword.Text = "";
            email.Text = "";
            label7.Text = "*";
            label8.Text = "*";
            label9.Text = "*";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String name="" ;
            String mail="";
            String pass="";
            if (username.Text == "" || email.Text == "" || password.Text == "" || cpassword.Text == "")
            {
                MessageBox.Show("Check if any field is Empty", "Registration Failded", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {

                if (username.Text != "")
                {
                    //checking if user already registered using this USERNAME          

                    if (verify("select count(*) from register where username = '" + username.Text + "'") > 0)
                    {
                        label9.ForeColor = Color.Red;
                        label9.Text = Convert.ToString("Username is already taken");
                        name = "";
                    }
                    else
                    {
                        name = username.Text;
                        label9.ForeColor = Color.White;
                        label9.Text = Convert.ToString("*");
                    }
                }

                if (password.Text == cpassword.Text)
                {
                    pass = password.Text;
                    label8.ForeColor = Color.White;
                    label8.Text = Convert.ToString("*");
                }   

                if (email.Text != "")
                {
                    Regex check = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+)*@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");

                    bool vaild = check.IsMatch(email.Text);

                    if (vaild==true)
                    {
                         mail = email.Text;
                        label7.ForeColor = Color.White;
                        label7.Text = Convert.ToString("*");

                        //checking if user already registered using this email

                        if (verify("select count(*) from register where email = '" + email.Text + "'") >0)
                        {
                            label7.ForeColor = Color.Red;
                            label7.Text = Convert.ToString("Email is already taken");
                            mail ="";
                        }
                    }
                    else
                    {
                        label7.ForeColor = Color.Red;
                        label7.Text = Convert.ToString("Invaild Email");
                        mail = "";
                    }
                }

                if (password.Text != cpassword.Text)
                {
                    label8.ForeColor = Color.Red;
                    label8.Text = Convert.ToString("Passward is not Matching");
                    pass = "";
                }
            

                if (name != "" && mail != "" && pass != "")
                {

                    con.Open();

                    string query = "insert into register (username, password,email) values ('" + username.Text + "','" + password.Text + "','" + email.Text + "')";

                    try
                    {
                        SqlCommand com = new SqlCommand(query, con);
                        com.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Oops! Something went wrong with the database. Please try again later.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    MessageBox.Show("Your Account has been Successfully Created", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    con.Close();

                    username.Text = "";
                    password.Text = "";
                    cpassword.Text = "";
                    email.Text = "";
                    label7.ForeColor = Color.White;
                    label8.ForeColor = Color.White;
                    label9.ForeColor = Color.White;
                    label7.Text = "*";
                    label8.Text = "*";
                    label9.Text = "*";
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
            catch(SqlException ex)
            {
                MessageBox.Show("Oops! Something went wrong with the database. Please try again later.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return count;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }

        private void spassward_CheckedChanged(object sender, EventArgs e)
        {
            if (spassward.Checked)
                password.PasswordChar = '\0';
            else
                password.PasswordChar = '*';

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            label7.ForeColor = Color.White;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                cpassword.PasswordChar = '\0';
            else
                cpassword.PasswordChar = '*';
        }
    }
}
