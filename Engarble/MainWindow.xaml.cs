using System;
using System.IO;
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
using Path = System.IO.Path;

namespace Engarble
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void fileSelect_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog file = new Microsoft.Win32.OpenFileDialog();

            bool? select = file.ShowDialog();

            if (select.HasValue)
                filePath.Text = file.FileName;

        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog save = new Microsoft.Win32.SaveFileDialog();
            save.DefaultExt = FindExtension(filePath.Text);
            save.FileName = FindFileName(filePath.Text, save.DefaultExt);

            if (String.IsNullOrEmpty(save.FileName))
            {
                MessageBox.Show("[FILE] Invalid file name!");
                return;
            }

            if (String.IsNullOrEmpty(save.DefaultExt))
            {
                MessageBox.Show("[EXT] Invalid file name!");
                return;
            }

            if (save.ShowDialog() == true)
            {
                File.WriteAllBytes(save.FileName, Engarbler.Compute(filePath.Text, passwordBox.Text));
            }
            

        }

        private string FindFileName(string filePath, string ext)
        {
            if (ext != ".engarble")
            {
                return Path.GetFileNameWithoutExtension(filePath);
            }

            return Path.GetFileNameWithoutExtension(Path.ChangeExtension(filePath, null));
        }

        private string FindExtension(string filePath)
        {
            string ext = Path.GetExtension(filePath);

            if (ext != ".engarble")
            {
                return $"{ext}.engarble";
            }

            return Path.GetExtension(Path.ChangeExtension(filePath, null));
        }

    }
}
