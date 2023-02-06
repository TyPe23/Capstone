using UnityEngine;
using System.Collections;

namespace TrilleonAutomation {

    [AutomationClass]
    public class ExampleFeatureTests : MonoBehaviour {

        [SetUpClass]
        public IEnumerator SetUpClass() {

            yield return null;

        }

        [SetUp]
        public IEnumerator SetUp() {

            GameObject TerminalIcon = Q.driver.Find(By.Name, "NAME_OF_CLICKABLE_OBJECT_YOU_COPIED"); 
            yield return StartCoroutine(Q.driver.Click(TerminalIcon, "Click first object in SetUp method."));

            ////If you needed a unique parent object to search under, use this method instead.

            //GameObject uniqueParentObject = Q.driver.Find(By.Name, "NAME_OF_UNIQUE_PARENT_OBJECT_YOU_COPIED"); 
            //GameObject firstClickableObject = Q.driver.FindIn(uniqueParentObject, By.Name, "NAME_OF_CLICKABLE_OBJECT_YOU_COPIED");
            //yield return StartCoroutine(Q.driver.Click(firstClickableObject, "Click first object in SetUp method."));
        }

    [Automation("Example Feature")]
        public IEnumerator MyFirstAutomationTestCanComplete() {

            yield return StartCoroutine(Q.assert.IsTrue(true, "This will pass.")); 
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