using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    public GameObject tessera;
    private int current_card = 0;
    private int player;
    private int n_cards;
    private List<GameObject>[] players;
    private List<GameObject> gameCards;
    private bool alreadyChanged;
    private int cnt_carte;
    private int space = -252;
    // Start is called before the first frame update
    void Start()
    {
        players = new List<GameObject>[2];
        gameCards = new List<GameObject>();
        for (var i = 0; i < 2; i++)
        {
            players[i] = new List<GameObject>();
        }

        n_cards = 7;
        switch (SetUp.n_players)
        {
            case 2:
                n_cards = 7;
                break;
            case 3:
                n_cards = 5;
                break;
            case 4:
                n_cards = 5;
                break;
                //default:
                //  throw new System.ArgumentException("Number of players is wrong");
        }
        cnt_carte = 55 - n_cards;

        foreach (var p in players)
        {
            for (var i = 0; i < Camera.main.pixelWidth; i += Camera.main.pixelWidth / n_cards)
            {
                p.Add(Instantiate(tessera, Camera.main.ScreenToWorldPoint(new Vector3(i, -50)), Quaternion.identity));

            }
        }

        foreach (List<GameObject> list in players)
        {
            foreach (GameObject prefab in list)
            {
                prefab.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
        }

        player = Random.Range(0, 2);
        int local_space = -82;
        foreach (GameObject prefab in players[player])
        {
            prefab.transform.position = new Vector3(local_space, -71);
            prefab.transform.eulerAngles = new Vector3(20, 0, 200);
            local_space += 44;
        }

        players[player][current_card].transform.position = new Vector3(players[player][current_card].transform.position.x, -95, players[player][current_card].transform.position.z);
    }

    bool equalToFirstCard = false;
    GameObject referenceCard = null;
    int index_card_to_achor;
    int index_card_to_insert;
    // Update is called once per frame
    void Update()
    {
        if (gameCards.Count > 1)
        {
            gameCards[0].GetComponentsInChildren<setText>()[0].tessera.anchored = false;
            gameCards[0].GetComponentsInChildren<setText>()[1].tessera.anchored = true;
            for (var i = 1; i < gameCards.Count-1; i++)
            {
                foreach (var go in gameCards[i].GetComponentsInChildren<setText>())
                {
                    go.tessera.anchored = true;
                }
            }
            
        }
        // else
        // {
        //     foreach (var gameObject in gameCards)
        //     {
        //         foreach (var setText in gameObject.GetComponentsInChildren<setText>())
        //         {
        //             setText.tessera.anchored = false;
        //         }
        //     }
        // }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            changePlayer();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            onClickP();
        }

        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp("return"))
        {
            onClickReturn();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            onClickRightArrow();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            onClickLeftArrow();
        }
    }

    private void onClickP()
    {
        if (!alreadyChanged)
        {
            if (cnt_carte > 0)
            {
                players[player].Add(Instantiate(tessera, new Vector3(players[player][players[player].Count - 1].transform.position.x + 44, -71), Quaternion.identity));
                players[player][players[player].Count - 1].transform.eulerAngles = new Vector3(20, 0, 200);
                players[player][players[player].Count - 1].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                alreadyChanged = true;
            }
        }
    }

    private void onClickReturn()
    {
        Debug.Log(getGameCardData());
        try
        {
            if (isCompatible())
            {
                useCard();
            }
        }
        catch (System.ArgumentOutOfRangeException)
        {
            useFirstCard();
        }
    }

    private void onClickRightArrow()
    {
        players[player][current_card].transform.position = new Vector3(players[player][current_card].transform.position.x, -71, players[player][current_card].transform.position.z);
        current_card++;
        if (current_card >= players[player].Count)
        {
            current_card = 0;
        }
        players[player][current_card].transform.position = new Vector3(players[player][current_card].transform.position.x, -95, players[player][current_card].transform.position.z);
    }

    private void onClickLeftArrow()
    {
        players[player][current_card].transform.position = new Vector3(players[player][current_card].transform.position.x, -71, players[player][current_card].transform.position.z);
        current_card--;
        if (current_card < 0)
        {
            current_card = players[player].Count - 1;
        }
        players[player][current_card].transform.position = new Vector3(players[player][current_card].transform.position.x, -95, players[player][current_card].transform.position.z);
    }

    bool turnCard = false;

    private bool isCompatible()
    {
        bool compatible = false;
        setText[] array = players[player][current_card].GetComponentsInChildren<setText>();
        for (int i = 0; i < array.Length; i++)
        {
            setText setText = array[i];
            setText[] array1 = gameCards[0].GetComponentsInChildren<setText>();
            for (int j = 0; j < array1.Length; j++)
            {
                setText setText1 = array1[j];
                if (!setText1.tessera.anchored)
                {
                    Debug.Log($"{setText.tessera} = {setText.tessera.value()}\n" +
                              $"{setText1.tessera} = {setText1.tessera.value()}");
                    if (setText.tessera.value() < 0 && setText1.tessera.value() < 0)
                    {
                        float diff;
                        if (Mathf.Abs(setText.tessera.value()) > Mathf.Abs(setText1.tessera.value()))
                        {
                            diff = Mathf.Abs(setText.tessera.value()) - Mathf.Abs(setText1.tessera.value());
                        }
                        else
                        {
                            diff = Mathf.Abs(setText1.tessera.value()) - Mathf.Abs(setText.tessera.value());
                        }
                        compatible = diff < 0.01;
                    }
                    else
                    {
                        Debug.Log($"{setText.tessera} = {setText.tessera.value()}\n" +
                                  $"{setText1.tessera} = {setText1.tessera.value()}");
                        if (setText.tessera.value() >= 0 && setText1.tessera.value() >= 0)
                        {
                            float diff = 5.0f;
                            if (setText.tessera.value() > setText1.tessera.value())
                            {
                                diff = setText.tessera.value() - setText1.tessera.value();
                            }
                            else
                            {
                                diff = setText1.tessera.value() - setText.tessera.value();
                            }
                            compatible = diff < 0.01;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (compatible)
                    {
                        Debug.Log($"{setText.tessera}=={setText1.tessera}");
                        setText.tessera.anchored = true;
                        setText1.tessera.anchored = true;
                        referenceCard = players[player][current_card];
                        if (i == 0 && j == 0)
                        {
                            equalToFirstCard = true;
                            turnCard = true;
                        }
                        else
                        {
                            if (i == 0 && j == 1)
                            {
                                equalToFirstCard = false;
                                turnCard = false;
                            }
                            else
                            {
                                if (i == 1 && j == 0)
                                {
                                    equalToFirstCard = true;
                                    turnCard = false;
                                }
                                else
                                {
                                    if (i == 1 && j == 1)
                                    {
                                        equalToFirstCard = false;
                                        turnCard = true;
                                    }
                                }
                            }
                        }
                        index_card_to_insert = i;
                        index_card_to_achor = j;
                        break;
                    }
                }
            }
            if (!compatible)
            {
                setText[] array2 = gameCards[gameCards.Count - 1].GetComponentsInChildren<setText>();
                for (int j = 0; j < array2.Length; j++)
                {
                    setText setText1 = array2[j];
                    if (!setText1.tessera.anchored)
                    {
                        float diff = 5.0f;
                        if (setText.tessera.value() > setText1.tessera.value())
                        {
                            diff = setText.tessera.value() - setText1.tessera.value();
                        }
                        else
                        {
                            diff = setText1.tessera.value() - setText.tessera.value();
                        }
                        compatible = diff < 0.01;
                        if (compatible)
                        {
                            setText.tessera.anchored = true;
                            setText1.tessera.anchored = true;
                            referenceCard = players[player][current_card];
                            equalToFirstCard = false;
                            index_card_to_achor = j;
                            index_card_to_insert = i;
                            break;
                        }
                    }
                }
            }

        }
        return compatible;
    }

    private void useCard()
    {
        players[player].RemoveAt(current_card);
        if (equalToFirstCard)
        { 
            foreach (GameObject go in gameCards)
            {
                var position = go.transform.position;
                position = new Vector3(position.x + 40f, position.y, position.z);
                go.transform.position = position;
            }
            gameCards.Insert(0, referenceCard);
            gameCards[0].transform.position = new Vector3(-252, 131);
            gameCards[0].transform.eulerAngles = new Vector3(0, 95, -65);
            gameCards[0].transform.localScale = new Vector3(1.5f, 1.5f, 0.7f);
            if (turnCard)
            {
                Tessera temp = gameCards[0].GetComponentsInChildren<setText>()[0].tessera;
                gameCards[0].GetComponentsInChildren<setText>()[0].tessera = gameCards[0].GetComponentsInChildren<setText>()[1].tessera;
                gameCards[0].GetComponentsInChildren<setText>()[1].tessera = temp;
                gameCards[0].GetComponentsInChildren<setText>()[0].rewrite();
                gameCards[0].GetComponentsInChildren<setText>()[1].rewrite();
                temp = gameCards[gameCards.Count - 1].GetComponentsInChildren<setText>()[0].tessera;
                gameCards[gameCards.Count - 1].GetComponentsInChildren<setText>()[0].tessera = gameCards[gameCards.Count - 1].GetComponentsInChildren<setText>()[1].tessera;
                gameCards[gameCards.Count - 1].GetComponentsInChildren<setText>()[1].tessera = temp;
                foreach (var setText in gameCards[gameCards.Count - 1].GetComponentsInChildren<setText>())
                {
                    setText.rewrite();
                }
            }

        }
        else
        {
            Debug.Log($"index_anchored={index_card_to_achor}");
            Debug.Log($"index_insert={index_card_to_insert}");
            gameCards.Add(referenceCard);
            gameCards[gameCards.Count - 1].transform.position = new Vector3(this.space, 131f);
            if (index_card_to_achor == index_card_to_insert)
            {
                Tessera temp = gameCards[gameCards.Count - 1].GetComponentsInChildren<setText>()[0].tessera;
                gameCards[gameCards.Count - 1].GetComponentsInChildren<setText>()[0].tessera =
                    gameCards[gameCards.Count - 1].GetComponentsInChildren<setText>()[1].tessera;
                gameCards[gameCards.Count - 1].GetComponentsInChildren<setText>()[1].tessera = temp;
                gameCards[gameCards.Count - 1].GetComponentsInChildren<setText>()[0].rewrite();
                gameCards[gameCards.Count - 1].GetComponentsInChildren<setText>()[1].rewrite();
            }
            

        }
        gameCards[gameCards.Count - 1].transform.eulerAngles = new Vector3(0, 95, -65);
        gameCards[gameCards.Count - 1].transform.localScale = new Vector3(1.5f, 1.5f, 0.7f);
        this.space += 40;
        changePlayer();
    }

    private void useFirstCard()
    {
        gameCards.Add(players[player][current_card]);
        players[player].RemoveAt(current_card);
        gameCards[0].transform.position = new Vector3(-252, 131);
        gameCards[0].transform.eulerAngles = new Vector3(0, 95, -65);
        gameCards[0].transform.localScale = new Vector3(1.5f, 1.5f, 0.7f);
        this.space += 40;
        changePlayer();
    }

    private void changePlayer()
    {
        foreach (GameObject go in players[player])
        {
            go.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, -50));
        }

        player++;
        if (player >= players.Length)
        {
            player = 0;
        }

        int local_space = -82;

        foreach (GameObject prefab in players[player])
        {
            prefab.transform.position = new Vector3(local_space, -71);
            prefab.transform.eulerAngles = new Vector3(20, 0, 200);
            local_space += 44;
        }
        current_card = 0;
        alreadyChanged = false;
    }

    private string getGameCardData()
    {
        string str = "\n";
        foreach(GameObject gameObject in gameCards)
        {
            foreach(setText setText in gameObject.GetComponentsInChildren<setText>())
            {
                Tessera tessera = setText.tessera;
                str += $"{tessera}\tanchored = {tessera.anchored}\n";
            }
        }
        return str;
    }
}