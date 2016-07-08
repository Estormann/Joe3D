using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HelixToolkit.Wpf;
using Joe3D.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace Joe3D.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(IFileDialogService fds, HelixViewport3D viewport)
        {
            if (viewport == null)
            {
                throw new ArgumentNullException("viewport");
            }
            this.viewport = viewport;
            this.dispatcher = Dispatcher.CurrentDispatcher;
            this.fileDialogService = new FileDialogService();
            this.FileOpenCommand = new RelayCommand(this.FileOpen);
            this.FileExitCommand = new RelayCommand(FileExit);
            this.ViewZoomExtentsCommand = new RelayCommand(this.ViewZoomExtents);
            Title = "TEST of JOE 3D";
        }
        #region Public Properties
        public RelayCommand FileOpenCommand { get; set; }
        public RelayCommand FileExitCommand { get; set; }
        public RelayCommand ViewZoomExtentsCommand { get; set; }
        #endregion
        #region Private Variable/Backing Variable
        private const string OpenFileFilter = "3D model files (*.3ds;*.obj;*.lwo;*.stl)|*.3ds;*.obj;*.objz;*.lwo;*.stl";
        private const string TitleFormatString = "3D model viewer - {0}";

        private readonly IFileDialogService fileDialogService;
        private readonly IHelixViewport3D viewport;
        private readonly Dispatcher dispatcher;
        private string _CurrentModelPath;
        private string applicationTitle;
        private double expansion;
        private Model3D currentModel;
        #endregion

        #region Private Methods/Helpers
        private static void FileExit()
        {
            Application.Current.Shutdown();
        }
        private void ViewZoomExtents()
        {
            this.viewport.ZoomExtents(500);
        }
        #endregion
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
                this.RaisePropertyChanged("Title");
            }
        }
        private string _Title;
        public Model3D CurrentModel
        {
            get
            {
                return this.currentModel;
            }

            set
            {
                this.currentModel = value;
                this.RaisePropertyChanged("CurrentModel");
            }
        }
        public string CurrentModelPath
        {
            get
            {
                return this._CurrentModelPath;
            }

            set
            {
                this._CurrentModelPath = value;
                this.RaisePropertyChanged("CurrentModelPath");
            }
        }
        private async void FileOpen()
        {
            this.CurrentModelPath = this.fileDialogService.OpenFileDialog("models", null, OpenFileFilter, ".3ds");
            this.CurrentModel = ModelLoader.Load(this.CurrentModelPath, false,this.dispatcher);
            this.Title = string.Format(TitleFormatString, this.CurrentModelPath);
            this.viewport.ZoomExtents(0);
            this.RaisePropertyChanged("CurrentModel");
        }
        
    }
}