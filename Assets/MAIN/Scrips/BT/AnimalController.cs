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

    public TextMeshProUGUI _textNotiLogger;
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

    private void Update()
    {
        //show state ra text cho de xem thoi
        switch (configAnimal.stateAnimal)
        {
            case ConfigAnimal.STATE_ANIMAL.Hungry:
                _textNotiLogger.text = "GO TARGET ->SHOW STATE HUNGRY";
                break;
            case ConfigAnimal.STATE_ANIMAL.NotHungry:
                _textNotiLogger.text = "PLAEYR STAND INTERACT ->CLICK FEED  -> SHOW STATE NO HUNGRY (because foodIndex=100)";
                break;
            case ConfigAnimal.STATE_ANIMAL.Sleep:
                _textNotiLogger.text = "GO TARGET -> SHOW STATE SLEEP";
                break;
            case ConfigAnimal.STATE_ANIMAL.FeedAnimal:
                _textNotiLogger.text = "PLAEYR STAND INTERACT ->CLICK FEED -> LOGIC EAT";
                break;
            case ConfigAnimal.STATE_ANIMAL.MoveAround:
                _textNotiLogger.text = "NOTHING -> GO AROUND";
                break;
            default:
                break;
        }
    }
    #region SET UP NODE FOR BEHAVIOR TREE
    private void GenerateBehaviorTree()
    {

        /* BehaviorTree = new Selector("ROOT ANIMAL",

              new Sequence("ANIMAL ARE SLEEP",
                 new CheckPointNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Sleep), //CHECK CURRENT STATE OF ANIMAL EQUAL SOME STATE_ANIMAL 
                 new GoToTargetNode(configAnimal, TargetsMove[0]),
                 new Timer(2f, new ShowStatusNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Sleep, objectStatus)),
                 new NavigationNode(configAnimal, ConfigAnimal.STATE_ANIMAL.None)),


               new Sequence("SEQUENCE WHEN HUNGRY OF ANIMAL",
                 new CheckPointNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Hungry),

                 new Selector("SELECTOR IF HAVE FOOD IN STORAGE OR NOT",

                     new Sequence("ANIMAL HUNGRY AND HAVE FOOD IN STORAGE",
                         new StatusFoodStorge(foodStorage), //CHECK IN FOOD  STORAGE ? NULL
                         new GoToTargetNode(configAnimal, foodStorage.transform),
                         new EatNode(configAnimal, foodStorage), //DO LOGIC, ANIMATION OF EAT
                         new Timer(3, new ShowStatusNode(configAnimal, ConfigAnimal.STATE_ANIMAL.FeedAnimal, objectStatus)),
                         new NavigationNode(configAnimal, ConfigAnimal.STATE_ANIMAL.None)),

                     new Sequence("ANIMAL HUNGRY AND NOT HAVE FOOD",
                         new GoToTargetNode(configAnimal, TargetsMove[2]),
                         new Timer(2, new ShowStatusNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Hungry, objectStatus)))
                    )
               ),

               new Sequence("MEETING OTHER ANIMAL",
                 new MeetingNode(configAnimal, otherAnimal),//CHECK IN ANIMAL CAN MEETING ?
                 new InRangeNode(configAnimal, otherAnimal, ConfigAnimal.STATE_ANIMAL.Meeting), //CHECK IN AREA ANIMAL MEETING ? 
                 new GoToTargetNode(configAnimal, otherAnimal),
                 new Sequence("MEETING OTHER ANIMAL",
                          new Timer(3f, new ShowStatusNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Meeting, objectStatus)),
                          new NavigationNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Meeting, ConfigAnimal.STATE_ANIMAL.None))),


            //MOVE AROUND MAP
            new Sequence("STATE NONE",
                     new CheckPointNode(configAnimal, ConfigAnimal.STATE_ANIMAL.None), //CHECK CURRENT STATE OF ANIMAL EQUAL SOME STATE_ANIMAL 
                     new RandomSelector("RANDOM MOVING",
                         new Sequence("GO AROUND",
                             new GoAroundNode(TargetsMove, configAnimal),
                             new ShowStatusNode(configAnimal, ConfigAnimal.STATE_ANIMAL.MoveAround, objectStatus)),
                         new Sequence("GO TO TARGET IDLE",
                             new GoToTargetNode(configAnimal, foodStorage.transform),
                             new ShowStatusNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Idle, objectStatus)))));*/

        BehaviorTree = new Selector("ROOT ANIMAL",
             new Sequence("ANIMAL ARE SLEEP",
                 new CheckPointNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Sleep), 
                 new ShowStatusNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Sleep, objectStatus),
                 new GoToTargetNode(configAnimal, TargetsMove[0])),
                 
            new Sequence("MOVE AROUND",
                new CheckPointNode(configAnimal, ConfigAnimal.STATE_ANIMAL.MoveAround   ),
                new  GoAroundNode(TargetsMove, configAnimal),
                new ShowStatusNode(configAnimal, ConfigAnimal.STATE_ANIMAL.MoveAround, objectStatus))
            );


    }
    #endregion
    
    #region Event Action

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