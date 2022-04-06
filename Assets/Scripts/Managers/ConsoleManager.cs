using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public interface ConsolePresenter
    {
        /// <summary>
        /// 控制台命令登记
        /// </summary>
        /// <param name="action">登记无返回值无参数回调函数</param>
        /// <param name="tip">帮助文本</param>
        void RegisterCommand(System.Action action, string tip = null, string objectName = null, string methodName = null);
        /// <summary>
        /// 控制台命令登记
        /// </summary>
        /// <param name="action">登记无返回值一个参数回调函数</param>
        /// <param name="tip">帮助文本</param>
        void RegisterCommand<T>(System.Action<T> action, string tip = null, string objectName = null, string methodName = null);
        /// <summary>
        /// 控制台命令登记
        /// </summary>
        /// <param name="action">登记无返回值两个参数回调函数</param>
        /// <param name="tip">帮助文本</param>
        void RegisterCommand<T1, T2>(System.Action<T1, T2> action, string tip = null, string objectName = null, string methodName = null);
        /// <summary>
        /// 控制台命令登记
        /// </summary>
        /// <param name="action">登记无返回值三个参数回调函数</param>
        /// <param name="tip">帮助文本</param>
        void RegisterCommand<T1, T2, T3>(System.Action<T1, T2, T3> action, string tip = null, string objectName = null, string methodName = null);
        /// <summary>
        /// 控制台命令登记
        /// </summary>
        /// <param name="action">登记无返回值四个参数回调函数</param>
        /// <param name="tip">帮助文本</param>
        void RegisterCommand<T1, T2, T3, T4>(System.Action<T1, T2, T3, T4> action, string tip = null, string objectName = null, string methodName = null);
        /// <summary>
        /// 控制台命令移除
        /// </summary>
        /// <param name="action">登记有控制台命令的类的实例</param>
        void RemobeAllCommand(object obj);
        /// <summary>
        /// 控制台命令移除
        /// </summary>
        /// <param name="action"登记无返回值无参数回调函数></param>
        void RemoveCommand(string objectName, string methodName);
        void RemoveCommand(Action action);
        /// <summary>
        /// 控制台命令移除
        /// </summary>
        /// <param name="action"登记无返回值一个参数回调函数></param>
        void RemoveCommand<T>(Action<T> action);
        /// <summary>
        /// 控制台命令移除
        /// </summary>
        /// <param name="action"登记无返回值两个参数回调函数></param>
        void RemoveCommand<T1, T2>(Action<T1, T2> action);
        /// <summary>
        /// 控制台命令移除
        /// </summary>
        /// <param name="action"登记无返回值三个参数回调函数></param>
        void RemoveCommand<T1, T2, T3>(Action<T1, T2, T3> action);
        /// <summary>
        /// 控制台命令移除
        /// </summary>
        /// <param name="action"登记无返回值四个参数回调函数></param>
        void RemoveCommand<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action);
    }
    public class ConsoleManager : MonoBehaviour, ConsolePresenter
    {
        #region inner class
        /// <summary>
        /// 命令类
        /// </summary>
        public class Command
        {
            #region fields
            /// <summary>
            /// 命令名称
            /// </summary>
            public string CommandName;
            /// <summary>
            /// 帮助文本
            /// </summary>
            public string tip;
            /// <summary>
            /// 回调函数
            /// </summary>
            public Delegate callback;
            #endregion
            #region private Method
            private System.Object GetParameter(string value, Type type)
            {
                if (type == typeof(int))
                {
                    return int.Parse(value);
                }
                if (type == typeof(float))
                {
                    return float.Parse(value);
                }
                if (type == typeof(bool))
                {
                    return bool.Parse(value);
                }
                return value;
            }
            #endregion
            /// <summary>
            /// 命令实例构造函数
            /// </summary>
            /// <param name="callback">回调函数</param>
            /// <param name="tip">帮助文本</param>
            public Command(Delegate callback, string tip = null)
            {
                this.callback = callback;
                CommandName = callback.Target.GetType().Name;
                this.tip = tip;
            }
            /// <summary>
            /// 触发回调
            /// </summary>
            /// <param name="parameters">参数</param>
            public void Call(params string[] parameters)
            {
                ParameterInfo[] infos = callback.GetMethodInfo().GetParameters();
                System.Object[] objs = new System.Object[infos.Length];
                for (int i = 0; i < infos.Length; i++)
                {
                    objs[i] = GetParameter(parameters[i], infos[i].ParameterType);
                }
                callback.Method.Invoke(callback.Target, objs);
            }
            /// <summary>
            /// 获取帮助文本
            /// </summary>
            /// <returns>帮助文本</returns>
            public string GetTip() => CommandName + ": " + tip;
        }
        public class CommandHelper
        {
            private Dictionary<string, Dictionary<string, Command>> commands;
            public CommandHelper() => commands = new Dictionary<string, Dictionary<string, Command>>();
            /// <summary>
            /// 添加到命令库函数
            /// </summary>
            /// <param name="objectName">对象名</param>
            /// <param name="methodName">方法名</param>
            /// <param name="command">命令</param>
            public void SetCommand(string objectName, string methodName, Command command)
            {
                if (!commands.ContainsKey(objectName))
                    commands.Add(objectName, new Dictionary<string, Command>());
                if (commands[objectName].ContainsKey(methodName))
                {
                    commands[objectName][methodName] = command;
                }
                else
                {
                    commands[objectName].Add(methodName, command);
                }
            }
            /// <summary>
            /// 命令移除函数
            /// </summary>
            /// <param name="objectName">移除对象实例</param>
            public void RemoveCommand(string objectName)
            {
                commands.Remove(objectName);
            }
            /// <summary>
            /// 命令移除函数
            /// </summary>
            /// <param name="objectName">移除对象实例</param>
            /// <param name="methodName">移除方法名</param>
            public void RemoveCommand(string objectName, string methodName)
            {
                commands[objectName].Remove(methodName);
            }
            /// <summary>
            /// 命令查找函数
            /// </summary>
            /// <param name="objectName">对象实例名</param>
            /// <param name="methodName">方法名</param>
            /// <returns>命令</returns>
            public Command GetCommand(string objectName, string methodName)
            {
                if (commands.ContainsKey(objectName))
                {
                    if (commands[objectName].ContainsKey(methodName))
                    {
                        return commands[objectName][methodName];
                    }
                }
                return null;
            }
            /// <summary>
            /// 帮助文本获取函数
            /// </summary>
            /// <returns>帮助文本</returns>
            public string[] GetHelp()
            {
                List<string> tips = new List<string>();
                commands.AsParallel().ForAll(entry =>
                {
                    entry.Value.AsParallel().ForAll(command => tips.Add(command.Value.GetTip()));
                });
                return tips.ToArray();
            }
        }
        class InputToken
        {
            private InputField inputField;
            private System.Action<string> action;
            /// <summary>
            /// 输入管理构造函数
            /// </summary>
            /// <param name="inputField">输入容器</param>
            /// <param name="action">回调函数</param>
            public InputToken(InputField inputField, System.Action<string> action)
            {
                this.inputField = inputField;
                this.action = action;
                inputField.onEndEdit.AddListener(FinishInput);
            }
            /// <summary>
            /// 输入词组获取函数
            /// </summary>
            /// <param name="str">输入文本</param>
            /// <returns>单词组</returns>
            public string[] GetTokens(string str) => str.Split(' ');
            /// <summary>
            /// 输入完成回调
            /// </summary>
            /// <param name="str">输入文本</param>
            public void FinishInput(string str)
            {
                if (str == string.Empty)
                    return;
                inputField.text = string.Empty;
                action.Invoke(str);
                inputField.ActivateInputField();
            }
        }
        public class CommandRunner
        {
            private string objectName;
            private string methodName;
            private string[] parameters;
            private string errorMessage;
            public string ObjectName { get => objectName; set => objectName = value; }
            public string MethodName { get => methodName; set => methodName = value; }
            public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
            /// <summary>
            /// 词组分析函数
            /// </summary>
            /// <param name="command">单词组</param>
            /// <returns>是否合法</returns>
            public bool AnalyzeCommand(string[] command)
            {
                if (command.Length < 2)
                    return false;
                objectName = command[0];
                methodName = command[1];
                parameters = null;
                parameters = Common.CopyRange<string>(command, 2, command.Length - 2);
                return true;
            }
            /// <summary>
            /// 命令运行函数
            /// </summary>
            /// <param name="command">命令</param>
            /// <returns>是否运行成功</returns>
            public bool RunCommand(Command command)
            {
                try
                {
                    errorMessage = string.Empty;
                    command.Call(parameters);
                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                    return false;
                }
                return true;
            }
        }
        class ConsoleEmitter
        {
            private const string SuccessfulText = "\nsucceed!!!";
            private ScrollRect scrollRect;
            private Text text;
            private System.Text.StringBuilder stringBuilder;
            private bool isFirst;
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="scrollRect">滚动窗体容器</param>
            public ConsoleEmitter(ScrollRect scrollRect)
            {
                this.scrollRect = scrollRect;
                text = scrollRect.GetComponentInChildren<Text>();
                stringBuilder = new System.Text.StringBuilder();
                text.text = stringBuilder.ToString();
                isFirst = true;
            }
            /// <summary>
            /// 清楚窗体文本
            /// </summary>
            public void ClearText()
            {
                stringBuilder.Clear();
                text.text = stringBuilder.ToString();
                isFirst = true;
            }
            /// <summary>
            /// 追加窗体文本
            /// </summary>
            /// <param name="text">追加的文本</param>
            public void AppendText(string text)
            {
                if (!isFirst)
                {
                    stringBuilder.Append("\n" + text);
                }
                else
                {
                    stringBuilder.Append(text);
                    isFirst = false;
                }
                this.text.text = stringBuilder.ToString();
                scrollRect.SetLayoutVertical();
                scrollRect.verticalNormalizedPosition = 1;
            }
            /// <summary>
            /// 打印成功文本
            /// </summary>
            public void AppendSucceed()
            {
                stringBuilder.Append(SuccessfulText);
                this.text.text = stringBuilder.ToString();
                scrollRect.SetLayoutVertical();
                scrollRect.verticalNormalizedPosition = 1;
            }
        }
        #endregion
        [Header("控制台面板")]
        public GameObject panel;
        #region private fields
        private InputField inputField;
        private CommandHelper commandHelper;
        private InputToken inputToken;
        private CommandRunner runner;
        private ConsoleEmitter emitter;
        #endregion
        #region private methods
        private void Awake()
        {
            // #if !DEVELOPMENT_BUILD && !UNITY_EDITOR
            // DestroyImmediate(gameObject);
            // #endif
            Enable();
        }
        private void Enable()
        {
            inputField = GetComponentInChildren<InputField>(true);
            commandHelper = new CommandHelper();
            runner = new CommandRunner();
            emitter = new ConsoleEmitter(GetComponentInChildren<ScrollRect>(true));
            inputToken = new InputToken(inputField, CommandTokenRunner);
            GameManager.Instance.InputManager.AddInput(UnityEngine.InputSystem.Key.Tab, Name: "Console_Debug").AddListener(PanelControl).Enable();
            RegisterCommand(Help, "Help tips");
        }
        private void PanelControl(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            panel.SetActive(!panel.activeSelf);
            if (!panel.activeSelf)
            {
                emitter.ClearText();
            }
            inputField.ActivateInputField();
        }
        /// <summary>
        /// 帮助获取函数
        /// </summary>
        private void Help()
        {
            foreach (string tip in commandHelper.GetHelp())
            {
                emitter.AppendText(tip);
            }
        }
        private void OnDisable()
        {
            GameManager.Instance.InputManager.GetInputEvent(UnityEngine.InputSystem.Key.Tab).Dispose();
            RemoveCommand(Help);
        }
        #endregion
        /// <summary>
        /// 命令运行函数
        /// </summary>
        /// <param name="token">命令文本</param>
        public void CommandTokenRunner(string token)
        {
            emitter.AppendText(token);
            if (runner.AnalyzeCommand(inputToken.GetTokens(token)) && !runner.RunCommand(commandHelper.GetCommand(runner.ObjectName, runner.MethodName)))
            {
                emitter.AppendText(runner.ErrorMessage);
            }
            else
            {
                emitter.AppendSucceed();
            }
        }
        /// <summary>
        /// 命令设置函数
        /// </summary>
        /// <param name="action">回调</param>
        /// <param name="tip">帮助文本</param>
        public void SetCommand(Delegate action, string tip = null, string objectName = null, string methodName = null)
        {
            Command command = new Command(action, tip);
            if (objectName != null && methodName != null)
                commandHelper.SetCommand(objectName, methodName, command);
            else
                commandHelper.SetCommand(action.Target.GetType().Name, action.Method.Name, command);
        }
        /// <summary>
        /// 命令删除函数
        /// </summary>
        /// <param name="action">目标函数</param>
        public void DeleteCommand(Delegate action) => commandHelper.RemoveCommand(action.Target.GetType().Name, action.Method.Name);
        #region interface methods
        public void RemobeAllCommand(System.Object obj) => commandHelper.RemoveCommand(obj.GetType().Name);
        public void RemoveCommand(string objectName, string methodName) => commandHelper.RemoveCommand(objectName, methodName);
        public void RemoveCommand(Action action) => SetCommand(action as Delegate);
        public void RemoveCommand<T>(Action<T> action) => SetCommand(action as Delegate);
        public void RemoveCommand<T1, T2>(Action<T1, T2> action) => SetCommand(action as Delegate);
        public void RemoveCommand<T1, T2, T3>(Action<T1, T2, T3> action) => SetCommand(action as Delegate);
        public void RemoveCommand<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action) => SetCommand(action as Delegate);
        public void RegisterCommand(Action action, string tip = null, string objectName = null, string methodName = null) => SetCommand(action as Delegate, tip, objectName, methodName);
        public void RegisterCommand<T>(Action<T> action, string tip = null, string objectName = null, string methodName = null) => SetCommand(action as Delegate, tip, objectName, methodName);
        public void RegisterCommand<T1, T2>(Action<T1, T2> action, string tip = null, string objectName = null, string methodName = null) => SetCommand(action as Delegate, tip, objectName, methodName);
        public void RegisterCommand<T1, T2, T3>(Action<T1, T2, T3> action, string tip = null, string objectName = null, string methodName = null) => SetCommand(action as Delegate, tip, objectName, methodName);
        public void RegisterCommand<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, string tip = null, string objectName = null, string methodName = null) => SetCommand(action as Delegate, tip, objectName, methodName);
        #endregion
    }
}