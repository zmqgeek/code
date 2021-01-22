      //
      // Arrange
      //
      var mockFoo = MockRepository.GenerateMock<Foo>();
      mockFoo.GetRepository().Ordered();
      
      var expected = ...;
      var classToTest = new ClassToTest( mockFoo );
      // 
      // Act
      //
      var actual = classToTest.BarMethod();
      
      //  
      // Assert
      //
      Assert.AreEqual( expected, actual );
     mockFoo.VerifyAllExpectations();
      
