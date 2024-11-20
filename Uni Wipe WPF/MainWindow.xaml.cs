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
using System.Windows.Threading;
using System.Management;


namespace Uni_Wipe_WPF
{
    /// <summary>
    /// Compile By Yashan Namal   This Window Going to The Enter the Main Apllication This is Work As a Splash Screen    2024/ 06
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();




            #region // Task Bar ICO App

            TaskBarIcon.MainWindow task1 = new TaskBarIcon.MainWindow();
            task1.Show();



            #endregion




            // ============================================================
            //  Show In this Window RAM, SYSTEM And Processer 
            //===============================================================

            #region // System Information

            StringBuilder os1 = new StringBuilder();
            StringBuilder pro1 = new StringBuilder();
            StringBuilder ram = new StringBuilder();

            ManagementObjectSearcher osSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");


            foreach (ManagementObject os in osSearcher.Get())
            {

                os1.AppendLine($" {os["Caption"]}");
                label1.Content = os1.ToString();


               


            }


            ManagementObjectSearcher cpuSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

            foreach (ManagementObject cpu in cpuSearcher.Get())
            {


                pro1.AppendLine($" {cpu["Name"]}");
                label3.Content = pro1.ToString();



            }




            ManagementObjectSearcher memSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            foreach (ManagementObject mem in memSearcher.Get())
            {
              
                ram.AppendLine($" {Convert.ToInt64(mem["Capacity"]) / (1024 * 1024)} MB");
              

                label5.Content = ram.ToString();


            }













            #endregion



    





        }


        // ========================================================
        // SYstem Exit and Thread Close " Also Tray Application Close 
        //=========================================================





        #region  // Close Button
        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
            
           
           
        }




        #endregion













        #region // Enter the Form
        private void MBt2_Click(object sender, RoutedEventArgs e)
        {

            // open Windows1 Form and close this
            Window1 window1 = new Window1();
            window1.Show();
            this.Hide();

        }



        #endregion




        //=======================================================
        //  Chainge Button Color and Intract with the Button 
        //=======================================================

        #region // Chaing Button Color And Intractive Button



        private void Button_MouseEnter1(object sender, MouseEventArgs e)
        {
            Mbt1.Foreground = new SolidColorBrush(Colors.LightGreen);          
            Mbt1.FontSize = 22;
            Mbt1.FontStyle = FontStyles.Italic;


         



        }

      

        private void Button_MouseLeave1(object sender, MouseEventArgs e)
        {


            Mbt1.Foreground = new SolidColorBrush(Colors.White);
            Mbt1.FontSize = 18;
            Mbt1.FontStyle = FontStyles.Normal;

         



        }




        private void Button_MouseEnter2(object sender, MouseEventArgs e)
        {
      


            MBt2.Foreground = new SolidColorBrush(Colors.LightGreen);
            MBt2.FontSize = 22;
            MBt2.FontStyle = FontStyles.Italic;




        }









        private void Button_MouseLeave2(object sender, MouseEventArgs e)
        {


            

            MBt2.Foreground = new SolidColorBrush(Colors.White);
            MBt2.FontSize = 18;
            MBt2.FontStyle = FontStyles.Normal;



        }

        private void BtInfo_Click(object sender, RoutedEventArgs e)
        {
            Sysinformation sysinfor = new Sysinformation();
            sysinfor.Show();
           
        }





        #endregion






    }
}






