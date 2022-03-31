using System;
using DG.Tweening;
using UnityEngine;

namespace MiniGame.Volunteer
{
    public class PersonFactory
    {
        public abstract class Person
        {
            protected GameObject _obj;
            protected RectTransform rectTransform;
            protected SpriteRenderer _sprite;
            protected CharacterInfo _character;
            public GameObject Object { get => _obj; }
            public CharacterInfo characterInfo { get => _character; }
            /// <summary>
            /// Person实例抽象工厂方法
            /// </summary>
            /// <param name="tag">标识</param>
            /// <returns>Person</returns>
            public static Person GetInstance(int tag)
            {
                if (CharacterSystem.Instance.asset != null)
                {
                    foreach (var info in CharacterSystem.Instance.asset.Npcs)
                    {
                        if (info.tag == tag)
                        {
                            return (new Npc(GameObject.Instantiate<GameObject>
                            (CharacterSystem.Instance.asset.Perfabs), info)) as Person;
                        }
                    }
                }
                return null;
            }
            /// <summary>
            /// Player实例抽象工厂方法
            /// </summary>
            /// <param name="obj">绑定的GameObject</param>
            /// <returns>Player</returns>
            public static Person GetPlayerInstance(GameObject obj)
            {
                CharacterInfo characterInfo = CharacterSystem.Instance.asset.Player;
                if (characterInfo != null)
                    return (new Player(obj, characterInfo)) as Person;
                return null;
            }
            /// <summary>
            /// 抽象类构造函数
            /// </summary>
            /// <param name="obj">绑定的GameObject</param>
            /// <param name="character">参数信息</param>
            protected Person(GameObject obj, CharacterInfo character)
            {
                _obj = obj;
                _character = character;
                _sprite = obj.GetComponentInChildren<SpriteRenderer>();
                rectTransform = obj.transform as RectTransform;
                RectTransform rect = _sprite.gameObject.transform as RectTransform;
                rect.anchoredPosition = _character.TexOffset;
                rect.sizeDelta = Vector2.zero;
                _sprite.sprite = _character.frameTex[0];
            }
        }
        #region character
        public interface Talkable
        {
            /// <summary>
            /// 初始化行走动画
            /// </summary>
            /// <param name="targetPos">行走位置</param>
            /// <param name="isRight">行走方向</param>
            /// <param name="action">行走完成回调</param>
            void initWalkingAnimation(Vector3 targetPos, bool isRight, TweenCallback action = null);
            /// <summary>
            /// 是否隐藏
            /// </summary>
            /// <param name="ishide"></param>
            void setHide(bool ishide);
            /// <summary>
            /// 获取绑定的GameObject
            /// </summary>
            /// <returns>GameObject</returns>
            bool GetisHide();
            GameObject getGameObject();
            /// <summary>
            /// 加速行走
            /// </summary>
            void accelerate();
            /// <summary>
            /// 获取标识
            /// </summary>
            /// <returns>标识</returns>
            int getTag();
            /// <summary>
            /// 获取图片人物方向
            /// </summary>
            /// <returns>图片人物方向</returns>
            bool getIsRight();
            /// <summary>
            /// 开始行走
            /// </summary>
            void Walk();
            /// <summary>
            /// 停止行走
            /// </summary>
            void Pause();
            /// <summary>
            /// 结束行走
            /// </summary>
            void Finish();
            bool hasCalled();
            void Called();
        }
        class Npc : Person, Talkable
        {
            private bool isHide;
            private bool isStop;
            private int spriteIndex;
            private Sequence sequence;
            private bool isCalled;
            internal Npc(GameObject obj, CharacterInfo character) : base(obj, character)
            {
            }
            public void accelerate()
            {
                throw new System.NotImplementedException();
            }
            public int getTag() => _character.tag;
            public GameObject getGameObject() => _obj;
            public bool getIsRight() => _character.isRight;
            private System.Threading.Tasks.Task task;
            public void initWalkingAnimation(Vector3 targetPos, bool isRight, TweenCallback action = null)
            {
                if ((!_character.isRight && isRight) || (_character.isRight && !isRight))
                {
                    _sprite.flipX = true;
                }
                else
                {
                    _sprite.flipX = false;
                }
                sequence = DOTween.Sequence();
                sequence.Append(rectTransform.DOAnchorPos3D(targetPos, Vector3.Distance(targetPos, rectTransform.anchoredPosition3D) / _character.moveSpeed).SetEase(Ease.Linear));
                sequence.OnComplete(action);
                sequence.Pause();
            }
            private async System.Threading.Tasks.Task addFrameAnimation()
            {
                float timspan = GobalSetting.NpcFrameDelta;
                while (!isStop && sequence.active && sequence.IsPlaying())
                {
                    await sequence.AsyncWaitForPosition(timspan);
                    timspan += GobalSetting.NpcFrameDelta;
                    // Debug.Log("success");
                    _sprite.sprite = getNextFrameSprite();
                }
                // Debug.Log("finish");
            }
            public void Walk()
            {
                isStop = false;
                // Time.timeScale = 5f;
                sequence.Play();
                if (task == null)
                    task = addFrameAnimation();
            }
            public void setHide(bool isHide)
            {
                this.isHide= isHide;
            }
            public void Pause()
            {

                isStop = true;
                sequence.Pause();
            }
            private Sprite getNextFrameSprite()
            {
                spriteIndex++;
                spriteIndex = spriteIndex % _character.frameTex.Count;
                if(isHide)
                    return _character.alphaFrameTex[spriteIndex];
                else return _character.frameTex[spriteIndex];
            }
            public void Finish()
            {
                isHide=false;
                isCalled = false;
                isStop = true;
                sequence.Kill();
                task = null;
            }
            public bool hasCalled() => isCalled;
            public void Called() => isCalled = true;
            public bool GetisHide()=>isHide;
        }
        #endregion
        #region player
        public interface Playable
        {
            /// <summary>
            /// 向右转
            /// </summary>
            void TurnLeft();
            /// <summary>
            /// 向左转
            /// </summary>
            void TurnRight();
            bool isRight();
            void Call();
            public void SetCallAction(System.Action<bool> action);
        }
        class Player : Person, Playable
        {
            private Managers.InputManager.InputEvent TrunLeftInput;
            private Managers.InputManager.InputEvent TrunRightInput;
            private Managers.InputManager.InputEvent CallInput;
            private System.Action<bool> callAction;
            internal Player(GameObject obj, CharacterInfo character) : base(obj, character)
            {
                if (Managers.GameManager.Instance != null)
                {
                    TrunLeftInput = Managers.GameManager.Instance.InputManager.AddInput(GobalSetting.PlayerTurnLeftKey, UnityEngine.InputSystem.InputActionType.Button, "PlayerTurnLeft");
                    TrunRightInput = Managers.GameManager.Instance.InputManager.AddInput(GobalSetting.PlayerTurnRightKey, UnityEngine.InputSystem.InputActionType.Button, "PlayerTurnRight");
                    CallInput = Managers.GameManager.Instance.InputManager.AddInput(GobalSetting.PlayerCallKey, UnityEngine.InputSystem.InputActionType.Button, "PlayerCall");
                    TrunLeftInput.AddListener((c) =>
                    {
                        TurnLeft();
                    },Managers.InputManager.Response.Canceled);
                    TrunRightInput.AddListener((c) =>
                    {
                        TurnRight();
                    },Managers.InputManager.Response.Canceled);
                    CallInput.AddListener((c) =>
                    {
                        Call();
                    },Managers.InputManager.Response.Canceled);
                    TrunLeftInput.Enable();
                    TrunRightInput.Enable();
                    CallInput.Enable();
                }
            }
            public void Call()
            {
                if (callAction != null) callAction(isRight());
            }
            public void SetCallAction(System.Action<bool> action) => callAction = action;
            public bool isRight()
            {
                return (!_sprite.flipX && _character.isRight) || (_sprite.flipX && !_character.isRight);
            }
            public void TurnLeft()
            {
                _sprite.flipX = _character.isRight;
            }
            public void TurnRight()
            {
                _sprite.flipX = !_character.isRight;
            }
            ~Player()
            {
                if (TrunLeftInput != null)
                {
                    TrunLeftInput.Disable();
                    TrunLeftInput.Dispose();
                }
                if (TrunRightInput != null)
                {
                    TrunRightInput.Disable();
                    TrunRightInput.Dispose();
                }
                if (TrunLeftInput != null)
                {
                    CallInput.Disable();
                    CallInput.Dispose();
                }
            }
        }
        #endregion
    }
}