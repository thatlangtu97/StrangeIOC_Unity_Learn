using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnObject
{
    public class SpawnObjectContexView : ContextView
    {
        private void Start()
        {
            context = new SpawnObjectContext(this);
            context.Start();
        }
    }
}
