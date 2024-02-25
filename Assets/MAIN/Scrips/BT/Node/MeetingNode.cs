using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;
public class MeetingNode : Node
{
    private ConfigAnimal config;
    private Transform otherAnimal;

    protected ConfigAnimal otherConfigAnimal;
    public MeetingNode(ConfigAnimal config, Transform otherAnimal)
    {
        this.config = config;
        this.otherAnimal = otherAnimal;
        otherConfigAnimal = otherAnimal.gameObject.GetComponent<ConfigAnimal>();
    }

    protected override void OnReset()
    {
        
    }

    protected override NodeStatus OnRun()
    {
        if (config.CanMeeting && otherConfigAnimal.CanMeeting)
        {
            return NodeStatus.Success;
        }else { return NodeStatus.Failure; }
    }

   
}
