using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager Inst;


    public GameObject CeilItem;

    public List<Sprite> mapSps;

    private int row = 4;
    private int col = 4;
    int totalCeilCount;

    public List<int> numbers;

    //key是number
    private Dictionary<int, CeilItem> dataDict = new Dictionary<int, CeilItem>();


    // 洗牌
    void WashNumbers(List<int> listObj)
    {
        for(int i = listObj.Count - 1; i > 0; i--)
        {
            int randomIdex = Random.Range(0, i + 1);
            int temp = listObj[i];
            listObj[i] = listObj[randomIdex];
            listObj[randomIdex] = temp;
        }
    }

    CeilItem findCeilItem(int rIdx,int cIdx)
    {
        foreach (KeyValuePair<int,CeilItem> kv in dataDict)
        {
            if(kv.Value.rIdx==rIdx  && kv.Value.cIdx == cIdx)
            {
                return kv.Value;
            }
        }
        return null;
    }

    Vector3 getCeilItemPos(int i,int j )
    {

        Vector3 ve = default;

        ve.x = 1 * j;
        ve.y = -1 * i;
        return ve;
    }

    // 检查是否是合法的格子
    bool checkCeilRight(int rIdx,int cIndx)
    {
        if(rIdx<0 || rIdx>=row || cIndx<0 || cIndx >= col)
        {
            return false;
        }

        return true;
    }

    //执行一次是否能找到零的格子
    bool findOneZeroCeil(int rIdx,int cIndex)
    {
        if (checkCeilRight(rIdx, cIndex)==false)
        {
            return false;
        }

        CeilItem ceil = findCeilItem(rIdx, cIndex);

        if(ceil != null)
        {
            return ceil.number == 0;
        }

        return false;
    }

   public (int, int) findFourZeroCel(int rIdx, int cIdx)
    {
        List<(int, int)> dirs = new List<(int, int)>(        )
        {
                   (rIdx + 1, cIdx),
            (rIdx - 1, cIdx),
            (rIdx, cIdx + 1),
            (rIdx, cIdx - 1)
        };

        for (int i = 0; i < dirs.Count; i++)
        {
            (int, int) item = dirs[i];
            if (findOneZeroCeil(item.Item1, item.Item2))
            {
                return item;
            }
        }

        return (999,999);
    }

    //检查是否可以交换呢
    public void CheckSwap(int rIdx, int cIdx,int clickCeilNumber, System.Action palySound)
    {
      (int,int) res=  findFourZeroCel(rIdx, cIdx);

        if(res.Item1!=999)
        
        {
            StartSwap(clickCeilNumber,0);
            palySound?.Invoke();
        }
        else
        {
            Debug.Log("不可以交换哦");
        }
    }

    void StartSwap(int clickNumber, int zeroNumber)
    {
        CeilItem clickItem = dataDict[clickNumber];
        CeilItem zeroItem = dataDict[0];

        Vector3 clickPos = clickItem.transform.position;

        clickItem.transform.position = zeroItem.transform.position;

        zeroItem.transform.position = clickPos;

        Debug.Log("可以交换，位置交换成功了");

        SwapData(clickItem,zeroItem);
    }


    void SwapData(CeilItem clickItem, CeilItem zeroItem)
    {

        var clickRIdx = clickItem.rIdx;
        var clickCIdx = clickItem.cIdx;


        clickItem.updateRandCIdx(zeroItem.rIdx, zeroItem.cIdx);
        zeroItem.updateRandCIdx(clickRIdx, clickCIdx);


        Debug.Log("数据也交换成功了");

        IsWin();
    }



    bool IsWin()
    {
        bool res = false;

        int startNum = 1;
        
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                var ceil = findCeilItem(i, j);

                if (startNum == totalCeilCount)
                {
                    res = ceil.number == 0;
                    continue;
                }

           
                if (ceil.number != startNum)
                {
                    return res;
                }

                startNum += 1;
            }
        }

       
        if (res)
        {
            MainPanel.Inst.Show();
            Debug.Log("游戏胜利");
        }else
        {
           
        }

        return res;
    }

    void InitNumbers(List<int> numbers, int totalCeilCount)
    {
      int   startNum = 1;

        while (startNum < totalCeilCount)
        {
            numbers.Add(startNum);
            startNum += 1;
        }
        numbers.Add(0);
    }
    int rval;
    public int GetRval()
    {
        return rval;
    }
  
    void InitMap()
    {


         rval = Random.Range(3, 6);

        if (Random.value > 0.5)
        {
            rval = 3;
        }

        row = rval;
        col = rval;

        totalCeilCount = row * col;


        numbers = new List<int>() { };
        InitNumbers(numbers, totalCeilCount);
        WashNumbers(numbers);

        int numberIdx = 0;
        for(int i = 0; i < row; i++)
        {
            for(int j = 0; j < col; j++)
            {
                Vector3 pos = getCeilItemPos(i, j);// new Vector3(i, j, 0);
                var go = GameObject.Instantiate(CeilItem, pos, Quaternion.identity);
                var ceilItem = go.GetComponent<CeilItem>();
                int number = numbers[numberIdx];
                ceilItem.Init(number,i, j);
                dataDict.Add(number, ceilItem);

                numberIdx += 1;

            }
        }
    }

    private void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
