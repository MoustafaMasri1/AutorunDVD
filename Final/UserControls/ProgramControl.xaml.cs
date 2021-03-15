using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;


namespace Final
{
    public partial class ProgramControl : UserControl
    {
        private string getProgramNameOnClick;
        public ProgramControl()
        {
            InitializeComponent();
        }
        // get program name
        public string ProgramName
        {
            get => ItemName.Text;
            set
            {
                ItemName.Text = value;
            }
        }
        // click event for all install button changing by ProgramName
        private void ItemButton_Click(object sender, RoutedEventArgs e)
        {
            string defaultPath = AppDomain.CurrentDomain.BaseDirectory + "..\\Programs\\" + ProgramName;
            string[] x64File = new string[1];
            x64File[0] = null;
            string[] x32File = new string[1];
            x32File[0] = null;
            string[] SetupFile = new string[1];
            SetupFile[0] = null;
            string[] MsiFile = new string[1];
            MsiFile[0] = null;
            string[] unknownFile = new string[1];
            unknownFile[0] = null;
            getProgramNameOnClick = ProgramName;
            // if 64BitOperatingSystem Open x64 file else open other file
            if (Environment.Is64BitOperatingSystem)
            {
                x64File = Directory.GetFiles(defaultPath, "x64.exe", SearchOption.AllDirectories);
                if (x64File.Length > 0)
                {
                    if (File.Exists(x64File[0]))
                    {
                        ProcessInfo(x64File[0]);
                    }
                }
                x32File = Directory.GetFiles(defaultPath, "x32.exe", SearchOption.AllDirectories);
                if (x32File.Length > 0 && x64File.Length == 0)
                {
                    if (File.Exists(x32File[0]))
                    {
                        ProcessInfo(x32File[0]);
                    }
                }
                MsiFile = Directory.GetFiles(defaultPath, "Setup.msi", SearchOption.AllDirectories);
                if (MsiFile.Length > 0 && x64File.Length == 0 && x32File.Length == 0)
                {
                    if (File.Exists(MsiFile[0]))
                    {
                        ProcessInfo(MsiFile[0]);
                    }
                }
                SetupFile = Directory.GetFiles(defaultPath, "Setup.exe", SearchOption.AllDirectories);
                if (SetupFile.Length > 0 && x64File.Length == 0 && x32File.Length == 0 && MsiFile.Length == 0)
                {
                    if (File.Exists(SetupFile[0]))
                    {
                        ProcessInfo(SetupFile[0]);
                    }
                }
                unknownFile = Directory.GetFiles(defaultPath, "*.exe*", SearchOption.AllDirectories);
                if (unknownFile.Length > 0 && x64File.Length == 0 && x32File.Length == 0 && MsiFile.Length == 0 && SetupFile.Length == 0)
                {
                    if (File.Exists(unknownFile[0]))
                    {
                        ProcessInfo(unknownFile[0]);
                    }
                }

            }
            // if 32BitOperatingSystem Open x32 file else open other file
            if (!Environment.Is64BitOperatingSystem)
            {
                x32File = Directory.GetFiles(defaultPath, "x32.exe", SearchOption.AllDirectories);
                if (x32File.Length > 0)
                {
                    if (File.Exists(x32File[0]))
                    {
                        ProcessInfo(x32File[0]);
                    }
                }
                x64File = Directory.GetFiles(defaultPath, "x64.exe", SearchOption.AllDirectories);
                if (x64File.Length > 0 && x32File.Length == 0)
                {
                    if (File.Exists(x64File[0]))
                    {
                        ProcessInfo(x64File[0]);
                    }
                }

                MsiFile = Directory.GetFiles(defaultPath, "Setup.msi", SearchOption.AllDirectories);
                if (MsiFile.Length > 0 && x64File.Length == 0 && x32File.Length == 0)
                {
                    if (File.Exists(MsiFile[0]))
                    {
                        ProcessInfo(MsiFile[0]);
                    }
                }
                SetupFile = Directory.GetFiles(defaultPath, "Setup.exe", SearchOption.AllDirectories);
                if (SetupFile.Length > 0 && x64File.Length == 0 && x32File.Length == 0 && MsiFile.Length == 0)
                {
                    if (File.Exists(SetupFile[0]))
                    {
                        ProcessInfo(SetupFile[0]);
                    }
                }
                unknownFile = Directory.GetFiles(defaultPath, "*.exe*", SearchOption.AllDirectories);
                if (unknownFile.Length > 0 && x64File.Length == 0 && x32File.Length == 0 && MsiFile.Length == 0 && SetupFile.Length == 0)
                {
                    if (File.Exists(unknownFile[0]))
                    {
                        ProcessInfo(unknownFile[0]);
                    }
                }
            }


        }
        // open crack folder if exist and open readme.txt if exist after install exit
        void p_Exited(object sender, EventArgs e)
        {
            string crackPath = AppDomain.CurrentDomain.BaseDirectory + @"..\Programs\" + getProgramNameOnClick;
            if (Directory.Exists(crackPath + "\\Crack"))
            {
                Process.Start(crackPath + "\\Crack");
                if (File.Exists(crackPath + "\\Crack\\" + "Readme.txt"))
                {
                    Thread.Sleep(1000);
                    Process.Start(crackPath + "\\Crack\\" + "Readme.txt");
                }
            }
            TempoFile();
        }
        // write program info to temp file to know if the program installed or not
        private void TempoFile()
        {
            string[] tempoFile = File.ReadAllLines(Environment.GetEnvironmentVariable("userprofile") + "\\AppData\\Local\\Temp\\Alafandi & Sbahi\\" + getProgramNameOnClick + "\\" + getProgramNameOnClick + ".txt");
            StreamWriter streamWriter = new StreamWriter(Environment.GetEnvironmentVariable("userprofile") + "\\AppData\\Local\\Temp\\Alafandi & Sbahi\\" + getProgramNameOnClick + "\\" + getProgramNameOnClick + ".txt");
            streamWriter.WriteLine(getProgramNameOnClick);
            streamWriter.WriteLine(tempoFile[1]);
            streamWriter.WriteLine(tempoFile[2]);
            streamWriter.WriteLine(tempoFile[3]);
            streamWriter.WriteLine(tempoFile[4]);
            streamWriter.WriteLine("Installed Successfully");
            streamWriter.WriteLine("Check");
            streamWriter.Close();
        }
        // void for process info 
        public void ProcessInfo(string path)
        {
            Process p = new Process();
            p.Exited += new EventHandler(p_Exited);
            p.StartInfo.FileName = path;
            p.EnableRaisingEvents = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.Start();
            
        }
    }
}
