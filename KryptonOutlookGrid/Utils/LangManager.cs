﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;

namespace JDHSoftware.Krypton.Toolkit.Utils
{
    /// <summary>
    /// Handle localization (singleton)
    /// </summary>
    public class LangManager
    {
        // Variable locale pour stocker une référence vers l'instance
        private static LangManager mInstance = null;

        private static readonly object mylock = new object();
        private ResourceManager rm;

        private CultureInfo ci;
        //Used for blocking critical sections on updates
        private object locker = new object();

        // Le constructeur est Private
        private LangManager()
        {
            rm = new ResourceManager("JDHSoftware.Krypton.Toolkit.KryptonOutlookGrid.Utils.Lang.Strings", Assembly.GetExecutingAssembly());
            ci = Thread.CurrentThread.CurrentCulture; //CultureInfo.CurrentCulture;
        }

        /// <summary>
        /// Gets or sets the P locker.
        /// </summary>
        /// <value>The P locker.</value>
        public object PLocker
        {
            get { return this.locker; }
            set { this.locker = value; }
        }

        public static LangManager Instance
        {
            get
            {
                if (mInstance == null)
                {
                    lock (mylock)
                    {
                        if (mInstance == null)
                        {
                            mInstance = new LangManager();
                        }
                    }
                }

                return mInstance;
            }
        }

        /// <summary>
        /// Get localized string
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string GetString(string name)
        {
            return rm.GetString(name, ci);
        }
    }
}
