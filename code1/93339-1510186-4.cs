    Enumerable.Range(0, Math.Floor(2.52*Math.Sqrt(num)/Math.Log(num))).Aggregate(
        Enumerable.Range(2, num-1).ToList(), 
        (result, index) => { 
            var bp = result[index]; var sqr = bp * bp;
            result.RemoveAll(i => i >= sqr && i % bp == 0); 
            return result; 
        }
    );
