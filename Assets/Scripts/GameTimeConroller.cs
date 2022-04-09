using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class GameTimeConroller : MonoBehaviour
{

    [SerializeField] private ConsumerControll _consumerControll;

    public event UnityAction<int> NewHours;
    public event UnityAction EndDay;

    private int _timdeBeginDay;
    private float _timeStepSecond;

    /// <summary>
    /// Инициализация внутриигрового времени
    /// </summary>
    /// <param name="timebeginday"></param>
    /// <param name="timstepsecond"></param>
    public void InitGameTimeControll(int timebeginday, int timstepsecond) {
        _timdeBeginDay = timebeginday;
        _timeStepSecond = timstepsecond;
        NewDay();
    }

    /// <summary>
    /// Начало нового дня
    /// </summary>
    public void NewDay() {
        _consumerControll.RunSpawnNewConsumer();
        StartCoroutine(BeginHours());
    }

    /// <summary>
    /// Коррутина изменения внутриигрового времени
    /// </summary>
    /// <returns></returns>
    private IEnumerator BeginHours() {
        while (true) {
            yield return new WaitForSeconds(_timeStepSecond);
            _timdeBeginDay += 1;
            NewHours?.Invoke(_timdeBeginDay);
            if (_timdeBeginDay == 20) {
                EndDay?.Invoke();
                break;
            }
        }
        _consumerControll.SetStartingSpawn(false);
        _consumerControll.enemy.GetComponent<Consumer>().ExitConsumer(false);
    }
}
