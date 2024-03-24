using System;
using UnityEngine;

namespace Utils
{
    public static class Utils
    {
        public static T GetElementOfArray<T>(int index, T[] array)
        {
            return array[index - (array.Length * (index / array.Length))];
        }

        public static float EvaluteFloat(float min, float max, float evalute)
        {
            float minEvalute = 1 - evalute;
            return min * minEvalute + max * evalute;
        }

        public static T2 Unboxing<T2>(object obj)
        {
            if (obj.GetType() != typeof(T2))
            {
                throw new InvalidOperationException();
            }

            return (T2)obj;
        }
    }

    public static class Utils2D
    {
        public static int GetAngleBetween(Transform from, Transform to)
        {
            if (to == null)
            {
                return 0;
            }

            Quaternion targetRotation = GetRotationTo(from.position, to.position);
            int angle = Mathf.CeilToInt(targetRotation.eulerAngles.z);

            return angle;
        }

        private static Quaternion GetRotationTo(Vector2 from, Vector2 to)
        {
            var dir = to - from;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }
    }

    public static class DebugGizmos
    {
        public static void DrawRay(Vector3 start, Vector3 end, Color color)
        {
            UnityEngine.Debug.DrawRay(start, end, color);
        }

        public static void DrawPoint(Vector3 center, float radius, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(center, radius);
        }
    }
}
