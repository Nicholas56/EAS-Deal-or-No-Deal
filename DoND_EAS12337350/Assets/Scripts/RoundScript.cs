using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundScript : MonoBehaviour
{
    public List<int> picks;

    public GameObject pickbox;      //Box that simulates movement of the first box picked
    public GameObject dealBox;      //This is the button clicked to take deal
    bool FirstBoxPicked;            //A check for whether the fisrt box has been clicked
    public static float valuePick;  //This is the saved value of the first box picked
    public float offer;             //

	// Makes the special boxes disappear and sets up the first pick
	void Start ()
    {
        //Makes a list, with number of boxes picked per round
        picks = new List<int> { 3,5,4,5,4,3 };
        //Resets the conditions for the pick of the first box
        FirstBoxPicked = false;
        pickbox.SetActive(false);
        valuePick = 0;
    }
	
    //Function is called each time a box is clicked
    public void RoundEvent(GameObject btn)
    {
        //When only two boxes remain, calls the swap screen
        if (GameScript.boxes.Count == 2) { gameObject.GetComponent<GameScript>().SwapBoxes(null); return; }

        //ie. The first pick, only called the very first time a box is picked
        if (FirstBoxPicked == false)
        {
            //Makes the first box appear elsewhere, so it looks like it has moved
            pickbox.SetActive(true);
            //Records the picked value, despite still being used in the offer calculation
            valuePick = GameScript.boxes[0];
            //Adds the first box to the end of the list
            GameScript.boxes.Add(GameScript.boxes[0]);
            //Deletes the first box
            GameScript.boxes.RemoveAt(0);
            //Tells the game the first box has been picked
            FirstBoxPicked = true;            
            //Makes the button clicked inactive
            btn.SetActive(false);
        }
        else
        {
            //Resets offer for next round (offer is only available during deal stage)
            offer = 0;
            dealBox.GetComponent<BoxScript>().BoxValue(offer);

            //Shows number on box
            btn.GetComponent<BoxScript>().BoxNumber();
            //Removes picked box from possible boxes
            GameScript.boxes.RemoveAt(0);
            //Number of boxes to be picked is reduced by one, unless it is the last one
            if (picks[0] != 1)
            {                
                //Counts down boxes left from round
                picks[0] -= 1;
            }
            else
            {
                //Last box picked ends the round
                picks.RemoveAt(0);
                //Makes the deal panel visible
                this.GetComponent<GameScript>().dealPanel.SetActive(true);


                //          -This calculates the offer given by the Banker by looking at all the boxes remaining in the list -
                for (int i=0; i<GameScript.boxes.Count;i++)
                {
                    //Algorithm for calculating the offer   
                    float tempNumber = GameScript.boxes[i];
                    int round = 5 - picks.Count;
                    float calcNum = Mathf.Pow(30,Mathf.Pow(1.25f,0.76f*round)-1) - 1;
                    //For final round, if the values are too low, an average is given instead
                    if ((round == 5) && (150 > GameScript.boxes[0] + GameScript.boxes[1])) { offer = GameScript.boxes[0] + GameScript.boxes[1]; }
                    else
                    {
                        //If the number is a high value, Red values are ones over 1000, thus 1000 is considered red as it is over 999
                        if (tempNumber > 999)
                        {

                            tempNumber = tempNumber * (calcNum / 100);
                            offer = offer + tempNumber;
                        }
                        //If the number is a low value, Blue values are numbers under 1000, thus anything under 999 is considered blue
                        else
                        {
                            tempNumber = tempNumber * ((150 - calcNum) / 100);
                            offer = offer + tempNumber;
                        }
                    }
                }
                offer = offer / 2;
                //Shows the offer made in the offer box
                dealBox.GetComponent<BoxScript>().BoxValue(offer);
            }
        }
        //Update the lists after number has been chosen
        gameObject.GetComponent<GameScript>().makeLists();
    }
}
