using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardTyping : MonoBehaviour {

    public string word = "|";
    public int wordIndex = 0;
    public int curserIndex = 0;
    public TMP_Text output = null;
    bool shift = false;

    public IDictionary<string, string> shiftDictionary = new Dictionary<string, string>() {
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
        if (shiftDictionary.ContainsKey(letter) & shift == true) {
            //if so, set to capital version of the letter
            letter = shiftDictionary[letter];
        }

        //check if the curser has been moved
        //if (curserIndex != wordIndex) {
        //    string[] wordParts = word.Split("|");
        //    word = wordParts[0] + letter + "|" + wordParts[1];
        //    word = word.Substring(0, curserIndex) + letter + "|" + word.Substring(curserIndex + 1);
        //    wordIndex++;
        //    curserIndex++;
        //}
        //else {
        //    wordIndex++;
        //    curserIndex++;
        //    word = word.Substring(0, wordIndex - 1) + letter + "|";
        //}

        wordIndex++;
        curserIndex++;
        word = word.Substring(0, wordIndex - 1) + letter + "|";

        printFunct(word); // send to text mesh pro
        shift = false;
    }

    public void backspaceFunct() {
        //only erase a character if there are characters to erase
        if (curserIndex > 0) {
            //only bother doing this if the curser is not at the end of the word
            if (curserIndex < wordIndex) {
                //split up word by curser
                string[] wordParts = word.Split("|");
                //subtract a character from the first half of the word
                string beginningOfWord = wordParts[0].Remove(wordParts[0].Length - 1, 1);
                //set new word
                word = beginningOfWord + "|" + wordParts[1];
            }
            else {
                word = word.Substring(0, wordIndex - 1) + "|";
            }
            wordIndex--;
            curserIndex--;
            printFunct(word);
        }
    }

    public void shiftFunct() {
        shift = true;
    }

    public void MoveCurser(string direction) {
        if (direction == "left") {
            //move curser to the left
            if (curserIndex > 0) {
                curserIndex--;
                //split up word
                string[] wordParts = word.Split("|");
                //get the character to the left of the curser
                char lastChar = (wordParts[0])[wordParts[0].Length - 1];
                //remove the last char from the first part of the word
                string beginningOfWord = wordParts[0].Remove(wordParts[0].Length - 1, 1);
                //bring everything back together
                word = beginningOfWord + "|" + lastChar + wordParts[1];
            }
        }
        if (direction == "right") {
            //move curser to the right
            if (curserIndex < wordIndex) {
                curserIndex++;
                //split up word
                string[] wordParts = word.Split("|");
                //get the character to the right of the curser
                char firstChar = (wordParts[1])[0];
                //remove the first char from the first part of the word
                string endOfWord = wordParts[1].Remove(0, 1);
                //bring everything back together
                word = wordParts[0] + firstChar + "|" + endOfWord;
            }
        }
        printFunct(word);
    }

    public virtual void printFunct(string input) {
        output.text = input;
    }
}