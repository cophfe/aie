using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace ConsoleImproved
{
    class RenderObject : Program
    {
        // would make this if i could be bothered, but alas
        Vector3[] points; 
        Vector3 rotation; 
        Vector3 position; 
        float scale; 
        int[,] edges;
        public RenderObject(Vector3[] points, Vector3 rotation, Vector3 position, float scale, int[,] edges)
        {

            this.points = points;
            this.rotation = rotation;
            this.position = position;
            this.scale = scale;
            this.edges = edges;
        }

        public Vector3[] toWorldPoints()
        {
            return TransformVectorArray(points, GenerateMatrix(position, rotation));
        } 
        //public Vector3[] toCameraPoints()
        //{
        //    return TransformVectorArray(toWorldPoints(), CameraObject)
        //}
    }
}
