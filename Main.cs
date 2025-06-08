


Вывод информации в дата гриф из бд

using Npgsql;
using System.Data;

namespace WinFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = null;
            DataTable dataTable = new DataTable();
            try
            {
                conn = dbconnection.GetConnection();
                if (conn != null)
                {
                    string sqlQuery = "SELECT * FROM Products;";

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sqlQuery, conn))
                    {
                        adapter.Fill(dataTable);
                    }
                    dgvData.DataSource = dataTable;
                }
            }
            catch (NpgsqlException ex)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}




класс дб конектион



    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace WinFormsApp5
{
    public static class dbconnection
    {
        private static string ConnectionString = "Host=localhost;Port=5432;Database=2;Username=postgres;Password=1234321";

        public static NpgsqlConnection GetConnection()
        {
            NpgsqlConnection conn = null;

            try
            {
                conn = new NpgsqlConnection(ConnectionString);
                conn.Open();
                Console.WriteLine();
                return conn;
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Ошибка подключения к PostgreSQL: {ex.Message}");
                return null;
            }
        }

    }
}



CREATE TABLE users (
    user_id SERIAL PRIMARY KEY,            -- Уникальный ID пользователя, автоинкремент, первичный ключ
    username VARCHAR(50) UNIQUE NOT NULL,  -- Имя пользователя (до 50 символов), уникальное, не пустое
    email VARCHAR(100) UNIQUE NOT NULL,   -- Email (до 100 символов), уникальный, не пустой
    password_hash VARCHAR(255) NOT NULL,  -- Хэш пароля (до 255 символов), не пустой
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP -- Время создания записи (по умолчанию - текущее время)
);


INSERT INTO users (username, email, password_hash) VALUES
('alice_123', 'alice@example.com', 'hashed_pass_for_alice'),
('bob_456', 'bob@example.com', 'hashed_pass_for_bob');

Выбрать все столбцы из таблицы users:
SQL

SELECT * FROM users;
Выбрать только username и email из таблицы users:
SQL

SELECT username, email FROM users;
Выбрать пользователей, у которых user_id равен 1:
SQL

SELECT * FROM users WHERE user_id = 1;
Выбрать пользователей, чье имя пользователя начинается с 'a' (регистронезависимо):
SQL

SELECT * FROM users WHERE username ILIKE 'a%';
Как выполнить: Вставьте код в Query Tool и нажмите "Execute/Refresh". Результат появится в нижней панели "Data Output".

г) Обновление данных (UPDATE)
Изменяет существующие данные в таблице.

Пример: Меняем email пользователя alice_123.

SQL

UPDATE users
SET email = 'new_alice_email@example.com'
WHERE username = 'alice_123';
Внимание! Всегда используйте WHERE с UPDATE, иначе вы обновите все записи в таблице!

Как выполнить: Вставьте код в Query Tool и нажмите "Execute/Refresh".

д) Удаление данных (DELETE FROM)
Удаляет строки из таблицы.

Пример: Удаляем пользователя с user_id, равным 2.

SQL

DELETE FROM users
WHERE user_id = 2;
