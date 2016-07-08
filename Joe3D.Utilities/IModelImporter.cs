using System;
using System.IO;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace Joe3D.Utilities
{
    public interface IModelImporter
    {
        Material DefaultMaterial { get; set; }

        Model3DGroup Load(string path, Dispatcher dispatcher = null, bool freeze = false);
    }
}
