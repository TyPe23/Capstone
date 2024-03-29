using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class TerminalCipher : Terminal
{

    //public string userName = "Champ";
    public GameObject pass;
    public string password;
    public string userInfo;
    int rot = 13;
    public Dictionary<string, string> files;
    private bool updated = false;
    public bool loggedin = false;
    public bool question1 = false;
    public bool question2 = false;
    public bool question3 = false;
    
    public void Start()
    {
        filesUser.Add("users.txt", userInfo);
        files = filesRoot;

    }

    public void Update()
    {

    }

    //a dictionary of files and their contents
    Dictionary<string, string> filesRoot = new Dictionary<string, string>()
    {
        {"tmp.txt", "This is just a temporary file" },
        {"passwords.txt", "p@ss123" },
        {"taxes.exe", "asdvip23&t29t07gvq9Q673RFg972Q3TG)v*^\n" +
                      "&!RP97Q3VBQ0f#@f0RGFvfa0-r89fg47C)*&d\n" +
                      "va234p9f7B#R907ghqp9f38hpgq13p&*W@DFp\n" +
                      "qg4g97gq3f97hgasG(P#&TSDef;oqi4ghllef" },
        {"user_info", "Cat can only read the content of files, not directories" },
        {"forms", "Cat can only read the content of files, not directories" }
    };

    //a dictionary of files and their contents
    Dictionary<string, string> filesUser = new Dictionary<string, string>()
    {
        {"Classified.txt", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                    "Vivamus pretium leo eu ultricies accumsan. Duis sagittis " +
                    "ornare risus, et posuere nunc. Integer eu dolor laoreet " +
                    "metus elementum congue ut nec ante. Quisque nec nulla dolor."}
    };

    //a dictionary of files and their contents
    Dictionary<string, string> filesForms = new Dictionary<string, string>()
    {
        {"tmp.txt", "This is just a temporary file" },
        {"taxes.exe", "asdvip23&t29t07gvq9Q673RFg972Q3TG)v*^\n" +
                      "&!RP97Q3VBQ0f#@f0RGFvfa0-r89fg47C)*&d\n" +
                      "va234p9f7B#R907ghqp9f38hpgq13p&*W@DFp\n" +
                      "qg4g97gq3f97hgasG(P#&TSDef;oqi4ghllef" }
    };
    public void quest1(string inp)
    {
        if (inp == "Smith")
        {
            commandLine += "\n Answer accepted";
            question1 = true;
            commandLine += "\n Answer Denied \n Security Question:  \r\nHighschool mascot name:";

        }
        else
        {
            commandLine += "\n Answer Denied \n Security Question:  \r\nMother's maiden name:";
        }
    }
    public void quest2(string inp)
    {
        if (inp == "bears")
        {
            commandLine += "\n Answer accepted";
            question2 = true;
        }
        else
        {
            commandLine += "\n Answer Denied \n Security Question:  \r\nHighschool mascot name:";
        }
    }

    public void quest3(string inp)
    {
        if (inp == "October 20")
        {
            commandLine += "\n Answer accepted";
            question3 = true;
            UI.taskDone(3);
        }
        else
        {
            commandLine += "\n Answer Denied \n Security Question:  \r\nWedding anniversary (Month day)";
        }
    }


    //
    //triggered when the "Enter" button is pressed
    public override void commandExecution()
    {
        removeCurser();
        getPassword();
        commandLine += word;
        if (loggedin == false)
        {
            login(word);
        }
        else
        {
            if (question1 == false) 
            {

                quest1(word);
            }
            else
            {
                if (question2 == false)
                {

                    quest2(word);
                }
                else
                {

                    if (question3 == false)
                    {

                        quest3(word);
                    }
                    else
                    {

                        commandOptions(word);
                    }
                }
            }

            

           
            
        }
        // Determin the number of \n (new lines) are in CommandLine
        int newLines = 0;
        int j = 0;
        bool change = true;
        int lastTwoLocation1 = 0;
        int lastTwoLocation2 = 0;
        foreach (char c in commandLine)
        {
            //Debug.Log(c);
            if (c == '\\') // count new lines 
            {
                Debug.Log("Found");
                newLines++;
                if (change)
                {
                    lastTwoLocation1 = j;
                    change = false;
                }
                else
                {
                    lastTwoLocation2 = j;
                    change = true;

                }
            }
            j++;
        }
        Debug.Log(newLines);
        if (newLines > 5)
        {
            Debug.Log("Working");
            if (change)
            {
                commandLine = commandLine.Substring(lastTwoLocation2);

            }
            else
            {
                commandLine = commandLine.Substring(lastTwoLocation1);
            }
        }
        //Debug.Log(commandLine);
        commandLine += ("\n" + user);
        //print output
        output.text = commandLine;
        //reset terminal input
        word = "";
        wordIndex = 0;
        resetInput();
    }

   
    public override void printFunct(string command)
    {
        //add input to the print statement
        //the command is the entire word typed up to this point, which is why it it not yet saved to the commandLine
        output.text = (commandLine + command);
    }

    //the idea here was to use this function to implement command feedback and overwrite this funciton in a child class for every computer instance
    //controls what happens in the command line
    public string getPassword()
    {
        password = pass.GetComponent<TMP_Text>().text;
        char[] s = password.ToCharArray();

        for (int i = 0; i < password.Length; i++)
        {
            int number = (int)s[i];

            if (number >= 'a' && number <= 'z')
            {
                if (number > 'm')
                {
                    number -= rot;
                }
                else
                {
                    number += rot;
                }
            }
            else if (number >= 'A' && number <= 'Z')
            {
                if (number > 'M')
                {
                    number -= rot;
                }
                else
                {
                    number += rot;
                }
            }
            s[i] = (char)number;
        }

        password = new string(s);
        return password;
    }

    public void login(string inp)
    {

        if (inp == password)
        {
            commandLine += "\n Password accepted";
            loggedin = true;
            UI.taskDone(2);
        }
        else
        {
            commandLine += "\n Password Denied \n Login: Champ\r\nPassword:";
        }
    }
    public override void commandOptions(string input)
    {
        
        string[] inputArgs = input.Split(' ');

        //identify command
        switch (inputArgs[0])
        {
            case "ls":
                if (inputArgs.Length == 1)
                {
                    commandLine += "\n";
                    foreach (var file in files)
                    {
                        commandLine += file.Key + "\t";
                    }
                }
                else
                {
                    commandLine += "\ntoo many args for ls";
                }
                break;

            case "cat":
                if (inputArgs.Length == 1)
                {
                    commandLine += "\nnot enough args for cat, please provide a filename in the command";
                }
                else if (inputArgs.Length > 2)
                {
                    commandLine += "\ntoo many args for cat";
                }
                else if (files.ContainsKey(inputArgs[1]))
                {
                    commandLine += "\n" + files[inputArgs[1]];
                    if (inputArgs[1] == "passwords.txt")
                    {
                        
                    }
                }
                else
                {
                    commandLine += "\nfile not found";
                }

                break;

            case "help":
                commandLine += "\ncat\tused to display the content of a file" +
                               "\n\texample:  cat <filename>" +
                               "\ncd\tused to change the current directory" +
                               "\n\ncd\tused to change the directory" +
                               "\n\texample: cd <directoryname>" +
                               "\n\texample: cd" +
                               "\n\t         the exclusion of a directory" +
                               "\n\t         name returns to the previous directory" +
                               "\n\nexit\tused to exit the terminal" +
                               "\n\nls\tused to display the files in the directory" +
                               "\n\nrm\tused to delete files" +
                               "\n\texample: rm <options> <filename>" +
                               "\n\nwhoami\tused to display the name of the user";
                break;

            case "whoami":
                if (inputArgs.Length == 1)
                {
                    commandLine += "\nChamp";
                    //add function to add bonus points here
                }
                else
                {
                    commandLine += "\ntoo many args for whoami";
                }
                break;

            case ":(){":
                if (input == ":(){ :|:& };:")
                {
                    commandLine += "\nsorry, no duplication glitches here";
                    //add function to add bonus points here
                }
                else
                {
                    commandLine += ("\n\'" + input + "\' is not recognized as an internal or external command");
                }
                break;

            case "rm":
                if (inputArgs.Length == 1)
                {
                    commandLine += "\nnot enough args for rm, please provide a filename in the command";
                }
                else if (inputArgs.Length > 3)
                {
                    commandLine += "\ntoo many args for rm";
                }
                else if (input == "rm -rf \\")
                {
                    commandLine += "\nsorry, not a great idea to delete the entire file system";
                    // bonus point function
                }
                else if (files.ContainsKey(inputArgs[1]))
                {
                    commandLine += "\nFile Deleted";
                    UI.taskDone(4);
                }
                else
                {
                    commandLine += "\nfile not found";
                }
                break;

            case "cd":
                if (inputArgs.Length == 1)
                {
                    if (user == @"C:\Users\Champ>")
                    {
                        commandLine += "\nthis is the root directory";
                    }
                    else
                    {
                        user = @"C:\Users\Champ>";
                        files = filesRoot;
                    }
                }
                else if (inputArgs.Length > 2)
                {
                    commandLine += "\ntoo many args for cd";
                }
                else if (files.ContainsKey(inputArgs[1]))
                {
                    if (inputArgs[1] == "forms")
                    {
                        user = @"C:\Users\Champ\forms>";
                        files = filesForms;
                    }
                    else if (inputArgs[1] == "user_info")
                    {
                        user = @"C:\Users\Champ\user_info>";
                        files = filesUser;
                    }
                }
                else
                {
                    commandLine += "\ndirectory not found";
                }
                break;

            case "sudo":
                commandLine += "\nyou have no power here";
                //bonus point function
                break;

            case "exit":
                if (inputArgs.Length == 1)
                {
                    closeTerminal();
                }
                break;

            case "":
                break;

            default:
                commandLine += ("\n\'" + input + "\' is not recognized as an internal or external command");
                break;

        }
    }
}
