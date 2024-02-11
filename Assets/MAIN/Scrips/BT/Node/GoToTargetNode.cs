using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using WUG.BehaviorTreeVisualizer;

public class GoToTargetNode : Node
{
    private NavMeshAgent agent;
    private Transform target;
    private ConfigAnimal config;
    public GoToTargetNode(ConfigAnimal config, Transform target) 
    {
        this.target = target;
        this.config = config;

        this.agent = config.agent;
    }
    protected override void OnReset()
    {

    }
    protected override NodeStatus OnRun()
    {
        
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            config.animator.Play("Idle");
            Debug.Log("Object has reached the destination.");
            return NodeStatus.Success;
        }
        else
        {
            agent.SetDestination(target.position);
            return NodeStatus.Running;
        }
        
    }

 
}
