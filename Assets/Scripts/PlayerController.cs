using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private int count;
    public Text countText;
    public Text winText;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);

        rb.AddForce(movement*speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            setCountText();
        }
        //origine force point:(-9.75,0,0)
        if (other.gameObject.CompareTag("WestBoost"))
        {
            Vector3 old = rb.velocity;
            Vector3 newVector = new Vector3(old.x * (-1), old.y, old.z);
            Vector3 windForce = new Vector3(150, 150, 0);
            rb.velocity = newVector;
            rb.AddForce(windForce);
        }
        if (other.gameObject.CompareTag("WestBoostSouthSide"))
        {
            Vector3 old = rb.velocity;
            Vector3 newVector = new Vector3(old.x, old.y, old.z*(-1));
            Vector3 windForce = new Vector3(0, 150, -150);
            rb.velocity = newVector;
            rb.AddForce(windForce);
        }
        if (other.gameObject.CompareTag("WestBoostNorthSide"))
        {
            Vector3 old = rb.velocity;
            Vector3 newVector = new Vector3(old.x, old.y, old.z * (-1));
            Vector3 windForce = new Vector3(0, 150, 150);
            rb.velocity = newVector;
            rb.AddForce(windForce);
        }
        if (other.gameObject.CompareTag("WestBoostWestSide"))
        {
            Vector3 old = rb.velocity;
            Vector3 newVector = new Vector3(old.x*(-1), old.y, old.z );
            Vector3 windForce = new Vector3(-150, 150, 0);
            rb.velocity = newVector;
            rb.AddForce(windForce);
        }

        if (other.gameObject.CompareTag("portalEntrance"))
        {
            //position change
            double trans_x=rb.position.x+4.6;
            double trans_y = 0;
            double trans_z = rb.position.z + 5.2;

            //vector3 change
            double oldVector_x = rb.velocity.x;
            double oldVector_y = rb.velocity.y;
            double oldVector_z = rb.velocity.z;


            //new position
            Vector3 temp = new Vector3();
            temp.x = -8;
            temp.y = (float)(trans_x +1.4);
            temp.z = (float)(trans_z-5.2 );
            rb.position = temp;

            //new vector
            Vector3 temp2=new Vector3();
            temp2.x=(float)oldVector_x;
            temp2.y=(float)oldVector_y;
            temp2.z=(float)oldVector_z;
            rb.velocity = temp2;
        }
        

    }

    void OnTriggerStay(Collider other1) 
    {
        if (other1.gameObject.CompareTag("blackHole"))
        {
            Vector3 newAdded = new Vector3();
            newAdded.x = 4 - rb.position.x;
            newAdded.y = 0 - rb.position.y;
            newAdded.z = 4 - rb.position.z;
            double total = System.Math.Sqrt(newAdded.x * newAdded.x + newAdded.y * newAdded.y + newAdded.z * newAdded.z);
            double xPart = newAdded.x / total;
            double yPart = newAdded.y / total;
            double zPart = newAdded.z / total;
            //gravity
            double Drag = 10;
            xPart = xPart * Drag;
            yPart = yPart * Drag;
            zPart = zPart * Drag;
            Vector3 addOnBall = new Vector3();
            addOnBall.x = (float)xPart;
            addOnBall.y = (float)yPart;
            addOnBall.z = (float)zPart;
            rb.AddForce(addOnBall);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("map"))
        {
                //reset rb
                Vector3 temp = new Vector3();
                temp.x = 0;
                temp.y = 0;
                temp.z = 0;
                rb.position = temp;
                Vector3 temp2 = new Vector3();
                temp2.x = 0;
                temp2.y = 0;
                temp2.z = 0;
                rb.velocity = temp2;

        }
        
    }

    void setCountText() 
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12) 
        {
            winText.text = "You win!";
        }
    }

}
