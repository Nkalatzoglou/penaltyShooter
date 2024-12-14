using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu_Handler : MonoBehaviour
{
    // Tworzymy  zmienną  w celu późniejszego wywołania
    public static MainMenu_Handler instance;
    [Header("Serialised Attributes")]
    public GameObject MainMenu;

    public TextMeshProUGUI score;
    // Start jest wywoływany przed aktualizacją pierwszej ramki
    void Awake()
    {

        //Przypisujemy ten skrypt do zmiennej
        //Teraz możemy wywołać go z dowolnego miejsca, o ile ten skrypt jest aktywny
        instance= this;
    }

    //Służy do aktywowania głównego menu i dezaktywacji
    public void Activate_MainMenu()
    {
        DisActivateAll();
        MainMenu.SetActive(true);
    }

     //Metoda, która jest używana, gdy naciskamy przycisk wyjścia w menu głównym
    public void ExitGame()
    {
        Application.Quit();
    }


    //W tej metodzie dezaktywujemy wszystkie komponenty.

    public void DisActivateAll()
    {
        MainMenu.SetActive(false);
    }


    //Metoda używana po naciśnięciu przycisku Rozpocznij grę w menu głównym
    public void StartGame()
    {

        LevelManager.instance.LoadScene("GameScene");
    }

}