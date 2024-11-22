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
using System.Windows.Shapes;

namespace PracticaBD
{
    /// <summary>
    /// Логика взаимодействия для Services_WPF.xaml
    /// </summary>
    public partial class Services_WPF : Window
    {
        CarRepairEntities db;

        public Services_WPF()
        {
            InitializeComponent();
            db = new CarRepairEntities();
            dgServices.ItemsSource = db.Services.ToList();
        }

        private void dgServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedService = dgServices.SelectedItem as Services;
            if (selectedService != null)
            {
                tbServiceID.Text = selectedService.service_id.ToString();
                tbServiceName.Text = selectedService.service_name;
                tbServiceDescription.Text = selectedService.service_description;
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            Services newService = new Services
            {
                service_name = tbServiceName.Text,
                service_description = tbServiceDescription.Text
            };
            db.Services.Add(newService);
            db.SaveChanges();
            dgServices.ItemsSource = db.Services.ToList();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            int serviceID = Convert.ToInt32(tbServiceID.Text);
            var serviceToDelete = db.Services.FirstOrDefault(s => s.service_id == serviceID);
            if (serviceToDelete != null)
            {
                db.Services.Remove(serviceToDelete);
                db.SaveChanges();
                dgServices.ItemsSource = db.Services.ToList();
            }
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            int serviceID = Convert.ToInt32(tbServiceID.Text);
            var serviceToUpdate = db.Services.FirstOrDefault(s => s.service_id == serviceID);
            if (serviceToUpdate != null)
            {
                serviceToUpdate.service_name = tbServiceName.Text;
                serviceToUpdate.service_description = tbServiceDescription.Text;
                db.SaveChanges();
                dgServices.ItemsSource = db.Services.ToList();
            }
        }

    }
}
