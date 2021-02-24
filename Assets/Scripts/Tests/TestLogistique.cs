using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;


namespace Tests
{
    public class TestLogistique
    {

        [UnityTest]
        public IEnumerator TestFonctionEt()
        {
            yield return new WaitForFixedUpdate();

            Assert.IsFalse(Logique.FonctionEt(false, false));
            Assert.IsFalse(Logique.FonctionEt(true, false));
            Assert.IsFalse(Logique.FonctionEt(false, true));
            Assert.IsTrue(Logique.FonctionEt(true, true));

            
        }

        [UnityTest]
        public IEnumerator TestFonctionOu()
        {
            Assert.IsFalse(Logique.FonctionOu(false, false));
            Assert.IsTrue(Logique.FonctionOu(true, false));
            Assert.IsTrue(Logique.FonctionOu(false, true));
            Assert.IsTrue(Logique.FonctionOu(true, true));

            yield return new WaitForFixedUpdate();

        }

        [UnityTest]
        public IEnumerator TestFonctionNon()
        {

            yield return new WaitForFixedUpdate();

            Assert.IsTrue(Logique.FonctionNon(false));
            Assert.IsFalse(Logique.FonctionNon(true));

        }

        [UnityTest]
        public IEnumerator TestFonctionNonEt()
        {
            Assert.IsTrue(Logique.FonctionNonEt(false, false));
            Assert.IsTrue(Logique.FonctionNonEt(false, true));
            Assert.IsTrue(Logique.FonctionNonEt(true, false));
            Assert.IsFalse(Logique.FonctionNonEt(true, true));

            yield return new WaitForFixedUpdate();
        }

        [UnityTest]
        public IEnumerator TestFonctionNonOu()
        {
            Assert.IsTrue(Logique.FonctionNonOu(false, false));
            Assert.IsFalse(Logique.FonctionNonOu(false, true));
            Assert.IsFalse(Logique.FonctionNonOu(true, false));
            Assert.IsFalse(Logique.FonctionNonOu(true, true));

            yield return new WaitForFixedUpdate();
        }


        [UnityTest]
        public IEnumerator TestFonctionOuExclusif()
        {
            Assert.IsFalse(Logique.FonctionOuExclusif(false, false));
            Assert.IsTrue(Logique.FonctionOuExclusif(false, true));
            Assert.IsTrue(Logique.FonctionOuExclusif(true, false));
            Assert.IsFalse(Logique.FonctionOuExclusif(true, true));

            yield return new WaitForFixedUpdate();
        }

        [UnityTest]
        public IEnumerator TestFonctionNonOuExclusif()
        {
            Assert.IsTrue(Logique.FonctionNonOuExclusif(false, false));
            Assert.IsFalse(Logique.FonctionNonOuExclusif(false, true));
            Assert.IsFalse(Logique.FonctionNonOuExclusif(true, false));
            Assert.IsTrue(Logique.FonctionNonOuExclusif(true, true));

            yield return new WaitForFixedUpdate();
        }

    }
}