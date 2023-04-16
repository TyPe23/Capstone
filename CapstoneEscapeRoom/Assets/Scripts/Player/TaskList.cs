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
using System.Linq.Expressions;

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
    public string L;
    //audio
    public AudioSource source;
    public AudioClip Clip1;
    public AudioClip Clip2;
    public TMP_Text outs;
    //end game screen 
    public GameObject EndGame;
   


    // Start is called before the first frame update
    void Start()
    {
        
        // file connection 
        //FilePath = "Assets/Scenes/TestWorlds/" + "TaskList" +level+ ".txt";
        //fileLines = File.ReadAllLines(FilePath).ToList();
        switch (level)// task selection 
        {
            case 0: // test worlds 
                fileLines = new List<string>() {};
                break;
            case 1: // level 1
                fileLines = new List<string>() { "Gain Access to managers office", "Enter Server Room", "Find password on the computer" };
              
                break;
            case 2: // level 2
                fileLines = new List<string>() { "Gain Access to Managers room", "Gain Access to Managers Computer", "Find out this information","Delete Taxes.exe on managers computer"};
          
                break;
            case 3: // level 3
                fileLines = new List<string>() {" Gain Access to Server room", "Connect to server from computer", "Run exploit commands" };
             
                break;
            case 4: // level 4
                fileLines = new List<string>() { };
           
                break;
            case 5: // level 5 
                fileLines = new List<string>() { };
              
                break;
               
        }
        //outs.text = Lesson;

        //if(level == 1) // temp for task list 
        //{
        //    fileLines = new List<string>() {"Gain Access to managers office", "Enter Server Room", "Find password on the computer" };
        //}

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
    public void Lessons(int level)
    {
        switch (level)// task selection 
        {
            case 0: // test worlds 

                break;
            case 1: // level 1

                L = "Do not store passwords in unsecured locations such as sticky notes";
                break;
            case 2: // level 2

                L = "Do not have security questions with easily accessible information";
                break;
            case 3: // level 3

                L = "Make sure all security software is up to date";
                break;
            case 4: // level 4

                L = "Lessons 4";
                break;
            case 5: // level 5 

                L = "Lessons 5";
                break;
        }
        outs.text = L;
    }
        // function to end the level 
    public void endLevel()
    {
        EndGame.SetActive(true);
    }
    public void leveCompleated()
    {
        if (left == 0 && compleated == total) // no objectives left 
        {
            Lessons(level);
            SceneManager.LoadScene("Lessons"); // sending player to level Selection

        }
    }
}
