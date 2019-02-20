using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Originator, del od Memento Shablonot
/// </summary>
// Klasa cijasto sostojba treba da ja zacuvame. Samo ovaa klasa ja gleda klasata Memento koja e vo slucajov vgnezdena vo nea. Sostojbite na promenlivite od 
// Originator klasata se zacuvuvaat vo Memento klasata 
public class Originator
{ // Memento klasata sluzi za zacuvuvanje na  vrednostite od promenlivite od Originatorot
   public class Memento
    {
        public double zacuvanaVrednost;
       // cuvanje na vrednosta od originatorot
        public Memento(double vrednost)
        {
            zacuvanaVrednost = vrednost;
        }
    }

      double vrednostVoRegistarot;
    public  Originator(double vrednostVoRegistarot)
	{
		//
		// TODO: Add constructor logic here
		//
        this.vrednostVoRegistarot = vrednostVoRegistarot;
	}
    // sobiranje na vrednost so registarot 
    public void DodadiVrednostVoRegistarot(double vrednost)
    {
        this.vrednostVoRegistarot += vrednost;
    }
    //odzemanje na registarot so opredelena vrednost 
    public void OdzemiVrednostOdRegistarot(double vrednost)
    {
        this.vrednostVoRegistarot -= vrednost;
    }

   
    // f-ja sto vraka momentalna vrednost od originatorot
    public double VrednostVoRegisterot()
    {

        return vrednostVoRegistarot;
    
    }
    // f-ja koja ja zacuvuva vrednosta od originatorot kako memento objekt, se povikuva vo caretakerot
    public Memento save()
    {
      //  return new Memento(vrednostVoRegistarot);

        Memento memento = new Memento(vrednostVoRegistarot);
        return memento;
    }

    // f-ja za citanje na zacuvanata vrednost vo Memento objektot
    public void restore(Object objMemento)
    {
        Memento memento = (Memento)objMemento;
        vrednostVoRegistarot = memento.zacuvanaVrednost;
    }



}


