using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;
using static ConfigAnimal;
public class InRangeNode : Node
{
    private Transform target;
    private ConfigAnimal config;
    private STATE_ANIMAL setState;
    public InRangeNode(ConfigAnimal config, Transform target, STATE_ANIMAL setState)
    {
        this.config = config;
        this.target = target;
        this.setState = setState;
    }

    protected override void OnReset()
    {

    }

    protected override NodeStatus OnRun()
    {
        Vector3 tempTarget = new Vector3(target.position.x, target.gameObject.transform.position.y, target.position.z);
        float distance = Vector3.Distance(tempTarget, config.transform.position);
        Debug.Log("distance"+target+":"+ distance);
        if (distance<= config.rangerInteractWithAnimal)
        {
            Debug.Log("da vao area");
            config.stateAnimal = setState;
            return NodeStatus.Success;
        }
        else
        {
            return NodeStatus.Failure;
        }

       
    }

}
