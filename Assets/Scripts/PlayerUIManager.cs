using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{

    [SerializeField] GameObject fightButton;
    [SerializeField] GameObject magicButton;
    [SerializeField] GameObject itemButton;
    [SerializeField] GameObject fleeButton;

    AudioSource audi;

    Vector3 slot0, slot1, slot2, slot3;
    // goes from 0 -> 1 -> 2 -> 3 -> 0 -> ...
    //
    //    [2]
    // [3]   [1] 
    //    [0]

    bool yourTurn = true;

    int action = 0; // can be used to determine which is active. 
    /* 0 = fight
     * 1 = magic
     * 2 = item
     * 3 = flee
     */

    // Start is called before the first frame update
    void Start()
    {
        audi = GetComponent<AudioSource>();
        slot0 = fightButton.transform.position;
        slot1 = magicButton.transform.position;
        slot2 = itemButton.transform.position;
        slot3 = fleeButton.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        buttonAppear();
        selection();
        debugging();
    }

    void buttonAppear()
    {
        switch (action)
        {
            case 0: // fight select
                fightButton.transform.position = slot0;
                magicButton.transform.position = slot1;
                itemButton.transform.position = slot2;
                fleeButton.transform.position = slot3;
                break;
            case 1: // magic select
                fightButton.transform.position = slot3;
                magicButton.transform.position = slot0;
                itemButton.transform.position = slot1;
                fleeButton.transform.position = slot2;
                break;
            case 2: // item select
                fightButton.transform.position = slot2;
                magicButton.transform.position = slot3;
                itemButton.transform.position = slot0;
                fleeButton.transform.position = slot1;
                break;
            case 3: // flee select
                fightButton.transform.position = slot1;
                magicButton.transform.position = slot2;
                itemButton.transform.position = slot3;
                fleeButton.transform.position = slot0;
                break;
            default: // failsafe
                Debug.LogError("you shouldn't be here.");
                break;
        }

        if (yourTurn)
        {
            fightButton.gameObject.SetActive(true);
            magicButton.gameObject.SetActive(true);
            itemButton.gameObject.SetActive(true);
            fleeButton.gameObject.SetActive(true);
        }
        else
        {
            fightButton.gameObject.SetActive(false);
            magicButton.gameObject.SetActive(false);
            itemButton.gameObject.SetActive(false);
            fleeButton.gameObject.SetActive(false);
        }
    }

    void selection()
    {

        if (!yourTurn)// makes sure it's actually player turn
        {
            return;
        }

        if (Input.GetButtonDown("Space"))
        {
            switch (action)
            {

                case 0:
                    //perform fight here
                    Debug.Log("you chose to fight.");
                    yourTurn = false;
                    break;
                case 1:
                    // perform magic here
                    Debug.Log("you chose to use magic.");
                    yourTurn = false;
                    break;
                case 2:
                    // item here
                    Debug.Log("you chose to use an item.");
                    yourTurn = false;
                    break;
                case 3:
                    // run away
                    Debug.Log("you chose to run away.");
                    break;
                default: // failsafe
                    Debug.LogError("you shouldn't be here.");
                    break;

            }
            //if (fightActive)
            //{
            //    //perform fight here
            //    Debug.Log("You chose to fight.");
            //    yourTurn = false;
            //}
            //else if (magicActive)
            //{
            //    // perform magic here
            //    Debug.Log("You chose to use magic.");
            //    yourTurn = false;
            //}
            //else // item
            //{
            //    Debug.Log("You chose to use an item.");
            //    yourTurn = false;
            //}
        }

        if (Input.GetButtonDown("Left")) {
            audi.Play();
            switch (action)
            {
                case 0:
                    action = 3;
                    break;
                case 1:
                    action = 0;
                    break;
                case 2:
                    action = 1;
                    break;
                case 3:
                    action = 2;
                    break;
            }
        }

        if (Input.GetButtonDown("Right"))
        {
            audi.Play();
            switch (action)
            {
                case 0: // fight turns to magic
                    action = 1;
                    break;
                case 1: // magic turns to item
                    action = 2;
                    break;
                case 2: // item turns to flee
                    action = 3;
                    break;
                case 3: // flee turns to fight
                    action = 0;
                    break;
            }
        }
    }
    void debugging()
    {
        if (Input.GetButtonDown("Debug1"))
        {
            Debug.Log("Switching Turn");
            if (yourTurn)
            {
                Debug.Log("Enemy Turn, icons should be off");
                yourTurn = false;
            }
            else
            {
                Debug.Log("Player Turn, icons should be on");
                yourTurn = true;
            }
        }
    }

}
