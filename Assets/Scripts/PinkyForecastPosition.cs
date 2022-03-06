using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PinkyForecastPosition : MonoBehaviour
{
    bool hasNextBlock = false;
    bool canMove = false;
    GameObject nextBlock;

    float dist = 1f;

    enum Direction
    {
        up,
        down,
        left,
        right
    }
    Direction direction = Direction.right;

    Quaternion upDirection = Quaternion.Euler(0, 0, 0);
    Quaternion downDirection = Quaternion.Euler(0, -180, 0);
    Quaternion leftDirection = Quaternion.Euler(0, -90, 0);
    Quaternion rightDirection = Quaternion.Euler(0, 90, 0);

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
        //GameObject block4 = GetFourBlocksAhead();
        //Debug.Log("Name = " + block4.name);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public GameObject GetPinkyTargetBlock(GameObject currentBlock)
    {

        if (GetForwardBlock() == null)
        {
            HandleRotationsBasedOnPlayerPosition(currentBlock);
        }

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
            MoveAlongPath(nextBlock);
            // Handle Rotation Based On Random
            HandleRotationsBasedOnPlayerPosition(nextBlock);
            currentBlock = nextBlock;
        }
        return nextBlock;
    }

    GameObject GetForwardBlock()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, dist))
        {
            return (hit.transform.gameObject);
        }
        // player facing off screen so no forward block
        return null;
    }

    void HandleRotationsBasedOnPlayerPosition(GameObject nextBlock)
    {

        //Quaternion rotation = transform.rotation;
        //Quaternion rotation = transform.parent.localRotation;
        Quaternion rotation = transform.rotation;

        nextBlockType = GetBlockType(nextBlock);
        if (nextBlockType == null)
        {
            print("ERROR");
        }

        // STANDARD BLOCKS
        if (nextBlockType == BlockType.upRight)
        {
            if (transform.rotation == leftDirection)
            {
                transform.rotation = upDirection;
            }
            else if (transform.rotation == downDirection)
            {
                transform.rotation = rightDirection;
            }
        }

        if (nextBlockType == BlockType.upLeft)
        {
            if (transform.rotation == rightDirection)
            {
                transform.rotation = upDirection;
            }
            else if (transform.rotation == downDirection)
            {
                transform.rotation = leftDirection;
            }
        }

        if (nextBlockType == BlockType.downLeft)
        {
            if (transform.rotation == rightDirection)
            {
                transform.rotation = downDirection;
            }
            if (transform.rotation == upDirection)
            {
                transform.rotation = leftDirection;
            }
        }

        if (nextBlockType == BlockType.downRight)
        {
            if (transform.rotation == leftDirection)
            {
                transform.rotation = downDirection;
            }
            if (transform.rotation == upDirection)
            {
                transform.rotation = rightDirection;
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
            // FROM RIGHT ┤
            if (transform.rotation == rightDirection)
            {
                if (Random.Range(1, 10) < 5)
                {
                    transform.rotation = upDirection;
                }
                else
                {
                    transform.rotation = downDirection;
                }
            }
            // COMING UP ┤
            else if (transform.rotation == upDirection)
            {
                if (Random.Range(1, 10) < 5)
                {
                    transform.rotation = upDirection;
                }
                else
                {
                    transform.rotation = leftDirection;
                }
            }
            // COMING DOWN ┤
            else if (transform.rotation == downDirection)
            {
                if (Random.Range(1, 10) < 5)
                {
                    transform.rotation = downDirection;
                }
                else
                {
                    transform.rotation = leftDirection;
                }
            }
        }

        // DECISION BLOCK - RIGHT LEFT DOWN ╤
        else if (nextBlockType == BlockType.leftRightDown)
        {
            // COMING RIGHT ╤
            if (transform.rotation == rightDirection)
            {
                if (Random.Range(1, 10) < 5)
                {
                    transform.rotation = rightDirection;
                }
                else
                {
                    transform.rotation = downDirection;
                }
            }
            // COMING UP ╤
            else if (transform.rotation == upDirection)
            {
                if (Random.Range(1, 10) < 5)
                {
                    transform.rotation = leftDirection;
                }
                else
                {
                    transform.rotation = rightDirection;
                }
            }
            // COMING LEFT ╤
            else if (transform.rotation == leftDirection)
            {
                if (Random.Range(1, 10) < 5)
                {
                    transform.rotation = leftDirection;
                }
                else
                {
                    transform.rotation = downDirection;
                }
            }
        }

        // DECISION BLOCK - RIGHT UP DOWN ╞ 
        else if (nextBlockType == BlockType.rightUpDown)
        {
            // Coming Up ╞ 
            if (transform.rotation == rightDirection)
            {
                if (Random.Range(1, 10) < 5)
                {
                    transform.rotation = rightDirection;
                }
                else
                {
                    transform.rotation = upDirection;
                }
            }
            // Coming down ╞
            else if (transform.rotation == downDirection)
            {
                if (Random.Range(1, 10) < 5)
                {
                    transform.rotation = rightDirection;
                }
                else
                {
                    transform.rotation = downDirection;
                }
            }
            // Coming left ╞
            else if (transform.rotation == leftDirection)
            {
                if (Random.Range(1, 10) < 5)
                {
                    transform.rotation = downDirection;
                }
                else
                {
                    transform.rotation = upDirection;
                }
            }
        }

        // DECISION BLOCK - LEFT RIGHT UP ╨
        else if (nextBlockType == BlockType.leftRightUp)
        {
            // Coming down ╨
            if (transform.rotation == downDirection)
            {
                if (Random.Range(1, 10) < 5)
                {
                    transform.rotation = rightDirection;
                }
                else
                {
                    transform.rotation = leftDirection;
                }
            }
            // Coming down ╨
            else if (transform.rotation == leftDirection)
            {
                if (Random.Range(1, 10) < 5)
                {
                    transform.rotation = leftDirection;
                }
                else
                {
                    transform.rotation = upDirection;
                }
            }
            // Coming right ╨
            else if (transform.rotation == rightDirection)
            {
                if (Random.Range(1, 10) < 5)
                {
                    transform.rotation = rightDirection;
                }
                else
                {
                    transform.rotation = upDirection;
                }
            }

        }

        // DECISION BLOCK - CROSS ╬
        else if (nextBlockType == BlockType.cross)
        {
            // Coming down ╬
            if (transform.rotation == downDirection)
            {
                if (Random.Range(1, 10) < 5)
                {
                    transform.rotation = rightDirection;
                }
                else
                {
                    transform.rotation = leftDirection;
                }
            }
            // Coming up ╬
            else if (transform.rotation == upDirection)
            {
                if (Random.Range(1, 10) < 5)
                {
                    transform.rotation = rightDirection;
                }
                else
                {
                    transform.rotation = leftDirection;
                }
            }
            // Coming left ╬
            else if (transform.rotation == leftDirection)
            {
                if (Random.Range(1, 10) < 5)
                {
                    transform.rotation = upDirection;
                }
                else
                {
                    transform.rotation = downDirection;
                }
            }
            // Coming right ╬
            else if (transform.rotation == rightDirection)
            {
                if (Random.Range(1, 10) < 5)
                {
                    transform.rotation = upDirection;
                }
                else
                {
                    transform.rotation = downDirection;
                }
            }

        }
    }

    private void MoveAlongPath(GameObject nextBlock)
    {
        Debug.DrawLine(transform.position, nextBlock.transform.position, Color.white, 2f);
        transform.position = nextBlock.transform.position;

    }



    BlockType GetBlockType(GameObject tile)
    {
        string tileTag = tile.transform.tag;

        switch (tileTag)
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
                print("ERROR GetTileType");
                return BlockType.none;
                break;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward);

    }

}


