using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Piece : MonoBehaviour
{
    private Vector3 rightPosition;
    public bool inRightPosition;
    public bool selected;
    // Start is called before the first frame update
    void Start()
    {
        rightPosition = transform.position;
        transform.position = new Vector3(Random.Range(8.5f, 15f), Random.Range(.5f, -5.5f));
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
                GetComponent<SortingGroup>().sortingOrder = 0;
            }
        }        
    }
}
