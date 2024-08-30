using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;
public class PrototypeRenderer : MonoBehaviour
{
    //Define Dictionary to Color Atoms
    public IDictionary<int, Color> atom_colors = new Dictionary<int, Color>();

    double[] radii = { 1.20, 1.43, 2.12, 1.98, 1.91, 1.77,
                    1.66, 1.50, 1.46, 1.58, 2.50, 2.51,
                    2.25, 2.19, 1.90, 1.89, 1.82, 1.83,
                    2.73, 2.62, 2.58, 2.46, 2.42, 2.45,
                    2.45, 2.44, 2.40, 2.40, 2.38, 2.39,
                    2.32, 2.29, 1.88, 1.82, 1.86, 2.25,
                    3.21, 2.84, 2.75, 2.52, 2.56, 2.45,
                    2.44, 2.46, 2.44, 2.15, 2.53, 2.49,
                    2.43, 2.42, 2.47, 1.99, 2.04, 2.06,
                    3.48, 3.03, 2.98, 2.88, 2.92, 2.95,
                    0.00, 2.90, 2.87, 2.83, 2.79, 2.87,
                    2.81, 2.83, 2.79, 2.80, 2.74, 2.63,
                    2.53, 2.57, 2.49, 2.48, 2.41, 2.29,
                    2.32, 2.45, 2.47, 2.60, 2.54, 0.00,
                    0.00, 0.00, 0.00, 0.00, 2.80, 2.93,
                    2.88, 2.71, 2.82, 2.81, 2.83, 3.05,
                    3.40, 3.05, 2.70, 0.00, 0.00, 0.00,
                    0.00, 0.00, 0.00, 0.00, 0.00, 0.00,
                    0.00, 0.00, 0.00, 0.00 };

    void Awake()
    {
        /*
        atom_colors.Add("CT", Color.black);
        atom_colors.Add("C", Color.black);
        atom_colors.Add("HC", Color.white);
        atom_colors.Add("H", Color.white);
        atom_colors.Add("O", Color.red);
        */
        atom_colors.Add(1, Color.white);
        atom_colors.Add(6, Color.black);
        atom_colors.Add(7, Color.blue);
        atom_colors.Add(8, Color.red);

        //print("testing atom num " + lookup_atom(220));
    }

    //this method looks up an atom type and returns the atom's atomic number
    public int lookup_atom(int atomType, string param_path)
    {
        //System.IO.StreamReader file = new System.IO.StreamReader("Assets/Tinker_Files/params/amber94.prm");
        System.IO.StreamReader file = new System.IO.StreamReader(param_path);
        string line;
        while ((line = file.ReadLine()) != null)
        {
            if (line != "")
            {
                //split each line where occurences of 2 or more spaces appear concurrrently
                string[] s = Regex.Split(line, @"\s{2,}");
                //print(line);

                if(s[0] == "atom" && int.Parse(s[1]) == atomType)
                {
                    return(int.Parse(s[5]));
                }

                /*
                //s[0] appears to be blank? s[1] represents atom #
                atomNumbers[counter] = int.Parse(s[1]);
                atomNames[counter] = s[2];
                coordinates[counter, 0] = float.Parse(s[3]);
                coordinates[counter, 1] = float.Parse(s[4]);
                coordinates[counter, 2] = float.Parse(s[5]);
                atomTypes[counter] = int.Parse(s[6]);

                List<int> l = new List<int>();
                for (int j = 7; j < s.Length; j++)
                {
                    l.Add(int.Parse(s[j]));
                }
                connectivities.Add(l);

                counter++;
                */
            }
        }
        file.Close();
        return -1;
    }

    public void drawLine(Vector3 start, Vector3 end, Color color, string curr_molecule_number)
    {
        GameObject bondLine = new GameObject("BondLine", typeof(LineRenderer));
        LineRenderer line = bondLine.GetComponent<LineRenderer>();
        line.startWidth = 0.1f;
        line.startColor = Color.white;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        line.material.color = color;
        Trajectory_textfield.prototypeMolecules[int.Parse(curr_molecule_number)].atomsAndBonds.Add(bondLine);//.object.Add(bondLine);
    }

    public void drawSphere(float x, float y, float z, string tag, int atomNum, string atomName, int atomType, string param_path)
    {
        GameObject sphere = Instantiate(Resources.Load("atom_prefab"), new Vector3(x, y, z), Quaternion.identity) as GameObject;
        sphere.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        sphere.transform.localScale *= 0.5f;
        sphere.name = tag + ": " + atomNum;

        //print(atomName + " " + atomType + " " + lookup_atom(atomType, param_path) + " " + atomNum);

        //find radii for specific atomic number
        if (lookup_atom(atomType, param_path) !=-1 && radii[lookup_atom(atomType, param_path) - 1] != null)
        {
            float atomic_radii = (float)radii[lookup_atom(atomType, param_path) - 1];
            //print(atomName + " " + atomic_radii + " " + atomType + " " + lookup_atom(atomType) + " " + atomNum);
            sphere.transform.localScale *= atomic_radii;
        }

        if (atom_colors.ContainsKey(lookup_atom(atomType, param_path)))
        {
            sphere.GetComponent<Renderer>().material.color = atom_colors[lookup_atom(atomType, param_path)];
        }
        else
        {
            sphere.GetComponent<Renderer>().material.color = Color.green;
        }

        sphere.GetComponent<atom_overlap>().m2 = sphere.GetComponent<Renderer>().material;

        Trajectory_textfield.prototypeMolecules[int.Parse(tag)].atomsAndBonds.Add(sphere);//.object.Add(sphere);
    }

