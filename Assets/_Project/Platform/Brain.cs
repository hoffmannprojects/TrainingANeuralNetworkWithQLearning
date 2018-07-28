using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

#region Helper Classes
public class Replay
{
    public List<double> States { get; set; }
    public double Reward { get; set; }

    public Replay(double zRotation, double ballPositionX, double ballVelocityX, double reward)
    {
        States = new List<double>
        {
            zRotation,
            ballPositionX,
            ballVelocityX
        };
        Reward = reward;
    }
}
#endregion

public class Brain : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject _ball;
    [SerializeField] private GameObject _stats;
    private Text[] _statsTexts;
    private Vector3 _ballStartPosition;

    private Ann _ann;
    // List of past actions and rewards.
    private List<Replay> _replayMemory = new List<Replay>();

    // Reward to associate with actions.
    private float _reward = 0f;
    private int _memoryCapacity = 10000;
    // How much future states affect rewards.
    private float _discount = 0.99f;

    // Chance of picking random action.
    private float _exploreRate = 100f;
    private float _maxExploreRate = 100f;
    private float _minExploreRate = 0.01f;
    // Decay amount for each update.
    private float _exploreDecay = 0.0001f;

    // How many times the ball is dropped.
    private int _failCount = 0;
    private float _timer = 0f;
    private float _maxBalanceTime = 0f;

    // Max angle to apply to tilting each update.
    // Make sure the value is large enough to achieve success.
    private float _tiltSpeed = 0.5f;
    #endregion

    // Use this for initialization
    private void Start()
    {
        _ann = new Ann(3, 2, 1, 6, 0.2);

        _statsTexts = _stats.GetComponentsInChildren<Text>();
        Assert.IsNotNull(_statsTexts);

        Assert.IsNotNull(_ball);
        _ballStartPosition = _ball.transform.position;

        Time.timeScale = 5.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateStats();
    }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;

        var states = new List<double>
        {
            transform.rotation.z,
            _ball.transform.position.x,
            _ball.GetComponent<Rigidbody>().angularVelocity.x
        };
    }

    private void UpdateStats()
    {
        _statsTexts[0].text = "Fails: " + _failCount;
        _statsTexts[1].text = "Decay Rate: " + _exploreRate;
        _statsTexts[2].text = "Last Best Balance: " + _maxBalanceTime;
        _statsTexts[3].text = "This Balance: " + _timer;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown("space"))
        {
            _ball.transform.position = _ballStartPosition;
        }
    }
}
