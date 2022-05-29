using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public Rigidbody2D rigidbody;
    public float velocidadeY;

    // Start is called before the first frame update
    void Start()
    {
        this.rigidbody.velocity = new Vector2(0, this.velocidadeY);
    }

    private void Update() {
        Camera camera = Camera.main;
        Vector3 posicaoNaCamera = camera.WorldToViewportPoint(this.transform.position);
        // Saiu da tela pela parte superior
        if (posicaoNaCamera.y > 1) {
            // Destr�i o pr�prio laser
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Inimigo")) {
            // Destr�i o inimigo
            Inimigo inimigo = collider.GetComponent<Inimigo>();
            inimigo.ReceberDano();
            // Destr�i o pr�prio laser
            Destroy(this.gameObject);
        }
    }

}