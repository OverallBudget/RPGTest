using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FightScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;
    [SerializeField] GameObject enemy4;
    [SerializeField] TextMeshProUGUI actionDescript;
    [SerializeField] GameObject selector;
    [SerializeField] GameObject UIManager;
    int enemy;
    bool fighting;

    Vector2 pos1;
    Vector2 pos2;
    Vector2 pos3;
    Vector2 pos4;
    float upOffset = 50f;

    private void Start()
    {
        if(enemy1 != null)
        {
            Vector2 v = new Vector2(enemy1.transform.position.x, enemy1.transform.position.y + upOffset);
            pos1 = v;
        }

        if (enemy2 != null)
        {
            Vector2 v = new Vector2(enemy2.transform.position.x, enemy2.transform.position.y + upOffset);
            pos2 = v;
        }

        if (enemy3 != null)
        {
            Vector2 v = new Vector2(enemy3.transform.position.x, enemy3.transform.position.y + upOffset);
            pos3 = v;
        }

        if (enemy4 != null)
        {
            Vector2 v = new Vector2(enemy4.transform.position.x, enemy4.transform.position.y + upOffset);
            enemy4 = new GameObject { hideFlags = HideFlags.HideAndDontSave };
            pos4 = v;
        }

    }

    private void Update()
    {
        Fight();
    }
    void Fight()
    {
        selector.SetActive(true);
        if (enemy1 != null)
        {
            selector.transform.position = pos1;
        }
        else if(enemy2 != null)
        {
            selector.transform.position = pos2;
        }
        else if(enemy3 != null)
        {
            selector.transform.position = pos3;
        }
        else
        {
            selector.transform.position = pos4;
        }

        if (Input.GetButtonDown("Space"))
        {
            switch (enemy)
            {

                case 0:
                    Debug.Log("This is enemy 1");
                    break;
                case 1:
                    Debug.Log("This is enemy 2");
                    break;
                case 2:
                    Debug.Log("This is enemy 3");
                    break;
                case 3:
                    Debug.Log("This is enemy 4");
                    break;
                default: // failsafe
                    Debug.LogError("you shouldn't be here.");
                    break;

            }
            selector.SetActive(false);
        }
        if (Input.GetButtonDown("Escape"))
        {
            selector.SetActive(false);
        }
    }
}
