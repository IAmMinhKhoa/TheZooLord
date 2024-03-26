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
        if (config.previousState != Compare_stateAnimal)
        {
            config.DestroyChildren(_pointSpamw.transform);

            GameObject objStatus = config.ObjectEmojiStatus(Compare_stateAnimal, _pointSpamw);
            config.previousState = Compare_stateAnimal;
            return NodeStatus.Success;
        }

        return NodeStatus.Success;
    }

    
   
}
