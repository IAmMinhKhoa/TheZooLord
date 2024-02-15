using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;
using static ConfigAnimal;

public class ShowStatusNode : Node
{
    private STATE_ANIMAL Compare_stateAnimal;
    private float timeDelay = 0f;
    private GameObject showStatus;

    //use local
    private float DefaultTimeDelay;
    private TextMeshProUGUI _textMeshPro;
    public ShowStatusNode(STATE_ANIMAL compare_stateAnimal, float timeDelay, GameObject showStatus)
    {
        this.Compare_stateAnimal = compare_stateAnimal;
        this.timeDelay = timeDelay;
        this.showStatus = showStatus;


        _textMeshPro = GetText(this.showStatus);
      //  Debug.Log(_textMeshPro);
        DefaultTimeDelay = timeDelay;
        
    }

    protected override void OnReset()
    {
        timeDelay = DefaultTimeDelay;
    }

    protected override NodeStatus OnRun()
    {
      //  Debug.Log(timeDelay);
        if (timeDelay > 0)
        {
            timeDelay -= Time.deltaTime*5;
            if (Compare_stateAnimal == STATE_ANIMAL.NotHungry)
            {
                showStatus.SetActive(true);
                _textMeshPro.text = " NO HUNGRY";
                //Debug.Log("show emoji NO HUNGRY")
            }
            if (Compare_stateAnimal == STATE_ANIMAL.Hungry)
            {
                showStatus.SetActive(true);
                _textMeshPro.text = "NEED FEED,I HUNGRY";
                // Debug.Log("show emoji HUNGRY");
            }
            if (Compare_stateAnimal == STATE_ANIMAL.FeedAnimal)
            {
                showStatus.SetActive(true);
                _textMeshPro.text = "EATING";
                // Debug.Log("show emoji HUNGRY");
            }
            if (Compare_stateAnimal == STATE_ANIMAL.Sleep)
            {
                showStatus.SetActive(true);
                _textMeshPro.text = "SLEEP";
                //Debug.Log("show emoji SLEEP");
            }
            return NodeStatus.Running;
        }
        else
        {
            showStatus.SetActive(false);
            Debug.Log("turn off show");
            return NodeStatus.Success;
        } 
    }


    protected TextMeshProUGUI GetText(GameObject canvas)
    {
        return canvas.GetComponentInChildren<TextMeshProUGUI>();

    }
}
