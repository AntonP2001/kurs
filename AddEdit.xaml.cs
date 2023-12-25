using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Microsoft.Win32;


namespace kurs
{
    /// <summary>
    /// Логика взаимодействия для AddEdit.xaml
    /// </summary>
    public partial class AddEdit : Window
    {
        //ScladContext _db=new ScladContext();
        Rabotum _table2;
        OpenFileDialog open = new OpenFileDialog();
        public AddEdit()
        {
            InitializeComponent();

        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if (Data.Rabota == null)
            {
                this.Title = "Добавление записи";
                btnAddEdit.Content = "Добавить";
                _table2 = new Rabotum();

            }
            else
            {
                this.Title = "Изменение записи";
                btnAddEdit.Content = "Изменить";
                _table2 = Data.Rabota;
            }
            this.DataContext = _table2;
        }

        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (tbFamily.Text.Length == 0) errors.AppendLine("Введите фамилию");
            if (tbName.Text.Length == 0) errors.AppendLine("Введите имя");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            //try
            //{
            //    if (open.SafeFileName.Length != 0)
            //    {
            //        string newNamePhoto = Directory.GetCurrentDirectory() + "\\image\\" + open.SafeFileName;
            //        File.Copy(open.FileName, newNamePhoto, true);
            //        _table2.Photo = open.SafeFileName;
            //    }

            //}
            //catch { }
            //try
            //{
            //    if (Data.table2 == null)
            //    {
            //        _db.Table2s.Add(_table2);
            //        _db.SaveChanges();
            //    }
            //    else
            //    {
            //        _db.SaveChanges();
            //    }
            //    this.Close();
            ////}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btn_AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            open.Filter = "Все файлы |*.*|Файлы * .jpg|*.jpg";
            open.FilterIndex = 2;
            if (open.ShowDialog() == true)
            {
                BitmapImage photoImage=new BitmapImage(new Uri(open.FileName));
                imgPhoto.Source = photoImage;
            }
        }
    }
}
