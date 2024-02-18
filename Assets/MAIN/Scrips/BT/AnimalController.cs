using System;
using System.Collections;
using System.Collections.Generic;
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
    public Transform Target;
    public Transform player;
    public Transform foodStorage;
    public List<Transform> TargetsMove = new List<Transform>();
    public float rangerFollowPlayer;
    public GameObject objectStatus;

    public TextMeshProUGUI _textNotiLogger;
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
        configAnimal.foodStorage = foodStorage.GetComponent<FoodStorage>();
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
            case ConfigAnimal.STATE_ANIMAL.Other:
                _textNotiLogger.text = "NOTHING -> GO AROUND";
                break;
            default:
                break;
        }
    }
    #region SET UP NODE FOR BEHAVIOR TREE
    private void GenerateBehaviorTree()
    {

        BehaviorTree = new Selector("ROOT ANIMAL",

             new Sequence("ANIMAL ARE SLEEP",
                new CheckPointNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Sleep),
                new GoToTargetNode(configAnimal, TargetsMove[0]),
                new ShowStatusNode(ConfigAnimal.STATE_ANIMAL.Sleep, 3f, objectStatus)),


              new Sequence("SEQUENCE WHEN HUNGRY OF ANIMAL",
                new CheckPointNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Hungry),

                new Selector("SELECTOR IF HAVE FOOD IN STORAGE OR NOT",

                    new Sequence("ANIMAL HUNGRY AND HAVE FOOD IN STORAGE",
                        new StatusFoodStorge(configAnimal), //CHECK IN FOOD  STORAGE ? NULL
                        new GoToTargetNode(configAnimal, foodStorage),
                        new EatNode(configAnimal, foodStorage), //DO LOGIC, ANIMATION OF EAT
                        new ShowStatusNode(ConfigAnimal.STATE_ANIMAL.FeedAnimal, 2f, objectStatus),
                        new NavigationNode(configAnimal, ConfigAnimal.STATE_ANIMAL.Other)),

                    new Sequence("ANIMAL HUNGRY AND NOT HAVE FOOD",
                        new GoToTargetNode(configAnimal, TargetsMove[2]),
                        new ShowStatusNode(ConfigAnimal.STATE_ANIMAL.Hungry, 0.5f, objectStatus))
                   )
              ),


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