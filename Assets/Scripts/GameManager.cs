using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Reference variables
    public Ghost pinky;
    public Ghost blinky;
    public Ghost inky;
    public Ghost clyde;
    // Targets
    public GameObject pinkyTarget;
    public GameObject inkyTarget;


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

    public void SetPinkyTarget(GameObject target)
    {
        pinkyTarget = target;
        print("pinky target = " + pinkyTarget);
    }
    public GameObject GetPinkyTarget()
    {
        return pinkyTarget;
    }
    public void SetInkyTarget(GameObject target)
    {
        inkyTarget = target;
        print("inky target = " + inkyTarget);
    }
    public GameObject GetInkyTarget()
    {
        return inkyTarget;
    }

}
