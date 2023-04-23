using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Called when the "X" on the terminal screen is clicked. It resets the scroll position of the screen
/// </summary>
public class TerminalScrollReset : MonoBehaviour {
    public ScrollRect myScrollRect;

    public void ResetScrolling() {
        //Change the current vertical scroll position.
        myScrollRect.verticalNormalizedPosition = 0.5f;
    }
}