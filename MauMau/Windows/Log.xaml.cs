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

namespace MauMau.Windows
{
    /// <summary>
    /// Interaction logic for Log.xaml
    /// </summary>
    public partial class Log : Window
    {
        public Log()
        {
            InitializeComponent();           
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {            
            if(e.Key == Key.L)
            {
                this.Close();
            }
        }

        private void TextBlock_Initialized(object sender, EventArgs e)
        {
           
        }
    }
}
