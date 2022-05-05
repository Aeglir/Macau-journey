public abstract class AsyncController
{
    public delegate System.Threading.Tasks.Task asyncOperation();
    protected asyncOperation AsyncAction;
    protected System.Threading.Tasks.Task _task;
    protected System.Threading.CancellationTokenSource _tokenSource;
    protected bool _isStop;
    public bool isStop { get => _isStop; }
    public System.Threading.CancellationTokenSource tokenSource { get => _tokenSource; }
    public AsyncController()
    {
        _isStop = true;
        _tokenSource = new System.Threading.CancellationTokenSource();
    }
    ~AsyncController()
    {
        Pause();
    }
    public System.Threading.Tasks.TaskStatus GetTaskStatus() => _task.Status;
    public virtual void Pause()
    {
        if(!isStop)
            _isStop = true;
        if(_tokenSource!=null)
            _tokenSource.Cancel();
        _tokenSource=null;
        _task = null;
    }
    public virtual void Start()
    {
        if (AsyncAction!=null&&_isStop)
        {
            _isStop = false;
            _tokenSource = new System.Threading.CancellationTokenSource();
            _task = AsyncAction.Invoke();
        }
    }
}
