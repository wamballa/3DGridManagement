using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PinkyForecastPosition : ForecastPosition
{
    bool hasNextBlock = false;
    bool canMove = false;
    GameObject nextBlock;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject block4 = GetFourBlocksAhead();
        //Debug.Log("Name = " + block4.name);
        direction = Direction.right;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Vector3 GetPinkyTargetBlock(GameObject currentBlock)
    {

        //if (GetForwardBlock() == null)
        //{
        //HandleRotationsBasedOnPlayerPosition(currentBlock);
        //}
        if (currentBlock != null) HandleRotationsBasedOnPlayerPosition(currentBlock);

        Vector3 targetPos = new Vector3();

        for (int i = 0; i < 4; i++)
        {
            // Get Next Block
            nextBlock = GetForwardBlock();
            //if (nextBlock == null)
            //{
            //    print("ROTATE IF FORWARD BLOCK NULL!");
            //        HandleRotationsBasedOnPlayerPosition(currentBlock);
            //        nextBlock = GetForwardBlock();
            //}
            // Move to Next Block


            //MoveAlongPath(nextBlock);
            targetPos = MoveAlongPath();

            if (nextBlock != null)
            {
                // Handle Rotation Based On Random
                HandleRotationsBasedOnPlayerPosition(nextBlock);
                //currentBlock = nextBlock;
            }

        }
        return targetPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward);

    }

}
