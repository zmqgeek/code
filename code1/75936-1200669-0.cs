    // very paranoid cleanup
    try {varOne.SomeEvent -= onSomeEvent; }
    catch (Exception ex) { Trace.WriteLine(ex); } // best endeavours...
    try { varOne.Dispose(); }
    catch (Exception ex) { Trace.WriteLine(ex); } // best endeavours...
