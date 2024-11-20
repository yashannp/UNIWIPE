using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Windows.Threading;
using System.Management;
using System.Windows.Navigation;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Animation;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Threading;
using Microsoft.Win32;


namespace Uni_Wipe_WPF
{
    /// <summary>
    /// Compile By Yashan Namal 2024/06 Window1.xaml  
    /// </summary>
    public partial class Window1 : Window
    {




        #region // Parameters

        public bool isTotalWipe = false;
        public int numberPass = 0;
        public int passed = 0;
        public string diskPath = "";
        public string diskLetter = "";
        public long space = 0;
        public bool isOsDisk = false;
        public bool _canWrite = true;
        public DateTime currentDateTime;
        public DateTime startDateTime;
        public string path = "";
        public bool reboot = false;

        #endregion









        #region  // Form Move 



        public bool mouseDown;
        public Point lastLocation;



        #endregion




        private DispatcherTimer _timer;



        private Dictionary<int, HDD> hddDrives = new Dictionary<int, HDD>();





        public Window1()
        {


            InitializeComponent();
            InitializeTimer();


            LoadDrives();


            SetupDriveWatchers();

            LoadHddData();

            InitializeDriveInfo(textBlock_HDDinfor);






            #region  // Combo Box getting to zero 



            ComboSystem.SelectedIndex = 0;
            Passess.SelectedIndex = 0;


            #endregion




            #region // load Driver to the Drive list Combo box


            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo info in drives)
            {

                if (info.IsReady && info.DriveType != DriveType.Network && info.DriveType != DriveType.NoRootDirectory && info.DriveType != DriveType.Ram)
                {
                    DriveList.Items.Add(info.Name);


                }
            }




            if (DriveList.Items != null)
            {

                DriveList.SelectedIndex = 0;
            }



















            #endregion

























            #region // Startup Information






            StringBuilder sbos = new StringBuilder();



            ManagementObjectSearcher osSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");


            foreach (ManagementObject os in osSearcher.Get())
            {

                sbos.AppendLine("Operating System Information:");
                sbos.AppendLine($"Name: {os["Caption"]}");
                sbos.AppendLine($"Version: {os["Version"]}");
                sbos.AppendLine($"Manufacturer: {os["Manufacturer"]}");
                sbos.AppendLine($"Computer Name: {os["CSName"]}");
                sbos.AppendLine($"Last Boot Up Time: {os["LastBootUpTime"]}");


                Sys_TextBlock.Text = sbos.ToString();

            }


            StringBuilder sbcpu = new StringBuilder();

            // Retrieve Processor information
            ManagementObjectSearcher cpuSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (ManagementObject cpu in cpuSearcher.Get())
            {
                sbcpu.AppendLine("Processor Information:");
                sbcpu.AppendLine($"Name: {cpu["Name"]}");
                sbcpu.AppendLine($"Manufacturer: {cpu["Manufacturer"]}");
                sbcpu.AppendLine($"Description: {cpu["Description"]}");
                sbcpu.AppendLine($"Number Of Cores: {cpu["NumberOfCores"]}");

                CPU_TextBlock.Text = sbcpu.ToString();

            }



            StringBuilder sbRAM = new StringBuilder();

            // Retrieve Memory information
            ManagementObjectSearcher memSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            foreach (ManagementObject mem in memSearcher.Get())
            {
                sbRAM.AppendLine("Memory Information:");
                sbRAM.AppendLine($"Capacity: {Convert.ToInt64(mem["Capacity"]) / (1024 * 1024)} MB");
                sbRAM.AppendLine($"Speed: {mem["Speed"]} MHz");
                sbRAM.AppendLine($"Manufacturer: {mem["Manufacturer"]}");
                sbRAM.AppendLine($"Speed: {mem["Speed"]} MHz");
                sbRAM.AppendLine($"Manufacturer: {mem["Manufacturer"]}");
                sbRAM.AppendLine($"Serial Number: {mem["SerialNumber"]}");
                sbRAM.AppendLine($"Part Number: {mem["PartNumber"]}");
                sbRAM.AppendLine($"Form Factor: {mem["FormFactor"]}");
                sbRAM.AppendLine($"Memory Type: {mem["MemoryType"]}");
                sbRAM.AppendLine($"Configured Voltage: {mem["ConfiguredVoltage"]}");
                sbRAM.AppendLine($"Data Width: {mem["DataWidth"]}");
                sbRAM.AppendLine($"Total Width: {mem["TotalWidth"]}");
                sbRAM.AppendLine($"Tag: {mem["Tag"]}");
                sbRAM.AppendLine($"Position In Row: {mem["PositionInRow"]}");
                sbRAM.AppendLine($"Bank Label: {mem["BankLabel"]}");
                sbRAM.AppendLine($"Configured Clock Speed: {mem["ConfiguredClockSpeed"]} MHz");
                sbRAM.AppendLine($"Creation Class Name: {mem["CreationClassName"]}");
                sbRAM.AppendLine($"Device Locator: {mem["DeviceLocator"]}");
                sbRAM.AppendLine($"SMBIOS Memory Type: {mem["SMBIOSMemoryType"]}");
                sbRAM.AppendLine($"Type Detail: {mem["TypeDetail"]}");

                RAM_TextBlock.Text = sbRAM.ToString();



            }



            StringBuilder VGA = new StringBuilder();


            ManagementObjectSearcher vgaSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            foreach (ManagementObject vga in vgaSearcher.Get())
            {
                VGA.AppendLine("VGA Information:");
                VGA.AppendLine($"Name: {vga["Name"]}");
                VGA.AppendLine($"Description: {vga["Description"]}");
                VGA.AppendLine($"Video Processor: {vga["VideoProcessor"]}");
                VGA.AppendLine($"Adapter RAM: {Convert.ToInt64(vga["AdapterRAM"]) / (1024 * 1024)} MB");
                VGA.AppendLine($"Driver Version: {vga["DriverVersion"]}");
                VGA.AppendLine($"Status: {vga["Status"]}");
                VGA.AppendLine($"Video Mode Description: {vga["VideoModeDescription"]}");
                VGA.AppendLine($"Current Refresh Rate: {vga["CurrentRefreshRate"]} Hz");

                VGA_TextBlock.Text = VGA.ToString();








            }









            StringBuilder HDD = new StringBuilder();

            ManagementObjectSearcher diskSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject disk in diskSearcher.Get())
            {
                HDD.AppendLine("Disk Drive Information:");
                HDD.AppendLine($"Model: {disk["Model"]}");
                HDD.AppendLine($"Interface Type: {disk["InterfaceType"]}");
                HDD.AppendLine($"Serial Number: {disk["SerialNumber"]}");
                HDD.AppendLine($"Size: {Convert.ToInt64(disk["Size"]) / (1024 * 1024 * 1024)} GB");

                HDD_TextBlock.Text = HDD.ToString();




            }





            #region           This is the Data Show in the textbox other Shows 




            StringBuilder systemInfo = new StringBuilder();

            ManagementObjectSearcher cpuSearcher1 = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

            foreach (ManagementObject cpu in cpuSearcher.Get())
            {
                systemInfo.AppendLine("CPU Information:");
                systemInfo.AppendLine($"Name: {cpu["Name"]}");
                systemInfo.AppendLine($"Manufacturer: {cpu["Manufacturer"]}");
                systemInfo.AppendLine($"Max Clock Speed: {cpu["MaxClockSpeed"]} MHz");
                systemInfo.AppendLine($"Number of Cores: {cpu["NumberOfCores"]}");
                systemInfo.AppendLine($"Number of Logical Processors: {cpu["NumberOfLogicalProcessors"]}");
                systemInfo.AppendLine($"Processor ID: {cpu["ProcessorId"]}");
                systemInfo.AppendLine($"Socket Designation: {cpu["SocketDesignation"]}");
                systemInfo.AppendLine($"L2 Cache Size: {cpu["L2CacheSize"]} KB");
                systemInfo.AppendLine($"L3 Cache Size: {cpu["L3CacheSize"]} KB");
                systemInfo.AppendLine("--------------------------");
                systemInfo.AppendLine("");
                systemInfo.AppendLine("");


            }




