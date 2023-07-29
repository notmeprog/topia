using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrotController : MonoBehaviour
{
    [SerializeField] KrotPatterns[] krotPatterns;

    public int countActive = 0;

    float nextSpawn = 0;
    public float spawnRate = 0.1f;

    bool stopFight = false;

    void Start()
    {
        for (int i = 0; i < krotPatterns.Length; i++)
            krotPatterns[i].gameObject.SetActive(false);

        StartCoroutine("Patterns");
    }

    void Update()
    {
        /*if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + 1f / spawnRate;
            ActiveKrots();
        }*/


        if (Input.GetKeyUp(KeyCode.M))
            Pattern1();
        /*if (Input.GetKeyUp(KeyCode.M))
            Pattern1();

        if (Input.GetKeyUp(KeyCode.L))
            ActiveAllKrots();

        if (Input.GetKeyUp(KeyCode.K))
            NoneActive();*/
    }

    [Header("Count")]
    public int count = 0;
    IEnumerator Patterns()
    {
        yield return new WaitForSeconds(1);

        ActiveAllKrots();
        yield return new WaitForSeconds(3);
        NoneActive();
        yield return new WaitForSeconds(2);

        while (stopFight == false)
        {
            if (count % 5 == 0 && count != 0)
            {
                ActiveAllKrots();
                yield return new WaitForSeconds(3);
                NoneActive();
                yield return new WaitForSeconds(3);
            }
            else
            {
                Pattern1();
                yield return new WaitForSeconds(5);
            }

            count++;
        }
    }

    void Pattern2()
    {
        ActiveAllKrots();
        Invoke("NoneActive", 2.5f);
    }

    void Pattern1()
    {
        //countActive = 0;

        /*for (int i = 0; i < krotPatterns.Length; i++)
        {
            if (countActive < 4 && !krotPatterns[i].IsActive)
            {
                krotPatterns[i].gameObject.SetActive(true);
                countActive++;
            }
        }*/

        for (int i = 0; i < countActive; i++)
        {
            int randomKrot = Random.Range(0, krotPatterns.Length);

            if (!krotPatterns[randomKrot].IsActive && !krotPatterns[randomKrot].krotMainStats.IsDead)
            {
                krotPatterns[randomKrot].gameObject.SetActive(true);
                krotPatterns[randomKrot].Pattern1ForControl();
            }
        }
    }

    void ActiveAllKrots()
    {
        //for Pattern2

        for (int i = 0; i < krotPatterns.Length; i++)
        {
            if (!krotPatterns[i].krotMainStats.IsDead)
            {
                krotPatterns[i].isRocketAttack = true;
                krotPatterns[i].gameObject.SetActive(true);
            }
        }
    }

    void NoneActive()
    {
        for (int i = 0; i < krotPatterns.Length; i++)
        {
            if (!krotPatterns[i].krotMainStats.IsDead)
            {
                krotPatterns[i].isRocketAttack = false;
                krotPatterns[i].DisableKrot();
            }
        }
    }
}
