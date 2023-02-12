using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//runs at the start of the program
[InitializeOnLoad]
public class Start{
	static Start(){
        //calls the Trilleon automatic tester if Debug is on
        if (Application.isEditor || Debug.isDebugBuild) {
            TrilleonAutomation.AutomationMaster.Initialize();
        }
        //Main test = new Main();
    }
}
