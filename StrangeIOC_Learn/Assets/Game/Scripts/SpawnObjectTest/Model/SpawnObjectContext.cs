using strange.extensions.context.impl;

namespace SpawnObject
{
    public class SpawnObjectContext : MVCSContext
    {
        private readonly SpawnObjectContexView view;

        public SpawnObjectContext(SpawnObjectContexView view) : base(view , true)
        {
            this.view = view;
        }
        protected override void mapBindings()
        {
            base.mapBindings();
            injectionBinder.Bind<PopupManager>().ToValue(new PopupManager()).ToSingleton();
            commandBinder.Bind<LoadSpawnObjectPopupSignal>().To<LoadSpawnObjectPopupCmd>();
            commandBinder.Bind<ShowLogSignal>().To<ShowLogCmd>();
            mediationBinder.Bind<SpawnObjectView>().To<SpawnObjectMediator>();
        }
        // Remove Inject nếu k cần đến nữa
        public override void OnRemove()
        {
            base.OnRemove();
        }
        public override void Launch()
        {
            base.Launch();
            injectionBinder.GetInstance<LoadSpawnObjectPopupSignal>().Dispatch();
        }
    }
}
