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
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Annotations;
using System.Windows.Annotations.Storage;

namespace WpfControlsAndAPIs
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            myInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            inkRadio.IsChecked = true;
            comboColors.SelectedIndex = 0;
            PopulateDocument();
            EnableAnnotations();
        }

        private void RadioButtonClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            switch((sender as RadioButton).Content.ToString())
            {
                case "Ink Mode!":
                    myInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "Erase Mode!":
                    myInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;
                case "Select Mode!":
                    myInkCanvas.EditingMode = InkCanvasEditingMode.Select ;
                    break;
                default:
                    myInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;
            }
        }

        private void comboColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string strColorUse = (comboColors.SelectedItem as StackPanel).Tag.ToString();
            myInkCanvas.DefaultDrawingAttributes.Color = (Color)ColorConverter.ConvertFromString(strColorUse);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("StrokeData.bin", FileMode.Create))
            {
                myInkCanvas.Strokes.Save(fs);
                fs.Close();
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("StrokeData.bin", FileMode.Open, FileAccess.Read))
            {
                myInkCanvas.Strokes = new StrokeCollection(fs);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.Strokes.Clear();
        }

        private void PopulateDocument()
        {
            listOfFunFacts.FontSize = 14;
            listOfFunFacts.MarkerStyle = TextMarkerStyle.Circle;
            listOfFunFacts.ListItems.Add(new ListItem(new
                Paragraph(new Run("Fixed documents are for WYSIWYG print ready docs!"))));
            listOfFunFacts.ListItems.Add(new ListItem(new
                Paragraph(new Run("The API supports tables and embedded figures!"))));
            listOfFunFacts.ListItems.Add(new ListItem(new
                Paragraph(new Run("Flow documents are read only!"))));
            listOfFunFacts.ListItems.Add(new ListItem(new
                Paragraph(new Run("BlokcUIContainer allows you to embed WPF controls in the document!"))));
            Run prefix = new Run("This paragraph was generated ");

            Bold b = new Bold();
            Run infix = new Run("dynamically");
            infix.Foreground = Brushes.Red;
            infix.FontSize = 30;
            b.Inlines.Add(infix);

            Run suffix = new Run(" at runtime!");
            paraBodyText.Inlines.Add(prefix);
            paraBodyText.Inlines.Add(infix);
            paraBodyText.Inlines.Add(suffix);
        }

        private void EnableAnnotations()
        {
            AnnotationService anoService = new AnnotationService(myDocumentReader);
            using (MemoryStream anoStream = new MemoryStream())
            {
                anoService.Enable(new XmlStreamStore(anoStream));
            }
        }
    }
}
