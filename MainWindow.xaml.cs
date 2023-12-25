using Microsoft.IdentityModel.Tokens;
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
using System.Windows.Threading;
using System.Xml;

namespace kurs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        public WorkContext _db = new WorkContext();

        public MainWindow()
        {
            InitializeComponent();
            _db = new WorkContext();
            ShowAuthorizationDialog();

        }
        private void ShowAuthorizationDialog()
        {
            Autorization f = new Autorization();
            f.ShowDialog();
            if (Data.Login == false) Close();
            if (Data.Right == "admin")
            {
            }
            //else
            //{
            //    btnDelete.IsEnabled = false;
            //}
            this.Title = this.Title + " " + Data.Surname + "" + Data.Name + "(" + Data.Right + ")";
            LoadDBInListView();
        }
        private void WindowLoaded(object sender, EventArgs e)
        {
            LoadDBInListView();
        }

        void LoadDBInListView()
        {

            using (WorkContext _db = new WorkContext())
            {
                var items = _db.Rabota.Select(x => x);

                listview1.ItemsSource = _db.Rabota.Select(x => x).ToList();

                int selectedIndex = listview1.SelectedIndex;
                //listview1.ItemsSource = _db.Table2s.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == listview1.Items.Count) selectedIndex--;
                    listview1.SelectedIndex = selectedIndex;
                    listview1.ScrollIntoView(listview1.SelectedItem);
                }
                listview1.Focus();
            }

        }
        private void addButton_Click(object sender, EventArgs e)
        {
            Data.Rabota  = null;
            AddEdit f=new AddEdit();
            f.Owner = this;
            f.ShowDialog();
            LoadDBInListView();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if(listview1.SelectedItem!=null)
            {
                Data.Rabota = (Rabotum)listview1.SelectedItem;
                AddEdit f = new AddEdit();
                f.Owner = this;
                f.ShowDialog();
                LoadDBInListView();
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Rabotum row = (Rabotum)listview1.SelectedItem;
                    String value = row.Family;
                    if (row != null)
                    {
                        using (WorkContext _db = new WorkContext())
                        {
                            _db.Rabota.Remove(row);
                            _db.SaveChanges();
                        }
                        LoadDBInListView();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка удаления");
                }
            }
            else listview1.Focus();
        }

        private void Filter_Click(object sender, EventArgs e)
        {
            if (txtFilter.Text.IsNullOrEmpty() == false)
            {
                using (WorkContext _db = new WorkContext())
                {
                    //var filtered = _db.Table2s.Where(p => p.Family.Contains(txtFilter.Text));
                    //listview1.ItemsSource = filtered.ToList();
                }
            }
            else
            {
                LoadDBInListView();
            }
        }
        private string currentSearchColumn = "Family"; // Значение по умолчанию
        private void ButtonName_Click(object sender, RoutedEventArgs e)
        {
            currentSearchColumn = "Name";
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string selectedColumn = ((ComboBoxItem)cmbSearchBy.SelectedItem)?.Tag as string;

            if (selectedColumn != null)
            {
                List<Rabotum> listItem = (List<Rabotum>)listview1.ItemsSource;
                PerformSearch(listItem, selectedColumn, txtSearch.Text);
            }
        }

        private void PerformSearch(List<Rabotum> items, string columnName, string searchText)
        {
            // В зависимости от выбранного столбца выполните поиск
            var filtered = items.Where(item =>
            {
                switch (columnName)
                {
                    case "Family":
                        return item.Family.Contains(searchText);
                    //case "Title_of_ceh":
                    //    return item.Title_of_ceh.Contains(searchText);
                    case "Country":
                        return item.Type.ToString().Contains(searchText);
                    default:
                        return false;
                }
            });

            if (filtered.Count() > 0)
            {
                var item = filtered.First();
                listview1.SelectedItem = item;
                listview1.ScrollIntoView(item);
            }
        }


    }
}
