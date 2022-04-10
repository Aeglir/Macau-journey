namespace MiniGame.Volunteer
{
    public static class GobalSetting
    {
        #region Person
        public const float NpcFrameDelta = 0.4f;
        public const UnityEngine.InputSystem.Key PlayerTurnLeftKey = UnityEngine.InputSystem.Key.A;
        public const UnityEngine.InputSystem.Key PlayerTurnRightKey = UnityEngine.InputSystem.Key.D;
        public const UnityEngine.InputSystem.Key PlayerCallKey = UnityEngine.InputSystem.Key.Enter;
        #endregion
        #region System
        public const int CheckLeftRange = -600;
        public const int CheckRightRange = 600;
        public const int ScreenWidth = 2400;
        public const int RoadBehindYoffset = 100;
        public const int RoadBehindZoffset = 0;
        public const int RoadMiddleYoffset = 50;
        public const int RoadMiddleZoffset = -1;
        public const int RoadFrontYoffset = 0;
        public const int RoadFrontZoffset = -2;
        public const int MinTag = 1;
        public const int MaxTag = 21;
        public const int MaxPerson = 5;
        public const int maxAlphaPerson = 3;
        public const float MinDelay = 2f;
        public const float MaxDelay = 8f;
        public const UnityEngine.InputSystem.Key PauseKey = UnityEngine.InputSystem.Key.Escape;
        public const UnityEngine.InputSystem.Key TipsKey = UnityEngine.InputSystem.Key.T;
        public const UnityEngine.InputSystem.Key AccelerateKey = UnityEngine.InputSystem.Key.Space;
        public const int FailtrueYoffset = 444;
        public const float Failtrueduration = 1;
        public const float AccelerateTimeMulity = 5.0f;
        public const float SourceTimeMulity = 5.0f;
        public const int InvaildTag = -1;
        #endregion
    }
}