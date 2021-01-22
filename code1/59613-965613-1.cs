    DHTestBusinessServiceClient client = new DHTestBusinessServiceClient();
    ServiceEndpoint endpoint = client.Endpoint;
    BindingElementCollection bindingElements = endpoint.Binding.CreateBindingElements();
    SslStreamSecurityBindingElement sslElement = bindingElements.Find<SslStreamSecurityBindingElement>();
    sslElement.RequireClientCertificate = true; //Turn on client certificate validation
    CustomBinding newBinding = new CustomBinding(bindingElements);
    NetTcpBinding oldBinding = (NetTcpBinding)endpoint.Binding;
    newBinding.Namespace = oldBinding.Namespace;
    endpoint.Binding = newBinding;