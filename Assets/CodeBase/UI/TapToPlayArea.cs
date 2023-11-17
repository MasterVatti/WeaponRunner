using System.Collections;
using CodeBase.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI
{
  public class TapToPlayArea : MonoBehaviour
  {
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _text;
    
    private IUIService _uiService;

    [Inject]
    private void Constructor(IUIService uiService)
    {
      _uiService = uiService;
    }

    private void Start()
    {
      _button.onClick.AddListener(OnTapToPlayAreaClicked);
    }

    private void OnTapToPlayAreaClicked()
    {
      _button.onClick.RemoveListener(OnTapToPlayAreaClicked);
      StartCoroutine(DisableText());
      _uiService.OnTapAreaClicked();
    }

    private IEnumerator DisableText()
    {
      while (_text.alpha > 0)
      {
        _text.alpha -= 0.03f;
        yield return new WaitForSeconds(0.02f);
      }
    }
  }
}