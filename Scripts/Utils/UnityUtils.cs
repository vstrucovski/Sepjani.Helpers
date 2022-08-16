using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Sepjani.Helpers.Scripts.Utils
{
    public static class UnityUtils
    {
        public static int NameToLayerBitmask(string name)
        {
            return 1 << LayerMask.NameToLayer(name);
        }

        public static float AsNegativeAngle(float angle)
        {
            return angle > 180 ? angle - 360 : angle;
        }

        public static bool IsBlockedByUI(string uiLayerName)
        {
            var currSelected = EventSystem.current.currentSelectedGameObject;
            return currSelected != null && currSelected.layer == NameToLayerBitmask(uiLayerName);
        }

        public static bool SequenceEquals<T>(this T[,] a, T[,] b)
        {
            return a.Rank == b.Rank &&
                   Enumerable.Range(0, a.Rank).All(d =>
                       a.GetLength(d) == b.GetLength(d)) &&
                   a.Cast<T>().SequenceEqual(b.Cast<T>());
        }

        public static void SetRate(this ParticleSystem system, int rate)
        {
            var systemEmission = system.emission;
            systemEmission.rateOverTime = new ParticleSystem.MinMaxCurve(rate);
        }


        public static void SetMeshVertexColor(Mesh mesh, Color color)
        {
            var colors = new Color[mesh.vertices.Length];
            for (var i = 0; i < mesh.vertices.Length; i++) colors[i] = color;

            mesh.colors = colors;
        }

        public static void Destroy(this GameObject go)
        {
            if (Application.isPlaying)
                Object.Destroy(go);
            else
                Object.DestroyImmediate(go);
        }

        public static void DestroyComponent(Component component)
        {
            if (Application.isPlaying)
                Object.Destroy(component);
            else
                Object.DestroyImmediate(component);
        }

        public static T GetRandom<T>(this List<T> self)
        {
            return self[Random.Range(0, self.Count)];
        }

        public static T GetRandom<T>(this T[] self)
        {
            return self[Random.Range(0, self.Length)];
        }

        public static T RandomEnum<T>()
        {
            var values = Enum.GetValues(typeof(T));
            int random = Random.Range(0, values.Length);
            return (T) values.GetValue(random);
        }

        internal static T[][] ToJaggedArray<T>(this T[,] twoDimensionalArray)
        {
            var rowsFirstIndex = twoDimensionalArray.GetLowerBound(0);
            var rowsLastIndex = twoDimensionalArray.GetUpperBound(0);
            var numberOfRows = rowsLastIndex + 1;

            var columnsFirstIndex = twoDimensionalArray.GetLowerBound(1);
            var columnsLastIndex = twoDimensionalArray.GetUpperBound(1);
            var numberOfColumns = columnsLastIndex + 1;

            var jaggedArray = new T[numberOfRows][];
            for (var i = rowsFirstIndex; i <= rowsLastIndex; i++)
            {
                jaggedArray[i] = new T[numberOfColumns];

                for (var j = columnsFirstIndex; j <= columnsLastIndex; j++)
                    jaggedArray[i][j] = twoDimensionalArray[i, j];
            }

            return jaggedArray;
        }

        public static void DebugInit(string text, string status)
        {
            UnityEngine.Debug.Log($"<color=cyan>{status}</color>: {text}");
        }

        public static void DebugInitError(string text, string status)
        {
            UnityEngine.Debug.Log($"<color=red>{status}</color>: {text}");
        }
    }
}