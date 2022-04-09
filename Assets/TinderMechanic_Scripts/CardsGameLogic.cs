using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardsGameLogic : MonoBehaviour
{
	public CardsLogic[] _cardsArray;
	[SerializeField] private Transform positionCards;

    private Queue<CardsLogic> _cards = new Queue<CardsLogic>();
	private CardsLogic _currentCard;
	private int numberCards;

	public event UnityAction GoodCardChek;
	
	/// <summary>
	/// Инициализация
	/// </summary>
    void Start()
    {
		InitQueueCards();
		UpdateCard();
	}
	/// <summary>
	/// Перемещение карты по панели
	/// </summary>
	void Update()
	{
		if (Input.GetMouseButton(0) && _currentCard.moveCards == true) {

			Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.y = _currentCard.transform.position.y;
			_currentCard.transform.position = pos;
		} else {
			_currentCard.transform.position = positionCards.position;
		}
		ChekSwaipeCard();
	}

	/// <summary>
	/// Заполнение Очереди(круговое перемещение) из кард
	/// </summary>
	private void InitQueueCards() {
		for (int i = 0; i < _cardsArray.Length; i++) {
			_cards.Enqueue(_cardsArray[i]);
		}
		numberCards = _cardsArray.Length;
	}
	/// <summary>
	/// Создание карты
	/// </summary>
	private void UpdateCard() {
		_currentCard = Instantiate(_cards.Dequeue(), positionCards.position, Quaternion.identity, gameObject.transform);
	}
	/// <summary>
	/// Отслеживание положения карты, изменения состояния Очереди
	/// </summary> 
	private void ChekSwaipeCard() {
		if (_currentCard.transform.position.x < -1.5)
		{
			ChangesCard();
		}
		else if (_currentCard.transform.position.x > 1.5)
		{
			GoodCard();
		}
	}
	/// <summary>
	/// Поменять карту
	/// </summary>
	private void ChangesCard() {
		if (numberCards == 0)
		{
			numberCards = _cardsArray.Length;
		}
		_cards.Enqueue(_cardsArray[numberCards - 1]);
		Destroy(_currentCard.gameObject);
		numberCards--;
		UpdateCard();
	}
	/// <summary>
	/// Отправка карты на проверку
	/// </summary>
	private void GoodCard() {
		PlayerPrefs.SetInt("CardsMenu", _currentCard.GetMenuNumber());
		GoodCardChek?.Invoke();
	}
}
