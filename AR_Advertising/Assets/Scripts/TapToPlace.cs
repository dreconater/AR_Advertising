
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlace : MonoBehaviour
{
    [SerializeField] private GameObject prefabObject;

    private GameObject spawnedObject;
    private Vector2 touchPosition;
    public ARRaycastManager _arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                touchPosition = touch.position;

            if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                if (spawnedObject == null)
                    spawnedObject = Instantiate(prefabObject, hitPose.position, hitPose.rotation);
                else
                    spawnedObject.transform.position = hitPose.position;
            }
        }
    }
}