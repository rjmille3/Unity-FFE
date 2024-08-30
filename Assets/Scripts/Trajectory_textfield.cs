using System.Collections;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading.Tasks;


public class Trajectory_textfield : MonoBehaviour
{
    string frame_number = "";
    string curr_frame_number = "";
    public static bool docking = true;
    public static bool viewing = false;
    public static bool load = false;
    public static bool save = false;
    public static string mode_status = "Docking Mode";
    public static string movement_mode = "Camera Movement";
    public static string selected_molecule = "No Selected Molecule";
    //translation mode (moves atom coordinates rather than camera coordinates)
    public static bool molecule_movement = false;
    public static string file_path = "";
    public static string save_path = "";
    public static string param_path = "";
    public GameObject f;
    //drag and dropped the FileExplorer_obj prefab into this field
    public GameObject file_explorer_prefab;
    public GameObject save_prefab;
    public GameObject param_prefab;
    public static List<PrototypeParseInputFile> prototypeMolecules = new List<PrototypeParseInputFile>();
    public List<string> file_lines = new List<string>();
    public static string selected_tag = "";



    public GameObject placeholder;
    public Text viewModeText;
    public Text movementModeText;
    public Text selectedItemText;
    public Text frameNumberText;
    public GameObject fileExplorerPopup;
    public GameObject fileExplorerPrefab;
    public GameObject saveFileExplorerPrefab;
    public GameObject paramFileExplorerPrefab;

    public GameObject cancelPopupPanel;
    public Text cancelPopupPanelText;
    public GameObject cancelOrDeletePopupPanel;
    public Text cancelOrDeletePopupPanelText;

    public InputField frameNumberInputField;
    public Text frameNumberInputPlaceholderText;
    public Slider frameNumberSlider;
    public PrototypeRenderer PrototypeRendererFile;
    int prevFrameNumber = 0;

