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
        DiagonalCheck(x_dir, y_dir);
        HorizontalCheck(x_dir, y_dir);
        VerticleCheck(x_dir, y_dir);
    }
    private void DiagonalCheck(int x, int y)
    {

    }

    private void HorizontalCheck(int x, int y)
    {

    }

    private void VerticleCheck(int x, int y)
    {
        int streak = 1;
        int max_x_check = NODES_IN_ROW;
        int min_x_check = 0;

        if(x < NODES_IN_ROW - 5)
        {
            max_x_check = x + 4;
        }
        if(x > 5)
        {
            min_x_check = x - 4;
        }

        for(int i = x+1; i < max_x_check; i++)
        {
            if (board.nodes[i,y].GetMyBall())
            {
                if(board.nodes[i,y].GetMyBall().myColor == board.nodes[x,y].GetMyBall().myColor)
                {
                    streak ++;
                }
            }
            else i = max_x_check - 1;
        }

        for(int i = x-1; i > min_x_check; i--)
        {
            if (board.nodes[i,y].GetMyBall())
            {
                if(board.nodes[i,y].GetMyBall().myColor == board.nodes[x,y].GetMyBall().myColor)
                {
                    streak ++;
                }
            }
            else i = min_x_check + 1;
        }

        print("steak: " + streak);
    }

    private void ScoreCheck(int x, int y, int type_x /*1*/, int type_y /*0*/)
    {
        int streak = 1;
        int max_x_check = NODES_IN_ROW - 1;
        int min_x_check = 0;

        if(x < NODES_IN_ROW - 5)
        {
            max_x_check = x + 4;
        }
        if(x > 4)
        {
            min_x_check = x - 4;
        }


        int counter = x +1;


        for(int i = x+1; i <= max_x_check; i++)
        {
            if (board.nodes[i,y].GetMyBall())
            {
                if(board.nodes[i,y].GetMyBall().myColor == board.nodes[x,y].GetMyBall().myColor)
                {
                    streak ++;
                }
            }
            else i = max_x_check;
        }

        for(int i = x-1; i >= min_x_check; i--)
        {
            if (board.nodes[i,y].GetMyBall())
            {
                if(board.nodes[i,y].GetMyBall().myColor == board.nodes[x,y].GetMyBall().myColor)
                {
                    streak ++;
                }
            }
            else i = min_x_check;
        }
        
        print("steak: " + streak);
    }
}
