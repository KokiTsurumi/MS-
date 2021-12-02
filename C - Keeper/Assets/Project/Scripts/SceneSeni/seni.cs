using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class seni : MonoBehaviour
{
    public string NextSceanName;//次のシーンの名前入れさせる

    private static Canvas fadeCanvas;//キャンバス作って

    private static Image fadeImage;//イメージ作って
    
    private static float alpha = 0.0f;//α値

    public static bool isFadeOut = false;//フェードアウトフラグ
    public static bool isFadeIn = false;//フェードアウトフラグ
    //フェードしたい時間（8で割ってる）
    private static float fadeTime = 0.2f;

    static void Init()
    {
        //SceneManager.GetActiveScene().buildIndex
        GameObject FadeCanvasObject = new GameObject("CanvasFade");
        fadeCanvas = FadeCanvasObject.AddComponent<Canvas>();
        FadeCanvasObject.AddComponent<GraphicRaycaster>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        FadeCanvasObject.AddComponent<seni>();
        
        fadeCanvas.sortingOrder = 10;//最前面になるよう

        //フェード用の板生成
        fadeImage = new GameObject("ImageFade").AddComponent<Image>();
        fadeImage.transform.SetParent(fadeCanvas.transform, false);
        fadeImage.rectTransform.anchoredPosition = Vector3.zero;

        fadeImage.rectTransform.sizeDelta = new Vector2(3000, 3000);
    }


    public static void FadeIn()
    {
        if (fadeImage == null) Init();
        fadeImage.color = Color.black;
        isFadeIn = true;
    }
    private static void FadeOut()
    {
   
            if (fadeImage == null) Init();
            fadeImage.color = Color.clear;
            fadeCanvas.enabled = true;
            isFadeOut = true;
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            seni.FadeOut();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            seni.FadeIn();
        }
        if (isFadeIn)
        {
            //経過時間から透明度計算
            alpha -= Time.deltaTime / fadeTime/8;

            //フェードイン終了判定
            if (alpha <= 0.0f)
            {
                isFadeIn = false;
                alpha = 0.0f;
                fadeCanvas.enabled = false;
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
