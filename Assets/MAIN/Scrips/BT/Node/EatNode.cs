using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using WUG.BehaviorTreeVisualizer;



public class EatNode : Node
{
    private ConfigAnimal config;
    private FoodStorage foodStorage;

    float waitingTimeEat = 0.2f;
    float tempTime;
    public EatNode(ConfigAnimal config, FoodStorage foodStorage)
    {
        this.config = config;
        this.foodStorage = foodStorage;
        tempTime = waitingTimeEat;
    }

    protected override void OnReset()
    {
        tempTime = waitingTimeEat;
    }

    protected override NodeStatus OnRun()
    {
        tempTime -= Time.deltaTime;
        Debug.Log(tempTime);
        if (tempTime <= 0)
        {
            Debug.Log("t---------------Start logic eat---------------");

            Debug.Log("ANIMATION EAT");

            foodStorage.ReduceFood(1);
            config.foodIndex += 10; //add value eat (max =100)

            Debug.Log("t---------------End logic eat---------------");

            return NodeStatus.Success;
        }
        else
            return NodeStatus.Running;


    }
}
