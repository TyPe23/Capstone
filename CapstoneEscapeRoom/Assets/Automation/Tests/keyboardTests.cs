using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TrilleonAutomation {

    [AutomationClass]
    public class TerminalTests : MonoBehaviour {

        [SetUpClass]
        public IEnumerator SetUpClass() {

            yield return null;

        }

        [SetUp]
        public IEnumerator SetUp() {

            yield return null;

        }

        [Automation("Terminal")]
        public IEnumerator OpenTerminal() {

            GameObject gameObject = null;
            gameObject = Q.driver.Find(By.Name, "ComputerScreen");
            yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(gameObject, By.Name, "TerminalIcon", false), "Click object with name TerminalIcon"));
            //check that the computer screen is inactive
            yield return StartCoroutine(Q.assert.IsTrue(!gameObject.activeInHierarchy, "Ensure the ComputerScreen is set to inactive"));
            //check that the terminal screen is active
            gameObject = Q.driver.Find(By.Name, "TerminalScreen");
            yield return StartCoroutine(Q.assert.IsTrue(gameObject.activeInHierarchy, "Ensure the TerminalScreen is set to active"));
        }

        //[Automation("Terminal")]
        //public IEnumerator KeyTesting() {
        //    //IDictionary<string, string> keyDictionary = new Dictionary<string, string>();
        //    //keyDictionary = KeyboardTyping.shiftDictionary;

        //    GameObject gameObject = null;
        //    gameObject = Q.driver.Find(By.Name, "KeyboardObject");
        //    //foreach (KeyValuePair<string, string> entry in keyDictionary) {
        //    //    // do something with entry.Value or entry.Key

        //    //}
        //    foreach (Transform child in transform) {
        //        Something(child.gameObject);
        //    }


        //    yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(gameObject, By.Name, "Button", false), "Click object with name TerminalIcon"));
        //    //check that the computer screen is inactive
        //    yield return StartCoroutine(Q.assert.IsTrue(!gameObject.activeInHierarchy, "Ensure the TerminalScreen is set to inactive"));
        //    //check that the terminal screen is active
        //    gameObject = Q.driver.Find(By.Name, "ComputerScreen");
        //    yield return StartCoroutine(Q.assert.IsTrue(gameObject.activeInHierarchy, "Ensure the ComputerScreen is set to active"));

        //}

        [Automation("Terminal")]
        public IEnumerator CloseTerminal() {

            GameObject gameObject = null;
            gameObject = Q.driver.Find(By.Name, "TerminalScreen");
            yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(gameObject, By.Name, "Button", false), "Click object with name TerminalIcon"));
            //check that the computer screen is inactive
            yield return StartCoroutine(Q.assert.IsTrue(!gameObject.activeInHierarchy, "Ensure the TerminalScreen is set to inactive"));
            //check that the terminal screen is active
            gameObject = Q.driver.Find(By.Name, "ComputerScreen");
            yield return StartCoroutine(Q.assert.IsTrue(gameObject.activeInHierarchy, "Ensure the ComputerScreen is set to active"));

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