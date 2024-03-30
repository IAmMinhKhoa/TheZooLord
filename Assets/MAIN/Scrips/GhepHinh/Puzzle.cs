using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Puzzle : MonoBehaviour
{
    public static Puzzle Instance { get; private set; }

    [SerializeField] float distanceRight;

    private Vector3 rightPosition;
    public Vector3 initialPosition;

    public bool inRightPosition;
    public bool selected;

    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        rightPosition = transform.position;
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, rightPosition) < distanceRight)
        {
            if (!selected && !inRightPosition)
            {
                transform.position = rightPosition;
                inRightPosition = true;
                scoreKeeper.IncrementCorrectPieces();
                GetComponent<SortingGroup>().sortingOrder = 0;
            }
        } else
        {
            
            transform.position = initialPosition;
        }
    }

    //public void SetInitialPosition(Puzzle puzzle, )
    //{
    //    puzzle.initialPosition = 
    //}
}
