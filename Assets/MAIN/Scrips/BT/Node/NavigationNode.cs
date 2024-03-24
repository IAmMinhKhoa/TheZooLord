using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;
using static ConfigAnimal;

public class NavigationNode : Node
{
    private STATE_ANIMAL current_State=STATE_ANIMAL.None;
    private STATE_ANIMAL next_State = STATE_ANIMAL.None;
    private ConfigAnimal config;

    public NavigationNode( ConfigAnimal config,STATE_ANIMAL current_State, STATE_ANIMAL next_State)
    {
        this.current_State = current_State;
        this.next_State = next_State;
        this.config = config;
    }
    public NavigationNode(ConfigAnimal config, STATE_ANIMAL next_State)
    {
        this.next_State = next_State;
        this.config = config;
    }

    protected override void OnReset()
    {
    }

    protected override NodeStatus OnRun()
    {
        if (current_State == STATE_ANIMAL.MeetingAnimal) //if from state.meet -> input next state -> need reset value count meeting
        {
            /*config.CanMeetingAnimal = false;
            config.CallEventOnHandleCoolDown();*/
            config.ToogleCanMeetingAnimal(5);
            Debug.Log("met");
            config.stateAnimal = next_State;
            return NodeStatus.Success;
        }
        else
        {
            Debug.Log("deo met");
            config.stateAnimal = next_State;
            return NodeStatus.Success;
        }
    }

   
}
