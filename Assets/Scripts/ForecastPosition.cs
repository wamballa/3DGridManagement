using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForecastPosition : MonoBehaviour
{

    public enum Direction
    {
        up,
        down,
        left,
        right
    }
    public Direction direction = Direction.right;

    public Quaternion upDirection = Quaternion.Euler(0, 0, 0);
    public Quaternion downDirection = Quaternion.Euler(0, -180, 0);
    public Quaternion leftDirection = Quaternion.Euler(0, -90, 0);
    public Quaternion rightDirection = Quaternion.Euler(0, 90, 0);

    public enum BlockType
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
    public BlockType nextBlockName;

    float dist = 1f;

    public GameObject GetForwardBlock()
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

    public Vector3 GetForwardPos()
    {
        Vector3 newPos = transform.position + transform.forward;
        return newPos;
    }

    BlockType GetBlockName(GameObject tile)
    {
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
                //print("ERROR GetTileType");
                return BlockType.none;
                break;
        }

    }

    public void HandleRotationsBasedOnPlayerPosition(GameObject nextBlock)
    {

        //Quaternion rotation = transform.rotation;
        //Quaternion rotation = transform.parent.localRotation;
        Quaternion rotation = transform.rotation;

        nextBlockName = GetBlockName(nextBlock);

        if (nextBlockName == null)
        {
            print("ERROR");
            return;
        }

        // STANDARD BLOCKS
        if (nextBlockName == BlockType.upRight)
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

        if (nextBlockName == BlockType.upLeft)
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

        if (nextBlockName == BlockType.downLeft)
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

        if (nextBlockName == BlockType.downRight)
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
        if (nextBlockName == BlockType.leftUpDown)
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
        else if (nextBlockName == BlockType.leftRightDown)
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
        else if (nextBlockName == BlockType.rightUpDown)
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
        else if (nextBlockName == BlockType.leftRightUp)
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
        else if (nextBlockName == BlockType.cross)
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

    public Vector3 MoveAlongPath()
    {
        Vector3 newPos = transform.position + transform.forward;
        Debug.DrawLine(transform.position, newPos, Color.white, 2f);
        transform.position = newPos;
        return newPos;
    }

    private void MoveAlongPath(GameObject nextBlock)
    {
        Debug.DrawLine(transform.position, nextBlock.transform.position, Color.white, 2f);
        transform.position = nextBlock.transform.position;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
