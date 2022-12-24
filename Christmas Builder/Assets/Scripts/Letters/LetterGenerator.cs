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
using static GameManager;
using System;

namespace Assets.Scripts.Letters
{
    public class LetterGenerator : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI TextUI { get; private set; }
        [field: SerializeField] public LocalizedStringTable LocalizedIntroTable { get; private set; }
        [field: SerializeField] public LocalizedStringTable LocalizedDeedsTable { get; private set; }
        [field: SerializeField] public LocalizedStringTable LocalizedWishTable { get; private set; }
        [field: SerializeField] public GiftType WantedType { get; private set; }
        [field: SerializeField] public int ChildAlignment { get; private set; }
        [SerializeField] private PlayerValue player;
        private StringTable _introTable;
        private StringTable _deedsTable;
        private StringTable _wishTable;

        private void Awake()
        {
            player = new PlayerValue();
        }

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
            var gift = _wishTable.GetRandom().Value;
            GetGiftType(gift);
            var deed = _deedsTable.GetRandom().Value;
            GetDeedValue(deed);
            TextUI.text = _introTable.GetRandom().Value.GetLocalizedString(player);
            TextUI.text += deed.GetLocalizedString(player);
            TextUI.text += gift.GetLocalizedString(player);
        }

        private void GetDeedValue(StringTableEntry deed)
        {
            var deedKey = deed.Key[0];
            switch (deedKey)
            {
                case 'W':
                    ChildAlignment = -2;
                    break;

                case 'B':
                    ChildAlignment = -1;
                    break;

                case 'N':
                    ChildAlignment = 0;
                    break;

                case 'G':
                    ChildAlignment = 1;
                    break;

                case 'E':
                    ChildAlignment = 2;
                    break;

                default:
                    Debug.LogError("Wrong Deed Type");
                    break;
            }
        }

        private void GetGiftType(StringTableEntry gift)
        {
            var giftKey = gift.Key[0];
            switch (giftKey)
            {
                case 'T':
                    WantedType = GiftType.Toy;
                    break;

                case 'V':
                    WantedType = GiftType.Videogame;
                    break;

                case 'S':
                    WantedType = GiftType.Sports;
                    break;

                default:
                    Debug.LogError("Wrong Gift Type");
                    break;
            }
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