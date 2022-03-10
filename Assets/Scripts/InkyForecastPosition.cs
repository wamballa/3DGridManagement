using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InkyForecastPosition : ForecastPosition
{
    bool hasNextBlock = false;
    bool canMove = false;

    GameObject nextBlock;

    public Vector3 GetInkyTargetBlock(GameObject currentBlock)
    {

        if (currentBlock != null) HandleRotationsBasedOnPlayerPosition(currentBlock);

        Vector3 targetPos = new Vector3();

        for (int i = 0; i < 2; i++)
        {
            // Get 2 Blocks ahead of player
            nextBlock = GetForwardBlock();
            //nextPosition = GetForwardPos();
            // Move to Next Block
            targetPos = MoveAlongPath();

            if (nextBlock != null)
            {
                // Handle Rotation Based On Random
                HandleRotationsBasedOnPlayerPosition(nextBlock);
                //currentBlock = nextBlock;
            }

        }



        //return nextBlock;

        if (nextBlock != null)
        {
        }
        // Draw vector from Blinky through this position & double it
        // Get Blinkys position
        Vector3 blinkysPosition = GameObject.Find("Blinky").GetComponent<Transform>().position;
        // Get direction
        Vector3 targetDir = targetPos - blinkysPosition;
        // Get distance
        float distance = Vector3.Distance(targetPos, blinkysPosition);
        // Double distance
        distance *= 2f;
        // Create Ray
        Ray ray = new Ray(blinkysPosition, targetDir);
        // Get position
        Vector3 targetPosition = ray.GetPoint(distance);

        //nextBlock.transform.position = targetPosition;
        transform.position = targetPosition;

        //Vector3 inkysPosition = GameObject.Find("Inky").GetComponent<Transform>().position;

        return transform.position;
        //// Draw vector from Blinky through this position & double it
        //// Get Blinkys position
        //Vector3 blinkysPosition = GameObject.Find("Blinky").GetComponent<Transform>().position;
        //// Get direction
        //Vector3 targetDir = nextBlock.transform.position - blinkysPosition;
        //// Get distance
        //float distance = Vector3.Distance(nextBlock.transform.position, blinkysPosition);
        //// Double distance
        //distance *= 2f;
        //// Create Ray
        //Ray ray = new Ray(blinkysPosition, targetDir);
        //// Get position
        //Vector3 targetPosition = ray.GetPoint(distance);

        ////nextBlock.transform.position = targetPosition;
        //transform.position = targetPosition;

        ////Vector3 inkysPosition = GameObject.Find("Inky").GetComponent<Transform>().position;

        //return transform.gameObject;



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward);
    }

}


