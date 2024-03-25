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
    private GameObject _pointSpamw;


    public ShowStatusNode(ConfigAnimal config,STATE_ANIMAL compare_stateAnimal, GameObject showStatus)
    {
        this.config = config;
        this.Compare_stateAnimal = compare_stateAnimal;
        this._pointSpamw = showStatus;
    }

    protected override void OnReset()
    {
    }

    protected override NodeStatus OnRun()
    {
        /* if (Compare_stateAnimal == STATE_ANIMAL.NotHungry)
         {
             showStatus.SetActive(true);
             _textMeshPro.text = " NO HUNGRY";
             //Debug.Log("show emoji NO HUNGRY")
             return NodeStatus.Success;
         }
         if (Compare_stateAnimal == STATE_ANIMAL.Hungry)
         {
             showStatus.SetActive(true);
             _textMeshPro.text = "NEED FEED,I HUNGRY";
             // Debug.Log("show emoji HUNGRY");
             return NodeStatus.Success;
         }
         if (Compare_stateAnimal == STATE_ANIMAL.Eat)
         {
             showStatus.SetActive(true);
             _textMeshPro.text = "EATING";
             // Debug.Log("show emoji HUNGRY");
             return NodeStatus.Success;
         }
         if (Compare_stateAnimal == STATE_ANIMAL.Sleep)
         {
             showStatus.SetActive(true);
             _textMeshPro.text = "SLEEP";
            Debug.Log("show emoji SLEEP");
             return NodeStatus.Success;
         }
         if (Compare_stateAnimal == STATE_ANIMAL.MeetingAnimal)
         {
             showStatus.SetActive(true);
             _textMeshPro.text = "MEETING";
             //Debug.Log("show metting");

             return NodeStatus.Success;
         }
         if (Compare_stateAnimal == STATE_ANIMAL.MoveAround
             )
         {
             showStatus.SetActive(true);
             _textMeshPro.text = "moveAround";
             Debug.Log("movearound");
             return NodeStatus.Success;
         }
         if (Compare_stateAnimal == STATE_ANIMAL.Idle
            )
         {
             showStatus.SetActive(true);
             _textMeshPro.text = "idle";
            // Debug.Log("idle");
             return NodeStatus.Success;
         }
         return NodeStatus.Success;*/
        if (Compare_stateAnimal == STATE_ANIMAL.Sleep)
        {
            if (_pointSpamw.transform.childCount == 0)
            {
                Debug.Log("spam status");
                GameObject objStatus = config.ObjectEmojiStatus(_pointSpamw);
                
            }
            return NodeStatus.Success;
        }
        return NodeStatus.Success;

    }
}