    //Used to initialize objects and called before start
    void Awake()
    {
        cancelPopupPanel = GameObject.Find("CancelPopupPanel");
        cancelPopupPanelText = GameObject.Find("CancelPopupPanelText").GetComponent<Text>();
        cancelOrDeletePopupPanel = GameObject.Find("CancelOrDeletePopupPanel");
        cancelOrDeletePopupPanelText = GameObject.Find("CancelOrDeletePopupPanelText").GetComponent<Text>();
        viewModeText = GameObject.Find("ViewModeText").GetComponent<Text>();
        movementModeText = GameObject.Find("MovementModeText").GetComponent<Text>();
        selectedItemText = GameObject.Find("SelectedItemText").GetComponent<Text>();
        frameNumberText = GameObject.Find("FrameNumberText").GetComponent<Text>();
        frameNumberInputField = GameObject.Find("FrameNumberInputField").GetComponent<InputField>();
        frameNumberInputPlaceholderText = GameObject.Find("FrameNumberInputPlaceHolder").GetComponent<Text>();
        frameNumberSlider = GameObject.Find("FrameNumberSlider").GetComponent<Slider>();
        placeholder = GameObject.Find("placeholder");
        PrototypeRendererFile = GameObject.Find("Render_Atoms").GetComponent<PrototypeRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(placeholder);
        viewModeText.text = "Docking Mode";
        movementModeText.text = "Camera Movement";
        frameNumberInputField.readOnly = true;
        frameNumberInputPlaceholderText.text = "Frames N/A";

        cancelPopupPanel.SetActive(false);
        cancelOrDeletePopupPanel.SetActive(false);
        frameNumberSlider.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkUserInput();

        if (file_path != "")
        {
            if (!File.Exists(file_path))
            {
                cancelPopupPanelText.text = "File does not exist";
                cancelPopupPanel.SetActive(true);
            }
            else
            {
                string fileExtension = Path.GetExtension(file_path);
                if (fileExtension == ".xyz")
                {
                    if (viewModeText.text == "Docking Mode")
                    {
                        if (prototypeMolecules.Count > 0)
                        {
                            if (Path.GetExtension(prototypeMolecules[0].file_path) == ".xyz")
                            {
                                PrototypeParseInputFile parser = new PrototypeParseInputFile(prototypeMolecules.Count.ToString());
                                parser.file_path = file_path;
                                parser.param_path = param_path;
                                parser.parse_xyz_file();
                                prototypeMolecules.Add(parser);
                                PrototypeRendererFile.render(parser);
                            }
                            else
                            {
                                cancelOrDeletePopupPanelText.text = "Previous opened file did not have a .xyz file extension. Do you still wish to continue?";
                                cancelOrDeletePopupPanel.SetActive(true);
                            }
                        }
                        else
                        {
                            PrototypeParseInputFile parser = new PrototypeParseInputFile(prototypeMolecules.Count.ToString());
                            parser.file_path = file_path;
                            parser.param_path = param_path;
                            parser.parse_xyz_file();
                            prototypeMolecules.Add(parser);
                            PrototypeRendererFile.render(parser);
                        }
                    }
                    else
                    {
                        cancelPopupPanelText.text = "To open .xyz files switch to Docking Mode";
                        cancelPopupPanel.SetActive(true);
                    }
                }
                else if (fileExtension == ".arc")
                {
                    if (viewModeText.text == "Viewing Mode")
                    {
                        if (prototypeMolecules.Count > 0)
                        {
                            cancelOrDeletePopupPanelText.text = "Only one arc file can be opened at a time. Do you still wish to continue?";
                            cancelOrDeletePopupPanel.SetActive(true);
                        }
                        else
                        {
                            PrototypeParseInputFile parser = new PrototypeParseInputFile(prototypeMolecules.Count.ToString());
                            parser.file_path = file_path;
                            parser.param_path = param_path;
                            parser.parse_arc_file();
                            prototypeMolecules.Add(parser);
                            PrototypeRendererFile.renderArc(parser, "0", "0");
                            frameNumberText.text = "0/" + (prototypeMolecules[0].atomNameFrames.Count - 1).ToString();
                            frameNumberSlider.maxValue = prototypeMolecules[0].atomNameFrames.Count - 1;
                        }
                    }
                    else
                    {
                        cancelPopupPanelText.text = "To open .arc files switch to Viewing Mode";
                        cancelPopupPanel.SetActive(true);
                    }
                }
                else
                {
                    cancelPopupPanelText.text = "Files must have .xyz or .arc extensions";
                    cancelPopupPanel.SetActive(true);
                }
            }
            file_path = "";
        }

        if (save_path != "" && prototypeMolecules.Count != 0 && !File.Exists(save_path))
        {
            file_lines = new List<string>();
            for (int i = 0; i < prototypeMolecules.Count; i++)
            {
                if (i == 0)
                {
                    prototypeMolecules[i].get_lines(save_path, true);
                }
                else
                {
                    prototypeMolecules[i].get_lines(save_path, false);
                }
            }
            save_file();
            save_path = "";
        }
    }

    public void dockingButtonClicked()
    {
        viewModeText.text = "Docking Mode";
        frameNumberText.text = "Frame Number";
        frameNumberInputField.text = "";
        frameNumberInputPlaceholderText.text = "Frames N/A";
        frameNumberInputField.readOnly = true;
        frameNumberSlider.interactable = false;
    }

    public void viewingButtonClicked()
    {
        viewModeText.text = "Viewing Mode";
        frameNumberInputPlaceholderText.text = "Enter frame number";
        frameNumberInputField.readOnly = false;
        frameNumberSlider.interactable = true;
    }

