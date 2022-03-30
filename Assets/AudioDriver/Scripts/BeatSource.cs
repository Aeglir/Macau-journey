using System.Collections.Generic;
using UnityEngine;

namespace AudioDrive
{
    [CreateAssetMenu(fileName = "BeatSource", menuName = "AudioDrive/BeatSource")]
    public class BeatSource : ScriptableObject
    {
        public AudioClip clip;
        public int BeatEventID;
        public System.Type type;
        public List<Track> TrackList;
        public BeatSource()
        {
            type = typeof(int);
            TrackList = new List<Track>();
        }
    }
    [System.Serializable]
    public class Track
    {
        public int TimeTick;
        public int intValue;
    }
}
