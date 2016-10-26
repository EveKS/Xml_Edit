using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Reflection
{
    [XmlRoot(ElementName = "a")]
    public class A
    {
        [XmlAttribute(AttributeName = "right")]
        public string Right { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "q")]
    public class Q
    {
        [XmlElement(ElementName = "a")]
        public List<A> A { get; set; }
        [XmlAttribute(AttributeName = "text")]
        public string Text { get; set; }
        [XmlAttribute(AttributeName = "src")]
        public string Src { get; set; }
    }

    [XmlRoot(ElementName = "qw")]
    public class Qw
    {
        [XmlElement(ElementName = "q")]
        public Q Q { get; set; }
    }

    [XmlRoot(ElementName = "test")]
    public class Test
    {
        [XmlElement(ElementName = "head")]
        public string Head { get; set; }
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "qw")]
        public Qw Qw { get; set; }
    }
}
