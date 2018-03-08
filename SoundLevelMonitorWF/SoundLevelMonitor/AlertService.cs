using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundLevelMonitor
{
    public class AlertService
    {
        
        Dictionary<String,bool> stopped = new Dictionary<string, bool>();
        Dictionary<String, long> lastShowed = new Dictionary<String,long>();

        public void ProcessAlert(String process, double val)
        {
            if (!inStopped(process) && val > 0.10 && !inTimeout(process))
            {
                DialogResult res = MessageBox.Show("Someone is talking in " + process + ", press cancel to listen again for someone talking (after timeout), press ok to stop listening", "Skype Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (res == DialogResult.OK)
                {
                    stopped[process] = true;
                }
                if (res == DialogResult.Cancel)
                {
                    stopped[process] = false;
                }
                lastShowed[process] = GetTimestamp();
            }

        }

        public static long GetTimestamp()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public Boolean inTimeout(String process)
        {
            if(lastShowed.ContainsKey(process))
                return GetTimestamp() - lastShowed[process] < 10000;
            return false;
        }

        public Boolean inStopped(String process)
        {
            if(stopped.ContainsKey(process))
                return stopped[process];
            return false;
        }
    }
}
