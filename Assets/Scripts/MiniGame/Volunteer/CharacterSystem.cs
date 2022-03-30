using System.Collections.Generic;
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
            private int count;
            public int RoadPersonCount { get => count; }
            public System.Collections.Generic.Dictionary<int, System.Collections.Generic.Queue<PersonFactory.Talkable>> RoadPersonDic { get => roadPersonDic; }
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
            }
            public CharacterDataBase()
            {
                roadPersonDic = new Dictionary<int, Queue<PersonFactory.Talkable>>();
            }
            public void add(PersonFactory.Talkable person)
            {
                int tag = person.getTag();
                if (!roadPersonDic.ContainsKey(tag))
                {
                    roadPersonDic.Add(tag, new Queue<PersonFactory.Talkable>());
                }
                roadPersonDic[tag].Enqueue(person);
                count++;
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
        }
        public class CharacterEmitter
        {
            public const int width = 2400;
            public const int height = 100;
            private const int MaxHeight = 100;
            private const int MinHeight = 0;
            private const int BehindYoffset = 100;
            private const int BehindZoffset = 0;
            private const int MiddleYoffset = 50;
            private const int MiddleZoffset = -1;
            private const int FrontYoffset = 0;
            private const int FrontZoffset = -2;
            public enum Road
            {
                Behind,
                Middle,
                Front
            }
            private GameObject road;
            private GameObject backGround;
            private readonly Vector3 scale = new Vector3(100, 100, 1);
            public CharacterEmitter(GameObject road, GameObject backGround)
            {
                this.road = road;
                this.backGround = backGround;
            }
            public void AddRoad(PersonFactory.Talkable person, Road source, Road destination, bool isRight = true, TweenCallback action = null)
            {
                GameObject obj = person.getGameObject();
                obj.transform.SetParent(road.transform);
                RectTransform rect = obj.transform as RectTransform;
                rect.localScale = scale;
                rect.anchoredPosition3D = GetPos(source, isRight);
                setAnimation(person, GetPos(destination, isRight, true), isRight, action);
            }
            public void AddCenter(PersonFactory.Talkable person)
            {
                GameObject obj = person.getGameObject();
                obj.transform.SetParent(road.transform);
                RectTransform rect = obj.transform as RectTransform;
                rect.localScale = scale;
                rect.anchoredPosition3D = Vector3.zero;
            }
            private Vector3 GetPos(Road type, bool isRight, bool isTarget = false)
            {
                Vector3 pos = Vector3.zero;
                if (isRight)
                {
                    pos.x = -width / 2;
                }
                else
                {
                    pos.x = width / 2;
                }
                if (isTarget)
                    pos.x = -pos.x;
                switch (type)
                {
                    case Road.Behind:
                        pos.y = BehindYoffset;
                        pos.z = BehindZoffset;
                        break;
                    case Road.Middle:
                        pos.y = MiddleYoffset;
                        pos.z = MiddleZoffset;
                        break;
                    case Road.Front:
                        pos.y = FrontYoffset;
                        pos.z = FrontZoffset;
                        break;
                }
                return pos;
            }
            public void setAnimation(PersonFactory.Talkable person, Vector3 destination, bool isRight = true, TweenCallback action = null)
            {
                person.initWalkingAnimation(destination, isRight, action);
                person.Walking();
            }
            public void AddBackground(GameObject obj)
            {
                obj.transform.SetParent(backGround.transform);
            }
        }
        public class CharacterPool : DicPool<int, PersonFactory.Talkable>
        {
            public CharacterPool(GameObject obj) : base(obj) { }
            public PersonFactory.Playable GetPlayer(GameObject obj) => PersonFactory.Person.GetPlayerInstance(obj) as PersonFactory.Playable;
            public override PersonFactory.Talkable get(int tag)
            {
                if (pool.ContainsKey(tag) && pool[tag].Count > 0)
                {
                    var person = pool[tag].Dequeue();
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
                base.push(tag, item);
            }
            protected override void DistroyItem(PersonFactory.Talkable item)
            {
                DistroyItem(item);
            }
            protected override PersonFactory.Talkable CreateItem(int tag)
            {
                return PersonFactory.Person.GetInstance(tag) as PersonFactory.Talkable;
            }
        }
        private const int minTag = 1;
        private const int maxTag = 21;
        public CharactersAsset asset;
        public GameObject Player;
        public GameObject perfab;
        public GameObject road;
        public GameObject backGround;
        public int maxOfRoadPerson;
        public int maxOfAlphaPerson;
        public bool gameStart;
        public float minAppearDelay;
        public float maxAppearDelay;
        private CharacterPool pool;
        private CharacterDataBase manager;
        private PersonFactory.Playable playable;
        private CharacterDataBase dataBase;
        private CharacterEmitter emitter;
        private bool isAppear;
        private int tag=-1;
        public int Tag { get => tag; }
        private void Awake()
        {
            pool = new CharacterPool(gameObject);
            dataBase = new CharacterDataBase();
            emitter = new CharacterEmitter(road, backGround);
            playable = pool.GetPlayer(Player);
            // AddPerson(Random.Range(minTag, maxTag), CharacterEmitter.Road.Front, CharacterEmitter.Road.Front, true, FinishHandle);
            personAppear();
        }
        public bool AddPerson(int tag, CharacterEmitter.Road road, CharacterEmitter.Road destination, bool isRight = true, System.Action<int> action = null)
        {
            PersonFactory.Talkable person = pool.get(tag);
            if (person == null)
                return false;
            dataBase.add(person);
            emitter.AddRoad(person, road, road, isRight, () =>
            {
                action.Invoke(person.getTag());
            });
            return true;
        }
        public void RandomRoadAppear()
        {
            CharacterEmitter.Road road = (CharacterEmitter.Road)Random.Range(0, 3);
            if (Random.Range(0f, 1f) < 0.8)
            {
                AddPerson(Random.Range(minTag, maxTag), road, road, Random.Range(0, 2) == 1, FinishHandle);
            }
            else
            {
                AddPerson(Random.Range(minTag, maxTag), road, (CharacterEmitter.Road)(((int)road + Random.Range(1, 3)) % 3), Random.Range(0, 2) == 1, FinishHandle);
            }
        }
        private void FinishHandle(int tag)
        {
            var person = dataBase.get(tag);
            person.Finish();
            pool.push(person.getTag(), person);
            if (!isAppear)
                personAppear();
        }
        async void personAppear()
        {
            isAppear = true;
            while (dataBase.RoadPersonCount < maxOfRoadPerson)
            {
                RandomRoadAppear();
                await System.Threading.Tasks.Task.Delay((int)(1000 * Random.Range(minAppearDelay, maxAppearDelay)));
            }
            isAppear = false;
        }
    }
}
