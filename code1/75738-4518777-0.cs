    var list = _session.CreateCriteria(typeof(Person))
                   .SetProjection(Projections.ProjectionList()
                   .Add(Projections.Property("FirstName"))
                   .Add(Projections.Property("LastName"))
                   .Add(Projections.Property("Jersey"))
                   .Add(Projections.Property("FortyYard"))
                   .Add(Projections.Property("BenchReps"))
                   .Add(Projections.Property("VertJump"))
                   .Add(Projections.Property("ProShuttle"))
                   .Add(Projections.Property("LongJump"))
                   .Add(Projections.Property("PersonSchoolCollection"))
                    )
                   .SetResultTransformer(new NHibernate.Transform.AliasToBeanResultTransformer(typeof(Person)))
                   .List<Person>();