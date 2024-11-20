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
using System.Globalization;

namespace Uni_Wipe_WPF
{
    /// <summary>
    /// Compile By Yashan Namal 2024/07 Sysinformation.xaml
    /// </summary>
    public partial class Sysinformation : Window
    {


        CultureInfo installedUICulture = CultureInfo.InstalledUICulture;
        string osVersion = Environment.OSVersion.ToString();








        public Sysinformation()
        {
            InitializeComponent();

















            //=====================================================
            // If close for OS
            //=====================================================


            #region // Get OS  

            string osName = GetOSName();

            label9.Content = osName;


            label12.Visibility = Visibility.Hidden;
            label15.Visibility = Visibility.Hidden;


            if (osName == "Unix")
            {
                label15.Visibility = Visibility.Visible;
            }
            else if (osName == "Xbox")
            {
                label15.Visibility = Visibility.Visible;
            }
            else if (osName == "Win32NT")
            {
                label15.Visibility = Visibility.Visible;
            }
            else if (osName == "MacOSX")
            {
                label15.Visibility = Visibility.Visible;

            }
            else if (osName == "WinCE")
            {
                label12.Visibility = Visibility.Visible;
            }
            else
            {
                osName = "Win32Windows";
                label12.Visibility = Visibility.Visible;
            }




            #endregion

            //========================================================





        }






        // =============================================================
        // End Button   
        //==============================================================




        #region// Button Close




        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }



        #endregion


        //===============================================================


        
         //==============================================================
        //OS information
        //=================================================================



        #region // Get Information


        private string GetOSName()
        {
            OperatingSystem os = Environment.OSVersion;
            string osName = "Unknown";

            switch (os.Platform)
            {
                case PlatformID.Win32NT:
                    if (os.Version.Major == 10)
                        osName = "Windows 10";
                    else if (os.Version.Major == 6)
                    {
                        if (os.Version.Minor == 3)
                            osName = "Windows 8.1";
                        else if (os.Version.Minor == 2)
                            osName = "Windows 8";
                        else if (os.Version.Minor == 1)
                            osName = "Windows 7";
                        else if (os.Version.Minor == 0)
                            osName = "Windows Vista";
                    }
                    else if (os.Version.Major == 5)
                    {
                        if (os.Version.Minor == 2)
                            osName = "Windows Server 2003";
                        else if (os.Version.Minor == 1)
                            osName = "Windows XP";
                        else if (os.Version.Minor == 0)
                            osName = "Windows 2000";
                    }
                    break;
                    // Add other platforms if needed
            }

            return osName;
        }



        #endregion


        //==================================================================



    }
}