    //original method
    /*
    public void drawSphere(float x, float y, float z, string tag, int atomNum, string atomName)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(x, y, z);
        sphere.transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
        sphere.name = tag + ": " + atomNum;

        if (atom_colors.ContainsKey(atomName))
        {
            sphere.GetComponent<Renderer>().material.color = atom_colors[atomName];
        }
        else
        {
            sphere.GetComponent<Renderer>().material.color = Color.green;
        }
        //Adding rigidbodies and colliders to make the atoms collide with each other when moving them around in docking mode
        sphere.AddComponent<Rigidbody>();
        sphere.GetComponent<Rigidbody>().useGravity = false;
        sphere.GetComponent<Rigidbody>().isKinematic = true;
        //sphere.GetComponent<Rigidbody>().bodyType = Rigidbody.Static;
        sphere.AddComponent<SphereCollider>();
        //sphere.GetComponent<SphereCollider>().material.bounciness = 0;

        Trajectory_textfield.prototypeMolecules[int.Parse(tag)].atomsAndBonds.Add(sphere);//.object.Add(sphere);
    }
    */

    public void render(PrototypeParseInputFile parser)
    {
        Vector3 curr_pos = Vector3.zero;
        Vector3 target_pos = Vector3.zero;
        float x;
        float y;
        float z;
        int connectedAtomIndex;

        for (int i = 0; i < parser.num_atoms; i++)
        {
            x = parser.coordinates[i, 0];
            y = parser.coordinates[i, 1];
            z = parser.coordinates[i, 2];
            drawSphere(x, y, z, parser.tag_name, i, parser.atomNames[i], parser.atomTypes[i], parser.param_path);

            //drawing bonds
            List<int> connectivities = parser.connectivities[i];
            for (int j = 0; j < connectivities.Count; j++)
            {
                //only draw bond if the current atom # is less than the atom # of what it is being connected to
                if (parser.atomNumbers[i] < connectivities[j])
                {
                    curr_pos[0] = x;
                    curr_pos[1] = y;
                    curr_pos[2] = z;

                    connectedAtomIndex = connectivities[j] - 1;
                    target_pos[0] = parser.coordinates[connectedAtomIndex, 0];
                    target_pos[1] = parser.coordinates[connectedAtomIndex, 1];
                    target_pos[2] = parser.coordinates[connectedAtomIndex, 2];

                    drawLine(curr_pos, target_pos, Color.red, parser.tag_name);
                }
            }
        }
    }

    public void renderArc(PrototypeParseInputFile parser, string prev_frame_number, string curr_frame_number)
    {
        destroyPreviousFrameObjectsIfPresent(int.Parse(prev_frame_number));

        Vector3 curr_pos = Vector3.zero;
        Vector3 target_pos = Vector3.zero;
        float x;
        float y;
        float z;
        int connectedAtomIndex;
        int curr_frame_number_int = int.Parse(curr_frame_number);

        for (int i = 0; i < parser.num_atoms; i++)
        {
            x = parser.coordinateFrames[curr_frame_number_int][i, 0];
            y = parser.coordinateFrames[curr_frame_number_int][i, 1];
            z = parser.coordinateFrames[curr_frame_number_int][i, 2];
            drawSphere(x, y, z, "0", i, parser.atomNames[i], parser.atomTypes[i], parser.param_path);

            List<int> connectivities = parser.frame_connectivities[curr_frame_number_int][i];
            for (int j = 0; j < connectivities.Count; j++)
            {
                //only draw bond if the current atom # is less than the atom # of what it is being connected to
                if (parser.atomNumberFrames[curr_frame_number_int][i] < connectivities[j])
                {
                    curr_pos[0] = x;
                    curr_pos[1] = y;
                    curr_pos[2] = z;

                    connectedAtomIndex = connectivities[j] - 1;
                    target_pos[0] = parser.coordinateFrames[curr_frame_number_int][connectedAtomIndex, 0];
                    target_pos[1] = parser.coordinateFrames[curr_frame_number_int][connectedAtomIndex, 1];
                    target_pos[2] = parser.coordinateFrames[curr_frame_number_int][connectedAtomIndex, 2];

                    drawLine(curr_pos, target_pos, Color.red, "0");
                }
            }
        }
    }

    public void destroyPreviousFrameObjectsIfPresent(int previousFrameNumber)
    {
        if (previousFrameNumber >= 0)
        {
            foreach(GameObject o in Trajectory_textfield.prototypeMolecules[0].atomsAndBonds)
            {
                Destroy(o);
            }
        }
    }

    public void destroyAtomsForDocking(string molecule_number)
    {
        if (molecule_number != "")
        {
            foreach(GameObject o in Trajectory_textfield.prototypeMolecules[int.Parse(molecule_number)].atomsAndBonds)
            {
                Destroy(o);
            }
        }
    }
}
