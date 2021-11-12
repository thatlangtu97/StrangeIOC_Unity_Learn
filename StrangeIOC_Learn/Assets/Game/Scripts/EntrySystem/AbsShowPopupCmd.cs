using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsShowPopupCmd : Command
{
    public override void Execute()
    {
    }
    //Check đã tồn tại chưa. nếu chưa có thì spawn popup
    protected T GetInstance<T>() where T : Component {
        bool isInited = injectionBinder.GetBinding<T>(GetInjectName()) == null || injectionBinder.GetInstance<T>(GetInjectName()) == null;

        if (isInited)
        {
            GameObject tempObject = Instantiate();
            if(injectionBinder.GetBinding<T>(GetInjectName()) != null)
            {
                injectionBinder.Unbind<T>(GetInjectName());
            }
            injectionBinder.Bind<T>()
                .ToValue(tempObject.GetComponent<T>())
                .ToName(GetInjectName());
        }
        return injectionBinder.GetInstance<T>(GetInjectName());

    }
    //Spawn GameObject Prefab
    private GameObject Instantiate()
    {
        GameObject tempObject = PrefabUtils.LoadPrefab(GetAssetPath());
        return GameObject.Instantiate(tempObject) as GameObject;
    }
    //Lấy tên prefab
    protected abstract string GetAssetPath();

    //Lấy tên Inject
    protected virtual string GetInjectName()
    {
        return "";
    }
}
