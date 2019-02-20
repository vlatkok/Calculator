using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Calculator /// Ova e Receiver
/// </summary>
// tuka se odviva glavnata logika, operaciite za sobiranje,mozenje,odzemanje i delenje na dva operandi i znae kaka da se izvrsat operaciite 
public class Calculator
{
	    // sluzat za registriranje na greski
       double delenje_so_0 = (double) -5;
   double greska = (double) -0.0001;

    public double tmp= 0;

    //  f-ja  koja ja izvrsuva operacija vrz dva operandi
    public double izvrsiOperacija(double operand1, double operand2, char znak_operator) {
        
		if(znak_operator=='+')  tmp = operand1 + operand2;
		 if(znak_operator=='-')  tmp = operand1 - operand2;
		 if(znak_operator=='*')  tmp = operand1 * operand2;
		 if(znak_operator=='/')
             // proverka za nepravilni vnesovi na operandite
{ if(operand2 == 0){  tmp = delenje_so_0;}
     if(operand2!=0) { tmp = operand1 / operand2;}
		};

         if ((znak_operator != '+') && (znak_operator != '-') && (znak_operator != '*') && (znak_operator != '/'))		
{     tmp = greska;}
         
            return tmp;
    }

}



