using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackLevel : MonoBehaviour
{
    [SerializeField] Canvas levelCanvas;

    private void Awake()
    {
    }

    private void OnMouseDown()
    {
        //winVFX.Stop(); 
        JigsawGameManager.instance.BackMenu();
        transform.parent.gameObject.SetActive(false); 
        levelCanvas.gameObject.SetActive(true);

    }
}
