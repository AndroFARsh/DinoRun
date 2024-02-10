using UnityEngine;

namespace CodeBase.Utils
{
    public static class MatrixUtils
    {
        public static Matrix4x4 Sum(this Matrix4x4 m1, Matrix4x4 m2) =>
            new()
            {
                m00 = m1.m00 + m2.m00, m01 = m1.m01 + m2.m01, m02 = m1.m02 + m2.m02, m03 = m1.m03 + m2.m03,
                m10 = m1.m10 + m2.m10, m11 = m1.m11 + m2.m11, m12 = m1.m12 + m2.m12, m13 = m1.m13 + m2.m13,
                m20 = m1.m20 + m2.m20, m21 = m1.m21 + m2.m21, m22 = m1.m22 + m2.m22, m23 = m1.m23 + m2.m23,
                m30 = m1.m30 + m2.m30, m31 = m1.m31 + m2.m31, m32 = m1.m32 + m2.m32, m33 = m1.m33 + m2.m33,
            };

        public static Matrix4x4 Mul(this Matrix4x4 matrix, float value) =>
            new()
            {
                m00 = matrix.m00 * value, m01 = matrix.m01 * value, m02 = matrix.m02 * value, m03 = matrix.m03 * value,
                m10 = matrix.m10 * value, m11 = matrix.m11 * value, m12 = matrix.m12 * value, m13 = matrix.m13 * value,
                m20 = matrix.m20 * value, m21 = matrix.m21 * value, m22 = matrix.m22 * value, m23 = matrix.m23 * value,
                m30 = matrix.m30 * value, m31 = matrix.m31 * value, m32 = matrix.m32 * value, m33 = matrix.m33 * value,
            };
    }
}