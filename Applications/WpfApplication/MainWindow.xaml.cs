using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ionic.Zip;
using System.IO;
using System.Diagnostics;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constants

        /// <summary>
        /// Python Zip Path
        /// - The relative filepath of the python zip file
        /// - Used to extract python for the first time
        /// </summary>
        private static readonly string PythonZipPath = @"Compressed\Python.zip";

        /// <summary>
        /// Python Unpack Directory
        /// - The relative folder path where python should be extracted to
        /// </summary>
        private static readonly string PythonUnpackDirectory = @"Scripts";

        /// <summary>
        /// Python Extracted Path
        /// - The final path that the python folder will be after being extracted
        /// </summary>
        private static readonly string PythonExtractedPath = string.Format(@"{0}\Python", MainWindow.PythonUnpackDirectory);

        /// <summary>
        /// Virtualenv Path
        /// - Used to create virtual environments
        /// </summary>
        private static readonly string VirtualenvPath = @"Scripts\virtualenv.py";

        /// <summary>
        /// Virtualenv Path
        /// - Used to create virtual environments
        /// </summary>
        private static readonly string GlobalVirtualenvDirectory = @"Env\Global";

        /// <summary>
        /// Virtualenv Path
        /// - Used to create virtual environments
        /// </summary>
        private static readonly string BaseVirtualenvPath = string.Format(@"{0}\Base", MainWindow.GlobalVirtualenvDirectory);

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.StartupCheck();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Startup Check
        /// </summary>
        private void StartupCheck()
        {
            //Check if python has been extracted
            if (!Directory.Exists(MainWindow.PythonExtractedPath))
            {
                //Python has not been extracted, let's do that
                this.ExtractPython();
            }
            else
            {
                //Python has already been extracted,
                //Do nothing
            }

            //Check if base environment has been set up
            if (!Directory.Exists(MainWindow.BaseVirtualenvPath))
            {
                //Base environment is not set up, le's do that
                this.CreateBaseEnvironment();
            }
            else
            {
                //Base environment is already set up,
                //Do nothing
            }
        }

        /// <summary>
        /// Extract Python
        /// </summary>
        private void ExtractPython()
        {
            using (ZipFile zip = ZipFile.Read(MainWindow.PythonZipPath))
            {
                foreach (ZipEntry entry in zip)
                {
                    entry.Extract(MainWindow.PythonUnpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }

        /// <summary>
        /// Create Base Environment
        /// </summary>
        private void CreateBaseEnvironment()
        {
            //Make sure directories exist
            if (!Directory.Exists(@"Env"))
            {
                Directory.CreateDirectory(@"Env");
            }

            if (!Directory.Exists(@"Env\Global"))
            {
                Directory.CreateDirectory(@"Env\Global");
            }

            var startInfo = new ProcessStartInfo() {
                UseShellExecute = true,
                //FileName = string.Format(@"{0}\python.exe {1} {2} --distribute", MainWindow.PythonExtractedPath, MainWindow.VirtualenvPath, BaseVirtualenvPath),
                FileName = @"Scripts\Python\python.exe Scripts\virtualenv.py",
            };

            try
            {
                var process = new Process()
                {
                    StartInfo = startInfo,
                }.Start();
            }
            catch
            {

            }
        }

        #endregion
    }
}
