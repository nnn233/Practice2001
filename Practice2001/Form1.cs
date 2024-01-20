using Npgsql;

namespace Practice2001
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User? user = CheckLogin(LoginTextBox.Text.Trim());
            if (user != null && CheckPassword(PasswordTextBox.Text.Trim(), user))
            {
                Form2 form2 = new(user.Login);
                form2.Show();
                Hide();
            }
        }

        private User? CheckLogin(string login)
        {
            if (login == "")
                MessageBox.Show("Поле логин не может быть пустым");
            else if (GetUserByLogin(login) == null) MessageBox.Show("Пользователя с таким логином не существует");
            else return GetUserByLogin(login);
            return null;
        }

        private Boolean CheckPassword(string password, User user)
        {
            if (password == "")
                MessageBox.Show("Поле пароль не может быть пустым");
            else if (user.Password != password) MessageBox.Show("Неверный пароль");
            else return true;
            return false;
        }

        private User GetUserByLogin(string login)
        {
            var connection = new NpgsqlConnection("Host=localhost:5432;" +
            "Username=postgres;" +
            "Password=25481;" +
            "Database=UserDatabase");
            connection.Open();
            string commandText = $"SELECT * FROM users WHERE login='{login}'";
            NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection);
            var reader = cmd.ExecuteReader();
            User result = null;
            while (reader.Read())
            {
                result = new()
                {
                    Id = (int)reader.GetValue(0),
                    Login = (string)reader.GetValue(1),
                    Password = (string)reader.GetValue(2)
                };
            }
            reader.Close();
            return result;
        }
    }
}