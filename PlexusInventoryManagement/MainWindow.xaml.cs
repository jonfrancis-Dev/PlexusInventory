using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Data;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
namespace PlexusInventoryManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PlexusIMSEntities db = new PlexusIMSEntities();
        public MainWindow()
        {
            InitializeComponent();

            //Selecting all items in Products
            var products = from p in db.Products
                           select p;
            

            foreach (var product in products)
            {
                Console.WriteLine(product.productsID);
                Console.WriteLine(product.Brand);
            }
            this.gridProducts.ItemsSource = products.ToList();
        }

        //Add Data using Textboxes, Save, then Refresh!!!
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {            
            //Creating a new object and adding the textbox fields to Database
                Product productObject = new Product()
                {
                    //TextBox = Column Name
                    Brand = textboxBrand.Text,
                    Location = textboxLocation.Text,
                    Model = textboxModel.Text,
                    Specs = textboxSpecs.Text,
                    Quantity = Convert.ToInt32(this.textboxQty.Text),
                    SerialNumber = textboxSerialNumber.Text,
                    EPC = textboxEPC.Text,
                    Category = textboxCategory.Text,
                    Grade = textboxGrade.Text,
                    UPC = textboxUPC.Text
                };
                db.Products.Add(productObject);//Add the textbox info to database
                db.SaveChanges();//Save Changes
                this.gridProducts.ItemsSource = db.Products.ToList();//Refresh DataGrid
        }

        //Refresh Dataset after Change
        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.gridProducts.ItemsSource = db.Products.ToList();
        }


        //Selected row is shown in Textbox
        private void gridProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.gridProducts.SelectedIndex >= 0)
                {
                    if (gridProducts.SelectedItems.Count >= 0)
                    {
                        if (this.gridProducts.SelectedItems[0].GetType() == typeof(Product))
                        {
                            Product p = (Product)this.gridProducts.SelectedItems[0];
                            this.textboxBrand.Text = p.Brand;
                            this.textboxLocation.Text = p.Location;
                            this.textboxSpecs.Text = p.Specs;
                            this.textboxModel.Text = p.Model;
                            this.textboxSerialNumber.Text = p.SerialNumber;
                            this.textboxCategory.Text = p.Category;
                            this.textboxQty.Text = p.Quantity.ToString(); //Convert to string, If Null Still Return(?)
                            this.textboxEPC.Text = p.EPC;
                            this.textboxUPC.Text = p.UPC;
                            this.textboxGrade.Text = p.Grade;
                            this.textboxSearch.Text = p.productsID.ToString();//Convert to String

                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        // UPDATE/EDIT THE DATABASE with the RowCell selected!!!        
       private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            PlexusIMSEntities db = new PlexusIMSEntities();
            int i = Convert.ToInt32(textboxQty.Text);//Convert QTY to Text for textbox to column
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-MCOMTN9;Initial Catalog=PlexusIMS;User ID=sa;Password=PlTe$#2018");
            con.Open();
            //Update product with Textboxes filled
            SqlCommand cmd = new SqlCommand("UPDATE Product SET Brand='" + textboxBrand.Text + "', EPC='"+textboxEPC.Text+"', Category='"+textboxCategory.Text+ "', " +
                "Location='"+textboxLocation.Text+ "', Model='"+textboxModel.Text+ "',Specs='"+textboxSpecs.Text+ "', UPC='"+textboxUPC.Text+ "', Quantity='" + textboxQty.Text + "', SerialNumber='" + textboxSerialNumber.Text+ "'," +
                "Grade='"+textboxGrade.Text+"' WHERE productsID ='"+textboxSearchBar.Text+"' ", con);
            try
            {
                cmd.ExecuteNonQuery();
                db.SaveChanges();
                this.gridProducts.ItemsSource = db.Products.ToList();
                MessageBox.Show("Record has Been Updated");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        //DELETE ROW FROM DATABASE, Save, Then Refresh
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            PlexusIMSEntities db = new PlexusIMSEntities();
            //Change productsID to Id and set the selected item as integer
            int Id = (gridProducts.SelectedItem as Product).productsID;
            //Remove item from DB
            var removeItem = db.Products.SingleOrDefault(x => x.productsID == Id); //returns a single item.
            //If Id is not = null remove the item
            if (removeItem != null)
            {
                db.Products.Remove(removeItem);
                db.SaveChanges();
                this.gridProducts.ItemsSource = db.Products.ToList();
            }
        }

        //CLEAR the Textboxes
        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            textboxBrand.Clear();
            textboxLocation.Clear();
            textboxModel.Clear();
            textboxSpecs.Clear();
            textboxQty.Clear();
            textboxSerialNumber.Clear();
            textboxEPC.Clear();
            textboxCategory.Clear();
            textboxGrade.Clear();
            textboxUPC.Clear();
            textboxSearch.Clear();
        }

        private void textboxSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (PlexusIMSEntities db = new PlexusIMSEntities())
            {
                ObservableCollection<Product> products = new ObservableCollection<Product>();
                ListCollectionView collectionView = new ListCollectionView(products);
                if (String.IsNullOrWhiteSpace(textboxSearchBar.Text))
                {
                
                }

                else
                {
                    foreach (Product product in db.Products)
                    {
                        if (textboxSearchBar.Text != "")
                        {
                            
                            if (product.Model.Contains(textboxSearchBar.Text))
                            {
                                products.Add(product);
                            }
                            if (product.Brand.Contains(textboxSearchBar.Text))
                            {
                                products.Add(product);
                            }
                            /* if (product.UPC.Contains(textboxSearchBar.Text))
                             {
                                 products.Add(product);
                             }*/

                            if (product.Category.Contains(textboxSearchBar.Text))
                            {
                                products.Add(product);
                            }
                            if (product.Grade.Contains(textboxSearchBar.Text))
                            {
                                products.Add(product);
                            }
                            if (product.EPC.Contains(textboxSearchBar.Text))
                            {
                                products.Add(product);
                            }
                            if (product.SerialNumber.Contains(textboxSearchBar.Text))
                            {
                                products.Add(product);
                            }
                            if (product.Location.Contains(textboxSearchBar.Text))
                            {
                                products.Add(product);
                            }
                        }
                        gridProducts.ItemsSource = products.ToList();
                    }
                }
            }                 
        }
    }
}