using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using WUG.BehaviorTreeVisualizer;
using Node = WUG.BehaviorTreeVisualizer.Node;

public enum NavigationActivity
{
    goToTarget
}
public class AnimalController : MonoBehaviour, IBehaviorTree
{
    //--- PUBLIC INSPECTOR
    protected NavMeshAgent MyNavMesh;
    protected Animator animator;
    public Transform Target;
    public Transform player;
    public List<Transform> TargetsMove = new List<Transform>();
    public float rangerFollowPlayer;
    
    //--- USING LOCAL
    public NodeBase BehaviorTree { get; set; }
    private Coroutine m_BehaviorTreeRoutine;
    private YieldInstruction m_WaitTime = new WaitForSeconds(.1f);


    //--- CONFIG CONTROLLER
    private ConfigAnimal configAnimal;
    public PointInteract pointInteract;
    private void Awake()
    {
        //Get Component
        animator = GetComponent<Animator>();
        MyNavMesh = GetComponent<NavMeshAgent>();
        configAnimal = GetComponent<ConfigAnimal>();
        //set event
        pointInteract.OnEnterTrigger += PointInteract_OnEnterTrigger;
        pointInteract.OnExitTrigger += PointInteract_OnExitTrigger;
        //referene
        configAnimal.animator = animator;
        configAnimal.agent = MyNavMesh;
    }
    private void Start()
    {
       
       

        //Init Function
        GenerateBehaviorTree();
       
        //Other
        if (m_BehaviorTreeRoutine == null && BehaviorTree != null)
        {
            m_BehaviorTreeRoutine = StartCoroutine(RunBehaviorTree());
        }
        ForceDrawingOfTree();
    }
    #region SET UP NODE FOR BEHAVIOR TREE
    private void GenerateBehaviorTree()
    {


        BehaviorTree = new Selector("ROOT ANIMAL",

            
            //GO TO TARGET WHEN PLAYER TOUCH POIN-INTERACT
           /* new Sequence("GO TO TARGET INTERACT ",
              *//*  new GoToTargetNode(configAnimal, TargetsMove[1]), //GO TO TARGET TO idle interact
                //sau nay them 1 node active button "eat" (KHONG DUOC XOA DONG COMMENT NAY)*//*
                new Sequence("DO EAT",
                    new CheckPointNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Eat),//CHECK PLAYER CLICK FOR ANIMAL EAT
                    new GoToTargetNode(configAnimal, TargetsMove[2]), //GO TO TARGET TO EAT
                    new EatNode(configAnimal))
                ),*/
            //DO ACTION EAT
            

            //MOVE TO PLAYER IF IN RANGE 
            new Sequence("MOVE TO PLAYER",
                    new InRangeNode(rangerFollowPlayer, this, player),
                    new GoToTargetNode(configAnimal, player)),


            //MOVE AROUND MAP
            new GoAroundNode(TargetsMove, configAnimal)) ;



    }
    #endregion

    #region Event Action
    private void PointInteract_OnExitTrigger()
    {
        configAnimal.CanInteract = false;
    }

    private void PointInteract_OnEnterTrigger()
    {
        configAnimal.CanInteract = true;
    }
    #endregion

    #region Default Behavior Tree
    private IEnumerator RunBehaviorTree()
    {
        while (enabled)
        {
            if (BehaviorTree == null)
            {
                $"{this.GetType().Name} is missing Behavior Tree. Did you set the BehaviorTree property?".BTDebugLog();
                continue;
            }

            (BehaviorTree as Node).Run();

            yield return m_WaitTime;
        }
    }

    public void ForceDrawingOfTree()
    {
        if (BehaviorTree == null)
        {
            $"Behavior tree is null - nothing to draw.".BTDebugLog();
        }
        //Tell the tool to draw the referenced behavior tree. The 'true' parameter tells it to give focus to the window. 
        BehaviorTreeGraphWindow.DrawBehaviorTree(BehaviorTree, true);
    }
    #endregion

    #region DrawRanger
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangerFollowPlayer);
    }
    #endregion

    #region Template
    #endregion

}