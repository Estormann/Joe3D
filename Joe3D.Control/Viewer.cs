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
    public class Viewer : Viewport3D
    {
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
                this.InvalidateVisual();
            }
        }

        /// <summary>
        /// Identifies the <see cref="Alpha" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AlphaProperty = DependencyProperty.Register(
            AlphaPropertyName,
            typeof(double),
            typeof(Viewer),
            new PropertyMetadata(0.0,new PropertyChangedCallback(OnAlphaChanged)));
        private static void OnAlphaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Viewer vwr = (Viewer)d;
            vwr.InvalidateVisual();

        }
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
            typeof(Viewer),
            new PropertyMetadata(0.0, OnBetaChanged));

        private static void OnBetaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Viewer vwr = (Viewer)d;
            vwr.InvalidateVisual();
        }
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
            typeof(Viewer),
            new UIPropertyMetadata(defaultValue:0.0));
        static Viewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Viewer), new FrameworkPropertyMetadata(typeof(Viewer)));
        }
      
    }
}
