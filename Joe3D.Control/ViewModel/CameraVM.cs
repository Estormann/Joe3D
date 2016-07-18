using GalaSoft.MvvmLight;
using Joe3D.ViewControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Joe3D.ViewControl.ViewModel
{
    public class CameraVM : ViewModelBase
    {
        bool _isSettingData;
        /// <summary>
        /// The <see cref="CameraData" /> dependency property's name.
        /// </summary>
        public const string CameraDataPropertyName = "CameraData";

        private ICameraData _CameraData;

        public ICameraData CameraData
        {
            get { return _CameraData; }
            set
            {
                Set(() => CameraData, ref _CameraData, value, true);
                OnCameraDataChanged(value);
            }
        }
        protected void OnCameraDataChanged(ICameraData newvalue)
        {
            ICameraData newCameraData = newvalue;
            this._isSettingData = true;
            Rotation = newCameraData.Rotation;
            UpPercent = newCameraData.UpPercent;
            _isSettingData = false;
            RefreshCamera(newCameraData);
        }
        private double _Rotation;

        public double Rotation
        {
            get { return _Rotation; }
            set
            {
                Set(() => Rotation, ref _Rotation, value, true);
                OnRotationChanged(value);
            }
        }
        
        protected void OnRotationChanged(double NewValue)
        {
            if (!_isSettingData)
            {
                if (CameraData != null
                    && CameraData.SetRotation(NewValue))
                {
                    RefreshCamera(CameraData);
                }
            }
        }
        private double _UpPercent;

        public double UpPercent
        {
            get { return _UpPercent; }
            set
            {
                Set(() => UpPercent, ref _UpPercent, value, true);
                OnUpPercentChanged(value);

            }
        }
        protected void OnUpPercentChanged(double newUpPercent)
        {
            if (!_isSettingData)
            {
                if (CameraData != null
                    && CameraData.SetUpPercent(newUpPercent))
                {
                    RefreshCamera(CameraData);
                }
            }
        }

        private Point3D _Position;

        public Point3D Position
        {
            get { return _Position; }
            set
            {
                Set(() => Position, ref _Position, value, true);
            }
        }

        private Vector3D _LookDirection;

        public Vector3D LookDirection
        {
            get { return _LookDirection; }
            set
            {
                Set(() => LookDirection, ref _LookDirection, value, true);
            }

        }
        private Vector3D _UpDirection;

        public Vector3D UpDirection
        {
            get { return _UpDirection; }
            set
            {
                Set(() => UpDirection, ref _UpDirection, value, true);
            }
        }
        private void RefreshCamera(ICameraData cameraData)
        {
            double rotation = cameraData != null
                ? cameraData.Rotation
                : 0;
            double cameraX = Math.Sin(rotation);
            double cameraZ = Math.Cos(rotation);
            double upDownPercent = cameraData != null
                ? cameraData.UpPercent
                : 0;
            double rotationPercent = 1.0 - Math.Abs(upDownPercent);
            Vector3D straightUpVector = new Vector3D(0.0, 1.0, 0.0);
            Vector3D rotationVector = new Vector3D(cameraX, 0.0, cameraZ);
            Vector3D rightVector = Vector3D.CrossProduct(
                straightUpVector,
                rotationVector);
            Vector3D positionVector = new Vector3D(
                rotationVector.X * rotationPercent,
                straightUpVector.Y * upDownPercent,
                rotationVector.Z * rotationPercent);
            Vector3D lookVector = Vector3D.Subtract(
                new Vector3D(),
                positionVector);
            Vector3D upVector = Vector3D.CrossProduct(
                positionVector,
                rightVector);
            upVector.Normalize();
            lookVector.Normalize();
            positionVector.Normalize();
            positionVector = positionVector * 2.5;
            Position = new Point3D(
                positionVector.X,
                positionVector.Y,
                positionVector.Z);
            LookDirection = lookVector;
            UpDirection = upVector;
        }
    }
}
