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
    /// Логика взаимодействия для MechanicsTab.xaml
    /// </summary>
    public partial class MechanicsTab : Window
    {
        CarRepairEntities db;
        public MechanicsTab()
        {
            InitializeComponent();
            db = new CarRepairEntities();
            dgMechanics.ItemsSource = db.Mechanics.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mechanics pr = new Mechanics();
            pr.mechanic_id = Convert.ToInt32(tbMechanic_id.Text);
            pr.phone_number = tbPhone_number.Text;
            pr.last_name = tbLast_name.Text;
            pr.first_name = tbFirst_name.Text;
            pr.specialization = tbSpecialization.Text;
            db.Mechanics.Add(pr);
            db.SaveChanges();
            dgMechanics.ItemsSource = db.Mechanics.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int sDMechanicId = Convert.ToInt32(tbMechanic_id.Text);
            var selectDMechanicId = db.Mechanics.Where(w => w.mechanic_id == sDMechanicId).FirstOrDefault();
            db.Mechanics.Remove(selectDMechanicId);
            db.SaveChanges();
            dgMechanics.ItemsSource = db.Mechanics.ToList();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int sUMechanicId = Convert.ToInt32(tbMechanic_id.Text);
            var selectUMechanicId = db.Mechanics.Where(w => w.mechanic_id == sUMechanicId).FirstOrDefault();
            selectUMechanicId.mechanic_id = Convert.ToInt32(tbMechanic_id.Text);
            selectUMechanicId.first_name = tbFirst_name.Text;
            selectUMechanicId.last_name = tbLast_name.Text;
            selectUMechanicId.phone_number = tbPhone_number.Text;
            selectUMechanicId.specialization = tbSpecialization.Text;
            db.SaveChanges();
            dgMechanics.ItemsSource = db.Mechanics.ToList();
        }

        private void DelB_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
