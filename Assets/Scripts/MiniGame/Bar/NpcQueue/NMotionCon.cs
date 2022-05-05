using UnityEngine;
using MiniGame.Bar.NPC;

namespace MiniGame.Bar.NPCQUEUE
{
    public class NMotionCon : AnimationControllable
    {
        public delegate Npc GetAction();
        private GameObject layer;
        private int[] poslist;
        private float[] durationlist;
        private int[] jumpnums;
        private System.Collections.Generic.List<Npc> currentNpcs;
        public NMotionCon(GameObject layer)
        {
            poslist = new int[]{
                GobalSetting.FIRSTPOS,
                GobalSetting.SECONDPOS,
                GobalSetting.THIRDPOS,
                GobalSetting.FOURPOS
            };
            durationlist = new float[]{
                GobalSetting.NPCMOVEFIRSTDURATION,
                GobalSetting.NPCMOVESECONDDURATION,
                GobalSetting.NPCMOVETHIRDDURATION,
                GobalSetting.NPCMOVEFOURDURATION
            };
            jumpnums = new int[]{
                GobalSetting.WALKJUMPNUMSFIRST,
                GobalSetting.WALKJUMPNUMSSECOND,
                GobalSetting.WALKJUMPNUMSTHIRD,
                GobalSetting.WALKJUMPNUMSFOUR
            };
            currentNpcs = new System.Collections.Generic.List<Npc>();
            this.layer = layer;
        }
        private void Reset(GameObject obj)
        {
            obj.transform.SetParent(layer.transform);
            obj.transform.localScale=Vector3.one;
            RectTransform rect = obj.transform as RectTransform;
            Vector3 pos = new Vector3(GobalSetting.NPCSTARTPOSX,0,0);
            rect.anchoredPosition3D=pos;
            rect.SetSiblingIndex(0);
        }
        internal void SetTargetPos(Npc npc,int chaIndex)
        {
            GameObject obj=npc.gameObject;
            currentNpcs.Add(npc);
            Reset(obj);
            RectTransform rect = obj.transform as RectTransform;
            Vector3 pos = rect.anchoredPosition3D;
            pos.x=poslist[chaIndex];
            if(chaIndex==0)
                npc.SetWaitFinishEvent((n)=>{
                    n.AccessBar=true;
                    // currentNpcs.Remove(n);
                });
            npc.GoIntoQueue(pos,durationlist[chaIndex],jumpnums[chaIndex]);
            npc.Start();
        }
#if DEBUGMODE
        internal void SetOnGoal(Npc npc)
        {
            GameObject obj=npc.gameObject;
            currentNpcs.Add(npc);
            obj.transform.SetParent(layer.transform);
            obj.transform.localScale=Vector3.one;
            RectTransform rect = obj.transform as RectTransform;
            Vector3 pos = new Vector3(GobalSetting.FIRSTPOS,0,0);
            rect.anchoredPosition3D=pos;
            rect.SetSiblingIndex(0);
        }
#endif
        public void Distory()
        {
            foreach(Npc npc in currentNpcs)
            {
                npc.Distory();
            }
        }

        public void Pause()
        {
            foreach(Npc npc in currentNpcs)
            {
                npc.Pause();
            }
        }

        public void Start()
        {
            foreach(Npc npc in currentNpcs)
            {
                npc.Start();
            }
        }
    }
}
