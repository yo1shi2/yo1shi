public static class dbconnection
{
    public static string ConnectionString = "Host=localhost;Port=5432;Database=2;Username=postgres;Password=1234321";

    public static NpgsqlConnection GetConnection()
    {
        NpgsqlConnection conn = null;

        try
        {
            conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"Ошибка подключения к PostgreSQL: {ex.Message}");
            return null;
        }
    }

    public static bool RegisterUser(string username, string password)
    {
        using (var conn = GetConnection())
        {
            if (conn == null) return false;

            // Проверка на существование пользователя
            var checkUserCmd = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE username = @username", conn);
            checkUserCmd.Parameters.AddWithValue("username", username);
            var userCount = (long)checkUserCmd.ExecuteScalar();

            if (userCount > 0)
            {
                MessageBox.Show("Пользователь с таким именем уже существует!");
                return false;
            }

            // Регистрация нового пользователя
            var insertUserCmd = new NpgsqlCommand("INSERT INTO users (username, password) VALUES (@username, @password)", conn);
            insertUserCmd.Parameters.AddWithValue("username", username);
            insertUserCmd.Parameters.AddWithValue("password", password);
            insertUserCmd.ExecuteNonQuery();

            MessageBox.Show("Регистрация успешна!");
            return true;
        }
    }

    public static bool LoginUser(string username, string password)
    {
        using (var conn = GetConnection())
        {
            if (conn == null) return false;

            var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE username = @username AND password = @password", conn);
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("password", password);
            var userCount = (long)cmd.ExecuteScalar();

            if (userCount > 0)
            {
                MessageBox.Show("Авторизация успешна!");
                return true;
            }
            else
            {
                MessageBox.Show("Неправильное имя пользователя или пароль.");
                return false;
            }
        }
    }
}

private void btnRegister_Click(object sender, EventArgs e)
{
    string username = txtUsername.Text;
    string password = txtPassword.Text;

    dbconnection.RegisterUser(username, password);
}

private void btnLogin_Click(object sender, EventArgs e)
{
    string username = txtUsername.Text;
    string password = txtPassword.Text;

    dbconnection.LoginUser(username, password);
}



CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(50) NOT NULL
);

