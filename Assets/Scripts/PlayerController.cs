using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRB;

    /// <summary>
    /// animasyonların basladığı yer  zıplama için bunu kullanıyoruz
    private Animator playerAnim;
    /// death için de kullanabiliyoruz
    /// </summary>



    private float jumpForce = 10;
    public float gravityModifier;

    public bool isOnGround = true;
    public bool gameOver = false;

    public ParticleSystem dirthParticle;
    public ParticleSystem explosionPartical;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    private AudioSource playerAudio;
    private AudioSource backgroundAudio;




    // Start is called before the first frame update
    void Start()
    {

        playerRB = GetComponent<Rigidbody>();



        //player anim için bura 
        playerAnim = GetComponent<Animator>();




        //physics. gravity i garavitymodifier ile çapıp gravity e ekliyor

        Physics.gravity *= gravityModifier;

        playerAudio = GetComponent<AudioSource>();
        backgroundAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
 

    }

    // Update is called once per frame
    void Update()
    {

        //!gameover   da !  false demek oluyor ! not anlamı katıyor
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver )
        {

            playerRB.AddForce(Vector3.up * jumpForce , ForceMode.Impulse);
            isOnGround = false;

            //animasyonu zıplama için tetikliyor.  jump_trig trigger olduğu için settrigger
            playerAnim.SetTrigger("Jump_trig");
            dirthParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //gameObject üzerinde bir collision varsa ve tag i varsa
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            dirthParticle.Play();
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            //Death_b  bool olduğu için setbool  DeathType_int kısmında hangi death efecti olacak seciyoruz.
            playerAnim.SetBool("Death_b",true);
            playerAnim.SetInteger("DeathType_int",1);
            dirthParticle.Stop();
            explosionPartical.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            backgroundAudio.Stop();

        }

      

    }



}
