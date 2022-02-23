using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDrawerTool : MonoBehaviour
{
    public Mesh mesh;
    public static GizmoDrawerTool instance {
        get { 
            if(skeleton == null)
            {
                GameObject temp = new GameObject();
                temp.name = "GizmoDrawerTool";
                skeleton = temp.AddComponent<GizmoDrawerTool>();
            }
            return skeleton; }
    
    }
    public Color colorGimoz = Color.green;
    public List<colliderGizmo> listCollider =new List<colliderGizmo>();
    public int frameDestroy;
    static GizmoDrawerTool skeleton;
    private void Awake()
    {
        if (skeleton == null)
        {
            skeleton = this;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = colorGimoz;
        if (listCollider != null)
        {
            for (int i = 0; i < listCollider.Count; i++)
            {
                if (listCollider[i].flameDestroy < frameDestroy)
                {
                    switch (listCollider[i].colliderType)
                    {
                        case colliderType.Box:
                            Gizmos.DrawWireCube(listCollider[i].position, listCollider[i].size);
                            //Gizmos.DrawWireMesh(mesh, listCollider[i].position, Quaternion.Euler(0, 0, listCollider[i].angle),listCollider[i].size);
                            break;
                        case colliderType.Circle:
                            Gizmos.DrawWireSphere(listCollider[i].position, listCollider[i].size.x);
                            break;
                    }
                    listCollider[i].flameDestroy++;
                }
                else
                {
                    listCollider.RemoveAt(i);
                }
            }
        }
    }
    public void draw(Vector3 position, Vector3 sizeBox, colliderType colliderType,float angle )
    {
        if (listCollider == null) listCollider = new List<colliderGizmo>();
        listCollider.Add(new colliderGizmo(position, sizeBox, colliderType,angle));
    }
    [System.Serializable]
    public class colliderGizmo
    {
        public Vector3 position;
        public Vector3 size;
        public colliderType colliderType;
        public float angle;
        public int flameDestroy;
        public colliderGizmo(Vector3 position, Vector3 sizeBox, colliderType colliderType, float angle)
        {
            this.position = position;
            this.size = sizeBox;
            this.colliderType = colliderType;
            this.flameDestroy = 0;
            this.angle = angle;
        }
    }
    public enum colliderType
    {
        Box,
        Circle,
    }

}
