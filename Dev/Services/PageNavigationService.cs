
using System;
using System.Windows.Controls;

namespace ReportGenerator.Services
{
    /// <summary>
    /// Класс сервиса навигации по страницам
    /// </summary>
    public class PageNavigationService
    {
        public event Action<Page> OnPageChanged;
        
        public void Navigate(Page page)
        {
            OnPageChanged?.Invoke(page);            
        }        
    }
}
