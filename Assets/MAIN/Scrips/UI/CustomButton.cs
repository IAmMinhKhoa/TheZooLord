using System.Diagnostics;
using UnityEngine.EventSystems;

public class CustomButton : UnityEngine.UI.Button
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        //Play your sound default here
        print("click");
    }
}