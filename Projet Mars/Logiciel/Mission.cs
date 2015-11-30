using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace Logiciel
{
    class Mission
    {
        private List<Astronaute> _listAstronautes;
        private List<CategorieActivite> _listCategorieActivite;
        private Calendrier_Martien _calendar;

        public Mission(Calendrier_Martien calendar)
        {
            _calendar = calendar;
        }

        public Calendrier_Martien Calendar
        {
            get { return _calendar; }
        }

        public void AddAstronaute(Astronaute a)
        {
            _listAstronautes.Add(a);
        }

        public void RemoveAstronaute(Astronaute a)
        {
            _listAstronautes.Remove(a);
        }

        public void AddCategorie(CategorieActivite a)
        {
            _listCategorieActivite.Add(a);
        }

        public void RemoveCategorie(CategorieActivite a)
        {
            _listCategorieActivite.Remove(a);
        }

    }
}
