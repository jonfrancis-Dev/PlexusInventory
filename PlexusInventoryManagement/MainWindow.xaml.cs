using System;
using System.Collections.Generic;
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
using System.Data.Entity;

namespace PlexusInventoryManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PlexusIMSEntities db = new PlexusIMSEntities();
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
            PlexusIMSEntities db = new PlexusIMSEntities();

            //Creating a new object and adding the textbox fields to Database
            Product productObject = new Product()
            {
                //TextBox Binding to DB

                Brand = textboxBrand.Text,
                Location = textboxLocation.Text,
                Model = textboxModel.Text,
                Specs = textboxSpecs.Text,
                SerialNumber = textboxSerialNumber.Text,
                EPC = textboxEPC.Text,
                Category = textboxCategory.Text
            };
            db.Products.Add(productObject);
            db.SaveChanges();
            this.gridProducts.ItemsSource = db.Products.ToList();
        }


        //Refresh Dataset after Change!!!
        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
            PlexusIMSEntities db = new PlexusIMSEntities();

            this.gridProducts.ItemsSource = db.Products.ToList();
        }


        //Selected row is shown in Textbox !!!!
        private void gridProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime d = new DateTime();
            d=DateTime.Now;
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
                        this.textboxEPC.Text = p.EPC;
                        this.textboxUPC.Text= p.UPC;
                        this.textboxReceived.Text = d.ToString("dd.MM.yyyy");
                    }
                }
            }
        }


        //Update the database with the RowCell selected

        
       private void updateBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        //DELETE ROW FROM DATABASE, Save, Then Refresh!!!!!!!!
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            PlexusIMSEntities db = new PlexusIMSEntities();
            //Change productsID to Id and set the selected item as integer
            int Id = (gridProducts.SelectedItem as Product).productsID;
            //Remove item
            var removeItem = db.Products.SingleOrDefault(x => x.productsID == Id); //returns a single item.
            //If Id is not = null remove the item
            if (removeItem != null)
            {
                db.Products.Remove(removeItem);
                db.SaveChanges();
                this.gridProducts.ItemsSource = db.Products.ToList();
            }
        }
    }
}