using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;
using static ConfigAnimal;

public class ShowStatusNode : Node
{
    private ConfigAnimal config;
    private STATE_ANIMAL Compare_stateAnimal;
    private GameObject showStatus;

    //use local
    private TextMeshProUGUI _textMeshPro;
    public ShowStatusNode(ConfigAnimal config,STATE_ANIMAL compare_stateAnimal, GameObject showStatus)
    {
        this.config = config;
        this.Compare_stateAnimal = compare_stateAnimal;
        this.showStatus = showStatus;
        _textMeshPro = GetText(this.showStatus);
    }

    protected override void OnReset()
    {
    }

    protected override NodeStatus OnRun()
    {
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
        if (Compare_stateAnimal == STATE_ANIMAL.Meeting)
        {
            showStatus.SetActive(true);
            _textMeshPro.text = "MEETING";
            config.AnimalInRange.Clear();
        }
        return NodeStatus.Running;
        
    }


    protected TextMeshProUGUI GetText(GameObject canvas)
    {
        return canvas.GetComponentInChildren<TextMeshProUGUI>();

    }
}
