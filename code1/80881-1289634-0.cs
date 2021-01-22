    IQueryable<Committee> GetCommittees(int? committeeID, int? employeeID, int? committeeTypeID){
        IQueryable<Committee> result = db.Committees.AsQueryable();
        if(committeeID.HasValue)
        {
            result = result.Where(c => c.ID = committeeID);
        }
        else if(employeeID.HasValue)
        {
            result = result
                .Where(committee => committee.Employees
                    .Any(e => employeeID == e.ID)
                );
        }
        else if(committeeTypeID.HasValue)
        {
            result = result
                .Where(committee => committee.CommitteeTypes
                    .Any(ct => committeeTypeID == ct.ID)
                );
        }
        return result;
    }
