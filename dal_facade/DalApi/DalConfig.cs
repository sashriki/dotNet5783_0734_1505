using DO;
namespace DalApi;

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;  //מרחב שמות Linq.Xml.System כולל כלים לטיפול בקבצי x

static class DalConfig
{
    internal static string? s_dalName;
    internal static Dictionary<string, Dictionary<string, string>> s_dalPackages;

    static DalConfig()
    {
        
        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml")
            ?? throw new DO.DalConfigException("dal-config.xml file is not found"); //to read and to load xml file
        s_dalName = dalConfig?.Element("dal")?.Value
            ?? throw new DO.DalConfigException("<dal> element is missing");  //לקבל את שם האלמנט שממנו צריך לקרוא איזה מימוש לאתחל 
        var packages = dalConfig?.Element("dal-packages")?.Elements()  //לטעון את רשימת כל האלמנטים של כל האתחולים 
            ?? throw new DO.DalConfigException("<dal-packages> element is missing");  
        s_dalPackages = packages.ToDictionary(p => "" + p.Name, p => p.Attributes().ToDictionary(k => k.Name.LocalName, v => v.Value));   // לשמור את כל הרשימה במילון לפי שם אלמנט + ערך 
    }
}

