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
using System.IO;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace DisplayBixlerPath
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        FDRFileParse? _fDRFileParse;
        string? _fileName;
        string? _fileNameOut;
        string? _selectedSession;
        public ObservableCollection<string>? _names { get; set; }

        public MainWindow()
        { 
    	    InitializeComponent();
            DataContext = this;
        }

        /// Button handler for selection of the data file
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Vector files (*.FDR)|*.FDR";
            if (openFileDialog.ShowDialog() == true)
            {
                _fileName = openFileDialog.FileName;
                var lines = File.ReadAllLines(_fileName);
                _fDRFileParse = new FDRFileParse(lines);
                _names = _fDRFileParse.SessionNames();
                OnPropertyChanged("_names"); // notify the system that the _names collection has updated and then the binding to the view model will update the screen
                _fileNameOut = createOutputFileName(_fileName);
            }
            else
            {/// jgs shorty - fix 
                 _fileName = String.Empty;
                _fileNameOut = String.Empty;
                _names = new ObservableCollection<string>(new List <string>());
            }

        }

        /// Button handler for the extraction and writing of the actual route 
        private void btnExtractRoute_Click(object sender, RoutedEventArgs e)
        {
            _names = new ObservableCollection<string>(new List<string>());
            OnPropertyChanged();// not actually essential as the buton push is not updating any state in the model view connected to the view model 
            if(null!=_fDRFileParse)
            {
                List<CoordinateTriple> coordinateTripleList = _fDRFileParse.ExtractAllKMLPath();

                string kmlPath = new KMLPath(coordinateTripleList).GetPath();
                if (_fileNameOut != null && _fileNameOut != String.Empty)
                {
                    // does the file already exist and if it does do we over write it ? JGS todo
                    using (StreamWriter writer = new StreamWriter(_fileNameOut))
                    {
                        writer.WriteLine(kmlPath);
                    }
                }
                else
                { // JGS to do error indication
                  // try again ??? 
                }
            }
        }

        private string createOutputFileName(string outputFileName)
        {
            return System.IO.Path.GetDirectoryName(outputFileName) +"\\output1.kml";
        }

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = " ")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        /// function to extract the selected session for display 
        /// JGS seems a bit long winded https://www.c-sharpcorner.com/UploadFile/mahesh/wpf-combobox/
        private void cmbSessions_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selectedSession = (string)cmbSessions.SelectedItem;
            if (!string.IsNullOrEmpty(selectedSession))
            {
                _selectedSession = selectedSession;
            }
            else
            {

            }
        }


    }
}
