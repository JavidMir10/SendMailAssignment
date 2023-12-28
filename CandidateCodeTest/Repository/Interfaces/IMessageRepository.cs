using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateCodeTest.Repository
{
    public interface IMessageRepository
    {
        public bool SendEmail();
        //public void SendEmail();
        public int Add(int a, int b);
    }
}
