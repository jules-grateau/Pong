using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameController gameController;
    public SettingsManager settingsManager;
    private Vector3 initialPosition;
    private float maxY = 4f;
    private float minY = -4f;

    private void Start()
    {
        initialPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if(gameController.isGameStarted && !gameController.isPaused)
        {
            Transform paddleTransform = transform;
            float vertAxis = Input.GetAxisRaw("Vertical");
            if ((paddleTransform.position.y <= maxY && paddleTransform.position.y >= minY)
                || (paddleTransform.position.y >= maxY && vertAxis < 0)
                || (paddleTransform.position.y <= minY && vertAxis > 0))
                paddleTransform.Translate(0, (vertAxis * settingsManager.playerSpeed) * Time.deltaTime, 0);
        }
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }
}
