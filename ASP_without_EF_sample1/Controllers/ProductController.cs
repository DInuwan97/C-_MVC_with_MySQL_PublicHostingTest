using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MySql.Data.MySqlClient;

namespace ASP_without_EF_sample1.Controllers
{
    

    public class ProductController : Controller
    {
        //String connectionString = @"server=localhost;port=3306;user id=root;database=mvc_curd_db;persistsecurityinfo=True;password=ThirtyFirst9731@;";
        //above one is the local db connection


        //to public host
        String connectionString = @"server=148.72.232.177;user id=sustain_db;password=Smart@7744;database=sustain_db;";



        [HttpGet]
        // GET: Product
        public ActionResult Index()
        {
            
            DataTable dtTableProduct = new DataTable();
            using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
            {
                mySqlCon.Open();
                MySqlDataAdapter MySqlDA = new MySqlDataAdapter("SELECT * FROM products",mySqlCon);
                MySqlDA.Fill(dtTableProduct);
            }
                return View(dtTableProduct);
        }





        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Models.ProductModel());
        }





        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Models.ProductModel productModel)
        {
             // TODO: Add insert logic here
                using (MySqlConnection mysqlCon = new MySqlConnection(connectionString))
                {
                    mysqlCon.Open();
                    String insertQry = "INSERT INTO products(productName,price,stock)VALUES(@ProductName,@Price,@Stock)";
                    MySqlCommand mySqlcmd = new MySqlCommand(insertQry,mysqlCon);
                    mySqlcmd.Parameters.AddWithValue("@ProductName",productModel.ProductName);
                    mySqlcmd.Parameters.AddWithValue("@Price", productModel.Price);
                    mySqlcmd.Parameters.AddWithValue("@Stock", productModel.Stock);
                    mySqlcmd.ExecuteNonQuery();

                }
                    return RedirectToAction("Index");
            
        }




        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Models.ProductModel productModel = new Models.ProductModel();
            DataTable dtblProduct = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                String query = "SELECT * FROM products WHERE productId = @ProductID";
                MySqlDataAdapter mySqlDa = new MySqlDataAdapter(query,con);
                mySqlDa.SelectCommand.Parameters.AddWithValue("@ProductID",id);
                mySqlDa.Fill(dtblProduct);

            }
            if(dtblProduct.Rows.Count == 1)
            {
                productModel.ProductID = Convert.ToInt32(dtblProduct.Rows[0][0].ToString());
                productModel.ProductName = dtblProduct.Rows[0][1].ToString();
                productModel.Price = Convert.ToDecimal(dtblProduct.Rows[0][2].ToString());
                productModel.Stock = Convert.ToInt32(dtblProduct.Rows[0][3].ToString());
                return View(productModel);
            }
            else
            {
                return RedirectToAction("index");
            }

                
        }





        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.ProductModel productModel)
        {
            using (MySqlConnection SqlCon = new MySqlConnection(connectionString))
            {
                SqlCon.Open();
                String query = "UPDATE products SET productName = @ProductName , Price = @Price , stock = @Stock WHERE productId = @ProductID";
                MySqlCommand sqlCmd = new MySqlCommand(query,SqlCon);
                sqlCmd.Parameters.AddWithValue("@ProductID", productModel.ProductID);
                sqlCmd.Parameters.AddWithValue("@ProductName",productModel.ProductName);
                sqlCmd.Parameters.AddWithValue("@Price", productModel.Price);
                sqlCmd.Parameters.AddWithValue("@Stock", productModel.Stock);
                sqlCmd.ExecuteNonQuery();


            }

          return RedirectToAction("Index");
           
        }




        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {

            using (MySqlConnection mySqlcon = new MySqlConnection(connectionString))
            {
                mySqlcon.Open();
                String query = "DELETE FROM products WHERE productId = @ProductID";
                MySqlCommand mySqlCmd = new MySqlCommand(query,mySqlcon);
                mySqlCmd.Parameters.AddWithValue("@ProductID",id);
                mySqlCmd.ExecuteNonQuery();
            }
                return RedirectToAction("index");
        }




        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




    }//end of the ProductController Class
}
