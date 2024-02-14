using System.Collections;
using System.Collections.Generic;
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
    public ShowStatusNode(STATE_ANIMAL compare_stateAnimal, float timeDelay, GameObject showStatus)
    {
        this.Compare_stateAnimal = compare_stateAnimal;
        this.timeDelay = timeDelay;
        this.showStatus = showStatus;

        DefaultTimeDelay = timeDelay;
        
    }

    protected override void OnReset()
    {
        timeDelay = DefaultTimeDelay;
    }

    protected override NodeStatus OnRun()
    {
        Debug.Log(timeDelay);
        if (timeDelay > 0)
        {
            timeDelay -= Time.deltaTime*5;
            if (Compare_stateAnimal == STATE_ANIMAL.NotHungry)
            {
                showStatus.SetActive(true);
                Debug.Log("show emoji NO HUNGRY");
                //return NodeStatus.Success;
            }
            if (Compare_stateAnimal == STATE_ANIMAL.Hungry)
            {
                showStatus.SetActive(true);
                Debug.Log("show emoji HUNGRY");
                //return NodeStatus.Success;
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
}
