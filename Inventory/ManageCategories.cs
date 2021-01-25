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
namespace Inventory
{
    public partial class ManageCategories : Form
    {
        public ManageCategories()
        {
            InitializeComponent();
        }


        /** Connection */
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DIMESH\Documents\Inventorydb2.mdf;Integrated Security=True;Connect Timeout=30");



        /** database table to grid view */
        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from CategoryTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CategoriesGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }



        /** Add Table */
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into CategoryTbl values('" + CategoriesIdTb.Text + "','" +CategoryNameTb.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Successfully Added");
                Con.Close();
                populate();
            }
            catch
            {

            }

        }


        /** delete */
        private void button3_Click(object sender, EventArgs e)
        {
            if (CategoriesIdTb.Text == "")
            {
                MessageBox.Show("Enter the Users Phone Number");
            }
            else
            {
                Con.Open();
                String myquery = "delete from CustomerTbl where CustId='" + CategoriesIdTb.Text + "';";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category successfully Deleted");
                Con.Close();
                populate();
            }
        }



        /** update */
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update CategoryTbl set CatName='" + CategoryNameTb.Text + "'where CatId= '" + CategoriesIdTb.Text + "'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Successfully Updated");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void ManageCategories_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void CategoriesGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CategoriesIdTb.Text= CategoriesGV.SelectedRows[0].Cells[0].Value.ToString();
            CategoryNameTb.Text = CategoriesGV.SelectedRows[0].Cells[1].Value.ToString();
            
        }
    }
}