            ManagementObjectSearcher batterySearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Battery");

            foreach (ManagementObject battery in batterySearcher.Get())
            {
                systemInfo.AppendLine("Battery Information:");
                systemInfo.AppendLine($"Full Charge Capacity: {battery["FullChargeCapacity"]} mWh");
                systemInfo.AppendLine($"Design Capacity: {battery["DesignCapacity"]} mWh");
                systemInfo.AppendLine($"Battery Status: {battery["BatteryStatus"]}");
                systemInfo.AppendLine($"Estimated Charge Remaining: {battery["EstimatedChargeRemaining"]}%");
                systemInfo.AppendLine($"Estimated Run Time: {battery["EstimatedRunTime"]} minutes");
                systemInfo.AppendLine($"Expected Life: {battery["ExpectedLife"]} minutes");
                systemInfo.AppendLine($"Max Recharge Time: {battery["MaxRechargeTime"]} minutes");
                systemInfo.AppendLine($"Time on Battery: {battery["TimeOnBattery"]} minutes");
                systemInfo.AppendLine($"Time to Full Charge: {battery["TimeToFullCharge"]} minutes");
                systemInfo.AppendLine("--------------------------");
                systemInfo.AppendLine("");
                systemInfo.AppendLine("");


            }







            ManagementObjectSearcher biosSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");

            foreach (ManagementObject bios in biosSearcher.Get())
            {
                systemInfo.AppendLine("BIOS Information:");
                systemInfo.AppendLine($"Caption: {bios["Caption"]}");
                systemInfo.AppendLine($"Description: {bios["Description"]}");
                systemInfo.AppendLine($"Manufacturer: {bios["Manufacturer"]}");
                systemInfo.AppendLine($"Name: {bios["Name"]}");
                systemInfo.AppendLine($"Release Date: {bios["ReleaseDate"]}");
                systemInfo.AppendLine($"Serial Number: {bios["SerialNumber"]}");
                systemInfo.AppendLine($"Software Element ID: {bios["SoftwareElementID"]}");
                systemInfo.AppendLine($"Version: {bios["Version"]}");
                systemInfo.AppendLine("--------------------------");
                systemInfo.AppendLine("");
                systemInfo.AppendLine("");


            }





            ManagementObjectSearcher systemSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystemProduct");

            foreach (ManagementObject system in systemSearcher.Get())
            {
                systemInfo.AppendLine("Computer System Information:");
                systemInfo.AppendLine($"Caption: {system["Caption"]}");
                systemInfo.AppendLine($"Description: {system["Description"]}");
                systemInfo.AppendLine($"Identifying Number: {system["IdentifyingNumber"]}");
                systemInfo.AppendLine($"Name: {system["Name"]}");
                systemInfo.AppendLine($"SKU Number: {system["SKUNumber"]}");
                systemInfo.AppendLine($"UUID: {system["UUID"]}");
                systemInfo.AppendLine($"Vendor: {system["Vendor"]}");
                systemInfo.AppendLine($"Version: {system["Version"]}");
                systemInfo.AppendLine("--------------------------");
                systemInfo.AppendLine("");
                systemInfo.AppendLine("");


            }




            ManagementObjectSearcher motherboardSearcher = new ManagementObjectSearcher("SELECT Manufacturer, Product, SerialNumber FROM Win32_BaseBoard");

            foreach (ManagementObject motherboard in motherboardSearcher.Get())
            {
                systemInfo.AppendLine("Motherboard Information:");
                systemInfo.AppendLine($"Manufacturer: {motherboard["Manufacturer"]}");
                systemInfo.AppendLine($"Product: {motherboard["Product"]}");
                systemInfo.AppendLine($"Serial Number: {motherboard["SerialNumber"]}");
                systemInfo.AppendLine("--------------------------");
                systemInfo.AppendLine("");
                systemInfo.AppendLine("");




            }



            ManagementObjectSearcher mouseSearcher = new ManagementObjectSearcher("SELECT Caption, Description, Manufacturer, Name, NumberOfButtons FROM Win32_PointingDevice");

            foreach (ManagementObject mouse in mouseSearcher.Get())
            {
                systemInfo.AppendLine("Mouse Information:");
                systemInfo.AppendLine($"Caption: {mouse["Caption"]}");
                systemInfo.AppendLine($"Description: {mouse["Description"]}");
                systemInfo.AppendLine($"Manufacturer: {mouse["Manufacturer"]}");
                systemInfo.AppendLine($"Name: {mouse["Name"]}");
                systemInfo.AppendLine($"Number of Buttons: {mouse["NumberOfButtons"]}");
                systemInfo.AppendLine("--------------------------/");
                systemInfo.AppendLine("");
                systemInfo.AppendLine("");


            }







            ManagementObjectSearcher videoControllerSearcher = new ManagementObjectSearcher("SELECT AdapterCompatibility, AdapterRAM, Caption, CurrentBitsPerPixel, CurrentHorizontalResolution, CurrentNumberOfColors, CurrentRefreshRate, CurrentVerticalResolution, Description, DriverDate, DriverVersion, MaxRefreshRate, MinRefreshRate, Name, PNPDeviceID, VideoModeDescription, VideoProcessor FROM Win32_VideoController");

            foreach (ManagementObject videoController in videoControllerSearcher.Get())
            {
                systemInfo.AppendLine("Video Controller Information:");
                systemInfo.AppendLine($"Manufacturer: {videoController["AdapterCompatibility"]}");
                systemInfo.AppendLine($"Adapter RAM: {Convert.ToUInt64(videoController["AdapterRAM"]) / (1024 * 1024)} MB");
                systemInfo.AppendLine($"Caption: {videoController["Caption"]}");
                systemInfo.AppendLine($"Current Bits Per Pixel: {videoController["CurrentBitsPerPixel"]}");
                systemInfo.AppendLine($"Current Horizontal Resolution: {videoController["CurrentHorizontalResolution"]}");
                systemInfo.AppendLine($"Current Number of Colors: {videoController["CurrentNumberOfColors"]}");
                systemInfo.AppendLine($"Current Refresh Rate: {videoController["CurrentRefreshRate"]} Hz");
                systemInfo.AppendLine($"Current Vertical Resolution: {videoController["CurrentVerticalResolution"]}");
                systemInfo.AppendLine($"Description: {videoController["Description"]}");
                systemInfo.AppendLine($"Driver Date: {videoController["DriverDate"]}");
                systemInfo.AppendLine($"Driver Version: {videoController["DriverVersion"]}");
                systemInfo.AppendLine($"Max Refresh Rate: {videoController["MaxRefreshRate"]} Hz");
                systemInfo.AppendLine($"Min Refresh Rate: {videoController["MinRefreshRate"]} Hz");
                systemInfo.AppendLine($"Name: {videoController["Name"]}");
                systemInfo.AppendLine($"Video Mode Description: {videoController["VideoModeDescription"]}");
                systemInfo.AppendLine($"Video Processor: {videoController["VideoProcessor"]}");
                systemInfo.AppendLine("--------------------------");
                systemInfo.AppendLine("");
                systemInfo.AppendLine("");

            }


            ManagementObjectSearcher ramSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            foreach (ManagementObject ram in ramSearcher.Get())
            {
                systemInfo.AppendLine("RAM Information:");
                systemInfo.AppendLine($"Capacity: {Convert.ToInt64(ram["Capacity"]) / (1024 * 1024 * 1024)} GB");
                systemInfo.AppendLine($"Speed: {ram["Speed"]} MHz");
                systemInfo.AppendLine($"Manufacturer: {ram["Manufacturer"]}");
                systemInfo.AppendLine($"Part Number: {ram["PartNumber"]}");
                systemInfo.AppendLine($"Status: {ram["Status"]}");
                systemInfo.AppendLine("--------------------------");
                systemInfo.AppendLine("");
                systemInfo.AppendLine("");
            }




