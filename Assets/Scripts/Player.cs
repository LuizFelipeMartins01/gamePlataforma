using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocidade = 5f;
    [SerializeField] private float forcaPulo = 5f;
    [SerializeField] private float moveH;
    [SerializeField] private bool noPiso = true;
    [SerializeField] private int pontostotais = 0;
    private TextMeshProUGUI textoPontos;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        textoPontos =  GameObject.Find("Points").GetComponent<TextMeshProUGUI>();
        textoPontos.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        Andar();
        Pular();
    }

    private void Andar()
    {
        moveH = Input.GetAxis("Horizontal");
        transform.position += new Vector3(moveH * Time.deltaTime * velocidade, 0, 0);
        AnimaAndar();
    }

    private void AnimaAndar()
    {
        if (moveH > 0)
        {
            sprite.flipX = false;
            animator.SetBool("Run", true);
        }
        else if (moveH < 0)
        {
            sprite.flipX = true;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void Pular()
    {
        if(Input.GetKeyDown(KeyCode.Space) && noPiso)
        {
            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
            noPiso = false;
            animator.SetBool("Piso", false);
            animator.SetTrigger("Pulo");
        }

        if(rb.linearVelocity.y < 0)
        {
            animator.SetFloat("ValorPulo", rb.linearVelocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Piso"))
        {
            noPiso = true;
            animator.SetBool("Piso", true);
            animator.SetFloat("ValorPulo", 0);
        }


        if(collision.gameObject.CompareTag("Trap"))
        {
            animator.SetTrigger("Hit");
            StartCoroutine(Destruir());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
        {
            noPiso = true;
            animator.SetBool("Piso", true);
            animator.SetFloat("ValorPulo", 0);
        }
      
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
           if(other.CompareTag("Point"))
            {
                other.GetComponent<Animator>().SetTrigger("Sumir");
                Destroy(other.gameObject, 0.5f);
                pontostotais++;
            textoPontos.text = pontostotais.ToString();
            }
        }
        IEnumerator Destruir()
    {
        animator.SetBool("EstaVivo", false);
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }

}
