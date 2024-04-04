using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Piece : MonoBehaviour
{
    [SerializeField] float distanceRight;

    public Vector3 rightPosition;
    public Vector3 initialPosition;

    public bool inRightPosition;
    public bool selected;


    private void Awake()
    {
        //rightPosition = transform.position;
        //Debug.Log(gameObject.name + ": " + rightPosition.ToString());
    }

    //private void OnDisable()
    //{
    //    inRightPosition = false;
    //    selected = false;
    //}

    // Start is called before the first frame update
    private void OnDisable()
    {
        inRightPosition = false;
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!selected)
        {
            if (Vector3.Distance(transform.localPosition, rightPosition) < distanceRight)
            {
                if (!inRightPosition)
                {
                    JigsawGameManager.instance.PlayDropDownTrue();
                    transform.localPosition = rightPosition;
                    inRightPosition = true;
                    ScoreKeeper.instance.IncrementCorrectPieces();
                    GetComponent<SortingGroup>().sortingOrder = 0;
                }
            }
            else if (transform.localPosition != initialPosition)
            {
                JigsawGameManager.instance.PlayDropDownWrong();
                transform.localPosition = initialPosition;
            }
        }
    }
}

