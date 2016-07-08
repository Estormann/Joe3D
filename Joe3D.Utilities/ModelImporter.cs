using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace Joe3D.Utilities
{
    public class ModelImporter : IModelImporter
    {
        public ModelImporter()
        {
            this.DefaultMaterial = Materials.Blue;
        }
        public Material DefaultMaterial { get; set; }
        public Model3DGroup Load(string path, Dispatcher dispatcher = null, bool freeze = false)
        {
            if (path == null)
            {
                return null;
            }

            if (dispatcher == null)
            {
                dispatcher = Dispatcher.CurrentDispatcher;
            }

            Model3DGroup model;
            var ext = Path.GetExtension(path);
            if (ext != null)
            {
                ext = ext.ToLower();
            }

            switch (ext)
            {
                case ".3ds":
                    {
                        var r = new StudioReader(dispatcher) { DefaultMaterial = this.DefaultMaterial, Freeze = freeze };
                        model = r.Read(path);
                        break;
                    }

              

                case ".obj":
                    {
                        var r = new ObjReader(dispatcher) { DefaultMaterial = this.DefaultMaterial, Freeze = freeze };
                        model = r.Read(path);
                        break;
                    }

                case ".objz":
                    {
                        var r = new ObjReader(dispatcher) { DefaultMaterial = this.DefaultMaterial, Freeze = freeze };
                        model = r.ReadZ(path);
                        break;
                    }
                //case ".lwo":
                //    {
                //        var r = new LwoReader(dispatcher) { DefaultMaterial = this.DefaultMaterial, Freeze = freeze };
                //        model = r.Read(path);

                //        break;
                //    }
                //case ".stl":
                //    {
                //        var r = new StLReader(dispatcher) { DefaultMaterial = this.DefaultMaterial, Freeze = freeze };
                //        model = r.Read(path);
                //        break;
                //    }

                //case ".off":
                //    {
                //        var r = new OffReader(dispatcher) { DefaultMaterial = this.DefaultMaterial, Freeze = freeze };
                //        model = r.Read(path);
                //        break;
                //    }

                default:
                    throw new InvalidOperationException("File format not supported.");
            }

            return model;
        }
    }
}
