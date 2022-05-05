public abstract class Settable<T> : System.IDisposable where T: class
{
    protected UnityEngine.GameObject pool;
    protected System.Collections.Queue queue;
    protected Settable(UnityEngine.GameObject pool){
        queue = new System.Collections.Queue();
        this.pool=pool;
    }
    ~Settable(){
        Dispose();
    }
    abstract protected void Reset(T data);
    abstract protected void DistoryItem(T data);
    abstract protected T Create();
    public virtual T pop()
    {
        if(queue.Count>0)
            return queue.Dequeue() as T;
        return Create();
    }
    public virtual void push(T data){
        Reset(data);
        queue.Enqueue(data);
    }

    public virtual void Dispose()
    {
        while(queue.Count>0)
        {
            DistoryItem(queue.Dequeue() as T);
        }
    }
}
