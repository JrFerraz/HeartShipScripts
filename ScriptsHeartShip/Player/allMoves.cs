using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class allMoves : MonoBehaviour
{
    //fisicasNave
    Rigidbody2D rb2D;
    //Camara
    public Camera cam;
    //Direccion
    Vector3 moveDir;
    Vector2 posicionMouse;

    public GameObject barrera;

    float moveX;
    float moveY;
    float dashAmount = 3f;
    //cooldown
    float nextFireTime = 0f;
    //dash
    bool isDashButtonDown;
    public GameObject dash;
    //evitar colisiones con el dash
    [SerializeField] private LayerMask dashLayerMask;

    //Interfaz relativa
    public Canvas canvasJugador;

    private bool bloqued;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        barrera.GetComponent<SpriteRenderer>().enabled = false;
        bloqued = false;
    }
    private void Start()
    {
        FindObjectOfType<audioManager>().Play("Aparecer");
    }
    void Update()
    {
        captarBloqueo();

        controlMoverse();

        if(!InterfazGame.isPaused) controlDash();

        controlRotation();

        moverInterfaz();

        if (!InterfazGame.isPaused) activarBarrera();

        matarCanvas();
       
    }
    void matarCanvas()
    {
        if (!StatsPlayer.jugador.vivo)
        {
            canvasJugador.gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        moverPlayer();

        rotarPlayer();

        dashPlayer();
    }
    void controlMoverse()
    {
        //devuelven 0,1,-1
        moveX = 0f;
        moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
        }
        //creamos un vector x/y con los valores
        //normalized: solucion al bug de mayor velocidad
        moveDir = new Vector3(moveX, moveY).normalized;
    }

    void captarBloqueo()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, 1.5f);
        if (hit)
        {
            if (hit.transform.gameObject.layer == 15) bloqued = true;
        }
        else bloqued = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 15) bloqued = true;
    }
    void controlDash()
    {
        //Pulsar espacio da permiso al FixedUpdate para dashear
        if (Time.time > nextFireTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                Instantiate(dash, transform.position, Quaternion.identity);
                isDashButtonDown = true;
                nextFireTime = Time.time + StatsPlayer.jugador.miGeneradorVelocidad.cdDash;
            }
        }
    }
    void controlRotation()
    {
        //coge la posicion del mouse respecto a la camara en el mundo
        posicionMouse = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    void moverPlayer()
    {
        //mueves por fisicas el jugador
        rb2D.velocity = moveDir * StatsPlayer.jugador.miGeneradorVelocidad.velocidadNave;
        //rb2D.MovePosition(transform.position + moveDir * StatsPlayer.jugador.velocidad * Time.fixedDeltaTime);
    }
    void rotarPlayer()
    {
        //rotas con fisicas el jugador, en funcion del mouse, sin bugs en los ángulos
        Vector2 lookDir = posicionMouse - rb2D.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb2D.rotation = angle;
    }
    void activarBarrera()
    {
        if (Barrera.castBarrera)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                barrera.GetComponent<SpriteRenderer>().enabled = true;
                barrera.gameObject.GetComponent<CircleCollider2D>().enabled = true;
            }
            else
            {
                barrera.GetComponent<SpriteRenderer>().enabled = false;
                barrera.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
        else
        {
            
            barrera.GetComponent<SpriteRenderer>().enabled = false;
            barrera.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        
    }
    void dashPlayer()
    {
        if (isDashButtonDown)
        {
            //vector que controla el dash
            Vector3 dashPosition = transform.position + moveDir * StatsPlayer.jugador.miGeneradorVelocidad.impulsoDash;
            //comprobamos que no vaya a haber colisiones
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, moveDir, StatsPlayer.jugador.miGeneradorVelocidad.impulsoDash, dashLayerMask);
            if (raycastHit.collider != null)
            {
                dashPosition = raycastHit.point;
            }
            //ejecutamos el dash
            rb2D.MovePosition(dashPosition);
            isDashButtonDown = false;
            Invoke("hacerDashAnim",0.05f);
           
        }
    }
    void hacerDashAnim()
    {
        Instantiate(dash, transform.position, Quaternion.identity);
        FindObjectOfType<audioManager>().Play("Dash");
    }

    void moverInterfaz()
    {
        canvasJugador.transform.position = transform.position;
    }
}
