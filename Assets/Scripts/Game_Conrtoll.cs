using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� ��� �������� ������ ������
/// </summary>
public struct PlayerScoreData {
    public int numberAvatar;
    public int numberConsumer;
    public int money;
    public int MoneyThisDay;
    public int Respect;
    public float RespectThisDay;
    public float MiddleRespect;
}

public class Game_Conrtoll : MonoBehaviour
{
    [Space(10)]
    [Header("�������� ������� ���������")]
    [SerializeField] private PlayerControll _playerConrtoll;
    [SerializeField] private GameTimeConroller _gameTimeControll;
    [SerializeField] private UIController _uiController;
    [SerializeField] private ConsumerControll _consumerControll;
    [SerializeField] private CardsGameLogic _cardsGameLogic;
    [SerializeField] private AvatarSelect _avatarSelect;

    [Space(10)]
    [Header("��������� ��������� ������")]
    [Range(0, 2)] // TODO //
    [SerializeField] private int AvatarNumber;
    [Range(0, 999)]
    [SerializeField] private int Money;
    [Range(0, 100)]
    [SerializeField] private int Respect;
    [Range(0.01f, 0.02f)]
    [SerializeField] private float koefAddMoney;
    [Range(25, 75)]
    [SerializeField] private int CostMagazine;
    
    [Space(10)]
    [Header("������� �����")]
    [Range(10, 19)]
    [SerializeField] private int TimeBeginDay;
    [Range(15, 120)]
    [SerializeField] private int TimeStepSecond;

    [Space(10)]
    [Header("��������� ������")]
    [SerializeField] private Transform StartPosition;
    [SerializeField] private Transform playerSellPoint;
    [Range(2, 5)]
    [SerializeField] private float speedPlayer;
    [Range(1, 3)]
    [SerializeField] private float DistanceBetweenPlayerConsumer;

    public PlayerScoreData _playerScoreData;

    void Start()
    {
        InitializePlayerScoreData();
        SetMyPlayerAvatar();
    }

    /// <summary>
    /// ������������� ������ ������
    /// </summary>
    private void InitializePlayerScoreData()
    {
        //_playerScoreData.numberAvatar = AvatarNumber;
        _playerScoreData.numberConsumer = 0;
        _playerScoreData.money = Money;
        _playerScoreData.MoneyThisDay = 0;
        _playerScoreData.Respect = Respect;
        _playerScoreData.RespectThisDay = 0;
        _playerScoreData.MiddleRespect = 0;
    }

    /// <summary>
    /// ��������� �������
    /// </summary>
    private void SetMyPlayerAvatar()
    {
        _uiController.WindowSetAvatar(true);
    }


    /// <summary>
    /// ������������� ������� ��������� 
    /// </summary>
    private void InitializeGame() {
        _playerConrtoll.InitPlayer(StartPosition, playerSellPoint, speedPlayer, DistanceBetweenPlayerConsumer);
        _gameTimeControll.InitGameTimeControll(TimeBeginDay, TimeStepSecond);
        _uiController.InitUIControll(_playerScoreData, TimeBeginDay);
        //_consumerControll.InitConsumerControll(); // ����� _gameTimeControll
    }

    /// <summary>
    /// ������� �������
    /// </summary>
    private void OnEnable()
    {
        _gameTimeControll.NewHours += UpdateTimeUI;
        _gameTimeControll.EndDay += OpenEndDayStat;

        _playerConrtoll.BeginDialog += OpenDialogWindow;

        _cardsGameLogic.GoodCardChek += CheckCards;
        _avatarSelect.beginGame += SetBeginStartGame;
    }
    private void OnDisable()
    {
        _gameTimeControll.NewHours -= UpdateTimeUI;
        _gameTimeControll.EndDay -= OpenEndDayStat;
        _playerConrtoll.BeginDialog -= OpenDialogWindow;

        _cardsGameLogic.GoodCardChek -= CheckCards;
        _avatarSelect.beginGame -= SetBeginStartGame;
    }

