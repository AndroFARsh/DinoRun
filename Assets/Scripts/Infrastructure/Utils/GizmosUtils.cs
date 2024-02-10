using UnityEngine;

namespace Infrastructure.Utils
{
    public static class GizmosUtils
    {
        public static void DrawMarker(Vector3 position, float size, Color color)
        {
            var line1PosA = position + Vector3.up * (size * 0.5f);
            var line1PosB = position - Vector3.up * (size * 0.5f);

            var line2PosA = position + Vector3.right * (size * 0.5f);
            var line2PosB = position - Vector3.right * (size * 0.5f);

            var line3PosA = position + Vector3.forward * (size * 0.5f);
            var line3PosB = position - Vector3.forward * size * 0.5f;

            var prevColor = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawLine(line1PosA, line1PosB);
            Gizmos.DrawLine(line2PosA, line2PosB);
            Gizmos.DrawLine(line3PosA, line3PosB);
            Gizmos.color = prevColor;
        }
        
        public static void DrawPlane(Vector3 position, Vector3 normal, float size, Color color)
        {
            Vector3 v3 = normal.normalized != Vector3.forward 
                ? Vector3.Cross(normal, Vector3.forward).normalized * normal.magnitude
                : Vector3.Cross(normal, Vector3.up).normalized * normal.magnitude;
            
            var corner0 = position + v3 * size;
            var corner2 = position - v3 * size;

            var q = Quaternion.AngleAxis(90.0f, normal);
            
            var corner1 = position + q * v3 * size;
            var corner3 = position - q * v3 * size;

            var prevColor = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawLine(corner0, corner2);
            Gizmos.DrawLine(corner1, corner3);
            Gizmos.DrawLine(corner0, corner1);
            
            Gizmos.DrawLine(corner1, corner2);
            Gizmos.DrawLine(corner2, corner3);
            Gizmos.DrawLine(corner3, corner0);
            Gizmos.DrawRay(position, normal * size);
            Gizmos.color = prevColor;
        }
        
        public static void DrawVector(Vector3 position, Vector3 direction, float raySize, float markerSize, Color color)
        {
            Debug.DrawRay(position, direction * raySize, color);
            
            if (markerSize > 0) DrawMarker(position, markerSize, color);
        }

        public static void DrawTriangle(Vector3 a, Vector3 b, Vector3 c, Color color, Transform t = null)
        {
            a = t ? t.TransformPoint(a) : a;
            b = t ? t.TransformPoint(b) : b;
            c = t ? t.TransformPoint(c) : c;

            var prevColor = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawLine(a, b);
            Gizmos.DrawLine(b, c);
            Gizmos.DrawLine(c, a);
            Gizmos.color = prevColor;
        }

        public static void DrawMesh(Mesh mesh, Color color, Transform t)
        {
            for (var i = 0; i < mesh.triangles.Length; i += 3)
            {
                DrawTriangle(mesh.vertices[mesh.triangles[i]], mesh.vertices[mesh.triangles[i + 1]],
                    mesh.vertices[mesh.triangles[i + 2]], color, t);
            }
        }
        
        public static void DrawArk(Vector3 o, float radius, float fromAngle, float toAngle, Color color, Transform t = null, int numSegment = 32)
        {
            {
                var p = o + new Vector3
                {
                    x = radius * Mathf.Cos(fromAngle * Mathf.Deg2Rad),
                    y = radius * Mathf.Sin(fromAngle * Mathf.Deg2Rad)
                };
                var prevColor = Gizmos.color;
                Gizmos.color = color;
                Gizmos.DrawLine(o, t ? t.TransformPoint(p) : p);
                Gizmos.color = prevColor;
            }
            
            {
                var p = o + new Vector3
                {
                    x = radius * Mathf.Cos(toAngle * Mathf.Deg2Rad),
                    y = radius * Mathf.Sin(toAngle * Mathf.Deg2Rad)
                };

                var prevColor = Gizmos.color;
                Gizmos.color = color;
                Gizmos.DrawLine(o, t ? t.TransformPoint(p) : p);
                Gizmos.color = prevColor;
            }
            
            numSegment = Mathf.Max(numSegment, 4);
            
            var stepAngle = 360.0f / numSegment;
            for (var angle = fromAngle; angle <= toAngle; angle += stepAngle)
            {
                var a = o + new Vector3{
                    x = radius * Mathf.Cos(angle * Mathf.Deg2Rad), 
                    y = radius * Mathf.Sin(angle * Mathf.Deg2Rad)
                };
                
                var b = o + new Vector3{
                    x = radius * Mathf.Cos(Mathf.Min(angle + stepAngle, toAngle) * Mathf.Deg2Rad), 
                    y = radius * Mathf.Sin(Mathf.Min(angle + stepAngle, toAngle) * Mathf.Deg2Rad)
                };
                
                a = t ? t.TransformPoint(a) : a;
                b = t ? t.TransformPoint(b) : b;
                
                var prevColor = Gizmos.color;
                Gizmos.color = color;
                Gizmos.DrawLine(a, b);
                Gizmos.color = prevColor;
            }
        }
        
