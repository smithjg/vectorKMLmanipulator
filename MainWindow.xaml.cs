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

namespace DisplayBixlerPath
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FDRFileParse? _fDRFileParse;
        string? _fileName;

        public MainWindow()
        {            
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Vector files (*.FDR)|*.FDR";
            if (openFileDialog.ShowDialog() == true)
            {
                _fileName = openFileDialog.FileName;
                var lines = File.ReadAllLines(_fileName);
                _fDRFileParse = new FDRFileParse(lines);
            }
            else
            {/// jgs shorty - fix 
                 _fileName = String.Empty;
            }
        }

        private void btnExtractRoute_Click(object sender, RoutedEventArgs e)
        {
            if(null!=_fDRFileParse)
            { 
                string outFile = @"C:\Projects\KMLProject\MyTest.txt";
                List < CoordinateTriple > coordinateTripleList = _fDRFileParse.ExtractKMLCoordinates();

                string kmlPath = new KMLPath(coordinateTripleList).GetPath();
            
                if (!File.Exists(outFile))
                {
                    File.WriteAllText(outFile, kmlPath, Encoding.UTF8);
                }
            }
        }
    }
}
