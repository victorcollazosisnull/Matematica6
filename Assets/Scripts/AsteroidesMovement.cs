using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidesMovement : MonoBehaviour
{
    [Header("Seguimiento")]
    public Transform player;
    [SerializeField] private float speed = 7f;

    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        Vector3 direction = (player.position - transform.position).normalized;

        transform.position += new Vector3(direction.x, 0, 0) * speed * Time.deltaTime;
    }
}
