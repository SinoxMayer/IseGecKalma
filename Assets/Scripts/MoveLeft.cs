using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20f;
    private float lowerBound = -25f;
    private float awayBound = -25f;
    private PlayerController playerControllerScript;



    // Start is called before the first frame update
    void Start()
    {

        //bu sayede player dan playercontroler scripti alıyoruz.
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            
            transform.Translate(Vector3.left * Time.deltaTime * speed);


        }
      
        




        if (transform.position.y < lowerBound || transform.position.x < awayBound)
        {
            Destroy(gameObject);
        }


    }
}
