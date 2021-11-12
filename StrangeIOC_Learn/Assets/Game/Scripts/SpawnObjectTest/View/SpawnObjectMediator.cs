using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnObject
{
    public class SpawnObjectMediator : EventMediator
    {
        [Inject()]
        public SpawnObjectView view { get; set; }
        public override void OnRegister()
        {
            base.OnRegister();
        }
        public override void OnRemove()
        {
            base.OnRemove();
        }

        private void OnDestroy()
        {
            OnRemove();
        }
    }
}
