using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Scripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraMovement : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public GameObject cam;

    private int coins = 0;
    private GameObject player;
    // Update is called once per frame
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        cam.transform.position = new Vector3(player.transform.position.x + 5, 7.5f, -10f);
    }

    private void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.name == "Brick(Clone)")
                {
                    Destroy(hit.transform.gameObject);
                }

                if (hit.transform.name == "Question(Clone)")
                {
                    coins++;
                    coinText.text = $"Coins: {coins}";
                }
            }
        }
    }
}
