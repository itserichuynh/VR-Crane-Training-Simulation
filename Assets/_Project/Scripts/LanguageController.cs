using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class LanguageController : Singleton<LanguageController>
{
    

    public void CheckLanguage()
    {
        /*if(gameController.spokenLanguage=="en")*/
        {
            // show all items w/ "Language EN" tag
            // hide all items w/"Language ES" tag
        }

        /*if(gameController.spokenLanguage=="es")*/
        {
            // show all items w/ "Language ES" tag
            // hide all items w/"Language EN" tag
        }
    }

    public void ChangeLanguage(string spokenLanguage)
    {
        
    }
}
