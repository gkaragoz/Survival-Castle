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

    public Color logColor, exceptionColor, errorColor, assertColor, warningColor;
    public GameObject chatContainer;
    public GameObject logTextPrefab;

    private Queue<Log> _logs = new Queue<Log>();

    private void Start() {
        Application.logMessageReceived += LogCallback;
    }

    private void OnDestroy() {
        Application.logMessageReceived -= LogCallback;
    }

    public void AddLog(string message, LogType type = LogType.Log) {
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
        Color color = logColor;

        switch (type) {
            case LogType.Error:
                color = errorColor;
                break;
            case LogType.Assert:
                color = assertColor;
                break;
            case LogType.Warning:
                color = warningColor;
                break;
            case LogType.Log:
                color = logColor;
                break;
            case LogType.Exception:
                color = exceptionColor;
                break;
        }

        return color;
    }

    private Log CreateLogObject(string message, LogType type) {
        string colorStringHEX = "#" + ColorUtility.ToHtmlStringRGB(GetLogColor(type));

        Log log = Instantiate(logTextPrefab, chatContainer.transform).GetComponent<Log>();
        log.Init(
            message,
            DateTime.Now,
            colorStringHEX,
            type);

        return log;
    }

}
