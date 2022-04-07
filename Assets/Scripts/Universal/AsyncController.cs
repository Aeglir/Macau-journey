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
    public void Pause()
    {
        _isStop = true;
        tokenSource.Cancel();
        _task = null;
        // UnityEngine.Debug.Log("Pause isStop "+_isStop);
        // if(_task==null)
        //     UnityEngine.Debug.Log("Pause task null");
        // else 
        //     UnityEngine.Debug.Log("Pause task not null");
    }
    public void Start()
    {
        if (_isStop)
        {
            _isStop = false;
            _tokenSource = new System.Threading.CancellationTokenSource();
            _task = AsyncAction.Invoke();
            // UnityEngine.Debug.Log("Start isStop " + _isStop);
            // if (_task == null)
            //     UnityEngine.Debug.Log("Start task null");
            // else
            //     UnityEngine.Debug.Log("Start task not null");
        }
    }
}
