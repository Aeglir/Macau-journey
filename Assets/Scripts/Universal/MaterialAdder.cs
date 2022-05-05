
public interface MaterialAdder<T,M> where T : System.Enum
{
    void addMaterial(T type,M material);
}