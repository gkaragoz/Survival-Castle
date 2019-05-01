using System;
using TMPro;
using UnityEngine;

public class Log : Menu {

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private string _message;
    [SerializeField]
    [Utils.ReadOnly]
    private DateTime _dateTime;
    [SerializeField]
    [Utils.ReadOnly]
    private string _colorStringHEX;
    [SerializeField]
    [Utils.ReadOnly]
    private string _tracingString;
    [SerializeField]
    [Utils.ReadOnly]
    private LogType _logType;
    [SerializeField]
    [Utils.ReadOnly]
    private TextMeshProUGUI _txtLog;

    private void Awake() {
        _txtLog = GetComponent<TextMeshProUGUI>();
    }

    public void Init(string message, DateTime dateTime, string colorStringHEX, LogType type) {
        this._message = message;
        this._dateTime = dateTime;
        this._colorStringHEX = colorStringHEX;
        this._tracingString = System.Environment.StackTrace;
        this._logType = type;

        _txtLog.text = "<color=" + this._colorStringHEX + ">" + "[" + this._dateTime.ToLongTimeString() + "] " + this._message;
    }

    public string GetMessage() {
        return this._message;
    }

    public string GetFormattedDateTime() {
        return string.Format("{0:d/M/yyyy HH:mm:ss}", this._dateTime);
    }

    public string GetTracingString() {
        return _tracingString;
    }

}
