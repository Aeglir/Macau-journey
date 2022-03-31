using UnityEngine;
[RequireComponent(typeof(UnityEngine.UI.Image))]
public class HighlightedImage : MonoBehaviour
{
    public UnityEngine.UI.Image image;
    public Sprite HighlightedSprite;
    public Sprite DefaultSpriet;
    private void Awake() {
        image = GetComponent<UnityEngine.UI.Image>();
    }
    public void SetHighlighted()=>image.sprite=HighlightedSprite;
    public void SetDefault()=>image.sprite=DefaultSpriet;
}
