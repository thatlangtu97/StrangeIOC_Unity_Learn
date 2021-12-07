using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEventEmitter : MonoBehaviour
{
    public MovementSoundController moveSoundController;
    
    public AttackSoundController atkSoundController;

    public int stopChargeSoundIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
    public void FootStep()
    {
        //Debug.Log("move");
        moveSoundController.PlaySound(AudioName.Move, false);
    }

    public void StopCharge()
    {
        atkSoundController.PlaySoundSkill(stopChargeSoundIndex, 0, 0);
    }

    public void PlaySoundAttack1()
    {
        atkSoundController.PlaySoundCombo1(0, 0);
    }
    
}
