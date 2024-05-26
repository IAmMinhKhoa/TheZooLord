using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class JigsawPieces : MonoBehaviour
{
    [SerializeField] GameObject selectedPiece;
    int OIL = 1;

    JigsawGameManager jigsawGameManager;

    private RaycastHit2D hit;
    private Vector3 mousePoint;
    Hint hint;

    private void Awake()
    {
        jigsawGameManager = FindObjectOfType<JigsawGameManager>();
        hint = FindObjectOfType<Hint>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayRandomSound_MiniGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (jigsawGameManager.isComplete)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && !hint.isHintActive)
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform.CompareTag("Puzzle"))
            {
                if (!hit.transform.GetComponent<Piece>().inRightPosition)
                {
                    Debug.Log(hit.transform.gameObject.name);
                    hit.transform.localScale = new Vector3(1f,1f, 1f);
                    JigsawGameManager.instance.PlayPickUp();
                    selectedPiece = hit.transform.gameObject;
                    selectedPiece.GetComponent<Piece>().selected = true;
                    selectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
                    OIL++;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectedPiece != null)
            {
                selectedPiece.GetComponent<Piece>().selected = false;
                selectedPiece = null;
            }
        }

        if (selectedPiece != null)
        {
            mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedPiece.transform.localPosition = new Vector3(mousePoint.x, mousePoint.y, 0);
        }
    }
}
