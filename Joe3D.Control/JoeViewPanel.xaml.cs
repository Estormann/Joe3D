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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Joe3D.ViewControl
{
    /// <summary>
    /// Interaction logic for JoeViewPanel.xaml
    /// </summary>
    public partial class JoeViewPanel : UserControl
    {
        public JoeViewPanel()
        {
            InitializeComponent();
            this.DataContext = new Joe3D.ViewControl.ViewModel.JoeViewPanelVM();
        }
    }
}
