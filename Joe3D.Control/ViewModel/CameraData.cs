using Joe3D.ViewControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joe3D.ViewControl.ViewModel
{
    public class CameraData : ICameraData
    {
        public double Rotation { get; protected set; }
        public double UpPercent { get; protected set; }
        public bool SetRotation(double rotation)
        {
            Rotation = rotation;
            return true;
        }
        public bool SetUpPercent(double upPercent)
        {
            UpPercent = upPercent;
            return true;
        }
    }
}
