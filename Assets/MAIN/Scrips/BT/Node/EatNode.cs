using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using WUG.BehaviorTreeVisualizer;



public class EatNode : Node
{
    private ConfigAnimal config;
    private Transform foodStorage;
    private enum EatState
    {
       AnimationEat,
       LogicValue,

    }
    private EatState eatState;

    public EatNode(ConfigAnimal config, Transform foodStorage)
    {
        this.config = config;
        this.foodStorage = foodStorage;
        //eatState = EatState.AnimationEat;
    }

    protected override void OnReset()
    {    }

    protected override NodeStatus OnRun()
    {
        Debug.Log("t---------------Start logic eat---------------");

        Debug.Log("ANIMATION EAT");

        config.foodIndex += 10; //add value eat (max =100)

        Debug.Log("t---------------End logic eat---------------");

        return NodeStatus.Success;
    }
}
