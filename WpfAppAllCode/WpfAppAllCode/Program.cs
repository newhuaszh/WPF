using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppAllCode
{
    class Program : Application
    {
        [STAThread]
        static void Main(string[] args)
        {
            Program app = new Program();
            app.Startup += AppStartUp;
            app.Exit += AppExit;
            app.Run();
        }

        static void AppExit(object sender, ExitEventArgs e)
        {
            MessageBox.Show("Exited");
        }

        static void AppStartUp(object sender, StartupEventArgs e)
        {
            Application.Current.Properties["GodMode"] = false;
            foreach (string arg in e.Args)
            {
                if (arg.ToString() == "/godmode")
                {
                    Application.Current.Properties["GodMode"] = true;
                    break;
                }
            }

            MainWindow mainWindow = new MainWindow("My firstt", 200, 300);
            //mainWindow.Title = "My first";
            //mainWindow.Height = 200;
            //mainWindow.Width = 300;
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
        }
    }

    class MainWindow : Window
    {
        private Button btnExitApp = new Button();

        public MainWindow(string windowTitle, int height, int width)
        {
            btnExitApp.Click += BtnExitApp_Click;
            btnExitApp.Content = "Exit App";
            btnExitApp.Height = 25;
            btnExitApp.Width = 100;

            this.Content = btnExitApp;

            this.Title = windowTitle;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Height = height;
            this.Width = width;

            this.MouseMove += MainWindow_MouseMove;
            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            btnExitApp.Content = e.Key.ToString();
        }

        private void MainWindow_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Title = e.GetPosition(this).ToString();
        }

        private void BtnExitApp_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Application.Current.Properties["GodMode"])
                MessageBox.Show("Cheater");

            this.Close();
        }
    }
}
