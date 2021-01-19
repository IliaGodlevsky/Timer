using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace Timer.Model
{
    public class NotifyIconHandler
    {
        public NotifyIconHandler()
        {
            window = System.Windows.Application.Current.MainWindow;

            icon = new NotifyIcon
            {
                Icon = new Icon("stopwatch.ico")
            };

            icon.MouseDoubleClick += NotifyIconDoubleClickEvent;
            window.StateChanged += WindowStateChanged;
        }

        private void NotifyIconDoubleClickEvent(object sender, EventArgs e)
        {
            window.WindowState = WindowState.Normal;
        }

        private void WindowStateChanged(object sender, EventArgs e)
        {
            switch (window.WindowState)
            {
                case WindowState.Minimized:
                    window.ShowInTaskbar = false;
                    icon.Visible = true;
                    break;
                case WindowState.Normal:
                    icon.Visible = false;
                    window.ShowInTaskbar = true;
                    break;
            }
        }

        private readonly NotifyIcon icon;
        private readonly Window window;
    }
}
