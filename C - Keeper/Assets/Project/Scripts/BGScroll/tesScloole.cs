using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�w�i�̃T�C�Y����920�c242.5�̎��c�����������������̍���D���ɂ������悤�������܂��B
public class tesScloole : MonoBehaviour
{
    [SerializeField] float scrollSpeed; //�w�i���X�N���[��������X�s�[�h
    [SerializeField] float startLine;//�w�i�̃X�N���[�����J�n����ʒu
    [SerializeField] float deadLine; //�w�i�̃X�N���[�����I������ʒu
    [SerializeField] float CanvasY;//�摜�̍���
    //private Vector3 startPosition;//�J�n�n�_�i�߂�ꏊ�j

    void Start()
    {
        //startPosition = transform.position;//���͂Ȃ���΂��̂܂܃|�W�V�������ꂿ�Ⴄ
    }
    void Update()
    {
        Scroll();
    }

    public void Scroll()
    {
        transform.Translate(scrollSpeed, 0, 0); //x���W��scrollSpeed��������

        if (transform.position.x > deadLine) //�����w�i��x���W���deadLine���傫���Ȃ�����
        {
            //transform.position = startPosition;//�w�i��startLine�܂Ŗ߂�
            transform.position = new Vector3(startLine, CanvasY, 0);//�w�i��startLine�܂Ŗ߂�

        }

    }
}

