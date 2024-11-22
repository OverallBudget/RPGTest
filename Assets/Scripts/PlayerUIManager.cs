using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{

    [SerializeField] GameObject fightButton;
    [SerializeField] GameObject magicButton;
    [SerializeField] GameObject itemButton;
    [SerializeField] GameObject fleeButton;
    [SerializeField] GameObject selector;
    [SerializeField] GameObject selector2;
    [SerializeField] GameObject selector3;
    [SerializeField] GameObject selector4;
    [SerializeField] GameObject magicSelector;
    [SerializeField] GameObject magicPanel;
    [SerializeField] GameObject itemSelector;
    [SerializeField] GameObject itemPanel;

    [SerializeField] TextMeshProUGUI actionDescript;
    [SerializeField] BattleSystem battleSystem;
    
    [SerializeField] Transform enemy1Location;
    [SerializeField] Transform enemy2Location;
    [SerializeField] Transform enemy3Location;
    [SerializeField] Transform enemy4Location;
    [SerializeField] Transform magicLocation;

    [SerializeField] List<MagicScript> MagicAttack;

    public BattleState state; // STATES START, PLAYERTURN, ENEMYTURN, WIN, LOSE, FIGHT

    bool isChoosingFight = false;
    bool isChoosingMagic = false;
    bool isChoosingMagic2 = false;
    bool isChoosingItem = false;
    public AudioClip swap; // swap icon
    public AudioClip select; // select icon
    public AudioClip back; // back out of choice
    public AudioSource audi;
    float offset = 1f;


    Vector3 slot0, slot1, slot2, slot3;
    // goes from 0 -> 1 -> 2 -> 3 -> 0 -> ...
    //
    //    [2]
    // [3]   [1] 
    //    [0]

    int action = 0; // can be used to determine which is active. 
    /* 0 = fight
     * 1 = magic
     * 2 = item
     * 3 = flee
     */
    int savedAction;
    int magic = 0;
    int item = 0;

    // Start is called before the first frame update
    void Start()
    {
        audi = GetComponent<AudioSource>();
        slot0 = fightButton.transform.position;
        slot1 = magicButton.transform.position;
        slot2 = itemButton.transform.position;
        slot3 = fleeButton.transform.position;
        magicPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        buttonAppear();
        selection();
        debugging();
        if (isChoosingFight)
        {
            chooseFight();
        }
        if (isChoosingMagic)
        {
            chooseMagic();
        }
        if (isChoosingMagic2)
        {
            chooseMagicFight();
        }
    }

    void buttonAppear()
    {
        if (!isChoosingFight && !isChoosingMagic && !isChoosingMagic2)
        {
            switch (action)
            {
                case 0: // fight select
                    fightButton.transform.position = slot0;
                    magicButton.transform.position = slot1;
                    itemButton.transform.position = slot2;
                    fleeButton.transform.position = slot3;
                    actionDescript.text = "Fight";
                    break;
                case 1: // magic select
                    fightButton.transform.position = slot3;
                    magicButton.transform.position = slot0;
                    itemButton.transform.position = slot1;
                    fleeButton.transform.position = slot2;
                    actionDescript.text = "Magic";
                    break;
                case 2: // item select
                    fightButton.transform.position = slot2;
                    magicButton.transform.position = slot3;
                    itemButton.transform.position = slot0;
                    fleeButton.transform.position = slot1;
                    actionDescript.text = "Items";
                    break;
                case 3: // flee select
                    fightButton.transform.position = slot1;
                    magicButton.transform.position = slot2;
                    itemButton.transform.position = slot3;
                    fleeButton.transform.position = slot0;
                    actionDescript.text = "Flee";
                    break;
                default: // failsafe
                    Debug.LogError("you shouldn't be here.");
                    break;
            }
        }
        else
        {
            switch (action)
            {
                case 1: 
                    actionDescript.text = battleSystem.enemy1Name;
                    break;
                case 2: 
                    actionDescript.text = battleSystem.enemy2Name;
                    break;
                case 3: 
                    actionDescript.text = battleSystem.enemy3Name;
                    break;
                case 4: 
                    actionDescript.text = battleSystem.enemy4Name;
                    break;
            }
        }

        if (battleSystem.state == BattleState.PLAYERTURN && !isChoosingFight && !isChoosingMagic && !isChoosingMagic2)
        {
            fightButton.gameObject.SetActive(true);
            magicButton.gameObject.SetActive(true);
            itemButton.gameObject.SetActive(true);
            fleeButton.gameObject.SetActive(true);
            actionDescript.gameObject.SetActive(true);
        }
        else if(isChoosingFight || isChoosingMagic2)
        {
            fightButton.gameObject.SetActive(false);
            magicButton.gameObject.SetActive(false);
            itemButton.gameObject.SetActive(false);
            fleeButton.gameObject.SetActive(false);
        }
        else
        {
            fightButton.gameObject.SetActive(false);
            magicButton.gameObject.SetActive(false);
            itemButton.gameObject.SetActive(false);
            fleeButton.gameObject.SetActive(false);
            actionDescript.gameObject.SetActive(false);
        }
    }

    void selection()
    {

        if (battleSystem.state != BattleState.PLAYERTURN || isChoosingFight || isChoosingMagic || isChoosingMagic2 || isChoosingItem) // makes sure it's actually player turn
        {
            return;
        }

        if (Input.GetButtonDown("Space"))
        {
            audi.PlayOneShot(select);
            savedAction = action;
            if (battleSystem.enemy1Living)
            {
                Vector3 newPos = new Vector3(enemy1Location.position.x, enemy1Location.position.y + offset, enemy1Location.position.z);
                selector.transform.position = newPos;
            }
            else if (battleSystem.enemy2Living)
            {
                Vector3 newPos = new Vector3(enemy2Location.position.x, enemy2Location.position.y + offset, enemy2Location.position.z);
                selector.transform.position = newPos;
            }
            else if (battleSystem.enemy3Living)
            {
                Vector3 newPos = new Vector3(enemy3Location.position.x, enemy3Location.position.y + offset, enemy3Location.position.z);
                selector.transform.position = newPos;
            }
            else
            {
                Vector3 newPos = new Vector3(enemy4Location.position.x, enemy4Location.position.y + offset, enemy4Location.position.z);
                selector.transform.position = newPos;
            }
            switch (action)
            {
                
                case 0:

                    //perform fight here
                    action += 1;
                    Debug.Log("you chose to fight.");
                    isChoosingFight = true;
                    Input.ResetInputAxes();
                    selector.SetActive(true);
                    
                    break;
                case 1:
                    // perform magic here
                    action += 1;
                    Debug.Log("you chose to use magic.");
                    magic = 0;
                    isChoosingMagic = true;
                    Input.ResetInputAxes();
                    magicPanel.SetActive(true);
                    break;
                case 2:
                    // item here
                    action += 1;
                    Debug.Log("you chose to use an item.");
                    isChoosingItem = true;
                    Input.ResetInputAxes();
                    itemPanel.SetActive(true);

                    break;
                case 3:
                    // run away
                    Debug.Log("you chose to run away.");
                    SceneChanger.instance.ChangeScene("OverworldTest");
                    break;
                default: // failsafe
                    Debug.LogError("you shouldn't be here.");
                    break;

            }
        }

        if (Input.GetButtonDown("Left")) {
            audi.PlayOneShot(swap);
            switch (action)
            {
                case 0: // fight turns to flee
                    action = 3;
                    break;
                case 1: // magic to fight
                    action = 0;
                    break;
                case 2: // items to magic
                    action = 1;
                    break;
                case 3: // flee to items
                    action = 2;
                    break;
            }
        }

        if (Input.GetButtonDown("Right"))
        {
            audi.PlayOneShot(swap);
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

    void chooseFight()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            audi.PlayOneShot(back);
            selector.SetActive(false);
            selector2.SetActive(false);
            selector3.SetActive(false);
            selector4.SetActive(false);
            action = savedAction;
            isChoosingFight = false;
        }
        if (Input.GetButtonDown("Space"))
        {
            selector.SetActive(false);
            selector2.SetActive(false);
            selector3.SetActive(false);
            selector4.SetActive(false);
            audi.PlayOneShot(select);
            battleSystem.onFight(action);
            battleSystem.state = BattleState.ENEMYTURN;
            isChoosingFight = false;
            action = savedAction;
        }
        if (Input.GetButtonDown("Left"))
        {
            if (action == 1)
            {
                if (!battleSystem.enemy2Living && !battleSystem.enemy3Living && !battleSystem.enemy4Living) // if only enemy1 is living
                {
                    return;
                }
                else
                {
                    audi.PlayOneShot(swap);
                    if (battleSystem.enemy4Living)
                    {
                        Vector3 newPos = new Vector3(enemy4Location.position.x, enemy4Location.position.y + offset, enemy4Location.position.z);
                        selector.transform.position = newPos;
                        action = 4;
                    }
                    else if (battleSystem.enemy3Living)
                    {
                        Vector3 newPos = new Vector3(enemy3Location.position.x, enemy3Location.position.y + offset, enemy3Location.position.z);
                        selector.transform.position = newPos;
                        action = 3;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(enemy2Location.position.x, enemy2Location.position.y + offset, enemy2Location.position.z);
                        selector.transform.position = newPos;
                        action = 2;
                    }
                }
            }
            else if (action == 2)
            {
                if (!battleSystem.enemy1Living && !battleSystem.enemy3Living && !battleSystem.enemy4Living) // if only enemy2 is living
                {
                    return;
                }
                else
                {
                    audi.PlayOneShot(swap);
                    if (battleSystem.enemy1Living)
                    {
                        Vector3 newPos = new Vector3(enemy1Location.position.x, enemy1Location.position.y + offset, enemy1Location.position.z);
                        selector.transform.position = newPos;
                        action = 1;
                    }
                    else if (battleSystem.enemy4Living)
                    {
                        Vector3 newPos = new Vector3(enemy4Location.position.x, enemy4Location.position.y + offset, enemy4Location.position.z);
                        selector.transform.position = newPos;
                        action = 4;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(enemy3Location.position.x, enemy3Location.position.y + offset, enemy3Location.position.z);
                        selector.transform.position = newPos;
                        action = 3;
                    }
                }
            }
            else if (action == 3)
            {
                if (!battleSystem.enemy2Living && !battleSystem.enemy1Living && !battleSystem.enemy4Living) // if only enemy3 is living
                {
                    return;
                }
                else
                {
                    audi.PlayOneShot(swap);
                    if (battleSystem.enemy2Living)
                    {
                        Vector3 newPos = new Vector3(enemy2Location.position.x, enemy2Location.position.y + offset, enemy2Location.position.z);
                        selector.transform.position = newPos;
                        action = 2;
                    }
                    else if (battleSystem.enemy1Living)
                    {
                        Vector3 newPos = new Vector3(enemy1Location.position.x, enemy1Location.position.y + offset, enemy1Location.position.z);
                        selector.transform.position = newPos;
                        action = 1;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(enemy4Location.position.x, enemy4Location.position.y + offset, enemy4Location.position.z);
                        selector.transform.position = newPos;
                        action = 4;
                    }
                }
            }
            else
            {
                if (!battleSystem.enemy2Living && !battleSystem.enemy3Living && !battleSystem.enemy1Living) // if only enemy4 is living
                {
                    return;
                }
                else
                {
                    audi.PlayOneShot(swap);
                    if (battleSystem.enemy3Living)
                    {
                        Vector3 newPos = new Vector3(enemy3Location.position.x, enemy3Location.position.y + offset, enemy3Location.position.z);
                        selector.transform.position = newPos;
                        action = 3;
                    }
                    else if (battleSystem.enemy2Living)
                    {
                        Vector3 newPos = new Vector3(enemy2Location.position.x, enemy2Location.position.y + offset, enemy2Location.position.z);
                        selector.transform.position = newPos;
                        action = 2;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(enemy1Location.position.x, enemy1Location.position.y + offset, enemy1Location.position.z);
                        selector.transform.position = newPos;
                        action = 1;
                    }
                }
            }
        }

        if (Input.GetButtonDown("Right"))
        {
            if (action == 1)
            {
                if (!battleSystem.enemy2Living && !battleSystem.enemy3Living && !battleSystem.enemy4Living) // if only enemy1 is living
                {
                    return;
                }
                else
                {
                    audi.PlayOneShot(swap);
                    if (battleSystem.enemy2Living)
                    {
                        Vector3 newPos = new Vector3(enemy2Location.position.x, enemy2Location.position.y + offset, enemy2Location.position.z);
                        selector.transform.position = newPos;
                        action = 2;
                    }
                    else if (battleSystem.enemy3Living)
                    {
                        Vector3 newPos = new Vector3(enemy3Location.position.x, enemy3Location.position.y + offset, enemy3Location.position.z);
                        selector.transform.position = newPos;
                        action = 3;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(enemy4Location.position.x, enemy4Location.position.y + offset, enemy4Location.position.z);
                        selector.transform.position = newPos;
                        action = 4;
                    }
                }
            }
            else if (action == 2)
            {
                if (!battleSystem.enemy1Living && !battleSystem.enemy3Living && !battleSystem.enemy4Living) // if only enemy2 is living
                {
                    return;
                }
                else
                {
                    audi.PlayOneShot(swap);
                    if (battleSystem.enemy3Living)
                    {
                        Vector3 newPos = new Vector3(enemy3Location.position.x, enemy3Location.position.y + offset, enemy3Location.position.z);
                        selector.transform.position = newPos;
                        action = 3;
                    }
                    else if (battleSystem.enemy4Living)
                    {
                        Vector3 newPos = new Vector3(enemy4Location.position.x, enemy4Location.position.y + offset, enemy4Location.position.z);
                        selector.transform.position = newPos;
                        action = 4;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(enemy1Location.position.x, enemy1Location.position.y + offset, enemy1Location.position.z);
                        selector.transform.position = newPos;
                        action = 1;
                    }
                }
            }
            else if (action == 3)
            {
                if (!battleSystem.enemy2Living && !battleSystem.enemy1Living && !battleSystem.enemy4Living) // if only enemy3 is living
                {
                    return;
                }
                else
                {
                    audi.PlayOneShot(swap);
                    if (battleSystem.enemy4Living)
                    {
                        Vector3 newPos = new Vector3(enemy4Location.position.x, enemy4Location.position.y + offset, enemy4Location.position.z);
                        selector.transform.position = newPos;
                        action = 4;
                    }
                    else if (battleSystem.enemy1Living)
                    {
                        Vector3 newPos = new Vector3(enemy1Location.position.x, enemy1Location.position.y + offset, enemy1Location.position.z);
                        selector.transform.position = newPos;
                        action = 1;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(enemy2Location.position.x, enemy2Location.position.y + offset, enemy2Location.position.z);
                        selector.transform.position = newPos;
                        action = 2;
                    }
                }
            }
            else
            {
                if (!battleSystem.enemy2Living && !battleSystem.enemy3Living && !battleSystem.enemy1Living) // if only enemy4 is living
                {
                    return;
                }
                else
                {
                    audi.PlayOneShot(swap);
                    if (battleSystem.enemy1Living)
                    {
                        Vector3 newPos = new Vector3(enemy1Location.position.x, enemy1Location.position.y + offset, enemy1Location.position.z);
                        selector.transform.position = newPos;
                        action = 1;
                    }
                    else if (battleSystem.enemy2Living)
                    {
                        Vector3 newPos = new Vector3(enemy2Location.position.x, enemy2Location.position.y + offset, enemy2Location.position.z);
                        selector.transform.position = newPos;
                        action = 2;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(enemy3Location.position.x, enemy3Location.position.y + offset, enemy3Location.position.z);
                        selector.transform.position = newPos;
                        action = 3;
                    }
                }
            }
        }
    }

    void chooseMagic()
    {
        int magicOffset = 35 * magic;
        Vector3 newPos = new Vector3(magicLocation.position.x, magicLocation.position.y - magicOffset, magicLocation.position.z);
        magicSelector.transform.position = newPos;

        // spell list. 0 = fireball, 1 = firestorm, 2 = ice shrapnel, 3 = minor heal, will update this in time to be better
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            audi.PlayOneShot(back);
            action = savedAction;
            isChoosingMagic = false;
            magicPanel.gameObject.SetActive(false);
        }
        if (Input.GetButtonDown("Space"))
        {
            selector.SetActive(true);
            
            audi.PlayOneShot(select);
            isChoosingMagic = false;
            isChoosingMagic2 = true;
            Input.ResetInputAxes();
            magicPanel.gameObject.SetActive(false);
            
            switch (magic)
            {
                case 0: // fireball      
                    action = 1;
                    break;
                case 1: // firestorm
                    selector2.SetActive(true);
                    selector3.SetActive(true);
                    selector4.SetActive(true);
                    action = 0;
                    break;
            }
        }
        if (Input.GetButtonDown("Up"))
        {
            audi.PlayOneShot(swap);
            switch (magic)
            {
                case 0:
                    magic = 3;
                    break;
                case 1:
                    magic = 0;
                    break;
                case 2:
                    magic = 1;
                    break;
                case 3:
                    magic = 2;
                    break;
            }
        }
        if (Input.GetButtonDown("Down"))
        {
            audi.PlayOneShot(swap);
            switch (magic)
            {
                case 0:
                    magic = 1;
                    break;
                case 1:
                    magic = 2;
                    break;
                case 2:
                    magic = 3;
                    break;
                case 3:
                    magic = 0;
                    break;
            }
        }
    }

    void chooseMagicFight()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            audi.PlayOneShot(back);
            selector.SetActive(false);
            selector2.SetActive(false);
            selector3.SetActive(false);
            selector4.SetActive(false);
            isChoosingMagic = true;
            isChoosingMagic2 = false;
            magicPanel.gameObject.SetActive(true);
        }
        if (Input.GetButtonDown("Space"))
        {
            selector.SetActive(false);
            selector2.SetActive(false);
            selector3.SetActive(false);
            selector4.SetActive(false);
            audi.PlayOneShot(select);
            battleSystem.onMagic(action,magic);
            battleSystem.state = BattleState.ENEMYTURN;
            isChoosingMagic2 = false;
            action = savedAction;
        }
        if (action == 0)
        {
            if (battleSystem.enemy1Living)
            {
                Vector3 newPos = new Vector3(enemy2Location.position.x, enemy2Location.position.y + offset, enemy2Location.position.z);
                selector.transform.position = newPos;
            }
            if (battleSystem.enemy2Living)
            {
                Vector3 newPos = new Vector3(enemy2Location.position.x, enemy2Location.position.y + offset, enemy2Location.position.z);
                selector2.transform.position = newPos;
            }
            if (battleSystem.enemy3Living)
            {
                Vector3 newPos = new Vector3(enemy3Location.position.x, enemy3Location.position.y + offset, enemy3Location.position.z);
                selector3.transform.position = newPos;
            }
            if (battleSystem.enemy4Living)
            {
                Vector3 newPos = new Vector3(enemy4Location.position.x, enemy4Location.position.y + offset, enemy4Location.position.z);
                selector4.transform.position = newPos;
            }
        }
        if (Input.GetButtonDown("Left") && action != 0)
        {
            if (action == 1)
            {
                if (!battleSystem.enemy2Living && !battleSystem.enemy3Living && !battleSystem.enemy4Living) // if only enemy1 is living
                {
                    return;
                }
                else
                {
                    audi.PlayOneShot(swap);
                    if (battleSystem.enemy4Living)
                    {
                        Vector3 newPos = new Vector3(enemy4Location.position.x, enemy4Location.position.y + offset, enemy4Location.position.z);
                        selector.transform.position = newPos;
                        action = 4;
                    }
                    else if (battleSystem.enemy3Living)
                    {
                        Vector3 newPos = new Vector3(enemy3Location.position.x, enemy3Location.position.y + offset, enemy3Location.position.z);
                        selector.transform.position = newPos;
                        action = 3;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(enemy2Location.position.x, enemy2Location.position.y + offset, enemy2Location.position.z);
                        selector.transform.position = newPos;
                        action = 2;
                    }
                }
            }
            else if (action == 2)
            {
                if (!battleSystem.enemy1Living && !battleSystem.enemy3Living && !battleSystem.enemy4Living) // if only enemy2 is living
                {
                    return;
                }
                else
                {
                    audi.PlayOneShot(swap);
                    if (battleSystem.enemy1Living)
                    {
                        Vector3 newPos = new Vector3(enemy1Location.position.x, enemy1Location.position.y + offset, enemy1Location.position.z);
                        selector.transform.position = newPos;
                        action = 1;
                    }
                    else if (battleSystem.enemy4Living)
                    {
                        Vector3 newPos = new Vector3(enemy4Location.position.x, enemy4Location.position.y + offset, enemy4Location.position.z);
                        selector.transform.position = newPos;
                        action = 4;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(enemy3Location.position.x, enemy3Location.position.y + offset, enemy3Location.position.z);
                        selector.transform.position = newPos;
                        action = 3;
                    }
                }
            }
            else if (action == 3)
            {
                if (!battleSystem.enemy2Living && !battleSystem.enemy1Living && !battleSystem.enemy4Living) // if only enemy3 is living
                {
                    return;
                }
                else
                {
                    audi.PlayOneShot(swap);
                    if (battleSystem.enemy2Living)
                    {
                        Vector3 newPos = new Vector3(enemy2Location.position.x, enemy2Location.position.y + offset, enemy2Location.position.z);
                        selector.transform.position = newPos;
                        action = 2;
                    }
                    else if (battleSystem.enemy1Living)
                    {
                        Vector3 newPos = new Vector3(enemy1Location.position.x, enemy1Location.position.y + offset, enemy1Location.position.z);
                        selector.transform.position = newPos;
                        action = 1;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(enemy4Location.position.x, enemy4Location.position.y + offset, enemy4Location.position.z);
                        selector.transform.position = newPos;
                        action = 4;
                    }
                }
            }
            else
            {
                if (!battleSystem.enemy2Living && !battleSystem.enemy3Living && !battleSystem.enemy1Living) // if only enemy4 is living
                {
                    return;
                }
                else
                {
                    audi.PlayOneShot(swap);
                    if (battleSystem.enemy3Living)
                    {
                        Vector3 newPos = new Vector3(enemy3Location.position.x, enemy3Location.position.y + offset, enemy3Location.position.z);
                        selector.transform.position = newPos;
                        action = 3;
                    }
                    else if (battleSystem.enemy2Living)
                    {
                        Vector3 newPos = new Vector3(enemy2Location.position.x, enemy2Location.position.y + offset, enemy2Location.position.z);
                        selector.transform.position = newPos;
                        action = 2;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(enemy1Location.position.x, enemy1Location.position.y + offset, enemy1Location.position.z);
                        selector.transform.position = newPos;
                        action = 1;
                    }
                }
            }
        }

        if (Input.GetButtonDown("Right") && action != 0)
        {
            if (action == 1)
            {
                if (!battleSystem.enemy2Living && !battleSystem.enemy3Living && !battleSystem.enemy4Living) // if only enemy1 is living
                {
                    return;
                }
                else
                {
                    audi.PlayOneShot(swap);
                    if (battleSystem.enemy2Living)
                    {
                        Vector3 newPos = new Vector3(enemy2Location.position.x, enemy2Location.position.y + offset, enemy2Location.position.z);
                        selector.transform.position = newPos;
                        action = 2;
                    }
                    else if (battleSystem.enemy3Living)
                    {
                        Vector3 newPos = new Vector3(enemy3Location.position.x, enemy3Location.position.y + offset, enemy3Location.position.z);
                        selector.transform.position = newPos;
                        action = 3;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(enemy4Location.position.x, enemy4Location.position.y + offset, enemy4Location.position.z);
                        selector.transform.position = newPos;
                        action = 4;
                    }
                }
            }
            else if (action == 2)
            {
                if (!battleSystem.enemy1Living && !battleSystem.enemy3Living && !battleSystem.enemy4Living) // if only enemy2 is living
                {
                    return;
                }
                else
                {
                    audi.PlayOneShot(swap);
                    if (battleSystem.enemy3Living)
                    {
                        Vector3 newPos = new Vector3(enemy3Location.position.x, enemy3Location.position.y + offset, enemy3Location.position.z);
                        selector.transform.position = newPos;
                        action = 3;
                    }
                    else if (battleSystem.enemy4Living)
                    {
                        Vector3 newPos = new Vector3(enemy4Location.position.x, enemy4Location.position.y + offset, enemy4Location.position.z);
                        selector.transform.position = newPos;
                        action = 4;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(enemy1Location.position.x, enemy1Location.position.y + offset, enemy1Location.position.z);
                        selector.transform.position = newPos;
                        action = 1;
                    }
                }
            }
            else if (action == 3)
            {
                if (!battleSystem.enemy2Living && !battleSystem.enemy1Living && !battleSystem.enemy4Living) // if only enemy3 is living
                {
                    return;
                }
                else
                {
                    audi.PlayOneShot(swap);
                    if (battleSystem.enemy4Living)
                    {
                        Vector3 newPos = new Vector3(enemy4Location.position.x, enemy4Location.position.y + offset, enemy4Location.position.z);
                        selector.transform.position = newPos;
                        action = 4;
                    }
                    else if (battleSystem.enemy1Living)
                    {
                        Vector3 newPos = new Vector3(enemy1Location.position.x, enemy1Location.position.y + offset, enemy1Location.position.z);
                        selector.transform.position = newPos;
                        action = 1;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(enemy2Location.position.x, enemy2Location.position.y + offset, enemy2Location.position.z);
                        selector.transform.position = newPos;
                        action = 2;
                    }
                }
            }
            else
            {
                if (!battleSystem.enemy2Living && !battleSystem.enemy3Living && !battleSystem.enemy1Living) // if only enemy4 is living
                {
                    return;
                }
                else
                {
                    audi.PlayOneShot(swap);
                    if (battleSystem.enemy1Living)
                    {
                        Vector3 newPos = new Vector3(enemy1Location.position.x, enemy1Location.position.y + offset, enemy1Location.position.z);
                        selector.transform.position = newPos;
                        action = 1;
                    }
                    else if (battleSystem.enemy2Living)
                    {
                        Vector3 newPos = new Vector3(enemy2Location.position.x, enemy2Location.position.y + offset, enemy2Location.position.z);
                        selector.transform.position = newPos;
                        action = 2;
                    }
                    else
                    {
                        Vector3 newPos = new Vector3(enemy3Location.position.x, enemy3Location.position.y + offset, enemy3Location.position.z);
                        selector.transform.position = newPos;
                        action = 3;
                    }
                }
            }
        }
    }

    void chooseItem()
    {
        int itemOffset = 35 * item;
        Vector3 newPos = new Vector3(magicLocation.position.x, magicLocation.position.y - itemOffset, magicLocation.position.z);
        magicSelector.transform.position = newPos;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            audi.PlayOneShot(back);
            action = savedAction;
            isChoosingItem = false;
            itemPanel.gameObject.SetActive(false);
        }
        if (Input.GetButtonDown("Space"))
        {
            selector.SetActive(true);

            audi.PlayOneShot(select);
            isChoosingItem = false;
            Input.ResetInputAxes();
            itemPanel.gameObject.SetActive(false);
            battleSystem.onHeal(2);
        }
    }
    void debugging()
    {
        if (Input.GetButtonDown("Debug1"))
        {
            Debug.Log("Switching Turn");
            if (battleSystem.state == BattleState.PLAYERTURN)
            {
                Debug.Log("Enemy Turn, icons should be off");
                battleSystem.state = BattleState.ENEMYTURN;
            }
            else
            {
                Debug.Log("Player Turn, icons should be on");
                battleSystem.state = BattleState.PLAYERTURN;
            }
        }
    }

}
