using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kariMoveobutu : MonoBehaviour
{

    [SerializeField] int counter;
    [SerializeField] float scrollSpeed; //�X�N���[��������X�s�[�h
    [SerializeField] int deadcounter; //�J�E���g�I�����C��

    private Vector3 startPosition;//�J�n�n�_�i�߂�ꏊ�j
    void Start()
    {
        startPosition = transform.position;
       
    }
    void Update()
    {
        Move();
    }
    public void Move()
    {
        transform.Translate(scrollSpeed, 0, 0);
        counter++;
        if (counter == deadcounter)
        {
            counter = 0;
            scrollSpeed *= -1;
        }
    }
}
