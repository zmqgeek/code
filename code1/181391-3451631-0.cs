    NetworkCredential credentials = new NetworkCredential(user, pw, userDomain);
    
    //  This is the client generated by the WCF Service Reference
    AppClient appClient = new AppClient();
    appClient.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
    appClient.ClientCredentials.Windows.ClientCredential = credentials;
    appClient.MyWcfServiceCall();
