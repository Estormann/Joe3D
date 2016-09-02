using Joe3D.ViewControl;
using Joe3D.WPF.ViewModel;
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

namespace Joe3D.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //string path = @"D:\Users\estormann\Documents\Visual Studio 2015\Projects\Joe3D\Joe3D.WPF\bin\Debug\Handgun\Handgun_Obj\Handgun_obj.obj";
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Handgun\Handgun_Obj\Handgun_obj.obj");
            ((ViewerViewModel)this.DataContext).CurrentModel =  Joe3D.Utilities.Generator.GetObjModel(path);
        }

      
    }
}
