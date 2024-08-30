using System.Collections;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEditor;

public class Docking_Parser
{
    /*
    public string[,] atoms;
    public List<List<int>> connectivities = new List<List<int>>();
    public int num_atoms;
    //public SerializedObject tagManager;
    //public SerializedProperty tagsProp;
    public Vector3 centroid;
    public string file_path = "";
    public string tag_name;
    public GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        //tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        //tagsProp = tagManager.FindProperty("tags");
    }
    
    void compute_centroid()
    {
        float x = 0;
        float y = 0;
        float z = 0;
        for (int i = 0; i < num_atoms; i++)
        {
            x += float.Parse(atoms[i, 2]);
            y += float.Parse(atoms[i, 3]);
            z += float.Parse(atoms[i, 4]);
        }
        x /= num_atoms;
        y /= num_atoms;
        z /= num_atoms;
        centroid = new Vector3(x, y, z);
    }

    void create_tag(string molecule_number)
    {
        //GameObject.Find("UI").GetComponent<Trajectory_textfield>().tagsProp.InsertArrayElementAtIndex(0);
        //SerializedProperty n = GameObject.Find("UI").GetComponent<Trajectory_textfield>().tagsProp.GetArrayElementAtIndex(0);

        //working
        //int tag_length = GameObject.Find("UI").GetComponent<Trajectory_textfield>().tagsProp.arraySize;
        //GameObject.Find("UI").GetComponent<Trajectory_textfield>().tagsProp.InsertArrayElementAtIndex(tag_length - 1);
        //SerializedProperty n = GameObject.Find("UI").GetComponent<Trajectory_textfield>().tagsProp.GetArrayElementAtIndex(tag_length - 1);
        //n.stringValue = molecule_number;
    }

    public void read_file()
    {
        string[] lines = File.ReadAllLines(file_path);
        int i = -1;
        int counter = 0;
        int frame_num = 0;

        foreach (string line in lines)
        {
            //ignore first line
            if (i != -1)
            {
                string[] s = Regex.Split(line, @"\s+");
                //s[0] appears to be blank? s[1] represents atom #
                atoms[i, 0] = s[1];
                atoms[i, 1] = s[2];
                atoms[i, 2] = s[3];
                atoms[i, 3] = s[4];
                atoms[i, 4] = s[5];
                atoms[i, 5] = s[6];

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
                string[] s = Regex.Split(line, @"\s+");
                num_atoms = int.Parse(s[1]);
                atoms = new string[num_atoms, 6];
                connectivities = new List<List<int>>();
                //print(num_atoms);
            }
            i++;

        }
        //Add the atoms and tag for the last frame
        // create_tag(GameObject.Find("UI").GetComponent<Trajectory_textfield>().molecules.Count.ToString());
        // tag_name = GameObject.Find("UI").GetComponent<Trajectory_textfield>().molecules.Count.ToString();

        //create_tag(Trajectory_textfield.molecules.Count.ToString());
        //tag_name = Trajectory_textfield.molecules.Count.ToString();

        //compute the centroid
        compute_centroid();

        //update tag manager
        //GameObject.Find("UI").GetComponent<Trajectory_textfield>().tagManager.ApplyModifiedProperties();
        //GameObject.Find("UI").GetComponent<Trajectory_textfield>().tagManager.Update();


        //Trajectory_textfield.tagManager.ApplyModifiedProperties();
        //Trajectory_textfield.tagManager.Update();

        //render the atoms
        //print("before " + tag_name);
        //Render_atoms.Docking_render(this);
        //print("SCRIPT" + this);
        //GameObject.Find("Render_Atoms").GetComponent<Render_atoms>().Docking_render(this);
    }*/
}
