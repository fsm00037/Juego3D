using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    float xAxis, yAxis;
    public float velocidad;
    public float velocidaGiro;
    Animator myAnimator;
    public GameObject mano;
    public LayerMask whatisDamaged;
    public LayerMask suelo;
    public GameObject explosion;
    bool sePuedeMover;
    Rigidbody myrb;
    // Start is called before the first frame update
    void Start()
    {
        myrb = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {

        //Variables con la entrada de teclado
        //xAxis = Input.GetAxis("Horizontal");
        //Movimiento
        //Movimiento
        if (sePuedeMover)
        {
            yAxis = Input.GetAxis("Vertical");

        }
        else
        {
            yAxis = 0;
        }
        transform.Translate(xAxis * velocidad * Time.deltaTime, 0, yAxis * velocidad * Time.deltaTime);
        Apuntado();


        myAnimator.SetBool("walking", (yAxis != 0) || (xAxis != 0));
       

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            myAnimator.SetTrigger("attack");
            //CheckAttackHitBox(); lo llamamos desde la animacion
        }
        //SoltarObjetos
        if (Input.GetKeyDown(KeyCode.Q) && mano.transform.childCount == 1)
        {
            mano.transform.GetChild(0).transform.GetComponent<Rigidbody>().isKinematic = false;
            mano.transform.GetChild(0).transform.GetComponent<Collider>().isTrigger = false;
            mano.transform.GetChild(0).transform.GetComponent<Rigidbody>().AddForce(Vector3.forward *1000);
            mano.transform.DetachChildren();
        }
    }
    private void FixedUpdate()
    {
       


        //Animator detecta si la entrada es != 0
    }
    //Coger objetos
    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag == "object" && Input.GetKeyDown(KeyCode.E) && mano.transform.childCount == 0)
        {
            
            collision.transform.position = mano.transform.position;
            collision.transform.rotation = mano.transform.rotation;
            collision.transform.GetComponent<Rigidbody>().isKinematic = true;
            collision.transform.GetComponent<Collider>().isTrigger = true;
            collision.transform.SetParent(mano.transform);
        }
    }
    void Apuntado()
    {
        RaycastHit hit;
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit,Mathf.Infinity,suelo))
        {
            if (hit.collider != null)
            {
                if (Vector3.Distance(transform.position, hit.point) > 1)
                {
                    sePuedeMover = true;
                    transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                }
                else
                {
                    myrb.velocity = Vector3.zero;
                    sePuedeMover = false;
                }

            }
            
        }
    }
    private void CheckAttackHitBox()
    {
        Collider[] detectedObject = Physics.OverlapSphere(mano.transform.GetChild(0).position,mano.transform.GetChild(0).transform.localScale.z,whatisDamaged);
        foreach(Collider col in detectedObject)
        {
            col.gameObject.SetActive(false);
            explosion.transform.position = col.gameObject.transform.position;
            explosion.GetComponent<ParticleSystem>().Play();
            print("Damaged");
        }
       
    }


    
}
