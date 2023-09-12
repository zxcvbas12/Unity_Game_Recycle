using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float PlayerMoveSpeed;

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f;
    private float lastShotTime = 0f;

    // Update is called once per frame
    void Update()
    {
         float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveTo = new Vector3(horizontalInput, verticalInput, 0f);
        transform.position += moveTo * PlayerMoveSpeed * Time.deltaTime;
       
       if (transform.position.x < -2.5f)
       {
            transform.position = new Vector3(-2.5f, transform.position.y, transform.position.z);
       }
       else if(transform.position.x > 2.4f )
       {
            transform.position = new Vector3(2.4f, transform.position.y, transform.position.z);
       }
       else if(transform.position.y < -4.5f)
       {
            transform.position = new Vector3(transform.position.x, -4.5f, transform.position.z);

       }
       else if(transform.position.y > 4.6f)
       {
            transform.position = new Vector3(transform.position.x, 4.6f, transform.position.z);
       }

        if(!GameManager.instance.isGameOver)
        {
            Shoot();           

        }
    }

    void Shoot()
    {
        if(Time.time - lastShotTime > shootInterval)
        {
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Coin")
        {
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    
    public void Upgrade()
    {
        weaponIndex++;
        if(weaponIndex >= weapons.Length)
        {
            weaponIndex = weapons.Length - 1;
        }
    }
}
