using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDrawerTool : MonoBehaviour
{
    public static GizmoDrawerTool instance {
        get { 
            if(skeleton == null)
            {
                GameObject temp = new GameObject();
                skeleton = temp.AddComponent<GizmoDrawerTool>();
                temp.name = "GizmoDrawerTool";
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
            //for (int i = 0; i < listCollider.Count; i++)
            //{
            //    if (listCollider[i].flameDestroy >= frameDestroy)
            //    {
            //        listCollider.RemoveAt(i);
            //    }
            //}

        }
    }
    public void draw(Vector3 position, Vector3 sizeBox, colliderType colliderType )
    {
        if (listCollider == null) listCollider = new List<colliderGizmo>();
        listCollider.Add(new colliderGizmo(position, sizeBox, colliderType));
    }
    [System.Serializable]
    public class colliderGizmo
    {
        public Vector3 position;
        public Vector3 size;
        public colliderType colliderType;
        public int flameDestroy;
        public colliderGizmo(Vector3 position, Vector3 sizeBox, colliderType colliderType)
        {
            this.position = position;
            this.size = sizeBox;
            this.colliderType = colliderType;
            this.flameDestroy = 0;
        }
    }
    public enum colliderType
    {
        Box,
        Circle,
    }

}
