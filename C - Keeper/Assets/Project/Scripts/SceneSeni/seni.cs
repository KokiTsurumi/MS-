using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class seni : MonoBehaviour
{
    public string NextSceanName;//次のシーンの名前入れさせる
    [SerializeField] Image fadeImage;

    private static float alpha = 1.0f;//α値

    public static bool isFadeOut = false;//フェードアウトフラグ
    public static bool isFadeIn = false;//フェードアウトフラグ
    //フェードしたい時間（8で割ってる）
    private static float fadeTime = 0.2f;

    private void Start()
    {
        FadeIn();
    }
    
    public void FadeIn()
    {    
        fadeImage.color = Color.black;
        isFadeIn = true;

    }

    public void FadeOut()
    {
        fadeImage.color = Color.clear;
        fadeImage.enabled = true;

        isFadeOut = true;
        
        
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    FadeOut();
        //}
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    FadeIn();
        //}

        if (Input.GetMouseButton(0) && SceneManager.GetActiveScene().name != "GameMainScene")
            FadeOut();

        if (isFadeIn)
        {
            
            //経過時間から透明度計算
            alpha -= Time.deltaTime / fadeTime/8;

            //フェードイン終了判定
            if (alpha <= 0.0f)
            {
                isFadeIn = false;
                fadeImage.enabled = false;
                alpha = 0.0f;
            }

            //フェード用Imageの色・透明度設定
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
        else if (isFadeOut)
        {
            //経過時間から透明度計算
            alpha += Time.deltaTime / fadeTime/8;

            //フェードアウト終了判定
            if (alpha >= 1.0f)
            {
                isFadeOut = false;
                alpha = 1.0f;

                //次のシーン
               
                SceneManager.LoadScene(NextSceanName);
                
            }

            //フェード用Imageの色・透明度設定
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }
   
}
