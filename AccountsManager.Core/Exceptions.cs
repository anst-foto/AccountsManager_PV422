namespace AccountsManager.Core;

/// <summary>
/// Исключение "Пустая таблица"
/// </summary>
public class EmptyTableException() : Exception("Пустая таблица!");

/// <summary>
/// Исключение "Не удалось десериализовать файл"
/// </summary>
/// <param name="path">Путь к файлу конфигурации</param>
public class FailDeserializeException(string path) : Exception($"Не удалось десериализовать файл {path}!");