        public static void DrawCircle(Vector3 o, float radius, Color color, Transform t = null, int numSegment = 32)
        {
            numSegment = Mathf.Max(numSegment, 4);
            
            var stepAngle = 360.0f / numSegment;
            for (var angle = 0.0f; angle <= 360.0f; angle += stepAngle)
            {
                var a = o + new Vector3{
                    x = radius * Mathf.Cos(angle * Mathf.Deg2Rad), 
                    y = radius * Mathf.Sin(angle * Mathf.Deg2Rad)
                };
                
                var b = o + new Vector3{
                    x = radius * Mathf.Cos((angle + stepAngle) * Mathf.Deg2Rad), 
                    y = radius * Mathf.Sin((angle + stepAngle) * Mathf.Deg2Rad)
                };
                
                a = t ? t.TransformPoint(a) : a;
                b = t ? t.TransformPoint(b) : b;
                
                var prevColor = Gizmos.color;
                Gizmos.color = color;
                Gizmos.DrawLine(a, b);
                Gizmos.color = prevColor;
            }
        }

        public static void DrawBox(Vector3 o, Vector2 size, Color color, Transform t = null)
        {
            var halfSize = size * 0.5f;
            
            var a = o + new Vector3 { x = -halfSize.x, y =  halfSize.y};
            var b = o + new Vector3 { x = -halfSize.x, y = -halfSize.y};
            var c = o + new Vector3 { x =  halfSize.x, y = -halfSize.y};
            var d = o + new Vector3 { x =  halfSize.x, y =  halfSize.y};
            
            a = t ? t.TransformPoint(a) : a;
            b = t ? t.TransformPoint(b) : b;
            c = t ? t.TransformPoint(c) : c;
            d = t ? t.TransformPoint(d) : d;

            var prevColor = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawLine(a, b);
            Gizmos.DrawLine(b, c);
            Gizmos.DrawLine(c, d);
            Gizmos.DrawLine(d, a);
            Gizmos.color = prevColor;
        }

        public static void DrawCapsule(Vector3 o, Vector2 size, CapsuleDirection2D direction, Color color,
            Transform t = null, int numSegment = 32)
        {
            switch (direction)
            {
                case CapsuleDirection2D.Horizontal:
                    DrawCapsuleHorizontal(o, size, color, t, numSegment);
                    break;
                case CapsuleDirection2D.Vertical:
                    DrawCapsuleVertical(o, size, color, t, numSegment);
                    break;
            }
        }

        public static void DrawCapsuleVertical(Vector3 o, Vector2 size, Color color, Transform t = null, int numSegment = 32)
        {
            var lineLength = size.y - size.x;
            
            var dir = Vector2.up;

            // The position of the radius of the upper sphere in local coordinates
            var upperSphere = o + (Vector3)dir * (lineLength * 0.5f);
            // The position of the radius of the lower sphere in local coordinates
            var lowerSphere = o - (Vector3)dir * (lineLength * 0.5f);
         
            numSegment = Mathf.Max(numSegment, 4);

            var radius = size.x * 0.5f;
            var stepAngle = 360.0f / numSegment;
            for (var angle = 0.0f; angle <= 180.0f; angle += stepAngle)
            {
                var a = upperSphere + new Vector3{
                    x = radius * Mathf.Cos(angle * Mathf.Deg2Rad), 
                    y = radius * Mathf.Sin(angle * Mathf.Deg2Rad)
                };
                
                var b = upperSphere + new Vector3{
                    x = radius * Mathf.Cos((angle + stepAngle) * Mathf.Deg2Rad), 
                    y = radius * Mathf.Sin((angle + stepAngle) * Mathf.Deg2Rad)
                };
                
                a = t ? t.TransformPoint(a) : a;
                b = t ? t.TransformPoint(b) : b;
                
                var prevColor = Gizmos.color;
                Gizmos.color = color;
                Gizmos.DrawLine(a, b);
                Gizmos.color = prevColor;
            }

            {
                var halfSize = new Vector2(size.x, lineLength) * 0.5f;

                var a = o + new Vector3 {x = -halfSize.x, y =  halfSize.y};
                var b = o + new Vector3 {x = -halfSize.x, y = -halfSize.y};
                var c = o + new Vector3 {x =  halfSize.x, y = -halfSize.y};
                var d = o + new Vector3 {x =  halfSize.x, y =  halfSize.y};

                a = t ? t.TransformPoint(a) : a;
                b = t ? t.TransformPoint(b) : b;
                c = t ? t.TransformPoint(c) : c;
                d = t ? t.TransformPoint(d) : d;

                var prevColor = Gizmos.color;
                Gizmos.color = color;
                Gizmos.DrawLine(a, b);
                Gizmos.DrawLine(c, d);
                Gizmos.color = prevColor;
            }
            
            for (var angle = 180.0f; angle <= 360.0f; angle += stepAngle)
            {
                var a = lowerSphere + new Vector3{
                    x = radius * Mathf.Cos(angle * Mathf.Deg2Rad), 
                    y = radius * Mathf.Sin(angle * Mathf.Deg2Rad)
                };
                
                var b = lowerSphere + new Vector3{
                    x = radius * Mathf.Cos((angle + stepAngle) * Mathf.Deg2Rad), 
                    y = radius * Mathf.Sin((angle + stepAngle) * Mathf.Deg2Rad)
                };
                
                a = t ? t.TransformPoint(a) : a;
                b = t ? t.TransformPoint(b) : b;
                
                var prevColor = Gizmos.color;
                Gizmos.color = color;
                Gizmos.DrawLine(a, b);
                Gizmos.color = prevColor;
            }
            
        }
        
