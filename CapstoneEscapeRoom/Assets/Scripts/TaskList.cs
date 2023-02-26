// Name: Matthew Tucker 
// Description: handles task list and compleation 
// Date: 1/31/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Diagnostics.Tracing;

public class TaskList : MonoBehaviour
{
    public TMP_Text outputs; // connect TMP text to outputs in code
    public int level; // current level

    public int compleated = 0; // number compleated 
    public int left = 0; // number left 
    public int total = 0; // total task 
    //private string FilePath; // path to file 
    private List<string> fileLines; // lines in file 
    private string output; // output 
    public bool done = false;

    //audio
    public AudioSource source;
    public AudioClip Clip1;
    public AudioClip Clip2;


    // Start is called before the first frame update
    void Start()
    {
        // file connection 
        //FilePath = "Assets/Scenes/TestWorlds/" + "TaskList" +level+ ".txt";
        //fileLines = File.ReadAllLines(FilePath).ToList();
        if(level == 1) // temp for task list 
        {
            fileLines = new List<string>() {"Gain Access to managers office", "Enter Server Room", "Find password on the computer" };
        }

        // task output 
        output = "Task:"; // starting output default 
        foreach(string line in fileLines)
        {
            left += 1;
            if (output == "Task:") // only display one task at a time 
            {
                output += "\n[]" + line + "\n"; // The List
            }
        }
        total = left;
         
        outputs.text = output; // send to text mesh pro
    }

    public void taskDone(int num) // task compleated and update list 
    {
        if ((!(num > total))&& num>0 &&(fileLines[num - 1].Substring(0, 3) != "[X]")) // check if within valid numbers and not already done 
        {
            // play audio if a task is done and have audio 
            if(source != null & Clip1 != null)
            {
                source.PlayOneShot(Clip1);
            }
            output = "Task:"; // starting output default 
            fileLines[num-1] = "[X]" + fileLines[num-1]; // add x to compleated task 
            compleated = compleated + 1;
            left = left - 1;
            foreach (string line in fileLines)
            {
                //print(output +"  output");
                //print(line  +"  line");
                if (!(line.Substring(0,3) == "[X]")) {
                    if (output == "Task:") // only display one task at a time 
                    {
                        output += "\n[]" + line + "\n"; // The List
                    }
                }
                
            }
            outputs.text = output; // send to text mesh pro
        }

        if(left == 0 && compleated == total) // check if done with all task
        {
            done = true;
            // play sound if all task are done and have audio 
            if (source != null)
            {
                if (Clip2 != null)
                {
                    source.PlayOneShot(Clip2);
                }
                else if (Clip1 != null)
                {
                    source.PlayOneShot(Clip1);
                }
            }
            output = "Task\n"; // starting output default
            //foreach (string line in fileLines) // dont need old task just to Escape
            //{
            //    output += line + "\n"; // The List
            //}
            output += "[]Escape\n"; // tell user to leave 
            outputs.text = output; // send to text mesh pro
        }
    }
    // function to end the level 
    public void endLevel()
    {
        if(left == 0 && compleated == total) // no objectives left 
        {
            SceneManager.LoadScene(1); // sending player to level Selection
        }
    }

}
