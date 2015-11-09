using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiaolu.Utility.Model.Interface
{
    public interface IXmlSupport<XMLType>
    {
        XMLType ToXML();
        void FromXML(XMLType xmlObj);
    }
}