    public void loadButtonClicked()
    {
        if (fileExplorerPopup != null)
        {
            Destroy(fileExplorerPopup);
        }
        fileExplorerPopup = (GameObject)Instantiate(fileExplorerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void saveButtonClicked()
    {
        if (fileExplorerPopup != null)
        {
            Destroy(fileExplorerPopup);
        }
        fileExplorerPopup = (GameObject)Instantiate(saveFileExplorerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void paramButtonClicked()
    {
        if (fileExplorerPopup != null)
        {
            Destroy(fileExplorerPopup);
        }
        fileExplorerPopup = (GameObject)Instantiate(paramFileExplorerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void closePopupButtonClicked()
    {
        cancelPopupPanel.SetActive(false);
        cancelOrDeletePopupPanel.SetActive(false);
    }

    public void destroyGameObjects()
    {
        int i = 0;
        int j = 0;
        for (i = prototypeMolecules.Count - 1; i >= 0; i--)
        {
            for (j = prototypeMolecules[i].atomsAndBonds.Count - 1; j >= 0; j--)
            {
                Destroy(prototypeMolecules[i].atomsAndBonds[j]);
            }
        }
        prototypeMolecules.Clear();
        cancelOrDeletePopupPanel.SetActive(false);
        frameNumberText.text = "Frame Number";
        frameNumberInputField.text = "";
        frameNumberSlider.maxValue = 0;

        if (viewModeText.text == "Docking Mode")
        {
            frameNumberInputPlaceholderText.text = "Frames N/A";
        }
        else if (viewModeText.text == "Viewing Mode")
        {
            frameNumberInputPlaceholderText.text = "Enter frame number";
        }
    }

    public void getFrameNumberInput()
    {
        if (int.TryParse(frameNumberInputField.text, out int frameNumberInputInt))
        {
            if (prototypeMolecules.Count > 0 && frameNumberInputInt < prototypeMolecules[0].atomNumberFrames.Count && frameNumberInputInt >= 0)
            {
                if (frameNumberInputInt != prevFrameNumber)
                {
                    PrototypeRendererFile.renderArc(prototypeMolecules[0], prevFrameNumber.ToString(), frameNumberInputField.text);
                    frameNumberText.text = frameNumberInputField.text + "/" + (prototypeMolecules[0].atomNameFrames.Count - 1).ToString();
                    prevFrameNumber = frameNumberInputInt;
                    frameNumberSlider.value = frameNumberInputInt;
                }
            }
            else
            {
                frameNumberInputField.text = "";
                frameNumberText.text = "0/" + (prototypeMolecules[0].atomNameFrames.Count - 1).ToString();
                PrototypeRendererFile.renderArc(prototypeMolecules[0], prevFrameNumber.ToString(), "0");
                // PrototypeRendererFile.destroyPreviousFrameObjectsIfPresent(prevFrameNumber);
                prevFrameNumber = 0;
                frameNumberSlider.value = 0;
            }
        }
        else
        {
            if(prototypeMolecules.Count > 0){
                frameNumberInputField.text = "";
                frameNumberText.text = "0/" + (prototypeMolecules[0].atomNameFrames.Count - 1).ToString();
                PrototypeRendererFile.renderArc(prototypeMolecules[0], prevFrameNumber.ToString(), "0");
                // PrototypeRendererFile.destroyPreviousFrameObjectsIfPresent(prevFrameNumber);
                prevFrameNumber = 0;
                frameNumberSlider.value = 0;
            }
        }
    }

    public void changeFrameNumberSlider()
    {
        if(prototypeMolecules.Count > 0){
            frameNumberInputPlaceholderText.text = "";
            frameNumberText.text = ((int)frameNumberSlider.value).ToString() + "/" + (prototypeMolecules[0].atomNameFrames.Count - 1).ToString();
            frameNumberInputField.text = ((int)frameNumberSlider.value).ToString();
            Debug.Log("SLIDER");
        } 
    }

    /*
    rightClick: select molecule
    shift key pressed: switch between modes: moving camera, moving molecule 
    */
    void checkUserInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                /*
                if(selected_tag != "")
                {
                    print(selected_tag);
                    prototypeMolecules[int.Parse(selected_tag)].enableKinematic();
                }
                */
                String[] objName = hit.transform.gameObject.name.Split(':');
                selected_tag = objName[0];
                selectedItemText.text = "Selected Molecule: " + objName[0] + " Atom Number: " + objName[1];
                /*
                if (selected_tag != "")
                {
                    prototypeMolecules[int.Parse(selected_tag)].disableKinematic();
                }
                */
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            molecule_movement = !molecule_movement;
            if (molecule_movement == true)
            {
                movementModeText.text = "Molecule Movement";
            }
            else
            {
                movementModeText.text = "Camera Movement";
            }
        }
    }

    void save_file()
    {
        // Create a file to write to.
        using (StreamWriter sw = File.CreateText(save_path))
        {
            foreach (string s in file_lines)
            {
                sw.WriteLine(s);
            }
        }
    }
}
