using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace kurs
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        public Autorization()
        {
            InitializeComponent();
        }
        DispatcherTimer _timer;
        int _countLogin = 1;

        void GetCaptcha()
        {
            string masChar = "QWERTYUIOPLKJHGFDSAZXCVBNMmnbvcxzasdfghjk" + "lpoiuytrewq1234567890";
            string captcha = "";
            Random rnd = new Random();
            for (int i = 1; i <= 6; i++)
            {
                captcha = captcha + masChar[rnd.Next(0, masChar.Length)];
            }
            grid.Visibility = Visibility.Visible;
            txtCaprcha.Text = captcha;
            tbCaptcha.Text = null;
            txtCaprcha.LayoutTransform = new RotateTransform(rnd.Next(-15, 15));
            line.X1 = 10;
            line.Y1 = rnd.Next(10, 40);
            line.X2 = 280;
            line.Y2 = rnd.Next(10, 40);
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            tbLogin.Focus();
            Data.Login = false;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 10);
            _timer.Tick += new EventHandler(timer_Tick);
    
        }
        private void timer_Tick (object sender, EventArgs e)
        {
            stackPanel.IsEnabled = true;
        }
        private void btn_Registration_Click(object sender, EventArgs e)
        {
            Registration f = new Registration();
            f.Owner = this;
            f.ShowDialog();
        }

        private void btn_Enter_Click(object sender, EventArgs e)
        {
            using (WorkContext _db = new WorkContext())
            {
                var user = _db.Users.Where(user => user.Login == tbLogin.Text && user.Password == tbPas.Password);
                if (user.Count() == 1 && txtCaprcha.Text == tbCaptcha.Text)
                {
                    Data.Login = true;
                    Data.Surname = user.First().Surname;
                    Data.Name = user.First().Name;
                    Data.Patronymic = user.First().Patronymic;
                    Data.Right = user.First().Role;
                    Close();
                }
                else
                {
                    if (user.Count() == 1)
                    {
                        MessageBox.Show("Каптча неверна! Повторите ввод");
                    }
                    else
                    {
                        MessageBox.Show("Логин, пароль неверны! Повторите ввод");
                    }
                    GetCaptcha();
                    if (_countLogin >= 2)
                    {
                        stackPanel.IsEnabled = false;
                        _timer.Start();
                    }
                    _countLogin++;
                    tbLogin.Focus();
                }
            }
        }
        private void btn_Esc_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Guest_Click(object sender, EventArgs e)
        {
            Data.Login = true;
            Data.Surname = "Гость";
            Data.Name = "";
            Data.Patronymic = "";
            Data.Right = "Клиент";
            Close();
        }
    }
}