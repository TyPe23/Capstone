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
        public IEnumerator TerminalTest() {

            GameObject gameObject = null;
            gameObject = Q.driver.Find(By.Name, "ComputerScreen");
            yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(gameObject, By.Name, "TerminalIcon", false), "Click object with name TerminalIcon"));
            //yield return StartCoroutine(Q.assert.IsTrue(false, "This will fail.")); 
            //yield return null;

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