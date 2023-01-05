using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Cards
{
    public class CardLoader : MonoBehaviour
    {
        private StringTable _cardsTable;
        [field: SerializeField] public List<CardDataSO> Cards { get; private set; }

        private void OnEnable()
        {
            LocalizationSettings.SelectedLocaleChanged += LoadCard;
            StartCoroutine(LoadTables());
        }

        private void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= LoadCard;
        }

        private void LoadCard(Locale locale)
        {
            StartCoroutine(LoadTables());
        }

        private IEnumerator LoadTables()
        {
            yield return StartCoroutine(LoadTable());
            foreach (var card in Cards)
            {
                card.Description = _cardsTable[card.Name].GetLocalizedString();
            }
        }

        private IEnumerator LoadTable()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync("Cards");
            yield return loadingOperation;
            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                _cardsTable = loadingOperation.Result;
            }
        }
    }
}