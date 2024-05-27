using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hintText;
    [SerializeField] Button hintButton;
    [SerializeField] Sprite activeSprite;
    [SerializeField] Sprite deactiveSprite;

    [SerializeField] GameObject hintImage;

    int hintNum = 0;

    public bool isHintActive = false;
    // Start is called before the first frame update
    //void Start()
    //{
    //    hintText.text = hintNum.ToString();
    //}

    private void OnEnable()
    {
        hintNum = 3;
        hintText.text = hintNum.ToString();
    }

    private void OnDisable()
    {
        StopCoroutine(ActivateAndDeactivateAfterDelay(5f));
        isHintActive = false;       
    }

    // Update is called once per frame
    void Update()
    {
        SetActiveHint(isHintActive, hintImage);        
    }

    public void OnSelectHint()
    {
        if(hintNum > 0 && !isHintActive)
        {
            JigsawGameManager.instance.PlayHintOn();
            StartCoroutine(ActivateAndDeactivateAfterDelay(5f));
            hintNum--;
            hintText.text = hintNum.ToString();
        }
        else
        {
            JigsawGameManager.instance.PlayHintCant();
        }

    }
    IEnumerator ActivateAndDeactivateAfterDelay(float delay)
    {
        // Kích hoạt puzzleImage
        isHintActive = true;

        yield return new WaitForSeconds(delay);
        JigsawGameManager.instance.PlayHintOff();

        // Vô hiệu hóa puzzleImage
        isHintActive = false;
    }

    void SetActiveHint(bool active, GameObject hintImgae)
    {
        if(active)
        {
            hintButton.GetComponent<Image>().sprite = activeSprite;
            hintImgae.SetActive(active);
        } else
        {
            hintButton.GetComponent<Image>().sprite = deactiveSprite;
            hintImgae.SetActive(active);
        }
    }
}
