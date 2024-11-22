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
    /// Логика взаимодействия для wpf_workOrders.xaml
    /// </summary>
    public partial class wpf_workOrders : Window
    {
        CarRepairEntities db;

        public wpf_workOrders()
        {
            InitializeComponent();
            db = new CarRepairEntities();

            // Загрузка данных в DataGrid
            dgWorkOrders.ItemsSource = db.WorkOrders.Include("Cars").Include("Mechanics").Include("Services").ToList();

            // Загрузка данных в ComboBoxes
            cbCar.ItemsSource = db.Cars.ToList();
            cbMechanic.ItemsSource = db.Mechanics.ToList();
            cbService.ItemsSource = db.Services.ToList();
        }

        private void dgWorkOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgWorkOrders.SelectedItem is WorkOrders selectedWorkOrder)
            {
                cbCar.SelectedValue = selectedWorkOrder.car_id;
                cbMechanic.SelectedValue = selectedWorkOrder.mechanic_id;
                cbService.SelectedValue = selectedWorkOrder.service_id;

                tbOrderDate.Text = selectedWorkOrder.work_order_date.ToString("yyyy-MM-dd");
                tbCompletionDate.Text = selectedWorkOrder.completion_date?.ToString("yyyy-MM-dd");
                tbStatus.Text = selectedWorkOrder.status;
            }
        }

        private void AddWorkOrder_Click(object sender, RoutedEventArgs e)
        {
            WorkOrders newOrder = new WorkOrders
            {
                car_id = (int?)cbCar.SelectedValue,
                mechanic_id = (int?)cbMechanic.SelectedValue,
                service_id = (int?)cbService.SelectedValue,
                work_order_date = DateTime.Parse(tbOrderDate.Text),
                completion_date = string.IsNullOrEmpty(tbCompletionDate.Text) ? (DateTime?)null : DateTime.Parse(tbCompletionDate.Text),
                status = tbStatus.Text
            };

            db.WorkOrders.Add(newOrder);
            db.SaveChanges();
            RefreshDataGrid();
        }

        private void DeleteWorkOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgWorkOrders.SelectedItem is WorkOrders selectedWorkOrder)
            {
                db.WorkOrders.Remove(selectedWorkOrder);
                db.SaveChanges();
                RefreshDataGrid();
            }
        }

        private void UpdateWorkOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgWorkOrders.SelectedItem is WorkOrders selectedWorkOrder)
            {
                selectedWorkOrder.car_id = (int?)cbCar.SelectedValue;
                selectedWorkOrder.mechanic_id = (int?)cbMechanic.SelectedValue;
                selectedWorkOrder.service_id = (int?)cbService.SelectedValue;
                selectedWorkOrder.work_order_date = DateTime.Parse(tbOrderDate.Text);
                selectedWorkOrder.completion_date = string.IsNullOrEmpty(tbCompletionDate.Text) ? (DateTime?)null : DateTime.Parse(tbCompletionDate.Text);
                selectedWorkOrder.status = tbStatus.Text;

                db.SaveChanges();
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            dgWorkOrders.ItemsSource = db.WorkOrders.Include("Car").Include("Mechanic").Include("Service").ToList();
        }


    }
}
