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

namespace Uni_Wipe_WPF
{
    /// <summary>
    /// Interaction logic for Skin.xaml
    /// </summary>
    public partial class Skin : Window
    {
        public Skin()
        {
            InitializeComponent();





        }

         public Slider slider1 = new Slider();


        private void sliderg_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            SolidColorBrush magicBrush = (SolidColorBrush)this.Resources["magicBrush"];

            if ((sliderr != null) && (sliderg != null) && (sliderb != null))
            {
                magicBrush.Color = Color.FromRgb((byte)sliderr.Value, (byte)sliderg.Value, (byte)sliderb.Value);
            }



        }











    
}
}
