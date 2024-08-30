using System.Collections;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEditor;

public class PrototypeParseInputFile
{

    public int[] atomNumbers;
    public string[] atomNames;
    public float[,] coordinates;
    public int[] atomTypes;
    public static List<string[,]> frames = new List<string[,]>();

    public List<int[]> atomNumberFrames = new List<int[]>();
    public List<string[]> atomNameFrames = new List<string[]>();
    public List<float[,]> coordinateFrames = new List<float[,]>();
    public List<int[]> atomTypeFrames = new List<int[]>();

    public List<List<int>> connectivities = new List<List<int>>();
    public List<List<List<int>>> frame_connectivities = new List<List<List<int>>>();
    public int num_atoms;
    public Vector3 centroid;
    public string file_path = "";
    public string param_path = "";
    public string tag_name;

    public List<GameObject> atomsAndBonds = new List<GameObject>();

    public Trajectory_textfield trajectoryTextFieldObj;
    public PrototypeParseInputFile(string tag)
    {
        tag_name = tag;
        trajectoryTextFieldObj = GameObject.Find("UI").GetComponent<Trajectory_textfield>();
    }

    /*
    public void move_atoms()
    {
        Vector3 displacement = GameObject.Find("UI").GetComponent<Trajectory_textfield>().prototypeMolecules[0].centroid - new Vector3(0,0,0);
        Quaternion rotation = Quaternion.LookRotation(displacement.normalized);
        Matrix4x4 m = Matrix4x4.identity;
        m.SetTRS(displacement, rotation, new Vector3(1, 1, 1));
        Debug.Log(coordinates.GetLength(0));
        Debug.Log(coordinates.GetLength(1));
        for(int i = 0; i < coordinates.GetLength(0); i++)
        {
            Debug.Log(i);
            Vector3 p = new Vector3(coordinates[i,0], coordinates[i,1], coordinates[i,2]);
            p = m.MultiplyPoint3x4(p);
            coordinates[i,0] = p[0];
            coordinates[i,1] = p[1];
            coordinates[i,2] = p[2];
        }
        GameObject.Find("Render_Atoms").GetComponent<PrototypeRenderer>().render(this);
    }
    */

