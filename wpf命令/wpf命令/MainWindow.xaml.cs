using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf命令
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetF1CommandBinding();
        }

        private void SetF1CommandBinding()
        {
            CommandBinding helpBinding = new CommandBinding(ApplicationCommands.Help);
            helpBinding.CanExecute += HelpBinding_CanExecute;
            helpBinding.Executed += HelpBinding_Executed;
            CommandBindings.Add(helpBinding);

            CommandBinding closeBinding = new CommandBinding(ApplicationCommands.Copy);
            closeBinding.CanExecute += CloseBinding_CanExecute;
            closeBinding.Executed += CloseBinding_Executed;
            CommandBindings.Add(closeBinding);
        }

        private void CloseBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Close!");
        }

        private void CloseBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void HelpBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Help!");
        }

        private void HelpBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "Text Files |*.txt";
            if (true == openDlg.ShowDialog())
            {
                string dataFromFile = File.ReadAllText(openDlg.FileName);
                // Show string in TextBox.
                txtData.Text = dataFromFile;
            }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
