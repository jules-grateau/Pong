using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public int playerNumber;
    public GameObject gameController;
    private GameController gameControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        gameControllerScript = gameController.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            gameControllerScript.ScoreGoal(playerNumber);
            Destroy(collision.gameObject);
        }
            
    }
}
