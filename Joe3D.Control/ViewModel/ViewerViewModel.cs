using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace Joe3D.ViewControl
{
    public class ViewerViewModel : ViewModelBase
    {
        #region Public Properties

        private readonly Dispatcher dispatcher = null;
        public const string DistancePropertyName = "Distance";
        private double _Distance = 10D;

        public double Distance
        {
            get
            {
                return _Distance;
            }

            set
            {
                if (_Distance == value)
                {
                    return;
                }

                var oldValue = _Distance;
                _Distance = value;
                RaisePropertyChanged(() => Distance, oldValue, value, true);
            }
        }
        public const string CurrentModelPropertyName = "CurrentModel";

        private Model3D _CurrentModel = Joe3D.Utilities.Generator.GetCube();

        public Model3D CurrentModel
        {
            get
            {
                return _CurrentModel;
            }

            set
            {
                if (_CurrentModel == value)
                {
                    return;
                }

                var oldValue = _CurrentModel;
                _CurrentModel = value;
                RaisePropertyChanged(() => CurrentModel, oldValue, value, true);

            }
        }
        public const string LightsPropertyName = "Lights";

        private Model3DGroup _Lights = Joe3D.Utilities.Generator.GetLight();
        /// <summary>
        /// The <see cref="X" /> property's name.
        /// </summary>
        public const string XPropertyName = "X";

        private double _X = 0.0D;

        /// <summary>
        /// Sets and gets the X property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double X
        {
            get
            {
                return _X;
            }

            set
            {
                if (_X == value)
                {
                    return;
                }

                _X = value;
                RaisePropertyChanged(() => X);
            }
        }
        /// <summary>
        /// The <see cref="Y" /> property's name.
        /// </summary>
        public const string YPropertyName = "Y";

        private double _Y = 0.0D;

        /// <summary>
        /// Sets and gets the Y property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double Y
        {
            get
            {
                return _Y;
            }

            set
            {
                if (_Y == value)
                {
                    return;
                }

                _Y = value;
                RaisePropertyChanged(() => Y);
            }
        }
        /// <summary>
        /// The <see cref="Z" /> property's name.
        /// </summary>
        public const string ZPropertyName = "Z";

        private double _Z = 0.0D;

        /// <summary>
        /// Sets and gets the Z property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double Z
        {
            get
            {
                return _Z;
            }

            set
            {
                if (_Z == value)
                {
                    return;
                }

                _Z = value;
                RaisePropertyChanged(() => Z);
            }
        }
        /// <summary>
        /// Sets and gets the Lights property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public Model3DGroup Lights
        {
            get
            {
                return _Lights;
            }

            set
            {
                if (_Lights == value)
                {
                    return;
                }

                var oldValue = _Lights;
                _Lights = value;
                RaisePropertyChanged(() => Lights, oldValue, value, true);
            }
        }
        #endregion
        #region Commands
        private RelayCommand _OpenFile;

        public RelayCommand OpenFile
        {
            get
            {
                return _OpenFile ?? (_OpenFile = new RelayCommand(
                    ExecuteOpenFile,
                    CanExecuteOpenFile));
            }
        }
        private const string OpenFileFilter = "3D model files (*.3ds;*.obj;*.lwo;*.stl)|*.3ds;*.obj;*.objz;*.lwo;*.stl";
        private void ExecuteOpenFile()
        {
            var fds = new Joe3D.Utilities.FileDialogService();

            var CurrentModelPath = fds.OpenFileDialog("models", null, OpenFileFilter, ".obj");
            this.CurrentModel = this.Load(CurrentModelPath, false);
        }

        private bool CanExecuteOpenFile()
        {
            return true;
        }
        private async Task<Model3DGroup> LoadAsync(string model3DPath, bool freeze)
        {
            return await Task.Factory.StartNew(() =>
            {
                var mi = new Joe3D.Utilities.ModelImporter();

                if (freeze)
                {
                    // Alt 1. - freeze the model 
                    return mi.Load(model3DPath, null, true);
                }

                // Alt. 2 - create the model on the UI dispatcher
                return mi.Load(model3DPath, this.dispatcher);
            });
        }
        private  Model3DGroup Load(string model3DPath, bool freeze)
        {
                var mi = new Joe3D.Utilities.ModelImporter();

                if (freeze)
                {
                    // Alt 1. - freeze the model 
                    return mi.Load(model3DPath, null, true);
                }

                // Alt. 2 - create the model on the UI dispatcher
                return mi.Load(model3DPath, this.dispatcher);
        }
        #endregion
    }
}
