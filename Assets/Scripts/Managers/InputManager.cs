using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Managers
{
    public interface InputSetable
    {
        /// <summary>
        /// 添加输入控制器
        /// </summary>
        /// <param name="key">按键key</param>
        /// <param name="type">响应类型</param>
        /// <param name="Name">名称</param>
        /// <returns>输入响应事件</returns>
        InputManager.InputEvent AddInput(Key key, InputActionType type = InputActionType.Button, string Name = null);
        
        /// <summary>
        /// 添加输入控制器
        /// </summary>
        /// <param name="key">鼠标key</param>
        /// <param name="type">响应类型</param>
        /// <param name="Name">名称</param>
        /// <returns>输入响应事件</returns>
        InputManager.InputEvent AddInput(InputManager.Mouse key, InputActionType type = InputActionType.Button, string Name = null);
        /// <summary>
        /// 添加输入控制器
        /// </summary>
        /// <param name="path">输入路径</param>
        /// <param name="type">响应类型</param>
        /// <param name="Name">名称</param>
        /// <returns>输入响应事件</returns>
        InputManager.InputEvent AddInput(string path, InputActionType type = InputActionType.Button, string Name = null);
        /// <summary>
        /// 获取输入响应事件
        /// </summary>
        /// <param name="key">按键key</param>
        /// <param name="type">响应类型</param>
        /// <returns>输入响应事件</returns>
        InputManager.InputEvent GetInputEvent(Key key, InputActionType type = InputActionType.Button);
        /// <summary>
        /// 获取输入响应事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type">响应类型</param>
        /// <returns>输入响应事件</returns>
        InputManager.InputEvent GetInputEvent(InputManager.Mouse key, InputActionType type = InputActionType.Button);
        /// <summary>
        /// 获取输入响应事件
        /// </summary>
        /// <param name="path">输入路径</param>
        /// <param name="type">响应类型</param>
        /// <returns>输入响应事件</returns>
        InputManager.InputEvent GetInputEvent(string path, InputActionType type = InputActionType.Button);
    }
    public class InputManager : MonoBehaviour, InputSetable
    {
        public enum Response
        {
            Started,
            Performed,
            Canceled
        }
        public enum Mouse
        {
            Left,
            Right,
            Position
        }
        class InputDataBank
        {
            private Dictionary<string, InputEvent> inputDic;
            private InputActionFactory factory;
            public InputDataBank()
            {
                inputDic = new Dictionary<string, InputEvent>();
                factory = new InputActionFactory();
            }
            ~InputDataBank()
            {
                foreach (string key in inputDic.Keys)
                {
                    inputDic[key].Dispose();
                    inputDic.Remove(key);
                }
            }
            /// <summary>
            /// key获取
            /// </summary>
            /// <param name="path">输入路径</param>
            /// <param name="type">输入类型</param>
            /// <returns></returns>
            private string GetKey(string path, string type) => path + type;
            private void RemoveInputEvent(string key) => inputDic.Remove(key);
            #region AddInput
            /// <summary>
            /// 输入添加
            /// </summary>
            /// <param name="key">按键key</param>
            /// <param name="type">响应类型</param>
            /// <param name="Name">名称</param>
            /// <returns>输入响应事件</returns>
            public InputEvent AddInput(Key key, InputActionType type = InputActionType.Button, string Name = null)
            {
                string path = InputCheckHelper.GetInputPath(key);
                if (path == null) return null;
                return AddInput(path, type, Name);
            }
            /// <summary>
            /// 输入添加
            /// </summary>
            /// <param name="key">鼠标key</param>
            /// <param name="type">响应类型</param>
            /// <param name="Name">名称</param>
            /// <returns>输入响应事件</returns>
            public InputEvent AddInput(Mouse key, InputActionType type = InputActionType.Button, string Name = null)
            {
                string path = InputCheckHelper.GetInputPath(key);
                if (path == null) return null;
                return AddInput(path, type, Name);
            }
            /// <summary>
            /// 输入添加
            /// </summary>
            /// <param name="path">输入路径</param>
            /// <param name="type">响应类型</param>
            /// <param name="Name">名称</param>
            /// <returns>输入响应事件</returns>
            public InputEvent AddInput(string path, InputActionType type = InputActionType.Button, string Name = null)
            {
                InputAction action = factory.GetNewInstance(path, type, Name);
                string key = GetKey(path, type.ToString());
                InputEvent inputEvent = new InputEvent(key, action,RemoveInputEvent);
                inputDic.Add(key, inputEvent);
                return inputEvent;
            }
            #endregion
            #region GetInputEvent
            /// <summary>
            /// 输入响应事件获取
            /// </summary>
            /// <param name="key">按键key</param>
            /// <param name="type">响应类型</param>
            /// <returns>输入响应事件</returns>
            public InputEvent GetInputEvent(Key key, InputActionType type = InputActionType.Button)
            {
                string path = InputCheckHelper.GetInputPath(key);
                if (path == null) return null;
                return GetInputEvent(path, type);
            }
            /// <summary>
            /// 输入响应事件获取
            /// </summary>
            /// <param name="key">鼠标key</param>
            /// <param name="type">响应类型</param>
            /// <returns>输入响应事件</returns>
            public InputEvent GetInputEvent(Mouse key, InputActionType type = InputActionType.Button)
            {
                string path = InputCheckHelper.GetInputPath(key);
                if (path == null) return null;
                return GetInputEvent(path, type);
            }
            /// <summary>
            /// 输入响应事件获取
            /// </summary>
            /// <param name="path">输入路径</param>
            /// <param name="type">响应类型</param>
            /// <returns>输入响应事件</returns>
            public InputEvent GetInputEvent(string path, InputActionType type = InputActionType.Button)
            {
                string key = GetKey(path, type.ToString());
                if (inputDic.ContainsKey(key))
                {
                    return inputDic[key];
                }
                else
                {
                    return null;
                }
            }
            #endregion
        }
        class InputActionFactory
        {
            /// <summary>
            /// 输入控制器获取
            /// </summary>
            /// <param name="path">输入路径</param>
            /// <param name="type">响应类型</param>
            /// <param name="Name">名称</param>
            /// <returns>输入控制器</returns>
            public InputAction GetNewInstance(string path, InputActionType type = InputActionType.Button, string Name = null)
            {
                InputAction action = new InputAction(Name, type);
                action.AddBinding(GetInputBinding(path, Name));
                return action;
            }
            /// <summary>
            /// 输入绑定器
            /// </summary>
            /// <param name="path">输入路径</param>
            /// <param name="Name">名称</param>
            /// <returns>输入绑定器</returns>
            private InputBinding GetInputBinding(string path, string Name = null)
            {
                InputBinding binding = new InputBinding(path: path, name: Name);
                return binding;
            }
        }
        public static class InputCheckHelper
        {
            public static string GetInputPath(Key key)
            {
                if(key==Key.Escape)
                    return "keyboard/escape";
                return Keyboard.current.FindKeyOnCurrentKeyboardLayout(key.ToString()).path;
            }
            public static string GetInputPath(Mouse type)
            {
                switch (type)
                {
                    case Mouse.Left:
                        return UnityEngine.InputSystem.Mouse.current.leftButton.path;
                    case Mouse.Right:
                        return UnityEngine.InputSystem.Mouse.current.rightButton.path;
                    case Mouse.Position:
                        return UnityEngine.InputSystem.Mouse.current.position.path;
                    default:
                        return UnityEngine.InputSystem.Mouse.current.position.path;
                }
            }
        }
        public class InputEvent : System.IDisposable
        {
            #region private fields
            private string key;
            private bool hasDisposed;
            private InputAction inputAction;
            private System.Action<string> disposeAction;
            private List<System.Action<InputAction.CallbackContext>> startedActions;
            private List<System.Action<InputAction.CallbackContext>> performedActions;
            private List<System.Action<InputAction.CallbackContext>> canceledActions;
            private bool isDiable;
            #endregion
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="key">字典key</param>
            /// <param name="inputAction">输入控制器</param>
            /// <param name="disposeAction">销毁回调</param>
            internal InputEvent(string key, InputAction inputAction, System.Action<string> disposeAction)
            {
                startedActions = new List<System.Action<InputAction.CallbackContext>>();
                performedActions = new List<System.Action<InputAction.CallbackContext>>();
                canceledActions = new List<System.Action<InputAction.CallbackContext>>();
                this.key = key;
                this.inputAction = inputAction;
                this.disposeAction = disposeAction;
                inputAction.started += StartedInvoke;
                inputAction.performed += PerformedInvoke;
                inputAction.canceled += CanceledInvoke;
            }
            #region  dispose
            ~InputEvent()
            {
                Dispose(true);
            }
            public void Dispose()
            {
                Dispose(false);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (hasDisposed) return;
                disposeAction.Invoke(key);
                inputAction.Disable();
                inputAction.Dispose();
                hasDisposed = true;
            }
            #endregion
            #region Invoke
            private void StartedInvoke(InputAction.CallbackContext context)
            {
                if(isDiable) return;
                for (int i = 0; i < startedActions.Count; i++)
                {
                    startedActions[i].Invoke(context);
                }
            }
            private void PerformedInvoke(InputAction.CallbackContext context)
            {
                if(isDiable) return;
                for (int i = 0; i < performedActions.Count; i++)
                {
                    performedActions[i].Invoke(context);
                }
            }
            private void CanceledInvoke(InputAction.CallbackContext context)
            {
                if(isDiable) return;
                for (int i = 0; i < canceledActions.Count; i++)
                {
                    canceledActions[i].Invoke(context);
                }
            }
            #endregion
            /// <summary>
            /// 回调添加函数
            /// </summary>
            /// <param name="action">回调函数</param>
            /// <param name="type">响应类型</param>
            /// <returns>输入响应事件</returns>
            public InputEvent AddListener(System.Action<InputAction.CallbackContext> action, Response type = Response.Performed)
            {
                switch (type)
                {
                    case Response.Started:
                        startedActions.Add(action);
                        break;
                    case Response.Performed:
                        performedActions.Add(action);
                        break;
                    case Response.Canceled:
                        canceledActions.Add(action);
                        break;
                    default:
                        performedActions.Add(action);
                        break;
                }
                return this;
            }
            /// <summary>
            /// 回调移除
            /// </summary>
            /// <param name="action">回调函数</param>
            /// <param name="type">响应类型</param>
            /// <returns>输入响应事件</returns>
            public InputEvent RemoveListener(System.Action<InputAction.CallbackContext> action, Response type = Response.Performed)
            {
                switch (type)
                {
                    case Response.Started:
                        startedActions.Remove(action);
                        break;
                    case Response.Performed:
                        performedActions.Remove(action);
                        break;
                    case Response.Canceled:
                        canceledActions.Remove(action);
                        break;
                    default:
                        performedActions.Remove(action);
                        break;
                }
                return this;
            }
            public void Enable()
            {
                isDiable=false;
                inputAction.Enable();
            }
            public void Disable()
            {
                isDiable=true;
                inputAction.Disable();
            }
        }
        private InputDataBank _data;
        private void Awake()
        {
            if (GameManager.Instance.hasLoad)
            {
                DestroyImmediate(this);
                return;
            }
            _data = new InputDataBank();
        }
        public InputEvent AddInput(Key key, InputActionType type = InputActionType.Button, string Name = null) => _data.AddInput(key, type, name);
        public InputEvent AddInput(Mouse key, InputActionType type = InputActionType.Button, string Name = null) => _data.AddInput(key, type, name);
        public InputEvent AddInput(string path, InputActionType type = InputActionType.Button, string Name = null) => _data.AddInput(path, type, name);
        public InputEvent GetInputEvent(Key key, InputActionType type = InputActionType.Button) => _data.GetInputEvent(key, type);
        public InputEvent GetInputEvent(Mouse key, InputActionType type = InputActionType.Button) => _data.GetInputEvent(key, type);
        public InputEvent GetInputEvent(string path, InputActionType type = InputActionType.Button) => _data.GetInputEvent(path, type);
    }
}
