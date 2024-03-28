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
using System.Data;
using System.Text.RegularExpressions;

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ritesh\Desktop\BCA\SYBCA\project\projectDB.mdf;Integrated Security=True;Connect Timeout=30");

        public static int registerid = 0;
        public Form2()
        {
            InitializeComponent();
        }

        private void register_Click(object sender, EventArgs e)
        {
            string name = "";
            string mail = "";
            if (username.Text == "" || password.Text == "")
            {
                MessageBox.Show("Check if any field is Empty", "Login Failded", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (verify("select count(*) from register where username = '" + username.Text + "' or email='" + username.Text + "'") == 0)
                {
                    label4.ForeColor = Color.Red;
                    label4.Text = Convert.ToString("Incorrect Username or Email");
                }
                else
                {
                    Regex check = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+)*@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");

                    bool vaild = check.IsMatch(username.Text);

                    if (vaild == true)
                    {
                        mail = username.Text;
                    }
                    else
                    {
                        name = username.Text;
                    }
                    label4.ForeColor = Color.White;
                    label4.Text = Convert.ToString("*");
                }

                if (verify("select count(*) from register where username = '" + username.Text + "' or email='" + username.Text + "'") > 0 && name != "")
                {

                    if (verify("select count(*) from register where username='" + username.Text + "' and password='" + password.Text + "'") > 0)
                    {
                        send("select r_id from register where username='" + username.Text + "'");

                        username.Text = "";
                        password.Text = "";
                        label4.ForeColor = Color.White;
                        label6.ForeColor = Color.White;
                        label4.Text = Convert.ToString("*");
                        label6.Text = Convert.ToString("*");                  
                        new Form3().Show();
                        this.Hide();
                    }
                    else
                    {
                        password.Text = "";
                        label6.ForeColor = Color.Red;
                        label6.Text = Convert.ToString("Incorrect Password");
                    }
                }

                if (verify("select count(*) from register where username = '" + username.Text + "' or email='" + username.Text + "'") > 0 && mail != "")
                {

                    if (verify("select count(*) from register where email='" + username.Text + "' and password='" + password.Text + "'") > 0)
                    {
                        send("select r_id from register where email='" + username.Text + "'");

                        username.Text = "";
                        password.Text = "";
                        label4.ForeColor = Color.White;
                        label6.ForeColor = Color.White;
                        label4.Text = Convert.ToString("*");
                        label6.Text = Convert.ToString("*");
                        name = "";
                        mail = "";
                        new Form3().Show();
                        this.Hide();
                    }
                    else
                    {
                        password.Text = "";
                        label6.ForeColor = Color.Red;
                        label6.Text = Convert.ToString("Incorrect Password");
                    }
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

        public void send(string query)
        {       
          
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    registerid = Convert.ToInt32(dr["r_id"]);
                }
                else
                {
                    MessageBox.Show("Register ID Not Found!");
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Oops! Something went wrong with the database. Please try again later.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Form4 form4 =new Form4();

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void login_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void spassward_CheckedChanged(object sender, EventArgs e)
        {
            if (spassward.Checked)
                password.PasswordChar = '\0';
            else
                password.PasswordChar = '*';
        }

        private void clear_Click(object sender, EventArgs e)
        {
            username.Text = "";
            password.Text = "";
            label4.Text = Convert.ToString("*");
            label6.Text = Convert.ToString("*");
        }

        private void username_TextChanged(object sender, EventArgs e)
        {
            label4.ForeColor = Color.White;
        }

        private void password_TextChanged(object sender, EventArgs e)
        {
            label6.ForeColor = Color.White;
        }
    }
}
