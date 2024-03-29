using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

//used to alter and print to the command line
public class Terminal1 : Terminal {

    //wordIndex, word, and output are defined in the parent class
    public string userName;
    public string password;
    public string userInfo;
    public string[] userNames = { "Ben", "Champ", "Dr. Cherry", "Dr. Glisson", "Mary", "Matt", "Techie", "Ty" };
    public string[] passwords = { "!dawg123", "@cat456", "#horse789", "$cow910", "%pig111", "^bird213", "&bee141", "*turtle516", "(geko171", ")lion819", "-crow202", "+star122", "!wars232", "@hammer425", "#time262", "$out728", "%play293", "^off130" };
    public Dictionary<string, string> files;
    private bool updated = false;

    public void Start()
    {
        filesUser.Add("users.txt", userInfo);
        files = filesRoot;
    }

    public void Update() 
    { 
        if (taskComplete && !updated)
        {
            UI.taskDone(3);
            updated = true;
        }
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
        {"tmp.txt", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
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

    //triggered when the "Enter" button is pressed
    public override void commandExecution() {
        removeCurser();
        //add word to commandLine so it will remain printed
        commandLine += word;

        //check for the command
        commandOptions(word);
         
        //return and print username
        commandLine += ("\n" + user);
        //print output
        output.text = commandLine;
        //reset terminal input
        resetInput();
    }

    //the idea here was to use this function to implement command feedback and overwrite this funciton in a child class for every computer instance
    //controls what happens in the command line
    public override void commandOptions(string input) {

        string[] inputArgs = input.Split(' ');

        //identify command
        switch (inputArgs[0]) {
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
                        taskComplete = true;
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
                    commandLine += "\nsorry, you do not have the permissions to delete files";
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
                    else if(inputArgs[1] == "user_info") 
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