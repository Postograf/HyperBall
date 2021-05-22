using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class BonusCollector : MonoBehaviour
{
    [SerializeField] private TMP_Text _view;

    private int _bonusCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bonus bonus))
        {
            Destroy(bonus.gameObject);
            _bonusCount++;

            _view.text = _bonusCount.ToString();
        }
    }
}
