using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raquete : MonoBehaviour {
    [SerializeField]
    private float velocidade = 8;
    private Rigidbody rb;
    private float limite = 3.9f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float movimentoEmY = Input.GetAxis("Vertical");
        Vector3 direcao = transform.position;

        direcao.y += movimentoEmY * velocidade * Time.deltaTime;
        direcao.y = Mathf.Clamp(direcao.y, -limite, limite);

        rb.MovePosition(direcao);
    }
}
