using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public float Magnitude
        {
            get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z); }
        }

        public Vector3 Normalized
        {
            get 
            {
                Vector3 value = this;
                return value.Normalize();
            }
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3 UP
        {
            get { return new Vector3(0, 1, 0); }
        }

        public static Vector3 RIGHT
        {
            get { return new Vector3(1, 0, 0); }
        }

        public Vector3 Normalize()
        {
            if (Magnitude == 0)
                return new Vector3();

            return this /= Magnitude;
        }

        public static float DotProduct(Vector3 lhs, Vector3 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y) + (lhs.Z * rhs.Z);
        }

        public static Vector3 CrossProduct(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3
                (
                    (lhs.Y * rhs.Z) - (lhs.Z * rhs.Y),
                    (lhs.Z * rhs.X) - (lhs.X * rhs.Z),
                    (lhs.X * rhs.Y) - (lhs.Y * rhs.X)
                );
        }

        public static float Distance(Vector3 lhs, Vector3 rhs)
        {
            return (rhs - lhs).Magnitude;
        }

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3 { X = lhs.X - rhs.X, Y = lhs.Y - rhs.Y, Z = lhs.Z - rhs.Z };
        }

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3 { X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y, Z = lhs.Z + rhs.Z };
        }

        public static Vector3 operator *(Vector3 lhs, float rhs)
        {
            Vector3 result = new Vector3();

            result.X = lhs.X *= rhs;
            result.Y = lhs.Y *= rhs;
            result.Z = lhs.Z *= rhs;

            return result;
        }

        public static Vector3 operator *(float lhs, Vector3 rhs)
        {
            Vector3 result = new Vector3();

            result.X = rhs.X *= lhs;
            result.Y = rhs.Y *= lhs;
            result.Z = rhs.Z *= lhs;           

            return result;
        }

        public static Vector3 operator /(Vector3 lhs, float rhs)
        {
            Vector3 result = new Vector3();

            result.X = lhs.X /= rhs;
            result.Y = lhs.Y /= rhs;
            result.Z = lhs.Z /= rhs;

            return result;
        }

        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            if (lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z)
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            if (lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z)
            {
                return true;
            }

            return false;
        }
    }
}
