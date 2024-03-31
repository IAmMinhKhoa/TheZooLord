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

    private bool soundWrong;

    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        rightPosition = transform.localPosition;
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

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
                scoreKeeper.IncrementCorrectPieces();
                GetComponent<SortingGroup>().sortingOrder = 0;
            }
        } else
        {
            if(!selected && transform.localPosition != initialPosition)
            {
                PuzzleManager.instance.PlayDropDownWrong();
            }
            transform.localPosition = initialPosition;
        }
    }
}
