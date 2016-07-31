using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace Joe3D.ViewControl
{
    public class ViewerViewModel : ViewModelBase
    {
        #region Public Properties

        public const string AlphaPropertyName = "Alpha";
        private double _Alpha = 0.0;

        public double Alpha
        {
            get
            {
                return _Alpha;
            }

            set
            {
                if (_Alpha == value)
                {
                    return;
                }

                var oldValue = _Alpha;
                _Alpha = value;
                RaisePropertyChanged(() => Alpha, oldValue, value, true);
            }
        }
        public const string BetaPropertyName = "Beta";
        private double _Beta = 0.0;
        private readonly Dispatcher dispatcher = null;
        public double Beta
        {
            get
            {
                return _Beta;
            }

            set
            {
                if (_Beta == value)
                {
                    return;
                }

                var oldValue = _Beta;
                _Beta = value;
                RaisePropertyChanged(() => Beta, oldValue, value, true);
            }
        }
        public const string ThetaPropertyName = "Theta";

        private double _Theta = 0.0;

        public double Theta
        {
            get
            {
                return _Theta;
            }

            set
            {
                if (_Theta == value)
                {
                    return;
                }

                var oldValue = _Theta;
                _Theta = value;
                RaisePropertyChanged(() => Theta, oldValue, value, true);
            }
        }
        /// <summary>
        /// The <see cref="Distance" /> property's name.
        /// </summary>
        public const string DistancePropertyName = "Distance";

        private double _Distance = 10D;

        /// <summary>
        /// Sets and gets the Distance property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
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
        /// <summary>
        /// The <see cref="Lights" /> property's name.
        /// </summary>
        public const string LightsPropertyName = "Lights";

        private Model3DGroup _Lights = Joe3D.Utilities.Generator.GetLight();

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

        /// <summary>
        /// Gets the OpenFile.
        /// </summary>
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
        private async void ExecuteOpenFile()
        {
            var fds = new Joe3D.Utilities.FileDialogService();

            var CurrentModelPath = fds.OpenFileDialog("models", null, OpenFileFilter, ".obj");
            this.CurrentModel = await this.LoadAsync(CurrentModelPath, false);
        }

        private bool CanExecuteOpenFile()
        {
            return true;
        }
        private RelayCommand<double> _SetAlpha;
        public RelayCommand<double> SetAlpha
        {
            get
            {
                return _SetAlpha ?? (_SetAlpha = new RelayCommand<double>(
                    (s) => ExecuteSetAlpha(s),s=>CanExecuteSetAlpha(s)));
            }
        }

      
        private RelayCommand _SetBeta;
        public RelayCommand SetBeta
        {
            get
            {
                return _SetBeta ?? (_SetBeta = new RelayCommand(
                    ExecuteSetBeta,
                    CanExecuteSetBeta));
            }
        }

       
        private RelayCommand _SetTheta;

        public RelayCommand SetTheta
        {
            get
            {
                return _SetTheta ?? (_SetTheta = new RelayCommand(
                    ExecuteSetTheta,
                    CanExecuteSetTheta));
            }
        }

        #endregion
        #region private Action
        private void ExecuteSetAlpha(double alpha)
        {
            var oldValue = Alpha;
            this.Alpha = 77.7;
            RaisePropertyChanged(() => Alpha, oldValue, Alpha, true);
        }

        private bool CanExecuteSetAlpha(double alpha)
        {
            return true;
        }
        private void ExecuteSetBeta()
        {

        }

        private bool CanExecuteSetBeta()
        {
            return true;
        }
        private void ExecuteSetTheta()
        {

        }

        private bool CanExecuteSetTheta()
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
        #endregion
    }
}
