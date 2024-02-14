using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;
using static ConfigAnimal;

public class NavigationNode : Node
{
    private STATE_ANIMAL setState;

    private ConfigAnimal config;

    public NavigationNode( ConfigAnimal config,STATE_ANIMAL setState)
    {
        this.setState = setState;
        this.config = config;
    }

    protected override void OnReset()
    {
    }

    protected override NodeStatus OnRun()
    {
        config.stateAnimal = setState;
        return NodeStatus.Success;
    }

   
}
