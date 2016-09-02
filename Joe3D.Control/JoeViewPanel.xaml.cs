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
        #region X
        /// <summary>
        /// The <see cref="X" /> dependency property's name.
        /// </summary>
        public const string XPropertyName = "X";

        /// <summary>
        /// Gets or sets the value of the <see cref="X" />
        /// property. This is a dependency property.
        /// </summary>
        public double X
        {
            get
            {
                return (double)GetValue(XProperty);
            }
            set
            {
                SetValue(XProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="X" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
            XPropertyName,
            typeof(double),
            typeof(JoeViewPanel),
            new FrameworkPropertyMetadata
            (
                0.0D,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnXChanged)
            )
        );
        private static void OnXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            JoeViewPanel JoeViewPanel = (JoeViewPanel)d;
            double X = (double)e.NewValue;
            double Y = JoeViewPanel.Y;
            double Z = JoeViewPanel.Z;
            double Distance = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
            Point3D origin = new Point3D(0, 0, 0);

            var RotMatrix = Joe3D.Utilities.Generator.CalculateRotationMatrix(X, Y, Z);
            MatrixTransform3D trans = new MatrixTransform3D(RotMatrix);

            JoeViewPanel.Model.Transform = trans;
        }
        #endregion
        #region Y
        /// <summary>
        /// The <see cref="Y" /> dependency property's name.
        /// </summary>
        public const string YPropertyName = "Y";

        /// <summary>
        /// Gets or sets the value of the <see cref="Y" />
        /// property. This is a dependency property.
        /// </summary>
        public double Y
        {
            get
            {
                return (double)GetValue(YProperty);
            }
            set
            {
                SetValue(YProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Y" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
            YPropertyName,
            typeof(double),
            typeof(JoeViewPanel),
           new FrameworkPropertyMetadata
            (
                0.0D,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnYChanged)
            )
        );
        private static void OnYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            JoeViewPanel JoeViewPanel = (JoeViewPanel)d;
            double Y = (double)e.NewValue;
            double X = JoeViewPanel.X;
            double Z = JoeViewPanel.Z;
            double Distance = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
            Point3D origin = new Point3D(0, 0, 0);

            var RotMatrix = Joe3D.Utilities.Generator.CalculateRotationMatrix(X, Y, Z);
            MatrixTransform3D trans = new MatrixTransform3D(RotMatrix);

            JoeViewPanel.Model.Transform = trans;
        }
        #endregion
        #region Z
        /// <summary>
        /// The <see cref="Z" /> dependency property's name.
        /// </summary>
        public const string ZPropertyName = "Z";

        /// <summary>
        /// Gets or sets the value of the <see cref="Z" />
        /// property. This is a dependency property.
        /// </summary>
        public double Z
        {
            get
            {
                return (double)GetValue(ZProperty);
            }
            set
            {
                SetValue(ZProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Z" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ZProperty = DependencyProperty.Register(
            ZPropertyName,
            typeof(double),
            typeof(JoeViewPanel),
            new FrameworkPropertyMetadata
            (
                0.0D,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnZChanged)
            )
        );
        private static void OnZChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            JoeViewPanel JoeViewPanel = (JoeViewPanel)d;
            double Z = (double)e.NewValue;
            double X = JoeViewPanel.X;
            double Y = JoeViewPanel.Y;
            double Distance = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
            Point3D origin = new Point3D(0, 0, 0);

            var RotMatrix = Joe3D.Utilities.Generator.CalculateRotationMatrix(X, Y, Z);
            MatrixTransform3D trans = new MatrixTransform3D(RotMatrix);

            JoeViewPanel.Model.Transform = trans;
        }

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
            Point3D origin = new Point3D(0, 0, 0);
            var Camera = JoeViewPanel.Camera;
            if (newCameraDistance >= 1)
            {
                //Keeping camera on Z Axis
                Camera.Position = new Point3D(0,0,newCameraDistance);
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
