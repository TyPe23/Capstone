using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class ScoreBoard : MonoBehaviour
{
    public TMP_Text output;
    public string[,,] data;
    public GameObject[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        data = Database.getPlayers();
        string header =
                    "\t Level _\n" +
                    "------------------------------\n";
        output.text = header;
    }

    private void Update()
    {
        string header =
                    "\t Level _\n" +
                    "Username\tScore\tTime\n" +
                    "------------------------------\n";

        string text = "";

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].activeSelf == false)
            {
                header =
                        "\t Level " + (i + 1) + "\n" +
                        "Username\tScore\tTime\n" + 
                        "------------------------------\n";
                
                List<string> content = new List<string>();
                List<int> indicies = new List<int>();
                try {
                    for (int j = 0; j < data.GetLength(1); j++) {
                        indicies.Add(j);
                        content.Add(data[i, j, 0].ToString() + "\t" + data[i, j, 1].ToString() + "\t" + data[i, j, 2].ToString() + "\n");
                    }

                    for (int k = 0; k < data.GetLength(1); k++) {
                        bool swapped = false;
                        for (int l = 0; l < indicies.Count - 1; l++) {
                            if (Int32.Parse(data[i, indicies[l], 1]) < Int32.Parse(data[i, indicies[l + 1], 1])) {
                                swapped = true;
                                int temp = indicies[l];
                                indicies[l] = indicies[l + 1];
                                indicies[l + 1] = temp;
                            }
                        }
                        if (!swapped) {
                            break;
                        }
                    }

                    for (int m = 0; m < indicies.Count; m++) {
                        text += content[indicies[m]];
                    }
                }
                catch(Exception e) {
                    Debug.Log(e);
                    text = "";
                }
            }
        }
        output.text = header + text;
    }
}
