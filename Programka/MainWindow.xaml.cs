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

namespace Programka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string FilePath { get; private set; }

        List<TaskData> taskList = new List<TaskData>(); 

        public MainWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // GET FILE PATH
        private void Button_Open_File(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName; 
            }
            
            Console.WriteLine("Opened");
        }

        // ADD COMMAND FOR {TASK}.exe
        private void Button_Add_Task(object sender, RoutedEventArgs e)
        {
            string commands = taskArgs.Text.Trim();
            if(commands != "")
            {
                if(FilePath != null)
                {
                    string taskCommand = $"{FilePath}:  {commands}";
                    taskList.Add(new TaskData(FilePath, commands));
                 
                  
                   listOfTasks.Items.Add(taskCommand);
                     
                    taskArgs.Text = "";
                }
                else
                {
                    MessageBox.Show("Pick Folder");
                }
              
            }
        }

        // REMOVE TASK FROM taskList
        private void list_Selected(object sender, RoutedEventArgs e)
        {
            var p = listOfTasks.SelectedItem as string;
            MessageBox.Show(p);
        }

        private  void Button_Start(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in taskList)
                {
                     Process.Start(item.FilePath, item.FileCommand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
