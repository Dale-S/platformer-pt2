using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;
    public float jump = 8f;
    public float currSpeed = 0;
    public float walk = 6f;
    public float run = 8f;

    public bool isGrounded = true;
    public bool blockHit = false;
    private int coins = 0;
    private int score = 0;
    private bool debounce = false;
    private GameObject tv;
    private bool A = false;
    private bool D = false;
    private bool Space = false;
    private bool Idle = false;
    private bool Run = false;

    public CameraMovement CamScript;
    // Start is called before the first frame update
    void Start()
    {
        tv = GameObject.Find("T_V Head");
    }

    // Update is called once per frame
    void Update()
    {
        //walk
        Rigidbody rbody = GetComponent<Rigidbody>();
        if (currSpeed < walk && Input.GetKey(KeyCode.D))
        {
            Idle = false;
            D = true;
            currSpeed += 0.5f;
            rbody.velocity = new Vector3(currSpeed, rbody.velocity.y, 0f);
        }
        else if (currSpeed >= walk && Input.GetKey(KeyCode.D))
        {
            rbody.velocity = new Vector3(currSpeed, rbody.velocity.y, 0f);
        }

        if (currSpeed > -walk && Input.GetKey(KeyCode.A))
        {
            A = true;
            Idle = false;
            currSpeed -= 0.5f;
            rbody.velocity = new Vector3(currSpeed, rbody.velocity.y, 0f);
        }
        else if (currSpeed <= -walk && Input.GetKey(KeyCode.A))
        {
            rbody.velocity = new Vector3(currSpeed, rbody.velocity.y, 0f);
        }

        //run
        if (currSpeed < run && (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift)))
        {
            Run = true;
            Idle = false;
            currSpeed += 0.5f;
            rbody.velocity = new Vector3(currSpeed, rbody.velocity.y, 0f);
        }
        else if (currSpeed >= run && Input.GetKey(KeyCode.D))
        {
            rbody.velocity = new Vector3(currSpeed, rbody.velocity.y, 0f);
        }

        if (currSpeed > -run && (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)))
        {
            Run = true;
            Idle = false;
            currSpeed -= 0.5f;
            rbody.velocity = new Vector3(currSpeed, rbody.velocity.y, 0f);
        }
        else if (currSpeed <= -run && Input.GetKey(KeyCode.A))
        {
            rbody.velocity = new Vector3(currSpeed, rbody.velocity.y, 0f);
        }
        
        if ((Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.D)))
        {
            Idle = true;
            if (rbody.velocity.x > 0)
            {
                currSpeed -= 0.5f;
                rbody.velocity = new Vector3(currSpeed, rbody.velocity.y, 0f);
            }
            else if(rbody.velocity.x < 0)
            {
                currSpeed += 0.5f;
                rbody.velocity = new Vector3(currSpeed, rbody.velocity.y, 0f);
            }
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Space = true;
            Idle = false;
            rbody.velocity = new Vector3(rbody.velocity.x, jump, 0);
        }

        RaycastHit hit;
        blockHit = Physics.Raycast(transform.position, Vector3.up, out hit, 1f);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f);
        if (blockHit && isGrounded == false)
        {
            if (hit.transform.name == "Brick(Clone)")
            {
                if (debounce == false)
                {
                    Destroy(hit.transform.gameObject);
                    score += 100;
                    scoreText.text = $"Score: \n {score}";
                }
                debounce = true;
            }

            if (hit.transform.name == "Question(Clone)")
            {
                if (debounce == false)
                {
                    score += 100;
                    coins++;
                    coinText.text = $"Coins: {coins}";
                    scoreText.text = $"Score: \n {score}";
                }
                debounce = true;
            }
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded && blockHit)
        {
            if (hit.transform.name == "Brick(Clone)")
            {
                if (debounce == false)
                {
                    Destroy(hit.transform.gameObject);
                    score += 100;
                    scoreText.text = $"Score: \n {score}";
                }
                debounce = true;
            }

            if (hit.transform.name == "Question(Clone)")
            {
                if (debounce == false)
                {
                    score += 100;
                    coins++;
                    coinText.text = $"Coins: {coins}";
                    scoreText.text = $"Score: \n {score}";
                }
                debounce = true;
            }
        }
        
        if (!blockHit)
        {
            debounce = false;
        }

        if (Input.GetKeyUp(KeyCode.Space) && Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.D))
        {
            Idle = true;
        }
        else
        {
            Idle = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Space = false;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Run = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            A = false;
            if (Input.GetKeyUp(KeyCode.D))
            {
                Run = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            D = false;
            if (Input.GetKeyUp(KeyCode.A))
            {
                Run = false;
            }
        }
        
        tv.GetComponent<Animator>().SetBool("A", A);
        tv.GetComponent<Animator>().SetBool("D", D);
        tv.GetComponent<Animator>().SetBool("Space", Space);
        tv.GetComponent<Animator>().SetBool("Idle", Idle);
        tv.GetComponent<Animator>().SetBool("Run", Run);
    }
}
