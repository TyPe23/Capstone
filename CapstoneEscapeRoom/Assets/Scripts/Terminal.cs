using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Terminal : KeyboardTyping {

    //wordIndex, word, and output are defined in the parent class
    public string commandLine = null;
    public string user = "C:\\Users\\Champ> ";

    //a dictionary of commands and their outputs
    IDictionary<string, string> commands = new Dictionary<string, string>() {
        {"ls", "tmp.txt \t passwords.txt \t forms \n" +
                "user_information \t taxes.exe" }
    };

    //triggered when the terminal is opened
    public void startTerminal() {
        typingFunct(" ");
        word = user;
        output.text = word;

        print("word = " + word);
    }

    public void commandExecution() {
        //grab the input
        string command = word.Substring(word.Length - wordIndex, word.Length);
        //check for the command
        if (commands.ContainsKey(command)) {
            printTerm("\n" + commands[command]);
        }
        //give an error if that command is not valid
        else {
            printTerm("\n\'" + command + "\' is not recognized as an internal or external command");
        }
        //reset terminal input
        //word = "";
        wordIndex = 0;
    }

    //send text to the terminal one letter at a time
    public void textCrawler(string text) {
        for (int i = 0; i < text.Length; i++) {

            word += text[i];
            //System.Threading.Thread.Sleep(500);
            output.text = word;
        }
    }

    //prints output and formats command line
    public void printTerm(string input) {
        word += input;
        //return and print username
        word += ("\n" + user);
        //print output
        output.text = word;
        print("word = " + word);
    }

    //triggered when the terminal is closed
    public void closeTerminal() {
        word = "";
        wordIndex = 0;
        print("word = " + word);
    }
}