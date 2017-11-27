using System.Windows;
using System.Windows.Input;
using MauMau.Classes.Background;
using System;
using MauMau.Classes.Background.Estruturas;

namespace MauMau.Windows
{
    /// <summary>
    /// Interaction logic for ScreenLog.xaml
    /// </summary>
    public partial class ScreenLog : Window
    {
        Enginee eng;
        public ScreenLog(Enginee eng)
        {
            InitializeComponent();
            this.eng = eng;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.logfield.Items.Refresh();
            this.UpdateCount();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.L) this.Close();
        }

        private void btnlogclear_Click(object sender, RoutedEventArgs e)
        {
            Log.ListaEventos.Clear();
        }
        private void UpdateCount()
        {
            Lista<Player> pl = this.eng.GetPlayers();
            this.player1.Content = pl[0].Infos.Name + " : " + pl[0].Hand.Count;
            this.player2.Content = pl[1].Infos.Name + " : " + pl[1].Hand.Count;
            this.player3.Content = pl[2].Infos.Name + " : " + pl[2].Hand.Count;
            this.player4.Content = pl[3].Infos.Name + " : " + pl[3].Hand.Count;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.logfield.ItemsSource = Log.ListaEventos;
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            this.UpdateCount();
        }
    }
}
