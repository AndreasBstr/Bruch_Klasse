using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BruchKlasse
{
    class Bruch : IComparable
    {
        //Interne Variablen zum Speichern des Zählers, Nenners und Ergebnisses eines Bruch Objekts
        private decimal zaehler;
        private decimal nenner;
        private decimal dezimal;

        //Zugriffsmethode zum Abfragen des Zählers
        public decimal Zaehler
        {
            get { return zaehler; }
        }

        //Zugriffsmethode zum Abfragen des Nenners
        public decimal Nenner
        {
            get { return nenner; }
        }

        //Zugriffsmethode zum Abfragen des Ergebnisses
        public decimal Dezimal
        {
            get { return dezimal; }
        }

        //Zugriffsmethode zum Erfragen des Kehrwerts eines Bruches
        //Liefert einen Neuen Bruch zurück, bei dem als Zähler der eigene Nenner angegeben wird
        //und als Nenner der eignene Zähler
        public Bruch Reziproke
        {
            get { return new Bruch(Convert.ToInt32(this.nenner), Convert.ToInt32(this.zaehler)); }
        }
        //Zugriffsmethode zum Erfragen der Kürzbarkeit -> Wird ein größter gemeinsamer
        //Nenner ermittelt -> Ja, sonst -> Nein
        public bool IsKuerzbar
        {
            get
            {
                int zahlKleiner;
                int zahlGroesser;

                if (this.Zaehler > this.Nenner)
                {
                    zahlGroesser = Convert.ToInt32(this.Zaehler);
                    zahlKleiner = Convert.ToInt32(this.Nenner);
                }
                else
                {
                    zahlGroesser = Convert.ToInt32(this.Nenner);
                    zahlKleiner = Convert.ToInt32(this.Zaehler);
                }

                for (int i = zahlKleiner; i > 1; i--)
                {
                    if (zahlKleiner % i == 0 && zahlGroesser % i == 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        //Zugriffsmethode zum Erfragen des Bruchs wenn er gekürzt wurde
        //Liefert ein neues Objekt mit diesem gekürzen Bruch zurück
        public Bruch Gekuerzt
        {
            get
            {
                int zaehlerNeu;
                int nennerNeu;
                int zahlKleiner;
                int zahlGroesser;

                if (this.Zaehler > this.Nenner)
                {
                    zahlGroesser = Convert.ToInt32(this.Zaehler);
                    zahlKleiner = Convert.ToInt32(this.Nenner);
                }
                else
                {
                    zahlGroesser = Convert.ToInt32(this.Nenner);
                    zahlKleiner = Convert.ToInt32(this.Zaehler);
                }

                for (int i = zahlKleiner; i > 1; i--)
                {
                    if (zahlKleiner % i == 0 && zahlGroesser % i == 0)
                    {
                        zaehlerNeu = Convert.ToInt32(this.Zaehler / i);
                        nennerNeu = Convert.ToInt32(this.Nenner / i);
                        return new Bruch(zaehlerNeu, nennerNeu);
                    }
                }

                return new Bruch(Convert.ToInt32(this.Zaehler), Convert.ToInt32(this.Nenner));
            }
        }

        //Konstruktor für ein Bruchobjekt mit einer Ganzzahl als Parameter
        //der Parameter wird für den Nenner verwendet.
        public Bruch(int zahl)
        {
            this.zaehler = 1;
            this.nenner = zahl;
            this.dezimal = this.zaehler / this.nenner;
        }

        //Konstruktor für ein Bruchobjekt mit zwei Ganzzahlen als Parameter
        //Parameter 1 dient als Zähler, Parameter 2 als Nenner.
        public Bruch(int zahl1, int zahl2)
        {
            //Wird versucht durch 0 zu Teilen (parameter 2) -> Ausnahme auslösen
            if (zahl2 == 0)
            {
                throw new DivideByZeroException("Du Spaggn kannst nicht durch 0 teilen oida!");
            }
            else
            {
                this.zaehler = zahl1;
                this.nenner = zahl2;
                this.dezimal = this.zaehler / this.nenner;
            }
        }

        //Methode zum Addieren des Bruchobjekts mit einem objekt vom typ Bruch als Parameter
        public Bruch Add(Bruch obj)
        {
            int multiplikator;    //Neuer Zähler des Bruchs mit größerem Nenner wird hiermit multipliziert
            int eingangsNenner;   //Der Kleinere Nenner - der gemeinsame wird gebildet, indem der ursprüngliche vervielfacht wird
            int zielNenner;       //Der Größere der beiden Nenner - darüber wird der gemeinsame gesucht
            int gemeinsamerNenner;
            int neuerZaehler;     //Neuer Zähler für den Bruch mit dem kleineren Nenner - bildet sich aus Kleinerem Zähler * (gemeinsamer nenner / eingangsNenner)
            int meinZaehler;      //Zähler des Bruchs mit größerem Nenner
            int zaehler;
            int nenner;

            if (!(this.Nenner == obj.Nenner))
            {
                if (this.Nenner > obj.Nenner)
                {
                    zielNenner = Convert.ToInt32(this.Nenner);
                    meinZaehler = Convert.ToInt32(this.Zaehler);
                    gemeinsamerNenner = Convert.ToInt32(obj.Nenner);
                    eingangsNenner = Convert.ToInt32(obj.Nenner);
                    neuerZaehler = Convert.ToInt32(obj.Zaehler);
                }
                else
                {
                    zielNenner = Convert.ToInt32(obj.Nenner);
                    meinZaehler = Convert.ToInt32(obj.Zaehler);
                    gemeinsamerNenner = Convert.ToInt32(this.Nenner);
                    eingangsNenner = Convert.ToInt32(this.Nenner);
                    neuerZaehler = Convert.ToInt32(this.Zaehler);
                }

                while (!(gemeinsamerNenner % zielNenner == 0))
                {
                    gemeinsamerNenner = gemeinsamerNenner + eingangsNenner;
                }


                multiplikator = gemeinsamerNenner / zielNenner;

                neuerZaehler = neuerZaehler * (gemeinsamerNenner / eingangsNenner);
                meinZaehler = meinZaehler * multiplikator;

                zaehler = Convert.ToInt32(neuerZaehler + meinZaehler);
                nenner = Convert.ToInt32(gemeinsamerNenner);
                return new Bruch(zaehler, nenner);
            }
            else
            {
                zaehler = Convert.ToInt32(this.Zaehler + obj.Zaehler);
                nenner = Convert.ToInt32(this.Nenner);
                return new Bruch(zaehler, nenner);
            }
        }

        //Methode zur Addition des Bruchobjekts mit einer Zahl als Parameter
        //Dabei wird die reguläre Addition aufgerufen und als Argument
        //ein mithilfe des Parameter erzeugtes Bruchobjekt übergeben.
        public Bruch Add(int zahl)
        {

            return this.Add(new Bruch(zahl));
        }

        //Methode zur Substraktion des Bruchobjekts mit einem Bruchobjekt als Parameter
        //Kehrt den Bruch des Parameter objektes in einen negativen um und
        //ruft dann die reguläre Addition auf und übergibt als Parameter ein
        //aus den umgekehrten Werten erzeugtes Bruchobjekt
        public Bruch Sub(Bruch obj)
        {
            int neuerZaehler = Convert.ToInt32(obj.Zaehler * -1);
            int neuerNenner = Convert.ToInt32(obj.Nenner);

            return this.Add(new Bruch(neuerZaehler, neuerNenner));
        }

        //Methode zur Substraktion des Bruchobjekts mit einer Zahl als Parameter
        //Dabei wird die reguläre Substraktion aufgerufen und als Argument
        //ein mithilfe des Parameter erzeugtes Bruchobjekt übergeben.
        public Bruch Sub(int zahl)
        {

            return this.Sub(new Bruch(zahl));
        }

        //Methode zur Multiplikation des Bruchobjekts mit einem Bruchobjekt als Parameter
        public Bruch Mul(Bruch obj)
        {
            int zaehler = Convert.ToInt32(this.zaehler * obj.zaehler);
            int nenner = Convert.ToInt32(this.nenner * obj.nenner);
            return new Bruch(zaehler, nenner);
        }

        //Methode zur Multiplikation des Bruchobjekts mit einer Zahl als Parameter
        //Dabei wird die reguläre Multiplikation aufgerufen und als Argument
        //ein mithilfe des Parameter erzeugtes Bruchobjekt übergeben.
        public Bruch Mul(int zahl)
        {

            return this.Mul(new Bruch(zahl));
        }

        //Methode zur Division des Bruchobjekts mit einem Bruchobjekt als Parameter
        //Dabei wird die reguläre Multiplikation aufgerufen und als Argument die
        //Reziproke-Eigenschaft des Parameters (ein neuer umgekehrter Bruch basierend
        //auf dem des Parameters) übergeben.
        public Bruch Div(Bruch obj)
        {
            return Mul(obj.Reziproke);
        }

        //Methode zur Division des Bruchobjekts mit einer Zahl als Parameter
        //Dabei wird die reguläre Division aufgerufen und als Argument
        //ein mithilfe des Parameter erzeugtes Bruchobjekt übergeben.
        public Bruch Div(int zahl)
        {

            return this.Div(new Bruch(zahl));
        }

        //Methode zum erzeugen eines Bruchobjekts mit einem String als Parameter
        //Dabei wird zunächst der String an der stelle des angegebenen Zeichens,
        //hier '/' zerteilt und in einen Stringarray gelegt, der je Zeichenkette
        //VOR und NACH dem Zeichen ein Element enthält. Erwartet werden also 2 Elemente.
        //Anschließend wird das erste Element umgewandelt zu einem Integer als Zähler
        //das zweite Element wird umgewandelt zu einem Integer als Nenner
        //Zurück gegeben wird ein daraus neu erzeuges Bruchobjekt
        public static Bruch Parse(string textEingabe)
        {
            string[] res = textEingabe.Split(Convert.ToChar("/"));

            int zaehler = Convert.ToInt32(res[0]);
            int nenner = Convert.ToInt32(res[1]);
            return new Bruch(zaehler, nenner);
        }

        public static IComparer CreateComparer(string args)
        {
            return new BruchComparer(args);
        }

        //Implementierung von IComparer
        class BruchComparer : IComparer
        {
            protected string arg;
            public BruchComparer(string args)
            {
                this.arg = args;
            }

            public int Compare(object x, object y)
            {
                if (x is Bruch && y is Bruch)
                {
                    Bruch eins = x as Bruch;
                    Bruch zwei = y as Bruch;

                    switch (this.arg)
                    {
                        case "Dezimal":
                            return Convert.ToInt32(eins.dezimal - zwei.dezimal);

                        case "Nenner":
                            return Convert.ToInt32(eins.Nenner - zwei.Nenner);

                        case "Zaehler":
                            return Convert.ToInt32(eins.Zaehler - zwei.Zaehler);

                        case "ZahlAnzahl":
                            string einsLen = eins.Zaehler + "" + eins.Nenner;
                            string zweiLen = zwei.Zaehler + "" + zwei.Nenner;
                            int einsCount = einsLen.Length;
                            int zweiCount = zweiLen.Length;
                            return einsCount - zweiCount;

                        default:
                            throw new ArgumentException("Argument ist nicht bekannt!");
                    }
                }
                else
                {
                    throw new InvalidCastException("Objekt entspricht nicht dem erwarteten Typ.");
                }
            }
        }

        //Implementierung von IComparable um Brüche vergleichbar zu machen und somit Sortierbar
        public int CompareTo(Object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            //Gibt die Möglichkeit sämtliche Member der Bruch klasse auf den neuen Bezeichner anzuwenden
            //War das Objekt zuvor kein Bruch, lässt sich hiernach keine der ursprünglichen Member
            //über den neuen Bezeichner aufrufen!

            Bruch anderesObjekt = obj as Bruch;

            if (anderesObjekt != null)
            {
                return this.Dezimal.CompareTo(anderesObjekt.Dezimal);
            }
            else
            {
                throw new ArgumentException("Übergebenes Objekt ist nicht vom Typen 'Bruch' ");
            }
        }

        //Ab hier werden vorgegebene Dinge Überladen

        //Da jede eigene Klasse/Objekt zunächst vom Typen "Objekt" erbt,
        //welcher bereits die Methode "ToString()" virtuell implementiert,
        //für unser Bruchobjekt jedoch ein anderes Verhalten gewünscht wird als diese
        //Basis-implementierung vorsieht, müssen wir dieses Verhalten überschreiben
        //Dabei wird bei ausgabe des Objektbezeichners bei einem Nenner von 1 der
        //Zähler (also die ganze Zahl) ausgegeben und bei jedem anderen Nenner
        //der gesamte Bruch
        public override string ToString()
        {
            string erg;
            if (this.Nenner == 1)
            {
                erg = this.Zaehler.ToString();
            }
            else
            {
                erg = this.zaehler + "/" + this.nenner;
            }
            return erg;
        }

        //Überladen des regulären Additions-Operators, da dieser in seiner Basis-implementierung
        //nicht in der Lage ist, objekte vom Typ Bruch zu Addieren. Dabei wird unter Verwendung
        //des + operators die eigene Additions-implementierung Aufgerufen.
        //Der linke Operand ruft also die eigene Add(Bruch obj) Methode mit dem rechten Operanden als Argument auf.
        public static Bruch operator +(Bruch obj1, Bruch obj2)
        {
            return obj1.Add(obj2);
        }

        //Überladen des regulären Additions-Operators, da dieser in seiner Basis-implementierung
        //nicht in der Lage ist, objekte vom Typ Bruch zu Addieren. Dabei wird unter Verwendung
        //des + operators die eigene Additions-implementierung Aufgerufen.
        //Der linke Operand ruft also die eigene Add(int zahl) Methode mit dem rechten Operanden als Argument auf.
        public static Bruch operator +(Bruch obj1, int zahl)
        {
            return obj1.Add(zahl);
        }

        //Überladen des regulären Substraktions-Operators, da dieser in seiner Basis-implementierung
        //nicht in der Lage ist, objekte vom Typ Bruch zu Substrahieren. Dabei wird unter Verwendung
        //des + operators die eigene Substraktions-implementierung Aufgerufen.
        //Der linke Operand ruft also die eigene Sub(Bruch obj) Methode mit dem rechten Operanden als Argument auf.
        public static Bruch operator -(Bruch obj1, Bruch obj2)
        {
            return obj1.Sub(obj2);
        }

        //Überladen des regulären Substraktions-Operators, da dieser in seiner Basis-implementierung
        //nicht in der Lage ist, objekte vom Typ Bruch zu Substrahieren. Dabei wird unter Verwendung
        //des + operators die eigene Substraktions-implementierung Aufgerufen.
        //Der linke Operand ruft also die eigene Sub(int zahl) Methode mit dem rechten Operanden als Argument auf.
        public static Bruch operator -(Bruch obj1, int zahl)
        {
            return obj1.Sub(zahl);
        }

        //Überladen des regulären Multiplikations-Operators, da dieser in seiner Basis-implementierung
        //nicht in der Lage ist, objekte vom Typ Bruch zu Multiplizieren. Dabei wird unter Verwendung
        //des + operators die eigene Multiplikations-implementierung Aufgerufen.
        //Der linke Operand ruft also die eigene Mul(Bruch obj) Methode mit dem rechten Operanden als Argument auf.
        public static Bruch operator *(Bruch obj1, Bruch obj2)
        {
            return obj1.Mul(obj2);
        }

        //Überladen des regulären Multiplikations-Operators, da dieser in seiner Basis-implementierung
        //nicht in der Lage ist, objekte vom Typ Bruch zu Multiplizieren. Dabei wird unter Verwendung
        //des + operators die eigene Multiplikations-implementierung Aufgerufen.
        //Der linke Operand ruft also die eigene Mul(int zahl) Methode mit dem rechten Operanden als Argument auf.
        public static Bruch operator *(Bruch obj1, int zahl)
        {
            return obj1.Mul(zahl);
        }

        //Überladen des regulären Divisions-Operators, da dieser in seiner Basis-implementierung
        //nicht in der Lage ist, objekte vom Typ Bruch zu Dividieren. Dabei wird unter Verwendung
        //des + operators die eigene Divisions-implementierung Aufgerufen.
        //Der linke Operand ruft also die eigene Div(Bruch obj) Methode mit dem rechten Operanden als Argument auf.
        public static Bruch operator /(Bruch obj1, Bruch obj2)
        {
            return obj1.Div(obj2);
        }

        //Überladen des regulären Divisions-Operators, da dieser in seiner Basis-implementierung
        //nicht in der Lage ist, objekte vom Typ Bruch zu Dividieren. Dabei wird unter Verwendung
        //des + operators die eigene Divisions-implementierung Aufgerufen.
        //Der linke Operand ruft also die eigene Div(int zahl) Methode mit dem rechten Operanden als Argument auf.
        public static Bruch operator /(Bruch obj1, int zahl)
        {
            return obj1.Div(zahl);
        }

        public int this[string arg]
        {
            get
            {
                if (arg == "Nenner")
                {
                    return Convert.ToInt32(this.Nenner);
                }
                else if (arg == "Zähler")
                {
                    return Convert.ToInt32(this.Zaehler);
                }
                else
                {
                    throw new IndexOutOfRangeException("Argument außerhalb des Zulässigen bereichs!");
                }
            }
            set
            {
                if (arg == "Nenner")
                {
                    this.nenner = Convert.ToDecimal(value);
                }
                else if (arg == "Zähler")
                {
                    this.zaehler = Convert.ToDecimal(value);
                }
                else
                {
                    throw new IndexOutOfRangeException("Argument außerhalb des Zulässigen bereichs!");
                }
            }
        }
    }
}