using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joe3D.ViewControl.Interfaces
{
    public interface ICameraData
    {
        double Rotation { get; }
        /// <summary> value between -1.0 (looking straight up) and 1.0 (looking straight down) </summary>
        double UpPercent { get; }
        bool SetRotation(double rotation);
        bool SetUpPercent(double upPercent);
    }
}
