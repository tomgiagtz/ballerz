using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Script following Brackeys tutorial : https://www.youtube.com/watch?v=aLpixrPvlB8
// refactored and commented, changed GetDistance to use greatest x or z distance
[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{

    //list of targets (players) tranforms
    public List<Transform> targets;

    //camera offset for distance
    public Vector3 offset;

    // camera movement smoothing time
    [Range (0, 2)] public float smoothTime = 0.5f;

    //min Zoom value for FOV on camera
    public float minZoom = 40f;
    //max Zoom value for FOV on camera

    public float maxZoom = 10f;
    // normalizes greatest distance, set to furthest targets can get from eachother
    public float zoomLimiter = 40f;

    // var reference for Smooth Dampening
    private Vector3 cameraVelocity;
    private Camera cam;

    private void Start() {
        cam = GetComponent<Camera>();
    }

    //use late update so it performs movements after players move
    void LateUpdate()
    {   
        //if no targets, return
        if (targets.Count == 0) return;

        MoveCamera();
        ZoomCamera();
    }
    //move camera controller
    void MoveCamera() {
        //get center point
        Vector3 centerPoint = GetCenterPoint();

        //use add offset to center point
        Vector3 offsetPositon = centerPoint + offset;

        //set final camera position using smooth dampening
        transform.position = Vector3.SmoothDamp(transform.position, offsetPositon, ref cameraVelocity, smoothTime);
    }


    //zoom camera controller
    void ZoomCamera() {
        // set newZoom to value between min and max, based on the greatest didstance, normalized by the zoom limiter
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);

        //set FOV, smoothed using lerp between current and new zoom
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }


    // returns bounding box around all targets from list
    Bounds GetBounds() {
        //create a bounding box around all targets
        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++) {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds;
    }

    //uses unity bounds to find centerpoint of all targets
    Vector3 GetCenterPoint(){
        //trivial case for 1 target
        if (targets.Count == 1) {
            return targets[0].position;
        }

        //return center of all targets
        return GetBounds().center;
    }


    //return greatest distance from target positions
    float GetGreatestDistance() {
        Bounds bounds = GetBounds();
        float maxX = bounds.size.x;
        float maxZ = bounds.size.z;
        //check return greater of maxX or maxZ
        if (maxX >= maxZ) {
            return maxX;
        } else {
            return maxZ;
        }
    }

}