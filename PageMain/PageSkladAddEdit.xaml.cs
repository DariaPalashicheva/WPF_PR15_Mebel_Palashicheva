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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_PR15_Mebel_Palashicheva.ApplicationData;

namespace WPF_PR15_Mebel_Palashicheva.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageSkladAddEdit.xaml
    /// </summary>
    public partial class PageSkladAddEdit : Page
    {
        private Tovars _currentTovars = new Tovars();
        public PageSkladAddEdit(Tovars selectedTovars)
        {
            InitializeComponent();
            if (selectedTovars != null)
                _currentTovars = selectedTovars;

            DataContext = _currentTovars;
            ComboColor.ItemsSource = SleepEntities.GetContext().Color.ToList();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // проверка заполняемости полей
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentTovars.naimenov))
                errors.AppendLine("Укажите название товара");

            if (_currentTovars.kolichestvo <= 0)
                errors.AppendLine("Количество товара не может быть меньше или равно 0");

            if (_currentTovars.cena <= 0)
                errors.AppendLine("Цена не может быть меньше или равна 0");


            if (_currentTovars.Color == null)
                errors.AppendLine("Выберите цвет");

            if (string.IsNullOrWhiteSpace(_currentTovars.brand))
                errors.AppendLine("Укажите название бренда");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            //добавление
            if (_currentTovars.id == 0)
                SleepEntities.GetContext().Tovars.Add(_currentTovars);
            try
            {
                SleepEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            AppFrame.FrameMain.Navigate(new PageMain.PageTovars());
        }
    }
}
