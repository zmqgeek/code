    void Application_Start(object sender, EventArgs e) 
    {
        RegisterRoutes(RouteTable.Routes);
    }
    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.Add(new Route
        (
             "{action}.imu"
             , new ImageRouteHandler()
        ));
    }
