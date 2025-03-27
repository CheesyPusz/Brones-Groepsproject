using UnityEngine;
using TMPro;
using System.Diagnostics.Tracing;
using UnityEngine.InputSystem;
using UnityEngine.WSA;
using UnityEngine.SceneManagement;
public class IntroManager : MonoBehaviour
{
    private int Count;
    public TMP_Text m_Text;
    private string Name;
    private string route;
    private string Text;
    private void Update() // zorgt datde Change methode ieder frame word aangeroepen zodat de juiste tekst wordt weergegeven
    {
        Change();
    }
    private void Start()//Zorgt dat als de scene word geladen de juiste gegevens worden opgehaald
    {
        TestGegevens();
        //Naam kind ophalen
        //route ophalen
    }
    public void Logout()                                                                                                                                                                                   
    {
        ClearTokens(); // Verwijdert tokens
        SceneManager.LoadScene("LoginScene"); // Laad het inlogscherm
    }

    public void ClearTokens() //verwijdert de tokens die opgelsagen zijn bij het inloggen
    {
        Debug.Log("Succesvol uitgelogd");
        PlayerPrefs.DeleteKey("accessToken");
        PlayerPrefs.DeleteKey("refreshToken");
        PlayerPrefs.Save();
    }
    public void TestGegevens() // deze gegevns worden gebruikt om te testen voordat de api werkt
    {
        Count = 0;
        Name = "Test123";
        route = "A";
        //route = "B";
    }
    public void IncreaseCount() //
    {
        Count++;
        //Change();
    }
    public void CountBack()
    {
        Count--;
        //Change();
    }
    private void Change() // deze methode zorgt dat voor elke keer als er geklikt word de juiste tekst word geladen
    {
            if (Count == 0) { m_Text.text = Text1();}
            else if (Count == 1) { m_Text.text = Text2();}
            else if (Count == 2) { m_Text.text = Text3();}
            else if (Count == 3) { m_Text.text = Text4();}
            else if (Count == 4) { m_Text.text = Text5(); }
            else if (Count == 5) { m_Text.text = Text6(); }
            else if (Count == 6) { m_Text.text = Text7(); }
            else if (Count == 7 && route =="B") { SceneManager.LoadScene("Route B"); } //de Route worden na de tekst geladen
            else if (Count == 7 && route =="A") { SceneManager.LoadScene("Route A"); }
    }
    // de methodes waar de Teksten in staan:
    #region Texten 
    private string Text1()
    {
        return $"Hallo {Name}!!\nIk ben Jeff en heb mijn spaakbeen gebroken bij de judo ik loop met je mee door de stappen van jouw proces ";
    }
    private string Text2()
    {
        if (route == "B")
        {
            return $"Je hebt iets ernstig gebroken en moet geopereerd worden door een dokter daarom ben je vast een beetje bang";
        }
        else 
        {
            return $"Je hebt iets gebroken maar gelukkig is het niet heel erg  ";
        }
    }
    private string Text3()
    {
        return $"Maar maak je niet druk \nJe bent in goede handen en hoeft je nergens druk over te maken";
    }

    private string Text4()
    {
        return $"In deze app gaan wij jou stap voor stap uitleggen hoe jouw bot gaat genezen door samen met mij route {route} te belopen.";
        
    }
    private string Text5()
    {
        return $"Als het goed is heeft een ouder je net geholpen met in te loggen. Vraag of die ook mee wil kijken naar het spel";
    }
    private string Text6()
    {
        return $"Bewegen doe je met de pijltjes toetsen voor links en naar rechts of met de knoppen A en D op het toetsenbord!!";
    }
    private string Text7() 
    {
        return $"Onderweg Komen we stukjes informatie tegen. Door weer door te bewegen verdwijnen deze automatisch!\n {Name} heel veel succes";
    }
    #endregion
}