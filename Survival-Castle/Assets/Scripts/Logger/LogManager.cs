using System;
using System.Collections.Generic;
using UnityEngine;

public class LogManager : MonoBehaviour {

    #region Singleton

    public static LogManager instance;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [Header("Initialization")]
    [SerializeField]
    private GameObject _chatContainer = null;
    [SerializeField]
    private GameObject _logTextPrefab = null;

    [Header("Settings")]
    [SerializeField]
    private bool _isSystemOn = false;
    [SerializeField]
    private Color _logColor = Color.black;
    [SerializeField]
    private Color _exceptionColor = Color.black;
    [SerializeField]
    private Color _errorColor = Color.black;
    [SerializeField]
    private Color _assertColor = Color.black;
    [SerializeField]
    private Color _warningColor = Color.black;

    private Queue<Log> _logs = new Queue<Log>();

    private void Start() {
        Application.logMessageReceived += LogCallback;
    }

    private void OnDestroy() {
        Application.logMessageReceived -= LogCallback;
    }

    public void AddLog(string message, LogType type = LogType.Log) {
        if (!_isSystemOn) {
            return;
        }

        Log log = CreateLogObject(message, type);

        _logs.Enqueue(log);
    }

    private void LogCallback(string condition, string stackTrace, LogType type) {
        if (type == LogType.Log) {
            AddLog(condition, LogType.Log);
        } else {
            AddLog("LogType: " + type, type);
            AddLog("Condition: " + condition, type);
            AddLog("StackTrace: " + stackTrace, type);
        }
    }

    private Color GetLogColor(LogType type) {
        Color color = _logColor;

        switch (type) {
            case LogType.Error:
                color = _errorColor;
                break;
            case LogType.Assert:
                color = _assertColor;
                break;
            case LogType.Warning:
                color = _warningColor;
                break;
            case LogType.Log:
                color = _logColor;
                break;
            case LogType.Exception:
                color = _exceptionColor;
                break;
        }

        return color;
    }

    private Log CreateLogObject(string message, LogType type) {
        string colorStringHEX = "#" + ColorUtility.ToHtmlStringRGB(GetLogColor(type));

        Log log = Instantiate(_logTextPrefab, _chatContainer.transform).GetComponent<Log>();
        log.Init(
            message,
            DateTime.Now,
            colorStringHEX,
            type);

        return log;
    }

}
