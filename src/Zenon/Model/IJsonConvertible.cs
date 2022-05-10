namespace Zenon.Model
{
    public interface IJsonConvertible<T>
    {
        T ToJson();
    }
}
