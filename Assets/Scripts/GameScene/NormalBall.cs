using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NormalBall : MonoBehaviour
{
    private Node myStand = null;
    public enum STATUS{ Idle, Using}
    public STATUS status = STATUS.Idle;
    private Color myColor = new Color(0,0,0,0);
    //color val: white, green, blue, yellow, red, orange, purple, brown, dark-green, dark-blue, nothing
    private Transform target = null;
    private Transform nextPos = null;
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
    void SetTarget()
    {

    }
    public void SetColor(Color newColor)
    {
        myColor = newColor;
        gameObject.GetComponent<SpriteRenderer>().color = myColor;
    }
    void MoveToNextPos()
    {

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
}
