using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Nicholas Easterby     EAS12337350
public class GameScript : MonoBehaviour
{
    //List holding the numbers, accessible from any script
    public static List<float> boxes;
    //Text altered by the number list of remaining values
    public Text moneyTxtRed;        public Text moneyTxtBlue;       public Text moneyText;
    //Panels used to interrupt gameplay and present choices or messages
    public GameObject swapPanel;    public GameObject winPanel;     public GameObject dealPanel;

    // Sets up the box list and hides the end game panels
    void Start ()
    {
        //This declares all the numbers to be used in the game
        boxes = new List<float>
        {
            1000000,750000,500000,400000,300000,200000,100000,75000,50000,25000,10000,5000,1000,
            750,500,400,300,200,100,75,50,25,10,5,1,0.01f
        };
        
        //Randomizes the list of numbers, so pick is random
        for (int i = 0; i < boxes.Count; i++)
        {
            int randNumber = Random.Range(0, boxes.Count);
            float savedValue = boxes[randNumber];
            //Holds one number, then swaps it with a randomly picked remaining one
            boxes[randNumber] = boxes[i];
            boxes[i] = savedValue;
        }
        //Hides the panels, to be called later
        swapPanel.SetActive(false);        winPanel.SetActive(false);        dealPanel.SetActive(false);
        //Sets up the side panels for the first time
        makeLists();
    }

    // Update to populate the money lists with red and blue values
    public void makeLists ()
    {
        //Clears the lists   
        moneyTxtRed.text = ""; moneyTxtBlue.text = "";
        //Orders the boxes, then splits them into two new lists 
        List<float> temporary2 = new List<float>();
        List<float> temporary = new List<float>();
        //Checks the values in the remaining numbers and sorts them into high and low numbers
        for (int i = 0; i < boxes.Count; i++)
        {
            //A blue number is less than 1000(<999), red is higher(>999). first list is composed of red only numbers, second list is blue only
            if (boxes[i] > 999) { temporary.Add(boxes[i]); }
            else { temporary2.Add(boxes[i]); }
        }
        //Sorts the two lists into order
        temporary.Sort();temporary2.Sort();
        //Calls the function to display the lists
        MoneyList(temporary);
        MoneyList(temporary2);
    }

    //Outputs the lists made in makeList()
    public void MoneyList(List<float> newList)
    {
        for (int i = 0; i < newList.Count; i++)
        {
            //Checks if the list is high values and puts it on the red list, otherwise onto the blue list
            if (newList[0] > 999) { moneyTxtRed.text = moneyTxtRed.text + "$" + newList[i] + "\n"; }
            else { moneyTxtBlue.text = moneyTxtBlue.text + "$" + newList[i] + "\n"; }
        }
    }   
    
    //Begins the swap scene and handles the final choice
    public void SwapBoxes(GameObject btn)
    {
        swapPanel.SetActive(true);
        //Offer the swap at the end of the game
        if (btn != null)
        {
            //Checks the text component of the box clicked, and performs the associated action
            if (btn.GetComponent<BoxScript>().boxTxt.text == "Swap")
            //Outputs the final unpicked box
            {WinGame(boxes[0]); }
            else if (btn.GetComponent<BoxScript>().boxTxt.text == "Don't Swap")
            //Outputs the initially picked box
            {WinGame(RoundScript.valuePick); }
        }
    }

    //Displays the last scene and how much money you get
    public void WinGame(float money)
    {
        winPanel.SetActive(true);
        //This is only needed and works if 0.01 is the only decimal value in the list
        if (money != 0.01f)
        {
            //Rounds the value of the number down with no decimal places
            moneyText.text = "$" + Mathf.Floor(money).ToString("n0");
        }
        //If the value is 0.01, it does not round down
        else
        {
            moneyText.text = "$0.01";
        }
    }

    //If offer button is clicked, displays win screen with offer
    public void TakeOffer(GameObject btn)
    {
        //closes the deal panel
        dealPanel.SetActive(false);
        //If the button is the deal button, shows win screen
        if (btn.GetComponent<BoxScript>().offerMade == true)   
            //Activates the win function using the offer as the displayed value
        { WinGame(GetComponent<RoundScript>().offer); }
    }
}

