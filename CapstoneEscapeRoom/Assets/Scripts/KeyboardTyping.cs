using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardTyping : MonoBehaviour {

    string word = null;
    int wordIndex = 0;
    //string alpha;
    public TMP_Text output = null;
    

    public void typingFunct(string letter) {
        wordIndex++;
        word = word + letter;
        output.text = word; // send to text mesh pro
    }

    public void backspaceFunct() {
        //only erase a character if there are characters to erase
        if (wordIndex > 0) {
            //subtract a character from the word
            word = word.Substring(0, wordIndex - 1);
            wordIndex--;
            output.text = word;
        }
    }
}