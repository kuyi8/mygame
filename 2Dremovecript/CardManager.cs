using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    public GameObject CardPre;

    [SerializeField] private GameObject GameOver;

    private List<CardControl>cards = new List<CardControl>();

    private void Awake()
    {
        Instance = this;
    }

    public void Add(string name) 
    {
        GameObject go = Instantiate(CardPre, transform);        //以UIPanel为父对象
        go.GetComponent<CardControl>().Load(name);
        cards.Add(go.GetComponent<CardControl>());

        if (cards.Count > 7)
        {
            Debug.Log("游戏失败");
            if(GameOver != null)
                GameOver.SetActive(true);
        }
        else if (cards.Count > 2)
        {
            List<CardControl> tmpList = cards.GetRange(cards.Count - 3 , 3);
            CardControl card1 = tmpList[0];
            CardControl card2 = tmpList[1];
            CardControl card3 = tmpList[2];

            if (card1.cardName == card2.cardName && card1.cardName == card3.cardName)
            {
                cards.RemoveRange(cards.Count - 3, 3);
                Destroy(card1.gameObject);
                Destroy(card2.gameObject);
                Destroy(card3.gameObject);
            }
        }
    }
}
