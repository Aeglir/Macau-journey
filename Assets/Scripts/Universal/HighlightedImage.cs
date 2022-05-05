using UnityEngine;
public class HighlightedImage : MonoBehaviour
{
    public UnityEngine.UI.Image image;
    public Sprite HighlightedSprite;
    public Sprite SelectedSprite;
    public Sprite DefaultSprite;
    public Sprite PressedSprite;
    public Sprite DisableSprite;
    public void SetHighlighted()
    {
        image.sprite=HighlightedSprite;
    }
    public void SetDefault()
    {
        image.sprite=DefaultSprite;
    }
    public void SetSelected()
    {
        image.sprite=SelectedSprite;
    }
    public void SetPressed()
    {
        image.sprite=PressedSprite;
    }
    public void SetDisable()
    {
        image.sprite=DisableSprite;
    }
}
