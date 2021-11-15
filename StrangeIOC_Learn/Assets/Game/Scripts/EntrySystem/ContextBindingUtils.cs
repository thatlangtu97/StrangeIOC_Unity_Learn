using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextBindingUtils 
{
    //public static MVCSContext context;
    //public static List<Component> listTypeBinding = new List<Component>();
    //public static void SetMVCSContext(MVCSContext mvcContext)
    //{
    //    context = mvcContext;
    //}
    //public static T GetInstance<T>(GameObject o) where T : Component
    //{

    //    if (context == null) return null;
    //    if (listTypeBinding.Contains(o.GetComponent<T>()))
    //    {
    //        return listTypeBinding[]
    //    }
    //    bool isInit = context.injectionBinder.GetBinding<T>(GetInjectName()) == null ||
    //                  context.injectionBinder.GetInstance<T>(GetInjectName()) == null;

    //    if (isInit)
    //    {
    //        if (context.injectionBinder.GetBinding<T>(GetInjectName()) != null)
    //        {
    //            context.injectionBinder.Unbind<T>(GetInjectName());
    //        }

    //        context.injectionBinder.Bind<T>()
    //            .ToValue(o.GetComponent<T>())
    //            .ToName(GetInjectName());
    //    }

    //    return context.injectionBinder.GetInstance<T>(GetInjectName());
    //}
    //static string GetInjectName()
    //{
    //    return "";
    //}
}
