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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Joe3D.ViewControl
{
    /// <summary>
    /// Interaction logic for JoeViewPanel.xaml
    /// </summary>
    public partial class JoeViewPanel : UserControl
    {
        //private PerspectiveCamera _Camera;
        //We are going to maintain an internal Camera View Model
        public JoeViewPanel()
        {
            InitializeComponent();
            InitializeCamera();
            InitModel();
            InitLights();
        }

        private void InitLights()
        {
        }

        private void InitModel()
        {
        }

        public static Vector3D GetCameraLookDirection(Point3D cameraLoc, Point3D Origin)
        {
            return GetUnitVector(cameraLoc, Origin);
        }

        private static Point3D MovePoint2onVector(Point3D Point1, Point3D Point2, double NewDistance)
        {
            var UVect = GetUnitVector(Point1, Point2);//get a unit vector from point 1 to Point 2
            return new Point3D(UVect.X * NewDistance, UVect.Y * NewDistance, UVect.Z * NewDistance);
        }

        private static double GetVectorDistance(Point3D Point1, Point3D Point2)
        {
            //returns a Vector pointing at Point 2 from Point 1
            var Vect = GetVector(Point1, Point2);

            return Math.Sqrt(Math.Pow(Vect.X, 2) + Math.Pow(Vect.Y, 2) + Math.Pow(Vect.Z, 2));
        }
        private static Vector3D GetVector(Point3D Point1, Point3D Point2)
        {
            double deltaX = Point2.X - Point1.X;
            double deltaY = Point2.Y - Point1.Y;
            double deltaZ = Point2.Z - Point1.Z;
            return new Vector3D(deltaX,deltaY,deltaZ);
        }
        private static Vector3D GetUnitVector(Point3D Point1, Point3D Point2)
        {
            //returns a Vector pointing at Point 2 from Point 1
            var PVect = GetVector(Point1, Point2);
            double dist = GetVectorDistance(Point1, Point2);
            Vector3D UnitVector;
            if (dist != 0)
            {
                UnitVector = new Vector3D(PVect.X / dist, PVect.Y / dist, PVect.Z / dist);
            }
            else
            {
                UnitVector = new Vector3D(0, 0, 0);
            }
            return UnitVector;
        }
        private void InitializeCamera()
        {
            Point3D Origin = new Point3D(0, 0, 0);
            this.Camera = new PerspectiveCamera();
            this.Camera.FieldOfView = 45;
            this.Camera.NearPlaneDistance = 1;
            this.Camera.UpDirection = new Vector3D(0, 1, 0);
            this.Camera.Position = new Point3D(0,0,5);
            this.Camera.LookDirection = GetCameraLookDirection(this.Camera.Position, Origin);
            JoeView.Camera = this.Camera;
        }
        #region Dependency Properties
        #region Alpha
        /// <summary>
        /// The <see cref="Alpha" /> dependency property's name.
        /// </summary>
        public const string AlphaPropertyName = "Alpha";

        /// <summary>
        /// Gets or sets the value of the <see cref="Alpha" />
        /// property. This is a dependency property.
        /// </summary>
        public double Alpha
        {
            get
            {
                return (double)GetValue(AlphaProperty);
            }
            set
            {
                SetValue(AlphaProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Alpha" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AlphaProperty = DependencyProperty.Register(
            AlphaPropertyName,
            typeof(double),
            typeof(JoeViewPanel),
            new FrameworkPropertyMetadata
            (
                0.0D,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnAlphaChanged)
            )
        );
        private static void OnAlphaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            JoeViewPanel JoeViewPanel = (JoeViewPanel)d;
            double Distance = JoeViewPanel.Distance;
            double alpha = (double)e.NewValue;  
            double beta = JoeViewPanel.Beta;
            Point3D origin = new Point3D(0, 0, 0);
            var Camera = JoeViewPanel.Camera;
            if (Distance >= 1)
            {
                Camera.Position = GetNewPosition(alpha, beta, Distance);
            }
            Camera.LookDirection = GetCameraLookDirection(Camera.Position, origin);
        }
        #endregion
        #region Beta
        /// <summary>
        /// The <see cref="Beta" /> dependency property's name.
        /// </summary>
        public const string BetaPropertyName = "Beta";

        /// <summary>
        /// Gets or sets the value of the <see cref="Beta" />
        /// property. This is a dependency property.
        /// </summary>
        public double Beta
        {
            get
            {
                return (double)GetValue(BetaProperty);
            }
            set
            {
                SetValue(BetaProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Beta" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty BetaProperty = DependencyProperty.Register(
            BetaPropertyName,
            typeof(double),
            typeof(JoeViewPanel),
           new FrameworkPropertyMetadata
            (
                0.0D,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnBetaChanged)
            )
        );
        private static void OnBetaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            JoeViewPanel JoeViewPanel = (JoeViewPanel)d;
            double Distance = JoeViewPanel.Distance;
            double beta = (double)e.NewValue;
            double alpha = JoeViewPanel.Alpha;
            Point3D origin = new Point3D(0, 0, 0);
            var Camera = JoeViewPanel.Camera;
            if (Distance >= 1)
            {
                Camera.Position = GetNewPosition(alpha, beta, Distance);
            }
            Camera.LookDirection = GetCameraLookDirection(Camera.Position, origin);
        }
        #endregion
        #region Theta
        ///// <summary>
        ///// The <see cref="Theta" /> dependency property's name.
        ///// </summary>
        //public const string ThetaPropertyName = "Theta";

        ///// <summary>
        ///// Gets or sets the value of the <see cref="Theta" />
        ///// property. This is a dependency property.
        ///// </summary>
        //public double Theta
        //{
        //    get
        //    {
        //        return (double)GetValue(ThetaProperty);
        //    }
        //    set
        //    {
        //        SetValue(ThetaProperty, value);
        //    }
        //}

        ///// <summary>
        ///// Identifies the <see cref="Theta" /> dependency property.
        ///// </summary>
        //public static readonly DependencyProperty ThetaProperty = DependencyProperty.Register(
        //    ThetaPropertyName,
        //    typeof(double),
        //    typeof(JoeViewPanel),
        //    new FrameworkPropertyMetadata
        //    (
        //        0.0D,
        //        FrameworkPropertyMetadataOptions.AffectsRender,
        //        new PropertyChangedCallback(OnThetaChanged)
        //    )
        //);
        //private static void OnThetaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //}
        #endregion
        #region Distance
        /// <summary>
        /// The <see cref="Distance" /> dependency property's name.
        /// </summary>
        public const string DistancePropertyName = "Distance";

        /// <summary>
        /// Gets or sets the value of the <see cref="Distance" />
        /// property. This is a dependency property.
        /// </summary>
        public double Distance
        {
            get
            {
                return (double)GetValue(DistanceProperty);
            }
            set
            {
                SetValue(DistanceProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Distance" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DistanceProperty = DependencyProperty.Register(
            DistancePropertyName,
            typeof(double),
            typeof(JoeViewPanel),
            new FrameworkPropertyMetadata
            (
                1.0D,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnDistanceChanged)
            )
        );
        private static void OnDistanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            JoeViewPanel JoeViewPanel = (JoeViewPanel)d;
            double newCameraDistance = (double)e.NewValue;
            double alpha = JoeViewPanel.Alpha;
            double beta = JoeViewPanel.Beta;
            Point3D origin = new Point3D(0, 0, 0);
            var Camera = JoeViewPanel.Camera;
            if (newCameraDistance >= 1)
            {
                Camera.Position = GetNewPosition(alpha,beta,newCameraDistance);
            }
            Camera.LookDirection = GetCameraLookDirection(Camera.Position, origin);
        }
        #endregion
        #region Model
        /// <summary>
        /// The <see cref="JoeModel" /> dependency property's name.
        /// </summary>
        public const string JoeModelPropertyName = "JoeModel";

        /// <summary>
        /// Gets or sets the value of the <see cref="JoeModel" />
        /// property. This is a dependency property.
        /// </summary>
        public Model3D JoeModel
        {
            get
            {
                return (Model3D)GetValue(JoeModelProperty);
            }
            set
            {
                SetValue(JoeModelProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="JoeModel" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty JoeModelProperty = DependencyProperty.Register(
            JoeModelPropertyName,
            typeof(Model3D),
            typeof(JoeViewPanel),
            new FrameworkPropertyMetadata
            (
                Joe3D.Utilities.Generator.GetCube(),
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnModelChanged)
            )
        );
        private static void OnModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            JoeViewPanel JoeView = (JoeViewPanel)d;
            Model3D newModel = (Model3D)e.NewValue;
            //The model is not directly exposed in the collection, but we can get to the Children collection wich has a 
            //VisualModel3D named Model and set its contect to the Model3d of this bindable property.  
            //This lets us jump across several layers of abstraction defined in the XAML to just the property we are changing in the usage layer.  
            //Much simpler
            JoeView.Model.Content = (Model3D)e.NewValue;
        }
        #endregion
        #region Lights
        /// <summary>
        /// The <see cref="JoeLights" /> dependency property's name.
        /// </summary>
        public const string JoeLightsPropertyName = "JoeLights";

        /// <summary>
        /// Gets or sets the value of the <see cref="JoeLights" />
        /// property. This is a dependency property.
        /// </summary>
        public Model3DGroup JoeLights
        {
            get
            {
                return (Model3DGroup)GetValue(JoeLightsProperty);
            }
            set
            {
                SetValue(JoeLightsProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="JoeLights" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty JoeLightsProperty = DependencyProperty.Register(
            JoeLightsPropertyName,
            typeof(Model3DGroup),
            typeof(JoeViewPanel),
             new FrameworkPropertyMetadata
            (
                Joe3D.Utilities.Generator.GetLight(),
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnLightChanged)
            )
        );
        private static void OnLightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            JoeViewPanel JoeView = (JoeViewPanel)d;
            Model3DGroup newModel = (Model3DGroup)e.NewValue;
            //The model is not directly exposed in the collection, but we can get to the Children collection wich has a 
            //VisualModel3D named Model and set its contect to the Model3d of this bindable property.  
            //This lets us jump across several layers of abstraction defined in the XAML to just the property we are changing in the usage layer.  
            //Much simpler
            JoeView.Lights.Content = (Model3DGroup)e.NewValue;
        }
        #endregion
        #endregion

        private static Point3D GetNewPosition(double alpha, double beta, double Distance)
        {
            //convert input to radians
            double alpharad = alpha * Math.PI / 180;
            double betarad = beta * Math.PI / 180;
            double x = Distance * Math.Sin(alpharad) * Math.Cos(betarad);
            double y = Distance * Math.Sin(alpharad) * Math.Sin(betarad);
            double z = Distance * Math.Cos(alpharad);
            return new Point3D(x, y, z);
        }
    }
}
