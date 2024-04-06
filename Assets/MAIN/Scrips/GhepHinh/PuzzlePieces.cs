using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.Rendering;

public class PuzzlePieces : MonoBehaviour
{
    [SerializeField] GameObject selectedPiece;
    int OIL = 1;

    private RaycastHit2D hit;
    private Vector3 mousePoint;
    //Hint hint;

    private void Awake()
    {
        //hint = FindObjectOfType<Hint>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (AnimalManager.Instance != null) {
            if (AnimalManager.Instance.isComplete == true)
            {
                return;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform != null && hit.transform.CompareTag("Puzzle"))
            {
                Debug.Log(hit.transform.gameObject.name);
                if (!hit.transform.GetComponent<Puzzle>().inRightPosition)
                {
                    PuzzleManager.instance.PlayPickUp();
                    selectedPiece = hit.transform.gameObject;
                    selectedPiece.GetComponent<Puzzle>().selected = true;
                    selectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
                    OIL++;
                }

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectedPiece != null)
            {
                selectedPiece.GetComponent<Puzzle>().selected = false;
                selectedPiece = null;
            }
        }

        if (selectedPiece != null)
        {
            mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedPiece.transform.localPosition = new Vector3(mousePoint.x, mousePoint.y, 0);
            Debug.Log("dragging");
        }
    }
}
