#if UNITY_EDITOR

using System;
using UnityEngine;

namespace _Project.EditorAssistant
{
    public class EditorDrawer : MonoBehaviour
    {
        [Serializable]
        private class Draw
        {
            public Transform target;
            public Color fillColor;
            public Color wireColor;
            public Vector3 offsetPosition;
            public Vector3 scale;
            public float radius;
            public float wireMargin;
            public bool localToWorldMatrix;

            public enum ShapeType
            {
                Cube,
                Sphere
            }

            public ShapeType shape;
        }

        [SerializeField] private Draw[] draws;
        private void OnDrawGizmos()
        {
            foreach (var draw in draws)
            {
                Vector3 position;
                if (draw.localToWorldMatrix)
                {
                    Gizmos.matrix = draw.target.localToWorldMatrix;
                    position = Vector3.zero;
                }
                else
                {
                    position = draw.target.position;
                }

                Gizmos.color = draw.fillColor;
                switch (draw.shape)
                {
                    case Draw.ShapeType.Cube:
                        Gizmos.DrawCube(position + draw.offsetPosition, draw.scale);
                        Gizmos.color = draw.wireColor;
                        Gizmos.DrawWireCube(position + draw.offsetPosition, draw.scale + Vector3.one * draw.wireMargin);
                        break;
                    case Draw.ShapeType.Sphere:
                        Gizmos.DrawSphere(position + draw.offsetPosition, draw.radius);
                        Gizmos.color = draw.wireColor;
                        Gizmos.DrawWireSphere(position + draw.offsetPosition, draw.radius);
                        break;
                }
            }
        }
    }
}
#endif