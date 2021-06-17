using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlace : MonoBehaviour
{
    public GameObject robotPrefab;

    private ARRaycastManager raycastManager;
    private GameObject spawnedRobot;

    void Start()
    {
        // Grab the AR raycast manager
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // If we haven't already got a robot
        if(spawnedRobot == null)
        {
            // If the screen was touched
            if(Input.touchCount > 0)
            {
                // Get the position of the touch on the screen
                Vector2 touchPosition = Input.GetTouch(0).position;

                // Raycast into the world using the touch position
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if(raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
                {
                    // Get the closest plane touched
                    Pose hitPose = hits[0].pose;

                    // Spawn our robot at touched point on the plane
                    spawnedRobot = Instantiate(robotPrefab, hitPose.position, hitPose.rotation);
                }
            }
        }
    }
}
