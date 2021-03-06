    Help helpDBSession = new Help();
    IEnumerable<Article> articles = null;
    if (lang.ToLower() == "en")
    {
        articles = helpDBSession.Articles.Where(
                       artilce =>  artilce.NameEn.Contains(searchPattern)
        )
    }
    else
    {
        articles = helpDBSession.Articles.Where(
                       artilce => artilce.NameAr.Contains(searchPattern)
        )
    }
    
    if (articles != null && articles.Count() > 0)
    {
        if (lang.ToLower() == "en")
        {
            return articles.ToList().Where(
                artilce => System.Text.RegularExpressions.Regex.Replace(
                    artilce.ContentEn, 
                    "<(.|\n)*?>",String.Empty).Contains(searchPattern)
                )
            );
        }
        else
        {
            return articles.ToList().Where(
                artilce => System.Text.RegularExpressions.Regex.Replace(
                    artilce.ContentAr, 
                    "<(.|\n)*?>",String.Empty).Contains(searchPattern)
                )
            );
         }
    }
