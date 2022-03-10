using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://gameinternals.com/understanding-pac-man-ghost-behavior#:~:text=The%20purpose%20of%20the%20game%20is%20very%20simple,four%20ghosts%20that%20pursue%20Pac-Man%20through%20the%20maze.

public class GameManager : MonoBehaviour
{
    // Reference variables
    public Ghost pinky;
    public Ghost blinky;
    public Ghost inky;
    public Ghost clyde;
    // Targets
    [HideInInspector] public Vector3 pinkyTargetPos;
    [HideInInspector] public Vector3 inkyTargetPos;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            if (pinky != null) pinky.ToggleGhostMode();
            if (inky != null) inky.ToggleGhostMode();
            if (blinky != null) blinky.ToggleGhostMode();
            if (clyde != null) clyde.ToggleGhostMode();
        }
    }

    public void SetPinkyTarget(Vector3 targetPos)
    {
        pinkyTargetPos = targetPos;
        //print("pinky target = " + pinkyTarget);
    }
    public Vector3 GetPinkyTarget()
    {
        return pinkyTargetPos;
    }
    public void SetInkyTarget(Vector3 targetPos)
    {
        inkyTargetPos = targetPos;
        //print("inky target = " + inkyTarget);
    }
    public Vector3 GetInkyTarget()
    {
        return inkyTargetPos;
    }

}
