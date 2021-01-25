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
    public partial class ManageProducts : Form
    {
        public ManageProducts()
        {
            InitializeComponent();
        }


        /** Connection */
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DIMESH\Documents\Inventorydb2.mdf;Integrated Security=True;Connect Timeout=30");



        void fillcategory()
        {
            String query = "select * from CategoryTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                CatCombo.ValueMember = "CatName";
                CatCombo.DataSource = dt;
                SearchCombo.ValueMember = "CatName";
                SearchCombo.DataSource = dt;
                Con.Close();
            }
            catch
            {

            }

        }



        void fillSearchcombo()
        {
            String query = "select * from CategoryTbl where CatName='"+SearchCombo.SelectedValue.ToString()+"'";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                CatCombo.ValueMember = "CatName";
                CatCombo.DataSource = dt;
                Con.Close();
            }
            catch
            {

            }

        }

        private void ManageProducts_Load(object sender, EventArgs e)
        {
            fillcategory();
            populate();
        }



        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }




        void filterbycategory()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTbl where ProdCat='" + SearchCombo.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }





        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into ProductTbl values("+ ProductIdTb.Text + ",'" + ProductNameTb.Text + "','" + ProductQtyTb.Text + "','" +ProductPriceTb.Text +"','"+DescriptionTb.Text+"','"+CatCombo.SelectedValue.ToString()+ "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Added");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }




        private void CategoriesGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductIdTb.Text = ProductsGV.SelectedRows[0].Cells[0].Value.ToString();
            ProductNameTb.Text = ProductsGV.SelectedRows[0].Cells[1].Value.ToString();
            ProductQtyTb.Text = ProductsGV.SelectedRows[0].Cells[2].Value.ToString();
            ProductPriceTb.Text = ProductsGV.SelectedRows[0].Cells[3].Value.ToString();
            DescriptionTb.Text = ProductsGV.SelectedRows[0].Cells[4].Value.ToString();
            CatCombo.SelectedValue = ProductsGV.SelectedRows[0].Cells[5].Value.ToString();
        }





        private void button3_Click(object sender, EventArgs e)
        {
            if (ProductIdTb.Text == "")
            {
                MessageBox.Show("Enter the Product Id");
            }
            else
            {
                Con.Open();
                String myquery = "delete from ProductTbl where ProdId=" + ProductIdTb.Text + ";";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product successfully Deleted");
                Con.Close();
                populate();
            }
        }





        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update productTbl set ProductName='" + ProductNameTb.Text + "',ProductQty= "+ProductQtyTb.Text+",ProdDesc='"+DescriptionTb.Text+ "',ProductCategory='" + CatCombo.SelectedValue.ToString()+"' where ProductId=" + ProductIdTb.Text+"", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Updated");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            filterbycategory();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            populate();
        }
    }
}
