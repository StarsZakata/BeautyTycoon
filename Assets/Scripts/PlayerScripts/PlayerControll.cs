using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControll : MonoBehaviour
{

    private Vector3 TPosition;
   
    private bool isMoving = false;
    private bool enteringDialog = false;
    private bool enemyConsumer = false;
    public void SetEnteringDialog(bool value) {
        enteringDialog = value;
    }
   
    private GameObject myConsumer;

    private Transform _spawnPosition;
    private Transform _playerSellPoint;
    private float _speed;
    private float _distance;

    private float lastClickTime;
    private const float DOUBLE_CLICK_TIME = 0.2f;

    public event UnityAction BeginDialog;

    /// <summary>
    /// Инициализация Игрока
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <param name="playersellpoint"></param>
    /// <param name="speed"></param>
    /// <param name="distance"></param>
    public void InitPlayer(Transform spawnPosition, Transform playersellpoint, float speed, float distance) {
        gameObject.SetActive(true);
        _spawnPosition = spawnPosition;
        _speed = speed;
        _distance = distance;
        _playerSellPoint = playersellpoint;
        transform.position = _spawnPosition.position;
        enteringDialog = false;
    }

    /// <summary>
    /// Отслеживания
    /// 1. Двойного касания по экрану
    /// 2. Нахождение на Ресепшене
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && enteringDialog == false)
        {
            float timeSinceLastClick = Time.time - lastClickTime;
            if (timeSinceLastClick <= DOUBLE_CLICK_TIME)
            {
                TriggerPosition();
            }
            lastClickTime = Time.time;
        }
        
        if (isMoving == true) {
            ItsMove();
        }


        if (enemyConsumer == true && transform.position == _playerSellPoint.position)
        {
            /// TODO /// Начало диалога
            //Invoke("StartDialog", 0.5f);
            float distanceBetweenConsumerPlauer = Vector3.Distance(transform.position, myConsumer.transform.position);
            if (distanceBetweenConsumerPlauer < _distance)
            {
                enemyConsumer = false;
                enteringDialog = true;
                myConsumer.GetComponent<Consumer>().BeginDialog();
                BeginDialog?.Invoke();
            }
            
        }
    }

    /// <summary>
    /// Диалоговое окно
    /// </summary>
    private void StartDialog()
    {
        enteringDialog = true;
        myConsumer.GetComponent<Consumer>().BeginDialog();
        BeginDialog?.Invoke();
    }
    private void EndDialog() {
        transform.position = new Vector2(transform.position.x, transform.position.y + 0.05f);
        enemyConsumer = false;
        enteringDialog = false;
    }

    /// <summary>
    /// Движение и положение Игрока
    /// </summary>
    void TriggerPosition()
    {
        TPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TPosition.z = transform.position.z;
        isMoving = true;
    }
    void ItsMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, TPosition, _speed * Time.deltaTime);
        if (transform.position == TPosition)
        {
            isMoving = false;
        }
    }
    public void StopMove()
    {
        enteringDialog = true;
    }
    public void SetPlayerResecptionPoint()
    {
        transform.position = _playerSellPoint.position;
    }

    /// <summary>
    /// Внутриигровые события
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        /// TODO Ограничение Области Движения Игрока///
        /*
        if (collision.gameObject.TryGetComponent(out Limites limit)) {
            isMoving = false;
        }
        */
        if (collision.gameObject.TryGetComponent(out Consumer enemy)) {
            myConsumer = enemy.gameObject;
            enemyConsumer = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Consumer enemy))
        {
            EndDialog();
        }
    }
}