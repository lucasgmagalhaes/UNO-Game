using System.Windows;
using System.Windows.Input;
using MauMau.Classes.Background;
namespace MauMau.Windows
{
    /// <summary>
    /// Interaction logic for ScreenLog.xaml
    /// </summary>
    public partial class ScreenLog : Window
    {
        public ScreenLog()
        {
            InitializeComponent();
            this.logfield.ItemsSource = Log.ListaEventos; 
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.L) this.Close();
        }

        private void btnlogclear_Click(object sender, RoutedEventArgs e)
        {
            Log.ListaEventos.Clear();
        }
    }
}
