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
using System.Data.SQLite;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Game game;

        public Form1()
        {

            InitializeComponent();
            game = new Game(4);
            LoadData();
        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            int position = Convert.ToInt16(((Button)sender).Tag);
            game.shift(position);
            refresh();
            if (game.check_succes())
            {

                timer1.Enabled = false;
                MessageBox.Show(String.Format("you win! Ваше время: "));

            }

        }

        private Button button(int position)
        {
            switch (position)
            {
                case 0: return button0;
                case 1: return button1;
                case 2: return button2;
                case 3: return button3;
                case 4: return button4;
                case 5: return button5;
                case 6: return button6;
                case 7: return button7;
                case 8: return button8;
                case 9: return button9;
                case 10: return button10;
                case 11: return button11;
                case 12: return button12;
                case 13: return button13;
                case 14: return button14;
                case 15: return button15;
                default: return null;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public int m, s, ms;

        public void timer1_Tick(object sender, EventArgs e)
        {
            ms += 1;
            if (ms == 60)
            {
                ms = 0;
                s += 01;
            }
            if (s == 60)
            {
                s = 0;
                m += 01;
            }

            label1.Text = m.ToString() + ":" + s.ToString() + ":" + ms.ToString();
        }

        private void button16_Click(object sender, EventArgs e)
        {

            start_game();
            m = 0;
            ms = 0;
            s = 0;
            timer1.Enabled = true;

        }

        private void start_game()

        {
            game.start();
            for (int j = 0; j < 2; j++)
                game.shift_rand();
            refresh();
        }

        private void LoadData()

        {
            string connectString = "Data Source=.\\SQLEXPRESS;Initial Catalog=LSTU_Schedule_autumn20172018;" +
            "Integrated Security=true;";

            SqlConnection myConnection = new SqlConnection(connectString);

            myConnection.Open();

            string query = "SELECT * FROM Faculty ORDER BY N";

            SqlCommand command = new SqlCommand(query, myConnection);

            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[3]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
            }

            reader.Close();
            myConnection.Close();

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void refresh()

        {
            for (int position = 0; position < 16; position++)
            {
                int nr = game.get_number(position);
                button(position).Text = nr.ToString();
                button(position).Visible = (nr > 0);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
