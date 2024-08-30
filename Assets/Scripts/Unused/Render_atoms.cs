using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Render_atoms : MonoBehaviour
{
/*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void DrawLine(Vector3 start, Vector3 end, Color color, string curr_frame_number)
    {
        
        // GameObject myLine = new GameObject();
        // myLine.transform.position = start;
        // myLine.AddComponent<LineRenderer>();
        // LineRenderer lr = myLine.GetComponent<LineRenderer>();
        // lr.material = new Material(Shader.Find("Unlit/Texture"));
        // lr.SetColors(color, color);
        // lr.SetWidth(1f, 1f);
        // lr.SetPosition(0, start);
        // lr.SetPosition(1, end);
        // lr.tag = curr_frame_number;
    
        print(start);
        print(end);
        GameObject lineObj = new GameObject("DragLine", typeof(LineRenderer));
        LineRenderer line = lineObj.GetComponent<LineRenderer>();
        line.SetWidth(0.1f, 0.1f);
        line.SetColors(Color.white, Color.white);
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        line.material.color = color;
        lineObj.tag = curr_frame_number;

    }

    public static void render(string[,] atoms, string prev_frame_number, string curr_frame_number)
    {
        if(prev_frame_number!="")
        {
            foreach (GameObject o in GameObject.FindGameObjectsWithTag(prev_frame_number))
            {
                Destroy(o);
            }

        }

        Vector3 curr_pos = Vector3.zero;
        Vector3 target_pos = Vector3.zero;

        for (int i = 0; i < Parse_inputfile.num_atoms; i++)
        {
            //placing atoms
            float x_pos = float.Parse(atoms[i, 2]);
            float y_pos = float.Parse(atoms[i, 3]);
            float z_pos = float.Parse(atoms[i, 4]);
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector3(x_pos, y_pos, z_pos);
            sphere.transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
            sphere.tag = curr_frame_number;
            sphere.name = atoms[i, 0];

            //drawing bonds
            
            List<int> connectivities = Parse_inputfile.frame_connectivities[int.Parse(curr_frame_number)][i];
            print("connectivities for atom " + atoms[i,0] + ": " + string.Join(",", connectivities));
            for (int j=0; j<connectivities.Count; j++)
            {
                
                //only draw bond if the current atom # is less than the atom # of what it is being connected to
                if (int.Parse(atoms[i, 0]) < connectivities[j])
                {
                    curr_pos[0] = x_pos;
                    curr_pos[1] = y_pos;
                    curr_pos[2] = z_pos;
                    target_pos[0] = float.Parse(atoms[connectivities[j]-1, 2]);
                    target_pos[1] = float.Parse(atoms[connectivities[j]-1, 3]);
                    target_pos[2] = float.Parse(atoms[connectivities[j]-1, 4]);
                    DrawLine(curr_pos, target_pos, Color.red, curr_frame_number);
                }
                
            }
            
            
            
        }


    }

    public void Docking_DrawLine(Vector3 start, Vector3 end, Color color, string curr_molecule_number)
    {
        
        // GameObject myLine = new GameObject();
        // myLine.transform.position = start;
        // myLine.AddComponent<LineRenderer>();
        // LineRenderer lr = myLine.GetComponent<LineRenderer>();
        // lr.material = new Material(Shader.Find("Unlit/Texture"));
        // lr.SetColors(color, color);
        // lr.SetWidth(1f, 1f);
        // lr.SetPosition(0, start);
        // lr.SetPosition(1, end);
        // lr.tag = curr_frame_number;
        
        //print(start);
        //print(end);
        GameObject lineObj = new GameObject("DragLine", typeof(LineRenderer));
        LineRenderer line = lineObj.GetComponent<LineRenderer>();
        line.SetWidth(0.1f, 0.1f);
        line.SetColors(Color.white, Color.white);
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        line.material.color = color;
        lineObj.tag = curr_molecule_number;

    }

    public void Docking_render(Docking_Parser dp)
    {
        print("entered docking render");
        Vector3 curr_pos = Vector3.zero;
        Vector3 target_pos = Vector3.zero;

        print("render atoms numatoms " + dp.num_atoms);

        for (int i = 0; i < dp.num_atoms; i++)
        {
            //placing atoms
            float x_pos = float.Parse(dp.atoms[i, 2]);
            float y_pos = float.Parse(dp.atoms[i, 3]);
            float z_pos = float.Parse(dp.atoms[i, 4]);
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector3(x_pos, y_pos, z_pos);
            sphere.transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);

            //print("number of molecule structures" + Trajectory_textfield.molecules.Count.ToString());
            print(sphere.transform.name);
            print(sphere.tag);
            print(dp.tag_name);
            print(dp.num_atoms);
            sphere.tag = dp.tag_name;
            sphere.name = dp.tag_name + ": " + dp.atoms[i, 0];

            dp.test = sphere;

            //Adding rigidbodies and colliders to make the atoms collide with each other when moving them around in docking mode
            sphere.AddComponent<Rigidbody>();
            sphere.GetComponent<Rigidbody>().isKinematic = true;
            //shpere.AddComponent<SphereCollider>();

            //drawing bonds

            List<int> connectivities = dp.connectivities[i];
            //print("connectivities for atom " + dp.atoms[i, 0] + ": " + string.Join(",", connectivities));
            for (int j = 0; j < connectivities.Count; j++)
            {

                //only draw bond if the current atom # is less than the atom # of what it is being connected to
                if (int.Parse(dp.atoms[i, 0]) < connectivities[j])
                {
                    curr_pos[0] = x_pos;
                    curr_pos[1] = y_pos;
                    curr_pos[2] = z_pos;
                    target_pos[0] = float.Parse(dp.atoms[connectivities[j] - 1, 2]);
                    target_pos[1] = float.Parse(dp.atoms[connectivities[j] - 1, 3]);
                    target_pos[2] = float.Parse(dp.atoms[connectivities[j] - 1, 4]);
                    Docking_DrawLine(curr_pos, target_pos, Color.red, dp.tag_name);
                }

            }



        }


    }
*/

}
