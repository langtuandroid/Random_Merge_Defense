using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
// public class GaugeBarEffectSystem
// {
//     private MonoBehaviour _owner;
//     [SerializeField] private bool useAnimation = true;

//     [SerializeField] private SlicedFilledImage guageEffectBar;
//     [SerializeField] private SlicedFilledImage guageBar;
//     [SerializeField] private float waitTime = 0.5f;
//     [SerializeField] private float decreaseDuration = 1f;
//     private float _endTime;
//     private Tween _gaugeEffectHandle;
//     private bool _isWaitingDecrease = false;

//     public void Initialize(MonoBehaviour owner)
//     {
//         WoonyMethods.Asserts(owner,
//             (guageEffectBar, nameof(guageEffectBar)),
//             (guageBar, nameof(guageBar)));

//         _owner = owner;

//         guageEffectBar.fillAmount = 1;
//         guageBar.fillAmount = 1;
//     }

//     public void ForceUpdateGuage(float fiilAmountValue)
//     {
//         KillGuageEffectTween();

//         _isWaitingDecrease = false;
//         guageBar.fillAmount = fiilAmountValue;
//         guageEffectBar.fillAmount = fiilAmountValue;
//     }

//     public void UpdateGuage(float fillAmountValue)
//     {
//         guageBar.fillAmount = fillAmountValue;
//         if (Mathf.Approximately(fillAmountValue, 0))
//         {
//             KillGuageEffectTween();
//             guageEffectBar.fillAmount = fillAmountValue;
//             return;
//         }

//         if (useAnimation)
//         {
//             _owner.StartCoroutine(PlayGuageEffect());
//             return;
//         }
//         guageEffectBar.fillAmount = fillAmountValue;
//     }

//     private void KillGuageEffectTween()
//     {
//         if (_gaugeEffectHandle.IsActive())
//         {
//             _gaugeEffectHandle.Kill();
//         }
//     }

//     private IEnumerator PlayGuageEffect()
//     {
//         if (_isWaitingDecrease) yield break;

//         if (!_isWaitingDecrease)
//         {
//             _isWaitingDecrease = true;
//             _endTime = Time.time + waitTime;
//             while (_endTime > Time.time) yield return null;
//             _isWaitingDecrease = false;
//         }

//         KillGuageEffectTween();
//         _gaugeEffectHandle = DOTween.To(
//             getter: () => guageEffectBar.fillAmount,
//             setter: x => guageEffectBar.fillAmount = x,
//             endValue: guageBar.fillAmount,
//             duration: decreaseDuration)
//             .SetLink(_owner.gameObject);
//     }
// }
