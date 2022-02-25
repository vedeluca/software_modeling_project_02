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
using JsonProcessing.Values;
using JsonProcessing.Util;
using System.IO;

namespace JsonUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _filter;
        private DataNode _node;
        private Stack<DataValue> _values;
        private DataValue _previous;
        public MainWindow()
        {
            InitializeComponent();
            _filter = "JSON files (*.json)|*.json";
            _node = new DataNode(new JsonObject());
            _values = new Stack<DataValue>();
            _previous = new DataValue(new JsonValue());
        }

        private void MenuOpen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = _filter;
            if (openFileDialog.ShowDialog() == true)
            {
                DataFileParser fileParser = new(new JsonFileParser());
                try
                {
                    _node = fileParser.ParseDataFile(openFileDialog.FileName);
                    testText.Text = _node.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Open File", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
            if (testText.Text.Length == 0 || SaveCheck("New File"))
                testText.Text = "";
        }

        private bool SaveCheck(string name)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to save changes?", name, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                bool save = SaveJson();
                if (save)
                    return true;
            }
            else if (result == MessageBoxResult.No)
            {
                return true;
            }
            return false;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            if (testText.Text.Length == 0 || SaveCheck("Exit"))
                Application.Current.Shutdown();
        }

        private void SearchJson(object sender, RoutedEventArgs e)
        {
            try
            {
                DataValue query = _node.Query(SearchBar.Text);
                testText.Text = query.ToString();
                if (_previous.Type != DataType.Empty)
                    _values.Push(_previous);
                _previous = query;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search JSON", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ClearJson(object sender, RoutedEventArgs e)
        {
            testText.Text = _node.ToString();
            SearchBar.Text = "";
        }

        private void BackJson(object sender, RoutedEventArgs e)
        {
            if (_values.Count > 0)
                testText.Text = _values.Pop().ToString();
            else
                testText.Text = _node.ToString();
        }
    }
}
