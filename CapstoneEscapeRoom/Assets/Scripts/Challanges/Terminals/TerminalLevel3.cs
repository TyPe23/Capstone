using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

//used to alter and print to the command line
public class TerminalLevel3 : Terminal {

    //wordIndex, word, and output are defined in the parent class
    public string userName;
    public string password;
    public Dictionary<string, string> files;

    Dictionary<string, string> filesRoot = new Dictionary<string, string>
        {
            { "serverFiles", "killSwitch.exe" }
        };

    Dictionary<string, string> endFile = new Dictionary<string, string>
        {
            { "killSwitch.exe", "__ \n" +
                "\\_/ \n" +
                 " |._ \n" +
                 " |'.\"-._.-\"\"--.-\"-.__.-'/ \n" +
                 " |  \\        .-.           ( \n" +
                 " |   |      (@.@)       ) \n" +
                 " |   |    '=.|m|.='      / \n" +
                 " |  /     .='`\"``=.     / \n" +
                 " |.'                       ( \n" +
                 " |.-\"-.__.-\"\"-.__.-\"-.) \n" +
                 " | \n" +
                 " | \n" +
                 " | \n"
            }
        };

    public void Start()
    {
        //filesUser.Add("users.txt", userInfo);
        files = filesRoot;
    }

    public void Update() 
    { 
        //if (taskComplete && !updated)
        //{
        //    UI.taskDone(3);
        //    updated = true;
        //}
    }

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

        PlayerPrefs.SetString("cableHookedUp", "true");

        string[] inputArgs = input.Split(' ');

        //identify command
        switch (inputArgs[0]) {
            case "ls":
                //ensure the cables are connected
                if (PlayerPrefs.GetString("cableHookedUp") == "true") {
                    if (inputArgs.Length == 1) {
                        commandLine += "\n";
                        foreach (var file in files) {
                            commandLine += file.Key + "\t";
                        }
                    }
                }
                else if (inputArgs.Length > 2)
                {
                    commandLine += "\ntoo many args for ls";
                }
                break;

            case "killSwitch.exe":
                if (files.ContainsKey(inputArgs[0])) {
                    commandLine += "\n" + files[inputArgs[0]];
                        //UI.taskDone(3);
                }
                        else if (inputArgs.Length > 1) {
                    commandLine += "\ntoo many args for an executable";
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
                    commandLine += "\nsorry, you do not have the permissions to delete files";
                }
                else
                {
                    commandLine += "\nfile not found";
                }
                break;

            case "cd":
                if (inputArgs.Length == 1) {
                    if (user == @"C:\Users\Champ>") {
                        commandLine += "\nthis is the root directory";
                    }
                    else {
                        user = @"C:\Users\Champ>";
                    }
                }
                else if (inputArgs.Length > 2) {
                    commandLine += "\ntoo many args for cd";
                }
                else if (files.ContainsKey(inputArgs[1])) {
                    if (inputArgs[1] == "serverFiles") {
                        user = @"C:\Users\Champ\serverFiles>";
                        files = endFile;
                    }
                    else {
                        commandLine += "\ndirectory not found";
                    }
                    break;
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