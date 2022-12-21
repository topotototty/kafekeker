using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTRA2006_EDITION_KafeKeker
{
    internal interface CRUD
    {
        void Create();
        void Read();
        void Update(int Index);
        void Delete(int Index);
    }
}
