using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using Disguise.Behaviors.Decorators;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using WUG.BehaviorTreeVisualizer;
using Node = WUG.BehaviorTreeVisualizer.Node;


public class AnimalController : MonoBehaviour, IBehaviorTree
{
    //--- PUBLIC INSPECTOR
    protected NavMeshAgent MyNavMesh;
    protected Animator animator;
    public Transform otherAnimal;
    public FoodStorage foodStorage;
    public List<Transform> TargetsMove = new List<Transform>();
    
    public GameObject objectStatus;
    //--- USING LOCAL
    public NodeBase BehaviorTree { get; set; }
    private Coroutine m_BehaviorTreeRoutine;
    private YieldInstruction m_WaitTime = new WaitForSeconds(.1f);


    //--- CONFIG CONTROLLER
    private ConfigAnimal configAnimal;
    private void Awake()
    {
        //Get Component
        animator = GetComponent<Animator>();
        MyNavMesh = GetComponent<NavMeshAgent>();
        configAnimal = GetComponent<ConfigAnimal>();

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
             new Sequence("ANIMAL ARE SLEEP",
                 new CheckPointNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Sleep), 
                 new ShowStatusNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Sleep, objectStatus),
                 new GoToTargetNode(configAnimal, TargetsMove[0])),


             new Sequence("HUNGRY",
                new CheckPointNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Hungry),

                new Selector("ACTION HUNGRY",
                    new Sequence("HAVE FOOD IN STORAGE",
                        new StatusFoodStorge(foodStorage),
                        new GoToTargetNode(configAnimal, foodStorage.transform),
                        new ShowStatusNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Eat, objectStatus),
                        new EatNode(configAnimal, foodStorage)),

                    new Sequence("NOT FOOD IN STORAGE",
                     new ShowStatusNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Hungry, objectStatus),
                        new GoToTargetNode(configAnimal, TargetsMove[2]))
                       
                    )
                ),


         


            new Sequence("MEETING ANIMAL",
                new CheckPointNode(configAnimal, ConfigAnimal.STATE_ANIMAL.MeetingAnimal),
                new GoToTargetNode(configAnimal, otherAnimal),
                new ShowStatusNode(configAnimal, ConfigAnimal.STATE_ANIMAL.MeetingAnimal, objectStatus),
               new Timer(4f, new NavigationNode(configAnimal, ConfigAnimal.STATE_ANIMAL.MeetingAnimal, ConfigAnimal.STATE_ANIMAL.None))),

            new Sequence("MOVE AROUND",
                new CheckPointNode(configAnimal, ConfigAnimal.STATE_ANIMAL.MoveAround   ),
                new GoAroundNode(TargetsMove, configAnimal),
                new ShowStatusNode(configAnimal, ConfigAnimal.STATE_ANIMAL.MoveAround, objectStatus))
            );


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

  

    #region Template
    #endregion

}