using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    Quaternion upDirection = Quaternion.Euler(0, 0, 0);
    Quaternion downDirection = Quaternion.Euler(0, -180, 0);
    Quaternion leftDirection = Quaternion.Euler(0, -90, 0);
    Quaternion rightDirection = Quaternion.Euler(0, 90, 0);

    public PinkyForecastPosition pinkyForecastPosition;
    public InkyForecastPosition inkyForecastPosition;

    public Transform pinkyForecastMarker;
    public Transform inkyForecastMarker;
    public GameManager gameManager;

    float dist = 1f;

    GameObject nextBlock;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation = upDirection;
            nextBlock = GetForwardBlock();
            if (nextBlock != null)
            {
                float zPos = transform.position.z;
                zPos = zPos + 1f;
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    zPos
                    );
                pinkyForecastMarker.position = transform.position;
                pinkyForecastMarker.rotation = transform.rotation;
                inkyForecastMarker.position = transform.position;
                inkyForecastMarker.rotation = transform.rotation;
                gameManager.SetPinkyTarget(pinkyForecastPosition.GetPinkyTargetBlock(nextBlock));
                gameManager.SetInkyTarget(inkyForecastPosition.GetInkyTargetBlock(nextBlock));
            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation = downDirection;
            float zPos = transform.position.z;
            zPos = zPos - 1f;
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                zPos
                );
            pinkyForecastMarker.position = transform.position;
            pinkyForecastMarker.rotation = transform.rotation;
            inkyForecastMarker.position = transform.position;
            inkyForecastMarker.rotation = transform.rotation;
            gameManager.SetPinkyTarget(pinkyForecastPosition.GetPinkyTargetBlock(nextBlock));
            gameManager.SetInkyTarget(inkyForecastPosition.GetInkyTargetBlock(nextBlock));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            transform.rotation = leftDirection;
            nextBlock = GetForwardBlock();

            if (nextBlock != null)
            {
                float xPos = transform.position.x;
                xPos = xPos - 1f;
                transform.position = new Vector3(
                    xPos,
                    transform.position.y,
                    transform.position.z
                );
                pinkyForecastMarker.position = transform.position;
                pinkyForecastMarker.rotation = transform.rotation;
                inkyForecastMarker.position = transform.position;
                inkyForecastMarker.rotation = transform.rotation;
                gameManager.SetPinkyTarget(pinkyForecastPosition.GetPinkyTargetBlock(nextBlock));
                gameManager.SetInkyTarget(inkyForecastPosition.GetInkyTargetBlock(nextBlock));
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            transform.rotation = rightDirection;
            nextBlock = GetForwardBlock();

            if (nextBlock != null)
            {
                //print(nextBlock.name);
                float xPos = transform.position.x;
                xPos = xPos + 1f;
                transform.position = new Vector3(
                    xPos,
                    transform.position.y,
                    transform.position.z
                 );
                pinkyForecastMarker.position = transform.position;
                pinkyForecastMarker.rotation = transform.rotation;
                inkyForecastMarker.position = transform.position;
                inkyForecastMarker.rotation = transform.rotation;
                gameManager.SetPinkyTarget(pinkyForecastPosition.GetPinkyTargetBlock(nextBlock));
                gameManager.SetInkyTarget(inkyForecastPosition.GetInkyTargetBlock(nextBlock));
            }
        }

        //print(GetCurrentBlock().name);
    }

    GameObject GetForwardBlock()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward ) ;
        if (Physics.Raycast(ray, out hit, dist))
        {
            return (hit.transform.gameObject);
        }
        print("ERROR: Current block not found");
        return null;
    }
    GameObject GetCurrentBlock()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, (transform.forward + transform.right) / 2.8f);
        if (Physics.Raycast(ray, out hit, dist))
        {
            return (hit.transform.gameObject);
        }
        print("ERROR: Current block not found");
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward*2);

        //Gizmos.color = Color.red;
        //Gizmos.DrawRay(transform.position, (transform.forward + transform.right) / 2.8f);
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("other " + other.transform.tag);
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    print("other " + collision.transform.tag);
    //}

}
