using FWFL;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;

namespace FWFL_Layout
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public string fileName;
        private bool IsIndeterminate;

        List<List<int>> solutionLists = new();
        Dictionary<int, string> wordsLookUp = new();

        protected void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null) 
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _percent = 0;

        public int Percent
        {
            get { return _percent; }
            set { _percent = value; NotifyPropertyChange("Percent"); }
        }
        private int _sol = 0;

        public int Sol
        {
            get { return _sol; }
            set { _sol = value; NotifyPropertyChange("Sol"); }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this; 

        }

        //Kører programmet
        private async void Button_Click(object sender, EventArgs e)
        {
            IsIndeterminate = true;
            Class1 fwfl = new Class1();
            fwfl.SearchIndex += SearchThread_SearchIndex;
            await fwfl.Work(fileName);
            wordsLookUp = fwfl.wordsLookUp;
            Sol = fwfl.solution;
            solutionLists = fwfl.ints;
        }

        private void SearchThread_SearchIndex(object? sender, int e)
        {
            if (IsIndeterminate) IsIndeterminate = false;
            Percent = e;
            Sol = ((Class1)sender).solution;
        }

        //Brugt til at vælge en fil at læse fra
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "TXT Files (*.txt) | *.txt";


            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                fileName = dlg.FileName;
            }
        }
        //Gemmer løsninger til fil. 
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<string> solutions = new();
            foreach (List<int> list in solutionLists) 
            {
                string resultat = "";

                for (int i = 0; i < list.Count -1; i++)
                {
                    resultat += $"{wordsLookUp[list[i]]} ";
                }
                resultat += $"{wordsLookUp[list[^1]]}";
                solutions.Add(resultat);
            }

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Title = "";
            dlg.Filter = "txt file (*.txt) | *.txt";

            if (dlg.ShowDialog() == true)
            {
                File.WriteAllLines(dlg.FileName, solutions);

            }
        }
    }
}