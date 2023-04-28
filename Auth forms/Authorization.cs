using System;
using System.Windows.Forms;
using Npgsql;

namespace Kogtev_Lopushok
{
    public partial class Authorization : Form
    {
        public static string con_string = 
            "Host = localhost;" +
            "Database = demo30.01;" +
            "Username = postgres;" +
            "Password = 65adf4gs65d4fb4s6dfg4;";
        NpgsqlConnection con = new NpgsqlConnection(con_string);

        public Authorization()
        {
            InitializeComponent();
        }

        private void textBox_Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox_Password.Focus();
        }

        private void textBox_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                tryAuth();
        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            tryAuth();
        }





        private void tryAuth()
        {
            if (textBox_Login.TextLength > 1 && textBox_Password.TextLength > 1)
            {
                string login = textBox_Login.Text;
                string password = textBox_Password.Text;
                if (isAccountExists(login))
                {
                    con.Open();
                    var cmd = new NpgsqlCommand($"select * from \"Account\" where \"Login\" = '{login}'", con);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                        if (password == reader.GetString(2))
                        {
                            textBox_Login.Clear();
                            textBox_Password.Clear();
                            textBox_Login.Focus();

                            Products p = new Products(this, reader.GetInt32(3));
                            p.Show();
                            Hide();
                        }
                        else
                            MessageBox.Show("Неверный пароль!", "Ошибка авторизации");
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Аккаунт с таким логином не существует!", "Ошибка авторизации");
                }
            }
        }

        private bool isAccountExists(string login)
        {
            bool result = false;

            con.Open();
            var cmd = new NpgsqlCommand($"select exists(select \"ID\" from \"Account\" where \"Login\" = '{login}')", con);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
                result = reader.GetBoolean(0);
            con.Close();

            return result;
        }
    }
}
