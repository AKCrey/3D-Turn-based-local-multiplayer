using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Balancing")]
    public float speed = 13.0f;

    [Header("Controls")]
    public bool canMoveX = false;
    public bool canMoveZ = false;
    public Key shootKey = Key.Space;
    public Key changeGunKey = Key.Tab;

    [Header("BulletData")]
    [SerializeField] private BulletMover bullet;
    public Transform bulletSpawnPoint;
    public float fireRate = 1.0f;
    public bool isShootingContinous = true;

    private Rigidbody rb = null;
    private float fireRateTimer = 0.0f;

    Gun[] guns; //Array of guns

    private void Awake()//Give me the rigid body
    {
        rb = GetComponent<Rigidbody>();
        //health = GetComponent<HealthController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        guns = transform.GetComponentsInChildren<Gun>();//Looks for all the guns in the ship

        foreach (Gun gun in guns)
        {
            gun.isActive = true;
            if (gun.powerUpLevelRequireMent != 0)
            {
                gun.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        Move();

        Shoot();
    }

    private void Velocity(Vector3 direciton)
    {
        //Deltatime moves ONE unit per SECOND. (Time needed to execute one frame)
        //rb.position += direciton * Time.deltaTime * speed;
        //rb.MovePosition(rb.position + direciton * Time.deltaTime * speed);

        rb.velocity = direciton * speed;
    }

    private void Move()
    {
        speed = 13;

        //Storing keys from the keyboard
        var leftKey = Keyboard.current.aKey; // [Key.A]
        var rightKey = Keyboard.current.dKey;
        var upKey = Keyboard.current.wKey;
        var downKey = Keyboard.current.sKey;

        Vector3 inputDirection = Vector3.zero;

        if (canMoveX == true)
        {
            if (leftKey.isPressed)
            {
                inputDirection += Vector3.left;
            }
            if (rightKey.isPressed)
            {
                inputDirection += Vector3.right;
            }
        }
        if (canMoveZ == true)
        {
            if (/*canMoveVertical && */upKey.isPressed)
            {
              
                inputDirection += Vector3.forward;
            }
            if (downKey.isPressed)
            {
                inputDirection += Vector3.back;
            }
        }

        inputDirection.Normalize();
        Velocity(inputDirection);
    }

    private void Shoot()
    {
        if (fireRateTimer > 0.0f) // 1 > 0? 
        {
            fireRateTimer -= Time.deltaTime; // 1 - 0.02
            return;
        }

        var key = Keyboard.current[this.shootKey];
        bool isPressed = false;
        if (isShootingContinous == true)
        {
            isPressed = key.isPressed;
        }
        else
        {
            isPressed = key.wasPressedThisFrame;
        }
        if (key.isPressed)//wasPressedThisFrame to shoot once per press
        {
            fireRateTimer = fireRate;

            foreach (Gun gun in guns)
            {
                if (gun.gameObject.activeSelf)
                {
                    gun.Shoot();
                }
            }
            //var bulletInstance = Instantiate(bullet); //Create a copy of bullet
            //bulletInstance.transform.position = bulletSpawnPoint.position;

            //bullet.gameObject.SetActive(true);
            //this.tranform == playerr position
            //bulletSpawnPoint.position == this.tranformn.position 
            //bullet.transform.parent = null;
        }
    }


}
