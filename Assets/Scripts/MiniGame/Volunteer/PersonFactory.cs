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
            public static Person GetInstance(int tag)
            {
                if (CharacterSystem.Instance.asset != null)
                {
                    foreach (var info in CharacterSystem.Instance.asset.Npcs)
                    {
                        if (info.tag == tag)
                        {
                            return (new Npc(GameObject.Instantiate<GameObject>(CharacterSystem.Instance.asset.Perfabs), info)) as Person;
                        }
                    }
                }
                return null;
            }
            public static Person GetPlayerInstance(GameObject obj)
            {
                CharacterInfo characterInfo = CharacterSystem.Instance.asset.Player;
                if (characterInfo != null)
                    return (new Player(obj, characterInfo)) as Person;
                return null;
            }
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
            void initWalkingAnimation(Vector3 targetPos, bool isRight, TweenCallback action = null);
            void setHide(bool ishide);
            GameObject getGameObject();
            void accelerate();
            int getTag();
            bool getIsRight();
            void Walking();
            void Stop();
            void Finish();
        }
        class Npc : Person, Talkable
        {
            private const float FrameDelta = 1;
            private bool isHide;
            private bool isStop;
            private int spriteIndex;
            private Sequence sequence;
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
            public void initWalkingAnimation(Vector3 targetPos, bool isRight, TweenCallback action = null)
            {
                if ((!_character.isRight && isRight) || (_character.isRight && !isRight))
                {
                    _sprite.flipX = true;
                }else{
                    _sprite.flipX=false;
                }
                sequence = DOTween.Sequence();
                sequence.Append(rectTransform.DOAnchorPos3D(targetPos, Vector3.Distance(targetPos, rectTransform.anchoredPosition3D) / _character.moveSpeed).SetEase(Ease.Linear));
                sequence.OnComplete(action);
                sequence.Pause();
            }
            private async void addFrameAnimation()
            {
                float timspan = FrameDelta;
                while (!isStop && sequence.active && sequence.IsPlaying())
                {
                    await sequence.AsyncWaitForPosition(timspan);
                    timspan += FrameDelta;
                    // Debug.Log("success");
                    _sprite.sprite = getNextFrameSprite();
                }
            }
            public void Walking()
            {
                isStop = false;
                sequence.Play();
                addFrameAnimation();
            }
            public void setHide(bool ishide)
            {
                throw new System.NotImplementedException();
            }
            public void Stop()
            {
                isStop = true;
                sequence.Pause();
            }
            private Sprite getNextFrameSprite()
            {
                spriteIndex++;
                spriteIndex = spriteIndex % _character.frameTex.Count;
                return _character.frameTex[spriteIndex];
            }
            public void Finish()
            {
                isStop = true;
                sequence.Kill();
            }
        }
        #endregion
        #region player
        public interface Playable
        {
            void TurnLeft();
            void TurnRight();
        }
        class Player : Person, Playable
        {
            Managers.InputManager.InputEvent KeyALeft;
            Managers.InputManager.InputEvent KeyDEvent;
            internal Player(GameObject obj, CharacterInfo character) : base(obj, character)
            {
                // KeyALeft = Managers.GameManager.Instance.InputManager.AddInput(UnityEngine.InputSystem.Key.A, UnityEngine.InputSystem.InputActionType.Button, "PlayerTurnLeft");
                // KeyDEvent = Managers.GameManager.Instance.InputManager.AddInput(UnityEngine.InputSystem.Key.D, UnityEngine.InputSystem.InputActionType.Button, "PlayerTurnRight");
                // KeyALeft.AddListener((c) =>
                // {
                //     TurnLeft();
                // });
                // KeyDEvent.AddListener((c) =>
                // {
                //     TurnRight();
                // });
                // KeyALeft.Enable();
                // KeyDEvent.Enable();
            }
            public void TurnLeft()
            {
                _sprite.flipX = _character.isRight;
            }
            public void TurnRight()
            {
                _sprite.flipX = _character.isRight;
            }
            ~Player()
            {
                KeyALeft.Dispose();
                KeyDEvent.Dispose();
            }
        }
        #endregion
    }
}