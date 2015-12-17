using Logiciel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;

namespace TestProject
{
    
    
    /// <summary>
    ///Classe de test pour LieuTest, destinée à contenir tous
    ///les tests unitaires LieuTest
    ///</summary>
    [TestClass()]
    public class LieuTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        // 
        //Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        //Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test dans la classe
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Utilisez ClassCleanup pour exécuter du code après que tous les tests ont été exécutés dans une classe
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Test pour Parse
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            XmlDocument xmlDoc = new XmlDocument();

            Lieu l = new Lieu("TestDeLieu", new System.Drawing.Point(50, 50));

            XmlNode NodeLieu = xmlDoc.CreateElement("Lieu");

            XmlNode NodeNom = xmlDoc.CreateElement("Nom");
            NodeNom.InnerText = l.Nom.ToString();
            NodeLieu.AppendChild(NodeNom);

            XmlNode NodeCoords = xmlDoc.CreateElement("Coordonnées");
            NodeCoords.InnerText = l.Coords.ToString();
            NodeLieu.AppendChild(NodeCoords);



            XmlNode test = NodeLieu; // TODO: initialisez à une valeur appropriée
            string expected =  l.Nom ; // TODO: initialisez à une valeur appropriée
            System.Drawing.Point Expected = l.Coords;
            
            string actual = Lieu.Parse(test).Nom;
            System.Drawing.Point Actual = Lieu.Parse(test).Coords;

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(Expected, Actual);
            // Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }
    }
}
