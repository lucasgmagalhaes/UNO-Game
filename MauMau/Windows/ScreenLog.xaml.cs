using System.Windows;
using System.Windows.Input;
using MauMau.Classes.Background;
using System;

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
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.logfield.Items.Refresh();
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
