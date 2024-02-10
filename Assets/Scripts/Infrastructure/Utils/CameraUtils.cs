using UnityEngine;

namespace CodeBase.Utils
{
    public static class CameraUtils
    {
        public static Bounds PerspectiveBounds(this Camera camera, Vector3 point)
        {
            if (camera.orthographic)
            {
                Debug.Log($"The camera {camera.name} is not Perspective!", camera);
                return new Bounds();
            }

            var distance = Vector3.Distance(point, camera.transform.position);
            var frustumHeight = 2.0f * distance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            var frustumWidth = frustumHeight * camera.aspect;
            
            return new Bounds(point, new Vector3(frustumWidth, frustumHeight, 0));
        }
        
        public static Bounds OrthographicBounds(this Camera camera)
        {
            if (!camera.orthographic)
            {
                Debug.Log($"The camera {camera.name} is not Orthographic!", camera);
                return new Bounds();
            }
 
            var position = camera.transform.position;
            var x = position.x;
            var y = position.y;
            var size = camera.orthographicSize * 2;
            var width = size * (float)Screen.width / Screen.height;
            var height = size;
 
            return new Bounds(new Vector3(x, y, 0), new Vector3(width, height, 0));
        }
    }
}