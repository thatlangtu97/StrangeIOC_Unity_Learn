using strange.extensions.command.impl;
using UnityEngine;
namespace SpawnObject
{
    public class ShowLogCmd : Command
    {
        [Inject] public PopupManager popupManager { get; set; }
        public override void Execute()
        {
            Debug.Log("show signal "+this.GetType().Name);
            Debug.Log(DataManager.Instance.CurrencyDataManager.gem);
            popupManager.Start();
        }
    }
}
