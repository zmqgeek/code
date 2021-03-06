    access = new Access();
    access.SettingsRepository = MockRepository.GenerateStub<ISettingsRepository>();
    access.ShowRepository = MockRepository.GenerateStub<IShowRepository>();
    access.SettingsRepository.Stub(x => x.GetById(1)).Return(_settings);
    DbAccessProvider dbAccessProvider = new DbAccessProvider();
    dbAccessProvider.DbAccess = access;
    TestModule testModule = new TestModule();
    testModule.DbAccessProvider = dbAccessProvider;
    var kernel = new StandardKernel(testModule);
    target = new OptionsViewModel(kernel);
