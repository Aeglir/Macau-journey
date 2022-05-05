using UnityEngine.UI;

public class ABImage : Image
{
    protected override void Awake() {
        alphaHitTestMinimumThreshold=0.1f;
    }
}
