using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//used to alter and print to the command line
public class Terminal : KeyboardTyping {

    //wordIndex, word, and output are defined in the parent class
    public string commandLine = "";
    public string user = "C:\\Users\\Champ> ";
    public bool taskComplete = false;

    public TaskList UI; // access to UI class

    //a dictionary of commands and their outputs
    IDictionary<string, string> commands = new Dictionary<string, string>() {
        {"ls", "tmp.txt \t passwords.txt \t forms \n" +
                "user_information \t taxes.exe" },
        {"", "" },
        {"cat passwords.txt", "p@ss123" }
    };

    //triggered when the terminal is opened
    public void startTerminal() {
        commandLine = user;
        output.text = commandLine;
    }

    //triggered when the "Enter" button is pressed
    public void commandExecution() {
        //add word to commandLine so it will remain printed
        commandLine += word;
        //check for the command
        if (commands.ContainsKey(word)) {
            //print saved response
            commandOptions(word);
            //commandLine += ("\n" + commands[word]);
        }
        //give an error if that command is not valid
        else {
            commandLine += ("\n\'" + word + "\' is not recognized as an internal or external command");
        }
        //return and print username
        commandLine += ("\n" + user);
        //print output
        output.text = commandLine;
        //reset terminal input
        word = "";
        wordIndex = 0;
    }

    ////send text to the terminal one letter at a time
    //public void textCrawler(string text) {
    //    for (int i = 0; i < text.Length; i++) {

    //        word += text[i];
    //        //System.Threading.Thread.Sleep(500);
    //        output.text = word;
    //    }
    //}

    //prints output and formats command line
    public override void printFunct(string command) {
        //add input to the print statement
        //the command is the entire word typed up to this point, which is why it it not yet saved to the commandLine
        output.text = (commandLine + command);
    }

    //triggered when the terminal is closed
    public void closeTerminal() {
        //reset all values
        word = "";
        wordIndex = 0;
        commandLine = "";
        output.text = "";
    }

    //the idea here was to use this function to implement command feedback and overwrite this funciton in a child class for every computer instance
    //controls what happens in the command line
    public void commandOptions(string input) {
        //identify command
        switch (input) {
            case "ls":
                commandLine += ("\n" + "tmp.txt \t passwords.txt \t forms \n" +
                "user_information \t taxes.exe");
                break;
            case "cat passwords.txt":
                UI.taskDone(3);
                commandLine += ("\n" + "p@ss123");
                taskComplete = true;
                
                break;
            case "":
                break;
        }
    }
}