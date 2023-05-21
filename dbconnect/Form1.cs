using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;


namespace dbconnect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Hello PostgreSQL");
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=admin;Database=test_db";
            var conn = new NpgsqlConnection(connString);
            conn.Open();
            var cmd = new NpgsqlCommand("select name from public.member", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
                Console.WriteLine(reader.GetString(0));
           // cmd.Dispose();
           reader.Close();
            string SQL = "insert into public.member(id, name, password, singal) values(666,'member6','password6','signal6')";
            //ExecNonQuery(SQL, conn);
            Console.WriteLine("变更行数:" + ExecNonQuery(SQL, conn, cmd));
        }

        static int ExecNonQuery(string _SQLCommand, NpgsqlConnection _conn, NpgsqlCommand _cmd)
        {
            int result = 0;
            //NpgsqlCommand cmd2 = new NpgsqlCommand(_SQLCommand, _conn);
            _cmd.CommandText = _SQLCommand;
            _cmd.CommandType = CommandType.Text;
            result = _cmd.ExecuteNonQuery();  //执行SQL语句；Insert,Update,Delete方式都可以
            _cmd.Dispose();  //释放资源
            return result;
        }
    }
}
