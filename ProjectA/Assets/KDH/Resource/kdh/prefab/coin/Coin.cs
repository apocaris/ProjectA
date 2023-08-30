using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Coin : MonoBehaviour
{

    public int maxBounce;	// �ñ�� Ƚ��

    public float xForce;	// x�� �� (�� �ָ�)
    public float yForce;	// Y�� �� (�� ����)
    public float gravity;	// �߷� (�������� �ӵ� ����)

    private Vector2 direction;
    private int currentBounce = 0;
    private bool isGrounded = true;

    private float maxHeight;
    private float currentheight;

    public Transform sprite;
    public Transform shadow;

    void Start()
    {
        currentheight = Random.Range(yForce - 1, yForce);
        maxHeight = currentheight;
        Initialize(new Vector2(Random.Range(-xForce, xForce), Random.Range(-xForce, xForce)));
    }

    void Update()
    {

        if (!isGrounded)
        {

            currentheight += -gravity * Time.deltaTime;
            sprite.position += new Vector3(0, currentheight, 0) * Time.deltaTime;
            transform.position += (Vector3)direction * Time.deltaTime;

            float totalVelocity = Mathf.Abs(currentheight) + Mathf.Abs(maxHeight);
            float scaleXY = Mathf.Abs(currentheight) / totalVelocity;
      
            CheckGroundHit();
        }
    }

    void Initialize(Vector2 _direction)
    {
        isGrounded = false;
        maxHeight /= 1.5f;
        direction = _direction;
        currentheight = maxHeight;
        currentBounce++;
    }

    void CheckGroundHit()
    {
        if (sprite.position.y < shadow.position.y)
        {
            sprite.position = shadow.position;
            

            if (currentBounce < maxBounce)
            {
                Initialize(direction / 1.5f);
            }
            else
            {
                isGrounded = true;
            }
        }
    }
}
