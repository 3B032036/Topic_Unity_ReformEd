using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class level2 : MonoBehaviour
{

    public Dictionary<int, int> arrarys = new Dictionary<int, int>();
    //int[] password;

    public int Answer;
    public GameObject ArrayPanel;
    [SerializeField] private GameObject rowPrefab;
    


    void Start()
    {
        ArrayPanel = GameObject.Find("ArrayPanel");

        //需要設計索引值1~10 各存放不同變數

        for (int i = 0; i < 10; i++)
        {
            //系統生成
            //password[i] = Random.Range(0, 9);
            AddArrarys( i,Random.Range(0, 9));
            print($"第{i}項是{GetArrarys(i)}");

            GameObject obj = Instantiate(rowPrefab, ArrayPanel.transform.Find("IndexPanel"));
            
            obj.transform.Find("Row").GetComponent<Text>().text = $"第{i}列";
            obj.transform.Find("index_value_parent").transform.GetChild(0).GetComponent<Text>().text = $"{GetArrarys(i)}";
            
            //玩家正解
            Answer += GetArrarys(i) * (10 - i);

        }
        print($"{GetAnswer()}");
    }

    void Update()
    {
        
    }

    private void AddArrarys(int arrayIndex, int value)
    {
        if (!arrarys.ContainsKey(arrayIndex))
        {
            arrarys.Add(arrayIndex, value);
        }
        else
        {
            Debug.Log($"{arrayIndex}數值已經存在");
        }
    }

    public int GetArrarys(int arrayIndex)
    {
        if (arrarys.ContainsKey(arrayIndex))
        {
            print($"傳回{arrarys[arrayIndex]}");
            return arrarys[arrayIndex];
        }
        print($"傳回0");
        return 0;
    }

    public int GetAnswer()
    {
        return Answer;
    }
}
