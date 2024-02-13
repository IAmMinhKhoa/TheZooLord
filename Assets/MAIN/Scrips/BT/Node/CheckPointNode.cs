using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using WUG.BehaviorTreeVisualizer;
using static ConfigAnimal;

public class CheckPointNode : Node
{
    private ConfigAnimal configAnimal;
    private STATE_ANIMAL compareStateAnimal;

    public CheckPointNode(ConfigAnimal configAnimal, STATE_ANIMAL compareStateAnimal)
    {
        this.configAnimal = configAnimal;
        this.compareStateAnimal = compareStateAnimal;
    }

    protected override void OnReset(){}

    protected override NodeStatus OnRun()
    {
        if (configAnimal.CanInteract)
        {
            Debug.Log(configAnimal.stateAnimal);
            /*if (configAnimal.stateAnimal == compareStateAnimal)
            {
                return NodeStatus.Success;
            }*/
            if (configAnimal.stateAnimal == compareStateAnimal)
            {
                return NodeStatus.Success;
            }
        }
          
        
        return NodeStatus.Failure;
     
    }
}
