using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://emmaprats.com/p/how-to-rotate-the-camera-around-an-object-in-unity3d/

public class CameraMovement : MonoBehaviour
{/*
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget = 25;

    private Vector3 previousPosition;

    void Start()
    {
        //cam.transform.position = target.transform.position;
    }

    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        //     //print("entered if camera movements statement");
        // }
        // else if (Input.GetMouseButton(0))
        // {
        //     Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        //     Vector3 direction = previousPosition - newPosition;

        //     float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
        //     float rotationAroundXAxis = direction.y * 180; // camera moves vertically


        //     //Vector3 center_to_cam = Vector3.Normalize(cam.transform.position - target.transform.position);

        //     //float something = cam.transform.position[2] - target.transform.position[2];
        //     //distanceToTarget = something;

        //     //cam.transform.position = target.transform.position;

        //     cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
        //     cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!

        //     //cam.transform.Translate(new Vector3(target.transform.position[0], target.transform.position[1], -distanceToTarget));
        //     //cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));
        //     //cam.transform.Translate(center_to_cam * -distanceToTarget);


        //     previousPosition = newPosition;
        // }

        // if (Input.GetMouseButtonDown(1))
        // {
        //     previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        //     print("entered if statement for right click");
        // }
        // else if (Input.GetMouseButton(1))
        // {
        //     Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        //     Vector3 direction = newPosition - previousPosition;

        //     //cam.transform.position = direction;
        //     //cam.transform.Translate(new Vector3(direction[0], direction[1], -distanceToTarget));

        //     //cam.transform.position = new Vector3(cam.transform.position[0] + direction[0], cam.transform.position[0] + direction[1], -distanceToTarget);
        //     //cam.transform.Translate(new Vector3(cam.transform.position[0] + direction[0], cam.transform.position[0] + direction[1], -distanceToTarget));
        //     cam.transform.Translate(-direction*10);
        //     //target.transform.position = cam.transform.position;
        //     target.transform.Translate(-direction*10);

        //     previousPosition = newPosition;
        // }

        // 
        // if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetAxis("Mouse ScrollWheel") < 0f)
        // {
        //     float step = 1000 * Time.deltaTime;
        //     //Vector3 Scrolldirection = Vector3.Normalize(cam.transform.position - cam.ScreenToViewportPoint(Input.mousePosition));
        //     cam.transform.position = Vector3.MoveTowards(cam.transform.position, Input.mousePosition, Input.GetAxis("Mouse ScrollWheel") * step);
        //     print("entered scroll if statement");
        // }
        // 
        
        // if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        // {
        //     //Vector3 origin = new Vector3(0,0,0);
        //     //Vector3 direction = Vector3.Normalize(cam.transform.position - origin);
        //     //Vector3 direction = Vector3.Normalize(cam.transform.position - cam.ScreenToViewportPoint(Input.mousePosition));
        //     Vector3 direction = Vector3.Normalize(cam.transform.position - Parse_inputfile.centroid);
        //     cam.transform.Translate(direction * Input.GetAxis("Mouse ScrollWheel") * 10);
        //     //print("scolling up");
        // }
        // else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        // {
        //     //Vector3 origin = new Vector3(0, 0, 0);
        //     //Vector3 direction = Vector3.Normalize(cam.transform.position - origin);
        //     //Vector3 direction = Vector3.Normalize(cam.transform.position - cam.ScreenToViewportPoint(Input.mousePosition));
        //     Vector3 direction = Vector3.Normalize(cam.transform.position - Parse_inputfile.centroid);
        //     cam.transform.Translate(direction * Input.GetAxis("Mouse ScrollWheel") * 10);
        //     //print("scolling down");
        // }
        



    }*/
}
