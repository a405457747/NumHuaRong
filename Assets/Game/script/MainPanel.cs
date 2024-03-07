using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{

    public static MainPanel Inst;



    private void Awake()
    {
        Inst = this;
    }

    public Text replayText;
    public Button repalyBtn;
    public Button linkBtn;
    public Button repalyBtn2;

    // Start is called before the first frame update
    void Start()
    {
        repalyBtn.gameObject.SetActive(false);

        repalyBtn.onClick.AddListener(() => {

          
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        repalyBtn2.onClick.AddListener(() => {


            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        


        linkBtn.onClick.AddListener(() =>
        {
            Application.OpenURL(@"https://afdian.net/order/create?plan_id=4b152128487d11ee9ac95254001e7c00&product_type=0");
        });
    }

    public void Show()
    {
        replayText.text = $"游戏胜利，点击重玩，难度{GameManager.Inst.GetRval()}x{GameManager.Inst.GetRval()},耗时{Time.timeSinceLevelLoad}秒";
        repalyBtn.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
