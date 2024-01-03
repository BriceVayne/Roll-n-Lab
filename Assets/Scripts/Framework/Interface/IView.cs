namespace Framework
{
    public interface IView<TController, TModel>       
    {
        public TController Controller { get; }

        public void Initialize();
        public void Connect(TController _Controller);
        public void Bind(TModel _Model);
        public void UnbindAndDisconnect(TController _Controller, TModel _Model);
    }
}