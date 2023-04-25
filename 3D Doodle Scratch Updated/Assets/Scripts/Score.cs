using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public Transform player;

    public TextMeshProUGUI scoreText;

    private int _scoreNum = 0;

    private float _maxYPos = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var currScore = (int) Math.Round(player.position.y + player.position.z);
        if (currScore > _scoreNum)
        {
            scoreText.SetText(currScore.ToString());
            _scoreNum = currScore;
        }

        if (player.position.y > _maxYPos)
        {
            _maxYPos = player.position.y;
        }
        if (player.position.y < _maxYPos - 45)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
