using System;
using System.IO;
using System.Windows;
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
            
            save.DefaultExt = String.Empty;
            save.FileName = FindFileName(filePath.Text);

            if (String.IsNullOrEmpty(save.FileName))
            {
                MessageBox.Show("[FILE] Invalid file name!");
                return;
            }

            if (save.ShowDialog() == true)
            {
                File.WriteAllBytes(save.FileName, Engarbler.Compute(filePath.Text, passwordBox.Text));
            }
            

        }

        private static string FindFileName(string filePath)
        {
            string ext = Path.GetExtension(filePath);

            if (ext == ".engarble")
            {
                return Path.GetFileNameWithoutExtension(filePath);
            }

            return Path.GetFileName(filePath) + ".engarble";
        }

    }
}
