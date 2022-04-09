using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Consumer : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject _panelMenu;
    [SerializeField] private TMP_Text textMenu;
    [SerializeField] private Image totalImage;
    [SerializeField] private Sprite goodImage;
    [SerializeField] private Sprite badImage;

    public Transform ByuPoint;
    public Transform ByeByePoint;

    private bool MoveOne = false;
    private int NumberMenu;
    
    /// <summary>
    /// Выставление начального числа
    /// </summary>
    private void Start()
    {
        _panelMenu.SetActive(false);
        totalImage.enabled = false;
        textMenu.enabled = false;
        textMenu.text = "";
        NumberMenu = Random.RandomRange(1, 3);
        PlayerPrefs.SetInt("ConsMenu", NumberMenu);
    }
    /// <summary>
    /// Перемещение покупателя
    /// </summary>
    private void Update()
    {
        if (MoveOne == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, ByuPoint.position, speed * Time.deltaTime);
        }
        if (MoveOne == true) {
            transform.position = Vector3.MoveTowards(transform.position, ByeByePoint.position, speed * Time.deltaTime);
        }
        if (transform.position == ByeByePoint.position) {
            Destroy(gameObject);
            PlayerPrefs.DeleteAll();
        }
    }

    /// <summary>
    /// Запуск Диалога
    /// </summary>
    public void BeginDialog() {
        //totalImage.enabled = false;
        _panelMenu.SetActive(true);
        textMenu.enabled = true;
        textMenu.text = NumberMenu.ToString();
    }
    /// <summary>
    /// Отображение успеха или неудачи
    /// </summary>
    /// <param name="value"></param>
    public void ExitConsumer(bool value)
    {
        totalImage.enabled = true;
        textMenu.enabled = false;
        if (value == true)
        {
            totalImage.sprite = goodImage;
        }
        else if (value == false) {
            totalImage.sprite = badImage;
        }
        Invoke("GoodBye", 0.5f);
    }
    /// <summary>
    /// Перемещение к точки выхода
    /// </summary>
    private void GoodBye() {
        _panelMenu.SetActive(false);
        MoveOne = true;
    }
}
