using EntrySystem;
using strange.extensions.context.impl;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayFlashScene : View
{
    public string nameScene;
    public float timeDelay;
    float currentTime = 0;
    protected override void Start()
    {
        base.Start();
    }
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime>= timeDelay)
        {
            SceneManager.LoadScene(nameScene);
            Destroy(gameObject);
        }
    }
}
