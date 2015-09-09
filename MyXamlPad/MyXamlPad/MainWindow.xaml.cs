using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace MyXamlPad
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool m_bContinue;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            if (m_bContinue)
            {
                m_bContinue = false;
                btnProcess.Content = "开始处理图片";
            }
            else
            {
                m_bContinue = true;
                btnProcess.Content = "取消处理图片";
                Thread thr = new Thread(DoWork);
                thr.Start();
            }
        }

        private void DoWork()
        {
            // 构建4 * 4 的表格
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                        (ThreadStart)delegate ()
                        {
                            grdPic.RowDefinitions.RemoveRange(1, grdPic.RowDefinitions.Count - 1);
                            grdPic.RowDefinitions.Add(new RowDefinition());
                            grdPic.RowDefinitions.Add(new RowDefinition());
                            grdPic.RowDefinitions.Add(new RowDefinition());
                            grdPic.RowDefinitions.Add(new RowDefinition());
                        }
                        );

            int s32PicNum = 0;
            string strPath = @"C:\Users\sn01396\Desktop\16png\";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 4; j < 8; j++)
                {
                    if (!m_bContinue)
                    {
                        this.Dispatcher.Invoke(DispatcherPriority.Normal,
                        (ThreadStart)delegate ()
                        {
                            lblProcess.Content = string.Format("完成: 0 / 0");
                        }
                        );
                        return;
                    }
                    Thread.Sleep(1000);
                    this.Dispatcher.Invoke(DispatcherPriority.Normal,
                        (ThreadStart)delegate ()
                        {
                            s32PicNum++;
                            Image img = new Image();
                            string strFileName = strPath + s32PicNum.ToString() + ".png";
                            Uri uri = new Uri(strFileName);
                            BitmapImage bi = new BitmapImage(uri);
                            img.Source = bi;
                            grdPic.Children.Add(img);
                            Grid.SetColumn(img, j - 4);
                            Grid.SetRow(img, i + 1);
                            lblProcess.Content = string.Format("完成: {0} / 16", s32PicNum);

                        }
                        );
                }
            }
            this.Dispatcher.Invoke(DispatcherPriority.Normal,
                (ThreadStart)delegate ()
                {
                    btnProcess.Content = "开始处理图片";
                    lblProcess.Content = string.Format("完成: 0 / 0");
                }
                );
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            m_bContinue = false;
        }
    }
}
