using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Magnet : MonoBehaviour
{
    public float moveSpeed = 10f;    // �������� �̵� �ӵ�
    public float magnetDistance = 15f;    // �ڼ� �ۿ� �Ÿ�

    private Transform player;    // �÷��̾��� ��ġ�� �����ϴ� ����

    void Start()
    {
        // �÷��̾� ������Ʈ�� ã�Ƽ� ����
        player = GameObject.FindGameObjectWithTag("Player").transform;

       
    }

    void update()
    {

        // �����۰� �÷��̾� ������ �Ÿ� ���
        float distance = Vector3.Distance(transform.position, player.position);

        // �Ÿ��� magnetDistance �̳��� ��� �������� �÷��̾� ������ �̵�
        if (distance <= magnetDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
}