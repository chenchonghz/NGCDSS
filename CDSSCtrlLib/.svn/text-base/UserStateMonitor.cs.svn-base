using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace CDSSCtrlLib
{
    public class UserStateMonitor : Component
    {
        private int _timeout = 5000;
        private int _lastActiveTick;
        private Timer _timer;
        private int _interval = 1000;
        private bool _started;
        private bool _active = true;
        private bool _stateChangeEnable = true;

        private static readonly object EventUseStateChanged = new object();
        private static readonly object EventTimerTick = new object();

        public UserStateMonitor()
            : base()
        {
        }

        public UserStateMonitor(IContainer container)
            : base()
        {
            container.Add(this);
        }

        public event TimerTickEventHandler TimerTick
        {
            add { base.Events.AddHandler(EventTimerTick, value); }
            remove { base.Events.RemoveHandler(EventTimerTick, value); }
        }

        public event UserStateChangedEventHandler UserStateChanged
        {
            add { base.Events.AddHandler(EventUseStateChanged, value); }
            remove { base.Events.RemoveHandler(EventUseStateChanged, value); }
        }

        public bool StateChangedEnable
        {
            get { return _stateChangeEnable; }
            set { _stateChangeEnable = value; }
        }

        [DefaultValue(5000)]
        public int Timeout
        {
            get { return _timeout; }
            set
            {
                _timeout = Math.Max(5000, value);
            }
        }

        [DefaultValue(1000)]
        public int Interval
        {
            get { return _interval; }
            set
            {
                _interval = Math.Max(500, value);
                if (_started)
                {
                    Timer.Change(0, _interval);
                }
                else
                {
                    Timer.Change(System.Threading.Timeout.Infinite, _interval);
                }
            }
        }

        private Timer Timer
        {
            get
            {
                if (_timer == null)
                {
                    _timer = new Timer(
                        new TimerCallback(delegate(object obj)
                        {
                            LASTINPUTINFO lii = new LASTINPUTINFO();
                            lii.size = 8;
                            OnTimerTick(new EventArgs());
                            if (GetLastInputInfo(ref lii) && StateChangedEnable)
                            {
                                _lastActiveTick = Math.Max(lii.lastTick, _lastActiveTick);
                                bool active = Environment.TickCount - _lastActiveTick < _timeout;
                                if (_active != active)
                                {
                                    _active = active;
                                    OnUserStateChanged(new UserStateChangedEventArgs(active));
                                }
                            }
                        }),
                        null,
                        System.Threading.Timeout.Infinite,
                        _interval);
                }
                return _timer;
            }
        }

        public void Start()
        {
            if (!_started)
            {
                Timer.Change(_interval, _interval);
                _started = true;
            }
        }

        public void Stop()
        {
            if (_started)
            {
                Timer.Change(System.Threading.Timeout.Infinite, _interval);
                _started = false;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            public int size;
            public int lastTick;
        }

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO lii);

        protected virtual void OnUserStateChanged(UserStateChangedEventArgs e)
        {
            UserStateChangedEventHandler handler =
                base.Events[EventUseStateChanged] as UserStateChangedEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnTimerTick(EventArgs e)
        {
            TimerTickEventHandler handler = base.Events[EventTimerTick] as TimerTickEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                Stop();
                if (_timer != null)
                {
                    _timer.Dispose();
                    _timer = null;
                }
            }
        }
    }

    public delegate void TimerTickEventHandler(
                object sender, EventArgs e);

    public delegate void UserStateChangedEventHandler(
                object sender,
                UserStateChangedEventArgs e);

    public class UserStateChangedEventArgs : EventArgs
    {
        private bool _active;

        public UserStateChangedEventArgs(bool active)
            : base()
        {
            _active = active;
        }

        public bool Active
        {
            get { return _active; }
        }
    }

}
