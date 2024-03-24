using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawGameManager : MonoBehaviour
{
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject hintCanvas;

    [SerializeField] GameObject puzzleImage;

    DragAndDrop DragAndDrop;

    private void Awake()
    {
        DragAndDrop = FindObjectOfType<DragAndDrop>();    
    }
    // Start is called before the first frame update
    void Start()
    {
        winCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(DragAndDrop.isComplete)
        {
            winCanvas.SetActive(true);
            puzzleImage.SetActive(true);
            hintCanvas.SetActive(false);
        }    
    }
}
