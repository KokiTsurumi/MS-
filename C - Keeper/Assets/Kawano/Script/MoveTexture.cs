using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexture : MonoBehaviour
{
    [SerializeField] float scrollSpeed; //�w�i���X�N���[��������X�s�[�h
    [SerializeField] float startLine;//�w�i�̃X�N���[�����J�n����ʒu
    [SerializeField] float deadLine; //�w�i�̃X�N���[�����I������ʒu

    private Vector3 startPosition;//�J�n�n�_�i�߂�ꏊ�j

    void Start()
    {
        startPosition = transform.position;


    }
    void Update()
    {
        Scroll();
    }

    public void Scroll()
    {     
            transform.Translate(-scrollSpeed, 0, 0); //x���W��scrollSpeed��������

            if (transform.position.x < deadLine) //�����w�i��x���W���deadLine���傫���Ȃ�����
            {
                transform.position = startPosition;//�w�i��startLine�܂Ŗ߂�
            }        
        
    }
}

