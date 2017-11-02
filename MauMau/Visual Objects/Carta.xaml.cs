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

namespace MauMau.Visual_Objects
{
    /// <summary>
    /// Interaction logic for Carta.xaml
    /// </summary>
    public partial class Carta : UserControl
    {
        public Carta()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValorProperty = DependencyProperty.Register("Valor", typeof(object), typeof(Carta));
        public static readonly DependencyProperty FrontImageProperty = DependencyProperty.Register("FrontImage", typeof(BitmapImage), typeof(Carta));
        public static readonly DependencyProperty BackImageProperty = DependencyProperty.Register("BackImage", typeof(BitmapImage), typeof(Carta));
        public static readonly DependencyProperty IsToFollowCursorProperty = DependencyProperty.Register("IsToFollowCursor", typeof(bool), typeof(Carta));
        public static readonly DependencyProperty DragRelativeToProperty = DependencyProperty.Register("DragRelativeTo", typeof(FrameworkElement), typeof(Carta));
        public static readonly DependencyProperty IsToMoveOnMouseOverProperty = DependencyProperty.Register("IsToMoveOnMouseOver", typeof(bool), typeof(Carta));
        public static readonly DependencyProperty MoveToOffsetProperty = DependencyProperty.Register("MoveToOffset", typeof(Point), typeof(Carta));
        public static readonly DependencyProperty ShowBackProperty = DependencyProperty.Register("ShowBack", typeof(bool), typeof(Carta));

        public object Valor
        {
            get { return (object)GetValue(ValorProperty); }
            set { SetValue(ValorProperty, value); }
        }

        public BitmapImage FrontImage
        {
            get { return (BitmapImage)GetValue(FrontImageProperty); }
            set { SetValue(FrontImageProperty, value); }
        }
        public BitmapImage BackImage
        {
            get { return (BitmapImage)GetValue(BackImageProperty); }
            set { SetValue(BackImageProperty, value); }
        }

        public bool IsToFollowCursor
        {
            get { return (bool)GetValue(IsToFollowCursorProperty); }
            set { SetValue(IsToFollowCursorProperty, value); }
        }

        public FrameworkElement DragRelativeTo
        {
            get { return (FrameworkElement)GetValue(DragRelativeToProperty); }
            set { SetValue(DragRelativeToProperty, value); }
        }

        public bool IsToMoveOnMouseOver
        {
            get { return (bool)GetValue(IsToMoveOnMouseOverProperty); }
            set { SetValue(IsToMoveOnMouseOverProperty, value); }
        }
        public Point MoveToOffset
        {
            get { return (Point)GetValue(MoveToOffsetProperty); }
            set { SetValue(MoveToOffsetProperty, value); }
        }

        public bool ShowBack
        {
            get { return (bool)GetValue(ShowBackProperty); }
            set { SetValue(ShowBackProperty, value); }
        }
    }
}