        public static void DrawCapsuleHorizontal(Vector3 o, Vector2 size, Color color, Transform t = null, int numSegment = 32)
        {
            var lineLength = size.x - size.y;
            
            var dir = Vector2.right;

            // The position of the radius of the upper sphere in local coordinates
            var upperSphere = o + (Vector3)dir * (lineLength * 0.5f);
            // The position of the radius of the lower sphere in local coordinates
            var lowerSphere = o - (Vector3)dir * (lineLength * 0.5f);
         
            numSegment = Mathf.Max(numSegment, 4);

            var radius = size.y * 0.5f;
            var stepAngle = 360.0f / numSegment;
            for (var angle = -90.0f; angle <= 90.0f; angle += stepAngle)
            {
                var a = upperSphere + new Vector3{
                    x = radius * Mathf.Cos(angle * Mathf.Deg2Rad), 
                    y = radius * Mathf.Sin(angle * Mathf.Deg2Rad)
                };
                
                var b = upperSphere + new Vector3{
                    x = radius * Mathf.Cos((angle + stepAngle) * Mathf.Deg2Rad), 
                    y = radius * Mathf.Sin((angle + stepAngle) * Mathf.Deg2Rad)
                };
                
                a = t ? t.TransformPoint(a) : a;
                b = t ? t.TransformPoint(b) : b;
                
                var prevColor = Gizmos.color;
                Gizmos.color = color;
                Gizmos.DrawLine(a, b);
                Gizmos.color = prevColor;
            }

            {
                var halfSize = new Vector2(lineLength, size.y) * 0.5f;

                var a = o + new Vector3 {x = -halfSize.x, y =  halfSize.y};
                var b = o + new Vector3 {x = -halfSize.x, y = -halfSize.y};
                var c = o + new Vector3 {x =  halfSize.x, y = -halfSize.y};
                var d = o + new Vector3 {x =  halfSize.x, y =  halfSize.y};

                a = t ? t.TransformPoint(a) : a;
                b = t ? t.TransformPoint(b) : b;
                c = t ? t.TransformPoint(c) : c;
                d = t ? t.TransformPoint(d) : d;

                var prevColor = Gizmos.color;
                Gizmos.color = color;
                Gizmos.DrawLine(a, d);
                Gizmos.DrawLine(c, b);
                Gizmos.color = prevColor;
            }
            
            for (var angle = 90.0f; angle <= 270.0f; angle += stepAngle)
            {
                var a = lowerSphere + new Vector3{
                    x = radius * Mathf.Cos(angle * Mathf.Deg2Rad), 
                    y = radius * Mathf.Sin(angle * Mathf.Deg2Rad)
                };
                
                var b = lowerSphere + new Vector3{
                    x = radius * Mathf.Cos((angle + stepAngle) * Mathf.Deg2Rad), 
                    y = radius * Mathf.Sin((angle + stepAngle) * Mathf.Deg2Rad)
                };
                
                a = t ? t.TransformPoint(a) : a;
                b = t ? t.TransformPoint(b) : b;
                
                var prevColor = Gizmos.color;
                Gizmos.color = color;
                Gizmos.DrawLine(a, b);
                Gizmos.color = prevColor;
            }
            
        }
        
        public static void DrawPath(Vector2[] path, Color color, Transform t = null)
        {
            for (var i = 0; i < path.Length; i++)
            {
                var a = (Vector3)path[i];
                var b = (Vector3)path[(i + 1) % path.Length];
                
                a = t ? t.TransformPoint(a) : a;
                b = t ? t.TransformPoint(b) : b;
                
                var prevColor = Gizmos.color;
                Gizmos.color = color;
                Gizmos.DrawLine(a, b);
                Gizmos.color = prevColor;
            }
        }
        
        public static Color RandomColor()
        {
            return new Color(Random.value, Random.value, Random.value);
        }
    }
}