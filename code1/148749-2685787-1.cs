    public ActionResult Action()
    {
      //get doc somehow
      var model = new ViewModel();
      model.Elements = from x in doc.Descendants("person")
                  select new ViewModelElement
                  {
                    Fname=x.Element("fname").Value,
                    Lname=x.Element("lname").Value
                  };
      return View(model);
    }
