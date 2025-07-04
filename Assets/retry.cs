using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//追記しておこう

public class retry : MonoBehaviour
{
    //リトライするときに基準時間のリセット
    public float new_falltime;
    // Start is called before the first frame update
    void Start()
    {
        new_falltime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Retry()//ここに書いた文字「Retry」が選択画面で表示される
    {
        new_falltime = Time.time;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
