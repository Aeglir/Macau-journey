using UnityEngine;
using MiniGame.Bar.NPC;

namespace MiniGame.Bar.NPCQUEUE
{
    public class NMotionCon : AnimationControllable
    {
        public delegate Npc GetAction();
        private GameObject layer;
        private int[] poslist;
        private System.Collections.Generic.List<Npc> currentNpcs;
        public NMotionCon(GameObject layer)
        {
            poslist = new int[]{
                GobalSetting.FIRSTPOS,
                GobalSetting.SECONDPOS,
                GobalSetting.THIRDPOS,
                GobalSetting.FOURPOS
            };
            currentNpcs = new System.Collections.Generic.List<Npc>();
            this.layer = layer;
        }
        private void Reset(GameObject obj)
        {
            obj.transform.SetParent(layer.transform);
            obj.transform.localScale = Vector3.one;
            RectTransform rect = obj.transform as RectTransform;
            Vector3 pos = new Vector3(GobalSetting.NPCSTARTPOSX, 0, 0);
            rect.anchoredPosition3D = pos;
            rect.SetSiblingIndex(0);
        }
        internal void SetTargetPos(Npc npc, int chaIndex)
        {
            GameObject obj = npc.gameObject;
            currentNpcs.Add(npc);
            Reset(obj); GoNext(npc, chaIndex);
        }
        internal void MoveStep(System.Action action)
        {
            if (currentNpcs.Count < 1)
                return;
            GoOut(currentNpcs[0]);
            for (int i = 1; i < currentNpcs.Count; i++)
            {
                currentNpcs[i - 1] = currentNpcs[i];
                GoNext(currentNpcs[i - 1], i - 1);
            }
            currentNpcs.RemoveAt(currentNpcs.Count - 1);
            action();
        }
        private void GoNext(Npc npc, int index)
        {
            npc.Distory();
            Vector3 pos = (npc.transform as RectTransform).anchoredPosition3D;
            int delta = (int)(pos.x - poslist[index]) / GobalSetting.METERFACTOR;
            pos.x = poslist[index];
            if (index == 0)
                npc.AccessBar = true;
            npc.GoIntoQueue(pos, GobalSetting.NPCMOVEFIRSTDURATIONMULTI * delta, (int)(GobalSetting.JUMNUMMULTI * delta));
            npc.Start();
        }
        private void GoOut(Npc npc)
        {
            Vector3 pos = (npc.transform as RectTransform).anchoredPosition3D;
            int delta = (int)(pos.x - GobalSetting.FINISHPOS) / GobalSetting.METERFACTOR;
            pos.x = GobalSetting.FINISHPOS;
            npc.GoOut(pos, GobalSetting.NPCMOVEFIRSTDURATIONMULTI * delta, (int)(GobalSetting.JUMNUMMULTI * delta));
            npc.Start();
        }
#if DEBUGMODE
        internal void SetOnGoal(Npc npc)
        {
            GameObject obj = npc.gameObject;
            currentNpcs.Add(npc);
            obj.transform.SetParent(layer.transform);
            obj.transform.localScale = Vector3.one;
            RectTransform rect = obj.transform as RectTransform;
            Vector3 pos = new Vector3(GobalSetting.FIRSTPOS, 0, 0);
            rect.anchoredPosition3D = pos;
            rect.SetSiblingIndex(0);
        }
#endif
        public void Distory()
        {
            foreach (Npc npc in currentNpcs)
            {
                npc.Distory();
            }
        }

        public void Pause()
        {
            foreach (Npc npc in currentNpcs)
            {
                npc.Pause();
            }
        }

        public void Start()
        {
            foreach (Npc npc in currentNpcs)
            {
                npc.Start();
            }
        }
    }
}
