using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ghost : MonoBehaviour
{
    // Identity
    string ghostName;

    // Reference variables
    public GameObject player;
    public GameObject scatterPoint;
    public GameManager gameManager;
    public GameObject testBlock;

    // Movement variables
    //float range = 10f;
    //Vector3 nextTilePos;
    //bool canChangeDirection = false;
    GameObject nextBlock;
    bool canMove;
    bool hasNextBlock = false;
    float speed = 3f;
    float rayCastLength = 1f;

    private GameObject target;
    private Vector3 targetPos;

    // Mode variables
    private bool isChasemode = true;

    // UI
    public TMP_Text toggleText;
    // Constants
    enum Direction
    {
        up,
        down,
        left,
        right
    }
    //Direction direction = Direction.right;
    // Rotations
    Quaternion upDirection = Quaternion.Euler(0, 0, 0);
    Quaternion downDirection = Quaternion.Euler(0, -180, 0);
    Quaternion leftDirection = Quaternion.Euler(0, -90, 0);
    Quaternion rightDirection = Quaternion.Euler(0, 90, 0);
    // Types of block
    private enum BlockType
    {
        none,
        upRight,
        upLeft,
        downRight,
        downLeft,
        leftUpDown,
        rightUpDown,
        leftRightDown,
        leftRightUp,
        cross
    }
    BlockType nextBlockType;

    // Start is called before the first frame update
    void Start()
    {
        ghostName = transform.name;
        print("Start ghost script for " + transform.name);
    }

    // Update is called once per frame
    void Update()
    {
        // isChaseMode = true ... chase ... change target to player
        // isChaseMode = false .... scatter ... change target to scatter point
        if (isChasemode)
        {
            //print("<<<");
            switch (ghostName)
            {
                case "Pinky":
                    targetPos = gameManager.GetPinkyTarget();
                    if (targetPos == null) target = player;
                    Debug.DrawLine(transform.position, targetPos, Color.magenta, 0.1f);
                    break;
                case "Blinky":
                    targetPos = player.transform.position;
                    Debug.DrawLine(transform.position, targetPos, Color.red, 0.1f);
                    break;
                case "Inky":
                    targetPos = gameManager.GetInkyTarget();
                    if (targetPos == null) target = player;
                    Debug.DrawLine(transform.position, targetPos, Color.cyan, 0.1f);
                    break;
                case "Clyde":
                    float distanceToPlayer = Vector3.Distance(
                        transform.position, player.transform.position);
                    //Debug.Log("Clyde distance = " + distanceToPlayer);
                    if (Mathf.Abs(distanceToPlayer) <= 8f)
                    {
                        targetPos = scatterPoint.transform.position;
                        Debug.DrawLine(transform.position, targetPos,
                            Color.yellow, 0.1f);
                    }
                    else
                    {
                        targetPos = player.transform.position;
                        Debug.DrawLine(transform.position, targetPos,
                            Color.yellow, 0.1f);
                    }
                    break;
                case null:
                    print("ERROR; no ghost found");
                    break;
            }
        }
        else
        {
            targetPos = scatterPoint.transform.position;
            Debug.DrawLine(transform.position, targetPos, Color.red, 0.1f);
        }

        if (hasNextBlock == false)
        {
            nextBlock = GetForwardBlock();
            if (nextBlock == null)
            {
                MoveForward();
            }
            else
            {
                hasNextBlock = true;
                canMove = true;
            }
        }
        else
        {
            if (canMove)
            // MOVE
            {
                Move(nextBlock);
            }
            else
            // TURN
            {
                HandleRoatationsBasedOnPlayerPosition(nextBlock);

                hasNextBlock = false;
                canMove = false;
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            print("Hit Player");
            //target = scatterPoint;
            //Destroy(other.gameObject);
        }
    }
    public void HandleRoatationsBasedOnPlayerPosition(GameObject nextBlock)
    {
        // Rotates gameobject based on 
        nextBlockType = GetNextBlockType(nextBlock);

        // STANDARD BLOCKS
        if (nextBlockType == BlockType.upRight)
        {
            if (transform.localRotation == leftDirection)
            {
                transform.localRotation = upDirection;
            }
            else if (transform.localRotation == downDirection)
            {
                transform.localRotation = rightDirection;
            }
        }

        if (nextBlockType == BlockType.upLeft)
        {
            if (transform.localRotation == rightDirection)
            {
                transform.localRotation = upDirection;
            }
            else if (transform.localRotation == downDirection)
            {
                transform.localRotation = leftDirection;
            }
        }

        if (nextBlockType == BlockType.downLeft)
        {
            if (transform.localRotation == rightDirection)
            {
                transform.localRotation = downDirection;
            }
            if (transform.localRotation == upDirection)
            {
                transform.localRotation = leftDirection;
            }
        }

        if (nextBlockType == BlockType.downRight)
        {
            if (transform.localRotation == leftDirection)
            {
                transform.localRotation = downDirection;
            }
            if (transform.localRotation == upDirection)
            {
                transform.localRotation = rightDirection;
            }
        }

        // DECISION BLOCKs
        // ┤ done
        // ╞ done
        // ╤ done
        // ╨  done
        // ╬ done

        // DECISION BLOCK - LEFT UP DOWN ┤
        if (nextBlockType == BlockType.leftUpDown)
        {
            // COMING RIGHT ┤
            if (transform.localRotation == rightDirection)
            {

                // Get neighbour blocks 
                float tile1DistToTarget = Vector3.Distance(GetLeftBlockPos(), targetPos);
                float tile2DistToTarget = Vector3.Distance(GetRightBlockPos(), targetPos);

                if (tile1DistToTarget < tile2DistToTarget)
                {
                    transform.localRotation = upDirection;
                }
                else
                {
                    transform.localRotation = downDirection;
                }
            }
            // COMING UP ┤
            else if (transform.localRotation == upDirection)
            {
                // Get next block in a direction
                float forwardDistToTarget = Vector3.Distance(GetForwardBlockPos(), targetPos);
                float westDistToTarget = Vector3.Distance(GetLeftBlockPos(), targetPos);

                if (forwardDistToTarget < westDistToTarget)
                {
                    transform.localRotation = upDirection;
                }
                else
                {
                    transform.localRotation = leftDirection;
                }
            }
            // COMING DOWN ┤
            else if (transform.localRotation == downDirection)
            {
                // print("Coming down");
                // Get next block in a direction
                float tile1DistToTarget = Vector3.Distance(GetForwardBlockPos(), targetPos);
                float tile2DistToTarget = Vector3.Distance(GetRightBlockPos(), targetPos);

                if (tile1DistToTarget < tile2DistToTarget)
                {
                    transform.localRotation = downDirection;
                }
                else
                {
                    transform.localRotation = leftDirection;
                }
            }
        }

        // DECISION BLOCK - RIGHT LEFT DOWN ╤
        else if (nextBlockType == BlockType.leftRightDown)
        {
            // COMING RIGHT ╤
            if (transform.localRotation == rightDirection)
            {

                // Get neighbour blocks 
                float tile1DistToTarget = Vector3.Distance(GetForwardBlockPos(), targetPos);
                float tile2DistToTarget = Vector3.Distance(GetRightBlockPos(), targetPos);

                if (tile1DistToTarget < tile2DistToTarget)
                {
                    transform.localRotation = rightDirection;
                }
                else
                {
                    transform.localRotation = downDirection;
                }
            }
            // COMING UP ╤
            else if (transform.localRotation == upDirection)
            {
                // print("Coming up");
                // Get next block in a direction
                float tile1DistToTarget = Vector3.Distance(GetRightBlockPos(), targetPos);
                float tile2DistToTarget = Vector3.Distance(GetLeftBlockPos(), targetPos);

                if (tile1DistToTarget < tile2DistToTarget)
                {
                    transform.localRotation = rightDirection;
                }
                else
                {
                    transform.localRotation = leftDirection;
                }
            }
            // COMING LEFT ╤
            else if (transform.localRotation == leftDirection)
            {
                //print("Coming left");
                // Get next block in a direction
                float tile1DistToTarget = Vector3.Distance(GetForwardBlockPos(), targetPos);
                float tile2DistToTarget = Vector3.Distance(GetLeftBlockPos(), targetPos);

                if (tile1DistToTarget < tile2DistToTarget)
                {
                    transform.localRotation = leftDirection;
                }
                else
                {
                    transform.localRotation = downDirection;
                }
            }
        }

        // DECISION BLOCK - RIGHT UP DOWN ╞ 
        else if (nextBlockType == BlockType.rightUpDown)
        {
            // Coming Up ╞ 
            if (transform.localRotation == upDirection)
            {
                //print("Coming up");
                // Get next block in a direction
                float tile1DistToTarget = Vector3.Distance(GetRightBlockPos(), targetPos);
                float tile2DistToTarget = Vector3.Distance(GetForwardBlockPos(), targetPos);

                if (tile1DistToTarget < tile2DistToTarget)
                {
                    transform.localRotation = rightDirection;
                }
                else
                {
                    transform.localRotation = upDirection;
                }
            }
            // Coming down ╞
            else if (transform.localRotation == downDirection)
            {
                //print("Coming down");
                // Get next block in a direction
                float tile1DistToTarget = Vector3.Distance(GetLeftBlockPos(), targetPos);
                float tile2DistToTarget = Vector3.Distance(GetForwardBlockPos(), targetPos);
                //print("Coming down");

                if (tile1DistToTarget < tile2DistToTarget)
                {
                    //print("Coming down");
                    transform.localRotation = rightDirection;
                }
                else
                {
                    transform.localRotation = downDirection;
                }
            }
            // Coming left ╞
            else if (transform.localRotation == leftDirection)
            {
                // Get next block
                //print("Coming left");
                float tile1DistToTarget = Vector3.Distance(GetLeftBlockPos(), targetPos);
                float tile2DistToTarget = Vector3.Distance(GetRightBlockPos(), targetPos);

                if (tile1DistToTarget < tile2DistToTarget)
                {
                    transform.localRotation = downDirection;
                }
                else
                {
                    transform.localRotation = upDirection;
                }
            }
        }

        // DECISION BLOCK - LEFT RIGHT UP ╨
        else if (nextBlockType == BlockType.leftRightUp)
        {
            // Coming down ╨
            if (transform.localRotation == downDirection)
            {
                //print("Coming down");
                // Get next block in a direction
                float tile1DistToTarget = Vector3.Distance(GetLeftBlockPos(), targetPos);
                float tile2DistToTarget = Vector3.Distance(GetRightBlockPos(), targetPos);

                if (tile1DistToTarget < tile2DistToTarget)
                {
                    transform.localRotation = rightDirection;
                }
                else
                {
                    transform.localRotation = leftDirection;
                }
            }
            // Coming down ╨
            else if (transform.localRotation == leftDirection)
            {
                //print("Coming left");
                // Get next block in a direction
                float tile1DistToTarget = Vector3.Distance(GetRightBlockPos(), targetPos);
                float tile2DistToTarget = Vector3.Distance(GetForwardBlockPos(), targetPos);

                if (tile1DistToTarget < tile2DistToTarget)
                {
                    transform.localRotation = upDirection;
                }
                else
                {
                    transform.localRotation = leftDirection;
                }
            }
            // Coming right ╨
            else if (transform.localRotation == rightDirection)
            {
                //print("Coming right");
                // Get next block in a direction
                float tile1DistToTarget = Vector3.Distance(GetLeftBlockPos(), targetPos);
                float tile2DistToTarget = Vector3.Distance(GetForwardBlockPos(), targetPos);

                if (tile1DistToTarget < tile2DistToTarget)
                {
                    transform.localRotation = upDirection;
                }
                else
                {
                    transform.localRotation = rightDirection;
                }
            }

        }

        // DECISION BLOCK - CROSS ╬
        else if (nextBlockType == BlockType.cross)
        {
            //print("=>>>>>> CROSS ");
            // Coming down ╬
            if (transform.localRotation == downDirection)
            {

                // Get next block in a direction
                float tile1DistToTarget = Vector3.Distance(GetLeftBlockPos(), targetPos);
                float tile2DistToTarget = Vector3.Distance(GetRightBlockPos(), targetPos);
                float tile3DistToTarget = Vector3.Distance(GetForwardBlockPos(), targetPos);

                float min = Mathf.Min(Mathf.Min(tile1DistToTarget, tile2DistToTarget), tile3DistToTarget);

                if (tile1DistToTarget == min)
                {
                    transform.localRotation = rightDirection;
                }
                else if (tile2DistToTarget == min)
                {
                    transform.localRotation = leftDirection;
                }
                else if (tile3DistToTarget == min)
                {
                    transform.localRotation = downDirection;
                }
            }
            // Coming up ╬
            else if (transform.localRotation == upDirection)
            {
                //print("Coming up");
                // Get next block in a direction
                float tile1DistToTarget = Vector3.Distance(GetRightBlockPos(), targetPos);
                float tile2DistToTarget = Vector3.Distance(GetForwardBlockPos(), targetPos);
                float tile3DistToTarget = Vector3.Distance(GetLeftBlockPos(), targetPos);

                float min = Mathf.Min(Mathf.Min(tile1DistToTarget, tile2DistToTarget), tile3DistToTarget);

                if (tile1DistToTarget == min)
                {
                    transform.localRotation = rightDirection;
                }
                else if (tile2DistToTarget == min)
                {
                    transform.localRotation = upDirection;
                }
                else if (tile3DistToTarget == min)
                {
                    transform.localRotation = leftDirection;
                }
            }
            // Coming left ╬
            else if (transform.localRotation == leftDirection)
            {
                // print("Coming left");
                // Get next block in a direction
                float tile1DistToTarget = Vector3.Distance(GetRightBlockPos(), targetPos);
                float tile2DistToTarget = Vector3.Distance(GetForwardBlockPos(), targetPos);
                float tile3DistToTarget = Vector3.Distance(GetLeftBlockPos(), targetPos);

                float min = Mathf.Min(Mathf.Min(tile1DistToTarget, tile2DistToTarget), tile3DistToTarget);

                if (tile1DistToTarget == min)
                {
                    transform.localRotation = upDirection;
                }
                else if (tile2DistToTarget == min)
                {
                    transform.localRotation = leftDirection;
                }
                else if (tile3DistToTarget == min)
                {
                    transform.localRotation = downDirection;
                }
            }
            // Coming right ╬
            else if (transform.localRotation == rightDirection)
            {
                //print("Coming right");
                // Get next block in a direction
                float tile1DistToTarget = Vector3.Distance(GetRightBlockPos(), targetPos);
                float tile2DistToTarget = Vector3.Distance(GetForwardBlockPos(), targetPos);
                float tile3DistToTarget = Vector3.Distance(GetLeftBlockPos(), targetPos);

                float min = Mathf.Min(Mathf.Min(tile1DistToTarget, tile2DistToTarget), tile3DistToTarget);

                if (tile1DistToTarget == min)
                {
                    transform.localRotation = downDirection;
                }
                else if (tile2DistToTarget == min)
                {
                    transform.localRotation = rightDirection;
                }
                else if (tile3DistToTarget == min)
                {
                    transform.localRotation = upDirection;
                }
            }

        }
    }

    BlockType GetNextBlockType(GameObject tile)
    {
        //string tileTag = tile.transform.tag;
        string blockName = tile.transform.name;

        switch (blockName)
        {
            // DECISION BLOCKS ///////////////////////////////////////
            case "LeftUpDown":
                return BlockType.leftUpDown;
                break;
            case "RightUpDown":
                return BlockType.rightUpDown;
                break;
            case "LeftRightDown":
                return BlockType.leftRightDown;
                break;
            case "LeftRightUp":
                return BlockType.leftRightUp;
                break;
            case "Cross":
                return BlockType.cross;
                break;
            // DECISION BLOCKS ///////////////////////////////////////
            case "DownLeft":
                return BlockType.downLeft;
                break;
            case "UpRight":
                return BlockType.upRight;
                break;
            case "UpLeft":
                return BlockType.upLeft;
                break;
            case "DownRight":
                return BlockType.downRight;
                break;
            case "NA":
                return BlockType.none;
                break;
            default:
                // print("ERROR GetTileType");
                return BlockType.none;
                break;
        }

    }

    private void MoveForward()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void Move(GameObject nextTile)
    {


        if (Mathf.Abs(Vector3.Distance(transform.position, nextTile.transform.position)) >= 0.001f)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(
                transform.position,
                nextTile.transform.position,
                step);
        }
        else
        {
            canMove = false;
            //hasNextTile = false;
        }

    }
    GameObject GetForwardBlock()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, rayCastLength))
        {
            return (hit.transform.gameObject);
        }
        return null;
    }
    private Vector3 GetForwardBlockPos()
    {
        //RaycastHit hit;
        //Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.yellow, 3f);

        //GameObject go = Instantiate(testBlock, transform.position + transform.forward, Quaternion.identity);
        //Vector3 newPos = transform.position + transform.forward;
        //Debug.Log(" forward pos = " + newPos + " " + go.transform.position);

        return (transform.position + transform.forward);

        //if (Physics.Raycast(ray, out hit, rayCastLength))
        //{
        //    return (hit.transform.position);
        //}
        //// print("null");
        //return Vector3.zero;
    }
    private Vector3 GetLeftBlockPos()
    {
        //RaycastHit hit;
        //Ray ray = new Ray(transform.position, -transform.right);
        Debug.DrawRay(transform.position, -transform.right, Color.cyan, 3f);

        return (transform.position + -transform.right);

        //if (Physics.Raycast(ray, out hit, rayCastLength))
        //{
        //    return (hit.transform.position);
        //}
        //// print("null");
        //return Vector3.zero;
    }
    private Vector3 GetRightBlockPos()
    {
        //RaycastHit hit;
        //Ray ray = new Ray(transform.position, transform.right);
        Debug.DrawRay(transform.position, transform.right, Color.red, 3f);
        return (transform.position + transform.right);
        //if (Physics.Raycast(ray, out hit, rayCastLength))
        //{
        //    return (hit.transform.position);
        //}
        //// print("null");
        //return Vector3.zero;
    }
    public void ToggleGhostMode()
    {
        isChasemode = !isChasemode;
        if (isChasemode)
        {
            toggleText.text = "Chase Mode";
        }
        else
        {
            toggleText.text = "Scatter Mode";
        }
        //print("Toggle Chase Mode = " + isChasemode + " for " + transform.name);
    }
    private void OnDrawGizmos()
    {
    }
}
