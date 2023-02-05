using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardTyping : MonoBehaviour {

    public string word = null;
    public int wordIndex = 0;
    public TMP_Text output = null;
    bool shift = false;
    IDictionary<string, string> shiftDictionary = new Dictionary<string, string>() {
        {"a", "A"}, {"b", "B"}, {"c", "C" }, {"d", "D"},
        {"e", "E"}, {"f", "F"}, {"g", "G"}, {"h", "H"},
        {"i", "I"}, {"j", "J" }, {"k", "K"}, {"l", "L"},
        {"m", "M"}, {"n", "N"}, {"o", "O"}, {"p", "P"},
        {"q", "Q"}, {"r", "R"}, {"s", "S"}, {"t", "T"},
        {"u", "U"}, {"v", "V"}, {"w", "W"}, {"x", "X"},
        {"y", "Y"}, {"z", "Z"}, {"1", "!"}, {"2", "@"},
        {"3", "#"}, {"4", "$"}, {"5", "%"}, {"6", "^"},
        {"7", "&"}, {"8", "*"}, {"9", "("}, {"0", ")"},
        {"-", "_"}, {"=", "+"}, {"[", "{"}, {"]", "}"},
        {"\\", "|"}, {";", ":"}, {"'", "\""}, {",", "<"},
        {".", ">"}, {"/", "?"}
    };


    public void typingFunct(string letter) {
        //check if the character is not in the dictionary
        //or shift is false

        print("word = " + word);
        if (!shiftDictionary.ContainsKey(letter) | shift == false) {
            wordIndex++;
            word = word + letter;
            printFunct(word); // send to text mesh pro
        }
        else if (shift == true) {
            wordIndex++;
            word = word + shiftDictionary[letter];
            printFunct(word);
        }
        shift = false;
    }

    public void backspaceFunct() {
        //only erase a character if there are characters to erase
        if (wordIndex > 0) {
            //subtract a character from the word
            word = word.Substring(0, wordIndex - 1);
            wordIndex--;
            printFunct(word);
        }
    }

    public void shiftFunct() {
        shift = true;
    }

    public virtual void printFunct(string input) {
        output.text = input;
    }
}