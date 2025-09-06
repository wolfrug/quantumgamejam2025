using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace InkEngine
{
    public class InkArrays : MonoBehaviour
    { // Simply provides some static extensions that lets you create 'string arrays' that can be saved in Ink

        private static char delimiterLeft = '<';
        private static char delimiterRight = '>';
        private static char delimiterMiddle = '^';

        private static char delimiterLeftDictionary = '_';
        private static char delimiterRightDictionary = '_';

        public static string SerializeStrings<T>(List<T> serializableStrings, string startString = "")
        {
            foreach (var str in serializableStrings)
            {
                startString += delimiterLeft + str.ToString() + delimiterRight;
            }
            return startString;
        }
        public static string SerializeDictionaryStrings<T>(List<T> serializableStrings, string startString = "")
        {
            foreach (var str in serializableStrings)
            {
                startString += delimiterLeftDictionary + str.ToString() + delimiterRightDictionary;
            }
            return startString;
        }
        public static List<string> DeSerializeString(string serializedString)
        {
            List<string> returnList = new List<string> { };
            // Checks for anything between delimiter brackets and then sends the first match onward.
            Regex brackets = new Regex(delimiterLeft + ".*?" + delimiterRight);
            MatchCollection matches = brackets.Matches(serializedString);
            {
                if (matches.Count > 0)
                {
                    for (int i = 0; i < matches.Count; i++)
                    {
                        // trim out the actual text
                        string deserializedString = matches[i].Value.Trim(new Char[] { delimiterLeft, delimiterRight, ' ' });
                        returnList.Add(deserializedString);
                    }
                }
            }
            return returnList;
        }
        public static List<string> DeSerializeDictionaryString(string serializedString)
        {
            //Debug.Log("trying to deserialize string " + serializedString);
            List<string> returnList = new List<string> { };
            // Checks for anything between delimiter brackets and then sends the first match onward.
            Regex brackets = new Regex(delimiterLeftDictionary + ".*?" + delimiterRightDictionary);
            MatchCollection matches = brackets.Matches(serializedString);
            {
                if (matches.Count > 0)
                {
                    //Debug.Log("got matches, for example: " + matches[0]);
                    for (int i = 0; i < matches.Count; i++)
                    {
                        // trim out the actual text
                        string deserializedString = matches[i].Value.Trim(new Char[] { delimiterLeftDictionary, delimiterRightDictionary, ' ' });
                        returnList.Add(deserializedString);
                    }
                }
            }
            return returnList;
        }
        public static string SerializeProtoDictionary<T>(List<KeyValuePair<string, T>> keyValuePairs, string startString = "")
        {

            foreach (KeyValuePair<string, T> kvp in keyValuePairs)
            {
                startString = SerializeDictionaryStrings(new List<string> { string.Format("{0}{1}{2}", kvp.Key, delimiterMiddle, kvp.Value) }, startString);
            }
            return startString;
        }
        public static List<KeyValuePair<string, string>> DeSerializeProtoDictionary(string serializedString)
        {
            List<KeyValuePair<string, string>> returnList = new List<KeyValuePair<string, string>> { };
            List<string> deSerializedList = new List<string>(DeSerializeDictionaryString(serializedString));
            //Debug.Log("return deserialized list count: " + deSerializedList.Count);
            foreach (string str in deSerializedList)
            {
                string[] kvp = str.Split(delimiterMiddle);
                if (kvp.Length > 1)
                {
                    returnList.Add(new KeyValuePair<string, string>(kvp[0], kvp[1]));
                }
                else
                {
                    returnList.Add(new KeyValuePair<string, string>("null", kvp[0]));
                }
            }
            return returnList;
        }
        public static List<int> ParseIntList(string serializedString)
        {
            List<int> returnList = new List<int> { };
            List<string> deserializedList = DeSerializeString(serializedString);
            foreach (string str in deserializedList)
            {
                int tryParseInt = 0;
                if (int.TryParse(str, out tryParseInt))
                {
                    returnList.Add(tryParseInt);
                }
                else
                { // Will return null if it can't parse an entry - meaning it is NOT a list of pure ints
                    return null;
                }
            }
            return returnList;
        }
        public static List<float> ParseFloatList(string serializedString)
        {
            List<float> returnList = new List<float> { };
            List<string> deserializedList = DeSerializeString(serializedString);
            foreach (string str in deserializedList)
            {
                float tryParseFloat = 0f;
                if (float.TryParse(str, out tryParseFloat))
                {
                    returnList.Add(tryParseFloat);
                }
                else
                { // Will return null if it can't parse an entry - meaning it is NOT a list of pure ints
                    return null;
                }
            }
            return returnList;
        }
        public static List<KeyValuePair<string, int>> ParseStringIntProtoDictionary(string serializedString)
        {
            if (!IsProtoDictionary(serializedString))
            {
                return null;
            }
            List<KeyValuePair<string, int>> returnList = new List<KeyValuePair<string, int>> { };
            foreach (KeyValuePair<string, string> kvp in DeSerializeProtoDictionary(serializedString))
            {
                int tryInt = 0;
                if (int.TryParse(kvp.Value, out tryInt))
                {
                    returnList.Add(new KeyValuePair<string, int>(kvp.Key, tryInt));
                }
                else
                {
                    return null;
                }
            }
            return returnList;
        }
        public static List<KeyValuePair<string, float>> ParseStringFloatProtoDictionary(string serializedString)
        {
            if (!IsProtoDictionary(serializedString))
            {
                return null;
            }
            List<KeyValuePair<string, float>> returnList = new List<KeyValuePair<string, float>> { };
            foreach (KeyValuePair<string, string> kvp in DeSerializeProtoDictionary(serializedString))
            {
                float tryFloat = 0f;
                if (float.TryParse(kvp.Value, out tryFloat))
                {
                    returnList.Add(new KeyValuePair<string, float>(kvp.Key, tryFloat));
                }
                else
                {
                    return null;
                }
            }
            return returnList;
        }
        public static string GetStringByKey(string key, string serializedString) // use this for protodictionaries, will return "" if does not contain
        {
            List<KeyValuePair<string, string>> checkList = new List<KeyValuePair<string, string>>(DeSerializeProtoDictionary(serializedString));
            //Debug.Log("Checklist length: " + checkList.Count);
            string returnValue = "";
            returnValue += checkList.Find((x) => x.Key == key).Value;
            return returnValue;
        }
        public static bool HasValue(string value, string serializedString) // Can be used with either lists or protodictionaries
        {
            if (IsProtoDictionary(serializedString))
            {
                return GetStringByKey(value, serializedString) != "";
            }
            else
            {
                List<string> checkList = DeSerializeString(serializedString);
                return checkList.Contains(value);
            }
        }
        public static bool IsProtoDictionary(string serializedString)
        {
            return serializedString.Contains(delimiterMiddle.ToString());
        }
        public static int Count(string serializedString)
        {
            if (IsProtoDictionary(serializedString))
            {
                List<KeyValuePair<string, string>> dictionary = DeSerializeProtoDictionary(serializedString);
                return dictionary.Count;
            }
            else
            {
                List<string> list = DeSerializeString(serializedString);
                return list.Count;
            }
        }
    }
}