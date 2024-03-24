using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Hint : MonoBehaviour
{
    [SerializeField] int hintNum;
    [SerializeField] TextMeshProUGUI hintText;
    [SerializeField] GameObject puzzleImage;

    public bool isHintActive = false;
    // Start is called before the first frame update
    void Start()
    {
        hintText.text = hintNum.ToString();    
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public void OnSelectHint()
    {
        if(hintNum > 0)
        {
            StartCoroutine(ActivateAndDeactivateAfterDelay(5f));
            hintNum--;
            hintText.text = hintNum.ToString();
        }
        else
        {
            Debug.Log("Hết lượt");
        }

    }
    IEnumerator ActivateAndDeactivateAfterDelay(float delay)
    {
        // Kích hoạt puzzleImage
        puzzleImage.SetActive(true);
        isHintActive = true;

        yield return new WaitForSeconds(delay);

        // Vô hiệu hóa puzzleImage
        puzzleImage.SetActive(false);
        isHintActive = false;
    }
}
