using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] MazeNode nodePrefab;
    [SerializeField] PlayerMaze playerPrefab;

    [SerializeField] Vector2Int mazeSize;
    [SerializeField] float nodeSize;

    [SerializeField] CinemachineVirtualCamera vcam;
    [SerializeField] Transform Player;
    [SerializeField] Transform Goal;
    [SerializeField] Transform mazeTransform;

    public float MovementSmoothing;

    public int GoalX, GoalY;
    public int PlayerX, PlayerY;
    public Vector2 currentPlayerPosition;


    private void Start()
    {
        //GenerateMazeInstant(mazeSize);
        //StartCoroutine(GenerateMaze(mazeSize));
        StartNext(mazeSize);
    }

    private void Update()
    {
            
    }

    public int Rand(int max)
    {
        return UnityEngine.Random.Range(0, max);
    }

    public void StartNext(Vector2Int size)
    {
        foreach (Transform child in mazeTransform)
            Destroy(child.gameObject);
        PlayerX = Rand(size.x);
        PlayerY = Rand(size.y);

        int minDiff = Mathf.Max(size.x, size.y) / 2;
        while (true)
        {
            GoalX = Rand(size.x);
            GoalY = Rand(size.y);
            if (Mathf.Abs(GoalX - PlayerX) >= minDiff) break;
            if (Mathf.Abs(GoalY - PlayerY) >= minDiff) break;
        }

        GenerateMazeInstant(size);

        Player.transform.position = new Vector3(PlayerX - (size.x / 2f), PlayerY - (size.y / 2f), -1);
        //PlayerMaze playerMaze = Instantiate(playerPrefab, playerPos, Quaternion.identity, transform);
        Goal.transform.position = new Vector3(GoalX - (size.x / 2f), GoalY - (size.y / 2f), -1);

        vcam.m_Lens.OrthographicSize = Mathf.Pow(Mathf.Max(size.x / 1.5f, size.y), 0.70f) * 0.95f;
    }

    void GenerateMazeInstant(Vector2Int size)
    {
        List<MazeNode> nodes = new List<MazeNode>();

        // Create nodes  //Tạo ma trận node theo size
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)    //Tạo từ dưới lên trên, trái qua phải
            {
                Vector3 nodePos = new Vector3(x - (size.x / 2f), y - (size.y / 2f), 0);   //Lấy vị trí để tạo node 
                MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, mazeTransform);
                nodes.Add(newNode);
            }
        }

        List<MazeNode> currentPath = new List<MazeNode>();     //List chứa node hiện tại để xét - màu vàng  -> stack
        List<MazeNode> completedNodes = new List<MazeNode>();  //List chứa node đã xác nhận - màu xanh  -> Node hoàn thành là node ko tìm thấy cái node khả dụng xung quang

        // Choose starting node   //Chọn 1 node bất kì để bắt đầu
        currentPath.Add(nodes[Random.Range(0, nodes.Count)]);
        currentPath[0].SetState(NodeState.Current);

        while (completedNodes.Count < nodes.Count)
        {
            // Check nodes next to the current node
            List<int> possibleNextNodes = new List<int>();       //Chứa index(trong nodes) các node xung quanh node đang xét
            List<int> possibleDirections = new List<int>();      //Chứa hướng của các node xung quanh //Bên phải là 1, bên trái là 2, bên trên là 3, bên dưới là 4

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);   //Lấy node cuối cùng trong currentpath -> stack -> vào sau ra trước
            int currentNodeX = currentNodeIndex / size.y;      //Tìm toạ độ x của node đang xét
            int currentNodeY = currentNodeIndex % size.y;      //Tìm toạ độ y của node đang xét

            //Thực hiện tìm các node khả dụng xung quanh của currentNode đang xét
            if (currentNodeX < size.x - 1)  // Node đang xét có node bên phải (loại trừ các node ngoài bên phải ngoài cùng)
            {
                // Check node to the right of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }
            if (currentNodeX > 0)  //Node đang xét có node bên trái (loại trừ các node ngoài bên phải ngoài cùng vì toạ đồ x nó = với size.x - 1)
            {
                // Check node to the left of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            if (currentNodeY < size.y - 1)
            {
                // Check node above the current node       // Node đang xét có node bên trên (loại trừ các node bên trên cùng)
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }
            if (currentNodeY > 0)
            {
                // Check node below the current node    // Node đang xét có node bên dưới (loại trừ các node bên dưới cùng)
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            // Choose next node
            if (possibleDirections.Count > 0)   //nếu node đó tồn tại các node xung quanh
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);  //chọn 1 hướng bất kì
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];  //lấy node đó từ hướng đã random

                switch (possibleDirections[chosenDirection])
                {
                    case 1:       //nếu chọn node bên phải
                        chosenNode.RemoveWall(1);    //Xoá tường bên trái của node bên phải node đang xét
                        currentPath[currentPath.Count - 1].RemoveWall(0);    //Xoá tường bên phải của node đang xét
                        break;
                    case 2:      //nếu chọn node bên trái
                        chosenNode.RemoveWall(0);       //Xoá tường bên phải của node bên phải node đang xét
                        currentPath[currentPath.Count - 1].RemoveWall(1);  //Xoá tường bên trái của node đang xét
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }

                currentPath.Add(chosenNode);      //Thêm vào stack  ->> tiếp tục xét node này để tìm đường tiếp
                chosenNode.SetState(NodeState.Current);
            }
            else    //node đó ko còn tìm thấy các node khả dụng xung quanh -> node hoàn thành
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);    //add vào complete node

                currentPath[currentPath.Count - 1].SetState(NodeState.Completed);     
                currentPath.RemoveAt(currentPath.Count - 1);    // -> Xoá nó khỏi stack
            }
            // ->>> tiếp tục xét node cuối cùng trong stack
        }
    }

    IEnumerator GenerateMaze(Vector2Int size)
    {
        List<MazeNode> nodes = new List<MazeNode>();

        // Create nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x - (size.x / 2f), y - (size.y / 2f), 0);
                MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                nodes.Add(newNode);

                yield return null;
            }
        }

        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        // Choose starting node
        currentPath.Add(nodes[Random.Range(0, nodes.Count)]);
        currentPath[0].SetState(NodeState.Current);

        while (completedNodes.Count < nodes.Count)
        {
            // Check nodes next to the current node
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if (currentNodeX < size.x - 1)
            {
                // Check node to the right of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }
            if (currentNodeX > 0)
            {
                // Check node to the left of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            if (currentNodeY < size.y - 1)
            {
                // Check node above the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }
            if (currentNodeY > 0)
            {
                // Check node below the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            // Choose next node
            if (possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }

                currentPath.Add(chosenNode);
                chosenNode.SetState(NodeState.Current);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);

                currentPath[currentPath.Count - 1].SetState(NodeState.Completed);
                currentPath.RemoveAt(currentPath.Count - 1);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
