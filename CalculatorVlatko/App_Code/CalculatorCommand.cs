using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CalculatorCommand  Ova e konkretna komanda
/// </summary>
/// // Ovaa klasa e izvedena od Klasata Command i implementira konkretna Naredba. Funkciite koi sto se izvrsuvaat direktno vlijaat vrz Receiverot t.e  objekti od Calculator clasata
public class CalculatorCommand : Command   //nasleduva od apstraktna klasa command
{
    private char znak_operator;
    private double operand1;
    private double operand2;
    private double rezultat;
    private Calculator calculator;
    // konstruktor na komandata
	  public CalculatorCommand(Calculator calculator,double operand1, double operand2,char znak_operator) {
        this.calculator = calculator;
        this.operand1 = operand1;
        this.operand2 = operand2;
        this.znak_operator = znak_operator;
        
    }
    //zadavanje na vrednosti na operandite i operacijata
public char Operator
  {
   set{ znak_operator = value; }
  }
 public int Operand1
  {
   set{ operand1 = value; }
  }
 public int Operand2
  {
   set{ operand2 = value; }
  }
    //prepokrivanje na apstraktnite f-cii 
    // ja izvrsuva operacijata  so povik do Receiverot t.e naredba , se vraka rezultat od operacijata
  public override double execute() {
        rezultat = calculator.izvrsiOperacija(operand1,operand2,znak_operator);
        return rezultat;
    }
    // se izvrsuva naredba za operacija kon rceiverot po obraten redosled t.e operand1 se zamenuva so rezultatot i se vrsat sprotivni operacii (sobiranje so mnozenje i sl.)
 // operand ostanuva ist. Se vrsat inverzni operacii od execute() f-jata.
    public override double unExecute() {
        rezultat = calculator.izvrsiOperacija(rezultat,operand2,undo(znak_operator));
        return rezultat;
    }
    // fja koga gi menuva znacite  vo zavisnost od operacijata vo nejzinata sprotivna.
private char undo(char znak_operator) {
        char undo;
        switch (znak_operator) {
            case '+':
                undo = '-';
                break;
            case '-':
                undo = '+';
                break;
            case '*':
                undo = '/';
                break;
            case '/':
                undo = '*';
                break;
            default:
                undo = ' ';
                break;
        }
        return undo;
    }
}



  
 
 
  

 



  

  

   

    