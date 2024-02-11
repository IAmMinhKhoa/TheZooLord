using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using WUG.BehaviorTreeVisualizer;

public class GoAroundNode : Node
{
    //----parameter
    private List<Transform> targets;
    private AnimalController animalController;
    private ConfigAnimal config;
    //----local
    protected NavMeshAgent agen;
    protected int indexTargets = 0;
    protected Transform transAnimal;
    public GoAroundNode(List<Transform> targets, ConfigAnimal config)
    {
        this.targets = targets;
        this.config = config;
        Init();
    }
    protected void Init()
    {
        transAnimal = config.gameObject.transform;
        agen = config.agent;
    }

    protected override void OnReset() { 
    }

    protected override NodeStatus OnRun()
    {
        if (targets.Count == 0)
        {
            return NodeStatus.Failure;
        }
        Debug.Log(indexTargets);
        Transform target = targets[indexTargets];
        agen.SetDestination(targets[indexTargets].position);
        config.animator.SetBool("Walk", true);
        float distance = Vector3.Distance(target.position, transAnimal.position);//distane from now - targets[i]
        if (distance <= agen.stoppingDistance)
        {
            indexTargets++; 
            if (indexTargets >= targets.Count)
            {
                indexTargets = 0;
                return NodeStatus.Success;
            }
        }
        return NodeStatus.Success;
    }
}
