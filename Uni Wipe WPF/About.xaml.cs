using System;
using System.Collections.Generic;
using System.IO;
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
    /// Compile By Yashan Namal  2024/06 About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
            LoadTextFromFile();

        }






        // ===============================================================
        // Licence   Agrement  Getting From .Text File 
        //================================================================
        
        




        #region //  GNU Licence  


        private void LoadTextFromFile()
        {
            try
            {
                string filePath = "F:/Uni Wipe/Uni Wipe WPF/Uni Wipe WPF/Image/License.txt";
                if (File.Exists(filePath))
                {
                     textBox.Text  = File.ReadAllText(filePath);


                  
                }
                else
                {
                    MessageBox.Show($"The file '{filePath}' does not exist.");
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"An error occurred while reading the file: {ex.Message}");
            }
        }












        #endregion




        #region // Exit Button 

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }















        #endregion

      
    }

}
