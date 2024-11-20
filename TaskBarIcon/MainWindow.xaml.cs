
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
using System.Windows.Forms;
using System.Drawing;

/// <Summary>
/// Compile By Yashan 2024 08
/// </summary>











namespace TaskBarIcon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private NotifyIcon notifyIcon;




        public MainWindow()
        {

           


            InitializeComponent();


            InitializeNotifyIcon();
            this.Hide();


            //Uni_Wipe_WPF.MainWindow Mainwindow1 = new Uni_Wipe_WPF.MainWindow();

          //  Mainwindow1.Show();
          










        }

   
    //=====================================================
   // hear You can Chaing Task Bar Menu Item 
   //======================================================
     

        #region // Getting Task Bar ICON And Task Bar Menu Item 



        private void InitializeNotifyIcon()
        {



            string iconPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TaskBarIcon", "UNI-ICON.ico");
            if (System.IO.File.Exists(iconPath))
            {
                notifyIcon = new NotifyIcon
                {
                    Icon = new Icon(iconPath),
                    Visible = true
                };
            }
            else
            {
                Console.WriteLine("Icon file not found at: " + iconPath);
                notifyIcon = new NotifyIcon
                {
                    Icon = SystemIcons.Application, // Fallback icon
                    Visible = true
                };
            }



            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("Show", null, ShowApp);
            notifyIcon.ContextMenuStrip.Items.Add("Exit", null, ExitApp);




        }



        #endregion




        // ==================================================
        // Show Application 
        //======================================================


        private void ShowApp(object sender, EventArgs e)
        {
            
            Uni_Wipe_WPF.Window1 win1 = new Uni_Wipe_WPF.Window1();
            win1.Show();



            




             
        }








        //==========================================
        //  Exit Of the Main Application
        //==========================================



        private void ExitApp(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
           



          //  Uni_Wipe_WPF.Window1 win1 = new Uni_Wipe_WPF.Window1();
          //  win1.Close();
          //  Environment.Exit(0);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            System.Windows.Application.Current.Shutdown();





        }






        //===================================
        // caling to the APP.xaml
        //===================================



        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            base.OnClosed(e);
        }

















    }
}
