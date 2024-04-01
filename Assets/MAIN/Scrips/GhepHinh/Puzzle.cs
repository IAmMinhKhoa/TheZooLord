using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Puzzle : MonoBehaviour
{

    [SerializeField] float distanceRight;

    public Vector3 rightPosition;
    public Vector3 initialPosition;

    public bool inRightPosition;
    public bool selected;

    private bool soundWrong;


    private void OnEnable()
    {
        inRightPosition = false;
        selected = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(rightPosition == transform.localPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.localPosition, rightPosition) < distanceRight)
        {
           
            
            if (!selected && !inRightPosition)
            {
                PuzzleManager.instance.PlayDropDownTrue();
                transform.position = rightPosition;
                inRightPosition = true;
                ScoreKeeper.instance.IncrementCorrectPieces();
                GetComponent<SortingGroup>().sortingOrder = 0;
            }
        } else
        {
            if(!selected && transform.localPosition != initialPosition)
            {
                PuzzleManager.instance.PlayDropDownWrong();
                transform.localPosition = initialPosition; 
            }
            
        }
    }
}
