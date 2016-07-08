using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace Joe3D.Utilities
{
    public static class ModelLoader
    {
        public static async Task<Model3DGroup> LoadAsync(string model3DPath, bool freeze, Dispatcher dispatcher)
        {
            return await Task.Factory.StartNew(() =>
            {
                var mi = new Utilities.ModelImporter();

                if (freeze)
                {
                    // Alt 1. - freeze the model 
                    return mi.Load(model3DPath, null, true);
                }

                // Alt. 2 - create the model on the UI dispatcher
                return mi.Load(model3DPath, dispatcher);
            });
        }
        public static Model3DGroup Load(string model3DPath, bool freeze, Dispatcher dispatcher)
        {
            var mi = new Utilities.ModelImporter();

            if (freeze)
            {
                // Alt 1. - freeze the model 
                return mi.Load(model3DPath, null, true);
            }

            // Alt. 2 - create the model on the UI dispatcher
            return mi.Load(model3DPath, dispatcher);
        }
    }
}
