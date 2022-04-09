using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private GameObject PanelCardsMenu;

    [Space(10)]
    [Header("Панель Игрока")]
    [SerializeField] private GameObject PanelPlayer;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private Image _dataAvatarImage;
    [SerializeField] private Text _dataTextRespect;
    [SerializeField] private Text _dataTextMoney;
    [SerializeField] private Text _dataTimeDay;

    [Space(10)]
    [Header("Выбор Аватара")]
    [SerializeField] private GameObject PanelSelectAvatar;

    [Space(20)]
    [Header("Конец дня")]
    [SerializeField] private GameObject PanelEndDay;
    [SerializeField] private Text _dataSuccesfulConsumersThisDay;
    [SerializeField] private Text _dataTextMoneyThisDay;
    [SerializeField] private Text _dataMiddleRespThisDay;

    [Space(10)]
    [Header("Новый покупатель")]
    [SerializeField] private Reception ReceptionNewConsumer;
    [SerializeField] private GameObject _dataNewConsumerImage;


    /// <summary>
    /// Инициализация UI
    /// </summary>
    /// <param name="tmp"></param>
    /// <param name="inittime"></param>
    public void InitUIControll(PlayerScoreData tmp, int inittime)
    {
        InitPlayerData(tmp.money, tmp.Respect, tmp.numberAvatar, inittime);
        PanelCardsMenu.SetActive(false);
        PanelEndDay.SetActive(false);
        _dataNewConsumerImage.SetActive(false);
    }
    /// <summary>
    /// Инициализация Панели Игрока 
    /// </summary>
    /// <param name="money"> Начальное количество денег </param>
    /// <param name="respect"> Начальное количество репутации</param>
    /// <param name="numberAvatar"> Выставленный Аватар</param>
    /// <param name="inittime"> Начальной время игры</param>
    private void InitPlayerData(int money, int respect, int numberAvatar, int inittime)
    {
        PanelPlayer.SetActive(true);
        _dataTextMoney.text = money.ToString();
        _dataTextRespect.text = respect.ToString();
        _dataAvatarImage.sprite = _sprites[numberAvatar];
        _dataTimeDay.text = inittime.ToString() + ":00";

    }

    /// <summary>
    /// Система Событий
    /// </summary>
    private void OnEnable()
    {
        ReceptionNewConsumer.newConsumerByeBegin += MessageNewConsumer;
        ReceptionNewConsumer.leaveConsumer += LeaveConsumer;
    }
    private void OnDisable()
    {
        ReceptionNewConsumer.newConsumerByeBegin -= MessageNewConsumer;
        ReceptionNewConsumer.leaveConsumer -= LeaveConsumer;
    }

    /// <summary>
    /// Наличие нового Покупателя
    /// </summary>
    private void MessageNewConsumer() {
        _dataNewConsumerImage.SetActive(true);
    }
    private void LeaveConsumer() {
        _dataNewConsumerImage.SetActive(false);
        WindowCardsMenu(false);
    }


    /// <summary>
    /// Обновление времени
    /// </summary>
    /// <param name="newtime"></param>
    public void UpdateTime(int newtime) {
        _dataTimeDay.text = newtime.ToString()+":00";
    }
    /// <summary>
    /// Меню выбора карт 
    /// </summary>
    /// <param name="value"></param>
    public void WindowCardsMenu(bool value) {
        PanelCardsMenu.SetActive(value);
    }

    /// <summary>
    /// Измение отображения статистики игрока
    /// </summary>
    /// <param name="money"></param>
    /// <param name="respect"></param>
    public  void ChangesMoneyRespect(int money, int respect) {
        _dataTextMoney.text = money.ToString();
        _dataTextRespect.text = respect.ToString();
    }
  
    /// <summary>
    /// Завершение дня
    /// </summary>
    /// <param name="playerScoreData"></param>
    public void OpenWindowEndDayStat(PlayerScoreData playerScoreData) {
        PanelEndDay.SetActive(true);
        _dataSuccesfulConsumersThisDay.text = playerScoreData.numberConsumer.ToString();
        _dataTextMoneyThisDay.text = playerScoreData.MoneyThisDay.ToString();
        _dataMiddleRespThisDay.text = playerScoreData.MiddleRespect.ToString(); ;
    }
    public void CloseWindowEndDayStat() {
        PanelEndDay.SetActive(false);
    }


    /// <summary>
    /// Установка Аватара Игрока
    /// </summary>
    public void WindowSetAvatar(bool value) {
        PanelSelectAvatar.SetActive(value);
    }
}
