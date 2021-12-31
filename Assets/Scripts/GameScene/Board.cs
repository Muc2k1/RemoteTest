using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    const int NODES_IN_ROW = 9;
    const int SUPER_BIG_INT = 999;
    private Node[,] nodes = new Node[NODES_IN_ROW,NODES_IN_ROW];

    public NormalBall selectingBall = null;
    public Node target = null;

    public static Board mainBoard;
    // Start is called before the first frame update
    void Awake()
    {
        mainBoard = this;
        GameController.board = this.GetComponent<Board>();
        initNodes();
    }
    void Start()
    {
        //Debug side Testcase1:
            nodes[1, 1].SetNextSpawnBall(ColorDefine.Blue);
            nodes[1, 1].SpawnBall();

            nodes[2, 3].SetNextSpawnBall(ColorDefine.Red);
            nodes[2, 3].SpawnBall();

            nodes[6, 6].SetNextSpawnBall(ColorDefine.Yellow);
            nodes[6, 6].SpawnBall();

            nodes[3, 7].SetNextSpawnBall(ColorDefine.Gray);
            nodes[3, 7].SpawnBall();

            nodes[5, 4].SetNextSpawnBall(ColorDefine.Gray);
            nodes[5, 4].SpawnBall();
    }
    void Update()
    {
        //Debug side Testcase1:
            if (Input.GetKeyDown("space"))
            {
                nodes[1, 1].Score();
                nodes[2, 3].Score();
                nodes[6, 6].Score();
                nodes[3, 7].Score();
                nodes[5, 4].Score();
            }
            if (Input.GetKeyUp("space"))
            {
                nodes[1, 1].SetNextSpawnBall(ColorDefine.Blue);
                nodes[1, 1].SpawnBall();

                nodes[2, 3].SetNextSpawnBall(ColorDefine.Red);
                nodes[2, 3].SpawnBall();

                nodes[6, 6].SetNextSpawnBall(ColorDefine.Yellow);
                nodes[6, 6].SpawnBall();

                nodes[3, 7].SetNextSpawnBall(ColorDefine.Gray);
                nodes[3, 7].SpawnBall();

                nodes[5, 4].SetNextSpawnBall(ColorDefine.Gray);
                nodes[5, 4].SpawnBall();
            }
    }

    // Update is called once per frame
    void UpdateMap()
    {
        
    }
    void SetSpawnBall()
    {

    }
    void SetSpawnQueue()
    {
        UpdateMap();
    }
    void RouteForBall()
    {
        Node selectingStand = selectingBall.GetMyStand();
        for (int i = 0; i < NODES_IN_ROW; i++)
        {
            for (int j = 0; j < NODES_IN_ROW; j++)
            {
                nodes[i,j].costToSelectedBall = SUPER_BIG_INT;
                nodes[i,j].isAcceptByRouter = false;
            }
        }
        // //khởi tạo (lấy dữ liệu các cung a.k.a lấy các node có chứa ball hoặc không)

        selectingStand.costToSelectedBall = 0;     
        // //lấy node đang được chọn
        Node currentNode = null;
        for(int i = 0; i < NODES_IN_ROW * NODES_IN_ROW - 1; i++)
        {
            // print("Loop count: " + i);
            currentNode = GetNextNodeToCheck();
            CheckAroundNodes(currentNode);
        }

        //Debug Side
        // for (int i = 0; i < NODES_IN_ROW; i++)
        // {
        //     for (int j = 0; j < NODES_IN_ROW; j++)
        //     {
        //         print(nodes[i,j].GetMyPosition() + " take " + nodes[i,j].costToSelectedBall);
        //     }
        // }
    }
    private Node GetNextNodeToCheck()
    {
        Node cheapestNode = null;
        int cheapestCost = SUPER_BIG_INT * SUPER_BIG_INT;

        for (int i = 0; i < NODES_IN_ROW; i++)
        {
            for (int j = 0; j < NODES_IN_ROW; j++)
            {
                if (!nodes[i,j].isAcceptByRouter && nodes[i,j].costToSelectedBall < cheapestCost)
                {
                    cheapestNode = nodes[i,j];
                    cheapestCost = nodes[i,j].costToSelectedBall;
                }
            }
        }
        cheapestNode.isAcceptByRouter = true;
        return cheapestNode;
    }
    private void CostCalculate(Node node, Node nextNode)
    {
        int newCostToSelectingBall = node.costToSelectedBall + CostToNextNode(nextNode);

        if(nextNode.costToSelectedBall > newCostToSelectingBall)
        {
            nextNode.costToSelectedBall = newCostToSelectingBall;
            nextNode.previousNode = new Vector2((float)node.GetMyPosition().x, (float)node.GetMyPosition().y);
        }
            
    }
    private void CheckAroundNodes(Node node)
    {
        int node_x = (int)node.GetMyPosition().x;
        int node_y = (int)node.GetMyPosition().y;

        if (node_x > 0) //có node trên
        {
            CostCalculate(node, nodes[node_x - 1, node_y]);
        }
        if (node_x < NODES_IN_ROW -1) //có node dưới
        {
            CostCalculate(node, nodes[node_x + 1, node_y]);
        }
        if (node_y > 0) //có node trái
        {
            CostCalculate(node, nodes[node_x, node_y - 1]);
        }
        if (node_y < NODES_IN_ROW -1) //có node phải
        {
            CostCalculate(node, nodes[node_x, node_y + 1]);
        }
    }
    private int CostToNextNode(Node thisNode)
    {
        int costToNextNode;
        if(thisNode.status == Node.STATUS.Holding)
            costToNextNode = SUPER_BIG_INT;
        else
            costToNextNode = 1;
        return costToNextNode;
    }
    public void SetSelectingBall(NormalBall newBall)
    {
        if (selectingBall)
        {
            selectingBall.UnSelectedMe();
        }
        selectingBall = newBall;
        selectingBall.SetMeAsSelectedBall();
        RouteForBall();
        // print("select ball from: " + selectingBall.GetMyStand());
    }

    public void SetTarget(Node newTarget)
    {
        target = newTarget;
        MoveOrUnselect();
        // print("target is: " + target);
    }

    private void MoveOrUnselect()
    {
        if(HasPath(target))
        {
            MoveBall();
        }
        else
            UnselectBall();
    }
    private bool HasPath(Node target)
    {
        if(target.costToSelectedBall == 0 || target.costToSelectedBall >= SUPER_BIG_INT)
        {
            return false;
        }
        return true;
    }
    private void MoveBall()
    {

    }
    private void UnselectBall()
    {

    }
    void initNodes(){
        for (int i = 0; i < NODES_IN_ROW; i++)
        {
            for (int j = 0; j < NODES_IN_ROW; j++)
            {
                nodes[i,j] = transform.GetChild(NODES_IN_ROW * i + j).gameObject.GetComponent<Node>();
                nodes[i,j].SetMyPosition(i, j);
            }
        }
    }
}
