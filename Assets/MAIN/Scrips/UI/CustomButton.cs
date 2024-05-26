using System.Diagnostics;
using UnityEngine.EventSystems;

public class CustomButton : UnityEngine.UI.Button
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        SoundManager.instance.PlaySound(SoundType.ClickButton);
        print("click");
    }
}