using System;


namespace ReportGenerator.Services
{
    /// <summary>
    /// Класс для передачи объектов между ViewModel
    /// </summary>
    public static class MessageService
    {
        public static event Action<object> Bus;
        public static void Send(object data)
            => Bus?.Invoke(data);
    }
}
