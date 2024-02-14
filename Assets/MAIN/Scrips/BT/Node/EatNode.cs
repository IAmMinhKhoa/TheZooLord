using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using WUG.BehaviorTreeVisualizer;



public class EatNode : Node
{
    private ConfigAnimal config;
    private enum EatState
    {
        Emoji_EAT,
        Emoji_NOTEAT,
        
        Nothing
    }
    private EatState eatState;

    public EatNode(ConfigAnimal config)
    {
        this.config = config;
        //eatState = EatState.AnimationEat;
    }

    protected override void OnReset()
    {    }

    protected override NodeStatus OnRun()
    {
        Debug.Log("logic eat");
        /*NEU CHI SO DOI >=100 THI CHI SHOW EMOJI NOT EAT
         NGUOC LAI NEU <100 THI THUC HIEN QUY TRINH : ANIMATION EAT-> EMOJI_EAT*//*
        if (eatState!= EatState.Nothing)
        {
            if (config.foodIndex >= 100)
            {
                eatState = EatState.Emoji_NOTEAT;
                config.foodIndex = 100;
            }
            else
                eatState = EatState.Emoji_EAT;
        }

        switch (eatState)
        {
          
            case EatState.Emoji_EAT:
                Debug.Log("Show ANIMATION EAT");
                Debug.Log("Show Emoji EAT");
                config.foodIndex += 10;
                eatState = EatState.Nothing;
                return NodeStatus.Success;

            //CASE ANIMAL FULL FOODINDEX (DONG VAT KHONG CO DOI)
            case EatState.Emoji_NOTEAT:
                Debug.Log("Show Emoji NOT EAT");
                eatState = EatState.Nothing;
                return NodeStatus.Success;
           
            default:
                break;
        }*/
        return NodeStatus.Success;
    }
}
