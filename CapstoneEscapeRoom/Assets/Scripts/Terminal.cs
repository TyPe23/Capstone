using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Terminal : KeyboardTyping {

    //wordIndex, word, and output are defined in the parent class
    public string commandLine = "";
    public string user = "C:\\Users\\Champ> ";

    //a dictionary of commands and their outputs
    IDictionary<string, string> commands = new Dictionary<string, string>() {
        {"ls", "tmp.txt \t passwords.txt \t forms \n" +
                "user_information \t taxes.exe" },
        {"cd forms", "" }
    };

    //triggered when the terminal is opened
    public void startTerminal() {
        commandLine = user;
        output.text = commandLine;

        print("CL = " + commandLine);
    }

    public void commandExecution() {

        print("CLbeforeEx = " + commandLine);
        //add word to commandLine so it will remain printed
        commandLine += word;
        //grab the input
        string command = word;//.Substring(word.Length - wordIndex, word.Length);
        //check for the command
        if (commands.ContainsKey(command)) {
            commandLine += ("\n" + commands[command]);
        }
        //give an error if that command is not valid
        else {
            commandLine += ("\n\'" + command + "\' is not recognized as an internal or external command");
        }
        //return and print username
        commandLine += ("\n" + user);
        //print output
        output.text = commandLine;
        print("CLafterEx = " + commandLine);
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
        print("new print");
        //save input to process input
        print("command = " + command);
        //print input
        output.text = (commandLine + command);
        print("CL = " + commandLine);
    }

    //triggered when the terminal is closed
    public void closeTerminal() {
        print("closed");
        word = "";
        wordIndex = 0;
        commandLine = "";
        print("closeword = " + word);
        print("CL = " + commandLine);
    }

    //controls what happens in the command line
    public void commandOptions(string input) {
        //write on the next line
        commandLine += "\n";
        //identify command
        switch (input) {
            case "ls":
                commandLine += "tmp.txt \t passwords.txt \t forms \n" +
                "user_information \t taxes.exe";
                break;
            case "cd forms":
                user += "/forms";
                break;
        }
    }
}