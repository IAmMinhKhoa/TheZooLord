using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public class StatusFoodStorge : Node

{
    private FoodStorage foodStorage;

    public StatusFoodStorge(FoodStorage foodStorage)
    {
        this.foodStorage = foodStorage;
    }

    protected override void OnReset()
    {
    }

    protected override NodeStatus OnRun()
    {
        if(foodStorage!= null)
        {
            if (foodStorage.Foods.Count != 0) return NodeStatus.Success;
        }
        return NodeStatus.Failure;
    }
}
