using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class seni : MonoBehaviour
{
    public string NextSceanName;//���̃V�[���̖��O���ꂳ����

    private static Canvas fadeCanvas;//�L�����o�X�����

    private static Image fadeImage;//�C���[�W�����
    
    private static float alpha = 0.0f;//���l

    public static bool isFadeOut = false;//�t�F�[�h�A�E�g�t���O

    //�t�F�[�h���������ԁi�P�ʂ͕b�j
    private static float fadeTime = 0.2f;

    //�J�ڐ�̃V�[���ԍ�
    public static int nextScene;

    //�t�F�[�h�p��Canvas��Image����
    // Start is called before the first frame update
    static void Init()
    {
        //SceneManager.GetActiveScene().buildIndex
        GameObject FadeCanvasObject = new GameObject("CanvasFade");
        fadeCanvas = FadeCanvasObject.AddComponent<Canvas>();
        FadeCanvasObject.AddComponent<GraphicRaycaster>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        FadeCanvasObject.AddComponent<seni>();
        
        fadeCanvas.sortingOrder = 10;//�őO�ʂɂȂ�悤

        //�t�F�[�h�p��Image����
        fadeImage = new GameObject("ImageFade").AddComponent<Image>();
        fadeImage.transform.SetParent(fadeCanvas.transform, false);
        fadeImage.rectTransform.anchoredPosition = Vector3.zero;

        fadeImage.rectTransform.sizeDelta = new Vector2(9999, 9999);
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
            if (isFadeOut)
        {
            //�o�ߎ��Ԃ��瓧���x�v�Z
            alpha += Time.deltaTime / fadeTime/8;

            //�t�F�[�h�A�E�g�I������
            if (alpha >= 1.0f)
            {
                isFadeOut = false;
                alpha = 1.0f;

                //���̃V�[��
                    SceneManager.LoadScene(NextSceanName);
                
            }

            //�t�F�[�h�pImage�̐F�E�����x�ݒ�
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }
   
}
