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
using System.Windows.Threading;

namespace WpfParallalProcessingTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool isProcessing = false;
        public MainWindow()
        {
            InitializeComponent();

            #region define timer with 5 sec
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Start();
            #endregion
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isProcessing)
            {
                GoToNextView();
            }
        }

        private async void GoToNextView()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            await ProcessAsyncTaskParallel();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("elapsedMs :  " + elapsedMs);
        }


        private async Task ProcessAsyncTaskParallel()
        {
            string[] FileNames = { "File1", "File2", "File3", "File4", "File6" };
            Console.WriteLine("1 isProcessing: " + isProcessing);
            isProcessing = true;
            if (FileNames.Length != 0)
            {

                List<Task<string>> tasks = new List<Task<string>>();

                foreach (string fileName in FileNames.Take(2))
                {
                    tasks.Add(RunFileProcessParallel(fileName));
                }
                var results = await Task.WhenAll(tasks);
                Console.WriteLine("Processed Finished: "+ String.Join(", ", results.ToArray()) + ", isProcessing :" + isProcessing);
                isProcessing = false;
                Console.WriteLine("2 isProcessing: " + isProcessing);
            }
        }

        private async Task<string> RunFileProcessParallel(string fileName)
        {
            string processStatus = "";
            Console.WriteLine("Started -ProcessFile In MainForm. File :" + fileName);
            BusinessLogic bLogic = new BusinessLogic();
            processStatus = await bLogic.RunFileProcessLogicsParallel(fileName);

            Console.WriteLine("End -ProcessFile In MainForm. File :" + fileName);
            return processStatus;
        }
    }
}
