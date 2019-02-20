using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Command // komanda
/// </summary>
/// // Se definira apstraktna klasa vo koja se zadavaat apstraktni f-cii koi sto treba da se implementiraat vo izvedenite klasi
 // od ovaa klasa se izveduvaat razlicnite komandi 
public abstract class Command
{
   public abstract double execute();
   public abstract double unExecute();
}