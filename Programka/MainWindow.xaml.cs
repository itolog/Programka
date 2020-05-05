using Microsoft.Win32;
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
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace Programka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string FilePath { get; private set; }

        ObservableCollection<string> taskList;
        private List<Process> myProc = new List<Process>();

        public MainWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            taskList = new ObservableCollection<string> { };
            listOfTasks.ItemsSource = taskList;
            // listOfTasks.ToolTip = "delete";
        }

        // GET FILE PATH
        private void Button_Open_File(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                filePathLabel.Content = FilePath;
            }

        }

        // ADD COMMAND FOR {TASK}.exe
        private void Button_Add_Task(object sender, RoutedEventArgs e)
        {
            // Arguments for Task from Input
            string commands = taskArgs.Text.Trim();

            if (FilePath != null)
            {
                try
                {
                    taskList.Add(FilePath);

                    // Clear State
                    taskArgs.Text = "";
                    FilePath = null;
                    filePathLabel.Content = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Pick Folder");
            }
        }

        // REMOVE TASK FROM taskList
        private void list_Selected(object sender, RoutedEventArgs e)
        {
            if (listOfTasks != null)
            {
                try
                {
                    var p = listOfTasks.SelectedItem.ToString();
                    taskList.Remove(p);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in taskList)
                {

                    myProc.Add(Process.Start(item));
                }

                ButtonStart.IsEnabled = false;
                ButtonCancelTasks.IsEnabled = true;
                listOfTasks.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Cancel_Tasks(object sender, RoutedEventArgs e)
        {

            try
            {
                foreach (var item in myProc)
                {

                    item.Kill();
                    ButtonStart.IsEnabled = true;
                    listOfTasks.IsEnabled = true;
                    ButtonCancelTasks.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
