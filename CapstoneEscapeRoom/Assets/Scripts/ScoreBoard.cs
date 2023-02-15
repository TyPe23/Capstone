using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

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
                    "------------------------------\n";

        string content = "";

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].activeSelf == false)
            {
                header =
                        "\t Level " + (i + 1) + "\n" +
                        "------------------------------\n";

                content = "";
                for (int j = 0; j < (data.getPlayers().GetLength(i)); j++)
                {
                    content += data.getPlayers()[i, j, 0].ToString() + "\t" + data.getPlayers()[i, j, 1].ToString() + "\t" + data.getPlayers()[i, j, 2].ToString() + "\n";
                }

            }
        }
        output.text = header + content;
    }
}