    public void enableKinematic()
    {
        foreach (GameObject atom in atomsAndBonds)
        {
            //make sure it is an atom and not a bond
            if (atom.GetComponent<LineRenderer>() == null)
            {
                atom.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    public void disableKinematic()
    {
        foreach (GameObject atom in atomsAndBonds)
        {
            //make sure it is an atom and not a bond
            if (atom.GetComponent<LineRenderer>() == null)
            {
                atom.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    public void translate_atoms(Vector3 move)
    {
        Matrix4x4 m = Matrix4x4.identity;
        m.SetTRS(move, Quaternion.identity, new Vector3(1, 1, 1));
        for (int i = 0; i < coordinates.GetLength(0); i++)
        {
            // Debug.Log(i);
            Vector3 p = new Vector3(coordinates[i, 0], coordinates[i, 1], coordinates[i, 2]);
            p = m.MultiplyPoint3x4(p);
            coordinates[i, 0] = p[0];
            coordinates[i, 1] = p[1];
            coordinates[i, 2] = p[2];
        }
        GameObject.Find("Render_Atoms").GetComponent<PrototypeRenderer>().destroyAtomsForDocking(tag_name);
        GameObject.Find("Render_Atoms").GetComponent<PrototypeRenderer>().render(this);
    }

    public void translate_atomsv2(Vector3 move)
    {
        //bool first = true;
        //Vector3 start;
        //Vector3 offset = Vector3.zero;
        //Debug.Log(GameObject.FindGameObjectsWithTag(tag_name).Length);
        foreach (GameObject atom in Trajectory_textfield.prototypeMolecules[int.Parse(tag_name)].atomsAndBonds)
        //foreach (GameObject atom in GameObject.FindGameObjectsWithTag(tag_name))
        {
            if (atom.GetComponent<LineRenderer>() != null)
            {
                Vector3 beginPos = atom.GetComponent<LineRenderer>().GetPosition(0);
                Vector3 endPos = atom.GetComponent<LineRenderer>().GetPosition(1);
                beginPos = beginPos + move;
                endPos = endPos + move;
                Vector3 newBeginPos = atom.transform.localToWorldMatrix * new Vector4(beginPos.x, beginPos.y, beginPos.z, 1);
                Vector3 newEndPos = atom.transform.localToWorldMatrix * new Vector4(endPos.x, endPos.y, endPos.z, 1);

                //Apply new position
                atom.GetComponent<LineRenderer>().SetPosition(0, newBeginPos);
                atom.GetComponent<LineRenderer>().SetPosition(1, newEndPos);
            }
            else
            {
                string[] numbers = Regex.Split(atom.name, @"\D+");
                atom.transform.Translate(move, GameObject.Find("Main Camera").GetComponent<PrototypeCameraMovement>().camera_space);
                coordinates[int.Parse(numbers[1]), 0] = atom.transform.position.x;
                coordinates[int.Parse(numbers[1]), 1] = atom.transform.position.y;
                coordinates[int.Parse(numbers[1]), 2] = atom.transform.position.z;

            }

            //atom.transform.Translate(move, Space.World);
            /*
            if(atom.GetComponent<LineRenderer>() != null)
            {
                Vector3 beginPos = atom.GetComponent<LineRenderer>().GetPosition(0);
                Vector3 endPos = atom.GetComponent<LineRenderer>().GetPosition(1);
                beginPos = beginPos + move;
                endPos = endPos + move;
                Vector3 newBeginPos = atom.transform.localToWorldMatrix * new Vector4(beginPos.x, beginPos.y, beginPos.z, 1);
                Vector3 newEndPos = atom.transform.localToWorldMatrix * new Vector4(endPos.x, endPos.y, endPos.z, 1);

                //Apply new position
                atom.GetComponent<LineRenderer>().SetPosition(0, newBeginPos);
                atom.GetComponent<LineRenderer>().SetPosition(1, newEndPos);
            }
            else
            {
                //atom.transform.Translate(move, GameObject.Find("Main Camera").GetComponent<PrototypeCameraMovement>().camera_space);
                if (!first)
                {
                    atom.transform.Translate(move, GameObject.Find("Main Camera").GetComponent<PrototypeCameraMovement>().camera_space);
                }
                else
                {
                    start = atom.transform.position;
                    atom.transform.Translate(move, GameObject.Find("Main Camera").GetComponent<PrototypeCameraMovement>().camera_space);
                    offset = atom.transform.position - start;
                    centroid = centroid + offset;
                    first = false;
                }
            }
            */
            //figure out how to compute the centroid without brute force!! The code below seems close?
            /*
            if(atom.GetComponent<LineRenderer>() == null)
            {
                if (!first)
                {
                    atom.transform.Translate(move, Space.Self);
                }
                else
                {
                    start = atom.transform.position;
                    atom.transform.Translate(move, Space.Self);
                    offset = atom.transform.position - start;
                    centroid = centroid + offset;
                    first = false;
                }
            }
            */


        }
        
        /*
        for (int i = 0; i < coordinates.GetLength(0); i++)
        {
            coordinates[i, 0] = coordinates[i, 0] + offset[0];
            coordinates[i, 1] = coordinates[i, 1] + offset[1];
            coordinates[i, 2] = coordinates[i, 2] + offset[2];
        }
        */
        

        compute_centroid();

    }

    public void translate_atomsv3(Vector3 move)
    {
        bool first = true;
        Vector3 start;
        Vector3 offset = Vector3.zero;

        //"atom" actually refers to atoms and bonds
        foreach (GameObject atom in Trajectory_textfield.prototypeMolecules[int.Parse(tag_name)].atomsAndBonds)
        {
            //if the game object is a bond...
            if (atom.GetComponent<LineRenderer>() != null)
            {
                Vector3 beginPos = atom.GetComponent<LineRenderer>().GetPosition(0);
                Vector3 endPos = atom.GetComponent<LineRenderer>().GetPosition(1);
                beginPos = beginPos + move;
                endPos = endPos + move;
                Vector3 newBeginPos = atom.transform.localToWorldMatrix * new Vector4(beginPos.x, beginPos.y, beginPos.z, 1);
                Vector3 newEndPos = atom.transform.localToWorldMatrix * new Vector4(endPos.x, endPos.y, endPos.z, 1);

                //Apply new position
                atom.GetComponent<LineRenderer>().SetPosition(0, newBeginPos);
                atom.GetComponent<LineRenderer>().SetPosition(1, newEndPos);
            }
            else
            {
                atom.transform.Translate(move, Space.World);

                string[] numbers = Regex.Split(atom.name, @"\D+");
                coordinates[int.Parse(numbers[1]), 0] = atom.transform.position.x;
                coordinates[int.Parse(numbers[1]), 1] = atom.transform.position.y;
                coordinates[int.Parse(numbers[1]), 2] = atom.transform.position.z;
            }
            
        }

        /*
        for (int i = 0; i < num_atoms; i++)
        {
            coordinates[i, 0] = coordinates[i, 0] + move[0];
            coordinates[i, 1] = coordinates[i, 1] + move[1];
            coordinates[i, 2] = coordinates[i, 2] + move[2];
        }
        */

        //compute_centroid();
        centroid = centroid + move;

    }


    public void rotate_atoms(Quaternion q)
    {
        Matrix4x4 m = Matrix4x4.identity;
        m.SetTRS(new Vector3(0,0,0), q, new Vector3(1, 1, 1));
        for (int i = 0; i < coordinates.GetLength(0); i++)
        {
            // Debug.Log(i);
            Vector3 p = new Vector3(coordinates[i, 0], coordinates[i, 1], coordinates[i, 2]);
            p = m.MultiplyPoint3x4(p);
            coordinates[i, 0] = p[0];
            coordinates[i, 1] = p[1];
            coordinates[i, 2] = p[2];
        }
        GameObject.Find("Render_Atoms").GetComponent<PrototypeRenderer>().destroyAtomsForDocking(tag_name);
        GameObject.Find("Render_Atoms").GetComponent<PrototypeRenderer>().render(this);
    }

    public void rotate_atomsv2(Vector3 eulers)
    {
        Matrix4x4 m = Matrix4x4.identity;
        m.SetTRS(new Vector3(0, 0, 0), Quaternion.Euler(eulers), new Vector3(1, 1, 1));
        foreach (GameObject atom in Trajectory_textfield.prototypeMolecules[int.Parse(tag_name)].atomsAndBonds)
        {
            if (atom.GetComponent<LineRenderer>() != null)
            {
                Vector3 beginPos = atom.GetComponent<LineRenderer>().GetPosition(0);
                Vector3 endPos = atom.GetComponent<LineRenderer>().GetPosition(1);
                //beginPos = m.MultiplyPoint3x4(beginPos);
                //endPos = m.MultiplyPoint3x4(endPos);
                //Vector3 newBeginPos = atom.transform.localToWorldMatrix * new Vector4(beginPos.x, beginPos.y, beginPos.z, 1);
                //Vector3 newEndPos = atom.transform.localToWorldMatrix * new Vector4(endPos.x, endPos.y, endPos.z, 1);
                Vector3 newBeginPos = Quaternion.Euler(eulers) * (beginPos - centroid) + centroid;
                Vector3 newEndPos = Quaternion.Euler(eulers) * (endPos - centroid) + centroid;

                //Apply new position
                atom.GetComponent<LineRenderer>().SetPosition(0, newBeginPos);
                atom.GetComponent<LineRenderer>().SetPosition(1, newEndPos);
            }
            else
            {
                string[] numbers = Regex.Split(atom.name, @"\D+");
                atom.transform.RotateAround(centroid, new Vector3(centroid[0] + 1, centroid[1], centroid[2]) - centroid, eulers[0]);
                atom.transform.RotateAround(centroid, new Vector3(centroid[0], centroid[1] + 1, centroid[2]) - centroid, eulers[1]);
                atom.transform.RotateAround(centroid, new Vector3(centroid[0], centroid[1], centroid[2] + 1) - centroid, eulers[2]);
                coordinates[int.Parse(numbers[1]), 0] = atom.transform.position.x;
                coordinates[int.Parse(numbers[1]), 1] = atom.transform.position.y;
                coordinates[int.Parse(numbers[1]), 2] = atom.transform.position.z;
            }

        }


    }

    public void get_lines(string path, bool first)
    {
        if (first)
        {
            trajectoryTextFieldObj.file_lines.Add('\t' + num_atoms.ToString());
            for (int i = 0; i < num_atoms; i++)
            {
               string s = '\t' + atomNumbers[i].ToString() + '\t' + atomNames[i] + '\t' + String.Format("{0:0.000000}", coordinates[i, 0]) + '\t' + String.Format("{0:0.000000}", coordinates[i, 1]) + '\t' + String.Format("{0:0.000000}", coordinates[i, 2]) + '\t' + atomTypes[i].ToString();
               for (int j = 0; j < connectivities[i].Count; j++)
               {
                    s = s + '\t' + connectivities[i][j];
               }
               trajectoryTextFieldObj.file_lines.Add(s);
            }
        }
        else if (!first)
        {
            //should fix this, if the first line contains the num atoms and a name, this will cause an error
            int offset = int.Parse(trajectoryTextFieldObj.file_lines[0]);
            trajectoryTextFieldObj.file_lines[0] = '\t' + (int.Parse(trajectoryTextFieldObj.file_lines[0]) + num_atoms).ToString();
            for (int i = 0; i < num_atoms; i++)
            {
                string s = '\t' + (atomNumbers[i] + offset).ToString() + '\t' + atomNames[i] + '\t' + String.Format("{0:0.000000}", coordinates[i, 0]) + '\t' + String.Format("{0:0.000000}", coordinates[i, 1]) + '\t' + String.Format("{0:0.000000}", coordinates[i, 2]) + '\t' + atomTypes[i].ToString();
                for (int j = 0; j < connectivities[i].Count; j++)
                {
                    s = s + '\t' + (connectivities[i][j] + offset);
                }
                trajectoryTextFieldObj.file_lines.Add(s);
            }
        }

    }

    private void compute_centroid()
    {
        float x = 0;
        float y = 0;
        float z = 0;
        for (int i = 0; i < num_atoms; i++)
        {
            x += coordinates[i, 0];
            y += coordinates[i, 1];
            z += coordinates[i, 2];
        }
        x /= num_atoms;
        y /= num_atoms;
        z /= num_atoms;
        centroid = new Vector3(x, y, z);
    }

    public void parse_xyz_file()
    {
        bool firstLineRead = false;
        int counter = 0;
        string line;

        System.IO.StreamReader file = new System.IO.StreamReader(file_path);
        while ((line = file.ReadLine()) != null)
        {
            if (line != "")
            {
                if (firstLineRead)
                {
                    string[] s = Regex.Split(line, @"\s+");

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
                }
                else
                {
                    firstLineRead = true;
                    string[] s = Regex.Split(line, @"\s+");
                    num_atoms = int.Parse(s[1]);

                    atomNumbers = new int[num_atoms];
                    atomNames = new string[num_atoms];
                    coordinates = new float[num_atoms, 3];
                    atomTypes = new int[num_atoms];
                    connectivities = new List<List<int>>();
                }
            }
        }
        file.Close();
        compute_centroid();
    }

    public void parse_arc_file()
    {
        bool firstLineRead = false;
        int counter = 0;
        string line;
        int index = -1;
        int frame_num = 0;

        System.IO.StreamReader file = new System.IO.StreamReader(file_path);
        while ((line = file.ReadLine()) != null)
        {
            if (line != "")
            {
                if (index != -1)
                {
                    if (counter != num_atoms)
                    {
                        string[] s = Regex.Split(line, @"\s+");

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
                    }
                    else
                    {
                        counter = 0;

                        atomNumberFrames.Add(atomNumbers);
                        atomNameFrames.Add(atomNames);
                        coordinateFrames.Add(coordinates);
                        atomTypeFrames.Add(atomTypes);
                        frame_connectivities.Add(connectivities);

                        atomNumbers = new int[num_atoms];
                        atomNames = new string[num_atoms];
                        coordinates = new float[num_atoms, 3];
                        atomTypes = new int[num_atoms];
                        connectivities = new List<List<int>>();

                        frame_num++;
                        index = -1;
                    }
                }
                else
                {
                    string[] s = Regex.Split(line, @"\s+");
                    num_atoms = int.Parse(s[1]);

                    atomNumbers = new int[num_atoms];
                    atomNames = new string[num_atoms];
                    coordinates = new float[num_atoms, 3];
                    atomTypes = new int[num_atoms];
                    connectivities = new List<List<int>>();

                    firstLineRead = true;
                }
                index++;
            }

        }
        file.Close();

        //Add the atoms and tag for the last frame
        atomNumberFrames.Add(atomNumbers);
        atomNameFrames.Add(atomNames);
        coordinateFrames.Add(coordinates);
        atomTypeFrames.Add(atomTypes);
        frame_connectivities.Add(connectivities); 
        compute_centroid();
    }
}
