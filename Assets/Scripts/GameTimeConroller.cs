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
    /// ������������� �������������� �������
    /// </summary>
    /// <param name="timebeginday"></param>
    /// <param name="timstepsecond"></param>
    public void InitGameTimeControll(int timebeginday, int timstepsecond) {
        _timdeBeginDay = timebeginday;
        _timeStepSecond = timstepsecond;
        NewDay();
    }

    /// <summary>
    /// ������ ������ ���
    /// </summary>
    public void NewDay() {
        _consumerControll.RunSpawnNewConsumer();
        StartCoroutine(BeginHours());
    }

    /// <summary>
    /// ��������� ��������� �������������� �������
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
