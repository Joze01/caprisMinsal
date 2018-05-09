using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Checkout
/// </summary>
public class Checkout
{
    private string token;
    public Checkout()
    {
        
        //
        // TODO: Add constructor logic here
        //
    }

    public string Token
    {
        get
        {
            return token;
        }

        set
        {
            token = value;
        }
    }
}