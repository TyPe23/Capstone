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
    public Database data;
    public GameObject[] buttons;

    // Start is called before the first frame update
    void Start()
    {
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

                for (int j = 0; j < data.getPlayers().GetLength(i); j++)
                {
                    indicies.Add(j);
                    content.Add(data.getPlayers()[i, j, 0].ToString() + "\t" + data.getPlayers()[i, j, 1].ToString() + "\t" + data.getPlayers()[i, j, 2].ToString() + "\n");
                }

                for (int k = 0; k < data.getPlayers().GetLength(i); k++)
                {
                    bool swapped = false;
                    for (int l = 0; l < indicies.Count - 1; l++)
                    {
                        if (Int32.Parse(data.getPlayers()[i, indicies[l], 1]) < Int32.Parse(data.getPlayers()[i, indicies[l + 1], 1]))
                        {
                            swapped = true;
                            int temp = indicies[l];
                            indicies[l] = indicies[l + 1];
                            indicies[l + 1] = temp;
                        }
                    }
                    if (!swapped)
                    {
                        break;
                    }
                }

                for (int m = 0; m < indicies.Count; m++)
                {
                    text += content[indicies[m]];
                }
            }
        }
        output.text = header + text;
    }
}
