public abstract class DicPool<T0, T1>
{
    protected UnityEngine.GameObject obj;
    protected System.Collections.Generic.Dictionary<T0, System.Collections.Generic.Queue<T1>> pool;
    protected UnityEngine.GameObject perfab;
    public DicPool(UnityEngine.GameObject obj)
    {
        this.obj = obj;
        pool = new System.Collections.Generic.Dictionary<T0, System.Collections.Generic.Queue<T1>>();
    }
    public int Count { get => pool.Count; }
    ~DicPool()
    {
        foreach (var pair in pool)
        {
            while(pair.Value.Count>0)
            {
                DistroyItem(pair.Value.Dequeue());
            }
            pool.Remove(pair.Key);
        }
    }
    protected virtual void DistroyItem(T1 item) { }
    public virtual T1 get(T0 tag)
    {
        if (pool.ContainsKey(tag))
        {
            var person = pool[tag].Dequeue();
            return person;
        }
        return CreateItem(tag);
    }
    protected virtual T1 CreateItem(T0 tag) => default(T1);
    public virtual void push(T0 tag, T1 item)
    {
        if (!pool.ContainsKey(tag))
        {
            pool.Add(tag, new System.Collections.Generic.Queue<T1>());
        }
        pool[tag].Enqueue(item);
    }
}
