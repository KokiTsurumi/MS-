using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeMoveTexture : MonoBehaviour
{
    public enum TransMoveTexture
    {
        Wavemove,//�g
        CloudMove,//�_
        shipMove
    }
    [SerializeField]
    float scrollSpeed;//�w�i���X�N���[��������X�s�[�h
    [SerializeField]
    float startLine;//�w�i�̃X�N���[�����J�n����ʒu
    [SerializeField]
    float deadLine; //�w�i�̃X�N���[�����I������ʒu
    [SerializeField]
    private TransMoveTexture Transmovetexture;
    [SerializeField]
    float anglex;
    [SerializeField]
    float angley;
    [SerializeField]
    float MoveWith;

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
        if (Transmovetexture == TransMoveTexture.CloudMove)
        {
            transform.Translate(-scrollSpeed, 0, 0); //x���W��scrollSpeed��������

            if (transform.position.x < deadLine) //�����w�i��x���W���deadLine���傫���Ȃ�����
            {
                transform.position = startPosition;//�w�i��startLine�܂Ŗ߂�
            }
        }
        if (Transmovetexture == TransMoveTexture.Wavemove)
        {
            anglex += Time.deltaTime * 2;
            angley += Time.deltaTime * 2;
            transform.Translate(Mathf.Sin(anglex) * MoveWith,
                Mathf.Sin(angley) * MoveWith, 0); //x���W��scrollSpeed��������

            if (transform.position.y < deadLine) //�����w�i��x���W���deadLine���傫���Ȃ�����
            {
                transform.position = startPosition;//�w�i��startLine�܂Ŗ߂�
            }
        }
        if (Transmovetexture == TransMoveTexture.shipMove)
        {
            angley += Time.deltaTime * 2;
            transform.Translate(0,Mathf.Sin(angley) * MoveWith, 0); //x���W��scrollSpeed��������

            if (transform.position.y < deadLine) //�����w�i��x���W���deadLine���傫���Ȃ�����
            {
                transform.position = startPosition;//�w�i��startLine�܂Ŗ߂�
            }
        }
    }
}

