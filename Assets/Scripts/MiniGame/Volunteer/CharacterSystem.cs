using UnityEngine;
using DG.Tweening;
namespace MiniGame.Volunteer
{
    /// <summary>
    /// 义工游戏主场景管理器
    /// </summary>
    [DefaultExecutionOrder(-1)]
    public class CharacterSystem : Singleton<CharacterSystem, CharacterSystem>
    {
        /// <summary>
        /// 场上NPC数据库
        /// </summary>
        public class CharacterDataBase
        {
            private System.Collections.Generic.Dictionary<int, System.Collections.Generic.Queue<PersonFactory.Talkable>> roadPersonDic;
            private System.Collections.Generic.Queue<PersonFactory.Talkable> backgroundPersonQueue;
            private int count;//当前Road中NPC数量
            public int RoadPersonCount { get => count; }
            public System.Collections.Generic.Dictionary<int, System.Collections.Generic.Queue<PersonFactory.Talkable>> RoadPersonDic { get => roadPersonDic; }
            public System.Collections.Generic.Queue<PersonFactory.Talkable> BackgroundPersonQueue { get => backgroundPersonQueue; }
            ~CharacterDataBase()
            {
                foreach (var pair in roadPersonDic)
                {
                    while (pair.Value.Count > 0)
                    {
                        var item = pair.Value.Dequeue();
                        DestroyImmediate(item.getGameObject());
                    }
                    roadPersonDic.Remove(pair.Key);
                }
                while (backgroundPersonQueue.Count > 0)
                {
                    var item = backgroundPersonQueue.Dequeue();
                    DestroyImmediate(item.getGameObject());
                }
            }
            public CharacterDataBase()
            {
                roadPersonDic = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.Queue<PersonFactory.Talkable>>();
                backgroundPersonQueue = new System.Collections.Generic.Queue<PersonFactory.Talkable>();
            }
            public void add(PersonFactory.Talkable person)
            {
                int tag = person.getTag();
                if (!roadPersonDic.ContainsKey(tag))
                {
                    roadPersonDic.Add(tag, new System.Collections.Generic.Queue<PersonFactory.Talkable>());
                }
                roadPersonDic[tag].Enqueue(person);
                count++;
            }
            public void addBack(PersonFactory.Talkable person)
            {
                backgroundPersonQueue.Enqueue(person);
            }
            public PersonFactory.Talkable get(int tag)
            {
                if (roadPersonDic.ContainsKey(tag))
                {
                    if (roadPersonDic[tag].Count > 0)
                    {
                        count--;
                        return roadPersonDic[tag].Dequeue();
                    }
                }
                return null;
            }
            public PersonFactory.Talkable getBack()
            {
                if (backgroundPersonQueue.Count > 0)
                {
                    return backgroundPersonQueue.Dequeue();
                }
                return null;
            }
            /// <summary>
            /// 检查场上符合条件的NPC
            /// </summary>
            /// <param name="isRight">主角朝向</param>
            /// <returns>NPCtag</returns>
            public int Check(bool isRight)
            {
                int left;
                int right;
                if (isRight)
                {
                    left = 0;
                    right = GobalSetting.CheckRightRange;
                }
                else
                {
                    left = GobalSetting.CheckLeftRange;
                    right = 0;
                }
                foreach (var pair in RoadPersonDic)
                {
                    foreach (var item in pair.Value)
                    {
                        RectTransform rectTransform = (item.getGameObject().transform as RectTransform);
                        if (rectTransform.anchoredPosition.x < right && rectTransform.anchoredPosition.x > left && !item.hasCalled())
                        {
                            item.Called();
                            return item.getTag();
                        }
                    }
                }
                return -1;
            }
        }
        /// <summary>
        /// NPC轨道发射器
        /// </summary>
        public class CharacterEmitter
        {
            public enum Road
            {
                Busy,
                Behind,
                Middle,
                Front
            }
            private GameObject road;
            private GameObject backGround;
            private readonly Vector3 scale = new Vector3(100, 100, 1);//NPC默认缩放倍率
            private System.Collections.Generic.Dictionary<Road, bool> roadDic;//轨道列表
            private bool _back;
            public bool backState { get => _back; }//背景轨道状态
            public CharacterEmitter(GameObject road, GameObject backGround)
            {
                this.road = road;
                this.backGround = backGround;
                roadDic = new System.Collections.Generic.Dictionary<Road, bool>();
                roadDic.Add(Road.Behind, false);
                roadDic.Add(Road.Middle, false);
                roadDic.Add(Road.Front, false);
            }
            public void AddRoad(PersonFactory.Talkable person, Road walkRoad, bool isRight = true, TweenCallback action = null)
            {
                person.SetRoad(walkRoad);
                GameObject obj = person.getGameObject();
                obj.transform.SetParent(road.transform);
                RectTransform rect = obj.transform as RectTransform;
                rect.localScale = scale;
                rect.anchoredPosition3D = GetPos(walkRoad, isRight);
                setAnimation(person, GetPos(walkRoad, isRight, true), isRight, action);
            }
            public void LockRoad(Road road) => roadDic[road] = true;
            public void UnLockRoad(Road road) => roadDic[road] = false;
            public void LockBack() => _back = true;
            public void UnLockBack() => _back = false;
            public bool checkRoad(Road road) => roadDic[road];
            //获取空闲轨道
            public Road GetFreeRoad()
            {
                foreach (var road in roadDic)
                {
                    if (!road.Value)
                        return road.Key;
                }
                return Road.Busy;
            }
            public void AddBackground(PersonFactory.Talkable person, bool isRight = true, TweenCallback action = null)
            {
                GameObject obj = person.getGameObject();
                obj.transform.SetParent(backGround.transform);
                RectTransform rect = obj.transform as RectTransform;
                rect.localScale = scale;
                rect.anchoredPosition3D = GetBackPos(Road.Front, isRight);
                setAnimation(person, GetBackPos(Road.Front, isRight, true), isRight, action);
            }
            private Vector3 GetBackPos(Road type, bool isRight, bool isTarget = false)
            {
                Vector3 pos = Vector3.zero;
                if (isRight)
                {
                    pos.x = -GobalSetting.ScreenWidth / 2;
                }
                else
                {
                    pos.x = GobalSetting.ScreenWidth / 2;
                }
                if (isTarget)
                    pos.x = -pos.x;
                return pos;
            }
            private Vector3 GetPos(Road type, bool isRight, bool isTarget = false)
            {
                Vector3 pos = Vector3.zero;
                if (isRight)
                {
                    pos.x = -GobalSetting.ScreenWidth / 2;
                }
                else
                {
                    pos.x = GobalSetting.ScreenWidth / 2;
                }
                if (isTarget)
                    pos.x = -pos.x;
                switch (type)
                {
                    case Road.Behind:
                        pos.y = GobalSetting.RoadBehindYoffset;
                        pos.z = GobalSetting.RoadBehindZoffset;
                        break;
                    case Road.Middle:
                        pos.y = GobalSetting.RoadMiddleYoffset;
                        pos.z = GobalSetting.RoadMiddleZoffset;
                        break;
                    case Road.Front:
                        pos.y = GobalSetting.RoadFrontYoffset;
                        pos.z = GobalSetting.RoadFrontZoffset;
                        break;
                }
                return pos;
            }
            public void setAnimation(PersonFactory.Talkable person, Vector3 destination, bool isRight = true, TweenCallback action = null)
            {
                person.initWalkingAnimation(destination, isRight, action);
                person.Walk();
            }
        }
        /// <summary>
        /// NPC对象池
        /// </summary>
        public class CharacterPool : SinglePool<int, PersonFactory.Talkable>
        {
            public CharacterPool(GameObject obj) : base(obj) { }
            public PersonFactory.Playable GetPlayer(GameObject obj) => PersonFactory.Person.GetPlayerInstance(obj) as PersonFactory.Playable;
            public override PersonFactory.Talkable get(int tag)
            {
                if (pool.ContainsKey(tag))
                {
                    if (pool[tag].isUsing())
                        return null;
                    var person = pool[tag];
                    person.Lock();
                    person.getGameObject().SetActive(true);
                    return person;
                }
                return PersonFactory.Person.GetInstance(tag) as PersonFactory.Talkable;
            }
            public override void push(int tag, PersonFactory.Talkable item)
            {
                GameObject itemObj = item.getGameObject();
                itemObj.SetActive(false);
                itemObj.transform.SetParent(obj.transform);
                item.unLock();
            }
            //子对象销毁函数
            protected override void DistroyItem(PersonFactory.Talkable item)
            {
                GameObject.DestroyImmediate(item.getGameObject());
            }
            //子对象创建函数
            protected override PersonFactory.Talkable CreateItem(int tag)
            {
                return PersonFactory.Person.GetInstance(tag) as PersonFactory.Talkable;
            }
        }
        /// <summary>
        /// 轨道NPC生成器
        /// </summary>
        class RoadPersonAppearController : AsyncController
        {
            private System.Action action;//具体生成委托
            public int currentPerson;//当前NPC数量
            public RoadPersonAppearController(System.Action action) : base()
            {
                this.action = action;
                AsyncAction = personAppear;
            }
            private async System.Threading.Tasks.Task personAppear()
            {
                // int tag=1;
                // CharacterEmitter.Road road = (CharacterEmitter.Road)Random.Range(0, 3);
                // while(tag<21)
                while (!_isStop && currentPerson < GobalSetting.MaxPerson)
                {
                    // AddPerson(tag, road, road, true, FinishHandle);
                    // Debug.Log("pass");
                    // tag++;
                    //随机等待
                    await System.Threading.Tasks.Task.Delay((int)((int)(1000 * Random.Range(GobalSetting.MinDelay, GobalSetting.MaxDelay)) / Time.timeScale), _tokenSource.Token);
                    action.Invoke();
                    currentPerson++;
                    // Debug.Log(_task.Status);
                    // await System.Threading.Tasks.Task.Delay((int)(1000 * 5));
                }
                _isStop = true;
                _task = null;
            }
            //NPC结束回调
            public void FinishHandle()
            {
                currentPerson--;
                Start();
            }
        }
        /// <summary>
        /// 背景轨道NPC生成器
        /// </summary>
        class BackRoadPersonAppaerController : AsyncController
        {
            private System.Action action;//具体委托
            public int currentPerson;//当前NPC数量
            public BackRoadPersonAppaerController(System.Action action) : base()
            {
                this.action = action;
                AsyncAction = personAppear;
            }
            private async System.Threading.Tasks.Task personAppear()
            {
                // int tag=1;
                // CharacterEmitter.Road road = (CharacterEmitter.Road)Random.Range(0, 3);
                // while(tag<21)
                while (!_isStop && currentPerson < GobalSetting.maxAlphaPerson)
                {
                    // AddPerson(tag, road, road, true, FinishHandle);
                    // Debug.Log("pass");
                    // tag++;
                    // Debug.Log("start async");
                    //随机等待
                    await System.Threading.Tasks.Task.Delay((int)((int)(1000 * Random.Range(GobalSetting.MinDelay, GobalSetting.MaxDelay)) / Time.timeScale), _tokenSource.Token);
                    // Debug.Log("finish async");
                    action.Invoke();
                    currentPerson++;
                    // Debug.Log(_task.Status);
                    // await System.Threading.Tasks.Task.Delay((int)(1000 * 5));
                }
                _isStop = true;
                _task = null;
            }
            //NPC结束回调
            public void FinishHandle()
            {
                currentPerson--;
                Start();
            }
        }
        /// <summary>
        /// 主场景输入控制器
        /// </summary>
        class CharacterInputManager
        {
            private Managers.InputManager.InputEvent PauseInput;
            private Managers.InputManager.InputEvent TipsInput;
            private Managers.InputManager.InputEvent AccelerateInput;
            public CharacterInputManager(System.Action pauseAction, System.Action accelerateStartAction, System.Action accelerateCancelAction)
            {
                PauseInput = Managers.GameManager.Instance.InputManager.AddInput(GobalSetting.PauseKey, UnityEngine.InputSystem.InputActionType.Button, "Pause");
                // TipsInput = Managers.GameManager.Instance.InputManager.AddInput(GobalSetting.PauseKey, UnityEngine.InputSystem.InputActionType.Button, "Pause");
                AccelerateInput = Managers.GameManager.Instance.InputManager.AddInput(GobalSetting.AccelerateKey, UnityEngine.InputSystem.InputActionType.Button, "Accelerate");
                PauseInput.AddListener((c) =>
                {
                    pauseAction.Invoke();
                }, Managers.InputManager.Response.Canceled);
                AccelerateInput.AddListener((c) =>
                {
                    accelerateStartAction.Invoke();
                }, Managers.InputManager.Response.Started);
                AccelerateInput.AddListener((c) =>
                {
                    accelerateCancelAction.Invoke();
                }, Managers.InputManager.Response.Canceled);
            }
            public void Dispose()
            {
                if (PauseInput != null)
                {
                    PauseInput.Disable();
                    PauseInput.Dispose();
                }
                if (TipsInput != null)
                {
                    TipsInput.Disable();
                    TipsInput.Dispose();
                }
                if (AccelerateInput != null)
                {
                    AccelerateInput.Disable();
                    AccelerateInput.Dispose();
                }
            }
            public void Enable()
            {
                if (PauseInput != null)
                    PauseInput.Enable();
                if (AccelerateInput != null)
                    AccelerateInput.Enable();
            }
            public void Disable()
            {
                if (PauseInput != null)
                    PauseInput.Disable();
                if (AccelerateInput != null)
                    AccelerateInput.Disable();
            }
        }
        [Header("NPC配置文件")]
        public CharactersAsset asset;
        [Header("玩家GameObject")]
        public GameObject Player;
        [Header("Npc预制体")]
        public GameObject perfab;
        [Header("轨道GameObject")]
        public GameObject road;
        [Header("背景轨道GameObject")]
        public GameObject backGround;
        public UnityEngine.Events.UnityEvent pauseEvent;
        public UnityEngine.Events.UnityEvent continueEvent;
        public UnityEngine.Events.UnityEvent callEvent;
        [Header("具体游戏场景")]
        public GameObject JFCanvas;
        [Header("失败幕布")]
        public GameObject Failtrue;
        private bool isAccelerate;//加速标志
        private CharacterPool pool;
        private PersonFactory.Playable playable;
        private CharacterDataBase dataBase;
        private CharacterEmitter emitter;
        private RoadPersonAppearController roadController;
        private BackRoadPersonAppaerController backRoadController;
        private CharacterInputManager inputManager;
        private System.Collections.Generic.List<int> tagFreeList;//空闲tag列表
        private System.Collections.Generic.List<int> tagUsingList;//使用中tag列表
        private int randomStart;//起始随机数
        private int _tag;//被呼喊NPCtag
        private int _round;//回合数
        public int round { get => _round; set => _round = value; }
        public int Tag
        {
            get
            {
                int t = _tag;
                _tag = GobalSetting.InvaildTag;
                return t;
            }
        }
        private void Awake()
        {
            //设置单例
            if (_instance == null)
                _instance = this;
            //检查全局输入管理器
            if (Managers.GameManager.Instance != null)
            {
                inputManager = new CharacterInputManager(PauseInvoke, AccelerateStart, AccelerateCancel);
                inputManager.Enable();
            }
            //初始化 空闲tag列表和使用中tag列表
            tagFreeList = new System.Collections.Generic.List<int>(GobalSetting.MaxTag - GobalSetting.MinTag);
            tagUsingList = new System.Collections.Generic.List<int>(GobalSetting.MaxTag - GobalSetting.MinTag);
            for (int i = GobalSetting.MinTag; i < GobalSetting.MaxTag; i++)
                tagFreeList.Add(i);
            //初始化初始随机数
            randomStart = Random.Range(GobalSetting.MinTag, GobalSetting.MaxTag);
            //初始化对象池、数据库、发射器
            pool = new CharacterPool(gameObject);
            dataBase = new CharacterDataBase();
            emitter = new CharacterEmitter(road, backGround);
            //获取并初始化玩家控制器
            playable = pool.GetPlayer(Player);
            playable.SetCallAction((t) =>
            {
                TryToCall(t);
            });
            playable.Enable();
            // AddPerson(Random.Range(minTag, maxTag), CharacterEmitter.Road.Front, CharacterEmitter.Road.Front, true, FinishHandle);
            //初始化轨道发射器和背景轨道发射器并启动
            roadController = new RoadPersonAppearController(RandomRoadAppear);
            backRoadController = new BackRoadPersonAppaerController(RandomBackRoadAppear);
            backRoadController.Start();
            roadController.Start();
            // Time.timeScale = 5f;
        }
        private void OnDestroy()
        {
            if (inputManager != null)
                inputManager.Dispose();
            playable.Disable();
            roadController.Pause();
            backRoadController.Pause();
            _instance = null;
        }
        public void CallInvoke()
        {
            TryToCall(playable.isRight());
        }
        private void TryToCall(bool isRight)
        {
            _tag = dataBase.Check(isRight);//查找有效NPCtag
            if (_tag != GobalSetting.InvaildTag)
            {
                Pause();
                Debug.Log("tag " + _tag);
                _round++;
                JFCanvas.SetActive(true);//开启并进入具体游戏场景
                if (callEvent != null)
                    callEvent.Invoke();
            }
        }
        public bool AddPerson(int tag, CharacterEmitter.Road road, bool isRight = true, System.Action<int, bool> action = null)
        {
            //尝试获取目标tagNPC
            PersonFactory.Talkable person = pool.get(tag);
            if (person == null)
                return false;
            dataBase.add(person);//添加到数据库中
            //发射到目标轨道
            emitter.AddRoad(person, road, isRight, () =>
            {
                action.Invoke(person.getTag(), person.GetisHide());
            });
            return true;
        }
        public bool AddBackPerson(int tag, bool isRight = true, System.Action<int, bool> action = null)
        {
            PersonFactory.Talkable person = pool.get(tag);
            if (person == null)
                return false;
            //设置为隐藏模式
            person.setHide(true);
            dataBase.addBack(person);
            emitter.AddBackground(person, isRight, () =>
            {
                action.Invoke(person.getTag(), person.GetisHide());
            });
            return true;
        }
        /// <summary>
        /// 背景轨道随机NPC生成函数
        /// </summary>
        public void RandomBackRoadAppear()
        {
            //检查是否有空闲tag
            if (tagFreeList.Count == 0) return;
            //检查轨道状态
            if (emitter.backState) return;
            //轨道加锁
            emitter.LockBack();
            //尝试获取有效tag
            int t = randomStart;
            while (!tagFreeList.Contains(randomStart))
            {
                t = (t + 1) % (GobalSetting.MaxTag - GobalSetting.MinTag) + GobalSetting.MinTag;
            }
            //尝试添加到目标轨道
            while (!AddBackPerson(t, Random.Range(0, 2) == 1, FinishHandle))
            {
                t = (t + 1) % (GobalSetting.MaxTag - GobalSetting.MinTag) + GobalSetting.MinTag;
            }
            //从空闲tag列表中移除tag并添加到使用中列表
            tagFreeList.Remove(t);
            tagUsingList.Add(t);
            //更新初始随机数
            randomStart = (t + 1) % (GobalSetting.MaxTag - GobalSetting.MinTag) + GobalSetting.MinTag;
            // Debug.Log("RandomBackRoadAppear"+ShowList(tagFreeList));
            // Debug.Log("RandomBackRoadAppear"+ShowList(tagUsingList));
        }
        /// <summary>
        /// 轨道NPC随机生成函数
        /// </summary>
        public void RandomRoadAppear()
        {
            // CharacterEmitter.Road road = CharacterEmitter.Road.Behind;
            // AddPerson(Random.Range(minTag, maxTag), road, road, true, FinishHandle);
            if (tagFreeList.Count == 0) return;
            CharacterEmitter.Road road = emitter.GetFreeRoad();
            if (road == CharacterEmitter.Road.Busy) return;
            emitter.LockRoad(road);
            int t = randomStart;
            while (!tagFreeList.Contains(randomStart))
            {
                t = (t + 1) % (GobalSetting.MaxTag - GobalSetting.MinTag) + GobalSetting.MinTag;
            }
            while (!AddPerson(t, road, Random.Range(0, 2) == 1, FinishHandle))
            {
                t = (t + 1) % (GobalSetting.MaxTag - GobalSetting.MinTag) + GobalSetting.MinTag;
            }
            tagFreeList.Remove(t);
            tagUsingList.Add(t);
            randomStart = (t + 1) % (GobalSetting.MaxTag - GobalSetting.MinTag) + GobalSetting.MinTag;
            // Debug.Log("RandomBackRoadAppear"+ShowList(tagFreeList));
            // Debug.Log("RandomBackRoadAppear"+ShowList(tagUsingList));
        }
        /// <summary>
        /// NPC结束回调
        /// </summary>
        /// <param name="tag">NPCtag</param>
        /// <param name="ishide">是否设置为隐藏</param>
        private void FinishHandle(int tag, bool ishide)
        {
            PersonFactory.Talkable person;
            if (ishide)
            {
                person = dataBase.getBack();
                //背景轨道解锁
                emitter.UnLockBack();
                backRoadController.FinishHandle();
            }
            else
            {
                person = dataBase.get(tag);
                //解锁NPC所在轨道
                emitter.UnLockRoad(person.GetRoad());
                roadController.FinishHandle();
            }
            //初始化NPC
            person.Finish();
            //压入对象池
            pool.push(tag, person);
            tagUsingList.Remove(tag);
            tagFreeList.Add(tag);
            // Debug.Log("RandomBackRoadAppear"+ShowList(tagFreeList));
            // Debug.Log("RandomBackRoadAppear"+ShowList(tagUsingList));
        }
        public void Pause()
        {
            //关闭玩家控制器和主场景输入控制器
            playable.Disable();
            if (inputManager != null)
                inputManager.Disable();
            // Debug.Log("pause");
            //暂停所有NPC生成
            roadController.Pause();
            backRoadController.Pause();
            //还原加速状态
            if (isAccelerate)
            {
                AccelerateCancel();
            }
            //暂停所有NPC动画
            foreach (var pair in dataBase.RoadPersonDic)
            {
                foreach (var item in pair.Value)
                {
                    item.Pause();
                }
            }
            foreach (var item in dataBase.BackgroundPersonQueue)
            {
                item.Pause();
            }
        }
        public void PauseInvoke() => pauseEvent.Invoke();
        public void Continue()
        {
            //结束加速状态
            if (isAccelerate)
            {
                AccelerateCancel();
            }
            // pauseCanvas.SetActive(false);
            //启动所有NPC动画
            foreach (var pair in dataBase.RoadPersonDic)
            {
                foreach (var item in pair.Value)
                {
                    item.Walk();
                }
            }
            foreach (var item in dataBase.BackgroundPersonQueue)
            {
                item.Walk();
            }
            // Debug.Log("continue");
            //启动NPC生成
            roadController.Start();
            backRoadController.Start();
            //启动玩家控制器和主场景输入控制器
            playable.Enable();
            if (inputManager != null)
                inputManager.Enable();
            // if (appearTask.Status == System.Threading.Tasks.TaskStatus.WaitingToRun)
            // {
            //     appearTask.Start();
            // }
            // else if (appearTask.IsCompleted)
            // {
            //     appearTask.Start();
            // }
        }
        private void AccelerateStart()
        {
            Time.timeScale = GobalSetting.AccelerateTimeMulity;
            isAccelerate = true;
        }
        private void AccelerateCancel()
        {
            Time.timeScale = GobalSetting.SourceTimeMulity;
            isAccelerate = false;
        }
        public void ContinueInvoke() => continueEvent.Invoke();
        public void Quit() => UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        /// <summary>
        /// 失败界面启动函数
        /// </summary>
        public void FailTrue()
        {
            RectTransform rect = Failtrue.transform as RectTransform;
            Failtrue.SetActive(true);
            var tweener = rect.DOAnchorPosY(0, GobalSetting.FailtrueAppearDuration).SetEase(Ease.InQuad).OnComplete(() =>
            {
                Failtrue.SetActive(false);
            }).Pause();
            rect.DOAnchorPosY(GobalSetting.FailtrueYoffset, GobalSetting.FailtrueDisappearDuration).OnComplete( async () =>
            {
                await System.Threading.Tasks.Task.Delay(GobalSetting.FailtrueDelayMillions);
                tweener.Play();
            });
        }
        public void AccelerateHandleClick()
        {
            if (isAccelerate)
            {
                Time.timeScale = GobalSetting.AccelerateTimeMulity;
                isAccelerate = false;
            }
            else
            {
                Time.timeScale = GobalSetting.SourceTimeMulity;
                isAccelerate = true;
            }
        }
    }
}
