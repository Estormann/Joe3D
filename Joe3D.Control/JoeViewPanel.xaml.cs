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
        private PerspectiveCamera _Camera;
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

        private void InitializeCamera()
        {
            _Camera = new PerspectiveCamera();
            _Camera.UpDirection = new Vector3D(0, 1, 0);
            _Camera.Position = new Point3D(2,2,2);
            _Camera.LookDirection = new Vector3D(-1, -1, -1);
            JoeView.Camera = _Camera;
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
        }
        #endregion
        #region Theta
        /// <summary>
        /// The <see cref="Theta" /> dependency property's name.
        /// </summary>
        public const string ThetaPropertyName = "Theta";

        /// <summary>
        /// Gets or sets the value of the <see cref="Theta" />
        /// property. This is a dependency property.
        /// </summary>
        public double Theta
        {
            get
            {
                return (double)GetValue(ThetaProperty);
            }
            set
            {
                SetValue(ThetaProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Theta" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ThetaProperty = DependencyProperty.Register(
            ThetaPropertyName,
            typeof(double),
            typeof(JoeViewPanel),
            new FrameworkPropertyMetadata
            (
                0.0D,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnThetaChanged)
            )
        );
        private static void OnThetaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
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
            JoeViewPanel JoeView = (JoeViewPanel)d;
            double newCameraDistance = (double)e.NewValue;
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







        
    }
}
