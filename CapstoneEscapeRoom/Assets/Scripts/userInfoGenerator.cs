using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class userInfoGenerator : MonoBehaviour
{
    public Terminal1 terminal;

    // Start is called before the first frame update
    void Start()
    {
        

        terminal.userInfo = "";
        for (int i = 0; i < terminal.userNames.Count(); i++)
        {
            int rand = Random.Range(i, terminal.passwords.Count());
            string temp = terminal.passwords[i];
            terminal.passwords[i] = terminal.passwords[rand];
            terminal.passwords[rand] = temp;
            terminal.userInfo += terminal.userNames[i] + "\t" + terminal.passwords[i] + "\n";
        }
        int seed = Random.Range(0, terminal.userNames.Count());
        terminal.userName = terminal.userNames[seed];
        terminal.password = terminal.passwords[seed];
    }
}
