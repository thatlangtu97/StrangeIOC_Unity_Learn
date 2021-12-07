using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCameraOnEvent : MonoBehaviour
{
    public CharacterDirection direction;
    public int shakeLevel;
    public float duration;
    public float durationShakeLevel1;
    public float durationShakeLevel2;
    public float durationShakeLevel3;
    public GameObject fx;
    public Vector3 offset;
    public Transform groundCastPoint;
    public int skillsoundindex;
    public AttackSoundController atksoundcontroller;
    public void Shake()
    {
        CameraFollow.instance.Shake(shakeLevel, duration);
    }

    public void GroundShake()
    {
        CameraFollow.instance.Shake(shakeLevel, duration);
        if (direction.isFaceRight)
            offset = offset * -1;
        ObjectPool.Spawn(fx, groundCastPoint.transform.position + offset, fx.transform.rotation);
        if (atksoundcontroller != null && skillsoundindex >= 0)
        {
            atksoundcontroller.PlaySoundSkill(skillsoundindex, 0,0);
        }
    }
    /*
    public void Shake1()
    {
        CameraFollow.instance.Shake(1, durationShakeLevel1);
    }

    public void Shake2()
    {
        CameraFollow.instance.Shake(2, durationShakeLevel2);
    }

    public void Shake3()
    {
        CameraFollow.instance.Shake(3, durationShakeLevel3);
    }
    */
}
