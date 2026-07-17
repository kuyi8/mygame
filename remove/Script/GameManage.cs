using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public GameObject ItemPref;
    public float startX = -5.2f;
    public float startY = 3f;
    public float speace = 0.5f;
    public int width = 6;
    public int height = 4;
    public int w = 0;
    public int p = 0;

    private string[] names = { "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9" };
    private List<int> cardID = new List<int>();
    private List<ItemControl> items = new List<ItemControl>();

    void Start()
    {
        Shuffle(width, height, cardID);
        Debug.Log("Layer 1");
        if (cardID.Count == width * height)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    float x = startX + j * (1.28f + speace);
                    float y = startY - i * (1.28f + speace);

                    var item = Instantiate(ItemPref, new Vector3(x, y, 0), Quaternion.identity, transform);
                    var sr = item.GetComponent<SpriteRenderer>();
                    sr.sortingOrder = i * width + j;
                    w = cardID[j + (i * width)];
                    string name = names[w];
                    item.GetComponent<ItemControl>().LoadImage(name);
                    items.Add(item.GetComponent<ItemControl>());
                }
            }
        }
        else Debug.LogError($"cardID.Count = {cardID.Count}");

        cardID.Clear();
        Shuffle(width, height, cardID);
        Debug.Log("Layer 2");
        if (cardID.Count == width * height)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    float x = startX + 0.1f + j * (1.28f + speace);
                    float y = startY + 0.2f - i * (1.28f + speace);

                    var item = Instantiate(ItemPref, new Vector3(x, y, 0), Quaternion.identity, transform);
                    var sr = item.GetComponent<SpriteRenderer>();
                    sr.sortingOrder = 100 + i * width + j;
                    p = cardID[j + (i * width)];
                    string name = names[p];
                    item.GetComponent<ItemControl>().LoadImage(name, 1);
                    items.Add(item.GetComponent<ItemControl>());
                }
            }
        }
        else Debug.LogError($"cardID.Count = {cardID.Count}");

        cardID.Clear();
        Shuffle(width, height, cardID);
        Debug.Log("Layer 3");
        if (cardID.Count == width * height)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    float x = startX + 0.2f + j * (1.28f + speace);
                    float y = startY + 0.4f - i * (1.28f + speace);

                    var item = Instantiate(ItemPref, new Vector3(x, y, 0), Quaternion.identity, transform);
                    var sr = item.GetComponent<SpriteRenderer>();
                    sr.sortingOrder = 200 + i * width + j;
                    p = cardID[j + (i * width)];
                    string name = names[p];
                    item.GetComponent<ItemControl>().LoadImage(name, 2);
                    items.Add(item.GetComponent<ItemControl>());
                }
            }
        }
        else Debug.LogError($"cardID.Count = {cardID.Count}");

        foreach (ItemControl item in items)
        {
            item.Check();
            item.handler = tmpItem =>
            {
                if (tmpItem.GetComponent<SpriteRenderer>().color == Color.black)
                {
                    Debug.Log("Blocked");
                    return;
                }
                CardManager.Instance.Add(tmpItem.imageName);
                items.Remove(tmpItem);
                Destroy(tmpItem.gameObject);
                Invoke("Refresh", 0.1f);
            };
        }
    }

    void Refresh()
    {
        foreach (ItemControl item in items)
        {
            item.Check();
        }
    }

    void Shuffle(int width, int height, List<int> cardID)
    {
        cardID.Clear();
        int id;
        if (width % 3 == 0)
        {
            for (int k = 0; k < height; k++)
            {
                for (int i = 0; i < width / 3; i++)
                {
                    id = Random.Range(0, names.Length);

                    for (int j = 0; j < 3; j++)
                    {
                        cardID.Add(id);
                        Debug.Log($"cardID.Count = {cardID.Count}");
                    }
                }
            }
        }
        else if (height % 3 == 0 && width % 3 != 0)
        {
            for (int k = 0; k < width; k++)
            {
                for (int i = 0; i < height / 3; i++)
                {
                    id = Random.Range(0, names.Length);

                    for (int j = 0; j < 3; j++)
                    {
                        cardID.Add(id);
                        Debug.Log($"cardID.Count = {cardID.Count}");
                    }
                }
            }
        }
        else
        {
            Debug.Log("Cannot generate valid level");
            return;
        }

        for (int m = cardID.Count - 1; m > 0; m--)
        {
            if (cardID.Count <= 1)
            { Debug.Log("cardID.Count error, cannot shuffle"); return; }
            Debug.Log($"m = {m}");
            int rangeID = Random.Range(0, m + 1);
            int temp = cardID[m];
            cardID[m] = cardID[rangeID];
            cardID[rangeID] = temp;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] hits = Physics2D.RaycastAll(worldPos, Vector2.zero);

            if (hits.Length > 0)
            {
                ItemControl topItem = null;
                int maxOrder = int.MinValue;

                foreach (var hit in hits)
                {
                    var item = hit.collider.GetComponent<ItemControl>();
                    var sr = hit.collider.GetComponent<SpriteRenderer>();

                    if (item != null && sr != null)
                    {
                        if (sr.sortingOrder > maxOrder)
                        {
                            maxOrder = sr.sortingOrder;
                            topItem = item;
                        }
                    }
                }

                if (topItem != null)
                {
                    topItem.OnClick();
                }
            }
        }
    }
}
