using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
// Klasa koja ima uloga na Klient na celiot proekt i od nea se povikuvaat naredbite  do drugite klasi
public partial class _Default : System.Web.UI.Page
{
   public static Invoker invoke = new Invoker(); //so samoto staratuvanje na klientot se kreira instanca od invoker klasata koja sto gi povikuva komandite
   public  static Caretaker caretaker = new Caretaker(); //instanca od Caretaker kalsata za vrsenje naredbi vrz originatorot
  public static  Originator originator; // objektot  cii sto vrednosti se zacuvuvaat
    
    // pomosni promenlivi
    string Displaytext;
   static  bool counter=false;   // pomosen brojac za vneseni operandi 
   static bool snimaj;  
   static int brojac; // broj na zacuvani komandi
   static bool lenta_visible=true; // vidliva e lentata
   static bool old_result;// promenliva vo koja ke se cuva dali na ekran ima rezultat ili pak vnesovi
    static bool znak; // promenliva koja ke oznacuva dali se ima vneseno znak na display
    double operand1;
    double operand2;
    double rezultat;
    char operacija;
    public double rez; // pomosna promenliva
   
    //se vcituva programata 
    protected void Page_Load(object sender, EventArgs e)
    {
        
              
        
        

    }

    // f-ja za pecatenje na display na vnesenite cifri ili tocka
    public void Number_Btn_Click(Object sender, EventArgs e)
    {
        if (old_result == true) // dokolku ima prethodno na display rezultat so vnes na cifra se brise prethodniot rezultat
        {                              // se oznacuva deka nema star rezultat na display
            txtDisplej.Text = "";
            old_result = false;
            DodadiVoLenta("Entering...");
        }
            if (counter == true) { counter = false; txtDisplej.Text = ""; }
            Button clickedButton = (Button)sender;
            txtDisplej.Text += clickedButton.Text;
           
        
       
    }
    //f-ja za pecatenje na display na vnesenite znaci za izvrsuvanje na operacijata vrz operandite
    public void Znak_BtnClick(Object sender, EventArgs e)
    {
        if (old_result == true) { old_result = false; }     // za star rezultat ovozmozi operacija vrz nego
        

            Button clickedButton = (Button)sender;
            char s = Convert.ToChar(clickedButton.Text.Trim());

            txtDisplej.Text += " " + s.ToString() + " ";
            counter = false;
            znak = true; // true e ako e vnesen znak na ekran
        
        
    }

    // funkcija koja gi zema od ekran operandite i znakot na opercijata i gi presmetuva rezultatite
    //Se povikuva invoke.compute() f-jata za presmetka na operacijata vrz operandite.
    //Vo zavisnost od toa dali e pritisnato kopceto za snimanje se vrsi dodavanje na maximum 10 komandi vo posebna lista,

    // se povikuva so funkcijata invoke.addCommand()
    //Sekoja od operciite se ispisuva na listbox lentata zaedno so rezultatite od operacijata
    public void Ednakvo_Btn_Click(Object sender, EventArgs e)
    {
        if (txtDisplej.Text != "")
        {
            string[] clenovi = (txtDisplej.Text.Trim()).Split(' ');
            if (clenovi.Length == 1) { txtDisplej.Text = txtDisplej.Text; }
            else if (clenovi.Length==2||(clenovi.Length>3)){ txtDisplej.Text = "ERROR"; }
            else
            {
                try
                {
                    operand1 = Convert.ToDouble(clenovi[0]);
                    operacija = Convert.ToChar(clenovi[1]);
                    operand2 = Convert.ToDouble(clenovi[2]);
                }
                catch (Exception a)
                {

                    txtDisplej.Text = "ERROR" + a.ToString().Trim();
                }


                double rezultat = invoke.compute(operand1, operand2, operacija);
                DodadiVoLenta(operand1.ToString() + " " + operacija.ToString() + " " + operand2.ToString());
                // proveruva dali e pritisnato kopceto za snimanje i dali ima pomalku
                if ((snimaj == true) && (brojac <= 10))
                {
                    invoke.addCommand(operand1, operand2, operacija);
                    brojac++;
                    lblsnimka.Text = "R";
                }
                else if (brojac > 10 || (brojac == null)) { snimaj = false; lblsnimka.Text = ""; }



                if ((operacija == '/') && (rezultat == -5)) { txtDisplej.Text = "Delenje so 0"; DodadiVoLenta("Rezultat= " + txtDisplej.Text); }
                else if (rezultat == -0.0001)
                {

                    if (operacija == '\0') { txtDisplej.Text = operand1.ToString(); }
                    else
                    {
                        txtDisplej.Text = "ERROR";
                        DodadiVoLenta("Rezultat= " + txtDisplej.Text);
                    }
                }
                else
                {
                    txtDisplej.Text = rezultat.ToString();
                    DodadiVoLenta("Rezultat= " + txtDisplej.Text);
                }
            }
            counter = true;
            old_result = true;
            znak = false;
        }
    }

