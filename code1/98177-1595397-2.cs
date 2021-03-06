        var query = from SPhysician in dtPhysicianServer.AsEnumerable()
                    join CPhysician in dtPhysicianClient.AsEnumerable()
                    on SPhysician.Field<string>("PhysicianNumber") equals
                        CPhysician.Field<string>("PhysicianNumber")
                    select CPhysician;
        DataTable FilterDt = query.CopyToDataTable();
