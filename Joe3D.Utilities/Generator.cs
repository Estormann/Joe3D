﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Joe3D.Utilities
{
    public static class Generator
    {
        public static Model3D GetCube()
        {
            GeometryModel3D Cube1 = new GeometryModel3D();
            MeshGeometry3D cubeMesh = GetMeshCube();
            Cube1.Geometry = cubeMesh;
            Cube1.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Green));
            Model3DGroup modelGroup = new Model3DGroup();
            modelGroup.Children.Add(Cube1);
            return modelGroup;
        }
        public static Model3D GetObjModel(string model3DPath)
        {
            var mi = new Joe3D.Utilities.ModelImporter();
            return mi.Load(model3DPath);
        }
        public static Model3DGroup GetLight()
        {
            //Lighting
            DirectionalLight light = new DirectionalLight(Colors.White, new Vector3D(-1,-1,-1));
            DirectionalLight light2 = new DirectionalLight(Colors.White, new Vector3D(1,1,1));
            //ModelGroup
            Model3DGroup modelGroup = new Model3DGroup();
            modelGroup.Children.Add(light);
            modelGroup.Children.Add(light2);
            return modelGroup;
        }
        private static Int32[] getindices()
        {
            Int32[] indices ={
            //front
                  0,1,2,
                  0,2,3,
            //back
                  4,7,6,
                  4,6,5,
            //Right
                  4,0,3,
                  4,3,7,
            //Left
                  1,5,6,
                  1,6,2,
            //Top
                  1,0,4,
                  1,4,5,
            //Bottom
                  2,6,7,
                  2,7,3
            };
            return indices;
        }
        private static Int32Collection getTriangles(Int32[] indices)
        {
            Int32Collection Triangles = new Int32Collection();
            foreach (Int32 index in indices)
            {
                Triangles.Add(index);
            }
            return Triangles;
        }
        private static MeshGeometry3D GetMeshCube()
        {
            MeshGeometry3D cube = new MeshGeometry3D();
            Point3DCollection corners = new Point3DCollection();

            corners.Add(new Point3D(0.5, 0.5, 0.5));
            corners.Add(new Point3D(-0.5, 0.5, 0.5));
            corners.Add(new Point3D(-0.5, -0.5, 0.5));
            corners.Add(new Point3D(0.5, -0.5, 0.5));
            corners.Add(new Point3D(0.5, 0.5, -0.5));
            corners.Add(new Point3D(-0.5, 0.5, -0.5));
            corners.Add(new Point3D(-0.5, -0.5, -0.5));
            corners.Add(new Point3D(0.5, -0.5, -0.5));
            cube.Positions = corners;
            cube.TriangleIndices = getTriangles(getindices());
            return cube;
        }
        
        public static Matrix3D CalculateRotationMatrix(double x, double y, double z)
        {
            Matrix3D matrix = new Matrix3D();

            matrix.Rotate(new Quaternion(new Vector3D(1, 0, 0), x));
            matrix.Rotate(new Quaternion(new Vector3D(0, 1, 0) * matrix, y));
            matrix.Rotate(new Quaternion(new Vector3D(0, 0, 1) * matrix, z));

            return matrix;
        }

    }
}
