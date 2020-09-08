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
using TestDataBase.Model;
using TestDataBase.ViewModel;

namespace TestDataBase.View
{
    /// <summary>
    /// Логика взаимодействия для UCDbView.xaml
    /// </summary>
    public partial class UCDbView : UserControl
    {
        public DbViewModel VM
        {
            get
            {
                return this.DataContext as DbViewModel;
            }
        }
        public UCDbView()
        {
            InitializeComponent();
            var vm = new DbViewModel();
            vm.SetDatabaseAbility(new DbDapperAccess());
            this.DataContext = vm;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (VM == null || String.IsNullOrEmpty(txbx.Text))
                return;
            if (VM.VMMode == Mode.Insert)
            {
                int res = await VM.AddNewMessage(txbx.Text);
                if (res == 0)
                    txbx.Clear();
            }
            else
                await VM.UpdateMessage(new DbRecord(VM.SelectedRecord.Id, txbx.Text));
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cmbx = sender as ComboBox;
            if (cmbx == null || VM == null)
                return;
            VM.UpdateViewMode(cmbx.SelectedIndex, () => { txbx.Text = (VM.VMMode == Mode.Update) ? VM.SelectedRecord.Message : String.Empty; });
        }

        private void txbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txbx = sender as TextBox;
            if (txbx == null || VM == null)
                return;
            if (VM.VMMode == Mode.Update)
                VM.CompareEditMessage(txbx.Text);
        }
    }
}
