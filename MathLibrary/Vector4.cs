using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public struct Vector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public float Magnitude
        {
            get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W); }
        }

        public Vector4 Normalized
        {
            get
            {
                Vector4 value = this;
                return value.Normalize();
            }
        }

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector4 Normalize()
        {
            if (Magnitude == 0)
                return new Vector4();

            return this /= Magnitude;
        }

        public static float DotProduct(Vector4 lhs, Vector4 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y) + (lhs.Z * rhs.Z) + (lhs.W * rhs.W);
        }

        public static Vector4 CrossProduct(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4
                (
                    (lhs.Y * rhs.Z) - (lhs.Z * rhs.Y),
                    (lhs.Z * rhs.X) - (lhs.X * rhs.Z),
                    (lhs.X * rhs.Y) - (lhs.Y * rhs.X),
                    0
                );
        }

        public static float Distance(Vector4 lhs, Vector4 rhs)
        {
            return (rhs - lhs).Magnitude;
        }

        public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4 { X = lhs.X - rhs.X, Y = lhs.Y - rhs.Y, Z = lhs.Z - rhs.Z, W = lhs.W - rhs.W };
        }

        public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4 { X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y, Z = lhs.Z + rhs.Z, W = lhs.W + rhs.W };
        }

        public static Vector4 operator *(Vector4 lhs, float rhs)
        {
            Vector4 result = new Vector4();

            result.X = lhs.X *= rhs;
            result.Y = lhs.Y *= rhs;
            result.Z = lhs.Z *= rhs;
            result.W = lhs.W *= rhs;

            return result;
        }

        public static Vector4 operator *(float lhs, Vector4 rhs)
        {
            Vector4 result = new Vector4();

            result.X = rhs.X *= lhs;
            result.Y = rhs.Y *= lhs;
            result.Z = rhs.Z *= lhs;
            result.W = rhs.W *= lhs;

            return result;
        }


        public static Vector4 operator /(Vector4 lhs, float rhs)
        {
            Vector4 result = new Vector4();

            result.X = lhs.X /= rhs;
            result.Y = lhs.Y /= rhs;
            result.Z = lhs.Z /= rhs;
            result.W = lhs.W /= rhs;

            return result;
        }

        public static bool operator ==(Vector4 lhs, Vector4 rhs)
        {
            if (lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z && lhs.W == rhs.W)
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(Vector4 lhs, Vector4 rhs)
        {
            if (lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z || lhs.W != rhs.W)
            {
                return true;
            }

            return false;
        }
    }
}
