using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NormalBall : MonoBehaviour
{
    const int MAX_STEP_BALL_CAN_MOVE = 80;
    private Node myStand = null;
    public enum STATUS{ Idle, Using}
    public STATUS status = STATUS.Idle;
    public Color myColor = new Color(0,0,0,0);
    //color val: white, green, blue, yellow, red, orange, purple, brown, dark-green, dark-blue, nothing
    private Node target = null;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        
    }

    void SetNextPos()
    {

    }
    public void SetTarget(Node newTarget)
    {
        target = newTarget;
    }
    public void SetColor(Color newColor)
    {
        myColor = newColor;
        gameObject.GetComponent<SpriteRenderer>().color = myColor;
    }

    public void SetMeAsSelectedBall()
    {
        anim.Play("Selected", 0, 0f);
    }
    public void UnSelectedMe()
    {
        anim.Play("Sleeping", 0, 0f);
    }
    void GetScore()
    {

    }

    public void SetMyStand(Node node)
    {
        myStand = node;
    }
    public Node GetMyStand()
    {
        return myStand;
    }

    public void Move(){
        GameController.turn = 0;
        StartCoroutine(MoveStepByStep());
    }
    IEnumerator MoveStepByStep()
    {
        Node[] route = new Node[MAX_STEP_BALL_CAN_MOVE];
        int counter = 0;
        Node currentNode = target;
        while(currentNode != myStand)
        {
            route[counter] = currentNode;
            counter++;
            currentNode = currentNode.previousNode;
        }
        for(int i = counter - 1; i >= 0; i--)
        {
            MoveToNextPos(route[i]);
            yield return new WaitForSeconds(0.1f);
        }
        ReCalculateStand();
    }
    private void MoveToNextPos(Node nextPos)
    {
        transform.position = nextPos.gameObject.transform.position;
    }
    private void ReCalculateStand()
    {
        myStand.status = Node.STATUS.Idle;
        myStand.SetMyBall(null);
        myStand = target;
        myStand.SetMyBall(this.GetComponent<NormalBall>());
        myStand.status = Node.STATUS.Holding;
        target = null;
        GameController.gamecontroller.CheckScore(this.myStand);

        //Delete below code later
        CanSelectAgain();
    }

    private void CanSelectAgain()
    {
        GameController.turn = 1;
        Board.mainBoard.UnselectBall();
    }
}
