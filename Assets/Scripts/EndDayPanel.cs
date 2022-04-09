using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndDayPanel : MonoBehaviour
{
    [SerializeField] private Text _numberSuccesfulConsumer;
    [SerializeField] private Text _moneyThisDay;
    [SerializeField] private Text _reputationThisDay;

    /// <summary>
    /// Заполнение полей данными
    /// </summary>
    /// <param name="numberConsumers"></param>
    /// <param name="monetThisDay"></param>
    /// <param name="thisDatRep"></param>
    public void SetDataThisDay(int numberConsumers, int monetThisDay, float thisDatRep) {
        _numberSuccesfulConsumer.text = numberConsumers.ToString();
        _moneyThisDay.text = monetThisDay.ToString();
        _reputationThisDay.text = thisDatRep.ToString();
    }
}
