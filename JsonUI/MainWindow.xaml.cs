using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
        private DataNode _root;
        private Stack<QueriedValue> _values;
        private QueriedValue _current;

        public MainWindow()
        {
            InitializeComponent();
            _filter = "JSON files (*.json)|*.json";
            _root = new DataNode(new JsonObject());
            _values = new Stack<QueriedValue>();
            _current = new QueriedValue();
        }

        private void MenuOpen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = _filter;
            if (openFileDialog.ShowDialog() == true)
            {
                DataFileParser fileParser = new();
                try
                {
                    _root = fileParser.ParseDataFile(openFileDialog.FileName);
                    StartTree(_root);
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
                File.WriteAllText(saveFileDialog.FileName, _root.ToString());
                return true;
            }
            return false;
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
            if (SaveCheck("Exit"))
                Application.Current.Shutdown();
        }

        private void SearchJson(object sender, RoutedEventArgs e)
        {
            try
            {
                string key = SearchBar.Text;
                DataValue value = _root.Query(key);
                QueriedValue query = new QueriedValue(key, value);
                StartTree(query);
                if (_current.Value.Type != DataType.Empty)
                    _values.Push(_current);
                _current = query;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search JSON", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackJson(object sender, RoutedEventArgs e)
        {
            if (_values.Count > 0)
                StartTree(_values.Pop());
            else
                StartTree(_root);
        }

        private void AddJson(object sender, RoutedEventArgs e)
        {
            try
            {
                string json = JsonText.Text;
                DataStringParser parser = new(new JsonStringParser());
                DataNode child = parser.ParseDataString(json);
                DataValue value = _current.Value;
                if (value.Type != DataType.Object && value.Type != DataType.Array)
                    throw new Exception("This JSON string can not be added to the current subtree. Only add to objects or arrays.");
                DataNode parent = (DataNode)value.GetValue();
                child.Parent = parent;
                child.Root = (parent.Root == null) ? parent : parent.Root;
                parent.Add("test", new DataValue(new JsonValue(child)));
                StartTree(_current);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add JSON", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartTree(DataNode node)
        {
            JsonTree.Items.Clear();
            TreeViewItem root = new();
            JsonTree.Items.Add(FillTree(node, root));
        }

        private void StartTree(QueriedValue query)
        {
            JsonTree.Items.Clear();
            TreeViewItem root = new();
            root.Header = query.Key;
            JsonTree.Items.Add(AddToTree(query.Value, root));
        }

        private TreeViewItem AddToTree(DataValue value, TreeViewItem trunk)
        {
            if (value.Type == DataType.Empty)
                return trunk;
            if (value.Type == DataType.Object || value.Type == DataType.Array)
            {
                DataNode node = (DataNode)value.GetValue();
                return FillTree(node, trunk);
            }
            TreeViewItem branch = new();
            branch.Header = value.ToString();
            trunk.Items.Add(branch);
            return trunk;
        }

        private TreeViewItem FillTree(DataNode node, TreeViewItem trunk)
        {
            for (int i = 0; i < node.Count; i++)
            {
                DataValue dataValue = node.GetValueAt(i);
                TreeViewItem branch = AddToTree(dataValue, new TreeViewItem());
                branch.Header = node.GetKeyAt(i);
                trunk.Items.Add(branch);
            }
            return trunk;
        }

        private class QueriedValue
        {
            public string Key { get; set; }
            public DataValue Value { get; set; }
            public QueriedValue()
            {
                Key = "";
                Value = new DataValue();
            }
            public QueriedValue(string key, DataValue value) { Key = key; Value = value; }
        }
    }
}
