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
using Microsoft.Win32;
using JsonProcessing.Files;
using JsonProcessing.Objects;
using System.IO;

namespace JsonUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _filter;
        public MainWindow()
        {
            InitializeComponent();
            _filter = "JSON files (*.json)|*.json";
        }

        private void MenuOpen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = _filter;
            if (openFileDialog.ShowDialog() == true)
            {
                DataFileParser fileParser = new(new JsonFileParser());
                DataNode node = fileParser.ParseDataFile(openFileDialog.FileName);
                testText.Text = node.ToString();
            }
        }

        private void MenuSave(object sender, RoutedEventArgs e)
        {
            SaveJson();
        }

        private bool SaveJson()
        {
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = _filter;
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, testText.Text);
                return true;
            }
            return false;
        }

        private void MenuNew(object sender, RoutedEventArgs e)
        {
            if (testText.Text.Length == 0)
                return;
            MessageBoxResult result = MessageBox.Show("Do you want to save changes?", "New File", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                bool save = SaveJson();
                if (save)
                    NewJson();
            }
            else if (result == MessageBoxResult.No)
            {
                NewJson();
            }
        }

        private void NewJson()
        {
            testText.Text = "";
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