    /// <summary>
    /// ������ �������� ��������
    /// </summary>
    /// <param name="numberavatar"></param>
    private void SetBeginStartGame(int numberavatar) {
        _uiController.WindowSetAvatar(false);
        _playerScoreData.numberAvatar = numberavatar;
        InitializePlayerScoreData();
        InitializeGame();
    }
    /// <summary>
    /// ���� ���������� ���
    /// </summary>
    private void OpenEndDayStat() {
        _playerConrtoll.StopMove();
        CalculateThisDay();
        _uiController.OpenWindowEndDayStat(_playerScoreData);
    }
    /// <summary>
    /// ���������� �������
    /// </summary>
    /// <param name="newtime"></param>
    private void UpdateTimeUI(int newtime) {
        _uiController.UpdateTime(newtime);
    }
    /// <summary>
    /// ���������� ����
    /// </summary>
    private void OpenDialogWindow() {
        _consumerControll.enemy.GetComponent<Consumer>().BeginDialog();
        _uiController.WindowCardsMenu(true);
    }
    /// <summary>
    /// �������� ����������� ����
    /// </summary>
    private void CheckCards() {
        bool succesfulConsumer;
        if (PlayerPrefs.GetInt("CardsMenu") == PlayerPrefs.GetInt("ConsMenu"))
        {
            //Debug.Log("You Win");
            _playerScoreData.numberConsumer++;
            AddMoneyPlayer(10);
            AddRescpetPlayer(3);
            succesfulConsumer = true;
        }
        else {
            //Debug.Log("You Lose");
            AddRescpetPlayer(-5);
            succesfulConsumer = false;
        }
        //Debug.Log("Current = " + _playerScoreData.money + " " + _playerScoreData.Respect);
        //Debug.Log("This Day = " +  _playerScoreData.MoneyThisDay + " " + _playerScoreData.RespectThisDay);
        _uiController.ChangesMoneyRespect(_playerScoreData.money, _playerScoreData.Respect);
        _consumerControll.enemy.GetComponent<Consumer>().ExitConsumer(succesfulConsumer);
        _uiController.WindowCardsMenu(false);    
    }

    /// <summary>
    /// ���������� ������ ������
    /// </summary>
    /// <param name="money"></param>
    private void AddMoneyPlayer(int money) {
        _playerScoreData.money = (int)((koefAddMoney * (float)_playerScoreData.Respect) * money) + _playerScoreData.money;
        _playerScoreData.MoneyThisDay += (int)((koefAddMoney * (float)_playerScoreData.Respect) * money);
    }
    private void AddRescpetPlayer(int respect) {
        if (_playerScoreData.Respect >= 100)
        {
            return;
        }
        else
        {
            _playerScoreData.Respect += respect;
            _playerScoreData.RespectThisDay += respect;
        }
    }
    private void CalculateThisDay() {
        if (_playerScoreData.numberConsumer == 0)
        {
            _playerScoreData.MiddleRespect = _playerScoreData.RespectThisDay;
        }
        else {
            _playerScoreData.MiddleRespect = _playerScoreData.RespectThisDay / (float)_playerScoreData.numberConsumer;
        }
    }

    /// <summary>
    /// ������ ������ ���
    /// </summary>
    public void RunNewDay() {
        ClearPlayerDataScore();
        _uiController.CloseWindowEndDayStat();
        _playerScoreData.money -= CostMagazine;
        _uiController.ChangesMoneyRespect(_playerScoreData.money, _playerScoreData.Respect);

        _playerConrtoll.InitPlayer(StartPosition, playerSellPoint, speedPlayer, DistanceBetweenPlayerConsumer);
        _gameTimeControll.InitGameTimeControll(TimeBeginDay, TimeStepSecond);
        _uiController.UpdateTime(TimeBeginDay);
    }
    /// <summary>
    /// ������ ������ ����������� ���
    /// </summary>
    private void ClearPlayerDataScore() {
        _playerScoreData.numberConsumer = 0;
        _playerScoreData.RespectThisDay = 0;
        _playerScoreData.MoneyThisDay = 0;
    }
}