    // brise display

    public void Clear_Displej(Object sender, EventArgs e)

    {
        txtDisplej.Text = "";
        DodadiVoLenta("Display cleared...");
    }
    // so klik na undo kopceto se vrakja prethodniot presmetan rezultat (neogranicen broj na nivoa ), se povikuva invokerot

    public void Undo(Object sender, EventArgs e)
    {
        txtDisplej.Text=Convert.ToString(invoke.undo());
        DodadiVoLenta("Undo operacija...");
        DodadiVoLenta("Rezultat= "+txtDisplej.Text);

    
    }
   // so klik na redo kopceto se prikazuva kon narednata izvrsena operacija (neogranicen broj na nivoa )
    public void Redo(Object sender, EventArgs e)
    {
        txtDisplej.Text = Convert.ToString(invoke.redo());
        DodadiVoLenta("Redo operacija...");
        DodadiVoLenta("Rezultat= " + txtDisplej.Text);
    }
    // gi prepozava kopcinjata za operaciite so memorijata-registerot
    // Tuka se izvrsuva klientot koj sto e del od Memento Shablonot implmentiran vo resnieto od zadacata, dodeka pak vo ostatokot od proektot 
    // se koristi Command zaedno so Memento shablonot
    public void MemoryOpButtons(Object sender, EventArgs e)
    {
        
        Button clickedButton = (Button)sender;
        if (clickedButton.Text.ToString() == "MS")    ///ako kliknato kopceto za zacuvuvanje vo memorija se sozdava objekt od originatorot i se zacuvuva sostojbata.
        {

             DodadiVoLenta("Set in Memory ");
            DodadiVoLenta(txtDisplej.Text.Trim().ToString());
            try { rez = Convert.ToDouble(txtDisplej.Text.Trim().ToString());
            originator = new Originator(rez);
            caretaker.saveState(originator);
            txtDisplej.Text = "";
            lblMemory.Text = "M";
            
            }
            catch (Exception ex) { DodadiVoLenta("Input Format Error..."); }
            //ja zacuvuva vrednosta od ekran
            
           
        }else
            if (clickedButton.Text.ToString() == "MC")   // se zadava 0 vrednost na orginatorot i se zacuvuva sostojbata
            { 
            //stava 0 vo memorijata
                double nula=0;
                originator=new Originator(nula);
                caretaker.saveState(originator);
                lblMemory.Text = "";
                txtDisplej.Text = "0";
                old_result = true;
                DodadiVoLenta(" Memory cleared,  Memory= " +nula.ToString());
            }
            else if (clickedButton.Text.ToString() == "MR")  // se cita od registerot, se restavrira vrednosta vo originatorot i se dodava na diplay
            {
                if (originator != null)
                {
                   string t = Convert.ToString(caretaker.restoreState(originator));
                   if (znak)
                   { txtDisplej.Text += t; }
                   else { txtDisplej.Text = t; }
                    lblMemory.Text = "M";
                    old_result = true;
                    DodadiVoLenta(" Memory Recall, vaulue= " + t);
                }
            }
            else if (clickedButton.Text.ToString() == "M+")  // se sobira vrednosta od ekran so vrednosta od registarot, prvo e vcituva,sobira pa zacuvuva. 
            {                                                  // zacuvuvanjeto se pravi vrz originatorot,   se zacuvuvs vo memento objekt
                if (originator != null)
                {
                    caretaker.restoreState(originator);
                   
                    try { rez=Convert.ToDouble(txtDisplej.Text.Trim().ToString());
                    originator.DodadiVrednostVoRegistarot(rez);
                    caretaker.saveState(originator);
                    caretaker.restoreState(originator);
                    txtDisplej.Text = Convert.ToString(originator.VrednostVoRegisterot());
                    old_result = true;
                    lblMemory.Text = "M";
                    DodadiVoLenta("Added to Memory , Memory= " + txtDisplej.Text);
                    }
                    catch (Exception ex) {DodadiVoLenta("Input Format Error..."); }
                    
                }
           
            }
        else if (clickedButton.Text.ToString() == "M-")// se odzema vrednosta od registarot so vrednosta na ekranot, redoledot e slicen na sobiranjeto 
        {
            if (originator != null)
            {
                caretaker.restoreState(originator);

                originator.OdzemiVrednostOdRegistarot(Convert.ToDouble(txtDisplej.Text.Trim().ToString()));
                caretaker.saveState(originator);
                caretaker.restoreState(originator);
                txtDisplej.Text = Convert.ToString(originator.VrednostVoRegisterot());
                old_result = true;
                lblMemory.Text = "M";
                DodadiVoLenta("Substracted from Memory , Memory= " + txtDisplej.Text);
            }

        }




    }


