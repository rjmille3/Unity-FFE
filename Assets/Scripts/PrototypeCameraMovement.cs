using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// https://emmaprats.com/p/how-to-rotate-the-camera-around-an-object-in-unity3d/
// https://gist.github.com/JISyed/5017805

public class PrototypeCameraMovement : MonoBehaviour
{
    private Vector3 previousPosition;
    public Vector3 mouseOrigin;
    public Space camera_space = Space.Self;
    Camera cam;

    public void Start()
    {
        cam = this.GetComponent<Camera>();
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject()){
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                previousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                mouseOrigin = Input.mousePosition;
            }

            if (Input.GetMouseButton(0) && Trajectory_textfield.molecule_movement == false)
            {
                Vector3 newPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Vector3 direction = previousPosition - newPosition;

                float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
                float rotationAroundXAxis = direction.y * 180; // camera moves vertically

                transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
                transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);
                previousPosition = newPosition;
            }
            else if (Input.GetMouseButton(1) && Trajectory_textfield.molecule_movement == false)
            {
                Vector3 newPosition = Camera.main.ScreenToViewportPoint(mouseOrigin - Input.mousePosition);

                Vector3 move = new Vector3(newPosition.x * 1.2f, newPosition.y * 1.2f, 0);
                transform.Translate(move, Space.Self);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") != 0f) //scrolling backward  
            {

                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
                Vector3 move = 3.0f * -1 * Input.GetAxis("Mouse ScrollWheel") * transform.forward;
                transform.Translate(move, Space.World);

                cam.orthographicSize += Input.GetAxis("Mouse ScrollWheel");
            }

            if (Input.GetKeyDown(KeyCode.Space) && Trajectory_textfield.prototypeMolecules.Count != 0)
            {
                transform.position = new Vector3(0, 0, 0);
                Vector3 displacement = Trajectory_textfield.prototypeMolecules[0].centroid - transform.position;
                Quaternion rotation = Quaternion.LookRotation(displacement.normalized);
                transform.rotation = rotation;
            }
        }
    }
}
