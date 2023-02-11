namespace DalApi;
using DalApi;
using DO;

using System;
using System.Collections.Generic;
using System.Reflection;
using static DalApi.DalConfig;

public static class Factory
{
    public static IDal? Get()
    {
        //היא תפנה לקובץ הקונפיגורציה בעזרת מחלקת DalConfig
        // תיקח ממנו את שם האלמנט הרצוי שבו יש הערך הרצוי לאתחול
        string dalType = s_dalName
            ?? throw new DalConfigException($"DAL name is not extracted from the configuration");

        //תפנה לרשימת האלמנטים ותיקח מהאלמנט הרצוי את ערכו שהוא שם ה dll שיש לטעון
        //והמחלקה שיש לאתחל
        Dictionary<string, string> data = s_dalPackages[dalType] ??
            throw new DalConfigException($"Package for {dalType} is not found in packages list");

        
        string projectName = data["project"];   
        string namespaceName = data["namespace"];
        string className = data["class"];

        try
        {
            // תטען את ה dll
            Assembly.Load(projectName ?? throw new DalConfigException($"Package {projectName} is null"));
        }
        catch (Exception)
        {
            throw new DalConfigException("Failed to load {dal}.dll package");
        }


        //ע"י שימוש בתכונות של reflection ,תיצור שורת אתחול מתאימה של האובייקט המממש
        //IDAL מסוג
        Type? type = Type.GetType($"{namespaceName}.{className}, {projectName}")
            ?? throw new DalConfigException($"Class {namespaceName}.{projectName} was not found in {projectName}.dll");

        return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?
                   .GetValue(null) as IDal
            ?? throw new DalConfigException($"Class {className} is not singleton or Instance property not found");
    }
}


