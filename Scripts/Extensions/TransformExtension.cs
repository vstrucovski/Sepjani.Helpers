using Sepjani.Helpers.Scripts.Utils;
using UnityEngine;

namespace Sepjani.Helpers.Scripts.Extensions
{
    public static class TransformExtension
    {
        public static void Reset(this Transform transform)
        {
            transform.ResetPosition();
            transform.ResetRotation();
            transform.ResetScale();
        }

        public static void ResetPosition(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
        }

        public static void ResetRotation(this Transform transform)
        {
            transform.localEulerAngles = Vector3.zero;
        }

        public static void ResetScale(this Transform transform)
        {
            transform.localScale = Vector3.one;
        }

        public static void DestroyAllChildren(this Transform transform, int startFrom = 0)
        {
            for (var i = transform.childCount - 1; i >= startFrom; i--)
            {
                var child = transform.GetChild(i);
                child.gameObject.Destroy();
            }
        }

        public static void DestroyAllChildren(this Transform transform, GameObject except)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i);
                if (except == child.gameObject) continue;
                child.gameObject.Destroy();
            }
        }

        public static void MoveAllChildren(this Transform transform, Transform newParent)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i);
                child.SetParent(newParent);
            }
        }

        public static Bounds CalculateBounds(this Transform transform)
        {
            var currentPosition = transform.position;
            var currentRotation = transform.rotation;

            var bounds = new Bounds(transform.position, Vector3.zero);
            foreach (var renderer in transform.GetComponentsInChildren<Renderer>()) bounds.Encapsulate(renderer.bounds);

            var localCenter = bounds.center - transform.position;
            bounds.center = localCenter;
            transform.position = currentPosition;
            transform.rotation = currentRotation;
            return bounds;
        }

        public static Bounds CalculateBounds(this Transform[] transforms)
        {
            var bounds = new Bounds(Vector3.zero, Vector3.zero);
            foreach (var transform in transforms)
            foreach (var renderer in transform.GetComponentsInChildren<Renderer>())
                bounds.Encapsulate(renderer.bounds);
            return bounds;
        }

        public static Bounds CalculateBounds2(this Transform transform)
        {
            var bounds = new Bounds(transform.position, Vector3.zero);
            foreach (var renderer in transform.GetComponentsInChildren<Renderer>()) bounds.Encapsulate(renderer.bounds);

            return bounds;
        }

        public static Bounds TransformBounds(this Transform _transform, Bounds _localBounds)
        {
            var center = _transform.TransformPoint(_localBounds.center);

            // transform the local extents' axes
            var extents = _localBounds.extents;
            var axisX = _transform.TransformVector(extents.x, 0, 0);
            var axisY = _transform.TransformVector(0, extents.y, 0);
            var axisZ = _transform.TransformVector(0, 0, extents.z);

            // sum their absolute value to get the world extents
            extents.x = Mathf.Abs(axisX.x) + Mathf.Abs(axisY.x) + Mathf.Abs(axisZ.x);
            extents.y = Mathf.Abs(axisX.y) + Mathf.Abs(axisY.y) + Mathf.Abs(axisZ.y);
            extents.z = Mathf.Abs(axisX.z) + Mathf.Abs(axisY.z) + Mathf.Abs(axisZ.z);

            return new Bounds {center = center, extents = extents};
        }
    }
}