using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //importação para se movimentar entre a telas (Cenas)

public class PlayerController : MonoBehaviour {

    public Animator animacao;
    public Rigidbody2D personagemRigdbody; //Para receber o objeto do personagem da unity
    public int forcaPulo;
    public bool derrapar;

    //verificacao se o personagem está tocando o chao
    public Transform verificaChao; //Pegando as posicoes X,Y,Z
    public bool controlaChao;
    public LayerMask oqehChao;

    //Controlando a derrapada:
    public float tempoDerrapada;
    public float controleTempo;

    //Controlando o obj que vai gerenciar as colisões
    public Transform colisor;

    //Audio:
    public AudioSource audio;
    public AudioClip somPular;
    public AudioClip somDerrapar;

    public UnityEngine.UI.Text txtPontos; //Para pegar o txtPontos do Canvas
    public static int pontuacao;

    // Use this for initialization
    void Start () {
        pontuacao = 0;
        PlayerPrefs.SetInt("pontuacao", pontuacao);
    }
	
	// Update is called once per frame
	void Update () {

        txtPontos.text = pontuacao.ToString(); //passado a pontuacao para o componente visual

        //Criando um objeto para controle de contato do personagem com o Obj Chao:
        controlaChao = Physics2D.OverlapCircle(verificaChao.position, 0.2F, oqehChao); //(posicao inicial, tamanho, layer q deve verificar)
        //overlap intercala entre true e false havendo colisao ou nao de objetos

        //Verificando se apertou o botao de pulo
        if (Input.GetMouseButtonDown(0) && controlaChao == true) //Se apertou o Pular (botao direito do mouse) e o Personagem está tocando o chao
        {
            //Atribuindo o som de pulo ao objeto audio:
            audio.PlayOneShot(somPular);
            personagemRigdbody.AddForce(new Vector2(0, forcaPulo)); //aumentando a força do eixo Y para pular
            if(derrapar == true)
            {
                derrapar = false; //se der o comando de pular no meio da derrapada, cancela a derrapada e executa o pulo
                //aumentando o eixo y do obj que controla as colisoes do personagem
                colisor.position = new Vector3(colisor.position.x, colisor.position.y + 0.25F, colisor.position.z);
            }
            //Debug.Log(""+forcaPulo);
        }
        
        //Verificando se apertou o botao de derrapar
        if (Input.GetMouseButtonDown(1) && controlaChao == true && !derrapar) //Se apertou o Derrapar e nao esta derrapando e o Personagem está tocando o chao
        {
            //Atribuindo o som de derrapar ao objeto audio:
            audio.PlayOneShot(somDerrapar);

            //abaixando o eixo y do obj que controla as colisoes do personagem
            colisor.position = new Vector3(colisor.position.x, colisor.position.y - 0.25F, colisor.position.z);
            derrapar = true;
            controleTempo = 0; //zerando o contador q vai controlar o tempo da derrapada no game            
            //Debug.Log("Derrapar");
        }


        if (derrapar) //se é o comando derrapar
        {
            controleTempo += Time.deltaTime; //Pegando o tempo q passa a cada frame
            if(controleTempo >= tempoDerrapada) //se atingiu o limite de tempo de execucao da derrapada (1s)
            {
                derrapar = false; // para de executar a derrapada
                //aumentando o eixo y do obj que controla as colisoes do personagem
                colisor.position = new Vector3(colisor.position.x, colisor.position.y + 0.25F, colisor.position.z);

            }
        }
        //Passando para a Unity a alteração na variavel que controla o pulo e a derrapada:
        animacao.SetBool("pular", !controlaChao); //é true com obj no chao
        animacao.SetBool("derrapar", derrapar);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Gravando a pontuação obtida na memoria
        PlayerPrefs.SetInt("pontuacao", pontuacao);
        //Verificando se a pontuação obtida é maior que  o recorde:
        if(pontuacao > PlayerPrefs.GetInt("recorde"))
        {
            PlayerPrefs.SetInt("recorde", pontuacao); //Se sim, grava a nova pontuação recorde
        }
        //Se colidir com algum obstaculo, fim de jogo:
        SceneManager.LoadScene("gameover"); //Movemos para a tela de inicio
        //Debug.Log("bateu");
    }
}
