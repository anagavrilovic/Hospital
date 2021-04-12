using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Hospital.Model
{
    public class Termini
    {
        private string vreme;
        private Appointment ponedeljak;
        private Appointment utorak;
        private Appointment sreda;
        private Appointment cetvrtak;
        private Appointment petak;
        private Appointment subota;
        private Appointment nedelja;

        public string Vreme
        {
            get { return vreme; }
            set { vreme = value; }
        }

        public Appointment Ponedeljak
        {
            get { return ponedeljak; }
            set { ponedeljak = value; }
        }

        public Appointment Utorak
        {
            get { return utorak; }
            set { utorak = value; }
        }

        public Appointment Sreda
        {
            get { return sreda; }
            set { sreda = value; }
        }

        public Appointment Cetvrtak
        {
            get { return cetvrtak; }
            set { cetvrtak = value; }
        }

        public Appointment Petak
        {
            get { return petak; }
            set { petak = value; }
        }

        public Appointment Subota
        {
            get { return subota; }
            set { subota = value; }
        }

        public Appointment Nedelja
        {
            get { return nedelja; }
            set { nedelja = value; }
        }

        public Termini()
        {
            this.Vreme = "";
            this.Ponedeljak = new Appointment();
            this.Utorak = new Appointment();
            this.Sreda = new Appointment();
            this.Cetvrtak = new Appointment();
            this.Petak = new Appointment();
            this.Subota = new Appointment();
            this.Nedelja = new Appointment();
        }



    }


}
