using Entitas;
using BehaviorDesigner.Runtime;
using UnityEngine;
//using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    Systems CharacterSystems;
    Systems GameSystem;
    public static GameController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Application.targetFrameRate = 70;
        var contexts = Contexts.sharedInstance;
        GameSystem = new Feature("Game System")
            .Add(new StateMachineUpdateSystem(contexts))
            .Add(new TakeDamageSystem(contexts))
            .Add(new ProjectileMoveBezierSystem(contexts))
            .Add(new DamageTextSystem(contexts))
            ;
        GameSystem.Initialize();
        //DontDestroyOnLoad(this);
    }
    void Start()
    {
        if (PlayFlashScene.instance != null)
        {
            PlayFlashScene.instance.HideLoading();
        }
    }
    private void OnDestroy()
    {
        //ExitGameScene();
    }
    void Update()
    {
        GameSystem.Execute();              
    }
    private void LateUpdate()
    {
        GameSystem.Cleanup();
    }
    public void ReloadScene()
    {
        /*
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        var contexts = Contexts.sharedInstance;
        contexts.game.DestroyAllEntities();
        contexts.Reset();       
        Contexts.sharedInstance = new Contexts();
        */

        StartCoroutine(delayReloadScene());
    }
    public void ExitGameScene()
    {
        /*
        GameSystem.TearDown();
        GameSystem.DeactivateReactiveSystems();
        GameSystem.ClearReactiveSystems();
        var contexts = Contexts.sharedInstance;
        contexts.game.DestroyAllEntities();        
        contexts.Reset();
        */
        ComponentManagerUtils.ResetAll();
        //Contexts.sharedInstance.Reset();
        Contexts.sharedInstance = new Contexts();
    }
    public void BackToHome()
    {
        PlayFlashScene.instance.Loading("HomeScene", 1.2f, ExitGameScene);
    }
    IEnumerator delayReloadScene()
    {
        if(PlayFlashScene.instance!=null)
            PlayFlashScene.instance.ShowLoading();
        ComponentManagerUtils.ResetAll();
        //Contexts.sharedInstance.Reset();
        Contexts.sharedInstance = new Contexts();
        yield return new WaitForSeconds(1.2f);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

        

    }
}

