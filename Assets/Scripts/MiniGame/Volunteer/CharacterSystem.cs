using UnityEngine;
using DG.Tweening;
namespace MiniGame.Volunteer
{
    [DefaultExecutionOrder(-1)]
    public class CharacterSystem : Singleton<CharacterSystem, CharacterSystem>
    {
        public class CharacterDataBase
        {
            private System.Collections.Generic.Dictionary<int, System.Collections.Generic.Queue<PersonFactory.Talkable>> roadPersonDic;
            private System.Collections.Generic.Queue<PersonFactory.Talkable> backgroundPersonQueue;
            private int count;
            private int backGroundCount;
            public int RoadPersonCount { get => count; }
            public int BackRoadPersonCount { get => backGroundCount; }
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
                backGroundCount++;
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
                    backGroundCount--;
                    return backgroundPersonQueue.Dequeue();
                }
                return null;
            }
            public void RemoveAll(int tag)
            {
                if (roadPersonDic.ContainsKey(tag))
                {
                    count -= roadPersonDic[tag].Count;
                    var queue = roadPersonDic[tag];
                    roadPersonDic.Remove(tag);
                    count -= queue.Count;
                }
            }
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
            private readonly Vector3 scale = new Vector3(100, 100, 1);
            private System.Collections.Generic.Dictionary<Road, bool> roadDic;
            private bool _back;
            public bool backState { get => _back; }
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
            public void AddCenter(PersonFactory.Talkable person)
            {
                GameObject obj = person.getGameObject();
                obj.transform.SetParent(road.transform);
                RectTransform rect = obj.transform as RectTransform;
                rect.localScale = scale;
                rect.anchoredPosition3D = Vector3.zero;
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
            public void AddBackground(GameObject obj)
            {
                obj.transform.SetParent(backGround.transform);
            }
        }
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
            protected override void DistroyItem(PersonFactory.Talkable item)
            {
                GameObject.DestroyImmediate(item.getGameObject());
            }
            protected override PersonFactory.Talkable CreateItem(int tag)
            {
                return PersonFactory.Person.GetInstance(tag) as PersonFactory.Talkable;
            }
        }
        class RoadPersonAppearController : AsyncController
        {
            private System.Action action;
            public int currentPerson;
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
                while (!isStop && currentPerson < GobalSetting.MaxPerson)
                {
                    // AddPerson(tag, road, road, true, FinishHandle);
                    // Debug.Log("pass");
                    // tag++;
                    await System.Threading.Tasks.Task.Delay((int)((int)(1000 * Random.Range(GobalSetting.MinDelay, GobalSetting.MaxDelay)) / Time.timeScale), _tokenSource.Token);
                    action.Invoke();
                    currentPerson++;
                    // Debug.Log(_task.Status);
                    // await System.Threading.Tasks.Task.Delay((int)(1000 * 5));
                }
                _isStop = true;
                _task = null;
            }
            public void FinishHandle()
            {
                currentPerson--;
                Start();
            }
        }
        class BackRoadPersonAppaerController : AsyncController
        {
            private System.Action action;
            public int currentPerson;
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
                while (!isStop && currentPerson < GobalSetting.maxAlphaPerson)
                {
                    // AddPerson(tag, road, road, true, FinishHandle);
                    // Debug.Log("pass");
                    // tag++;
                    await System.Threading.Tasks.Task.Delay((int)((int)(1000 * Random.Range(GobalSetting.MinDelay, GobalSetting.MaxDelay)) / Time.timeScale), _tokenSource.Token);
                    action.Invoke();
                    currentPerson++;
                    // Debug.Log(_task.Status);
                    // await System.Threading.Tasks.Task.Delay((int)(1000 * 5));
                }
                _isStop = true;
                _task = null;
            }
            public void FinishHandle()
            {
                currentPerson--;
                Start();
            }
        }
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
        public CharactersAsset asset;
        public GameObject Player;
        public GameObject perfab;
        public GameObject road;
        public GameObject backGround;
        public UnityEngine.Events.UnityEvent pauseEvent;
        public UnityEngine.Events.UnityEvent continueEvent;
        public UnityEngine.Events.UnityEvent callEvent;
        public GameObject JFCanvas;
        private bool isAccelerate;
        private CharacterPool pool;
        private PersonFactory.Playable playable;
        private CharacterDataBase dataBase;
        private CharacterEmitter emitter;
        private RoadPersonAppearController roadController;
        private BackRoadPersonAppaerController backRoadController;
        private CharacterInputManager inputManager;
        private System.Collections.Generic.List<int> tagFreeList;
        private System.Collections.Generic.List<int> tagUsingList;
        private int randomStart;
        private int _tag;
        public int Tag
        {
            get
            {
                int t = _tag;
                _tag = -1;
                return t;
            }
        }
        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            if (Managers.GameManager.Instance != null)
            {
                inputManager = new CharacterInputManager(PauseInvoke, AccelerateStart, AccelerateCancel);
                inputManager.Enable();
            }
            tagFreeList = new System.Collections.Generic.List<int>(GobalSetting.MaxTag - GobalSetting.MinTag);
            tagUsingList = new System.Collections.Generic.List<int>(GobalSetting.MaxTag - GobalSetting.MinTag);
            for (int i = GobalSetting.MinTag; i < GobalSetting.MaxTag; i++)
                tagFreeList.Add(i);
            randomStart = Random.Range(GobalSetting.MinTag, GobalSetting.MaxTag);
            pool = new CharacterPool(gameObject);
            dataBase = new CharacterDataBase();
            emitter = new CharacterEmitter(road, backGround);
            playable = pool.GetPlayer(Player);
            playable.SetCallAction((t) =>
            {
                TryToCall(t);
            });
            playable.Enable();
            // AddPerson(Random.Range(minTag, maxTag), CharacterEmitter.Road.Front, CharacterEmitter.Road.Front, true, FinishHandle);
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
        }
        public void CallInvoke()
        {
            TryToCall(playable.isRight());
        }
        private void TryToCall(bool isRight)
        {
            _tag = dataBase.Check(isRight);
            if (_tag != -1)
            {
                Pause();
                Debug.Log("tag" + _tag);
                JFCanvas.SetActive(true);
                if (callEvent != null)
                    callEvent.Invoke();
            }
        }
        public bool AddPerson(int tag, CharacterEmitter.Road road, bool isRight = true, System.Action<int, bool> action = null)
        {
            PersonFactory.Talkable person = pool.get(tag);
            if (person == null)
                return false;
            dataBase.add(person);
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
            person.setHide(true);
            dataBase.addBack(person);
            emitter.AddBackground(person, isRight, () =>
            {
                action.Invoke(person.getTag(), person.GetisHide());
            });
            return true;
        }
        public void RandomBackRoadAppear()
        {
            if (tagFreeList.Count == 0) return;
            if (emitter.backState) return;
            emitter.LockBack();
            int t = randomStart;
            while (!tagFreeList.Contains(randomStart))
            {
                t = (t + 1) % (GobalSetting.MaxTag - GobalSetting.MinTag) + GobalSetting.MinTag;
            }
            while (!AddBackPerson(t, Random.Range(0, 2) == 1, FinishHandle))
            {
                t = (t + 1) % (GobalSetting.MaxTag - GobalSetting.MinTag) + GobalSetting.MinTag;
            }
            tagFreeList.Remove(t);
            tagUsingList.Add(t);
            randomStart = (t + 1) % (GobalSetting.MaxTag - GobalSetting.MinTag) + GobalSetting.MinTag;
        }
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
        }
        private void FinishHandle(int tag, bool ishide)
        {
            PersonFactory.Talkable person;
            if (ishide)
            {
                person = dataBase.getBack();
                emitter.UnLockBack();
                backRoadController.FinishHandle();
            }
            else
            {
                person = dataBase.get(tag);
                emitter.UnLockRoad(person.GetRoad());
                roadController.FinishHandle();
            }
            person.Finish();
            pool.push(tag, person);
            tagUsingList.Remove(tag);
            tagFreeList.Add(tag);
        }
        public void Pause()
        {
            if (isAccelerate)
            {
                Time.timeScale = 1;
                isAccelerate = false;
            }
            playable.Disable();
            if (inputManager != null)
                inputManager.Disable();
            roadController.Pause();
            backRoadController.Pause();
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
            // pauseCanvas.SetActive(false);
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
            roadController.Start();
            backRoadController.Start();
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
            Time.timeScale = 5f;
            isAccelerate = true;
        }
        private void AccelerateCancel()
        {
            Time.timeScale = 1f;
            isAccelerate = false;
        }
        public void ContinueInvoke() => continueEvent.Invoke();
        public void Quit() => Application.Quit();
    }
}
