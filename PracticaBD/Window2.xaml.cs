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
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        CarRepairEntities db;
        public Window2()
        {
            InitializeComponent();
            combobind();
        }
        public List<Cars> Cars { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new CarRepairEntities();
            dgP.ItemsSource = db.Cars.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog pd = new PrintDialog();
                if (pd.ShowDialog() == true)
                {
                    pd.PrintVisual(dgP, "Проект");
                }
            }
            finally
            { this.IsEnabled = true; }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgP.ItemsSource= db.Cars.Where(x => x.brand.Contains(tbN.Text)).ToList();
        }

        private void cbP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgP.ItemsSource = db.Cars.Where(x => x.car_id ==  cbP.SelectedIndex+1).ToList();
        }

        private void combobind()
        {
            CarRepairEntities db = new CarRepairEntities();
            var Item = db.Cars.ToList();
            Cars = Item;
            DataContext = Cars;
        }

        private void dgP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
