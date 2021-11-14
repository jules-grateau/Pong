using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    public GameController gameController;
    public SettingsManager settingsManager;
    public Vector3 initialPosition;
    private float maxY = 4f;
    private float minY = -4f;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameController.isGameStarted && !gameController.isPaused && gameController.BallInstance != null)
        {
            Vector3 ballPosition = gameController.BallInstance.transform.position;
            Vector3 ballVelocity = gameController.BallInstance.GetComponent<Rigidbody2D>().velocity;

            if (ballPosition.y >= maxY)
            {
                ballPosition.y = maxY;
            }
            if (ballPosition.y <= minY)
            {
                ballPosition.y = minY;
            }

            if (ballVelocity.x > 0)
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, ballPosition.y), 
                    settingsManager.computerSpeed * Time.deltaTime);
            else
                transform.position = Vector2.MoveTowards(transform.position, initialPosition, 
                    settingsManager.computerSpeed * Time.deltaTime);
        } 
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }
}
