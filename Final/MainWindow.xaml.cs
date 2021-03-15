using Final.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;


namespace Final
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        #region Variables
        public MediaPlayer mediaPlayer = new MediaPlayer();
        public CollectionView view;
        public string Filter;
        public string[] DefualtDirectory;
        public string[] ProgramInfo;
        public string[] TempInfo;
        public FileInfo fileInfo;
        public string FolderName;
        public decimal fileSize;
        List<CLS_ProgramInfo> programs = new List<CLS_ProgramInfo>();
        public DispatcherTimer RefreshTimer = new DispatcherTimer();
        #endregion

        #region AddPrograms

        // write program info to temp files in "C:\\Temp"
        public void WriteTempFiles()
        {
            // defualt folder for programs
            DefualtDirectory = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "..\\Programs\\");
            for (int i = 0; i < DefualtDirectory.Length; i++)
            {
                // read all folder names 
                FolderName = Path.GetFileName(Path.GetDirectoryName(DefualtDirectory[i] + "\\"));
                // Read all program info from txt file
                try
                {
                    ProgramInfo = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "..\\Programs\\" + FolderName + "\\" + FolderName + ".txt");
                }
                catch (Exception)
                {
                    continue;
                }
                // Write all program info to temp folder
                if (!Directory.Exists(Environment.GetEnvironmentVariable("userprofile") + "\\AppData\\Local\\Temp\\Alafandi & Sbahi\\" + FolderName))
                {
                    Directory.CreateDirectory(Environment.GetEnvironmentVariable("userprofile") + "\\AppData\\Local\\Temp\\Alafandi & Sbahi\\" + FolderName);
                }
                if (!File.Exists(Environment.GetEnvironmentVariable("userprofile") + "\\AppData\\Local\\Temp\\Alafandi & Sbahi\\" + FolderName + "\\" + FolderName + ".txt"))
                {
                    StreamWriter streamWriter = new StreamWriter(Environment.GetEnvironmentVariable("userprofile") + "\\AppData\\Local\\Temp\\Alafandi & Sbahi\\" + FolderName + "\\" + FolderName + ".txt");
                    streamWriter.WriteLine(FolderName);
                    streamWriter.WriteLine(ProgramInfo[1]);
                    streamWriter.WriteLine(ProgramInfo[2]);
                    streamWriter.WriteLine(ProgramInfo[3]);
                    streamWriter.WriteLine(ProgramInfo[4]);
                    streamWriter.WriteLine("Install");
                    streamWriter.WriteLine("Download");
                    streamWriter.Close();
                }
                itPrograms.ItemsSource = programs;
                view = (CollectionView)CollectionViewSource.GetDefaultView(itPrograms.ItemsSource);
                view.Filter = SearchByName;

            }
        }

        public void AddProgram()
        {
            programs.Clear();
            DefualtDirectory = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "..\\Programs\\");
            for (int i = 0; i < DefualtDirectory.Length; i++)
            {
                // Get All folder name in main folder
                FolderName = Path.GetFileName(Path.GetDirectoryName(DefualtDirectory[i] + "\\"));


                #region Get file size

                string defaultPath = AppDomain.CurrentDomain.BaseDirectory + "..\\Programs\\" + FolderName;
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

                // if opreating system is 64bit get x64 size
                if (Environment.Is64BitOperatingSystem)
                {
                    x64File = Directory.GetFiles(defaultPath, "*64*.exe", SearchOption.AllDirectories);
                    if (x64File.Length > 0)
                    {
                        if (File.Exists(x64File[0]))
                        {
                            fileInfo = new FileInfo(x64File[0]);
                            fileSize = (fileInfo.Length / 1024) / 1024;
                        }
                    }
                    x32File = Directory.GetFiles(defaultPath, "*32*.exe", SearchOption.AllDirectories);
                    if (x32File.Length > 0 && x64File.Length == 0)
                    {
                        if (File.Exists(x32File[0]))
                        {
                            fileInfo = new FileInfo(x32File[0]);
                            fileSize = (fileInfo.Length / 1024) / 1024;
                        }
                    }
                    MsiFile = Directory.GetFiles(defaultPath, "Setup.msi", SearchOption.AllDirectories);
                    if (MsiFile.Length > 0 && x64File.Length == 0 && x32File.Length == 0)
                    {
                        if (File.Exists(MsiFile[0]))
                        {
                            fileInfo = new FileInfo(MsiFile[0]);
                            fileSize = (fileInfo.Length / 1024) / 1024;
                        }
                    }
                    SetupFile = Directory.GetFiles(defaultPath, "Setup.exe", SearchOption.AllDirectories);
                    if (SetupFile.Length > 0 && x64File.Length == 0 && x32File.Length == 0 && MsiFile.Length == 0)
                    {
                        if (File.Exists(SetupFile[0]))
                        {
                            fileInfo = new FileInfo(SetupFile[0]);
                            fileSize = (fileInfo.Length / 1024) / 1024;
                        }
                    }
                    unknownFile = Directory.GetFiles(defaultPath, "*.exe*", SearchOption.AllDirectories);
                    if (unknownFile.Length > 0 && x64File.Length == 0 && x32File.Length == 0 && MsiFile.Length == 0 && SetupFile.Length == 0)
                    {
                        if (File.Exists(unknownFile[0]))
                        {
                            fileInfo = new FileInfo(unknownFile[0]);
                            fileSize = (fileInfo.Length / 1024) / 1024;
                        }
                    }
                    if (unknownFile.Length == 0 && x64File.Length == 0 && x32File.Length == 0 && MsiFile.Length == 0 && SetupFile.Length == 0)
                    {
                        fileSize = 0;
                    }
                }

                // if opreating system is 32bit get x32 size
                if (!Environment.Is64BitOperatingSystem)
                {

                    x32File = Directory.GetFiles(defaultPath, "*32*.exe", SearchOption.AllDirectories);
                    if (x32File.Length > 0)
                    {
                        if (File.Exists(x32File[0]))
                        {
                            fileInfo = new FileInfo(x32File[0]);
                            fileSize = (fileInfo.Length / 1024) / 1024;
                        }
                    }
                    x64File = Directory.GetFiles(defaultPath, "*64*.exe", SearchOption.AllDirectories);
                    if (x64File.Length > 0 && x32File.Length == 0)
                    {
                        if (File.Exists(x64File[0]))
                        {
                            fileInfo = new FileInfo(x64File[0]);
                            fileSize = (fileInfo.Length / 1024) / 1024;
                        }
                    }
                    MsiFile = Directory.GetFiles(defaultPath, "Setup.msi", SearchOption.AllDirectories);
                    if (MsiFile.Length > 0 && x64File.Length == 0 && x32File.Length == 0)
                    {
                        if (File.Exists(MsiFile[0]))
                        {
                            fileInfo = new FileInfo(MsiFile[0]);
                            fileSize = (fileInfo.Length / 1024) / 1024;
                        }
                    }
                    SetupFile = Directory.GetFiles(defaultPath, "Setup.exe", SearchOption.AllDirectories);
                    if (SetupFile.Length > 0 && x64File.Length == 0 && x32File.Length == 0 && MsiFile.Length == 0)
                    {
                        if (File.Exists(SetupFile[0]))
                        {
                            fileInfo = new FileInfo(SetupFile[0]);
                            fileSize = (fileInfo.Length / 1024) / 1024;
                        }
                    }
                    unknownFile = Directory.GetFiles(defaultPath, "*.exe*", SearchOption.AllDirectories);
                    if (unknownFile.Length > 0 && x64File.Length == 0 && x32File.Length == 0 && MsiFile.Length == 0 && SetupFile.Length == 0)
                    {
                        if (File.Exists(unknownFile[0]))
                        {
                            fileInfo = new FileInfo(unknownFile[0]);
                            fileSize = (fileInfo.Length / 1024) / 1024;
                        }
                    }
                    if (unknownFile.Length == 0 && x64File.Length == 0 && x32File.Length == 0 && MsiFile.Length == 0 && SetupFile.Length == 0)
                    {
                        fileSize = 0;
                    }
                }
                #endregion
                // Read all program info from txt file in temp folder
                try
                {
                    TempInfo = File.ReadAllLines(Environment.GetEnvironmentVariable("userprofile") + "\\AppData\\Local\\Temp\\Alafandi & Sbahi\\" + FolderName + "\\" + FolderName + ".txt");
                }
                catch (Exception)
                {
                    continue;
                }
                // After read all lines add programs for ItemControl
                programs.Add(new CLS_ProgramInfo()
                {
                    ProgramImage = AppDomain.CurrentDomain.BaseDirectory + "..\\Programs\\" + FolderName + "\\" + FolderName + ".png",
                    ProgramName = TempInfo[0],
                    ProgramDescription = TempInfo[1],
                    ProgramToolTip = TempInfo[1],
                    ProgramOS = TempInfo[2],
                    ProgramOS32OR64 = TempInfo[3],
                    ProgramSize = fileSize.ToString(),
                    ProgramType = TempInfo[4],
                    ButtonToolTip = TempInfo[5],
                    Icon = TempInfo[6]
                });

            }

        }
        #endregion

        #region Filters
        private void Btn_AllProgram_Selected(object sender, EventArgs e)
        {
            AllPrograms_Grid.Visibility = Visibility.Visible;
            SelectedItemGrid.Visibility = Visibility.Hidden;
            AboutUs_Grid.Visibility = Visibility.Hidden;
            Filter = txtSearch.Text;
            CollectionViewSource.GetDefaultView(itPrograms.ItemsSource).Refresh();
            NoResult.Visibility = Visibility.Visible;
            NoResult.Text = "All Programs : " + itPrograms.Items.Count;
            SV_ProgramList.Visibility = Visibility.Visible;
            IsSelectedAllProgram();
        }
        private void Btn_Antivirus_Selected(object sender, RoutedEventArgs e)
        {
            AllPrograms_Grid.Visibility = Visibility.Hidden;
            SelectedItemGrid.Visibility = Visibility.Visible;
            AboutUs_Grid.Visibility = Visibility.Hidden;
            SV_ProgramList.Visibility = Visibility.Visible;
            view.Filter = FilterByType;
            Filter = "Antivirus";
            CollectionViewSource.GetDefaultView(itPrograms.ItemsSource).Refresh();
            NoResult.Visibility = Visibility.Visible;
            NoResult.Text = "Antivirus Program";
            btn_AllProgram.IsSelected = false;
        }
        private void Btn_Graphic_Selected(object sender, RoutedEventArgs e)
        {
            AllPrograms_Grid.Visibility = Visibility.Hidden;
            SelectedItemGrid.Visibility = Visibility.Visible;
            AboutUs_Grid.Visibility = Visibility.Hidden;
            SV_ProgramList.Visibility = Visibility.Visible;
            view.Filter = FilterByType;
            Filter = "Graphic";
            CollectionViewSource.GetDefaultView(itPrograms.ItemsSource).Refresh();
            NoResult.Visibility = Visibility.Visible;
            NoResult.Text = "Graphic Program";
            btn_AllProgram.IsSelected = false;
        }
        private void Btn_Internet_Selected(object sender, RoutedEventArgs e)
        {
            AllPrograms_Grid.Visibility = Visibility.Hidden;
            SelectedItemGrid.Visibility = Visibility.Visible;
            AboutUs_Grid.Visibility = Visibility.Hidden;
            SV_ProgramList.Visibility = Visibility.Visible;
            view.Filter = FilterByType;
            Filter = "Internet";
            CollectionViewSource.GetDefaultView(itPrograms.ItemsSource).Refresh();
            NoResult.Visibility = Visibility.Visible;
            NoResult.Text = "Internet Program";
            btn_AllProgram.IsSelected = false;
        }
        private void Btn_Multimedia_Selected(object sender, RoutedEventArgs e)
        {
            AllPrograms_Grid.Visibility = Visibility.Hidden;
            SelectedItemGrid.Visibility = Visibility.Visible;
            AboutUs_Grid.Visibility = Visibility.Hidden;
            SV_ProgramList.Visibility = Visibility.Visible;
            view.Filter = FilterByType;
            Filter = "Multimedia";
            CollectionViewSource.GetDefaultView(itPrograms.ItemsSource).Refresh();
            NoResult.Visibility = Visibility.Visible;
            NoResult.Text = "Multimedia Program";
            btn_AllProgram.IsSelected = false;
        }
        private void btn_Mobile_Selected(object sender, RoutedEventArgs e)
        {
            AllPrograms_Grid.Visibility = Visibility.Hidden;
            SelectedItemGrid.Visibility = Visibility.Visible;
            AboutUs_Grid.Visibility = Visibility.Hidden;
            SV_ProgramList.Visibility = Visibility.Visible;
            view.Filter = FilterByType;
            Filter = "Mobile";
            CollectionViewSource.GetDefaultView(itPrograms.ItemsSource).Refresh();
            NoResult.Visibility = Visibility.Visible;
            NoResult.Text = "Mobile Program";
            btn_AllProgram.IsSelected = false;


        }

        private void Btn_Support_Selected(object sender, RoutedEventArgs e)
        {
            AllPrograms_Grid.Visibility = Visibility.Hidden;
            SelectedItemGrid.Visibility = Visibility.Visible;
            AboutUs_Grid.Visibility = Visibility.Hidden;
            SV_ProgramList.Visibility = Visibility.Visible;
            view.Filter = FilterByType;
            Filter = "Support";
            CollectionViewSource.GetDefaultView(itPrograms.ItemsSource).Refresh();
            NoResult.Visibility = Visibility.Visible;
            NoResult.Text = "Support Program";
            btn_AllProgram.IsSelected = false;
        }
        private void Btn_Tools_Selected(object sender, RoutedEventArgs e)
        {
            AllPrograms_Grid.Visibility = Visibility.Hidden;
            SelectedItemGrid.Visibility = Visibility.Visible;
            AboutUs_Grid.Visibility = Visibility.Hidden;
            SV_ProgramList.Visibility = Visibility.Visible;
            view.Filter = FilterByType;
            Filter = "Tools";
            CollectionViewSource.GetDefaultView(itPrograms.ItemsSource).Refresh();
            NoResult.Visibility = Visibility.Visible;
            NoResult.Text = "Tools Program";
            btn_AllProgram.IsSelected = false;
        }
        private void Btn_Office_Selected(object sender, RoutedEventArgs e)
        {
            AllPrograms_Grid.Visibility = Visibility.Hidden;
            SelectedItemGrid.Visibility = Visibility.Visible;
            AboutUs_Grid.Visibility = Visibility.Hidden;
            SV_ProgramList.Visibility = Visibility.Visible;
            view.Filter = FilterByType;
            Filter = "Office";
            CollectionViewSource.GetDefaultView(itPrograms.ItemsSource).Refresh();
            NoResult.Visibility = Visibility.Visible;
            NoResult.Text = "Office Program";
            btn_AllProgram.IsSelected = false;
        }
        private void Btn_About_Selected(object sender, RoutedEventArgs e)
        {
            AllPrograms_Grid.Visibility = Visibility.Hidden;
            SelectedItemGrid.Visibility = Visibility.Visible;
            AboutUs_Grid.Visibility = Visibility.Visible;
            NoResult.Visibility = Visibility.Hidden;
            SV_ProgramList.Visibility = Visibility.Visible;
            SV_ProgramList.Visibility = Visibility.Hidden;
            btn_AllProgram.IsSelected = false;

            //AllProgram_Scroll.Visibility = Visibility.Collapsed;
        }
        private void Btn_Amwal_Selected(object sender, RoutedEventArgs e)
        {
            AllPrograms_Grid.Visibility = Visibility.Hidden;
            SelectedItemGrid.Visibility = Visibility.Visible;
            AboutUs_Grid.Visibility = Visibility.Hidden;
            NoResult.Visibility = Visibility.Hidden;
            SV_ProgramList.Visibility = Visibility.Visible;
            SV_ProgramList.Visibility = Visibility.Hidden;
            btn_AllProgram.IsSelected = false;
        }
        public void IsSelectedAllProgram()
        {
            btn_Antivirus.IsSelected = false;
            btn_Graphic.IsSelected = false;
            btn_Internet.IsSelected = false;
            btn_Multimedia.IsSelected = false;
            btn_Mobile.IsSelected = false;
            btn_Support.IsSelected = false;
            btn_Tools.IsSelected = false;
            btn_Office.IsSelected = false;
            btn_About.IsSelected = false;
            btn_Amwal.IsSelected = false;

        }

        #endregion

        #region DesignAndEvents
        private void Windows_Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.MouseLeftButtonDown += delegate { DragMove(); };
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.ToString());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WriteTempFiles();
            // Timer for Refresh program item
            RefreshTimer.Tick += RefreshTimer_Tick;
            RefreshTimer.Interval = new TimeSpan(0, 0, 0);
            RefreshTimer.Stop();
            // to watch files in "C:\\Temp" if changed
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher(Environment.GetEnvironmentVariable("userprofile") + "\\AppData\\Local\\Temp\\");
            fileSystemWatcher.IncludeSubdirectories = true;
            fileSystemWatcher.Changed += OnChanged;
            fileSystemWatcher.Filter = "*.*";
            fileSystemWatcher.EnableRaisingEvents = true;
            AddProgram();
            btn_AllProgram.IsSelected = true;
            try
            {
                var soundfile = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.mp3", SearchOption.AllDirectories);
                mediaPlayer.Volume = 1.0f;
                mediaPlayer.Open(new Uri(soundfile[0]));
                mediaPlayer.Play();
            }
            catch (Exception)
            {
            }

        }
        // if file changed enable refresh timer to refresh program 
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            RefreshTimer.Start();
        }
        // refresh Program item when file changed
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            AddProgram();
            CollectionViewSource.GetDefaultView(itPrograms.ItemsSource).Refresh();
            RefreshTimer.Stop();

        }
        //private void btn_RestoreDown_Click(object sender, RoutedEventArgs e)
        //{
        //    if (Maximize_Icon.Visibility == Visibility.Visible)
        //    {
        //        btn_RestoreDown.ToolTip = "Exit Full Screen";
        //        WindowState = WindowState.Maximized;
        //        Maximize_Icon.Visibility = Visibility.Hidden;
        //        Restore_Icon.Visibility = Visibility.Visible;
        //    }
        //    else if (Restore_Icon.Visibility == Visibility.Visible)
        //    {
        //        btn_RestoreDown.ToolTip = "Full Screen";
        //        WindowState = WindowState.Normal;
        //        Maximize_Icon.Visibility = Visibility.Visible;
        //        Restore_Icon.Visibility = Visibility.Hidden;

        //    }
        //}
        private void Volume_play_Click(object sender, RoutedEventArgs e)
        {
            if (Play_Icon.Visibility == Visibility.Visible)
            {
                Volume_play.ToolTip = "Play";
                mediaPlayer.Pause();
                Play_Icon.Visibility = Visibility.Hidden;
                Mute_Icon.Visibility = Visibility.Visible;
            }
            else if (Mute_Icon.Visibility == Visibility.Visible)
            {
                Volume_play.ToolTip = "Pause";
                mediaPlayer.Play();
                Play_Icon.Visibility = Visibility.Visible;
                Mute_Icon.Visibility = Visibility.Hidden;

            }
        }
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_Minimize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void CommandBinding_Executed_Close(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DefualtDirectory = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "..\\Programs\\");
            for (int i = 0; i < DefualtDirectory.Length; i++)
            {

                FolderName = Path.GetFileName(Path.GetDirectoryName(DefualtDirectory[i] + "\\"));
                try
                {
                    ProgramInfo = File.ReadAllLines(Environment.GetEnvironmentVariable("userprofile") + "\\AppData\\Local\\Temp\\Alafandi & Sbahi\\" + FolderName + "\\" + FolderName + ".txt");
                }
                catch (Exception)
                {
                    continue;
                }
                if (!Directory.Exists(Environment.GetEnvironmentVariable("userprofile") + "\\AppData\\Local\\Temp\\Alafandi & Sbahi\\" + FolderName))
                {
                    Directory.CreateDirectory(Environment.GetEnvironmentVariable("userprofile") + "\\AppData\\Local\\Temp\\Alafandi & Sbahi\\" + FolderName);
                }
                StreamWriter streamWriter = new StreamWriter(Environment.GetEnvironmentVariable("userprofile") + "\\AppData\\Local\\Temp\\Alafandi & Sbahi\\" + FolderName + "\\" + FolderName + ".txt");
                streamWriter.WriteLine(FolderName);
                streamWriter.WriteLine(ProgramInfo[1]);
                streamWriter.WriteLine(ProgramInfo[2]);
                streamWriter.WriteLine(ProgramInfo[3]);
                streamWriter.WriteLine(ProgramInfo[4]);
                if (ProgramInfo[5] == "Install")
                {
                    streamWriter.WriteLine("Install");
                }
                else
                {
                    streamWriter.WriteLine("It is already installed (Reinstall)");
                }
                if (ProgramInfo[6] == "Download")
                {
                    streamWriter.WriteLine("Download");
                }
                else
                {
                    streamWriter.WriteLine("Refresh");
                }
                streamWriter.Close();

                itPrograms.ItemsSource = programs;
                view = (CollectionView)CollectionViewSource.GetDefaultView(itPrograms.ItemsSource);
                view.Filter = SearchByName;

            }
            //DirectoryInfo di = new DirectoryInfo(Environment.GetEnvironmentVariable("userprofile") + "\\AppData\\Local\\Temp\\");

            //foreach (FileInfo file in di.GetFiles())
            //{
            //    file.Delete();
            //}
            //foreach (DirectoryInfo dir in di.GetDirectories())
            //{
            //    dir.Delete(true);
            //}
        }
        #endregion

        #region Search
        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            //search_border.Background = (Brush)bc.ConvertFrom("#7F414141");
            search_border.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 44, 81, 120));

        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            search_border.BorderBrush = (Brush)bc.ConvertFrom("#7F2C516A");
        }
        private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            SV_ProgramList.ScrollToTop();
            SV_ProgramList.Visibility = Visibility.Visible;
            view.Filter = SearchByName;
            Filter = txtSearch.Text;
            CollectionViewSource.GetDefaultView(itPrograms.ItemsSource).Refresh();
            if (itPrograms.Items.Count == 0)
            {

                NoResult.Visibility = Visibility.Visible;
                NoResult.Text = "Search Result : " + itPrograms.Items.Count;
            }
            if (itPrograms.Items.Count > 0)
            {
                NoResult.Visibility = Visibility.Visible;
                NoResult.Text = "Search Result : " + itPrograms.Items.Count;
            }
            if (txtSearch.Text == string.Empty)
            {
                NoResult.Visibility = Visibility.Visible;
                if (btn_AllProgram.IsSelected)
                {
                    Btn_AllProgram_Selected(sender, e);
                }
                if (btn_Antivirus.IsSelected)
                {
                    Btn_Antivirus_Selected(sender, e);
                }
                if (btn_Graphic.IsSelected)
                {
                    Btn_Graphic_Selected(sender, e);
                }
                if (btn_Internet.IsSelected)
                {
                    Btn_Internet_Selected(sender, e);
                }
                if (btn_Multimedia.IsSelected)
                {
                    Btn_Multimedia_Selected(sender, e);
                }
                if (btn_Mobile.IsSelected)
                {
                    btn_Mobile_Selected(sender, e);
                }
                if (btn_Office.IsSelected)
                {
                    Btn_Office_Selected(sender, e);
                }
                if (btn_Support.IsSelected)
                {
                    Btn_Support_Selected(sender, e);
                }
                if (btn_Tools.IsSelected)
                {
                    Btn_Tools_Selected(sender, e);
                }

            }
        }
        private bool SearchByName(object item)
        {
            if (String.IsNullOrEmpty(Filter))
                return true;
            else
                return (item as CLS_ProgramInfo).ProgramName.IndexOf(Filter, StringComparison.OrdinalIgnoreCase) >= 0;
        }
        private bool FilterByType(object item)
        {
            if (String.IsNullOrEmpty(Filter))
                return true;
            else
                return (item as CLS_ProgramInfo).ProgramType.IndexOf(Filter, StringComparison.OrdinalIgnoreCase) >= 0;
        }




        #endregion


    }
}
