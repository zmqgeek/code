    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;
    public class AnimatedScrollFlowLayoutPanel : FlowLayoutPanel
    {
        public new void ScrollControlIntoView(Control activeControl)
        {
            Rectangle clientRectangle = base.ClientRectangle;
            MethodInfo isDescendantMethod = typeof(Control).GetMethod(
                "IsDescendant", BindingFlags.NonPublic | BindingFlags.Instance);
            bool isDescendant = (bool)isDescendantMethod.Invoke(
                this, new object[] { activeControl });
            if (((isDescendant && this.AutoScroll) && (this.HScroll || this.VScroll)) && 
                (((activeControl != null) && (clientRectangle.Width > 0)) && 
                (clientRectangle.Height > 0)))
            {
                Point point = this.ScrollToControl(activeControl);
                int x = clientRectangle.Left, y = clientRectangle.Top;
                bool scrollUp = x < point.Y;
                bool scrollLeft = y < point.X;
                Timer timer = new Timer();
                timer.Tick += new EventHandler(delegate {
                    int jumpInterval = ClientRectangle.Height / 10;
                    if (x != point.X || y != point.Y)
                    {                       
                        if (scrollUp)
                        {
                            y = Math.Min(point.Y, y + jumpInterval);
                        }
                        else
                        {
                            y = Math.Max(point.Y, y - jumpInterval);
                        }
                        if (scrollLeft)
                        {
                            x = Math.Min(point.X, x + jumpInterval);
                        }
                        else
                        {
                            x = Math.Max(point.X, x - jumpInterval);
                        }
                        this.SetScrollState(8, false);
                        this.SetDisplayRectLocation(x, y);
                        MethodInfo syncScrollbarsMethod = 
                            typeof(ScrollableControl).GetMethod("SyncScrollbars", 
                            BindingFlags.NonPublic | BindingFlags.Instance);
                        syncScrollbarsMethod.Invoke(this, new object[] { true });
                    }
                    else
                    {
                        timer.Stop();
                    }
                });
                timer.Interval = 5;
                timer.Start();
            }
        }
    }
