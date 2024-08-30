using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeMovement : MonoBehaviour
{
    private Vector3 previousPosition;
    public Vector3 mouseOrigin;
    private string selected_tag = "";

    float x = 0;
    float y = 0;
    float z = 0;

    bool change = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            previousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            mouseOrigin = Input.mousePosition;
            selected_tag = Trajectory_textfield.selected_tag;
        }

        if (Input.GetMouseButton(0) && Trajectory_textfield.molecule_movement == true && selected_tag != "")
        {
            Vector3 newPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically
            float rotationAroundZAxis = direction.z * 180; // camera moves vertically

            Trajectory_textfield.prototypeMolecules[int.Parse(selected_tag)].rotate_atomsv2(new Vector3(rotationAroundXAxis, rotationAroundYAxis, rotationAroundZAxis));

            /*
            //transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            //transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);
            Quaternion q = Quaternion.Euler(rotationAroundXAxis, rotationAroundYAxis, rotationAroundZAxis);
            Trajectory_textfield.prototypeMolecules[int.Parse(selected_tag)].rotate_atoms(q);
            */

            previousPosition = newPosition;
        }
        else if (Input.GetMouseButton(1) && Trajectory_textfield.molecule_movement == true && selected_tag != "")
        {
            print("reached inside moleculemovement tranlsation statment");
            Vector3 newPosition = Camera.main.ScreenToViewportPoint(mouseOrigin - Input.mousePosition);
            Vector3 move = new Vector3(newPosition.x * 1.2f, newPosition.y * 1.2f, 0);

            move = Vector3.ProjectOnPlane(move, transform.forward);
            //move = Vector3.ProjectOnPlane(move, transform.position - Trajectory_textfield.prototypeMolecules[int.Parse(selected_tag)].centroid);

            //GameObject.Find("UI").GetComponent<Trajectory_textfield>().prototypeMolecules[int.Parse(selected_tag)].translate_atoms(-move);

            Trajectory_textfield.prototypeMolecules[int.Parse(selected_tag)].translate_atomsv3(-move);
        }

        /*
        if (Input.GetKeyDown(KeyCode.X))
        {
            x++;
            change = true;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            y++;
            change = true;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            z++;
            change = true;
        }

        if(change &&   Trajectory_textfield.molecule_movement == true && selected_tag != "")
        {
            Quaternion temp = Quaternion.Euler(x, y, z);
            Trajectory_textfield.prototypeMolecules[int.Parse(selected_tag)].rotate_atoms(temp);
            //Vector3 center = GameObject.Find("UI").GetComponent<Trajectory_textfield>().prototypeMolecules[int.Parse(selected_tag)].centroid;
            //Quaternion x_rot = Quaternion.AngleAxis(x, new Vector3(center[0] + 1, center[1], center[2]) - center);
            //Quaternion y_rot = Quaternion.AngleAxis(y, new Vector3(center[0], center[1] + 1, center[2]) - center);
            //Quaternion z_rot = Quaternion.AngleAxis(z, new Vector3(center[0], center[1], center[2] + 1) - center);
            //GameObject.Find("UI").GetComponent<Trajectory_textfield>().prototypeMolecules[int.Parse(selected_tag)].rotate_atoms(x_rot * y_rot * z_rot);

            change = false;
        }
        */

    }

}
