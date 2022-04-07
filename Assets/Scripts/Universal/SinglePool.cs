public abstract class SinglePool<T1, T2>
{
    protected UnityEngine.GameObject obj;
    protected System.Collections.Generic.Dictionary<T1, T2> pool;
    public int size { get => pool.Count; }
    protected SinglePool(UnityEngine.GameObject obj)
    {
        this.obj = obj;
        pool = new System.Collections.Generic.Dictionary<T1, T2>();
    }
    ~SinglePool()
    {
        foreach (var item in pool)
        {
            DistroyItem(item.Value);
            pool.Remove(item.Key);
        }
    }

    protected virtual void DistroyItem(T2 item) { }

    public virtual T2 get(T1 key)
    {
        if (pool.ContainsKey(key))
        {
            T2 item = pool[key];
            pool.Remove(key);
            return item;
        }
        return CreateItem(key);
    }
    public virtual void push(T1 key, T2 value)
    {
        if (!pool.ContainsKey(key))
            pool.Add(key, value);
        else pool[key] = value;
    }
    protected virtual T2 CreateItem(T1 key) => default(T2);
}
