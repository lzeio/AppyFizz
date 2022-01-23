using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public int routePosition;
    public bool isMoving;
    public int steps;
    public Route currentRoute;
    public Dice currentDice;
    int goBackPos, goForthPos;

    public bool movePlayer = true;

    public Bombs bb = new Bombs();
    public Fruits ff = new Fruits();
    private void Update()
    {
        StartCoroutine(Bombing());
        StartCoroutine(Appy());

        
        


        Debug.Log(gameObject.tag + routePosition);
    }
    public void PlayerMovement()
    {
        if(!isMoving)
        {
            
            steps = currentDice.SideValueCheck(steps);
            
            //Debug.Log("Dice Rolled from other script: " + steps);
       

            if (routePosition + steps <= currentRoute.childNodeList.Count)
            {
                Debug.Log("inside - routePosition + steps ");
                if (movePlayer)
                {
                    Debug.Log("inside - moveplayer if");
                    StartCoroutine(Move());
                }
                
            }
            else
            {
                Debug.Log("Rolled Number is too high " + steps);
            }
        }

       
    }


    IEnumerator Move()
    {
       
        if(isMoving)
        {
            yield break;
        }
        isMoving = true;

        while(steps > 0)
        {
            Vector3 nextPos = currentRoute.childNodeList[routePosition + 1].position;
            while(MoveToNextNode(nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);

            steps--;
            routePosition++;
        }
        
        isMoving = false;
    }

    private bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 8f * Time.deltaTime));
    }


    IEnumerator Bombing()
    {
        
       
        if(!isMoving)
        {
                   
            if (currentRoute.childNodeList[routePosition].tag == "Bomb")
            {
                int aa = currentRoute.childNodeList[routePosition].gameObject.GetComponent<Bombs>().goBackPos;
                //Bombing Graphics
                while (steps == 0)
                {
                   
                    Vector3 nextPos = currentRoute.childNodeList[aa].position;
                    while (MoveToNextNode(nextPos))
                    {

                        yield return null;
                       
                    }

                    yield return new WaitForSeconds(0.1f);
                    routePosition = aa;


                }
            }
        }
    }

    IEnumerator Appy()
    {
        
        if (!isMoving)
        {
            if (currentRoute.childNodeList[routePosition].tag == "Apple")
            {
                int aa =currentRoute.childNodeList[routePosition].gameObject.GetComponent<Fruits>().goForthPos;
                while (steps == 0)
                {
                    
                    Vector3 nextPos = currentRoute.childNodeList[aa].position;
                    while (MoveToNextNode(nextPos))
                    {
                       
                        yield return null;
                    
                    }

                    yield return new WaitForSeconds(0.1f);

                    routePosition = aa;


                }
            }
        }
        }
    }

