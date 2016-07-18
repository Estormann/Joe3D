using GalaSoft.MvvmLight;
using System.Windows;
using System.Windows.Media.Media3D;
using System;
using GalaSoft.MvvmLight.Messaging;

namespace Joe3D.ViewControl.ViewModel
{
    public class JoeViewPanelVM : ViewModelBase
    {
        public JoeViewPanelVM()
        {
            this.Camera = new CameraVM
            {
                Position = new Point3D(2, 2, 2),
                UpDirection = new Vector3D(0, 1, 0),
                LookDirection = new Vector3D(-1, -1, -1)
            };
            this.Lights = InitializeLights();
            this.Model = Joe3D.Utilities.Generator.GetCube();
        }
        private void AlphaChanged(float alpha)
        {

        }
        private Model3DGroup initializeModel()
        {
            return Joe3D.Utilities.Generator.GetCube();
        }

        private Model3DGroup InitializeLights()
        {
            return Joe3D.Utilities.Generator.GetLight();
        }

        //everything the Viewport3d needs

        //Camera
        /// <summary>
        /// The <see cref="Camera" /> property's name.
        /// </summary>
        public const string CameraPropertyName = "Camera";

        private CameraVM _Camera;

        /// <summary>
        /// Sets and gets the Camera property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public CameraVM Camera
        {
            get
            {
                return _Camera;
            }
            set
            {
                Set(() => Camera, ref _Camera, value, true);
                UpdateCamera();
            }
        }

        /// <summary>
        /// The <see cref="Alpha" /> property's name.
        /// </summary>
        public const string AlphaPropertyName = "Alpha";

        private float _Apha = 0;

        /// <summary>
        /// Sets and gets the Alpha property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public float Alpha
        {
            get
            {
                return _Apha;
            }

            set
            {
                if (_Apha == value)
                {
                    return;
                }

                var oldValue = _Apha;
                _Apha = value;
                RaisePropertyChanged(() => Alpha, oldValue, value, true);
                UpdateCamera();
            }
        }

        private void UpdateCamera()
        {
            this.Camera.Position = new Point3D(this.Camera.Position.X+1, this.Camera.Position.Y, this.Camera.Position.Z);

            RaisePropertyChanged(() => Camera);
        }

        //Model.Mesh
        /// <summary>
        /// The <see cref="3DModel" /> property's name.
        /// </summary>
        public const string ModelPropertyName = "3DModel";

        private Model3DGroup _Model;

        /// <summary>
        /// Sets and gets the 3DModel property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public Model3DGroup Model
        {
            get
            {
                return _Model;
            }
            set
            {
                Set(() => Model, ref _Model, value, true);
            }
        }
        //Model.Light
        /// <summary>
        /// The <see cref="Lights" /> property's name.
        /// </summary>
        public const string LightsPropertyName = "Lights";

        private Model3DGroup _Lights;

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
                Set(() => Lights, ref _Lights, value, true);
            }
        }
        //
        //private PerspectiveCamera InitializeCamera()
        //{
        //    PerspectiveCamera Camera1 = new PerspectiveCamera(new Point3D(2, 2, 2), new Vector3D(-1, -1, -1), new Vector3D(0, 1, 0), 45);
        //    Camera1.FarPlaneDistance = 20;
        //    Camera1.NearPlaneDistance = 1;
        //    return Camera1;
        //}

    }
}
