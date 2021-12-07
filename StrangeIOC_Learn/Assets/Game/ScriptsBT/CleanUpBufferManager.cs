using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUpBufferManager
{ 
    // Start is called before the first frame update
    public static CleanUpBufferManager instance;


    public CleanUpBufferManager()
    {
        instance = this;

    }
  
    public List<ActionBuffer> startActionList = new List<ActionBuffer>();
    public List<ActionBuffer> cleanUpActionList = new List<ActionBuffer>();
    public void AddReactiveComponent(Action startAction, Action cleanUpAction)
    {
        ActionBuffer actBuffer = new ActionBuffer();
        actBuffer.startAction = startAction;
        actBuffer.cleanUpAction = cleanUpAction;
        startActionList.Add(actBuffer);
        
    }
    

    // Update is called once per frame
    public void UpdateBuffer()
    {
        //foreach(var actClean in cleanUpActionList) // thuc thi trong clean trc
        //{

        //    try
        //    {
        //        actClean.cleanUpAction.Invoke();
        //    }
        //     catch(Exception ex)
        //    {
        //        //Debug.LogError(ex.StackTrace);
        //        continue;
        //    }
            
            
        //}
        for(int i =0; i< cleanUpActionList.Count; i++)
        {
            try
            {
                cleanUpActionList[i].cleanUpAction.Invoke();
            }
            catch (Exception e)
            {
                continue;
            }
        }
        cleanUpActionList.Clear();

        //foreach(var act in startActionList)
        //{
            
        //    try
        //    {
        //        act.startAction.Invoke();
        //        cleanUpActionList.Add(act);
        //    }
        //    catch(Exception ex)
        //    {
        //        //Debug.LogError(ex.ToString());
        //        continue;                
        //    }
            
            
            
        //}
        for (int i = 0; i < startActionList.Count; i++)
        {
            try
            {
                startActionList[i].startAction.Invoke();
                cleanUpActionList.Add(startActionList[i]);
            }
            catch (Exception e)
            {
                continue;
            }
        }
        startActionList.Clear();
    }
    public GameEntity CreateTempEntity()
    {
        return Contexts.sharedInstance.game.CreateEntity();
    }
}
public class ActionBuffer
{
    public Action startAction;
    public Action cleanUpAction;
}
