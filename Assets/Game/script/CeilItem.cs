using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CeilItem : MonoBehaviour
{
    public int rIdx { get; private set; }
    public int cIdx { get; private set; }
   
    public int number;
    private SpriteRenderer sp;

    private void Awake()
    {
        sp =transform.Find("Square").GetComponent<SpriteRenderer>();
    }


    public void Init(int number,int rIdx,int cIdx)
    {
        this.number = number;
        this.rIdx = rIdx;
        this.cIdx = cIdx;

        UpdateSp(this.number);
    }

    public void updateRandCIdx(int rIdx, int cIdx)
    {
        this.rIdx = rIdx;
        this.cIdx = cIdx;
    }

    private void palySound()
    {
        var aus = GetComponent<AudioSource>();
        aus.clip = Resources.Load<AudioClip>("al/" + this.number + "");
        print(aus.clip);
       aus.Play();
    }

    private void OnMouseDown()
    {
        if (number == 0) return;


        

        var printStr = $"rIdx:{rIdx},cIdx:{cIdx},number:{number}";


       GameManager.Inst.CheckSwap(rIdx, cIdx,number, palySound);
        Debug.Log(printStr);
    }

    public void UpdateSp(int number)
    {
        if (number == 0)
        {
            sp.sprite = null;
        }else
        {
            int idx = number - 1;

            //Debug.Log("item idx is " + idx);
            sp.sprite = GameManager.Inst.mapSps[idx];
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
