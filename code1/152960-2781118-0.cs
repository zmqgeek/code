    public void SendMessage()
    {
        using (SmtpClient client = new SmtpClient())
        {
            client.Send(Message);
        }
        DisposeAttachments(); 
    }
