    public class MyBL
    {
        public static void TakeAction()
        {
            try
            {
                //do something
            }
            catch(SpecificDotNetException ex)
            {
                //log, wrap and throw
                MyExceptionManagement.LogException(ex)
                throw new MyApplicationCustomException(ex);
            }
            finally
            {
                //clean up...
            }
        }
    }