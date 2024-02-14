using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;
public class CheckInteractNode : Node
{
    private ConfigAnimal config;

    public CheckInteractNode(ConfigAnimal config)
    {
        this.config = config;
    }

    protected override void OnReset()
    { }
      

    protected override NodeStatus OnRun()
    {
        if (config.CanInteract) return NodeStatus.Success;
        return NodeStatus.Failure;
    }

    
}
