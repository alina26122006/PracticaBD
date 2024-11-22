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
using System.Security.Cryptography;
using System.Drawing;
using Color = System.Drawing.Color;

namespace PracticaBD
{

    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        CarRepairEntities db;
        private int loginAttempts = 0;
        private string captchaCode;
        public Window1()
        {
            InitializeComponent();
            db = new CarRepairEntities();
            
        }
        private string GenerateSalt()
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            byte[] saltBytes = new byte[16]; 
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes); 
        }
        private void AddUser(string login, string password)
        {
            if (db == null) 
            {
                db = new CarRepairEntities();
            }
            var existingUser = db.Users.FirstOrDefault(u => u.Login == login);
            if (existingUser != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!");
                return;
            }
            string salt = GenerateSalt(); 
            string hashedPassword = HashPassword(password, salt);
            var newUser = new Users
            {
                Login = login,
                PasswordHash = hashedPassword,
                Salt = salt
            };
            db.Users.Add(newUser); 
            db.SaveChanges(); 
        }


        private string HashPassword(string password, string salt)
        {
            using (var rfc2898 = new System.Security.Cryptography.Rfc2898DeriveBytes(
                password,
                Convert.FromBase64String(salt), 
                10000, 
                System.Security.Cryptography.HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = rfc2898.GetBytes(32); 
                return Convert.ToBase64String(hashBytes); 
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db = new CarRepairEntities();
            try
            {
                var user = db.Users.FirstOrDefault(d => d.Login == tbL.Text);

                if (user != null)
                {
                    string inputHash = HashPassword(tbP.Text, user.Salt);

                    if (inputHash == user.PasswordHash)
                    {
                        loginAttempts = 0;
                        MessageBox.Show("Успешный вход!");
                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                    }
                    else
                    {
                        loginAttempts++;
                        MessageBox.Show("Неверный пароль!");
                    }
                }
                else
                {
                    loginAttempts++;
                    MessageBox.Show("Пользователь не найден!");
                }

                if (loginAttempts >= 3)
                {
                    GenerateCaptcha(); // Генерация капчи
                    MessageBox.Show("Введите капчу, чтобы продолжить.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            if (loginAttempts >= 3)
            {
                GenerateCaptcha();
                captchaImage.Visibility = Visibility.Visible;
                tbCaptcha.Visibility = Visibility.Visible;
                CheckCaptchaButton.Visibility = Visibility.Visible;
            }
        }

        private void GenerateCaptcha()
        {
            Random rand = new Random();
            int captchaLength = rand.Next(4, 8);
            captchaCode = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", captchaLength)
                                                .Select(s => s[rand.Next(s.Length)]).ToArray());

            int width = 200;
            int height = 80;

            using (Bitmap bitmap = new Bitmap(width, height))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.LightGray);

                // Добавление шума
                for (int i = 0; i < 100; i++)
                {
                    int x = rand.Next(0, width);
                    int y = rand.Next(0, height);
                    Color randomColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                    bitmap.SetPixel(x, y, randomColor);
                }

                // Добавление линий
                for (int i = 0; i < 10; i++)
                {
                    int x1 = rand.Next(0, width);
                    int y1 = rand.Next(0, height);
                    int x2 = rand.Next(0, width);
                    int y2 = rand.Next(0, height);

                    Color randomColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                    using (System.Drawing.Pen pen = new System.Drawing.Pen(randomColor, 2))
                    {
                        g.DrawLine(pen, x1, y1, x2, y2);
                    }
                }

                // Добавление текста
                int xPosition = 10;
                foreach (char c in captchaCode)
                {
                    int fontSize = rand.Next(20, 35);
                    float angle = rand.Next(-30, 30);
                    Color textColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));

                    using (Font font = new Font("Arial", fontSize, System.Drawing.FontStyle.Bold)) // Здесь используется FontStyle.Bold из System.Drawing
                    using (System.Drawing.Brush brush = new SolidBrush(textColor))
                    {
                        g.TranslateTransform(xPosition, height / 2);
                        g.RotateTransform(angle);

                        g.DrawString(c.ToString(), font, brush, 0, 0);
                        g.ResetTransform();

                        xPosition += fontSize - 5;
                    }
                }

                // Сохранение капчи
                string captchaPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "captcha.png");
                bitmap.Save(captchaPath);
                using (var ms = new System.IO.MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ms.Seek(0, System.IO.SeekOrigin.Begin);
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    // Передаем изображение в Image контрол
                    captchaImage.Source = bitmapImage;
                }
                MessageBox.Show($"Капча сохранена: {captchaPath}");
            }
        }

        private void CheckCaptcha_Click(object sender, RoutedEventArgs e)
        {
            if (tbCaptcha.Text == captchaCode)
            {
                MessageBox.Show("Капча введена верно!");
                loginAttempts = 0;
                captchaImage.Visibility = Visibility.Collapsed;
                tbCaptcha.Visibility = Visibility.Collapsed;
                CheckCaptchaButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Капча введена неверно. Попробуйте ещё раз.");
            }
        }



        private void tbP_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbL_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void TestHashButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void AddTestUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddUser("al", "a");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

    }
}
