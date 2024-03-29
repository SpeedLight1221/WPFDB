﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace db
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        const string dbFile = "C:\\Temp/char/data.db";
        const string tableName = "MyTable";
        const string connectionString = "Data Source=" + dbFile + ";Version=3";
        public MainWindow()
        {
            InitializeComponent();
            inntDB();
        }

        private void inntDB()
        {
            using(var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var command = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS {tableName} " + $"(Id INTEGER PRIMARY KEY,name TEXT)",conn);
                command.ExecuteNonQuery();
            }
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void insertData(string Name)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var command = new SQLiteCommand($"INSERT INTO {tableName} (name) " + $"VALUES ('{Name})", conn);
                command.ExecuteNonQuery();
            }
        }


        private List<string> GetData()
        {
            List<string> Data = new List<string>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var command = new SQLiteCommand($"SELECT * FROM {tableName}", conn);
                using (var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Data.Add(reader["name"].ToString());
                    }
                }
            }

            return Data;    
        }
    }
}