            // Add Hard Drive details
            ManagementObjectSearcher driveSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject drive in driveSearcher.Get())
            {
                systemInfo.AppendLine("Hard Drive Information:");
                systemInfo.AppendLine($"Model: {drive["Model"]}");
                systemInfo.AppendLine($"Interface Type: {drive["InterfaceType"]}");
                systemInfo.AppendLine($"Size: {Convert.ToInt64(drive["Size"]) / (1024 * 1024 * 1024)} GB");
                systemInfo.AppendLine($"Serial Number: {drive["SerialNumber"]}");
                systemInfo.AppendLine($"Status: {drive["Status"]}");
                systemInfo.AppendLine("--------------------------");
                systemInfo.AppendLine("");
                systemInfo.AppendLine("");
            }







            // Add OS details
            ManagementObjectSearcher osSearcherblock = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject os in osSearcher.Get())
            {
                systemInfo.AppendLine("Operating System Information:");
                systemInfo.AppendLine($"Name: {os["Caption"]}");
                systemInfo.AppendLine($"Version: {os["Version"]}");
                systemInfo.AppendLine($"Architecture: {os["OSArchitecture"]}");
                systemInfo.AppendLine($"Last Boot Up Time: {os["LastBootUpTime"]}");
                systemInfo.AppendLine($"Serial Number: {os["SerialNumber"]}");
                systemInfo.AppendLine("--------------------------");
                systemInfo.AppendLine("");
                systemInfo.AppendLine("");
            }





            // Add Network Adapter details
            ManagementObjectSearcher networkSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionStatus=2");
            foreach (ManagementObject adapter in networkSearcher.Get())
            {
                systemInfo.AppendLine("Network Adapter Information:");
                systemInfo.AppendLine($"Name: {adapter["Name"]}");
                systemInfo.AppendLine($"MAC Address: {adapter["MACAddress"]}");
                systemInfo.AppendLine($"Speed: {Convert.ToInt64(adapter["Speed"]) / (1024 * 1024)} Mbps");
                systemInfo.AppendLine($"Status: {adapter["Status"]}");
                systemInfo.AppendLine("--------------------------");
                systemInfo.AppendLine("");
                systemInfo.AppendLine("");
            }



            // Add Sound Device details
            ManagementObjectSearcher soundSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_SoundDevice");
            foreach (ManagementObject soundDevice in soundSearcher.Get())
            {
                systemInfo.AppendLine("Sound Device Information:");
                systemInfo.AppendLine($"Name: {soundDevice["Name"]}");
                systemInfo.AppendLine($"Manufacturer: {soundDevice["Manufacturer"]}");
                systemInfo.AppendLine($"Status: {soundDevice["Status"]}");
                systemInfo.AppendLine("--------------------------");
                systemInfo.AppendLine("");
                systemInfo.AppendLine("");
            }






            // Display All information in a TextBlock
            txtblock_other.Text = systemInfo.ToString();


            


            #endregion  End Of the Text Box  other Show






            #endregion







            #region    // Form Move


            this.MouseDown += Window_MouseDown;
            this.MouseMove += Window_MouseMove;
            this.MouseUp += Window_MouseUp;

            #endregion







        }







        #region // Menu Item





        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
            System.Windows.Application.Current.Shutdown();



        }






        #endregion





        #region // Form Move

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                mouseDown = true;
                lastLocation = e.GetPosition(this);
                this.CaptureMouse();
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Point currentPosition = e.GetPosition(this);
                this.Left += currentPosition.X - lastLocation.X;
                this.Top += currentPosition.Y - lastLocation.Y;
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                mouseDown = false;
                this.ReleaseMouseCapture();
            }
        }



        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.Application.Current.Shutdown();
            // this.Close();

            Environment.Exit(0);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            System.Windows.Application.Current.Shutdown();



        }

        private void Bt_Maxi_Click(object sender, RoutedEventArgs e)
        {

            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }

        }

        private void Bt_mini_Click(object sender, RoutedEventArgs e)
        {
            //  this.WindowState = WindowState.Minimized;


            this.Visibility = Visibility.Hidden;
            this.ShowInTaskbar = false;











        }




        #endregion




        #region // Timer Status Bar

        private void Timer_Tick(object sender, EventArgs e)
        {


            St_Lbt.Content = DateTime.Now.ToString("HH:mm:ss");



        }


        private void InitializeTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };


            _timer.Tick += Timer_Tick;
            _timer.Start();


        }




        #endregion










        #region // Mouse Close and Mini Max Button



        private void Button_MouseEnterclose(object sender, MouseEventArgs e)
        {



            Image imclose1 = new Image();
            imclose1.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Image/close2.jpg"));

            Button_Exit.Content = imclose1;



        }





        private void Button_MouseLeaveclose(object sender, System.Windows.Input.MouseEventArgs e)
        {



            Image imclose2 = new Image();
            imclose2.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Image/close1.jpg"));

            Button_Exit.Content = imclose2;




        }





        private void Button_MouseEnterMaxi(object sender, MouseEventArgs e)
        {



            Image imMaxi1 = new Image();
            imMaxi1.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Image/max2.jpg"));

            Bt_Maxi.Content = imMaxi1;



        }





        private void Button_MouseLeaveMaxi(object sender, MouseEventArgs e)
        {



            Image imMaxi1 = new Image();
            imMaxi1.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Image/max1.jpg"));

            Bt_Maxi.Content = imMaxi1;




        }







        private void Button_MouseEnterMini(object sender, MouseEventArgs e)
        {



            Image imMini1 = new Image();
            imMini1.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Image/mini2.jpg"));

            Bt_mini.Content = imMini1;



        }





        private void Button_MouseLeaveMini(object sender, MouseEventArgs e)
        {



            Image imMini2 = new Image();
            imMini2.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Image/mini1.jpg"));

            Bt_mini.Content = imMini2;




        }











































        #endregion







        #region // Show about Dialog





        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            About about1 = new About();
            about1.Show();
        }





        #endregion















        #region  // Format  Working




        #region // Format Button 

        private async void Button_Click(object sender, RoutedEventArgs e)
        {



            #region Main


            var msgB = MessageBox.Show("All Data Destroy Continue", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            if (msgB == MessageBoxResult.Yes)
            {

                #region Init

                #region UI
                ComboSystem.IsEnabled = false;
                Passess.IsEnabled = false;
                DriveList.IsEnabled = false;
                ProgressBar1.Value = 0;
                percentMainBar.Visibility = Visibility.Visible;
                sinceLabel.Visibility = Visibility.Visible;
                #endregion






                #region Letter
                diskPath = DriveList.SelectedItem.ToString();
                diskLetter = diskPath.Replace(@":\", "");
                #endregion



                #region Format
                DriveInfo getInfo = new DriveInfo(diskLetter);
                switch (ComboSystem.SelectedIndex)
                {
                    case 0:
                        isTotalWipe = false;
                        space = getInfo.AvailableFreeSpace;
                        break;
                    case 1:
                        isTotalWipe = true;
                        space = getInfo.TotalSize;
                        break;
                }
                #endregion




                #region passes
                switch (Passess.SelectedIndex)
                {
                    case 0:
                        numberPass = 1;
                        break;
                    case 1:
                        numberPass = 2;
                        break;
                    case 2:
                        numberPass = 3;
                        break;
                    case 3:
                        numberPass = 4;
                        break;
                }
                #endregion
                #endregion

                //   try
                //  {
                // UI Lock
                //    LockUI(true);



                if (mainButton.Content.ToString() == "Format")
                {
                    mainButton.Content = "Cancel";



                    #region Actions
                    if (isTotalWipe)
                    {

                        #region Format

                        #region FileSystem
                        string fileSystem = "";
                        if (space > 2000000000)
                        {
                            fileSystem = "NTFS";
                        }
                        else
                        {
                            fileSystem = "FAT32";
                        }
                        #endregion
                        sinceLabel.Visibility = Visibility.Hidden;
                        statutLabel.Text = $@"Statut : Formating {diskPath}";
                        ProgressBar1.IsIndeterminate = true;
                        await Task.Run(() => format(fileSystem, "UNIWIPE"));
                        ProgressBar1.IsIndeterminate = false;
                        #endregion

                        #region Writer
                        _canWrite = true;

                        percentMainBar.Visibility = Visibility.Visible;
                        startDateTime = DateTime.Now;
                        sinceLabel.Visibility = Visibility.Visible;
                        await Task.Run(() => overwrite());

                        #endregion
                    }
                    else
                    {

                        #region Writer
                        _canWrite = true;

                        percentMainBar.Visibility = Visibility.Visible;
                        startDateTime = DateTime.Now;
                        sinceLabel.Visibility = Visibility.Visible;
                        await Task.Run(() => overwrite());

                        #endregion
                    }


                    #endregion

                }
                else if (mainButton.Content.ToString() == "Cancel")
                {
                    reboot = true;
                    mainButton.Content = "Format";
                    _canWrite = false;


                }


            }

        }


             //   catch (Exception ex)
             //   {
              //      MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
             //   }
             //   finally
              //  {
                    // UI Unlock
               //     LockUI(false);
              //  }












            

        



// Drive lock Future Divolopment 

   //     public void LockUI(bool isLocked)
    //    {
      //      ComboSystem.IsEnabled = !isLocked;
    //        Passess.IsEnabled = !isLocked;
     //       DriveList.IsEnabled = !isLocked;
   //         mainButton.IsEnabled = !isLocked;
    //        ProgressBar1.IsIndeterminate = isLocked;
   //     }


















    #endregion






    #region Fonctions
    #region format
    public async void format(string fileSystem, string diskName)
        {
            DriveInfo info = new DriveInfo(diskPath);
            if (info.IsReady)
            {
                string path = $"{diskPath}format.cmd";

                #region delete
                if (File.Exists(path))
                    File.Delete(path);
                #endregion

                #region write
                using (StreamWriter writer = File.CreateText(path))
                {
                    writer.WriteLine($@"FORMAT {diskLetter}: /Y /FS:{fileSystem} /V:{diskName} /Q");
                    writer.Close();
                }
                #endregion


                #region process
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = path;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.Start();
                    process.WaitForExit();
                }
                #endregion

            }
            await Task.Delay(1);
        }
        #endregion




        public async void overwrite()
        {
            for (int x = 0; x < numberPass; x++)
            {
                passed++;

                string fileName = RandomString(8);
                path = diskPath + fileName;


                #region Writer


                #region DefVar
                int _update = 0;
                const int blockSize = 1024 * 8;
                const int blocksPerMb = (1024 * 1024) / blockSize;
                byte[] data = new byte[blockSize];
                Random rng = new Random();
                #endregion

                using (FileStream stream = File.OpenWrite(path))
                {
                    try
                    {
                        for (int i = 0; i < (space * blocksPerMb); i++)
                        {

                            #region Write&Update
                            if (_canWrite)
                            {
                                rng.NextBytes(data);
                                stream.Write(data, 0, data.Length);

                                #region Update UI
                                if (_update == 3500)
                                {
                                    _update = 0;
                                    this.Dispatcher.Invoke(() =>
                                    {
                                        long size;
                                        if (passed != 0)
                                        {
                                            size = GetFileSizeOnDisk(path) / 1024 / 1024 * passed;
                                        }
                                        else
                                        {
                                            size = GetFileSizeOnDisk(path) / 1024 / 1024;
                                        }
                                        long totalSpace = ((long)space / (long)1024 / (long)1024) * numberPass;
                                        long percent = getPercent(size, totalSpace);
                                        ProgressBar1.Value = percent;
                                        currentDateTime = DateTime.Now;
                                        TimeSpan ts = (startDateTime - currentDateTime);
                                        FileInfo fileInfo = new FileInfo(path);
                                        statutLabel.Text = $@"Statut : Wipping the disk {size}MB/{space / 1024 / 1024 * passed}MB {ProgressBar1.Value}%{Environment.NewLine}Step {passed}/{numberPass}";
                                        sinceLabel.Text = $"Started since {ts.ToString(@"hh\:mm\:ss")}";
                                        percentMainBar.Text = ProgressBar1.Value.ToString() + "%";
                                    });
                                }
                                else
                                {
                                    _update++;
                                }
                                #endregion
                            }
                            else
                            {
                                #region Closing
                                this.Dispatcher.Invoke(() =>
                                {
                                    statutLabel.Text = "Statut : Closing...";
                                    mainButton.IsEnabled = false;
                                    percentMainBar.Visibility = Visibility.Hidden;
                                    sinceLabel.Visibility = Visibility.Hidden;
                                    ProgressBar1.IsIndeterminate = true;

                                });

                                stream.Close();

                            #region Delete
                            del:;
                                try
                                {
                                    File.Delete(path);

                                }
                                catch
                                {
                                    goto del;
                                }
                                Environment.Exit(0);
                            }
                            #endregion

                            if (reboot)
                            {
                                this.Dispatcher.Invoke(() =>
                                {
                                    this.Visibility = Visibility.Hidden;
                                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                                });

                            }
                            #endregion
                            #endregion

                        }
                        stream.Close();
                    }
                    catch (System.IO.IOException)
                    {

                        #region Done
                        #region UpdateUI
                        this.Dispatcher.Invoke(() =>
                        {
                            long size = GetFileSizeOnDisk(path) / 1024 / 1024;
                            long totalSpace = ((long)space / (long)1024 / (long)1024) * numberPass;
                            long percent = getPercent(size, totalSpace);
                            ProgressBar1.Value = percent;
                            currentDateTime = DateTime.Now;
                            TimeSpan ts = (startDateTime - currentDateTime);
                            FileInfo fileInfo = new FileInfo(path);
                            statutLabel.Text = $@"Statut : Formatting the disk {size}MB/{space / 1024 / 1024}MB {ProgressBar1.Value}%{Environment.NewLine}Step {passed}/{numberPass}";
                            sinceLabel.Text = $"Started since {ts.ToString(@"hh\:mm\:ss")}";
                            percentMainBar.Text = ProgressBar1.Value.ToString() + "%";
                        });
                        #endregion



                        this.Dispatcher.Invoke(() =>
                        {
                            ComboSystem.IsEnabled = false;
                            Passess.IsEnabled = false;
                            DriveList.IsEnabled = false;
                            ProgressBar1.Value = 0;
                            percentMainBar.Visibility = Visibility.Hidden;
                            sinceLabel.Visibility = Visibility.Hidden;
                            ProgressBar1.IsIndeterminate = true;
                            mainButton.IsEnabled = false;
                            statutLabel.Text = "Statut : Deleting ...";
                        });

                        stream.Close();
                    #region Delete
                    delete:;
                        try
                        {
                            if (File.Exists(path)) File.Delete(path);
                        }
                        catch
                        {
                            goto delete;
                        }
                        this.Dispatcher.Invoke(() =>
                        {
                            ProgressBar1.IsIndeterminate = false;
                            percentMainBar.Visibility = Visibility.Visible;
                            sinceLabel.Visibility = Visibility.Visible;
                        });
                        #endregion
                        #endregion


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occured !\n{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }







                #endregion
            }

            this.Dispatcher.Invoke(() =>
            {
                ComboSystem.IsEnabled = true;
                Passess.IsEnabled = true;
                DriveList.IsEnabled = true;
                ProgressBar1.IsIndeterminate = false;
                mainButton.IsEnabled = true;
                mainButton.Content = "Format";
                ProgressBar1.Value = 0;
                percentMainBar.Visibility = Visibility.Hidden;
                sinceLabel.Visibility = Visibility.Hidden;
                statutLabel.Text = "Statut : Attention";
                MessageBox.Show("Done !", "Format Complet", MessageBoxButton.OK, MessageBoxImage.Information);
                ProgressBar1.Value = 0;
                passed = 0;
                sinceLabel.Text = "Started since 00:00:00";
            });

            await Task.Delay(1);

        }




        #region percent
        public long getPercent(long x, long y)
        {
            try
            {
                return x * 100 / y;
            }
            catch
            {
                MessageBox.Show($"Not enough space on drive", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return 0;
            }
        }
        #endregion

        #region FileSize
        public static long GetFileSizeOnDisk(string file)
        {
            try
            {
                FileInfo info = new FileInfo(file);
                uint dummy, sectorsPerCluster, bytesPerSector;
                int result = GetDiskFreeSpaceW(info.Directory.Root.FullName, out sectorsPerCluster, out bytesPerSector, out dummy, out dummy);
                if (result == 0) throw new Win32Exception();
                uint clusterSize = sectorsPerCluster * bytesPerSector;
                uint hosize;
                uint losize = GetCompressedFileSizeW(file, out hosize);
                long size;
                size = (long)hosize << 32 | losize;
                return ((size + clusterSize - 1) / clusterSize) * clusterSize;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                return 0;
            }
        }

        [DllImport("kernel32.dll")]
        static extern uint GetCompressedFileSizeW([In, MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
           [Out, MarshalAs(UnmanagedType.U4)] out uint lpFileSizeHigh);

        [DllImport("kernel32.dll", SetLastError = true, PreserveSig = true)]
        static extern int GetDiskFreeSpaceW([In, MarshalAs(UnmanagedType.LPWStr)] string lpRootPathName,
           out uint lpSectorsPerCluster, out uint lpBytesPerSector, out uint lpNumberOfFreeClusters,
           out uint lpTotalNumberOfClusters);
        #endregion

        #region randomString
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "UNI WIPE ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion

        #endregion






        private void Window_Closing(object sender, CancelEventArgs e)
        {
            statutLabel.Text = "Statut : Closing...";
            mainButton.IsEnabled = false;
            percentMainBar.Visibility = Visibility.Hidden;
            sinceLabel.Visibility = Visibility.Hidden;
            ProgressBar1.IsIndeterminate = true;
            _canWrite = false;


            if (File.Exists(path))
            {
                e.Cancel = true;

            }
            else
            {
                e.Cancel = false;
                Environment.Exit(0);
            }

        }







        #endregion










        #region // Application Closing




        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // Stop the refresh loop
            base.OnClosing(e);
        }

        // Ensure all resources are cleaned up and application is properly closed
        private void OnClosed(object sender, EventArgs e)
        {
            // Force garbage collection
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Application.Current.Shutdown();
           
        }



        #endregion







        /// <summary>
        /// Disk 
        /// This section references the work of several sources, including 
        /// Llewellyn Kruger, ChatGPT, GitHub Copilot, Black Box AI, various Google sources, and the author.
        /// </summary>


        #region DISK SMART DATA



        private void LoadHddData()
        {
            try
            {
                // Clear the previous data
                hddDrives.Clear();
                lstDrivesSMART.Items.Clear();

                var wdSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                int driveIndex = 0;

                foreach (ManagementObject drive in wdSearcher.Get())
                {
                    var hdd = new HDD
                    {
                        Model = drive["Model"]?.ToString().Trim() ?? "Unknown",
                        Type = drive["InterfaceType"]?.ToString().Trim() ?? "Unknown",
                        Serial = drive["SerialNumber"]?.ToString().Trim() ?? "None"
                    };

                    hddDrives.Add(driveIndex, hdd);
                    lstDrivesSMART.Items.Add($"Drive {driveIndex + 1}: {hdd.Model}");
                    driveIndex++;
                }

                var pmsearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                driveIndex = 0;
                foreach (ManagementObject drive in pmsearcher.Get())
                {
                    if (driveIndex >= hddDrives.Count)
                        break;

                    // Only update serial if it is missing from hddDrives
                    if (string.IsNullOrEmpty(hddDrives[driveIndex].Serial))
                    {
                        hddDrives[driveIndex].Serial = drive["SerialNumber"]?.ToString().Trim() ?? "None";
                    }
                    driveIndex++;
                }

                var scope = new ManagementScope(@"\\.\root\wmi");
                scope.Connect();
                var searcher = new ManagementObjectSearcher(scope, new ObjectQuery("SELECT * FROM MSStorageDriver_FailurePredictStatus"));


                // Fetch failure prediction status
                searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictStatus");
                driveIndex = 0;
                foreach (ManagementObject drive in searcher.Get())
                {
                    hddDrives[driveIndex].IsOK = !(bool)drive.Properties["PredictFailure"].Value;
                    driveIndex++;
                }

                // Fetch failure prediction data
                searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictData");
                driveIndex = 0;
                foreach (ManagementObject data in searcher.Get())
                {
                    Byte[] bytes = (Byte[])data.Properties["VendorSpecific"].Value;
                    for (int i = 0; i < 30; ++i)
                    {
                        try
                        {
                            int id = bytes[i * 12 + 2];
                            int value = bytes[i * 12 + 5];
                            int worst = bytes[i * 12 + 6];
                            int vendordata = BitConverter.ToInt32(bytes, i * 12 + 7);

                            if (id == 0) continue;

                            var attr = hddDrives[driveIndex].Attributes[id];
                            attr.Current = value;
                            attr.Worst = worst;
                            attr.Data = vendordata;
                            attr.IsOK = (bytes[i * 12 + 4] & 0x1) == 0;
                        }
                        catch { }
                    }
                    driveIndex++;
                }

                // Fetch failure prediction thresholds
                searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictThresholds");
                driveIndex = 0;
                foreach (ManagementObject data in searcher.Get())
                {
                    Byte[] bytes = (Byte[])data.Properties["VendorSpecific"].Value;
                    for (int i = 0; i < 30; ++i)
                    {
                        try
                        {
                            int id = bytes[i * 12 + 2];
                            int thresh = bytes[i * 12 + 3];

                            if (id == 0) continue;

                            hddDrives[driveIndex].Attributes[id].Threshold = thresh;
                        }
                        catch { }
                    }
                    driveIndex++;
                }

                lstDrivesSMART.SelectionChanged += LstDrives_SelectedIndexChanged;
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("This application requires administrative privileges to access SMART data. Please run as Administrator.",
                                "Privilege Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            catch (ManagementException ex)
            {
                MessageBox.Show($"WMI query failed: {ex.Message}",
                                "WMI Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }

        }




        private void LstDrives_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = lstDrivesSMART.SelectedIndex;

            // Check if the selected index is valid
            if (selectedIndex < 0 || selectedIndex >= hddDrives.Count)
            {
                return; // No valid selection, so exit
            }

            // Ensure that the selected drive exists and has necessary properties
            var selectedHdd = hddDrives[selectedIndex];

            // Check for null values or missing properties
            if (selectedHdd == null)
            {
                MessageBox.Show("Selected drive is invalid.");
                return;
            }

            // Ensure that the Model and Serial number are available (even if the drive doesn't have them)
            string model = !string.IsNullOrEmpty(selectedHdd.Model) ? selectedHdd.Model : "Unknown Model";
            string serial = !string.IsNullOrEmpty(selectedHdd.Serial) ? selectedHdd.Serial : "Unknown Serial";

            // Optionally, you can display the serial number or model to the user for debugging or information
            // lblDriveInfo.Content = $"Model: {model}\nSerial: {serial}";

            // Proceed to display SMART data for the selected drive
            DisplaySmartData(selectedHdd);
        }









        private void DisplaySmartData(HDD hdd)
        {



            //Disk Helth  Start 



            if (hdd.IsOK)
            {
                lblStatusHelth.Content = $"Status: OK";
                lblStatusHelth.Foreground = new SolidColorBrush(Colors.Green);  // Green color for OK
                lblStatusHelth1.Background = new SolidColorBrush(Colors.Green);

                // Stop animation
                //lblStatusHelth1.BeginAnimation(Label.OpacityProperty, null);

                var flashingAnimation = (Storyboard)FindResource("FlashingAnimation");
                flashingAnimation.Begin(lblStatusHelth1, true);
                flashingAnimation.Begin(lblStatusHelth, true);
            }
            else
            {
                lblStatusHelth.Content = $"Status: Warning";
                lblStatusHelth.Foreground = new SolidColorBrush(Colors.Red);   // Red color for Failing
                lblStatusHelth1.Background = new SolidColorBrush(Colors.Red);


                var flashingAnimation = (Storyboard)FindResource("FlashingAnimation");
                flashingAnimation.Begin(lblStatusHelth1, true);
                flashingAnimation.Begin(lblStatusHelth, true);

            }



            var sb = new StringBuilder();
            sb.AppendLine($"Model: {hdd.Model}");
            sb.AppendLine($"Type: {hdd.Type}");
            sb.AppendLine($"Serial: {hdd.Serial}");
            sb.AppendLine($"Status: {(hdd.IsOK ? "OK" : "Failing")}");

            // Txt Helth
            Texthelth.Text = sb.ToString();


            // End Helth 





            //  Send SMART Information To the RAW TEXT

            #region  RAW INFORMATION


            var raw = new StringBuilder();

            // Append basic drive information
            raw.AppendLine($"Model: {hdd.Model}");
            raw.AppendLine($"Type: {hdd.Type}");
            raw.AppendLine($"Serial: {hdd.Serial}");
            raw.AppendLine($"Status: {(hdd.IsOK ? "OK" : "Warning")}");

            // Append each SMART attribute's information
            raw.AppendLine("\nSMART Attributes:");
            foreach (var attr in hdd.Attributes)
            {
                if (attr.Value.HasData)
                {
                    raw.AppendLine($"ID: {attr.Key.ToString("X2")}, Name: {attr.Value.Attribute}, " +
                                  $"Current: {attr.Value.Current}, Worst: {attr.Value.Worst}, " +
                                  $"Threshold: {attr.Value.Threshold}, Data: {attr.Value.Data}");
                }
            }

            // Set the gathered information to the TextBox
            textBoxRaw.Text = raw.ToString();



            // END RAW TEXT

            #endregion   END RAW TEXT







            labelTemp.Content = "Temp: N/A";


            // Bind the SMART data to the DataGrid
            List<SmartAttribute> smartAttributes = new List<SmartAttribute>();

            foreach (var attr in hdd.Attributes)
            {
                if (attr.Value.HasData)
                {
                    var smartAttr = new SmartAttribute();
                    smartAttributes.Add(new SmartAttribute
                    {
                        ID = attr.Key.ToString("X2"),
                        Name = attr.Value.Attribute,
                        Current = attr.Value.Current,
                        Worst = attr.Value.Worst,
                        Threshold = attr.Value.Threshold,
                        Data = attr.Value.Data,



                    });

                    if (attr.Key == 0xC2)
                    {
                        // Format the temperature and update the label
                        string temperatureValue = $"{attr.Value.Data & 0xFF} °C";
                        labelTemp.Content = $"Temperature: {temperatureValue}";
                    }




                }




            }


            // Assign the list to the DataGrid's ItemsSource to display it
            dgSmartData.ItemsSource = smartAttributes;


        }









        private async void but_Test_Click(object sender, RoutedEventArgs e)


        {

            if (combo_test.SelectedItem == null)
            {
                MessageBox.Show("Please select a drive.");
                return;
            }

            string selectedDrive = combo_test.SelectedItem.ToString();
            string filePath = System.IO.Path.Combine(selectedDrive, "testfile.dat");
            long dataSize = 1024 * 1024 * 100; // 100 MB
            byte[] data = new byte[dataSize];

            // Fill the array with random data
            new Random().NextBytes(data);

            byte[] readData = new byte[dataSize];
            Stopwatch stopwatch = new Stopwatch();

            try
            {
                // Measure write speed
                stopwatch.Start();
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, FileOptions.WriteThrough))
                {
                    fs.Write(data, 0, data.Length);
                }
                stopwatch.Stop();

                double writeTimeInSeconds = stopwatch.Elapsed.TotalSeconds;
                double writeSpeed = (dataSize / 1024.0 / 1024.0) / writeTimeInSeconds; // Speed in MB/s
                lbl_WriteSpeed.Content = $"Write speed: {writeSpeed:F2} MB/s";

                // Add a delay to allow the system to flush write buffers
                await Task.Delay(10000);


                // Reset and measure read speed
                // stopwatch.Reset();
                stopwatch.Start();
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.SequentialScan))
                {
                    fs.Read(readData, 0, readData.Length);
                }
                stopwatch.Stop();

                double readTimeInSeconds = stopwatch.Elapsed.TotalSeconds;
                double readSpeed = (dataSize / 1024.0 / 1024.0) / readTimeInSeconds; // Speed in MB/s
                lbl_ReadSpeed.Content = $"Read speed: {readSpeed:F2} MB/s";
            }

            finally


            {
                // Delete the test file
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }






        }








        private void SetupDriveWatchers()
        {
            // Watch for drives added
            var insertWatcher = new ManagementEventWatcher("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2");
            insertWatcher.EventArrived += (sender, args) => Dispatcher.Invoke(LoadDrives);
            insertWatcher.Start();

            // Watch for drives removed
            var removeWatcher = new ManagementEventWatcher("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 3");
            removeWatcher.EventArrived += (sender, args) => Dispatcher.Invoke(LoadDrives);
            removeWatcher.Start();
        }




        private void LoadDrives()
        {
            combo_test.Items.Clear();

            // Get all logical drives and add them to the ComboBox
            foreach (var drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                combo_test.Items.Add(drive.Name);
            }

            // Set the first drive as selected by default, if available
            if (combo_test.Items.Count > 0)
            {
                combo_test.SelectedIndex = 0;
            }
        }




        #region Library of the SMART attributes


        public class HDD
        {
            public int Index { get; set; }
            public bool IsOK { get; set; }
            public string Model { get; set; }
            public string Type { get; set; }
            public string Serial { get; set; }


            public Dictionary<int, Smart> Attributes = new Dictionary<int, Smart>()

        {
            // ... ( attributes) ...
    {0, new Smart("Invalid")},
    {1, new Smart("Read error rate")},
    {2, new Smart("Throughput performance")},
    {3, new Smart("Spin up time")},
    {4, new Smart("Start/Stop count")},
    {5, new Smart("Reallocated sector count")},
    {6, new Smart("Read channel margin")},
    {7, new Smart("Seek error rate")},
    {8, new Smart("Seek timer performance")},
    {9, new Smart("Power-on hours count")},
    {10, new Smart("Spin up retry count")},
    {11, new Smart("Calibration retry count")},
    {12, new Smart("Power cycle count")},
    {13, new Smart("Soft read error rate")},
    {22, new Smart("Current Helium Level")},
    {160, new Smart("Uncorrectable sector count read or write")},
    {161, new Smart("Remaining spare block percentage")},
    {164, new Smart("Total Erase Count")},
    {165, new Smart("Maximum Erase Count")},
    {166, new Smart("Minimum Erase Count")},
    {167, new Smart("Average Erase Count")},
    {168, new Smart("Max NAND Erase Count from specification")},
    {169, new Smart("Remaining life percentage")},
    {170, new Smart("Available Reserved Space")},
    {171, new Smart("SSD Program Fail Count")},
    {172, new Smart("SSD Erase Fail Count")},
    {173, new Smart("SSD Wear Leveling Count")},
    {174, new Smart("Unexpected Power Loss Count")},
    {175, new Smart("Power Loss Protection Failure")},
    {176, new Smart("Erase Fail Count")},
    {177, new Smart("Wear Range Delta")},
    {178, new Smart("Used Reserved Block Count (Chip)")},
    {179, new Smart("Used Reserved Block Count (Total)")},
    {180, new Smart("Unused Reserved Block Count Total")},
    {181, new Smart("Program Fail Count Total or Non 4K Aligned Access Count")},
    {182, new Smart("Erase Fail Count")},
    {183, new Smart("SATA Down shift Error Count")},
    {184, new Smart("End-to-End error")},
    {185, new Smart("Head Stability")},
    {186, new Smart("Induced Op Vibration Detection")},
    {187, new Smart("Reported Uncorrectable Errors")},
    {188, new Smart("Command Timeout")},
    {189, new Smart("High Fly Writes")},
    {190, new Smart("Temperature Difference from 100")},
    {191, new Smart("G-sense error rate")},
    {192, new Smart("Power-off retract count")},
    {193, new Smart("Load/Unload cycle count")},
    {194, new Smart("Temperature")},
    {195, new Smart("Hardware ECC recovered")},
    {196, new Smart("Reallocation count")},
    {197, new Smart("Current pending sector count")},
    {198, new Smart("Off-line scan uncorrectable count")},
    {199, new Smart("UDMA CRC error rate")},
    {200, new Smart("Write error rate")},
    {201, new Smart("Soft read error rate")},
    {202, new Smart("Data Address Mark errors")},
    {203, new Smart("Run out cancel")},
    {204, new Smart("Soft ECC correction")},
    {205, new Smart("Thermal asperity rate (TAR)")},
    {206, new Smart("Flying height")},
    {207, new Smart("Spin high current")},
    {208, new Smart("Spin buzz")},
    {209, new Smart("Off-line seek performance")},
    {211, new Smart("Vibration During Write")},
    {212, new Smart("Shock During Write")},
    {220, new Smart("Disk shift")},
    {221, new Smart("G-sense error rate")},
    {222, new Smart("Loaded hours")},
    {223, new Smart("Load/unload retry count")},
    {224, new Smart("Load friction")},
    {225, new Smart("Load/Unload cycle count")},
    {226, new Smart("Load-in time")},
    {227, new Smart("Torque amplification count")},
    {228, new Smart("Power-off retract count")},
    {230, new Smart("Life Curve Status")},
    {231, new Smart("SSD Life Left")},
    {232, new Smart("Endurance Remaining")},
    {233, new Smart("Media Wear out Indicator")},
    {234, new Smart("Average Erase Count AND Maximum Erase Count")},
    {235, new Smart("Good Block Count AND System Free Block Count")},
    {240, new Smart("Head flying hours")},
    {241, new Smart("Lifetime Writes From Host GiB")},
    {242, new Smart("Lifetime Reads From Host GiB")},
    {243, new Smart("Total LBAs Written Expanded")},
    {244, new Smart("Total LBAs Read Expanded")},
    {249, new Smart("NAND Writes GiB")},
    {250, new Smart("Read error retry rate")},
    {251, new Smart("Minimum Spares Remaining")},
    {252, new Smart("Newly Added Bad Flash Block")},
    {254, new Smart("Free Fall Protection")}

        };
        }

        public class Smart
        {
            public string Attribute { get; set; }
            public int Current { get; set; }
            public int Worst { get; set; }
            public int Threshold { get; set; }
            public int Data { get; set; }
            public bool IsOK { get; set; }

            public bool HasData => Current != 0 || Worst != 0 || Threshold != 0 || Data != 0;

            public Smart() { }

            public Smart(string attributeName)
            {
                Attribute = attributeName;
            }
        }

        public class SmartAttribute
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public int Current { get; set; }
            public int Worst { get; set; }
            public int Threshold { get; set; }
            public int Data { get; set; }
            public string Temperature { get; set; }
        }




        #endregion end Library Of the SMART





        #endregion  End OF the  SMART in the HDD 












        /// <summary>
        /// File Backup System For the backup Tab 
        /// Eddy   https://github.com/MrEdWORD/FileBackupTool   
        /// ChatGPT , Writer   
        /// </summary>




        #region   Backup  File 





        private void btnselect_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = "";
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                dialog.Title = "Select the  folder Need To Backup";

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    folderPath = dialog.FileName;
                    textselctbacup.Text = folderPath;
                }


            }
        }

        private void btnwhere_Click(object sender, RoutedEventArgs e)
        {

            string folderPath = "";
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                dialog.Title = "Select the Where to Send ";
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    folderPath = dialog.FileName;
                    textwhere.Text = folderPath;
                }




            }


        }


        private CancellationTokenSource _cancellationTokenSource;

        private async void btnbackup_Click(object sender, RoutedEventArgs e)
        {


            // Ensure backup name is provided
            if (string.IsNullOrWhiteSpace(txtbackname.Text))
            {
                MessageBox.Show("You must provide a backup name", "Backup Name is Empty", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Ensure destination directory exists
            if (!Directory.Exists(textwhere.Text))
            {
                MessageBox.Show("You must provide a directory that exists", "Destination Directory Does Not Exist", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            // Ensure source directory exists
            if (!Directory.Exists(textselctbacup.Text))
            {
                MessageBox.Show("You must provide a directory that exists", "Source Directory Does Not Exist", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token; // Assign token from _cancellationTokenSource


            var bs = new FileBackupTool.BackupSettings(txtbackname.Text, textwhere.Text, textselctbacup.Text);
            var backup = new Backup(bs);

            var progress = new Progress<int>(percent =>
            {
                ProgressBack.Value = percent;
                Progressbacktext.Text = $"Progress: {percent}%";
            });


            try
            {
                await backup.BackupFilesAsync(progress, token);

                MessageBox.Show($"Success! Total files in backup: {backup.TotalFiles} Total copied files: {backup.CopiedFiles} Total copied dirs: {backup.CopiedDirectories}",
                          "Backup Complete", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Backup canceled.", "Operation Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            finally
            {
                _cancellationTokenSource.Dispose();
            }
        }





        #region  Backup Class


        public class Backup
        {
            private FileBackupTool.BackupSettings BackupSetting { get; set; }

            public int TotalFiles { get; private set; }
            public int CopiedFiles { get; private set; }
            public int CopiedDirectories { get; private set; }

            public Backup(FileBackupTool.BackupSettings backupSetting)
            {
                BackupSetting = backupSetting;
            }







            public async Task BackupFilesAsync(IProgress<int> progress, CancellationToken token)
            {
                string backupSourceDirectory = BackupSetting.SourceDirectory;
                string backupDestinationDirectory = BackupSetting.DestinationDirectory;
                bool copySubDirs = true;

                TotalFiles = Directory.GetFiles(backupSourceDirectory, "*", SearchOption.AllDirectories).Length;
                CopiedFiles = 0;
                CopiedDirectories = 0;

                await DirectoryCopyAsync(backupSourceDirectory, backupDestinationDirectory, copySubDirs, progress, token);
            }






            private async Task DirectoryCopyAsync(string sourceDir, string destDir, bool copySubDirs, IProgress<int> progress, CancellationToken token)
            {
                DirectoryInfo dir = new DirectoryInfo(sourceDir);

                if (!dir.Exists)
                {
                    throw new DirectoryNotFoundException($"Source directory not found: {sourceDir}");
                }

                DirectoryInfo[] dirs = dir.GetDirectories();
                if (!Directory.Exists(destDir))
                {
                    Directory.CreateDirectory(destDir);
                }

                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    token.ThrowIfCancellationRequested(); // Check for cancellation

                    string tempPath = System.IO.Path.Combine(destDir, file.Name);
                    await Task.Run(() => file.CopyTo(tempPath, true), token); // Asynchronously copy the file with cancellation support
                    CopiedFiles++;
                    progress.Report(CopiedFiles * 100 / TotalFiles);
                }

                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        token.ThrowIfCancellationRequested(); // Check for cancellation

                        string tempPath = System.IO.Path.Combine(destDir, subdir.Name);
                        await DirectoryCopyAsync(subdir.FullName, tempPath, copySubDirs, progress, token);
                        CopiedDirectories++;
                    }
                }
            }
        }




        private void btncancelback_Click(object sender, RoutedEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }






        private void InitializeDriveInfo(TextBlock textBlock)
        {
            if (textBlock == null)
                throw new ArgumentNullException(nameof(textBlock));

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            StringBuilder driveInfoBuilder = new StringBuilder();

            foreach (DriveInfo drive in allDrives)
            {
                driveInfoBuilder.AppendLine($"Drive Letter: {drive.Name}");
                driveInfoBuilder.AppendLine($"Drive Type: {drive.DriveType}");

                if (drive.IsReady)
                {
                    driveInfoBuilder.AppendLine($"Volume Label: {drive.VolumeLabel}");
                    driveInfoBuilder.AppendLine($"File System: {drive.DriveFormat}");
                    driveInfoBuilder.AppendLine($"Total Size: {drive.TotalSize} bytes ({ConvertBytesToMegabytes(drive.TotalSize)} MB, {ConvertBytesToGigabytes(drive.TotalSize)} GB)");
                    driveInfoBuilder.AppendLine($"Available Free Space: {drive.AvailableFreeSpace} bytes ({ConvertBytesToMegabytes(drive.AvailableFreeSpace)} MB, {ConvertBytesToGigabytes(drive.AvailableFreeSpace)} GB)");
                    driveInfoBuilder.AppendLine($"Total Free Space: {drive.TotalFreeSpace} bytes ({ConvertBytesToMegabytes(drive.TotalFreeSpace)} MB, {ConvertBytesToGigabytes(drive.TotalFreeSpace)} GB)");
                }

                driveInfoBuilder.AppendLine(); // Add spacing between drives
            }

            textBlock.Text = driveInfoBuilder.ToString();
            textBoxRaw.Text = driveInfoBuilder.ToString();
        }

        private double ConvertBytesToGigabytes(long bytes)
        {
            return (double)bytes / (1024 * 1024 * 1024);
        }

        private double ConvertBytesToMegabytes(long bytes)
        {
            return (double)bytes / (1024 * 1024);
        }








        /// <summary>
        /// Writer and Chat GPT    // in the SMART and Menu Section 
        /// </summary>
        /// <param name="Report Generate And Save Data To file"></param>


        #region Report Generat And Save 

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {




            if (lstDrivesSMART.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a drive.", "No Drive Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            HDD selectedHdd = hddDrives[lstDrivesSMART.SelectedIndex];

            // Gather other system information (replace with your data source)
            string otherSystemInfo = txtblock_other.Text;

            // Generate the FlowDocument
            FlowDocument reportDoc = GenerateReport(selectedHdd, otherSystemInfo);

            // Prompt the user to save the file
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf",
                Title = "Save Report"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                SaveReportToFile(reportDoc, saveFileDialog.FileName);
            }


        }




        private FlowDocument GenerateReport(HDD hdd, string otherSystemInfo)
        {
            // Create a new FlowDocument
            FlowDocument doc = new FlowDocument();

            // Title
            Paragraph title = new Paragraph(new Run("Uni Wipe System and SMART Data Report"))
            {
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20) // Add spacing after title
            };
            doc.Blocks.Add(title);

            // Add SMART Data Section
            Paragraph smartHeader = new Paragraph(new Run("SMART Data"))
            {
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 10, 0, 10) // Spacing before and after header
            };
            doc.Blocks.Add(smartHeader);

            foreach (var attr in hdd.Attributes)
            {
                if (attr.Value.HasData)
                {
                    Paragraph smartAttr = new Paragraph();
                    smartAttr.Inlines.Add(new Bold(new Run($"ID: {attr.Key:X2}")));
                    smartAttr.Inlines.Add(new Run($", Name: {attr.Value.Attribute}"));
                    smartAttr.Inlines.Add(new Run($", Current: {attr.Value.Current}"));
                    smartAttr.Inlines.Add(new Run($", Worst: {attr.Value.Worst}"));
                    smartAttr.Inlines.Add(new Run($", Threshold: {attr.Value.Threshold}"));
                    smartAttr.Inlines.Add(new Run($", Data: {attr.Value.Data}"));
                    smartAttr.Margin = new Thickness(10, 0, 0, 5); // Indent and spacing between attributes
                    doc.Blocks.Add(smartAttr);
                }
            }

            // Add Other System Information Section
            Paragraph systemInfoHeader = new Paragraph(new Run("Other System Information"))
            {
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 20, 0, 10) // Spacing before and after header
            };
            doc.Blocks.Add(systemInfoHeader);

            // Use a bullet list for readability
            var otherSystemLines = otherSystemInfo.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            List list = new List();
            foreach (string line in otherSystemLines)
            {
                list.ListItems.Add(new ListItem(new Paragraph(new Run(line))));
            }
            doc.Blocks.Add(list);

            // Add Footer
            Paragraph footer = new Paragraph(new Run("Generated by Uni Wipe"))
            {
                FontSize = 12,
                FontStyle = FontStyles.Italic,
                TextAlignment = TextAlignment.Right,
                Margin = new Thickness(0, 30, 0, 0) // Add spacing above footer
            };
            doc.Blocks.Add(footer);

            return doc;
        }





        private void SaveReportToFile(FlowDocument doc, string filePath)
        {
            try
            {
                // Create a TextRange covering the entire FlowDocument
                TextRange textRange = new TextRange(doc.ContentStart, doc.ContentEnd);

                // Save to file
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    textRange.Save(fileStream, DataFormats.Rtf);
                }

                MessageBox.Show("Report saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save report: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Bt_Save__Click(object sender, RoutedEventArgs e)
        {


            if (lstDrivesSMART.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a drive.", "No Drive Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            HDD selectedHdd = hddDrives[lstDrivesSMART.SelectedIndex];

            // Gather other system information (replace with your data source)
            string otherSystemInfo = txtblock_other.Text;

            // Generate the FlowDocument
            FlowDocument reportDoc = GenerateReport(selectedHdd, otherSystemInfo);

            // Prompt the user to save the file
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf",
                Title = "Save Report"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                SaveReportToFile(reportDoc, saveFileDialog.FileName);
            }





        }

         #endregion  //  End Of the Report Generation and Save Section 







    }
}







    namespace FileBackupTool
    {
        public class BackupSettings
        {
            public string Name { get; set; }
            public string DestinationDirectory { get; set; }
            public string SourceDirectory { get; set; }

            public BackupSettings(string name, string destinationDirectory, string sourceDirectory)
            {
                Name = name;
                DestinationDirectory = destinationDirectory;
                SourceDirectory = sourceDirectory;
            }
        }
}
#endregion backup Class


#endregion  End File Backup







/// <summary>
/// Font Size Controll in the other TAB 
///  Writer and Chat GPT 
/// </summary>



# region   // Font Size Control in Other TAB




namespace Uni_Wipe_WPF
{
    public class FontSizeViewModel : INotifyPropertyChanged
    {
        private double _fontSize = 14;

        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    OnPropertyChanged(nameof(FontSize));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}



#endregion  // Slider controll in the Other TAB 























#endregion