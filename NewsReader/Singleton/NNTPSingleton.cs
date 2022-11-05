using NewsReader.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NewsReader.Singleton
{
    public class NNTPSingleton : NNTP
    {
        private NNTPSingleton() { }
        private static NNTPSingleton instance = null;

        private object _key = new object();
        public static NNTPSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NNTPSingleton();
                }
                return instance;
            }
        }
    }
}
