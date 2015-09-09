using System;
using System.Collections.Generic;
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

namespace wpf布局
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region fields
        private bool m_bContinue = false;
        private List<string> m_lstCtl = new List<string>();
        #endregion
        #region construction
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region methods
        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            if (m_bContinue)
            {
                m_bContinue = false;
                btnProcess.Content = "开始";
            }
            else
            {
                m_bContinue = true;
                btnProcess.Content = "取消";
                pbShow.Value = 0;
                lblPercent.Content = "0.0%";
                Thread thr = new Thread(DoWork);
                thr.Start();
            }
        }

        private void DoWork()
        {
            var strPath = @"C:\Users\sn01396\Desktop\16png\";
            var astrFiles = Directory.GetFiles(strPath, "*.png");
            var s32FileCount = astrFiles.Length;
            if (s32FileCount > 0)
            {
                // 一行4列，计算出要几行，在grid中动态添加行
                var s32RowNum = (s32FileCount - 1) / 4 + 1;
                this.Dispatcher.Invoke(DispatcherPriority.Normal,
                    (ThreadStart)delegate ()
                    {
                        if (grdPic.RowDefinitions.Count > 1)
                        {
                            grdPic.RowDefinitions.RemoveRange(1, grdPic.RowDefinitions.Count - 1);
                            for (int i = 0; i < m_lstCtl.Count; )
                            {
                                Image img = grdPic.FindName(m_lstCtl[i]) as Image;
                                if (img != null)
                                {
                                    grdPic.Children.Remove(img);
                                    grdPic.UnregisterName(m_lstCtl[i]);
                                    m_lstCtl.RemoveAt(i);
                                }
                                else
                                {
                                    i++;
                                }
                            }

                            for (int i = 0; i < s32RowNum; i++)
                            {
                                grdPic.RowDefinitions.Add(new RowDefinition());
                            }
                        }
                    });
                for (int i = 0; i < astrFiles.Length; i++)
                {
                    if (!m_bContinue)
                    {
                        break;
                    }
                    Thread.Sleep(200);
                    this.Dispatcher.Invoke(DispatcherPriority.Normal,
                        (ThreadStart)delegate ()
                        {
                            Uri uri = new Uri(astrFiles[i]);
                            BitmapImage bi = new BitmapImage(uri);
                            Image img = new Image();
                            img.Source = bi;
                            grdPic.Children.Add(img);
                            string strCtlName = "img" + i.ToString();
                            grdPic.RegisterName(strCtlName, img);
                            m_lstCtl.Add(strCtlName);
                            Grid.SetRow(img, i / 4 + 1);
                            Grid.SetColumn(img, i % 4);
                            pbShow.Value = ((double)(i + 1)) / astrFiles.Length * 100;
                            lblPercent.Content = pbShow.Value.ToString("f1") + "%";
                        });
                }
                m_bContinue = false;
            }
            else
            {

            }
            this.Dispatcher.Invoke(DispatcherPriority.Normal,
                (ThreadStart)delegate ()
                {
                    btnProcess.Content = "开始";
                    MessageBox.Show("执行完成！", "提示");
                    //ShowWindow showWindow = new ShowWindow("执行完成！");
                    //showWindow.Owner = this;
                    //showWindow.Show();
                });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_bContinue = false;
        }
        #endregion
    }
}
