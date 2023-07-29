using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadMiniGame : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform bottomPoint;

    [Header("Зона перезарядки")]
    [SerializeField] private Transform reloadZone;
    [SerializeField] private int greenZoneHeight = 60;


    [Header("Количество пуль при выигрыше")]
    [SerializeField] int bestCountBullets = 3;
    [SerializeField] int goodCountBullets = 2;

    [Header("Скорость точки")]
    [SerializeField] private float speed;

    [Header("ХП игрока")]
    [SerializeField] private PlayerData playerData;

    bool upMove = true;
    bool isMoving = true;
    public bool IsMoving => isMoving;

    bool oneTime = false;


    private void OnEnable()
    {
        isMoving = true;
        upMove = true;

        PlacedReloadZone();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.R))
            isMoving = false;

        PointChangeDirection();
        PointDirectionMovement();
        CheckWinning();
    }


    void PointChangeDirection()
    {
        if (upMove && point.position.y > topPoint.position.y)
            upMove = false;
        else if (!upMove && point.position.y < bottomPoint.position.y)
            upMove = true;
    }

    void PointDirectionMovement()
    {
        if (!isMoving)
            return;

        if (upMove)
            point.Translate(Vector2.up * speed * 100 * Time.deltaTime);
        else
            point.Translate(-Vector2.up * speed * 100 * Time.deltaTime);
    }

    void PlacedReloadZone()
    {
        //reloadZone.position = new Vector2(reloadZone.position.x, Random.Range(bottomPoint.position.y + reloadZone.localScale.y / 2,
        //topPoint.position.y - reloadZone.localScale.y / 2));

        reloadZone.position = new Vector2(reloadZone.position.x, Random.Range(bottomPoint.position.y + reloadZone.GetComponent<RectTransform>().rect.height / 2.5f,
                                                                              topPoint.position.y - reloadZone.GetComponent<RectTransform>().rect.height / 2.5f));
    }

    public void WinGame(int bulletCount)
    {
        print("zaebis");
        DifferentStatic.canReload = bulletCount;
        Invoke("CloseMiniGame", 0.5f);
    }

    public void LoseGame()
    {
        //print("Lose!!!");
        playerData.health -= Random.Range(4, 7);
        DifferentStatic.canReload = 2;
        Invoke("CloseMiniGame", 0.5f);
    }

    void CheckWinning()
    {
        if (isMoving || oneTime)
            return;

        if (point.localPosition.y > reloadZone.localPosition.y - greenZoneHeight &&
                point.localPosition.y < reloadZone.localPosition.y + greenZoneHeight)
        {
            WinGame(bestCountBullets);
        }
        else if (point.localPosition.y > reloadZone.localPosition.y - reloadZone.GetComponent<RectTransform>().rect.height / 2 &&
                point.localPosition.y < reloadZone.localPosition.y + reloadZone.GetComponent<RectTransform>().rect.height / 2)
        {
            WinGame(goodCountBullets);
        }
        else
        {
            LoseGame();
        }

        oneTime = true;
        Invoke("ResetOneTime", 1);
    }

    void ResetOneTime()
    {
        oneTime = false;
    }

    void CloseMiniGame()
    {
        gameObject.SetActive(false);
    }
}
