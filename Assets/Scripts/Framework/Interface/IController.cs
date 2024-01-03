namespace Framework
{
    public interface IController<TModel>
        where TModel : Model
    {
        public TModel Model { get; }
    }
}
