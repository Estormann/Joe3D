using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Joe3D.ViewControl.Interfaces
{
    public interface IMeshData
    {
        IEnumerable<Point3D> Vertices { get; }
        IEnumerable<int> TriangleIndices { get; }
    }
}
