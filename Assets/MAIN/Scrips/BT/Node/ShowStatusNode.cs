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


    public ShowStatusNode(ConfigAnimal config,STATE_ANIMAL compare_stateAnimal)
    {
        this.config = config;
        this.Compare_stateAnimal = compare_stateAnimal;
     
    }

    protected override void OnReset()
    {
    }

    protected override NodeStatus OnRun()
    {
        if (config.previousState != Compare_stateAnimal)
        {
            

            GameObject objStatus = config.ObjectEmojiStatus(Compare_stateAnimal);
            config.previousState = Compare_stateAnimal;
            return NodeStatus.Success;
        }

        return NodeStatus.Success;
    }

    
   
}
