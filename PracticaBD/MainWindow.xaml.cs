using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
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

namespace PracticaBD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CarRepairEntities db;

        public MainWindow()
        {
            InitializeComponent();
            db = new CarRepairEntities();

            // Загрузка данных в DataGrid и ComboBox
            LoadData();
        }

        private void LoadData()
        {
            // Загрузка данных в DataGrid
            var cars = from car in db.Cars
                       join customer in db.Customers on car.customer_id equals customer.customer_id
                       select new
                       {
                           car.car_id,
                           CustomerName = customer.first_name + " " + customer.last_name,
                           car.brand,
                           car.model,
                           car.year,
                           car.license_plate,
                           car.vin
                       };
            dgCars.ItemsSource = cars.ToList();

            // Загрузка данных в ComboBox для клиентов
            tbCustomerID.ItemsSource = db.Customers
                .Select(c => new
                {
                    c.customer_id,
                    CustomerName = c.first_name + " " + c.last_name
                })
                .ToList();

            tbCustomerID.DisplayMemberPath = "CustomerName"; // Поле, отображаемое в ComboBox
            tbCustomerID.SelectedValuePath = "customer_id"; // Поле, используемое как значение
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cars car = new Cars
                {
                    car_id = Convert.ToInt32(tbCar_ID.Text),
                    customer_id = Convert.ToInt32(tbCustomerID.SelectedValue),
                    brand = tbBrand.Text,
                    model = tbmodel.Text,
                    year = Convert.ToInt32(tbyear.Text),
                    license_plate = tblicense_plate.Text,
                    vin = tbvin.Text
                };

                db.Cars.Add(car);
                db.SaveChanges();
                LoadData(); // Обновление данных в DataGrid
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                int carId = Convert.ToInt32(tbCar_ID.Text);
                var carToDelete = db.Cars.FirstOrDefault(car => car.car_id == carId);

                if (carToDelete != null)
                {
                    db.Cars.Remove(carToDelete);
                    db.SaveChanges();
                    LoadData(); // Обновление данных в DataGrid
                }
                else
                {
                    MessageBox.Show("Машина с указанным ID не найдена.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                int carId = Convert.ToInt32(tbCar_ID.Text);
                var carToUpdate = db.Cars.FirstOrDefault(car => car.car_id == carId);

                if (carToUpdate != null)
                {
                    carToUpdate.customer_id = Convert.ToInt32(tbCustomerID.SelectedValue);
                    carToUpdate.brand = tbBrand.Text;
                    carToUpdate.model = tbmodel.Text;
                    carToUpdate.year = Convert.ToInt32(tbyear.Text);
                    carToUpdate.license_plate = tblicense_plate.Text;
                    carToUpdate.vin = tbvin.Text;

                    db.SaveChanges();
                    LoadData(); // Обновление данных в DataGrid
                }
                else
                {
                    MessageBox.Show("Машина с указанным ID не найдена.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении: {ex.Message}");
            }
        }

        private void tbCustomerID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Здесь можно обработать выбор клиента в ComboBox, если нужно
        }

        private void tbyear_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Обработка изменений в поле года (опционально)
        }

        private void Button_Click_NextWindow(object sender, RoutedEventArgs e)
        {
            customers_wpf nextWindow = new customers_wpf();
            nextWindow.Show();
            this.Close();
        }

        private void Button_Click_Export(object sender, RoutedEventArgs e)
        {
            this.dgCars.SelectAllCells();
            this.dgCars.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, this.dgCars);
            this.dgCars.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            try
            {
                StreamWriter sw = new StreamWriter("wpfdaata.csv");
                sw.WriteLine(result);
                sw.Close();
                Process.Start("wpfdaata.csv");
            }
            catch (Exception ex)
            { }
        }
    }
}