using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Nicholas Easterby     EAS12337350
public class BoxScript : MonoBehaviour {

    //Public variable that holds the text for the button
    public Text boxTxt;
    //Boolian used for the offer buttons to determine whether offer has been made
    public bool offerMade;
    //Allows the script to alter button properties
    public Button myButton;

        //Shows the value as box is 'opened'
    public void BoxNumber()
    {
        //Selects the first number in the list of money values to be the value displayed
        boxTxt.text = "$" + GameScript.boxes[0];
        //Sets button to non-interactible, so no more clickin
        myButton.interactable = !myButton.interactable;
    }

    //Used for the offer box. Box appears when an offer is made
    public void BoxValue(float value)
    {
        if (value > 0) { offerMade = true; }
        else { offerMade = false; }
        //When offer is too small, number is not rounded down
        if (value < 1) { }
        else
        {
            value = Mathf.Floor(value);
            boxTxt.text = "The Banker has made you an offer of: $" + value.ToString("n0");
        }
    }    


}
