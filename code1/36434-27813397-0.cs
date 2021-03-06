    Namespace Service
      Public Delegate Sub ServiceDelegate(Of T)(Proxy As T)
      Public NotInheritable Class Disposable(Of T)
        Public Shared ChannelFactory As New ChannelFactory(Of T)(Service)
        Public Shared Sub Use(Execute As ServiceDelegate(Of T))
          Dim oProxy As IClientChannel
          oProxy = ChannelFactory.CreateChannel
          Try
            Execute(oProxy)
            oProxy.Close()
          Catch
            oProxy.Abort()
            Throw
          End Try
        End Sub
        Public Shared ReadOnly Property Service As ServiceEndpoint
          Get
            Return New ServiceEndpoint(
              ContractDescription.GetContract(
                GetType(T),
                GetType(ServiceDelegate(Of T))),
              New BasicHttpBinding,
              New EndpointAddress(Utils.WcfUri.ToString))
          End Get
        End Property
      End Class
    End Namespace
