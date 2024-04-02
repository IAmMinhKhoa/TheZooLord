using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Piece : MonoBehaviour
{
    private Vector3 rightPosition;

    public bool inRightPosition;
    public bool selected;


    private void Awake()
    {
        rightPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, rightPosition) < 0.5f)
        {
            if(!selected && !inRightPosition)
            {
                transform.position = rightPosition;
                inRightPosition = true;
                ScoreKeeper.instance.IncrementCorrectPieces();
                GetComponent<SortingGroup>().sortingOrder = 0;
            }
        }        
    }
}
