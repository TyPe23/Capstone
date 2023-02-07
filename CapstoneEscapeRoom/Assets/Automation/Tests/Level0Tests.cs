using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting;
using UnityEngine.XR.Interaction.Toolkit;

namespace TrilleonAutomation {

    [AutomationClass]
    public class Level0Tests : MonoBehaviour {

        [SetUpClass]
        public IEnumerator SetUpClass() {

            yield return null;

        }

        [SetUp]
        public IEnumerator SetUp() {

            yield return null;

        }

        [Automation("Level0Test")]
        public IEnumerator Level0Test() {
            //add keycard collision
            //Collision collision = new Collision();
            //collision.gameObject.name = "ID card";



            //yield return StartCoroutine(Q.driver.WaitFor(() => doorLock2.GetComponent<XRGrabInteractable>().enabled == true, "Test that the door is unlocked when a keycard is used"));
            //int currentEquation = QwackulatorGameTestObject.duck_tutorial.Id;
            //yield return StartCoroutine(Q.assert.IsTrue(QwackulatorGameTestObject.duck_tutorial.IsDuck, "Equation should be a Duck."));
            //yield return StartCoroutine(Q.driver.WaitFor(() => QwackulatorGameTestObject.duck_tutorial != null && currentEquation != QwackulatorGameTestObject.duck_tutorial.Id, "If the tutorial eqaution is not answered in time, it is destroyed, and a new one appears for the user.", 45f));
            yield return null;
            //GameObject parentObject = null;
            //parentObject = Q.driver.Find(By.Name, "ButtonOn");
            //yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "Clicker", false), "Depress On Button"));

        }

        [TearDown]
        public IEnumerator TearDown() {

            yield return null;

        }

        [TearDownClass]
        public IEnumerator TearDownClass() {

            yield return null;

        }

    }

}