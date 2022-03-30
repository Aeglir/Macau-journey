using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioDrive
{
    [CreateAssetMenu(fileName = "AudioDriveCore", menuName = "AudioDrive/AudioDriveCore")]
    public class AudioDriveCore : ScriptableObject
    {
        public List<Session> sessions;
        public string[] GetNames()
        {
            List<string> strings= new List<string>();
            foreach(Session session in sessions)
            {
                strings.Add(session.name);
            }
            return strings.ToArray();
        }
    }
    [System.Serializable]
    public class Session
    {
        public string name;
        public int sample;
        public float BPM;
        public int BeatPerSession;
    }
}
