using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    const int NODES_IN_ROW = 9;
    const int SUPER_BIG_INT = 999;
    public Node[,] nodes = new Node[NODES_IN_ROW,NODES_IN_ROW];

    public NormalBall selectingBall = null;
    public Node target = null;

    public static Board mainBoard;
    // Start is called before the first frame update
    void Awake()
    {
        mainBoard = this;
        initNodes();
    }
    void Start()
    {
        //Debug side Testcase1:
            nodes[1, 1].SetNextSpawnBall(ColorDefine.Gray);
            nodes[1, 1].SpawnBall();

            nodes[0, 1].SetNextSpawnBall(ColorDefine.Gray);
            nodes[0, 1].SpawnBall();

            nodes[1, 0].SetNextSpawnBall(ColorDefine.Gray);
            nodes[1, 0].SpawnBall();

            SetSpawnQueue();
    }
    void Update()
    {

    }

    // Update is called once per frame
    void UpdateMap()
    {
        
    }
    public void SetSpawnQueue()
    {
        for(int i = 0 ; i < 4; i++)
        {
            Node randomNode1 = ChooseRandomIdleNode();
            if(i != 3)
                randomNode1.status = Node.STATUS.WillSpawn;
            else
                randomNode1.status = Node.STATUS.SubSpawn;
            randomNode1.SetNextSpawnBall(ColorDefine.Gray);
        }
    }
    public void SpawnBalls()
    {
        int spawnCount = 0;
        Node SubNode = null;
        foreach(Node node in nodes)
        {
            if(node.status == Node.STATUS.WillSpawn)
            {
                node.SpawnBall();
                spawnCount ++;
            }
            if(node.status == Node.STATUS.WillSpawn)
            {
                SubNode = node;
            }
        }
        if(spawnCount < 2)
        {
            SubNode.SpawnBall();
        }
    }
    private Node ChooseRandomIdleNode()
    {
        Node[] idleNodes = new Node[0];
        foreach(Node node in nodes)
        {
            if(node.status == Node.STATUS.Idle || node.status == Node.STATUS.SubSpawn)
            {
                Array.Resize(ref idleNodes, idleNodes.Length + 1);
                idleNodes[idleNodes.Length - 1] = node;
            }
        }
        int randomIndex = (int)Mathf.Floor(UnityEngine.Random.Range(0.0f, (float)idleNodes.Length - 0.001f));
        //if idleNodes.Length == 0 -> endgame

        return idleNodes[randomIndex];
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

        selectingStand.costToSelectedBall = 0;    

        Node currentNode = null;
        for(int i = 0; i < NODES_IN_ROW * NODES_IN_ROW - 1; i++)
        {
            currentNode = GetNextNodeToCheck();
            CheckAroundNodes(currentNode);
        }
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
            nextNode.previousNode = node;
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
        UnselectBall();
        selectingBall = newBall;
        selectingBall.SetMeAsSelectedBall();
        RouteForBall();
    }

    public void SetTarget(Node newTarget)
    {
        target = newTarget;
        MoveOrUnselect();
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
        selectingBall.SetTarget(target);
        selectingBall.Move();
    }
    public void UnselectBall()
    {
        if (selectingBall)
        {
            selectingBall.UnSelectedMe();
            selectingBall = null;
        }
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
