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



    private float jumpForce = 20;
    public float gravityModifier;

    public bool isOnGround = true;
    public bool gameOver = false;

    public ParticleSystem dirthParticle;
    public ParticleSystem gunFire;
    public ParticleSystem explosionPartical;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip gun;
    public AudioClip gunReloadSFX;

    private AudioSource playerAudio;
    private AudioSource backgroundAudio;

    public GameObject bullet1, bullet2, bullet3;
    private int currentAmmo;




    // Start is called before the first frame update
    void Start()
    {

        playerRB = GetComponent<Rigidbody>();

        currentAmmo = 0;
        bulletCondition();

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
        else if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo > 0 )
        {
            playerAnim.SetBool("Shoot_b",true);
            playerAnim.SetInteger("WeaponType_int", 1);
            gunFire.Play();
            playerAudio.PlayOneShot(gun,1.0f);

            currentAmmo -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            playerAnim.SetBool("Reload_b", true);
            playerAnim.SetInteger("WeaponType_int", 1);
            Debug.Log("acctiiooonnn");
        }
        AmmoHoldNum();
    }

    private void AmmoHoldNum() 
    {
        if (currentAmmo > 3)
        {
            currentAmmo = 3;
        }
        switch (currentAmmo)
        {
            case 3:
                bullet1.gameObject.SetActive(true);
                bullet2.gameObject.SetActive(true);
                bullet3.gameObject.SetActive(true);
                break;
            case 2:
                bullet1.gameObject.SetActive(true);
                bullet2.gameObject.SetActive(true);
                bullet3.gameObject.SetActive(false);
                break;
            case 1:
                bullet1.gameObject.SetActive(true);
                bullet2.gameObject.SetActive(false);
                bullet3.gameObject.SetActive(false);
                break;
            case 0:
                bullet1.gameObject.SetActive(false);
                bullet2.gameObject.SetActive(false);
                bullet3.gameObject.SetActive(false);
                playerAnim.SetInteger("WeaponType_int", 0);
                break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //ammoya dokununca amma yok oluyor silah doldurma başlıyor
        if (other.gameObject.CompareTag("Ammo") && !gameOver)
        {
            
            playerAnim.SetInteger("WeaponType_int", 1);
 
                playerAnim.SetBool("Reload_b", true);
                playerAudio.PlayOneShot(gunReloadSFX,1.0f);
                StartCoroutine(ReloadAnim());
           
            currentAmmo = 3;
            Debug.Log(currentAmmo);
            Destroy(other.gameObject);
   
        }
    }
    //Reload efecti bitsin diye 
    private IEnumerator ReloadAnim()
    {
        yield return new WaitForSeconds(1f);
        playerAnim.SetBool("Reload_b", false);
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

    void bulletCondition()
    {
        bullet1.gameObject.SetActive(false);
        bullet2.gameObject.SetActive(false);
        bullet3.gameObject.SetActive(false);
    }



}
