using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;



namespace Logiciel
{
    class CategorieActivite
    {
        private string _nom;
        private List<Activite> _listActivite = new List<Activite>();

        public CategorieActivite(string nom)
        {
            _nom = nom;
        }

    }
}
