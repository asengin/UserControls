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

namespace UserControls
{
    /// <summary>
    /// Логика взаимодействия для ColorChanger.xaml
    /// </summary>
    public partial class ColorChanger : UserControl
    {
        public static DependencyProperty ColorProperty;
        public static DependencyProperty RedProperty;
        public static DependencyProperty GreenProperty;
        public static DependencyProperty BlueProperty;
        private static readonly RoutedEvent ColorChangedEvent;

        static ColorChanger()
        {
            ColorProperty = DependencyProperty.Register(
                nameof(Color),
                typeof(Color),
                typeof(ColorChanger),
                new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorChanged)));
            RedProperty = DependencyProperty.Register(
                nameof(Red),
                typeof(byte),
                typeof(ColorChanger),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            GreenProperty = DependencyProperty.Register(
                nameof(Green),
                typeof(byte),
                typeof(ColorChanger),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            BlueProperty = DependencyProperty.Register(
                nameof(Blue),
                typeof(byte),
                typeof(ColorChanger),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));

            ColorChangedEvent = EventManager.RegisterRoutedEvent(
                "ColorChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<Color>),
                typeof(ColorChanger));
        }

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }
        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }
        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }

        public ColorChanger()
        {
            InitializeComponent();
        }

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorChanger colorChange = (ColorChanger)d;
            Color oldColor = (Color)e.OldValue;
            Color newColor = (Color)e.NewValue;
            colorChange.Red = newColor.R;
            colorChange.Green = newColor.G;
            colorChange.Blue = newColor.B;

        }

        private static void OnColorRGBChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorChanger colorChange = (ColorChanger)d;
            Color color = colorChange.Color;
            if (e.Property == RedProperty)
                color.R = (byte)e.NewValue;
            else if (e.Property == GreenProperty)
                color.G = (byte)e.NewValue;
            else if (e.Property == BlueProperty)
                color.B = (byte)e.NewValue;
            colorChange.Color = color;
        }

        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }
    }
}