    // f-ja za aktiviranje na snimanjeto na programa, se ispisuvaat na ekran akciite
    public void SnimajPrograma(Object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        if (snimaj == false||(snimaj==null))
        { snimaj = true;
        lblsnimka.Text = "R";
        brojac = 0;
        clickedButton.BorderColor = Color.Orange;
        DodadiVoLenta("Program Recording Started...");
        } else if (snimaj = true)
        { 
            snimaj = false;
            lblsnimka.Text = "";
            clickedButton.BorderColor = Color.Black;
            DodadiVoLenta("Program Recording finished...");
        }
    
        
    }
    // f-ja koja ja povikuva klientot za izvrsuvanje na snimenata programa, invoke.izvrsiPrograma() vraka kraen rezultat od izvrsuvanjeto na programata
    public void IzvrsiPrograma(Object sender, EventArgs e)
    {
        if (snimaj == true) {

            snimaj = false;
            DodadiVoLenta("Program Recording finished...");
        }
        DodadiVoLenta("Executing program...");
       double rezultatKraj= invoke.izvrsiPrograma();
      
        if ((old_result == false) && (znak==true)) // ako nema rezultat od operacija na ekran, no ako e vnesen operand1 i znak na operacija, da se dodade izvrsuvanjeto na programata kako vtor operand
       {
           txtDisplej.Text += rezultatKraj.ToString();
           

       }
       else {       // se izvrsuva dokolku e vnesen samo operand na ekran
           txtDisplej.Text = rezultatKraj.ToString();
           DodadiVoLenta("Recorded Program  Result= " + txtDisplej.Text);
       }
               

    }
    // prikaz na lentata// listboxot
    public void  LentaONOFF(Object sender, EventArgs e)
    {
        if (lenta_visible == true)
        {
            lenta_visible = false;
            listboxLenta.Visible = false;
                
        
        }
        else if (lenta_visible == false)
        {

            lenta_visible = true;
            listboxLenta.Visible = true;
        
        }


    }

    // pomosna f-ja koja dodava tekst vo listata-lentata
    public void DodadiVoLenta(string tekst)
    {     if (lenta_visible == true )
        {listboxLenta.Items.Add(tekst);
        listboxLenta.SelectedIndex = listboxLenta.Items.Count - 1;
        }
       
    }


   
}