using System;
using UnityEngine;

namespace MiniGame.Bar.NPCQUEUE
{
    public class NAppearCon : AsyncController,IDisposable
    {
        private NMotionCon motionCon;
        private System.Action<int> action;
        private int current;
        public Action<int> Action { set => action = value; }
        internal NAppearCon()
        {
            this.AsyncAction=Appear;
            current = 0;
        }
        ~NAppearCon()=>Dispose();
        private async System.Threading.Tasks.Task Appear()
        {
            while (!_isStop && current < GobalSetting.MAXWAITNPC)
            {
                action.Invoke(current);
                // UnityEngine.Debug.Log(current);
                current++;
                await System.Threading.Tasks.Task.Delay(1000*UnityEngine.Random.Range(3,GobalSetting.MAXNEXTWAITTIME));
            }
            Pause();
        }
        public override void Start()
        {
            if (AsyncAction!=null&&_isStop&&current<GobalSetting.MAXWAITNPC)
            {
                _isStop = false;
                _tokenSource = new System.Threading.CancellationTokenSource();
                _task = AsyncAction.Invoke();
            }
        }

        public void Dispose()
        {
            Pause();
        }
    }
}
