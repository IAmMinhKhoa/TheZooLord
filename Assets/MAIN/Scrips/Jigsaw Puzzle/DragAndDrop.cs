using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] GameObject selectedPiece;
    int OIL = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.transform.CompareTag("Puzzle"))
            {     
                if(!hit.transform.GetComponent<Piece>().inRightPosition)
                {
                    selectedPiece = hit.transform.gameObject;    
                    selectedPiece.GetComponent<Piece>().selected = true;
                    selectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
                    OIL++;
                }
            }
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            if(selectedPiece != null)
            {
                selectedPiece.GetComponent<Piece>().selected = false;
                selectedPiece = null;
            }
        }

        if (selectedPiece != null)
        {
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedPiece.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0);
        }
    }
}
