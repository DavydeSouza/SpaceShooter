using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveJogador : MonoBehaviour
{

    public Rigidbody2D rigidbody;
    public float velocidadeMovimento;

    public Laser laserPrefab;
    public float tempoEsperaTiro;
    private float intervaloTiro;

    public Transform[] posicoesArmas;
    private Transform armaAtual;

    private int vidas;

    private FimJogo telaFimJogo;



    // Start is called before the first frame update
    void Start()
    {
        this.vidas = 5;
        this.intervaloTiro = 0;
        this.armaAtual = this.posicoesArmas[0];
        ControladorPontuacao.Pontuacao = 0;

        GameObject fimJogoGameObject = GameObject.FindGameObjectWithTag("TelaFimJogo");
        this.telaFimJogo = fimJogoGameObject.GetComponent<FimJogo>();
        this.telaFimJogo.Esconder();
    }

    // Update is called once per frame
    void Update()
    {
        this.intervaloTiro += Time.deltaTime;
        if (this.intervaloTiro >= this.tempoEsperaTiro) {
            this.intervaloTiro = 0;
            Atirar();
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float velocidadeX = (horizontal * this.velocidadeMovimento);
        float velocidadeY = (vertical * this.velocidadeMovimento);

        this.rigidbody.velocity = new Vector2(velocidadeX, velocidadeY);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Inimigo")) {
            Vida--;
            Inimigo inimigo = collider.GetComponent<Inimigo>();
            inimigo.ReceberDano();
        }
    }

    public int Vida {
        get {
            return this.vidas;
        }
        set {
            this.vidas = value;
            if (this.vidas <= 0) {
                this.vidas = 0;
                this.gameObject.SetActive(false);              
                telaFimJogo.Exibir();
            }
        }
    }

    private void Atirar() {
        Instantiate(this.laserPrefab, this.armaAtual.position, Quaternion.identity);
        if (this.armaAtual == this.posicoesArmas[0]) {
            this.armaAtual = this.posicoesArmas[1];
        } else {
            this.armaAtual = this.posicoesArmas[0];
        }
    }

}
