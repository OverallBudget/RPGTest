using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE}
public class BattleSystem : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;
    [SerializeField] GameObject enemy4;
    [SerializeField] TextMeshProUGUI actionDescript;
    [SerializeField] Transform playerLocation;

    PlayerScript PlayerUnit;
    EnemyScript EnemyUnit1;
    EnemyScript EnemyUnit2;
    EnemyScript EnemyUnit3;
    EnemyScript EnemyUnit4;
    [SerializeField] Transform enemy1Location;
    [SerializeField] Transform enemy2Location;
    [SerializeField] Transform enemy3Location;
    [SerializeField] Transform enemy4Location;
    Vector2 pos1;
    Vector2 pos2;
    Vector2 pos3;
    Vector2 pos4;

    int enemyActive;

    GameObject PlayerGO;
    GameObject EnemyGO1;
    GameObject EnemyGO2;
    GameObject EnemyGO3;
    GameObject EnemyGO4;

    public bool enemy1Living = true;
    public bool enemy2Living = false;
    public bool enemy3Living = false;
    public bool enemy4Living = false;

    public string enemy1Name;
    public string enemy2Name;
    public string enemy3Name;
    public string enemy4Name;

    public AudioClip hit;
    public AudioClip death;
    public AudioSource audi;

    public BattleState state;

    public BattleHUD enemy1HUD;
    public BattleHUD enemy2HUD;
    public BattleHUD enemy3HUD;
    public BattleHUD enemy4HUD;

    public PlayerHUD playerHUD;

    private void Start()
    {
        audi = GetComponent<AudioSource>();
        state = BattleState.START;
        SetupBattle();
    }

    public void HandleUpdate()
    {

    }
    void SetupBattle()
    {
        PlayerGO = Instantiate(player, playerLocation);
        PlayerUnit = PlayerGO.GetComponent<PlayerScript>();
        EnemyGO1 = Instantiate(enemy1, enemy1Location);
        EnemyUnit1 = EnemyGO1.GetComponent<EnemyScript>();
        enemy1Name = EnemyUnit1.getName();
        if (enemy2 != null) {
            EnemyGO2 = Instantiate(enemy2, enemy2Location);
            EnemyUnit2 = EnemyGO2.GetComponent<EnemyScript>();
            enemy2Name = EnemyUnit2.getName();
            enemy2Living = true;
            enemy2HUD.SetHUD(EnemyUnit2);
        } // keep it simple and have 1 for now
        //if (enemy3 != null) { Instantiate(enemy3, enemy3Location); }
        //if (enemy4 != null) { Instantiate(enemy4, enemy4Location); }

        enemy1HUD.SetHUD(EnemyUnit1);
        playerHUD.SetHUD(PlayerUnit);
        Debug.Log("Done Setting HUD");

        state = BattleState.PLAYERTURN; // STATES START, PLAYERTURN, ENEMYTURN, WIN, LOSE, FIGHT
    }

    IEnumerator PlayerAttack(int i)
    {
        int trueDamage;
        audi.PlayOneShot(hit);
        bool isDead = true;
        bool isDead2 = true;
        bool isDead3 = true;
        bool isDead4 = true;
        switch (i)
        {
            case 1:
                trueDamage = PlayerUnit.getAttack() - EnemyUnit1.getDefense();
                isDead = EnemyUnit1.TakeDamage(trueDamage);
                enemy1HUD.setHP(EnemyUnit1.getCurrentHealth()); 
                break;
            case 2:
                trueDamage = PlayerUnit.getAttack() - EnemyUnit2.getDefense();
                isDead2 = EnemyUnit2.TakeDamage(trueDamage);
                enemy2HUD.setHP(EnemyUnit2.getCurrentHealth());
                break;
            case 3:
                trueDamage = PlayerUnit.getAttack() - EnemyUnit3.getDefense();
                isDead3 = EnemyUnit3.TakeDamage(trueDamage);
                enemy3HUD.setHP(EnemyUnit3.getCurrentHealth());
                break;
            case 4:
                trueDamage = PlayerUnit.getAttack() - EnemyUnit4.getDefense();
                isDead4 = EnemyUnit4.TakeDamage(trueDamage);
                enemy4HUD.setHP(EnemyUnit4.getCurrentHealth());
                break;


                
        }
        yield return new WaitForSeconds(1f);
        // Damage chosen enemy
        if (enemy1Living)
        {
            if (isDead)
            {
                yield return new WaitForSeconds(2f);
                audi.PlayOneShot(death);
                enemy1Living = false;
                Destroy(EnemyGO1);
                enemy1HUD.hideText();
            }
        }
        if (enemy2Living)
        {
            if (isDead2)
            {
                yield return new WaitForSeconds(2f);
                audi.PlayOneShot(death);
                enemy2Living = false;
                Destroy(EnemyGO2);
                enemy2HUD.hideText();
            }
        }
        if (enemy3Living)
        {
            if (isDead3)
            {
                yield return new WaitForSeconds(2f);
                audi.PlayOneShot(death);
                enemy3Living = false;
                Destroy(EnemyGO3);
                enemy3HUD.hideText();
            }
        }
        if (enemy4Living)
        {
            if (isDead4)
            {
                yield return new WaitForSeconds(2f);
                audi.PlayOneShot(death);
                enemy4Living = false;
                Destroy(EnemyGO4);
                enemy4HUD.hideText();
            }
        }
        yield return new WaitForSeconds(2f);
        if (!enemy1Living && !enemy2Living && !enemy3Living && !enemy4Living) // if all enemies are dead.
        {
            // end battle
            state = BattleState.WIN;
            EndBattle();
        }
        else
        {
            // enemy turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        // check for enemy(s) dead
    }

    IEnumerator EnemyTurn()
    {
        int trueDamage;
        if (enemy1Living)
        {
            Debug.Log("Enemy 1 is attacking");

            yield return new WaitForSeconds(1f);

            audi.PlayOneShot(hit);
            trueDamage = EnemyUnit1.getDamage() - PlayerUnit.getDefense();
            PlayerUnit.TakeDamage(trueDamage);
            playerHUD.setHP(PlayerUnit.getCurrentHealth());
        }
        if (enemy2Living)
        {
            Debug.Log("Enemy 2 is attacking");

            yield return new WaitForSeconds(1f);

            audi.PlayOneShot(hit);
            trueDamage = EnemyUnit1.getDamage() - PlayerUnit.getDefense();
            PlayerUnit.TakeDamage(trueDamage);
            playerHUD.setHP(PlayerUnit.getCurrentHealth());
        }
        if (enemy3Living)
        {
            Debug.Log("Enemy 3 is attacking");

            yield return new WaitForSeconds(1f);

            audi.PlayOneShot(hit);
            PlayerUnit.TakeDamage(EnemyUnit3.getDamage());
            playerHUD.setHP(PlayerUnit.getCurrentHealth());
        }
        if (enemy4Living)
        {
            Debug.Log("Enemy 4 is attacking");

            yield return new WaitForSeconds(1f);

            audi.PlayOneShot(hit);
            PlayerUnit.TakeDamage(EnemyUnit4.getDamage());
            playerHUD.setHP(PlayerUnit.getCurrentHealth());
        }
        if(PlayerUnit.getCurrentHealth() <= 0)
        {
            state = BattleState.LOSE;
        }
        else
        {
            state = BattleState.PLAYERTURN;
        }
    }
    void EndBattle()
    {
        if (state == BattleState.WIN)
        {
            // show text saying you win
        }
    }

    IEnumerator MagicAttack(int i, int magic)
    {
        int trueDamage;
        audi.PlayOneShot(hit);
        bool isDead = true;
        switch (i)
        {
            case 0: // aoe
                PlayerUnit.changeMana(4);
                playerHUD.setMP(PlayerUnit.getCurrentMana());
                if (enemy1Living)
                {
                    trueDamage = PlayerUnit.getAttack() - EnemyUnit1.getDefense();
                    isDead = EnemyUnit1.TakeDamage(trueDamage);
                    enemy1HUD.setHP(EnemyUnit1.getCurrentHealth());
                    if (isDead)
                    {
                        yield return new WaitForSeconds(2f);
                        audi.PlayOneShot(death);
                        enemy1Living = false;
                        Destroy(EnemyGO1);
                        enemy1HUD.hideText();
                    }
                }
                if (enemy2Living)
                {
                    trueDamage = PlayerUnit.getAttack() - EnemyUnit2.getDefense();
                    isDead = EnemyUnit2.TakeDamage(trueDamage);
                    enemy2HUD.setHP(EnemyUnit2.getCurrentHealth());
                    if (isDead)
                    {
                        yield return new WaitForSeconds(2f);
                        audi.PlayOneShot(death);
                        enemy2Living = false;
                        Destroy(EnemyGO2);
                        enemy2HUD.hideText();
                    }
                }
                if (enemy3Living)
                {
                    trueDamage = PlayerUnit.getAttack() - EnemyUnit3.getDefense();
                    isDead = EnemyUnit3.TakeDamage(trueDamage);
                    enemy3HUD.setHP(EnemyUnit3.getCurrentHealth());
                    if (isDead)
                    {
                        yield return new WaitForSeconds(2f);
                        audi.PlayOneShot(death);
                        enemy3Living = false;
                        Destroy(EnemyGO3);
                        enemy3HUD.hideText();
                    }
                }
                if (enemy4Living)
                {
                    trueDamage = PlayerUnit.getAttack() - EnemyUnit4.getDefense();
                    isDead = EnemyUnit4.TakeDamage(trueDamage);
                    enemy4HUD.setHP(EnemyUnit4.getCurrentHealth());
                    if (isDead)
                    {
                        yield return new WaitForSeconds(2f);
                        audi.PlayOneShot(death);
                        enemy4Living = false;
                        Destroy(EnemyGO4);
                        enemy4HUD.hideText();
                    }
                }
                break;
            case 1:
                PlayerUnit.changeMana(2);
                playerHUD.setMP(PlayerUnit.getCurrentMana());
                trueDamage = PlayerUnit.getAttack() * 2 - EnemyUnit1.getDefense();
                isDead = EnemyUnit1.TakeDamage(trueDamage);
                enemy1HUD.setHP(EnemyUnit1.getCurrentHealth());
                if (isDead)
                {
                    yield return new WaitForSeconds(2f);
                    audi.PlayOneShot(death);
                    enemy1Living = false;
                    Destroy(EnemyGO1);
                    enemy1HUD.hideText();
                }
                break;
            case 2:
                PlayerUnit.changeMana(2);
                playerHUD.setMP(PlayerUnit.getCurrentMana());
                trueDamage = PlayerUnit.getAttack() * 2 - EnemyUnit2.getDefense();
                isDead = EnemyUnit2.TakeDamage(trueDamage);
                enemy2HUD.setHP(EnemyUnit2.getCurrentHealth());
                if (isDead)
                {
                    yield return new WaitForSeconds(2f);
                    audi.PlayOneShot(death);
                    enemy2Living = false;
                    Destroy(EnemyGO2);
                    enemy2HUD.hideText();
                }
                break;
            case 3:
                break;
            case 4:
                break;
            case 5: // self heal
                break;
        }

        yield return new WaitForSeconds(2f);
        if (!enemy1Living && !enemy2Living && !enemy3Living && !enemy4Living) // if all enemies are dead.
        {
            // end battle
            state = BattleState.WIN;
            EndBattle();
        }
        else
        {
            // enemy turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator Heal(int i)
    {
        PlayerUnit.TakeDamage(-i);
        if(PlayerUnit.getCurrentHealth() > PlayerUnit.getMaxHealth())
        {
            PlayerUnit.setHP(PlayerUnit.getMaxHealth());
        }
        playerHUD.setHP(PlayerUnit.getCurrentHealth());
        yield return new WaitForSeconds(1f);
    }
    public void onFight(int i)
    {
        if (state != BattleState.PLAYERTURN)
        {
            Debug.Log("Not Player turn.");
            return;
        }

        StartCoroutine(PlayerAttack(i));
    }

    public void onMagic(int i, int magic)
    {
        if (state != BattleState.PLAYERTURN)
        {
            Debug.Log("Not Player turn.");
            return;
        }
        StartCoroutine(MagicAttack(i, magic));
    }

    public void onHeal(int i)
    {
        if (state != BattleState.PLAYERTURN)
        {
            Debug.Log("Not Player turn.");
            return;
        }
        StartCoroutine(Heal(i));
    }
}
