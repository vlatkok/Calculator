using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
/// <summary>
/// Summary description for    Invoker
/// </summary>
/// 
// Ovaa klasa e del od Command shablonot i  koja ja prasuva Command klasata za da gi izvrsi baranjata
// odlucuva koga nekoj metod treba da bide povikan, go poznava primacot na naredbite i gi izvrsuva naredbite vrz nego
public class Invoker
{
	 
    private Calculator calculator = new Calculator();     //objekt od Calculator za izvrsuvanje na operaciite
    private  static ArrayList commands = new ArrayList();      //lista vo koja se cuvaat izvrsenite operacii-komandi (potrebno za undo,redo)
    private  ArrayList commandsInProgram = new ArrayList();   //lista vo koja se cuvaat 10 operacii od programata za snimanje
    public static int MAX_COUNT = 10;
    private static int brVoLista = 0;

    // f-ja koja dava naredba za izvrsuvanje na naredna komanda, koja prethodno bila izvrsena, izvrsuvanjeto go dava krajniot rezultat od operacijata
    public double redo()
    {
        double temp=0;
            if (brVoLista < commands.Count) {
                Command command = commands[brVoLista++] as Command;
                temp = command.execute();
            }
        return temp;
    }

    // f-ja koja dava naredba za izvrsuvanje na prethodnata komanda(cekor nanazad), koja prethodno bila izvrsena, go dava rezultat od prethodnata operacija (+,-,*,/)
    public double undo() {
        double temp=0;
            if (brVoLista > 0) {        
                
                                             
               Command command = commands[--brVoLista] as Command;               
                temp = command.unExecute();
            }
        return temp;
    }

    //f-ja koja ja presmetuva operacijata vrz operandite, ja zacuvuva komandata vo lista na izvrseni operacii vrz operandite
    public double compute(double operand1, double operand2,char znak_operator) {
        Command command = new CalculatorCommand(calculator, operand1,operand2,znak_operator);
        double temp = command.execute();
        commands.Add((Command)command);
        ++brVoLista;
        return temp;
    }
    
    // dodava objekt od Command klasata, komandi koi sto treba da se zacuvaat za izvrsuvanje na programata
    public void addCommand(double operand1, double operand2,char znak_operator) {
        if(commandsInProgram.Count<=MAX_COUNT) {
            Command command = new CalculatorCommand(calculator, operand1,operand2,znak_operator);
            commandsInProgram.Add(command);

        }
               
    }
   
    // f-ja za izvrsuvanje na site komandi koi sto se zacuvani vo listata od programta (site 10 ili pomalku od 10)
    public double izvrsiPrograma(){
        double temp =0;
        for(int i=0;i<commandsInProgram.Count;i++){
            Command command = (Command)commandsInProgram[i];
            temp = command.execute();
        }
        return temp;
        
    }
}