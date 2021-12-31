using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    const int MAX_BALLS_CAN_CLEAR = 33;
    public static GameController gamecontroller;
    const int NODES_IN_ROW = 9;
    public static int turn = 1; //0: waiting, 1: thinking
    private int maxNumberOfColor = 3;
    private GameObject selectingBall = null;
    private string gameMode = "Classic";
    private int score = 0; 

    private Board board;
    // Start is called before the first frame update
    void Awake()
    {
        gamecontroller = this;
    }
    void Start()
    {
        board = Board.mainBoard;
    }
    void CheckEndGame()
    {

    }
    void EndGame()
    {

    }
    public void CheckScore(Node justUpdateNode)
    {
        int x_dir = (int)justUpdateNode.GetMyPosition().x;
        int y_dir = (int)justUpdateNode.GetMyPosition().y;

        Node[] verNode = VerticleCheck(x_dir, y_dir);
        Node[] D130Node = Diagonal130Check(x_dir, y_dir);
        Node[] D1030Node = Diagonal1030Check(x_dir, y_dir);
        Node[] horNode = HorizontalCheck(x_dir, y_dir);

        bool _vc = CheckToClearBall(verNode);
        bool _vd1 = CheckToClearBall(D130Node); 
        bool _vd2 = CheckToClearBall(D1030Node);
        bool _vh = CheckToClearBall(horNode);
        if(_vc || _vd1 || _vd2 || _vh)
        {
            justUpdateNode.Score();
        }

        board.SpawnBalls();
        board.SetSpawnQueue();
    }
    private bool CheckToClearBall(Node[] nodes)
    {
        if(nodes.Length > 3)
        {
            foreach( Node node in nodes)
            {
                node.Score();
            }
            return true;
        }
        return false;
    }
    private Node[] Diagonal130Check(int x, int y)
    {
        return ScoreCheck(x, y, 1, 1);
    }
    private Node[] Diagonal1030Check(int x, int y)
    {
        return ScoreCheck(x, y, 1, -1);
    }

    private Node[] HorizontalCheck(int x, int y)
    {
        return ScoreCheck(x, y, 0, 1);
    }

    private Node[] VerticleCheck(int x, int y)
    {
       return ScoreCheck(x, y, 1, 0);
    }

    private Node[] ScoreCheck(int x, int y, int type_x, int type_y)
    {
        Node[] straighNode = new Node[0];
        // straighNode[0] = board.nodes[x,y];
        int streak = 1;
        const int MAX_LENGTH_CHECK = 4;

        int checkedNodeX = x;
        int checkedNodeY = y;
       
        for(int i = 0; i < MAX_LENGTH_CHECK; i++)
        {
            if(checkedNodeX > -1 && checkedNodeX < NODES_IN_ROW - 1 && checkedNodeY > -1 && checkedNodeY < NODES_IN_ROW - 1)
            {    
                checkedNodeX += type_x;
                checkedNodeY += type_y;

                if (board.nodes[checkedNodeX,checkedNodeY].GetMyBall())
                {
                    if(board.nodes[checkedNodeX,checkedNodeY].GetMyBall().myColor == board.nodes[x,y].GetMyBall().myColor)
                    {
                        Array.Resize(ref straighNode, straighNode.Length + 1);
                        straighNode[straighNode.Length - 1] = board.nodes[checkedNodeX,checkedNodeY];
                        streak ++;
                    }
                }
                else i = MAX_LENGTH_CHECK;
            }
        }

        checkedNodeX = x;
        checkedNodeY = y;

        for(int i = 0; i < MAX_LENGTH_CHECK; i++)
        {
            if(checkedNodeX > 0 && checkedNodeX < NODES_IN_ROW && checkedNodeY > 0 && checkedNodeY < NODES_IN_ROW) 
            {    
                checkedNodeX -= type_x;
                checkedNodeY -= type_y;

                if (board.nodes[checkedNodeX,checkedNodeY].GetMyBall())
                {
                    if(board.nodes[checkedNodeX,checkedNodeY].GetMyBall().myColor == board.nodes[x,y].GetMyBall().myColor)
                    {
                        Array.Resize(ref straighNode, straighNode.Length + 1);
                        straighNode[straighNode.Length - 1] = board.nodes[checkedNodeX,checkedNodeY];
                        streak ++;
                    }
                }
                else i = MAX_LENGTH_CHECK;
            }
        }
        return straighNode;
    }
}
