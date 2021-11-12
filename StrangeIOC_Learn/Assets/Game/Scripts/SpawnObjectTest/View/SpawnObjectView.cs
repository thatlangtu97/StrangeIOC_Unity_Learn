using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpawnObject
{
    public class SpawnObjectView : View
    {
        [Inject]
        public ShowLogSignal showLogSignal { get; set; }

        public Button btn;
        protected override void Awake()
        {
            if (btn != null)
                btn.onClick.AddListener(ShowLog);
        }
        void ShowLog()
        {
            showLogSignal.Dispatch();
            
        }
    }
}