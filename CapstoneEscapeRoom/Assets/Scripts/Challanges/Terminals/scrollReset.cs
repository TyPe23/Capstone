using UnityEngine;
using System.Collections;
using UnityEngine.UI;  // Required when Using UI elements.

public class scrollReset : MonoBehaviour {
    public ScrollRect myScrollRect;

    public void ResetScrolling() {
        //Change the current vertical scroll position.
        myScrollRect.verticalNormalizedPosition = 0.5f;
    }
}