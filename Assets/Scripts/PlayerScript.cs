using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    [SerializeField] int defense;
    [SerializeField] int attack;
    [SerializeField] int maxMP;
    [SerializeField] int currentMP;

    AudioSource audi;
    
    bool yourTurn = true;

    bool fightActive = true;
    bool magicActive = false;
    bool itemActive = false;

    [SerializeField] GameObject fightButton;
    [SerializeField] GameObject magicButton;

    Vector3 fightPos;
    Vector3 magicPos;

    void Start()
    {
        audi = GetComponent<AudioSource>();
        fightPos = fightButton.transform.position;
        magicPos = magicButton.transform.position;
    }
    void Update()
    {
        buttonAppear();
        debugging();
        selection();
    }
    // determines when the fight/magic button appears
    void buttonAppear()
    {

        if (fightActive)
        {
            fightButton.transform.position = fightPos + fightButton.transform.up;
            magicButton.transform.position = magicPos;
        }
        else
        {
            fightButton.transform.position = fightPos;
            magicButton.transform.position = magicPos + magicButton.transform.up;
        }
        if (yourTurn)
        {
            fightButton.gameObject.SetActive(true);
            magicButton.gameObject.SetActive(true);
        }
        else
        {
            fightButton.gameObject.SetActive(false);
            magicButton.gameObject.SetActive(false);
        }
    }
    // actual fight command
    void selection()
    {
        if (yourTurn) { // makes sure it's actually player turn
            if (Input.GetButtonDown("Space"))
            {
                if (fightActive)
                {
                    //perform fight here
                    Debug.Log("You chose to fight.");
                    yourTurn = false;
                }
                else if (magicActive)
                {
                    // perform magic here
                    Debug.Log("You chose to use magic.");
                    yourTurn = false;
                }
                else // item
                {
                    Debug.Log("You chose to use an item.");
                    yourTurn = false;
                }
            }
            if (Input.GetButtonDown("Right") || Input.GetButtonDown("Left"))
            {
                audi.Play();
                if (fightActive)
                {
                    fightActive = false;
                    magicActive = true;
                }
                else
                {
                    fightActive = true;
                    magicActive = false;
                }
            }
        }
    }
    // debugging commands
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
