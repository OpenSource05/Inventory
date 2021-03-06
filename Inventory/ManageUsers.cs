﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Inventory
{
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }


        /** Connection */
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DIMESH\Documents\Inventorydb2.mdf;Integrated Security=True;Connect Timeout=30");

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        /** database table to grid view */
        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from UserTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder  = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                UsersGv.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }


        /** Value Insert to table - Add */ 
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into UserTbl values('" + unameTb.Text + "','" + FnameTb.Text + "','" + PasswordTb.Text + "','" + PhoneTb.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Added");
                Con.Close();
                populate();
            }
            catch
            {

            }

        }


        /** Exit Button */
        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            populate();
        }



        /** Delete row from tabel - Delete */
        private void button3_Click(object sender, EventArgs e)
        {
            if(PhoneTb.Text =="")
            {
                MessageBox.Show("Enter the Users Phone Number");
            }
            else
            {
                Con.Open();
                String myquery = "delete from UserTbl where UPhone='" + PhoneTb.Text + "';";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User successfully Deleted");
                Con.Close();
                populate();
            }

        }


        /** after the grid view row click data shows in input text fields */
        private void UsersGv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            unameTb.Text = UsersGv.SelectedRows[0].Cells[0].Value.ToString();
            FnameTb.Text = UsersGv.SelectedRows[0].Cells[1].Value.ToString();
            PasswordTb.Text = UsersGv.SelectedRows[0].Cells[2].Value.ToString();
            PhoneTb.Text = UsersGv.SelectedRows[0].Cells[3].Value.ToString();
        }


        /** update table */
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update UserTbl set Uname='" + unameTb.Text + "',Ufullname'" + FnameTb.Text + "',Upassword='" + PasswordTb.Text + "' where UPhone='" + PhoneTb.Text + "'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Updated");
                Con.Close();
                populate();
            }
            catch
            {

            }

        }
    }
}
