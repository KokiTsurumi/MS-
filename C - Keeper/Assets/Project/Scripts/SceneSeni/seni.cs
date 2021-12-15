using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class seni : MonoBehaviour
{
    public string NextSceanName;//���̃V�[���̖��O���ꂳ����
    [SerializeField] Image fadeImage;

    private static float alpha = 1.0f;//���l

    public static bool isFadeOut = false;//�t�F�[�h�A�E�g�t���O
    public static bool isFadeIn = false;//�t�F�[�h�A�E�g�t���O
    //�t�F�[�h���������ԁi8�Ŋ����Ă�j
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
            
            //�o�ߎ��Ԃ��瓧���x�v�Z
            alpha -= Time.deltaTime / fadeTime/8;

            //�t�F�[�h�C���I������
            if (alpha <= 0.0f)
            {
                isFadeIn = false;
                fadeImage.enabled = false;
                alpha = 0.0f;
            }

            //�t�F�[�h�pImage�̐F�E�����x�ݒ�
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
        else if (isFadeOut)
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
