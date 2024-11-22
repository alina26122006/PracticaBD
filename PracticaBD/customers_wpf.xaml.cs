using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
using System.Windows.Shapes;

namespace PracticaBD
{
    /// <summary>
    /// Логика взаимодействия для customers_wpf.xaml
    /// </summary>
    public partial class customers_wpf : Window
    {
        CarRepairEntities db;
        public customers_wpf()
        {
            InitializeComponent();
            db = new CarRepairEntities();
            dgCustomers.ItemsSource = db.Customers.ToList();
        }

        private void dgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Customers pr = new Customers();
            pr.customer_id = Convert.ToInt32(tbCustomerID.Text);
            pr.phone_number = tbphone_number.Text;
            pr.first_name = tbfirst_name.Text;
            pr.last_name = tblast_name.Text;
            pr.email = tbemail.Text;
            db.Customers.Add(pr);
            db.SaveChanges();
            dgCustomers.ItemsSource = db.Customers.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int sDCustomersID = Convert.ToInt32(tbCustomerID.Text);
            var selectDCustomersID = db.Customers.Where(w => w.customer_id == sDCustomersID).FirstOrDefault();
            db.Customers.Remove(selectDCustomersID);
            db.SaveChanges();
            dgCustomers.ItemsSource = db.Customers.ToList();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int sUCustomersID = Convert.ToInt32(tbCustomerID.Text);
            var selectDCustomersID = db.Customers.Where(w => w.customer_id == sUCustomersID).FirstOrDefault();
            selectDCustomersID.customer_id = Convert.ToInt32(tbCustomerID.Text);
            selectDCustomersID.first_name = tbfirst_name.Text;
            selectDCustomersID.last_name = tblast_name.Text;
            selectDCustomersID.phone_number = tbphone_number.Text;
            selectDCustomersID.email = tbemail.Text;
            db.SaveChanges();
            dgCustomers.ItemsSource = db.Cars.ToList();
        }

        private void Button_Click_NextWindow(object sender, RoutedEventArgs e)
        {
            wpf_workOrders nextWindow = new wpf_workOrders();
            nextWindow.Show();
            this.Close();
        }
    }
}
