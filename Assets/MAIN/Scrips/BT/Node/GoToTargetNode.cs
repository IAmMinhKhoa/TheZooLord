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
    public NodeStatus status= NodeStatus.Failure;

    public GoToTargetNode(ConfigAnimal config, Transform target)
    {
        this.target = target;
        this.config = config;
        this.agent = config.agent;
        agent.SetDestination(target.position);
        status = NodeStatus.Failure;

    }

    protected override void OnReset()
    {
       
    }

    protected override NodeStatus OnRun()
    {
        float distance = Vector3.Distance(config.gameObject.transform.position, target.transform.position);
      
        if (distance > agent.stoppingDistance)
        {
            config.animator.SetBool("Walk", true);
            agent.SetDestination(target.position);
            status = NodeStatus.Running;

           
        }
        else if (distance <= agent.stoppingDistance)
        {
            config.animator.SetBool("Walk", false);
            Debug.Log("Object has reached the destination.");
            status = NodeStatus.Success;
        }
        else
        {
            status = NodeStatus.Failure;
        }

        return status;
    }
}
