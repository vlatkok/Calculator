using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Caretaker
/// </summary>

// Klasa vo koja  objektite znaat koga da se zacuva objektot od Originator ili da se procita zacuvanata sostojba
// Del e od Memento Shablonot i sluzi kako posrednik pomegu klientot i Originatorot
// Ne ja gledsa klasta Memento kade sto se zacuvuvaat objektite od Originatorot
//Cuva Memento objekt od zacuvuvanjeto
public class Caretaker
{
    Object objMemento;
    // f-ja za samozacuvuvanje na originator objektot
    public void saveState(Originator originator)
    {
        objMemento = originator.save();
    }
    //f-ja za citanje na zacuvanata vrednost od originatorot, ja vrakja  decimalnata zacuvana vrednost
    public double restoreState(Originator originator)
    {
        originator.restore(objMemento);
        return originator.VrednostVoRegisterot();
    }

}