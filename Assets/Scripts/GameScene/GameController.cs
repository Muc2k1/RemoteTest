using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    const int MAX_BALLS_CAN_CLEAR = 33;
    const int MAX_STEP_CHECK = 4;
    const int MIN_COLORS_CAN_ROLL = 3;
    const int MAX_COLORS_CAN_ROLL = 8;
    const int NO_OF_TURN_TO_GET_NEW_COLOR = 8;
    public static GameController gamecontroller;
    const int NODES_IN_ROW = 9;
    public static int turn = 1; //0: waiting, 1: thinking
    public int maxNumberOfColor = 3;
    private GameObject selectingBall = null;
    private string gameMode = "Classic";
    private int score = 0; 

    private Board board;

    public int turnCount = 0;
    // Start is called before the first frame update
    void Awake()
    {
        gamecontroller = this;
    }
    void Start()
    {
        board = Board.mainBoard;
    }
    public void NextTurn()
    {
        turnCount ++;
        maxNumberOfColor = turnCount / NO_OF_TURN_TO_GET_NEW_COLOR + MIN_COLORS_CAN_ROLL;
        maxNumberOfColor = Mathf.Clamp(maxNumberOfColor, MIN_COLORS_CAN_ROLL, MAX_COLORS_CAN_ROLL);
    }
    void CheckEndGame()
    {

    }
    void EndGame()
    {

    }
    public bool CheckScore(Node justUpdateNode)
    {
        if(justUpdateNode.HasHolding())
        {
            int x_dir = (int)justUpdateNode.GetMyPosition().x;
            int y_dir = (int)justUpdateNode.GetMyPosition().y;

            Node[] verNode = VerticleCheck(x_dir, y_dir);
            Node[] D130Node = Diagonal1Check(x_dir, y_dir);
            Node[] D1030Node = Diagonal2Check(x_dir, y_dir);
            Node[] horNode = HorizontalCheck(x_dir, y_dir);

            bool _vc = CheckToClearBall(verNode);
            bool _vd1 = CheckToClearBall(D130Node); 
            bool _vd2 = CheckToClearBall(D1030Node);
            bool _vh = CheckToClearBall(horNode);
            if(_vc || _vd1 || _vd2 || _vh)
            {
                justUpdateNode.Score();
            }
            return (_vc || _vd1 || _vd2 || _vh);
        }
        return false;
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
    private Node[] VerticleCheck(int x, int y)
    {
        Node[] res = new Node[0];
        int x_step_must_check_up = MAX_STEP_CHECK;
        int x_step_must_check_down = MAX_STEP_CHECK;
        if(x < MAX_STEP_CHECK)
        {
            x_step_must_check_up = x;
        }
        if(x > NODES_IN_ROW - 1 - MAX_STEP_CHECK)
        {
            x_step_must_check_down = NODES_IN_ROW - 1 - x;
        }
        for(int i = 1; i <= x_step_must_check_down; i++)
        {
            if(board.nodes[x+i,y].HasHolding())
            {
                if(board.nodes[x+i,y].GetMyBall().myColor == board.nodes[x,y].GetMyBall().myColor)
                {
                    Array.Resize(ref res, res.Length + 1);
                    res[res.Length - 1] = board.nodes[x+i,y];
                }
                else i = x_step_must_check_down;
            }
            else i = x_step_must_check_down;
        }
        for(int i = 1; i <= x_step_must_check_up; i++)
        {
            if(board.nodes[x-i,y].HasHolding())
            {
                if(board.nodes[x-i,y].GetMyBall().myColor == board.nodes[x,y].GetMyBall().myColor)
                {
                    Array.Resize(ref res, res.Length + 1);
                    res[res.Length - 1] = board.nodes[x-i,y];
                }
                else i = x_step_must_check_up;
            }
            else i = x_step_must_check_up;
        }
        return res;
    }
    private Node[] Diagonal1Check(int x, int y)
    {
        Node[] res = new Node[0];
        int y_step_must_check_left = MAX_STEP_CHECK;
        int y_step_must_check_right = MAX_STEP_CHECK;
        if(y < MAX_STEP_CHECK)
        {
            y_step_must_check_left = y;
        }
        if(y > NODES_IN_ROW - 1 - MAX_STEP_CHECK)
        {
            y_step_must_check_right = NODES_IN_ROW - 1 - y;
        }

        int x_step_must_check_up = MAX_STEP_CHECK;
        int x_step_must_check_down = MAX_STEP_CHECK;
        if(x < MAX_STEP_CHECK)
        {
            x_step_must_check_up = x;
        }
        if(x > NODES_IN_ROW - 1 - MAX_STEP_CHECK)
        {
            x_step_must_check_down = NODES_IN_ROW - 1 - x;
        }

        int step_must_check_ul = Math.Min(x_step_must_check_up, y_step_must_check_left);
        int step_must_check_dr = Math.Min(x_step_must_check_down, y_step_must_check_right);

        for(int i = 1; i <= step_must_check_ul; i++)
        {
            if(board.nodes[x-i,y-i].HasHolding())
            {
                if(board.nodes[x-i,y-i].GetMyBall().myColor == board.nodes[x,y].GetMyBall().myColor)
                {
                    Array.Resize(ref res, res.Length + 1);
                    res[res.Length - 1] = board.nodes[x-i,y-i];
                }
                 else i = step_must_check_ul;
            }
            else i = step_must_check_ul;
        }
        for(int i = 1; i <= step_must_check_dr; i++)
        {
            if(board.nodes[x+i,y+i].HasHolding())
            {
                if(board.nodes[x+i,y+i].GetMyBall().myColor == board.nodes[x,y].GetMyBall().myColor)
                {
                    Array.Resize(ref res, res.Length + 1);
                    res[res.Length - 1] = board.nodes[x+i,y+i];
                }
                 else i = step_must_check_dr;
            }
            else i = step_must_check_dr;
        }
        return res;
    }
    private Node[] Diagonal2Check(int x, int y)
    {
        Node[] res = new Node[0];
        int y_step_must_check_left = MAX_STEP_CHECK;
        int y_step_must_check_right = MAX_STEP_CHECK;
        if(y < MAX_STEP_CHECK)
        {
            y_step_must_check_left = y;
        }
        if(y > NODES_IN_ROW - 1 - MAX_STEP_CHECK)
        {
            y_step_must_check_right = NODES_IN_ROW - 1 - y;
        }

        int x_step_must_check_up = MAX_STEP_CHECK;
        int x_step_must_check_down = MAX_STEP_CHECK;
        if(x < MAX_STEP_CHECK)
        {
            x_step_must_check_up = x;
        }
        if(x > NODES_IN_ROW - 1 - MAX_STEP_CHECK)
        {
            x_step_must_check_down = NODES_IN_ROW - 1 - x;
        }

        int step_must_check_ur = Math.Min(x_step_must_check_up, y_step_must_check_right);
        int step_must_check_dl = Math.Min(x_step_must_check_down, y_step_must_check_left);

        for(int i = 1; i <= step_must_check_ur; i++)
        {
            if(board.nodes[x-i,y+i].HasHolding())
            {
                if(board.nodes[x-i,y+i].GetMyBall().myColor == board.nodes[x,y].GetMyBall().myColor)
                {
                    Array.Resize(ref res, res.Length + 1);
                    res[res.Length - 1] = board.nodes[x-i,y+i];
                }
                else i = step_must_check_ur;
            }
            else i = step_must_check_ur;
        }
        for(int i = 1; i <= step_must_check_dl; i++)
        {
            if(board.nodes[x+i,y-i].HasHolding())
            {
                if(board.nodes[x+i,y-i].GetMyBall().myColor == board.nodes[x,y].GetMyBall().myColor)
                {
                    Array.Resize(ref res, res.Length + 1);
                    res[res.Length - 1] = board.nodes[x+i,y-i];
                }
                else i = step_must_check_dl;
            }
            else i = step_must_check_dl;
        }
        return res;
    }
    private Node[] HorizontalCheck(int x, int y)
    {
        Node[] res = new Node[0];
        int y_step_must_check_left = MAX_STEP_CHECK;
        int y_step_must_check_right = MAX_STEP_CHECK;
        if(y < MAX_STEP_CHECK)
        {
            y_step_must_check_left = y;
        }
        if(y > NODES_IN_ROW - 1 - MAX_STEP_CHECK)
        {
            y_step_must_check_right = NODES_IN_ROW - 1 - y;
        }
        for(int i = 1; i <= y_step_must_check_right; i++)
        {
            if(board.nodes[x,y+i].HasHolding())
            {
                if(board.nodes[x,y+i].GetMyBall().myColor == board.nodes[x,y].GetMyBall().myColor)
                {
                    Array.Resize(ref res, res.Length + 1);
                    res[res.Length - 1] = board.nodes[x,y+i];
                }
                else i = y_step_must_check_right;
            }
            else i = y_step_must_check_right;
        }
        for(int i = 1; i <= y_step_must_check_left; i++)
        {
            if(board.nodes[x,y-i].HasHolding())
            {
                if(board.nodes[x,y-i].GetMyBall().myColor == board.nodes[x,y].GetMyBall().myColor)
                {
                    Array.Resize(ref res, res.Length + 1);
                    res[res.Length - 1] = board.nodes[x,y-i];
                }
                else i = y_step_must_check_left;
            }
            else i = y_step_must_check_left;
        }
        return res;
    }
}
