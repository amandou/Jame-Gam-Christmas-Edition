using Assets.Scripts.Cards;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using MyBox;
using UnityEngine.Localization.Settings;
using Assets.Scripts.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Letters
{
    public class LetterGenerator : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI TextUI { get; private set; }
        [field: SerializeField] public LocalizedStringTable LocalizedIntroTable { get; private set; }
        [field: SerializeField] public LocalizedStringTable LocalizedDeedsTable { get; private set; }
        [field: SerializeField] public LocalizedStringTable LocalizedWishTable { get; private set; }
        [SerializeField] private PlayerValue player;
        private StringTable _introTable;
        private StringTable _deedsTable;
        private StringTable _wishTable;

        [ButtonMethod]
        public void CreateLetter()
        {
            Debug.Log($"Player Name = {player.Name}, Gender = {player.Gender}");
            StartCoroutine(LoadTables());
        }

        private IEnumerator LoadTables()
        {
            yield return StartCoroutine(LoadTable("Intro"));
            yield return StartCoroutine(LoadTable("Deeds"));
            yield return StartCoroutine(LoadTable("Wish"));
            var gift = _wishTable.GetRandom();
            TextUI.text = _introTable.GetRandom().Value.GetLocalizedString(player);
            TextUI.text += _deedsTable.GetRandom().Value.GetLocalizedString(player);
            TextUI.text += _wishTable.GetRandom().Value.GetLocalizedString(player);
        }

        private IEnumerator LoadTable(string tableName)
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(tableName);
            yield return loadingOperation;
            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                if (tableName.Equals("Intro"))
                {
                    _introTable = loadingOperation.Result;
                }
                else if (tableName.Equals("Deeds"))
                {
                    _deedsTable = loadingOperation.Result;
                }
                else if (tableName.Equals("Wish"))
                {
                    _wishTable = loadingOperation.Result;
                }
            }
            else
            {
                Debug.Log("Could not load String Table\n" + loadingOperation.OperationException.ToString());
            }
        }
    }
}